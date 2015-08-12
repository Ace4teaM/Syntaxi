/*
   Extension de la classe d'entité Project

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

    public partial class Project : ISerializable , INotifyPropertyChanged , IEntity    {
         #region Constructor
         public Project(){

            // ObjectContent
            this.objectcontent = new Collection<ObjectContent>();
            // SearchParams
            this.searchparams = new Collection<SearchParams>();
            // ObjectSyntax
            this.objectsyntax = new Collection<ObjectSyntax>();
            // ParamSyntax
            this.paramsyntax = new Collection<ParamSyntax>();
         }
         
         public Project(String name, String version) : this(){
            this.name = name;
            this.version = version;
         }
         #endregion // Constructor

         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region Fields
         // 
         protected String name;
         public String Name { get{ return name; } set{ name = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Name")); } }
         // 
         protected String version;
         public String Version { get{ return version; } set{ version = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Version")); } }
         #endregion // Fields

         #region Associations
         // 
         protected Collection<ObjectContent> objectcontent;
         public virtual Collection<ObjectContent> ObjectContent { get{ return objectcontent; } set{ objectcontent = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectContent"));  } }
         public void AddObjectContent(ObjectContent obj){
            obj.Project = this;
            ObjectContent.Add(obj);
         }
         public void RemoveObjectContent(ObjectContent obj){
            obj.Project = null;
            ObjectContent.Remove(obj);
         }
         // 
         protected Collection<SearchParams> searchparams;
         public virtual Collection<SearchParams> SearchParams { get{ return searchparams; } set{ searchparams = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("SearchParams"));  } }
         public void AddSearchParams(SearchParams obj){
            obj.Project = this;
            SearchParams.Add(obj);
         }
         public void RemoveSearchParams(SearchParams obj){
            obj.Project = null;
            SearchParams.Remove(obj);
         }
         // 
         protected Collection<ObjectSyntax> objectsyntax;
         public virtual Collection<ObjectSyntax> ObjectSyntax { get{ return objectsyntax; } set{ objectsyntax = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectSyntax"));  } }
         public void AddObjectSyntax(ObjectSyntax obj){
            obj.Project = this;
            ObjectSyntax.Add(obj);
         }
         public void RemoveObjectSyntax(ObjectSyntax obj){
            obj.Project = null;
            ObjectSyntax.Remove(obj);
         }
         // 
         protected Collection<ParamSyntax> paramsyntax;
         public virtual Collection<ParamSyntax> ParamSyntax { get{ return paramsyntax; } set{ paramsyntax = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamSyntax"));  } }
         public void AddParamSyntax(ParamSyntax obj){
            obj.Project = this;
            ParamSyntax.Add(obj);
         }
         public void RemoveParamSyntax(ParamSyntax obj){
            obj.Project = null;
            ParamSyntax.Remove(obj);
         }
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "Name = " + Name + Environment.NewLine;
             result += "Version = " + Version + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("Name", Name, typeof(String));
              info.AddValue("Version", Version, typeof(String));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            Name =  reader.ReadString();
            Version =  reader.ReadString();

            // ObjectContent
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.ObjectContent = new Collection<ObjectContent>();
                for(int i=0;i<size;i++){
                    ObjectContent o = new ObjectContent();
                    o.ReadBinary(reader);
                    this.AddObjectContent(o);
                }
            }
            else
            {
                this.ObjectContent = new Collection<ObjectContent>();
            }
            // SearchParams
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.SearchParams = new Collection<SearchParams>();
                for(int i=0;i<size;i++){
                    SearchParams o = new SearchParams();
                    o.ReadBinary(reader);
                    this.AddSearchParams(o);
                }
            }
            else
            {
                this.SearchParams = new Collection<SearchParams>();
            }
            // ObjectSyntax
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.ObjectSyntax = new Collection<ObjectSyntax>();
                for(int i=0;i<size;i++){
                    ObjectSyntax o = new ObjectSyntax();
                    o.ReadBinary(reader);
                    this.AddObjectSyntax(o);
                }
            }
            else
            {
                this.ObjectSyntax = new Collection<ObjectSyntax>();
            }
            // ParamSyntax
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.ParamSyntax = new Collection<ParamSyntax>();
                for(int i=0;i<size;i++){
                    ParamSyntax o = new ParamSyntax();
                    o.ReadBinary(reader);
                    this.AddParamSyntax(o);
                }
            }
            else
            {
                this.ParamSyntax = new Collection<ParamSyntax>();
            }
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(Name);
            writer.Write(Version);

            // ObjectContent
            writer.Write(this.objectcontent.Count);
            if (this.objectcontent.Count > 0)
            {
                foreach (var col in this.objectcontent)
                    col.WriteBinary(writer);
            }
            // SearchParams
            writer.Write(this.searchparams.Count);
            if (this.searchparams.Count > 0)
            {
                foreach (var col in this.searchparams)
                    col.WriteBinary(writer);
            }
            // ObjectSyntax
            writer.Write(this.objectsyntax.Count);
            if (this.objectsyntax.Count > 0)
            {
                foreach (var col in this.objectsyntax)
                    col.WriteBinary(writer);
            }
            // ParamSyntax
            writer.Write(this.paramsyntax.Count);
            if (this.paramsyntax.Count > 0)
            {
                foreach (var col in this.paramsyntax)
                    col.WriteBinary(writer);
            }
       }
       #endregion // Serialization
       
       #region IEntity
       public IEntityFactory Factory{get;set;}
       
       public string TableName { get{ return "T_PROJECT";} }
       
       public static string[] PrimaryIdentifier = {"Name", "Version"};
       public string[] GetPrimaryIdentifier() { return PrimaryIdentifier; }
       
       // Identifiants
       public bool CompareIdentifier(IEntity e)
       {
           Project b = e as Project;
           if(b==null)
             return false;
           return (this.Name == b.Name && this.Version == b.Version);
       }
       
       public void Load()
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "SELECT FROM T_PROJECT WHERE Name = "+SqlFactory.ParseType(Name)+" and Version = "+SqlFactory.ParseType(Version)+"";
          db.QueryObject(query, this);
       }
       
       public object LoadAssociations(string name)
       {
          SqlFactory db = Factory as SqlFactory;
       
          if(name == "ObjectContent")
             return LoadObjectContent();
       
          return null;
       }
       
       public int Delete()
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "DELETE FROM T_PROJECT WHERE Name = "+SqlFactory.ParseType(Name)+" and Version = "+SqlFactory.ParseType(Version)+"";
          return db.Query(query);
       }
       
       public void Insert(string add_params = "", string add_values = "")
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "INSERT INTO T_PROJECT (Name, Version$add_params$) VALUES( " + SqlFactory.ParseType(Name) + ", " + SqlFactory.ParseType(Version) + "$add_values$)";
       
       
          query = query.Replace("$add_params$", add_params);
          query = query.Replace("$add_values$", add_values);
       
          db.Query(query);
       
       }
       
       public int Update(string add_params = "")
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "UPDATE T_PROJECT SET $add_params$ WHERE Name = "+SqlFactory.ParseType(Name)+" and Version = "+SqlFactory.ParseType(Version)+"";
       
       
          query = query.Replace("$add_params$", add_params);
          
          return db.Query(query);
       
       }
       
       // Project(0,1) <-> (0,*)ObjectContent
       public Collection<ObjectContent> LoadObjectContent()
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "SELECT Id FROM T_OBJECT_CONTENT WHERE Name = "+SqlFactory.ParseType(Name)+"and Version = "+SqlFactory.ParseType(Version)+"";
          this.ObjectContent = new Collection<ObjectContent>();
       
          db.Query(query, reader =>
          {
              while (reader.Read())
              {
                // obtient l'identifiant
                String Id = "";
       
                if (reader["Id"] == null)
                   continue;
                Id = reader["Id"].ToString();
                
                // obtient l'objet de reference
                ObjectContent _entity = (from p in db.References.OfType<ObjectContent>() where p.Id == Id select p).FirstOrDefault();
       
                if ( _entity == null)
                {
                    _entity = new ObjectContent();
                    _entity.Factory = db;
                    _entity.Id = Id;
                    _entity = db.GetReference(_entity) as ObjectContent;//mise en cache
                }
                
                // Recharge les données depuis la BDD
                _entity.Load();
          
                // Ajoute la reference à la collection
                this.AddObjectContent(_entity);
       
              }
              return 0;
          });
       
          return ObjectContent;
       }
       
       // Obtient l'identifiant primaire depuis un curseur SQL
       public void PickIdentity(object _reader)
       {
          SqlFactory db = Factory as SqlFactory;
          SqlDataReader reader = _reader as SqlDataReader;
          if (reader["Name"] != null)
             Name = reader["Name"].ToString();
       
          if (reader["Version"] != null)
             Version = reader["Version"].ToString();
       }
       
       // Obtient les propriétés depuis un curseur SQL
       public void PickProperties(object _reader)
       {
          SqlFactory db = Factory as SqlFactory;
          SqlDataReader reader = _reader as SqlDataReader;
       }
       #endregion // IEntity
      }

}