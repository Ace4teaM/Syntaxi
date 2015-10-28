/*
   Extension de la classe d'entité DatabaseSource

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
using Serial = System.Int32;

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class DatabaseSource : IEntity, ISerializable, IEntitySerializable, INotifyPropertyChanged, IDataErrorInfo, IEntityValidable    {
         #region Constructor
         public DatabaseSource(){
            // Id
            this.id = String.Empty;
            // Provider
            this.provider = new Int32();
            // ConnectionString
            this.connectionstring = String.Empty;
         }
         
         // copie
         public DatabaseSource(DatabaseSource src) : this(){
            Copy(this, src);
         }

         public DatabaseSource(String id, int? provider, String connectionstring) : this(){
            this.id = id;
            this.provider = provider;
            this.connectionstring = connectionstring;
         }
         #endregion // Constructor
         
          public string EntityName { get{ return "DatabaseSource"; } }

         // clone
         public IEntity Clone(){
            return Copy(new DatabaseSource(), this);
         }

         // copie
         public IEntity Copy(IEntity _dst,IEntity _src){
            DatabaseSource src = _src as DatabaseSource;
            DatabaseSource dst = _dst as DatabaseSource;
            if(src==null || dst==null)
               return null;
               
            // Id
            dst.id = src.id;
            // Provider
            dst.provider = src.provider;
            // ConnectionString
            dst.connectionstring = src.connectionstring;
            return dst;
         }

         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region Model
         private IModel model;
         public IModel Model { get { return model; } set { model = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Model")); } }
         #endregion // Model

         #region State
         private EntityState entityState;
         public EntityState EntityState { get{ return entityState; } set{ entityState = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("EntityState")); } }
         #endregion // State
        
         #region Fields
         // Identifiant de la source
         protected String id;
         public String Id { get{ return id; } set{ id = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Id")); } }
         // Fournisseur de données
         protected int? provider;
         public int? Provider { get{ return provider; } set{ provider = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Provider")); } }
         // Chaine de connexion
         protected String connectionstring;
         public String ConnectionString { get{ return connectionstring; } set{ connectionstring = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ConnectionString")); } }
         #endregion // Fields

         #region Associations

         //
         // Project
         // 

         protected Project project;
         public virtual Project Project { get{ return project; } set{ project = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Project"));  } }

         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "Id = " + Id + Environment.NewLine;
             result += "Provider = " + Provider + Environment.NewLine;
             result += "ConnectionString = " + ConnectionString + Environment.NewLine;
             return result;
         }

         #endregion // Methods

       #region ISerializable
       // Implement this method to serialize data. The method is called on serialization.
       public void GetObjectData(SerializationInfo info, StreamingContext context)
       {
            info.AddValue("Id", Id, typeof(String));
            info.AddValue("Provider", Provider, typeof(int));
            info.AddValue("ConnectionString", ConnectionString, typeof(String));
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
          Provider =  reader.ReadInt32();
          ConnectionString =  reader.ReadString();
       }
       
       public void WriteBinary(BinaryWriter writer)
       {
          // Properties
          writer.Write(Id);
          writer.Write(Provider.Value);
          writer.Write(ConnectionString);
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
          XmlElement cur = doc.CreateElement("DatabaseSource");
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
       
          // Assigne le membre Provider
          if (provider != null)
          {
              curMember = doc.CreateElement("Provider");
              curMember.AppendChild(doc.CreateTextNode(provider.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre ConnectionString
          if (connectionstring != null)
          {
              curMember = doc.CreateElement("ConnectionString");
              curMember.AppendChild(doc.CreateTextNode(connectionstring.ToString()));
              cur.AppendChild(curMember);
          }
          
          //
          // Compositions
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
                // Assigne le membre Provider
                case "Provider":
                {
                   int value;
                   if(int.TryParse(property_value,out value)==false)
                      this.Provider = new Int32();
                   else
                      this.Provider = value;
                }
                break;
                // Assigne le membre ConnectionString
                case "ConnectionString":
                {
                   this.connectionstring = property_value;
                }
                break;
       
                //
                // Compositions
                //
                
       			}
          }
       }
       
       #endregion // Serialization

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
              all_mess += ((msg = this["Provider"]) != String.Empty) ? (GetPropertyDesc("Provider") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["ConnectionString"]) != String.Empty) ? (GetPropertyDesc("ConnectionString") + " :\n\t" + msg + "\n") : String.Empty;
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
          return "";
       }
       
       public static string GetPropertyDesc(string propertyName)
       {
          switch (propertyName)
          {
       
              case "Id":
                  return "Identifiant de la source";
       
              case "Provider":
                  return "Fournisseur de données";
       
              case "ConnectionString":
                  return "Chaine de connexion";
          }
          return "";
       }
       #endregion
       
       #region IEntityValidable
       // Test la validité de tous les champs
       public void Validate(){
          string errorCode;
       
          if(CheckField("Id", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("Provider", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("ConnectionString", out errorCode) == false)
             throw new ApplicationException(errorCode);
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
                 break;
       
               case "Provider":
                 if(this.Provider == null)
                   break;
                 break;
       
               case "ConnectionString":
                 if(this.ConnectionString == null)
                   break;
                 break;
       
           }
           
           return true;
       }
       #endregion
       #endregion // Validation
      }

}