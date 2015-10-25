/*
   Extension de la classe d'entité ObjectSyntax

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

    public partial class ObjectSyntax : IEntity, ISerializable, IEntitySerializable, INotifyPropertyChanged, IDataErrorInfo, IEntityValidable    {
         #region Constructor
         public ObjectSyntax(){
            // ContentRegEx
            this.contentregex = String.Empty;
            // ParamRegEx
            this.paramregex = String.Empty;
            // ObjectType
            this.objecttype = String.Empty;
            // ObjectDesc
            this.objectdesc = String.Empty;
            // GroupName
            this.groupname = String.Empty;
         }
         
         // copie
         public ObjectSyntax(ObjectSyntax src) : this(){
            Copy(this, src);
         }

         public ObjectSyntax(String contentregex, String paramregex, String objecttype, String objectdesc, String groupname) : this(){
            this.contentregex = contentregex;
            this.paramregex = paramregex;
            this.objecttype = objecttype;
            this.objectdesc = objectdesc;
            this.groupname = groupname;
         }
         #endregion // Constructor
         
          public string EntityName { get{ return "ObjectSyntax"; } }

         // clone
         public IEntity Clone(){
            return Copy(new ObjectSyntax(), this);
         }

         // copie
         public IEntity Copy(IEntity _dst,IEntity _src){
            ObjectSyntax src = _src as ObjectSyntax;
            ObjectSyntax dst = _dst as ObjectSyntax;
            if(src==null || dst==null)
               return null;
               
            // ContentRegEx
            dst.contentregex = src.contentregex;
            // ParamRegEx
            dst.paramregex = src.paramregex;
            // ObjectType
            dst.objecttype = src.objecttype;
            // ObjectDesc
            dst.objectdesc = src.objectdesc;
            // GroupName
            dst.groupname = src.groupname;
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
         // Expression de contenu
         protected String contentregex;
         public String ContentRegEx { get{ return contentregex; } set{ contentregex = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ContentRegEx")); } }
         // Expression de paramètre
         protected String paramregex;
         public String ParamRegEx { get{ return paramregex; } set{ paramregex = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamRegEx")); } }
         // Type
         protected String objecttype;
         public String ObjectType { get{ return objecttype; } set{ objecttype = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectType")); } }
         // Description
         protected String objectdesc;
         public String ObjectDesc { get{ return objectdesc; } set{ objectdesc = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectDesc")); } }
         // Groupe
         protected String groupname;
         public String GroupName { get{ return groupname; } set{ groupname = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("GroupName")); } }
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
             result += "ContentRegEx = " + ContentRegEx + Environment.NewLine;
             result += "ParamRegEx = " + ParamRegEx + Environment.NewLine;
             result += "ObjectType = " + ObjectType + Environment.NewLine;
             result += "ObjectDesc = " + ObjectDesc + Environment.NewLine;
             result += "GroupName = " + GroupName + Environment.NewLine;
             return result;
         }

         #endregion // Methods

       #region ISerializable
       // Implement this method to serialize data. The method is called on serialization.
       public void GetObjectData(SerializationInfo info, StreamingContext context)
       {
            info.AddValue("ContentRegEx", ContentRegEx, typeof(String));
            info.AddValue("ParamRegEx", ParamRegEx, typeof(String));
            info.AddValue("ObjectType", ObjectType, typeof(String));
            info.AddValue("ObjectDesc", ObjectDesc, typeof(String));
            info.AddValue("GroupName", GroupName, typeof(String));
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
          ContentRegEx =  reader.ReadString();
          ParamRegEx =  reader.ReadString();
          ObjectType =  reader.ReadString();
          ObjectDesc =  reader.ReadString();
          GroupName =  reader.ReadString();
       }
       
       public void WriteBinary(BinaryWriter writer)
       {
          // Properties
          writer.Write(ContentRegEx);
          writer.Write(ParamRegEx);
          writer.Write(ObjectType);
          writer.Write(ObjectDesc);
          writer.Write(GroupName);
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
          XmlElement cur = doc.CreateElement("ObjectSyntax");
          parent.AppendChild(cur);
              
          //
          // Fields
          //
          
          // Assigne le membre ContentRegEx
          if (contentregex != null)
          {
              curMember = doc.CreateElement("ContentRegEx");
              curMember.AppendChild(doc.CreateTextNode(contentregex.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre ParamRegEx
          if (paramregex != null)
          {
              curMember = doc.CreateElement("ParamRegEx");
              curMember.AppendChild(doc.CreateTextNode(paramregex.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre ObjectType
          if (objecttype != null)
          {
              curMember = doc.CreateElement("ObjectType");
              curMember.AppendChild(doc.CreateTextNode(objecttype.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre ObjectDesc
          if (objectdesc != null)
          {
              curMember = doc.CreateElement("ObjectDesc");
              curMember.AppendChild(doc.CreateTextNode(objectdesc.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre GroupName
          if (groupname != null)
          {
              curMember = doc.CreateElement("GroupName");
              curMember.AppendChild(doc.CreateTextNode(groupname.ToString()));
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
       
                // Assigne le membre ContentRegEx
                case "ContentRegEx":
                {
                   this.contentregex = property_value;
                }
                break;
                // Assigne le membre ParamRegEx
                case "ParamRegEx":
                {
                   this.paramregex = property_value;
                }
                break;
                // Assigne le membre ObjectType
                case "ObjectType":
                {
                   this.objecttype = property_value;
                }
                break;
                // Assigne le membre ObjectDesc
                case "ObjectDesc":
                {
                   this.objectdesc = property_value;
                }
                break;
                // Assigne le membre GroupName
                case "GroupName":
                {
                   this.groupname = property_value;
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
              all_mess += ((msg = this["ContentRegEx"]) != String.Empty) ? (GetPropertyDesc("ContentRegEx") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["ParamRegEx"]) != String.Empty) ? (GetPropertyDesc("ParamRegEx") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["ObjectType"]) != String.Empty) ? (GetPropertyDesc("ObjectType") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["ObjectDesc"]) != String.Empty) ? (GetPropertyDesc("ObjectDesc") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["GroupName"]) != String.Empty) ? (GetPropertyDesc("GroupName") + " :\n\t" + msg + "\n") : String.Empty;
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
          return "Contient la définition d'une syntaxe d'objet";
       }
       
       public static string GetPropertyDesc(string propertyName)
       {
          switch (propertyName)
          {
       
              case "ContentRegEx":
                  return "Expression de contenu";
       
              case "ParamRegEx":
                  return "Expression de paramètre";
       
              case "ObjectType":
                  return "Type";
       
              case "ObjectDesc":
                  return "Description";
       
              case "GroupName":
                  return "Groupe";
          }
          return "";
       }
       #endregion
       
       #region IEntityValidable
       // Test la validité de tous les champs
       public void Validate(){
          string errorCode;
       
          if(CheckField("ContentRegEx", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("ParamRegEx", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("ObjectType", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("ObjectDesc", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("GroupName", out errorCode) == false)
             throw new ApplicationException(errorCode);
       }
       
       // Test la validité d'un champ
       public bool CheckField(string propertyName, out string errorCode){
           errorCode = String.Empty;
           
           switch (propertyName)
           {
               case "ContentRegEx":
                 // Obligatoire
                 if(this.ContentRegEx == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 return AppModel.Format.NotEmpty.Validate(this.ContentRegEx.ToString(),ref errorCode);
       
               case "ParamRegEx":
                 if(this.ParamRegEx == null)
                   break;
                 return AppModel.Format.NotEmpty.Validate(this.ParamRegEx.ToString(),ref errorCode);
       
               case "ObjectType":
                 if(this.ObjectType == null)
                   break;
                 return AppModel.Format.NotEmpty.Validate(this.ObjectType.ToString(),ref errorCode);
       
               case "ObjectDesc":
                 if(this.ObjectDesc == null)
                   break;
                 return AppModel.Format.NotEmpty.Validate(this.ObjectDesc.ToString(),ref errorCode);
       
               case "GroupName":
                 if(this.GroupName == null)
                   break;
                 return AppModel.Format.NotEmpty.Validate(this.GroupName.ToString(),ref errorCode);
       
           }
           
           return true;
       }
       #endregion
       #endregion // Validation
      }

}