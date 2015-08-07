/*
   Extension de la classe d'entité ParamContent

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

    public partial class ParamContent : ISerializable    {
         #region Constructor
         public ParamContent(){
         }
         
         public ParamContent(String paramname, String paramvalue) : this(){
            this.paramname = paramname;
            this.paramvalue = paramvalue;
         }

         #endregion // Constructor
         
         #region Fields
         // Nom

         protected String paramname;
         public String ParamName { get{ return paramname; } set{ paramname = value; } }
         // Valeur

         protected String paramvalue;
         public String ParamValue { get{ return paramvalue; } set{ paramvalue = value; } }
         #endregion // Fields

         #region Associations
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "ParamName = " + ParamName + Environment.NewLine;
             result += "ParamValue = " + ParamValue + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("ParamName", ParamName, typeof(String));
              info.AddValue("ParamValue", ParamValue, typeof(String));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            ParamName =  reader.ReadString();
            ParamValue =  reader.ReadString();
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(ParamName);
            writer.Write(ParamValue);
       }

       #endregion // Serialization
      }
}