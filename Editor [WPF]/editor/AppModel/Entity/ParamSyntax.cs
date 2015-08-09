/*
   Extension de la classe d'entité ParamSyntax

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

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class ParamSyntax : ISerializable , INotifyPropertyChanged    {
         #region Constructor
         public ParamSyntax(){
         }
         
         public ParamSyntax(String contentregex, String paramregex, String paramtype) : this(){
            this.contentregex = contentregex;
            this.paramregex = paramregex;
            this.paramtype = paramtype;
         }

         #endregion // Constructor
         
         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region Fields
         // 
         protected String contentregex;
         public String ContentRegEx { get{ return contentregex; } set{ contentregex = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ContentRegEx")); } }
         // 
         protected String paramregex;
         public String ParamRegEx { get{ return paramregex; } set{ paramregex = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamRegEx")); } }
         // 
         protected String paramtype;
         public String ParamType { get{ return paramtype; } set{ paramtype = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamType")); } }
         #endregion // Fields

         #region Associations
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "ContentRegEx = " + ContentRegEx + Environment.NewLine;
             result += "ParamRegEx = " + ParamRegEx + Environment.NewLine;
             result += "ParamType = " + ParamType + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("ContentRegEx", ContentRegEx, typeof(String));
              info.AddValue("ParamRegEx", ParamRegEx, typeof(String));
              info.AddValue("ParamType", ParamType, typeof(String));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            ContentRegEx =  reader.ReadString();
            ParamRegEx =  reader.ReadString();
            ParamType =  reader.ReadString();
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(ContentRegEx);
            writer.Write(ParamRegEx);
            writer.Write(ParamType);
       }

       #endregion // Serialization
      }
}