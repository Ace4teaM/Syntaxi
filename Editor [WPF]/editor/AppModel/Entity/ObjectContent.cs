/*
   Extension de la classe d'entité ObjectContent

   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications seront perdues
   
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Collections.ObjectModel;
using Lib;
using System.IO;
using System.Runtime.Serialization;
using System.Data.SqlClient;

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class ObjectContent : ISerializable , INotifyPropertyChanged , IEntity    {
         #region Constructor
         public ObjectContent(){

            // ParamContent
            this.paramcontent = new Collection<ParamContent>();
         }
         
         public ObjectContent(String id, String objecttype, String filename, int? position) : this(){
            this.id = id;
            this.objecttype = objecttype;
            this.filename = filename;
            this.position = position;
         }
         #endregion // Constructor

         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region Fields
         // Identifiant
         protected String id;
         public String Id { get{ return id; } set{ id = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Id")); } }
         // Type d'objet
         protected String objecttype;
         public String ObjectType { get{ return objecttype; } set{ objecttype = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectType")); } }
         // Emplacement du fichier source
         protected String filename;
         public String Filename { get{ return filename; } set{ filename = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Filename")); } }
         // Position de départ dans le fichier source
         protected int? position;
         public int? Position { get{ return position; } set{ position = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Position")); } }
         #endregion // Fields

         #region Associations
         // 
         protected Project project;
         public virtual Project Project { get{ return project; } set{ project = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Project"));  } }
         // 
         protected Collection<ParamContent> paramcontent;
         public virtual Collection<ParamContent> ParamContent { get{ return paramcontent; } set{ paramcontent = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamContent"));  } }
         public void AddParamContent(ParamContent obj){
            obj.ObjectContent = this;
            ParamContent.Add(obj);
         }
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "Id = " + Id + Environment.NewLine;
             result += "ObjectType = " + ObjectType + Environment.NewLine;
             result += "Filename = " + Filename + Environment.NewLine;
             result += "Position = " + Position + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("Id", Id, typeof(String));
              info.AddValue("ObjectType", ObjectType, typeof(String));
              info.AddValue("Filename", Filename, typeof(String));
              info.AddValue("Position", Position, typeof(int));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            Id =  reader.ReadString();
            ObjectType =  reader.ReadString();
            Filename =  reader.ReadString();
            Position =  reader.ReadInt32();

            // ParamContent
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.ParamContent = new Collection<ParamContent>();
                for(int i=0;i<size;i++){
                    ParamContent o = new ParamContent();
                    o.ReadBinary(reader);
                    this.AddParamContent(o);
                }
            }
            else
            {
                this.ParamContent = new Collection<ParamContent>();
            }
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(Id);
            writer.Write(ObjectType);
            writer.Write(Filename);
            writer.Write(Position.Value);

            // ParamContent
            writer.Write(this.paramcontent.Count);
            if (this.paramcontent.Count > 0)
            {
                foreach (var col in this.paramcontent)
                    col.WriteBinary(writer);
            }
       }
       #endregion // Serialization
       
       #region IEntity
       public IEntityFactory Factory{get;set;}
       
       public string TableName { get{ return "T_OBJECT_CONTENT";} }
       
       public static string[] PrimaryIdentifier = {"Id"};
       public string[] GetPrimaryIdentifier() { return PrimaryIdentifier; }
       
       // Identifiants
       public bool CompareIdentifier(IEntity e)
       {
           ObjectContent b = e as ObjectContent;
           if(b==null)
             return false;
           return (this.Id == b.Id);
       }
       
       public void Load()
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "SELECT ObjectType , Filename , Position FROM T_OBJECT_CONTENT WHERE Id = "+SqlFactory.ParseType(Id)+"";
          db.QueryObject(query, this);
       }
       
       public object LoadAssociations(string name)
       {
          SqlFactory db = Factory as SqlFactory;
       
          if(name == "Project")
             return LoadProject();
       
          if(name == "ParamContent")
             return LoadParamContent();
       
          return null;
       }
       
       public int Delete()
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "DELETE FROM T_OBJECT_CONTENT WHERE Id = "+SqlFactory.ParseType(Id)+"";
          return db.Query(query);
       }
       
       public void Insert(string add_params = "", string add_values = "")
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "INSERT INTO T_OBJECT_CONTENT (Id, ObjectType, Filename, Position$add_params$) VALUES( " + SqlFactory.ParseType(ObjectType) + ", " + SqlFactory.ParseType(Filename) + ", " + SqlFactory.ParseType(Position) + "$add_values$)";
       
          // Association Project
          if(Project != null){
             add_params += ", Name, Version";
             add_values += ", "+SqlFactory.ParseType(Project.Name)+", "+SqlFactory.ParseType(Project.Version)+"";
          }
       
          query = query.Replace("$add_params$", add_params);
          query = query.Replace("$add_values$", add_values);
       
          db.Query(query);
       
       }
       
       public int Update(string add_params = "")
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "UPDATE T_OBJECT_CONTENT SET ObjectType = "+SqlFactory.ParseType(ObjectType)+", Filename = "+SqlFactory.ParseType(Filename)+", Position = "+SqlFactory.ParseType(Position)+"$add_params$ WHERE Id = "+SqlFactory.ParseType(Id)+"";
       
          // Association Project
          if(Project != null){
             add_params += ", Name = "+SqlFactory.ParseType(Project.Name)+", Version = "+SqlFactory.ParseType(Project.Version)+"";
          }
       
          query = query.Replace("$add_params$", add_params);
          
          return db.Query(query);
       
       }
       
       // Project(0,1) <-> (0,*)ObjectContent
       public Project LoadProject()
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "SELECT Name , Version FROM T_OBJECT_CONTENT WHERE Id = "+SqlFactory.ParseType(Id)+"";
          String Name = "";
       
          String Version = "";
       
          bool ok = true;
          Project project = null;
           
          db.Query(query, reader =>
          {
              if (reader.Read())
              {
                 if (reader["Name"] != null)
                   Name = reader["Name"].ToString();
                else
                   ok = false;
                 if (reader["Version"] != null)
                   Version = reader["Version"].ToString();
                else
                   ok = false;
              }
              return 0;
          });
       
          if (ok == false)
              return null;
       
          // obtient l'objet de reference
          project = (from p in db.References.OfType<Project>() where p.Name == Name&& p.Version == Version select p).FirstOrDefault();
          if ( project == null)
          {
              project = new Project();
              project.Factory = db;
              project.Name = Name;
              project.Version = Version;
              project = db.GetReference(project) as Project;//mise en cache
          }
       
          // Recharge les données depuis la BDD
          project.Load();
       
          return Project = project;
       }
       
       // ObjectContent(0,1) <-> (0,*)ParamContent
       public Collection<ParamContent> LoadParamContent()
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "SELECT ParamName FROM T_PARAM_CONTENT WHERE Id = "+SqlFactory.ParseType(Id)+"";
          this.ParamContent = new Collection<ParamContent>();
       
          db.Query(query, reader =>
          {
              while (reader.Read())
              {
                // obtient l'identifiant
                String ParamName = "";
       
                if (reader["ParamName"] == null)
                   continue;
                ParamName = reader["ParamName"].ToString();
                
                // obtient l'objet de reference
                ParamContent _entity = (from p in db.References.OfType<ParamContent>() where p.ParamName == ParamName select p).FirstOrDefault();
       
                if ( _entity == null)
                {
                    _entity = new ParamContent();
                    _entity.Factory = db;
                    _entity.ParamName = ParamName;
                    _entity = db.GetReference(_entity) as ParamContent;//mise en cache
                }
                
                // Recharge les données depuis la BDD
                _entity.Load();
          
                // Ajoute la reference à la collection
                this.AddParamContent(_entity);
       
              }
              return 0;
          });
       
          return ParamContent;
       }
       
       // Obtient l'identifiant primaire depuis un curseur SQL
       public void PickIdentity(object _reader)
       {
          SqlFactory db = Factory as SqlFactory;
          SqlDataReader reader = _reader as SqlDataReader;
          if (reader["Id"] != null)
             Id = reader["Id"].ToString();
       }
       
       // Obtient les propriétés depuis un curseur SQL
       public void PickProperties(object _reader)
       {
          SqlFactory db = Factory as SqlFactory;
          SqlDataReader reader = _reader as SqlDataReader;
          if (reader["ObjectType"] != null)
             ObjectType = reader["ObjectType"].ToString();
       
          if (reader["Filename"] != null)
             Filename = reader["Filename"].ToString();
       
          if (reader["Position"] != null)
             Position = int.Parse(reader["Position"].ToString());
       }
       #endregion // IEntity
      }

}