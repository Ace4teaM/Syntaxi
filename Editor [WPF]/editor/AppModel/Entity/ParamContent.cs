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
using AppModel.Format;
using AppModel.Domain;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Data.Common;
using Serial = System.Int32;

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class ParamContent : IEntity, ISerializable, IEntitySerializable, INotifyPropertyChanged, IEntityPersistent, IDataErrorInfo, IEntityValidable    {
         #region Constructor
         public ParamContent(){
            // Id
            this.id = String.Empty;
            // ParamName
            this.paramname = String.Empty;
            // ParamValue
            this.paramvalue = String.Empty;
         }
         
         // copie
         public ParamContent(ParamContent src) : this(){
            // Id
            this.id = src.id;
            // ParamName
            this.paramname = src.paramname;
            // ParamValue
            this.paramvalue = src.paramvalue;
         }

         public ParamContent(String id, String paramname, String paramvalue) : this(){
            this.id = id;
            this.paramname = paramname;
            this.paramvalue = paramvalue;
         }
         #endregion // Constructor
         
          public string EntityName { get{ return "ParamContent"; } }

         // clone
         public IEntity Clone(){
            ParamContent e = new ParamContent();
            
            // Id
            e.id = this.id;
            // ParamName
            e.paramname = this.paramname;
            // ParamValue
            e.paramvalue = this.paramvalue;
            return e;
         }

         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region State
        private EntityState entityState;
        public EntityState EntityState { get{ return entityState; } set{ entityState = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("EntityState")); } }

         #endregion // State
        
         #region Fields
         // Identifiant
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
       /// <summary>
       /// Initialise l'instance depuis les données d'un flux binaire
       /// </summary>
       /// <param name="reader">Flux binaire</param>
       /// <param name="aggregationCallback">Permet d'appliquer des modifications aux entités importées par aggrégation</param>
       /// <remarks>Seuls les éléments existants dans le noeud Xml son importés dans l'objet</remarks>
       public void ReadBinary(BinaryReader reader, EntityCallback aggregationCallback)
       {
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
       
       
       /// <summary>
       /// Convertie l'instance en élément XML
       /// </summary>
       /// <param name="parent">Élément parent reçevant le nouveau noeud</param>
       /// <returns>Text XML du document</returns>
       public string ToXml(XmlElement parent)
       {
          XmlElement curMember = null;
          XmlDocument doc = null;
          // Element parent ?
          if (parent != null)
          {
              doc = parent.OwnerDocument;
          }
          else
          {
              doc = new XmlDocument();
              parent = doc.CreateElement("root");
              doc.AppendChild(parent);
          }
       
          //Ecrit au format XML
          XmlElement cur = doc.CreateElement("ParamContent");
          parent.AppendChild(cur);
              
          //
          // Fields
          //
          
       		// Assigne le membre Id
          if (id != null)
          {
              curMember = doc.CreateElement("Id");
              curMember.AppendChild(doc.CreateTextNode(id.ToString()));
              cur.AppendChild(curMember);
          }
       
       		// Assigne le membre ParamName
          if (paramname != null)
          {
              curMember = doc.CreateElement("ParamName");
              curMember.AppendChild(doc.CreateTextNode(paramname.ToString()));
              cur.AppendChild(curMember);
          }
       
       		// Assigne le membre ParamValue
          if (paramvalue != null)
          {
              curMember = doc.CreateElement("ParamValue");
              curMember.AppendChild(doc.CreateTextNode(paramvalue.ToString()));
              cur.AppendChild(curMember);
          }
          
          //
          // Aggregations
          //
       
          parent.AppendChild(cur);
          return doc.InnerXml;
       }
       
       /// <summary>
       /// Initialise l'instance depuis des données XML
       /// </summary>
       /// <param name="element">Élément contenant les information sur l'objet</param>
       /// <param name="aggregationCallback">Permet d'appliquer des modifications aux entités importées par aggrégation</param>
       /// <remarks>Seuls les éléments existants dans le noeud Xml son importés dans l'objet</remarks>
       public void FromXml(XmlElement element, EntityCallback aggregationCallback)
       {
          foreach (XmlElement m in element.ChildNodes)
          {
              string property_value = m.InnerText.Trim();
              // charge les paramètres
              switch (m.Name)
              {
                //
                // Fields
                //
       
                // Assigne le membre Id
                case "Id":
                {
                   this.id = property_value;
                }
                break;
                // Assigne le membre ParamName
                case "ParamName":
                {
                   this.paramname = property_value;
                }
                break;
                // Assigne le membre ParamValue
                case "ParamValue":
                {
                   this.paramvalue = property_value;
                }
                break;
       
                //
                // Aggregations
                //
                
       			}
          }
       }
       
       #endregion // Serialization
       
       #region IEntityPersistent
       public IEntityFactory Factory{get;set;}
       
       public string TableName { get{ return "T_PARAM_CONTENT";} }
       
       public static string[] PrimaryIdentifier = {"Id"};
       public string[] GetPrimaryIdentifier() { return PrimaryIdentifier; }
       
       // Identifiants
       public bool CompareIdentifier(IEntityPersistent e)
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
       
       
       // Réinitialise l'identifiant primaire
       public void RaiseIdentity()
       {
          Id = String.Empty;
       }
       
       // Obtient l'identifiant primaire depuis un curseur SQL
       public void PickIdentity(object _reader)
       {
          DbDataReader reader = _reader as DbDataReader;
          if (reader["Param_Content_Id"] != null)
             Id = reader["Param_Content_Id"].ToString();
       }
       
       // Obtient les propriétés depuis un curseur SQL
       public void PickProperties(object _reader)
       {
          DbDataReader reader = _reader as DbDataReader;
          if (reader["ParamName"] != null)
             ParamName = reader["ParamName"].ToString();
       
          if (reader["ParamValue"] != null)
             ParamValue = reader["ParamValue"].ToString();
       }
       #endregion // IEntityPersistent
       #region Validation
       #region IDataErrorInfo
       // Validation globale de l'entité
       public string Error
       {
          get
          {
              string all_mess = "";
              string msg;
              all_mess += ((msg = this["Id"]) != String.Empty) ? (GetPropertyDesc("Id") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["ParamName"]) != String.Empty) ? (GetPropertyDesc("ParamName") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["ParamValue"]) != String.Empty) ? (GetPropertyDesc("ParamValue") + " :\n\t" + msg + "\n") : String.Empty;
              return all_mess;
          }
       }
       
       // Validation par propriété
       public string this[string propertyName]
       {
          get
          {
              string code;
              CheckField(propertyName, out code);
              
              if (String.IsNullOrEmpty(code) == false)
                  return GetPropertyDesc(propertyName) + ":\n" + code;
                  
              return String.Empty;
          }
       }
       
       public static string GetClassDesc()
       {
          return "Paramètre d'un objet";
       }
       
       public static string GetPropertyDesc(string propertyName)
       {
          switch (propertyName)
          {
       
              case "Id":
                  return "Identifiant";
       
              case "ParamName":
                  return "Nom";
       
              case "ParamValue":
                  return "Valeur";
          }
          return "";
       }
       #endregion
       
       #region IEntityValidable
       // Test la validité de tous les champs
       public bool IsValid(){
          string errorCode;
          
          if(CheckField("Id", out errorCode) == false)
             return false;
          if(CheckField("ParamName", out errorCode) == false)
             return false;
          if(CheckField("ParamValue", out errorCode) == false)
             return false;
          return true;
       }
       
       // Test la validité d'un champ
       public bool CheckField(string propertyName, out string errorCode){
           errorCode = String.Empty;
           
           switch (propertyName)
           {
               case "Id":
                 // Obligatoire
                 if(this.Id == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 return AppModel.Format.Guid.Validate(this.Id.ToString(),ref errorCode);
       
               case "ParamName":
                 // Obligatoire
                 if(this.ParamName == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 break;
       
               case "ParamValue":
                 // Obligatoire
                 if(this.ParamValue == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 break;
       
           }
           
           return true;
       }
       #endregion
       #endregion // Validation
      }

}