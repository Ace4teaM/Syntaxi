/*
   Extension de la classe d'entité EditorSampleCode

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

    public partial class EditorSampleCode : IEntity, ISerializable, IEntitySerializable    {
         #region Constructor
         public EditorSampleCode(){
            // Text
            this.text = String.Empty;
            // ObjectSyntaxType
            this.objectsyntaxtype = String.Empty;
         }
         
         // copie
         public EditorSampleCode(EditorSampleCode src) : this(){
            Copy(this, src);
         }

         public EditorSampleCode(String text, String objectsyntaxtype) : this(){
            this.text = text;
            this.objectsyntaxtype = objectsyntaxtype;
         }
         #endregion // Constructor
         
          public string EntityName { get{ return "EditorSampleCode"; } }

         // clone
         public IEntity Clone(){
            return Copy(new EditorSampleCode(), this);
         }

         // copie
         public IEntity Copy(IEntity _dst,IEntity _src){
            EditorSampleCode src = _src as EditorSampleCode;
            EditorSampleCode dst = _dst as EditorSampleCode;
            if(src==null || dst==null)
               return null;
               
            // Text
            dst.text = src.text;
            // ObjectSyntaxType
            dst.objectsyntaxtype = src.objectsyntaxtype;
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
         protected String text;
         public String Text { get{ return text; } set{ text = value; } }
         // 
         protected String objectsyntaxtype;
         public String ObjectSyntaxType { get{ return objectsyntaxtype; } set{ objectsyntaxtype = value; } }
         #endregion // Fields

         #region Associations

         //
         // EditorStates
         // 

         protected EditorStates editorstates;
         public virtual EditorStates EditorStates { get{ return editorstates; } set{ editorstates = value; } }

         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "Text = " + Text + Environment.NewLine;
             result += "ObjectSyntaxType = " + ObjectSyntaxType + Environment.NewLine;
             return result;
         }

         #endregion // Methods

       #region ISerializable
       // Implement this method to serialize data. The method is called on serialization.
       public void GetObjectData(SerializationInfo info, StreamingContext context)
       {
            info.AddValue("Text", Text, typeof(String));
            info.AddValue("ObjectSyntaxType", ObjectSyntaxType, typeof(String));
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
          Text =  reader.ReadString();
          ObjectSyntaxType =  reader.ReadString();
       }
       
       public void WriteBinary(BinaryWriter writer)
       {
          // Properties
          writer.Write(Text);
          writer.Write(ObjectSyntaxType);
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
          XmlElement cur = doc.CreateElement("EditorSampleCode");
          parent.AppendChild(cur);
              
          //
          // Fields
          //
          
          // Assigne le membre Text
          if (text != null)
          {
              curMember = doc.CreateElement("Text");
              curMember.AppendChild(doc.CreateTextNode(text.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre ObjectSyntaxType
          if (objectsyntaxtype != null)
          {
              curMember = doc.CreateElement("ObjectSyntaxType");
              curMember.AppendChild(doc.CreateTextNode(objectsyntaxtype.ToString()));
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
       
                // Assigne le membre Text
                case "Text":
                {
                   this.text = property_value;
                }
                break;
                // Assigne le membre ObjectSyntaxType
                case "ObjectSyntaxType":
                {
                   this.objectsyntaxtype = property_value;
                }
                break;
       
                //
                // Compositions
                //
                
       			}
          }
       }
       
       #endregion // Serialization


      }

}