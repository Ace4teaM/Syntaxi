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
using Serial = System.Int32;

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
            //this.editorsamplecode = new Collection<EditorSampleCode>();
            // Version
            this.version = String.Empty;
            // SelectedDatabaseSourceId
            this.selecteddatabasesourceid = String.Empty;
         }
         
         // copie
         public EditorStates(EditorStates src) : this(){
            Copy(this, src);
         }

         public EditorStates(String version, String selecteddatabasesourceid) : this(){
            this.version = version;
            this.selecteddatabasesourceid = selecteddatabasesourceid;
         }
         #endregion // Constructor
         
          public string EntityName { get{ return "EditorStates"; } }

         // clone
         public IEntity Clone(){
            return Copy(new EditorStates(), this);
         }

         // copie
         public IEntity Copy(IEntity _dst,IEntity _src){
            EditorStates src = _src as EditorStates;
            EditorStates dst = _dst as EditorStates;
            if(src==null || dst==null)
               return null;
               
            // Version
            dst.version = src.version;
            // SelectedDatabaseSourceId
            dst.selecteddatabasesourceid = src.selecteddatabasesourceid;
            return dst;
         }

         #region Model
         private IModel model;
         public IModel Model { get { return model; } set { model = value; } }
         #endregion // Model

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
         // EditorSampleCode
         // 

         // Simple association
         public virtual IEnumerable<EditorSampleCode> EditorSampleCode
         {
             get { return this.Model.Objs.OfType<EditorSampleCode>().Where(p=>p.EditorStates == this); }
         }
         public void AddEditorSampleCode(EditorSampleCode obj)
         {
            this.Model.Add(obj);
            obj.EditorStates = this;
            obj.Model = this.Model;//assure l'initialisation (normalement par this.Model.Add)

         }
   
         public void RemoveEditorSampleCode(EditorSampleCode obj)
         {
            obj.Model.Remove(obj);
            obj.EditorStates = null;
            obj.Model = null;

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
       /// <summary>
       /// Initialise l'instance depuis les données d'un flux binaire
       /// </summary>
       /// <param name="reader">Flux binaire</param>
       /// <param name="aggregationCallback">Permet d'appliquer des modifications aux entités importées par aggrégation</param>
       /// <remarks>Seuls les éléments existants dans le noeud Xml son importés dans l'objet</remarks>
       public void ReadBinary(BinaryReader reader, EntityCallback aggregationCallback)
       {
          // Properties
          Version =  reader.ReadString();
          SelectedDatabaseSourceId =  reader.ReadString();
       
          // EditorSampleCode
          {
             int size = reader.ReadInt32();
             if (size > 0)
             {
                 //this.EditorSampleCode = new Collection<EditorSampleCode>();
                 for(int i=0;i<size;i++){
                     EditorSampleCode o = new EditorSampleCode();
                     this.Model.Add(o);
                     o.ReadBinary(reader, aggregationCallback);
                     if (aggregationCallback != null)
                        aggregationCallback(o);
                     this.AddEditorSampleCode(o);
                 }
             }
             //else
             //{
             //    this.EditorSampleCode = new Collection<EditorSampleCode>();
             //}
          }
       }
       
       public void WriteBinary(BinaryWriter writer)
       {
          // Properties
          writer.Write(Version);
          writer.Write(SelectedDatabaseSourceId);
       
          // EditorSampleCode
          List<EditorSampleCode> editorsamplecode = this.EditorSampleCode.ToList();
          writer.Write(editorsamplecode.Count);
          if (editorsamplecode.Count > 0)
          {
              foreach (var col in editorsamplecode)
                  col.WriteBinary(writer);
          }
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
          // Compositions
          //
       
          // EditorSampleCode
          {
             List<EditorSampleCode> editorsamplecode = this.EditorSampleCode.ToList();
             curMember = doc.CreateElement("EditorSampleCode");
             if (editorsamplecode.Count > 0)
             {
                 foreach (var col in editorsamplecode)
                     col.ToXml(curMember);
             }
             cur.AppendChild(curMember);
          }
       
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
                // Compositions
                //
                
                // Assigne la collection EditorSampleCode
                case "EditorSampleCode":
                   {
                      foreach (XmlElement c in m.ChildNodes)
                      {
                         if("EditorSampleCode" == c.Name){
                             EditorSampleCode value = new EditorSampleCode();
                             value.FromXml(c, aggregationCallback);
                             if (aggregationCallback != null)
                                aggregationCallback(value);
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