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
         
         public ParamContent(String paramname, int paramcount, String paramvalue) : this(){
            this.paramname = paramname;
            this.paramcount = paramcount;
            this.paramvalue = paramvalue;
         }
         #endregion // Constructor

         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region Fields
         // Nom
         protected String paramname;
         public String ParamName { get{ return paramname; } set{ paramname = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamName")); } }
         // Index
         protected int paramcount;
         public int ParamCount { get{ return paramcount; } set{ paramcount = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamCount")); } }
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
             result += "ParamName = " + ParamName + Environment.NewLine;
             result += "ParamCount = " + ParamCount + Environment.NewLine;
             result += "ParamValue = " + ParamValue + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("ParamName", ParamName, typeof(String));
              info.AddValue("ParamCount", ParamCount, typeof(int));
              info.AddValue("ParamValue", ParamValue, typeof(String));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            ParamName =  reader.ReadString();
            ParamCount =  reader.ReadInt32();
            ParamValue =  reader.ReadString();
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(ParamName);
            writer.Write(ParamCount);
            writer.Write(ParamValue);
       }
       #endregion // Serialization
       
       #region IEntity
       public IEntityFactory Factory{get;set;}
       
       public string TableName { get{ return "T_PARAM_CONTENT";} }
       
       public static string[] PrimaryIdentifier = {"ParamName", "ParamCount"};
       public string[] GetPrimaryIdentifier() { return PrimaryIdentifier; }
       
       // Identifiants
       public bool CompareIdentifier(IEntity e)
       {
           ParamContent b = e as ParamContent;
           if(b==null)
             return false;
           return (this.ParamName == b.ParamName && this.ParamCount == b.ParamCount);
       }
       
       public void Load()
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "SELECT ParamValue FROM T_PARAM_CONTENT WHERE ParamName = "+SqlFactory.ParseType(ParamName)+" and ParamCount = "+SqlFactory.ParseType(ParamCount)+"";
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
          string query = "DELETE FROM T_PARAM_CONTENT WHERE ParamName = "+SqlFactory.ParseType(ParamName)+" and ParamCount = "+SqlFactory.ParseType(ParamCount)+"";
          return db.Query(query);
       }
       
       public void Insert(string add_params = "", string add_values = "")
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "INSERT INTO T_PARAM_CONTENT (ParamName, ParamCount, ParamValue$add_params$) VALUES( " + SqlFactory.ParseType(ParamName) + ", " + SqlFactory.ParseType(ParamCount) + ", " + SqlFactory.ParseType(ParamValue) + "$add_values$)";
       
          // Association ObjectContent
          if(ObjectContent != null){
             add_params += ", Id";
             add_values += ", "+SqlFactory.ParseType(ObjectContent.Id)+"";
          }
       
          query = query.Replace("$add_params$", add_params);
          query = query.Replace("$add_values$", add_values);
       
          db.Query(query);
       
       }
       
       public int Update(string add_params = "")
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "UPDATE T_PARAM_CONTENT SET ParamValue = "+SqlFactory.ParseType(ParamValue)+"$add_params$ WHERE ParamName = "+SqlFactory.ParseType(ParamName)+" and ParamCount = "+SqlFactory.ParseType(ParamCount)+"";
       
          // Association ObjectContent
          if(ObjectContent != null){
             add_params += ", Id = "+SqlFactory.ParseType(ObjectContent.Id)+"";
          }
       
          query = query.Replace("$add_params$", add_params);
          
          return db.Query(query);
       
       }
       
       // ObjectContent(0,1) <-> (0,*)ParamContent
       public ObjectContent LoadObjectContent()
       {
          SqlFactory db = Factory as SqlFactory;
          string query = "SELECT Id FROM T_PARAM_CONTENT WHERE ParamName = "+SqlFactory.ParseType(ParamName)+"and ParamCount = "+SqlFactory.ParseType(ParamCount)+"";
          String Id = "";
       
          bool ok = true;
          ObjectContent objectcontent = null;
           
          db.Query(query, reader =>
          {
              if (reader.Read())
              {
                 if (reader["Id"] != null)
                   Id = reader["Id"].ToString();
                else
                   ok = false;
              }
              return 0;
          });
       
          if (ok == false)
              return null;
       
          // obtient l'objet de reference
          objectcontent = (from p in db.References.OfType<ObjectContent>() where p.Id == Id select p).FirstOrDefault();
          if ( objectcontent == null)
          {
              objectcontent = new ObjectContent();
              objectcontent.Factory = db;
              objectcontent.Id = Id;
              objectcontent = db.GetReference(objectcontent) as ObjectContent;//mise en cache
          }
       
          // Recharge les données depuis la BDD
          objectcontent.Load();
       
          return ObjectContent = objectcontent;
       }
       
       
       // Obtient l'identifiant primaire depuis un curseur SQL
       public void PickIdentity(object _reader)
       {
          SqlFactory db = Factory as SqlFactory;
          SqlDataReader reader = _reader as SqlDataReader;
          if (reader["ParamName"] != null)
             ParamName = reader["ParamName"].ToString();
       
          if (reader["ParamCount"] != null)
             ParamCount = int.Parse(reader["ParamCount"].ToString());
       }
       
       // Obtient les propriétés depuis un curseur SQL
       public void PickProperties(object _reader)
       {
          SqlFactory db = Factory as SqlFactory;
          SqlDataReader reader = _reader as SqlDataReader;
          if (reader["ParamValue"] != null)
             ParamValue = reader["ParamValue"].ToString();
       }
       #endregion // IEntity
      }

}