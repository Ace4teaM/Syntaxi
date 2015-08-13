/*
   Extension de la classe d'entité ParamContent

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

    public partial class ParamContent : ISerializable , INotifyPropertyChanged , IEntity    {
         #region Constructor
         public ParamContent(){
         }
         
         public ParamContent(String id, String paramname, String paramvalue) : this(){
            this.id = id;
            this.paramname = paramname;
            this.paramvalue = paramvalue;
         }
         #endregion // Constructor

         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region State
        private EntityState entityState;
        public EntityState EntityState { get{ return entityState; } set{ entityState = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("EntityState")); } }

         #endregion // State
        
         #region Fields
         // 
         protected String id;
         public String Id { get{ return id; } set{ id = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Id")); } }
         // Nom
         protected String paramname;
         public String ParamName { get{ return paramname; } set{ paramname = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamName")); } }
         // Valeur
         protected String paramvalue;
         public String ParamValue { get{ return paramvalue; } set{ paramvalue = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamValue")); } }
         #endregion // Fields

         #region Associations
         // 
         protected ObjectContent objectcontent;
         public virtual ObjectContent ObjectContent { get{ return objectcontent; } set{ objectcontent = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectContent"));  } }
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "Id = " + Id + Environment.NewLine;
             result += "ParamName = " + ParamName + Environment.NewLine;
             result += "ParamValue = " + ParamValue + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("Id", Id, typeof(String));
              info.AddValue("ParamName", ParamName, typeof(String));
              info.AddValue("ParamValue", ParamValue, typeof(String));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            Id =  reader.ReadString();
            ParamName =  reader.ReadString();
            ParamValue =  reader.ReadString();
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(Id);
            writer.Write(ParamName);
            writer.Write(ParamValue);
       }
       #endregion // Serialization
       
       #region IEntity
       public IEntityFactory Factory{get;set;}
       
       public string TableName { get{ return "T_PARAM_CONTENT";} }
       
       public static string[] PrimaryIdentifier = {"Id"};
       public string[] GetPrimaryIdentifier() { return PrimaryIdentifier; }
       
       // Identifiants
       public bool CompareIdentifier(IEntity e)
       {
           ParamContent b = e as ParamContent;
           if(b==null)
             return false;
           return (this.Id == b.Id);
       }
       
       public void Load()
       {
          string query = "SELECT ParamName , ParamValue FROM T_PARAM_CONTENT WHERE Param_Content_Id = "+Factory.ParseType(this.Id)+"";
          Factory.QueryObject(query, this);
       }
       
       public object LoadAssociations(string name)
       {
          
       
          if(name == "ObjectContent")
             return LoadObjectContent();
       
          return null;
       }
       
       public int Delete()
       {
          
          string query = "DELETE FROM T_PARAM_CONTENT WHERE  Param_Content_Id = "+Factory.ParseType(this.Id)+"";
          return Factory.Query(query);
       }
       
       public void Insert(string add_params = "", string add_values = "")
       {
          
          string query = "INSERT INTO T_PARAM_CONTENT (Param_Content_Id, ParamName, ParamValue$add_params$) VALUES( " + Factory.ParseType(this.Id) + ", " + Factory.ParseType(this.ParamName) + ", " + Factory.ParseType(this.ParamValue) + "$add_values$)";
       
          // Association ObjectContent
          if(ObjectContent != null){
             add_params += ", Object_Content_Id";
             add_values += ", "+Factory.ParseType(ObjectContent.Id)+"";
          }
       
          query = query.Replace("$add_params$", add_params);
          query = query.Replace("$add_values$", add_values);
       
          Factory.Query(query);
       
       }
       
       public int Update(string add_params = "")
       {
             string query = "UPDATE T_PARAM_CONTENT SET ParamName = "+Factory.ParseType(this.ParamName)+", ParamValue = "+Factory.ParseType(this.ParamValue)+"$add_params$ WHERE Param_Content_Id = "+Factory.ParseType(this.Id)+"";
       
          // Association ObjectContent
          if(ObjectContent != null){
             add_params += ", Object_Content_Id = "+Factory.ParseType(ObjectContent.Id)+"";
          }
       
          query = query.Replace("$add_params$", add_params);
          
          return Factory.Query(query);
       }
       
       // ObjectContent(0,1) <-> (0,*)ParamContent
       public ObjectContent LoadObjectContent()
       {
          
          string query = "SELECT Object_Content_Id FROM T_PARAM_CONTENT WHERE Param_Content_Id = "+Factory.ParseType(this.Id)+"";
          String Id = "";
       
          bool ok = true;
          ObjectContent objectcontent = null;
           
          Factory.Query(query, reader =>
          {
              if (reader.Read())
              {
                 if (reader["Id"] != null)
                   Id = reader["Object_Content_Id"].ToString();
                else
                   ok = false;
              }
              return 0;
          });
       
          if (ok == false)
              return null;
       
          // obtient l'objet de reference
          objectcontent = (from p in Factory.GetReferences().OfType<ObjectContent>() where p.Id == Id select p).FirstOrDefault();
          if ( objectcontent == null)
          {
              objectcontent = new ObjectContent();
              objectcontent.Factory = this.Factory;
              objectcontent.Id = Id;
              objectcontent = Factory.GetReference(objectcontent) as ObjectContent;//mise en cache
          }
       
          // Recharge les données depuis la BDD
          objectcontent.Load();
       
          return ObjectContent = objectcontent;
       }
       
       
       // Obtient l'identifiant primaire depuis un curseur SQL
       public void PickIdentity(object _reader)
       {
          SqlDataReader reader = _reader as SqlDataReader;
          if (reader["Param_Content_Id"] != null)
             Id = reader["Param_Content_Id"].ToString();
       }
       
       // Obtient les propriétés depuis un curseur SQL
       public void PickProperties(object _reader)
       {
          SqlDataReader reader = _reader as SqlDataReader;
          if (reader["ParamName"] != null)
             ParamName = reader["ParamName"].ToString();
       
          if (reader["ParamValue"] != null)
             ParamValue = reader["ParamValue"].ToString();
       }
       #endregion // IEntity
      }

}