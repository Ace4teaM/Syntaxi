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

    public partial class DatabaseSource : ISerializable, IEntitySerializable , INotifyPropertyChanged    {
         #region Constructor
         public DatabaseSource(){
            // Id
            this.id = String.Empty;
            // Provider
            this.provider = new Int32();
            // ConnectionString
            this.connectionstring = String.Empty;
         }
         
         public DatabaseSource(String id, DatabaseProvider? provider, String connectionstring) : this(){
            this.id = id;
            this.provider = provider;
            this.connectionstring = connectionstring;
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
         // Identifiant de la source
         protected String id;
         public String Id { get{ return id; } set{ id = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Id")); } }
         // 
         protected DatabaseProvider? provider;
         public DatabaseProvider? Provider { get{ return provider; } set{ provider = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Provider")); } }
         // 
         protected String connectionstring;
         public String ConnectionString { get{ return connectionstring; } set{ connectionstring = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ConnectionString")); } }
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
         public void ReadBinary(BinaryReader reader)
         {
            // Properties
            Id =  reader.ReadString();
            Provider = (DatabaseProvider) reader.ReadInt32();
            ConnectionString =  reader.ReadString();
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(Id);
            writer.Write((Int32)Provider.Value);
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
            curMember = doc.CreateElement("Id");
            curMember.AppendChild(doc.CreateTextNode(id.ToString()));
            cur.AppendChild(curMember);

       		// Assigne le membre Provider
            if (provider != null)
            {
                curMember = doc.CreateElement("Provider");
                curMember.AppendChild(doc.CreateTextNode(provider.ToString()));
                cur.AppendChild(curMember);
            }

       		// Assigne le membre ConnectionString
            curMember = doc.CreateElement("ConnectionString");
            curMember.AppendChild(doc.CreateTextNode(connectionstring.ToString()));
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
                        this.Provider = (DatabaseProvider)value;
                  }
                  break;
                  // Assigne le membre ConnectionString
                  case "ConnectionString":
                  {
                     this.connectionstring = property_value;
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