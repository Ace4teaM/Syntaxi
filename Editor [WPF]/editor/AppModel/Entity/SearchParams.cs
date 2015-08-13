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
using Lib;
using System.IO;
using System.Runtime.Serialization;

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class SearchParams : ISerializable , INotifyPropertyChanged    {
         #region Constructor
         public SearchParams(){
         }
         
         public SearchParams(String inputdir, String inputfilter, bool recursive) : this(){
            this.inputdir = inputdir;
            this.inputfilter = inputfilter;
            this.recursive = recursive;
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
         // Dossier de recherche
         protected String inputdir;
         public String InputDir { get{ return inputdir; } set{ inputdir = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("InputDir")); } }
         // Filtre de recherche sur les nom de fichiers
         protected String inputfilter;
         public String InputFilter { get{ return inputfilter; } set{ inputfilter = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("InputFilter")); } }
         // Récursif
         protected bool recursive;
         public bool Recursive { get{ return recursive; } set{ recursive = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Recursive")); } }
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