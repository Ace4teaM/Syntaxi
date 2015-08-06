/*
   Extension de la classe d'entité ObjectContent

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

    public partial class ObjectContent : ISerializable    {
         #region Constructor
         public ObjectContent(){

            // ObjectParam
            this.objectparam = new Collection<ObjectParam>();
         }
         
         public ObjectContent(String id, String objecttype, String filename, String position) : this(){
            this.id = id;
            this.objecttype = objecttype;
            this.filename = filename;
            this.position = position;
         }

         #endregion // Constructor
         
         #region Fields
         // Identifiant

         protected String id;
         public String Id { get{ return id; } set{ id = value; } }
         // Type d'objet

         protected String objecttype;
         public String ObjectType { get{ return objecttype; } set{ objecttype = value; } }
         // Emplacement du fichier source

         protected String filename;
         public String Filename { get{ return filename; } set{ filename = value; } }
         // Position de départ dans le fichier source

         protected String position;
         public String Position { get{ return position; } set{ position = value; } }
         #endregion // Fields

         #region Associations
         // 
         protected Collection<ObjectParam> objectparam;
         public virtual Collection<ObjectParam> ObjectParam { get{ return objectparam; } set{ objectparam = value; } }
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "Id = " + Id + Environment.NewLine;
             result += "ObjectType = " + ObjectType + Environment.NewLine;
             result += "Filename = " + Filename + Environment.NewLine;
             result += "Position = " + Position + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("Id", Id, typeof(String));
              info.AddValue("ObjectType", ObjectType, typeof(String));
              info.AddValue("Filename", Filename, typeof(String));
              info.AddValue("Position", Position, typeof(String));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            Id =  reader.ReadString();
            ObjectType =  reader.ReadString();
            Filename =  reader.ReadString();
            Position =  reader.ReadString();

            // ObjectParam
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.objectparam = new Collection<ObjectParam>(new ObjectParam[size]);
                foreach (var col in this.objectparam)
                    col.ReadBinary(reader);
            }
            else
            {
                this.objectparam = new Collection<ObjectParam>();
            }
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(Id);
            writer.Write(ObjectType);
            writer.Write(Filename);
            writer.Write(Position);

            // ObjectParam
            writer.Write(this.objectparam.Count);
            if (this.objectparam.Count > 0)
            {
                foreach (var col in this.objectparam)
                    col.WriteBinary(writer);
            }
       }

       #endregion // Serialization
      }
}