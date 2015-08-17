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

namespace EditorModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class EditorSampleCode : ISerializable    {
         #region Constructor
         public EditorSampleCode(){
            // Text
            this.text = String.Empty;
            // ObjectSyntaxType
            this.objectsyntaxtype = String.Empty;
         }
         
         public EditorSampleCode(String text, String objectsyntaxtype) : this(){
            this.text = text;
            this.objectsyntaxtype = objectsyntaxtype;
         }
         #endregion // Constructor

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
         public void ReadBinary(BinaryReader reader)
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
       #endregion // Serialization

      }

}