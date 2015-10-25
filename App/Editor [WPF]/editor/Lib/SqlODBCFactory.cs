/*
 *  Interface d'échange entre le model de données et une SGBD
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq;
using System.Data.Odbc;
using System.Data.Common;

namespace Lib
{
    public class SqlOdbcFactory : IEntityFactory
    {
        private  string connectionString;
        private  int maxPersistantConnection = 4;
        private  OdbcConnection con;
        private  List<OdbcConnection> unusedConList = new List<OdbcConnection>();
        private  List<OdbcConnection> usedConList = new List<OdbcConnection>();
        public   bool useCachedAssociation = false;
        public   int CommandTimeout = 10;
        private IModel model;

        public SqlOdbcFactory(IModel model)
        {
            this.model = model;
        }

        public List<IEntityPersistent> GetReferences()
        {
            return model.Objs.OfType<IEntityPersistent>().ToList();
        }

        public IEntityPersistent GetReference(IEntityPersistent e)
        {
            return model.GetReference(e) as IEntityPersistent;
        }

        public IModel Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Name { get { return "SQL SqlOdbcFactory " + (con != null ? con.Database : "[NonConnecté]"); } }

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
        public  OdbcConnection GetConnection(bool current=true)
        {
            // Connexion en cours
            if (current)
            {
                if (con == null)
                    con = new OdbcConnection(connectionString);

                if (con.State != ConnectionState.Open)
                    con.Open();

                return con;
            }

            // Nouvelle connexion
            OdbcConnection c;
            if (unusedConList.Count > 0)
            {
                c = unusedConList[unusedConList.Count - 1];
                unusedConList.Remove(c);
            }
            else
            {
                c = new OdbcConnection(connectionString);
                c.Open();
            }
            usedConList.Add(c);
            return c;
        }

        // Retourne une nouvelle instance de connexion
        public  void ReleaseConnection(OdbcConnection c)
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
            OdbcConnection conn = GetConnection();
            OdbcCommand cmd = new OdbcCommand();
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
            OdbcConnection conn = GetConnection();
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandTimeout = this.CommandTimeout;

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            result = cmd.ExecuteScalar();

            return result;
        }
        
        // Execute une requete et retourne le reader
        public void Query(string query, Func<DbDataReader, int> act)
        {
            OdbcDataReader reader;
            OdbcConnection conn = GetConnection(false);
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandTimeout = this.CommandTimeout;

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();
            act(reader);
            reader.Close();
            ReleaseConnection(conn);
        }
        
        // Exécute une requete et initialise les propriétés d'une entité de référence
        public IEntityPersistent QueryEntity<T>(string query) where T : IEntityPersistent, new()
        {
            IEntityPersistent entity = null;
            OdbcDataReader reader;
            OdbcConnection conn = GetConnection();//nouvelle connexion pour une possible imbrication
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandTimeout = this.CommandTimeout;

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                entity = new T();
                entity.Factory = this;
                entity.PickIdentity(reader);
                entity = GetReference(entity); // obtient l'entité en cache
                entity.PickProperties(reader);
            }
            reader.Close();

            return entity;
        }

        // Exécute une requete et intialise les propriétés d'un objet
        public  void QueryObject(string query, object obj)
        {
            OdbcDataReader reader;
            OdbcConnection conn = GetConnection();//nouvelle connexion pour une possible imbrication
            OdbcCommand cmd = new OdbcCommand();
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
                        throw new ApplicationException(
                            String.Format("The database field data type ({0}) differs from the entity property type ({1}) in entity '{2}'.",
                            b.Name, a.Name, obj.GetType().Name)
                        );
                    }
                }
            }
            reader.Close();
        }

        // Convertie une variable de base CLR en type SQL Server
        public string ParseType(object value)
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
        
        public IEnumerable Factory<T>(string query = null) where T : IEntityPersistent, new() { return new EntityFactory<T>(this, query); }

        public class EntityFactory<T> : IEnumerable where T : IEntityPersistent, new()
        {
            private SqlOdbcFactory db;
            private string q;

            public EntityFactory(SqlOdbcFactory database, string query = null)
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

        public class EntityEnum<T> : IEnumerator where T : IEntityPersistent, new()
        {
            private OdbcDataReader reader;
            private OdbcConnection conn;
            private OdbcCommand cmd;
            T entity;
            SqlOdbcFactory db;

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            int position = -1;

            public EntityEnum(SqlOdbcFactory database,string query=null)
            {
                db = database;

                conn = db.GetConnection(false); // nouvelle connexion pour les requetes imbriquées
                cmd = new OdbcCommand();
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

            // recherche une entité dans les references
            public T GetReference(T e)
            {
                return (T)db.Model.GetReference(e);
            }

            public bool MoveNext()
            {
                if (reader.Read())
                {
                    position++;
                    entity = new T();
                    entity.Factory = db;
                    entity.PickIdentity(reader);
                    entity = GetReference(entity); // obtient l'entité en cache
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