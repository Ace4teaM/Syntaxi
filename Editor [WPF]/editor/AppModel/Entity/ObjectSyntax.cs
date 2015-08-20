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
using AppModel.Domain;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class ObjectSyntax : ISerializable, IEntitySerializable , INotifyPropertyChanged    {
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
         }
         
         public ObjectSyntax(String contentregex, String paramregex, String objecttype, String objectdesc) : this(){
            this.contentregex = contentregex;
            this.paramregex = paramregex;
            this.objecttype = objecttype;
            this.objectdesc = objectdesc;
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
         protected String contentregex;
         public String ContentRegEx { get{ return contentregex; } set{ contentregex = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ContentRegEx")); } }
         // 
         protected String paramregex;
         public String ParamRegEx { get{ return paramregex; } set{ paramregex = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamRegEx")); } }
         // 
         protected String objecttype;
         public String ObjectType { get{ return objecttype; } set{ objecttype = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectType")); } }
         // 
         protected String objectdesc;
         public String ObjectDesc { get{ return objectdesc; } set{ objectdesc = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectDesc")); } }
         #endregion // Fields

         #region Associations
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
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            // Properties
            ContentRegEx =  reader.ReadString();
            ParamRegEx =  reader.ReadString();
            ObjectType =  reader.ReadString();
            ObjectDesc =  reader.ReadString();
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(ContentRegEx);
            writer.Write(ParamRegEx);
            writer.Write(ObjectType);
            writer.Write(ObjectDesc);
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
            curMember = doc.CreateElement("ContentRegEx");
            curMember.AppendChild(doc.CreateTextNode(contentregex.ToString()));
            cur.AppendChild(curMember);

       		// Assigne le membre ParamRegEx
            curMember = doc.CreateElement("ParamRegEx");
            curMember.AppendChild(doc.CreateTextNode(paramregex.ToString()));
            cur.AppendChild(curMember);

       		// Assigne le membre ObjectType
            curMember = doc.CreateElement("ObjectType");
            curMember.AppendChild(doc.CreateTextNode(objecttype.ToString()));
            cur.AppendChild(curMember);

       		// Assigne le membre ObjectDesc
            curMember = doc.CreateElement("ObjectDesc");
            curMember.AppendChild(doc.CreateTextNode(objectdesc.ToString()));
            cur.AppendChild(curMember);
            
            //
            // Aggregations
            //

            parent.AppendChild(cur);
            return doc.InnerXml;
        }
    	
        /// <summary>
        /// Initialise l'instance avec les données de l'élément XML
        /// </summary>
        /// <param name="element">Élément contenant les information sur l'objet</param>
        /// <remarks>Seuls les éléments existants dans le noeud Xml son importés dans l'objet</remarks>
        public void FromXml(XmlElement element)
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

                  //
                  // Aggregations
                  //
                  
       			}
            }
        }

       #endregion // Serialization

      }

}