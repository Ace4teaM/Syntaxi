/*
   Extension de la classe d'entité EditorStates

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
using System.Xml;

namespace EditorModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class EditorStates : IEntity, ISerializable, IEntitySerializable    {
         #region Constructor
         public EditorStates(){

            // EditorSampleCode
            this.editorsamplecode = new Collection<EditorSampleCode>();
            // Version
            this.version = String.Empty;
            // SelectedDatabaseSourceId
            this.selecteddatabasesourceid = String.Empty;
         }
         
         public EditorStates(String version, String selecteddatabasesourceid) : this(){
            this.version = version;
            this.selecteddatabasesourceid = selecteddatabasesourceid;
         }
         #endregion // Constructor
         
          public string EntityName { get{ return "EditorStates"; } }

         #region State
        private EntityState entityState;
        public EntityState EntityState { get{ return entityState; } set{ entityState = value; } }

         #endregion // State
        
         #region Fields
         // 
         protected String version;
         public String Version { get{ return version; } set{ version = value; } }
         // 
         protected String selecteddatabasesourceid;
         public String SelectedDatabaseSourceId { get{ return selecteddatabasesourceid; } set{ selecteddatabasesourceid = value; } }
         #endregion // Fields

         #region Associations
         // 
         protected Collection<EditorSampleCode> editorsamplecode;
         public virtual Collection<EditorSampleCode> EditorSampleCode { get{ return editorsamplecode; } set{ editorsamplecode = value; } }
         public void AddEditorSampleCode(EditorSampleCode obj){
            obj.EditorStates = this;
            EditorSampleCode.Add(obj);
         }
         
         public void RemoveEditorSampleCode(EditorSampleCode obj){
            obj.EditorStates = null;
            EditorSampleCode.Remove(obj);
         }
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "Version = " + Version + Environment.NewLine;
             result += "SelectedDatabaseSourceId = " + SelectedDatabaseSourceId + Environment.NewLine;
             return result;
         }

         #endregion // Methods

       #region ISerializable
        // Implement this method to serialize data. The method is called on serialization.
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Version", Version, typeof(String));
            info.AddValue("SelectedDatabaseSourceId", SelectedDatabaseSourceId, typeof(String));
                 }
       #endregion // ISerializable
       
       #region Serialization
       public void ReadBinary(BinaryReader reader)
       {
          // Properties
          Version =  reader.ReadString();
          SelectedDatabaseSourceId =  reader.ReadString();
       
          // EditorSampleCode
          {
             int size = reader.ReadInt32();
             if (size > 0)
             {
                 this.EditorSampleCode = new Collection<EditorSampleCode>();
                 for(int i=0;i<size;i++){
                     EditorSampleCode o = new EditorSampleCode();
                     o.ReadBinary(reader);
                     this.AddEditorSampleCode(o);
                 }
             }
             else
             {
                 this.EditorSampleCode = new Collection<EditorSampleCode>();
             }
          }
       }
       
       public void WriteBinary(BinaryWriter writer)
       {
          // Properties
          writer.Write(Version);
          writer.Write(SelectedDatabaseSourceId);
       
          // EditorSampleCode
          writer.Write(this.editorsamplecode.Count);
          if (this.editorsamplecode.Count > 0)
          {
              foreach (var col in this.editorsamplecode)
                  col.WriteBinary(writer);
          }}
       
       
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
          XmlElement cur = doc.CreateElement("EditorStates");
          parent.AppendChild(cur);
              
          //
          // Fields
          //
          
       		// Assigne le membre Version
          if (version != null)
          {
              curMember = doc.CreateElement("Version");
              curMember.AppendChild(doc.CreateTextNode(version.ToString()));
              cur.AppendChild(curMember);
          }
       
       		// Assigne le membre SelectedDatabaseSourceId
          if (selecteddatabasesourceid != null)
          {
              curMember = doc.CreateElement("SelectedDatabaseSourceId");
              curMember.AppendChild(doc.CreateTextNode(selecteddatabasesourceid.ToString()));
              cur.AppendChild(curMember);
          }
          
          //
          // Aggregations
          //
       
          // EditorSampleCode
          {
             curMember = doc.CreateElement("EditorSampleCode");
             if (this.editorsamplecode.Count > 0)
             {
                 foreach (var col in this.editorsamplecode)
                     col.ToXml(curMember);
             }
             cur.AppendChild(curMember);
          }
       
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
       
                // Assigne le membre Version
                case "Version":
                {
                   this.version = property_value;
                }
                break;
                // Assigne le membre SelectedDatabaseSourceId
                case "SelectedDatabaseSourceId":
                {
                   this.selecteddatabasesourceid = property_value;
                }
                break;
       
                //
                // Aggregations
                //
                
                // Assigne la collection EditorSampleCode
                case "EditorSampleCode":
                   {
                      foreach (XmlElement c in m.ChildNodes)
                      {
                         if("EditorSampleCode" == m.Name){
                             EditorSampleCode value = new EditorSampleCode();
                             value.FromXml(c);
                             this.AddEditorSampleCode(value);
                         }
                      }
                   }
                   break;
       			}
          }
       }
       
       #endregion // Serialization


      }

}