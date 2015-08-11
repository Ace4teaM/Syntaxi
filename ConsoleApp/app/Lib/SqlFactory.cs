﻿/*
 *  Interface d'échange entre le model de données et une SGBD
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq;

namespace Lib
{
    // Etat de modification
    public enum EntityState
    {
        Modified,
        Added,
        Deleted,
        Unmodified
    }

    public class EntityReferences<T> where T : IEntity
    {
        //cache des entités
        private List<T> references = new List<T>();
        public List<T> References { get { return references; } }

        // private List<EntityState> changes = new List<EntityState>();//modifs des entités
        //  public List<EntityState> Changes { get { return changes; } }//modifs des entités

        private Dictionary<IEntity, EntityState> changes = new Dictionary<IEntity, EntityState>();
        public Dictionary<IEntity, EntityState> Changes { get { return changes; } }//modifs des entités

        // Obtient l'état d'une entité
        public EntityState GetState(IEntity entity)
        {
            if (this.Changes.ContainsKey(entity))
                return this.Changes[entity];

            return EntityState.Unmodified;
        }

        // Modifie l'état d'une entité
        public void SetState(IEntity entity, EntityState state)
        {
            if (state == EntityState.Unmodified)
            {
                if (this.Changes.ContainsKey(entity))
                    this.Changes.Remove(entity);
                return;
            }

            if (!this.Changes.ContainsKey(entity))
                this.Changes.Add(entity, state);
            else
                this.Changes[entity] = state;
        }

        // recherche une entité dans les references
        public T GetReference(T e)
        {
            if (references.Contains(e))
                return e;
            foreach (T eref in references.OfType<T>())
            {
                if (eref.CompareIdentifier(e) == true)
                {
                    //eref.CopyFrom(e);
                    return eref;
                }
            }
            references.Add(e);
            Changes.Add(e, EntityState.Added);
            return e;
        }

        /* recherche une entité dans les references*/
        public E GetReference<E>(E e) where E : T
        {
            if (references.Contains(e))
                return e;
            foreach (E eref in references.OfType<E>())
            {
                if (eref.CompareIdentifier(e) == true)
                {
                    //eref.CopyFrom(e);
                    return eref;
                }
            }
            references.Add(e);
            return e;
        }
    }

    public class SqlFactory : EntityReferences<IEntity>, IEntityFactory
    {
        private  string connectionString;
        private  int maxPersistantConnection = 4;
        private  SqlConnection con;
        private  List<SqlConnection> unusedConList = new List<SqlConnection>();
        private  List<SqlConnection> usedConList = new List<SqlConnection>();
        public   bool useCachedAssociation = false;
        public   int CommandTimeout = 10;

        public List<IEntity> GetReferences()
        {
            return References;
        }

        public string Name { get { return "SQL SqlFactory " + (con != null ? con.Database : "[NonConnecté]"); } }

        // Ferme toutes les connexions
        public  void CloseConnections()
        {
            if (con != null && con.State == ConnectionState.Open)
                con.Close();

            unusedConList.RemoveAll(c =>
            {
                if (c.State == ConnectionState.Open)
                    c.Close();
                return true;
            });

            usedConList.RemoveAll(c =>
            {
                if (c.State == ConnectionState.Open)
                    c.Close();
                return true;
            });
        }

        // Retourne une nouvelle instance de connexion
        public  SqlConnection GetConnection(bool current=true)
        {
            if (current)
            {
                con = new SqlConnection(connectionString);

                if (con.State != ConnectionState.Open)
                    con.Open();
                return con;
            }

            // Nouvelle connexion
            SqlConnection c;
            if (unusedConList.Count > 0)
            {
                c = unusedConList[unusedConList.Count - 1];
                unusedConList.Remove(c);
            }
            else
            {
                c = new SqlConnection(connectionString);
                c.Open();
            }
            usedConList.Add(c);
            return c;
        }

        // Retourne une nouvelle instance de connexion
        public  void ReleaseConnection(SqlConnection c)
        {
            if (usedConList.Contains(c) == false)
                return;

            // maintient la connexion ouverte pour une utilisation future ?
            if (unusedConList.Count < maxPersistantConnection)
            {
                unusedConList.Add(c);
            }
            else
            {
                if (c.State == ConnectionState.Open)
                    c.Close();
            }

            // supprime de la liste des connexions utilisées
            usedConList.Remove(c);
        }

        // Définit la source de la connexion
        public  void SetConnection(string source)
        {
            connectionString = source;
        }

        // Execute une requete sans resultat
        public  int Query(string query)
        {
            int result;
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = this.CommandTimeout;

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

            return result;
        }

        // Execute une requete avec le premier resultat
        public  object QueryScalar(string query)
        {
            object result;
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = this.CommandTimeout;

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            result = cmd.ExecuteScalar();

            return result;
        }

        // Execute une requete et retourne le reader
        public  void Query(string query, Func<SqlDataReader,int> act)
        {
            SqlDataReader reader;
            SqlConnection conn = GetConnection(false);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = this.CommandTimeout;

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();
            act(reader);
            reader.Close();
            ReleaseConnection(conn);
        }

        // Exécute une requete et intialise les propriétés d'un objet
        public  void QueryObject(string query, object obj)
        {
            SqlDataReader reader;
            SqlConnection conn = GetConnection();//nouvelle connexion pour une possible imbrication
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = this.CommandTimeout;

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                for(int i=0; i<reader.FieldCount; i++){
                    PropertyInfo p = obj.GetType().GetProperty(reader.GetName(i));
                    if (p == null)
                        continue;
                    if (p.PropertyType == reader[i].GetType())
                        p.SetValue(obj, reader[i], null);
                    else
                    {
                        string name = reader.GetName(i);
                        Type a = p.PropertyType;
                        Type b = reader[i].GetType();
                    }
                }
            }
            reader.Close();
        }

        // Convertie une variable de base CLR en type SQL Server
        public static string ParseType(object value)
        {
            if(value == null)
                return "NULL";
        
            Type t = value.GetType();
    
            if (t == typeof(Nullable))
                t = t.BaseType;
        
            switch (t.Name)
            {
                case "Byte":
                case "Int16":
                case "Int32":
                case "Int64":
                case "SByte":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "Decimal":
                case "Single":
                case "Double":
                    return value.ToString().Replace(",", ".");
                case "DateTime":
                    return "'" + (value as DateTime?).Value.ToString("yyyy/MM/dd HH:mm:ss.fff") + "'";
                case "String":
                    return "'" + value.ToString().Replace("'","''")+"'";
                case "Boolean":
                    return ((value as bool?) == true ? "1" : "0");
                default:
                    Console.WriteLine("unknown = "+t.Name);
                    break;
            }
        
            return null;
        }
        
        // Commit les modifications
        public void Commit(IEntity[] entities )
        {
            //begin transaction
            //...

            if(entities != null){
                foreach (var sel in entities)
                {
                    if (this.Changes.ContainsKey(sel))
                    {
                        EntityState state = this.Changes[sel];

                        if (state == EntityState.Modified)
                            (sel as IEntity).Update();
                        else if (state == EntityState.Deleted)
                            (sel as IEntity).Delete();
                        else if (state == EntityState.Added)
                            (sel as IEntity).Insert();
                    }
                }

                foreach (var sel in entities)
                {
                    if (this.Changes.ContainsKey(sel))
                    {
                        this.Changes.Remove(sel);
//TODO                        (sel as Entity).OnPropertyChanged("State");
                    }
                }
            }
            else
            {
                //modifie les entités
                foreach (var es in this.Changes)
                {
                    if (es.Value == EntityState.Modified)
                        (es.Key as IEntity).Update();
                    else if (es.Value == EntityState.Deleted)
                        (es.Key as IEntity).Delete();
                    else if (es.Value == EntityState.Added)
                        (es.Key as IEntity).Insert();
                }

                // ok
                this.Changes.Clear();
            }

            //end transaction
            //...

        }

        // Annule les modifications
        public void Undo(IEntity[] entities)
        {
            //begin transaction
            //...

            //recharge les entités
            if (entities != null)
            {
                foreach (var sel in entities)
                {
                    if (this.Changes.ContainsKey(sel))
                    {
                        (sel as IEntity).Load();
                    }
                }

                foreach (var sel in entities)
                {
                    if (this.Changes.ContainsKey(sel))
                    {
                        this.Changes.Remove(sel);
//TODO                  (sel as Entity).OnPropertyChanged("State");
                    }
                }
            }
            else
            {
                //modifie les entités
                foreach (var es in this.Changes)
                {
                    (es.Key as IEntity).Load();
                }

                // ok
                this.Changes.Clear();
            }

            //end transaction
            //...
        }

        public IEnumerable Factory<T>() where T : IEntity, new() { return new EntityFactory<T>(this); }

        public class EntityFactory<T> : IEnumerable where T : IEntity, new()
        {
            private SqlFactory db;
            private string q;

            public EntityFactory(SqlFactory database, string query = null)
            {
                db = database;
                q = query;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }

            public EntityEnum<T> GetEnumerator()
            {
                return new EntityEnum<T>(db, q);
            }
        }

        public class EntityEnum<T> : IEnumerator where T : IEntity, new()
        {
            private SqlDataReader reader;
            private SqlConnection conn;
            private SqlCommand cmd;
            T entity;
            SqlFactory db;

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            int position = -1;

            public EntityEnum(SqlFactory database,string query=null)
            {
                db = database;

                conn = db.GetConnection(false); // nouvelle connexion pour les requetes imbriquées
                cmd = new SqlCommand();
                cmd.CommandTimeout = db.CommandTimeout;

                cmd.CommandText = String.IsNullOrEmpty(query) ? "SELECT * FROM " + (new T().TableName) : query;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                reader = cmd.ExecuteReader();
            }

            ~EntityEnum()
            {
                db.ReleaseConnection(conn);
            }

            public bool MoveNext()
            {
                if (reader.Read())
                {
                    position++;
                    entity = new T();
                    entity.Factory = db;
                    entity.PickIdentity(reader);
                    entity = db.GetReference(entity); // obtient l'entité en cache
                    entity.PickProperties(reader);
//TODO                    entity.RazPropertyCache();
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                reader = cmd.ExecuteReader();
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public T Current
            {
                get
                {
                    if (position < 0 || entity == null)
                    {
                        throw new InvalidOperationException();
                    }
                    return entity;
                }
            }
        }
    }
}