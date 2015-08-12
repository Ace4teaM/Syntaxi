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

namespace EditorModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class EditorStates : ISerializable    {
         #region Constructor
         public EditorStates(){

            // EditorSampleCode
            this.editorsamplecode = new Collection<EditorSampleCode>();
         }
         
         public EditorStates(String version) : this(){
            this.version = version;
         }
         #endregion // Constructor

         #region Fields
         // 
         protected String version;
         public String Version { get{ return version; } set{ version = value; } }
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
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("Version", Version, typeof(String));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            Version =  reader.ReadString();

            // EditorSampleCode
            size = reader.ReadInt32();
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
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(Version);

            // EditorSampleCode
            writer.Write(this.editorsamplecode.Count);
            if (this.editorsamplecode.Count > 0)
            {
                foreach (var col in this.editorsamplecode)
                    col.WriteBinary(writer);
            }
       }
       #endregion // Serialization

      }

}