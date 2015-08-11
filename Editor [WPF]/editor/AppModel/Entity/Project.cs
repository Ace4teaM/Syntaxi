/*
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
using Lib;
using System.IO;
using System.Runtime.Serialization;

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class Project : ISerializable , INotifyPropertyChanged    {
         #region Constructor
         public Project(){

            // ObjectContent
            this.objectcontent = new Collection<ObjectContent>();
            // SearchParams
            this.searchparams = new Collection<SearchParams>();
            // ObjectSyntax
            this.objectsyntax = new Collection<ObjectSyntax>();
            // ParamSyntax
            this.paramsyntax = new Collection<ParamSyntax>();
         }
         
         public Project(String name, String version) : this(){
            this.name = name;
            this.version = version;
         }
         #endregion // Constructor

         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region Fields
         // 
         protected String name;
         public String Name { get{ return name; } set{ name = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Name")); } }
         // 
         protected String version;
         public String Version { get{ return version; } set{ version = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Version")); } }
         #endregion // Fields

         #region Associations
         // 
         protected Collection<ObjectContent> objectcontent;
         public virtual Collection<ObjectContent> ObjectContent { get{ return objectcontent; } set{ objectcontent = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectContent"));  } }
         public void AddObjectContent(ObjectContent obj){
            obj.Project = this;
            ObjectContent.Add(obj);
         }
         // 
         protected Collection<SearchParams> searchparams;
         public virtual Collection<SearchParams> SearchParams { get{ return searchparams; } set{ searchparams = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("SearchParams"));  } }
         public void AddSearchParams(SearchParams obj){
            obj.Project = this;
            SearchParams.Add(obj);
         }
         // 
         protected Collection<ObjectSyntax> objectsyntax;
         public virtual Collection<ObjectSyntax> ObjectSyntax { get{ return objectsyntax; } set{ objectsyntax = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectSyntax"));  } }
         public void AddObjectSyntax(ObjectSyntax obj){
            obj.Project = this;
            ObjectSyntax.Add(obj);
         }
         // 
         protected Collection<ParamSyntax> paramsyntax;
         public virtual Collection<ParamSyntax> ParamSyntax { get{ return paramsyntax; } set{ paramsyntax = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("ParamSyntax"));  } }
         public void AddParamSyntax(ParamSyntax obj){
            obj.Project = this;
            ParamSyntax.Add(obj);
         }
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
                this.ObjectContent = new Collection<ObjectContent>();
                for(int i=0;i<size;i++){
                    ObjectContent o = new ObjectContent();
                    o.ReadBinary(reader);
                    this.AddObjectContent(o);
                }
            }
            else
            {
                this.ObjectContent = new Collection<ObjectContent>();
            }
            // SearchParams
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.SearchParams = new Collection<SearchParams>();
                for(int i=0;i<size;i++){
                    SearchParams o = new SearchParams();
                    o.ReadBinary(reader);
                    this.AddSearchParams(o);
                }
            }
            else
            {
                this.SearchParams = new Collection<SearchParams>();
            }
            // ObjectSyntax
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.ObjectSyntax = new Collection<ObjectSyntax>();
                for(int i=0;i<size;i++){
                    ObjectSyntax o = new ObjectSyntax();
                    o.ReadBinary(reader);
                    this.AddObjectSyntax(o);
                }
            }
            else
            {
                this.ObjectSyntax = new Collection<ObjectSyntax>();
            }
            // ParamSyntax
            size = reader.ReadInt32();
            if (size > 0)
            {
                this.ParamSyntax = new Collection<ParamSyntax>();
                for(int i=0;i<size;i++){
                    ParamSyntax o = new ParamSyntax();
                    o.ReadBinary(reader);
                    this.AddParamSyntax(o);
                }
            }
            else
            {
                this.ParamSyntax = new Collection<ParamSyntax>();
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
            // ParamSyntax
            writer.Write(this.paramsyntax.Count);
            if (this.paramsyntax.Count > 0)
            {
                foreach (var col in this.paramsyntax)
                    col.WriteBinary(writer);
            }
       }
       #endregion // Serialization

      }

}