﻿/*
   Extension de la classe d'entité Project

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

    public partial class Project : ISerializable    {
         #region Constructor
         public Project(){

            // ObjectContent
            this.objectcontent = new Collection<ObjectContent>();
            // SearchParams
            this.searchparams = new Collection<SearchParams>();
            // ObjectSyntax
            this.objectsyntax = new Collection<ObjectSyntax>();
         }
         
         public Project(String name, String version) : this(){
            this.name = name;
            this.version = version;
         }

         #endregion // Constructor
         
         #region Fields
         // 

         protected String name;
         public String Name { get{ return name; } set{ name = value; } }
         // 

         protected String version;
         public String Version { get{ return version; } set{ version = value; } }
         #endregion // Fields

         #region Associations
         // 
         protected Collection<ObjectContent> objectcontent;
         public virtual Collection<ObjectContent> ObjectContent { get{ return objectcontent; } set{ objectcontent = value; } }
         // 
         protected Collection<SearchParams> searchparams;
         public virtual Collection<SearchParams> SearchParams { get{ return searchparams; } set{ searchparams = value; } }
         // 
         protected Collection<ObjectSyntax> objectsyntax;
         public virtual Collection<ObjectSyntax> ObjectSyntax { get{ return objectsyntax; } set{ objectsyntax = value; } }
         #endregion // Associations

         #region Methods
         public override string ToString()
         {
             string result = this.GetType().Name+":"+Environment.NewLine+"-----------------------------"+Environment.NewLine;
             result += "Name = " + Name + Environment.NewLine;
             result += "Version = " + Version + Environment.NewLine;
             return result;
         }

         #endregion // Methods
         #region ISerializable
          // Implement this method to serialize data. The method is called on serialization.
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
              info.AddValue("Name", Name, typeof(String));
              info.AddValue("Version", Version, typeof(String));
          }
         #endregion // ISerializable
    
         #region Serialization
         public void ReadBinary(BinaryReader reader)
         {
            int size;
      
            // Properties
            Name =  reader.ReadString();
            Version =  reader.ReadString();

            // ObjectContent
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.objectcontent = new Collection<ObjectContent>(new ObjectContent[size]);
                foreach (var col in this.objectcontent)
                    col.ReadBinary(reader);
            }
            else
            {
                this.objectcontent = new Collection<ObjectContent>();
            }
            // SearchParams
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.searchparams = new Collection<SearchParams>(new SearchParams[size]);
                foreach (var col in this.searchparams)
                    col.ReadBinary(reader);
            }
            else
            {
                this.searchparams = new Collection<SearchParams>();
            }
            // ObjectSyntax
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.objectsyntax = new Collection<ObjectSyntax>(new ObjectSyntax[size]);
                foreach (var col in this.objectsyntax)
                    col.ReadBinary(reader);
            }
            else
            {
                this.objectsyntax = new Collection<ObjectSyntax>();
            }
         }
         
         public void WriteBinary(BinaryWriter writer)
         {
            // Properties
            writer.Write(Name);
            writer.Write(Version);

            // ObjectContent
            writer.Write(this.objectcontent.Count);
            if (this.objectcontent.Count > 0)
            {
                foreach (var col in this.objectcontent)
                    col.WriteBinary(writer);
            }
            // SearchParams
            writer.Write(this.searchparams.Count);
            if (this.searchparams.Count > 0)
            {
                foreach (var col in this.searchparams)
                    col.WriteBinary(writer);
            }
            // ObjectSyntax
            writer.Write(this.objectsyntax.Count);
            if (this.objectsyntax.Count > 0)
            {
                foreach (var col in this.objectsyntax)
                    col.WriteBinary(writer);
            }
       }

       #endregion // Serialization
      }
}