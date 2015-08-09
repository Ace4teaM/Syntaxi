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

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class ObjectContent : ISerializable , INotifyPropertyChanged    {
         #region Constructor
         public ObjectContent(){

            // ParamContent
            this.paramcontent = new Collection<ParamContent>();
         }
         
         public ObjectContent(String id, String objecttype, String filename, int? position) : this(){
            this.id = id;
            this.objecttype = objecttype;
            this.filename = filename;
            this.position = position;
         }

         #endregion // Constructor
         
         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region Fields
         // Identifiant
         protected String id;
         public String Id { get{ return id; } set{ id = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Id")); } }
         // Type d'objet
         protected String objecttype;
         public String ObjectType { get{ return objecttype; } set{ objecttype = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectType")); } }
         // Emplacement du fichier source
         protected String filename;
         public String Filename { get{ return filename; } set{ filename = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Filename")); } }
         // Position de départ dans le fichier source
         protected int? position;
         public int? Position { get{ return position; } set{ position = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Position")); } }
         #endregion // Fields

         #region Associations
         // 
         protected Collection<ParamContent> paramcontent;
         public virtual Collection<ParamContent> ParamContent { get{ return paramcontent; } set{ paramcontent = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamContent"));  } }
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
              info.AddValue("Position", Position, typeof(int));
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
            Position =  reader.ReadInt32();

            // ParamContent
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.paramcontent = new Collection<ParamContent>();
                for(int i=0;i<size;i++){
                    ParamContent o = new ParamContent();
                    o.ReadBinary(reader);
                    this.paramcontent.Add(o);
                }
            }
            else
            {
                this.paramcontent = new Collection<ParamContent>();
            }
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(Id);
            writer.Write(ObjectType);
            writer.Write(Filename);
            writer.Write(Position.Value);

            // ParamContent
            writer.Write(this.paramcontent.Count);
            if (this.paramcontent.Count > 0)
            {
                foreach (var col in this.paramcontent)
                    col.WriteBinary(writer);
            }
       }

       #endregion // Serialization
      }
}