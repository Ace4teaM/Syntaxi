/*
   Extension de la classe d'entité SearchParams

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
using System.IO;
using System.Runtime.Serialization;

namespace Data.Entity
{
    /// <summary>
    /// Implémente la validation des propriétés
    /// </summary>
   [Serializable]

    public partial class SearchParams : ISerializable    {
         #region Constructor
         public SearchParams(){
         }
         
         public SearchParams(String inputdir, String inputfilter, bool recursive) : this(){
            this.inputdir = inputdir;
            this.inputfilter = inputfilter;
            this.recursive = recursive;
         }

         #endregion // Constructor
         
         #region Fields
         // Dossier de recherche

         protected String inputdir;
         public String InputDir { get{ return inputdir; } set{ inputdir = value; } }
         // Filtre de recherche sur les nom de fichiers

         protected String inputfilter;
         public String InputFilter { get{ return inputfilter; } set{ inputfilter = value; } }
         // Récursif

         protected bool recursive;
         public bool Recursive { get{ return recursive; } set{ recursive = value; } }
         #endregion // Fields

         #region Associations
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "InputDir = " + InputDir + Environment.NewLine;
             result += "InputFilter = " + InputFilter + Environment.NewLine;
             result += "Recursive = " + Recursive + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("InputDir", InputDir, typeof(String));
              info.AddValue("InputFilter", InputFilter, typeof(String));
              info.AddValue("Recursive", Recursive, typeof(bool));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            InputDir =  reader.ReadString();
            InputFilter =  reader.ReadString();
            Recursive =  reader.ReadBoolean();
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(InputDir);
            writer.Write(InputFilter);
            writer.Write(Recursive);
       }

       #endregion // Serialization
      }
}