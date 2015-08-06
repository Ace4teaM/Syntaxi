/*
   Extension de la classe d'entité ObjectSyntax

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

    public partial class ObjectSyntax : ISerializable    {
         #region Constructor
         public ObjectSyntax(){
         }
         
         public ObjectSyntax(String contentregex, String paramregex, String objecttype) : this(){
            this.contentregex = contentregex;
            this.paramregex = paramregex;
            this.objecttype = objecttype;
         }

         #endregion // Constructor
         
         #region Fields
         // 

         protected String contentregex;
         public String ContentRegEx { get{ return contentregex; } set{ contentregex = value; } }
         // 

         protected String paramregex;
         public String ParamRegEx { get{ return paramregex; } set{ paramregex = value; } }
         // 

         protected String objecttype;
         public String ObjectType { get{ return objecttype; } set{ objecttype = value; } }
         #endregion // Fields

         #region Associations
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "ContentRegEx = " + ContentRegEx + Environment.NewLine;
             result += "ParamRegEx = " + ParamRegEx + Environment.NewLine;
             result += "ObjectType = " + ObjectType + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("ContentRegEx", ContentRegEx, typeof(String));
              info.AddValue("ParamRegEx", ParamRegEx, typeof(String));
              info.AddValue("ObjectType", ObjectType, typeof(String));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            ContentRegEx =  reader.ReadString();
            ParamRegEx =  reader.ReadString();
            ObjectType =  reader.ReadString();
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(ContentRegEx);
            writer.Write(ParamRegEx);
            writer.Write(ObjectType);
       }

       #endregion // Serialization
      }
}