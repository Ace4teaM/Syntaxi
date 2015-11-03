/*
   Extension de la classe d'entité Project

   !!Attention!!
   Ce code source est généré automatiquement, toute modification sera perdue
   
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
using AppModel.Format;
using AppModel.Domain;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Data.Common;
using Serial = System.Int32;

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class Project : IEntity, ISerializable, IEntitySerializable, INotifyPropertyChanged, IEntityPersistent, IDataErrorInfo, IEntityValidable    {
         #region Constructor
         public Project(){

            // ObjectContent
            //this.objectcontent = new Collection<ObjectContent>();
            // SearchParams
            //this.searchparams = new Collection<SearchParams>();
            // ObjectSyntax
            //this.objectsyntax = new Collection<ObjectSyntax>();
            // ParamSyntax
            //this.paramsyntax = new Collection<ParamSyntax>();
            // DatabaseSource
            //this.databasesource = new Collection<DatabaseSource>();
            // Name
            this.name = String.Empty;
            // Version
            this.version = String.Empty;
         }
         
         // copie
         public Project(Project src) : this(){
            Copy(this, src);
         }

         public Project(String name, String version) : this(){
            this.name = name;
            this.version = version;
         }
         #endregion // Constructor
         
          public string EntityName { get{ return "Project"; } }

         // clone
         public IEntity Clone(){
            return Copy(new Project(), this);
         }

         // copie
         public IEntity Copy(IEntity _dst,IEntity _src){
            Project src = _src as Project;
            Project dst = _dst as Project;
            if(src==null || dst==null)
               return null;
               
            // Name
            dst.name = src.name;
            // Version
            dst.version = src.version;
            return dst;
         }

         #region INotifyPropertyChanged
         public event PropertyChangedEventHandler PropertyChanged;
         #endregion // INotifyPropertyChanged

         #region Model
         private IModel model;
         public IModel Model { get { return model; } set { model = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Model")); } }
         #endregion // Model

         #region State
         private EntityState entityState;
         public EntityState EntityState { get{ return entityState; } set{ entityState = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("EntityState")); } }
         #endregion // State
        
         #region Fields
         // Nom
         protected String name;
         public String Name { get{ return name; } set{ name = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Name")); } }
         // Version
         protected String version;
         public String Version { get{ return version; } set{ version = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Version")); } }
         #endregion // Fields

         #region Associations

         //
         // ObjectContent
         // 

         // Simple association
         public virtual IEnumerable<ObjectContent> ObjectContent
         {
             get { return this.Model.Objs.OfType<ObjectContent>().Where(p=>p.Project == this); }
         }
         public void AddObjectContent(ObjectContent obj)
         {
            this.Model.Add(obj);
            obj.Project = this;
            obj.Factory = this.Factory;
            obj.Model = this.Model;//assure l'initialisation (normalement par this.Model.Add)
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectContent")); 
         }
   
         public void RemoveObjectContent(ObjectContent obj)
         {
            obj.Model.Remove(obj);
            obj.Project = null;
            obj.Factory = this.Factory;
            obj.Model = null;
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectContent")); 
         }
         //
         // SearchParams
         // 

         // Simple association
         public virtual IEnumerable<SearchParams> SearchParams
         {
             get { return this.Model.Objs.OfType<SearchParams>().Where(p=>p.Project == this); }
         }
         public void AddSearchParams(SearchParams obj)
         {
            this.Model.Add(obj);
            obj.Project = this;
            obj.Model = this.Model;//assure l'initialisation (normalement par this.Model.Add)
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("SearchParams")); 
         }
   
         public void RemoveSearchParams(SearchParams obj)
         {
            obj.Model.Remove(obj);
            obj.Project = null;
            obj.Model = null;
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("SearchParams")); 
         }
         //
         // ObjectSyntax
         // 

         // Simple association
         public virtual IEnumerable<ObjectSyntax> ObjectSyntax
         {
             get { return this.Model.Objs.OfType<ObjectSyntax>().Where(p=>p.Project == this); }
         }
         public void AddObjectSyntax(ObjectSyntax obj)
         {
            this.Model.Add(obj);
            obj.Project = this;
            obj.Model = this.Model;//assure l'initialisation (normalement par this.Model.Add)
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectSyntax")); 
         }
   
         public void RemoveObjectSyntax(ObjectSyntax obj)
         {
            obj.Model.Remove(obj);
            obj.Project = null;
            obj.Model = null;
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("ObjectSyntax")); 
         }
         //
         // ParamSyntax
         // 

         // Simple association
         public virtual IEnumerable<ParamSyntax> ParamSyntax
         {
             get { return this.Model.Objs.OfType<ParamSyntax>().Where(p=>p.Project == this); }
         }
         public void AddParamSyntax(ParamSyntax obj)
         {
            this.Model.Add(obj);
            obj.Project = this;
            obj.Model = this.Model;//assure l'initialisation (normalement par this.Model.Add)
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("ParamSyntax")); 
         }
   
         public void RemoveParamSyntax(ParamSyntax obj)
         {
            obj.Model.Remove(obj);
            obj.Project = null;
            obj.Model = null;
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("ParamSyntax")); 
         }
         //
         // DatabaseSource
         // 

         // Simple association
         public virtual IEnumerable<DatabaseSource> DatabaseSource
         {
             get { return this.Model.Objs.OfType<DatabaseSource>().Where(p=>p.Project == this); }
         }
         public void AddDatabaseSource(DatabaseSource obj)
         {
            this.Model.Add(obj);
            obj.Project = this;
            obj.Model = this.Model;//assure l'initialisation (normalement par this.Model.Add)
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("DatabaseSource")); 
         }
   
         public void RemoveDatabaseSource(DatabaseSource obj)
         {
            obj.Model.Remove(obj);
            obj.Project = null;
            obj.Model = null;
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("DatabaseSource")); 
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
       /// <summary>
       /// Initialise l'instance depuis les données d'un flux binaire
       /// </summary>
       /// <param name="reader">Flux binaire</param>
       /// <param name="aggregationCallback">Permet d'appliquer des modifications aux entités importées par aggrégation</param>
       /// <remarks>Seuls les éléments existants dans le noeud Xml son importés dans l'objet</remarks>
       public void ReadBinary(BinaryReader reader, EntityCallback aggregationCallback)
       {
          // Properties
          Name =  reader.ReadString();
          Version =  reader.ReadString();
       
          // ObjectContent
          {
             int size = reader.ReadInt32();
             if (size > 0)
             {
                 //this.ObjectContent = new Collection<ObjectContent>();
                 for(int i=0;i<size;i++){
                     ObjectContent o = new ObjectContent();
                     this.Model.Add(o);
                     o.ReadBinary(reader, aggregationCallback);
                     if (aggregationCallback != null)
                        aggregationCallback(o);
                     this.AddObjectContent(o);
                 }
             }
             //else
             //{
             //    this.ObjectContent = new Collection<ObjectContent>();
             //}
          }
          // SearchParams
          {
             int size = reader.ReadInt32();
             if (size > 0)
             {
                 //this.SearchParams = new Collection<SearchParams>();
                 for(int i=0;i<size;i++){
                     SearchParams o = new SearchParams();
                     this.Model.Add(o);
                     o.ReadBinary(reader, aggregationCallback);
                     if (aggregationCallback != null)
                        aggregationCallback(o);
                     this.AddSearchParams(o);
                 }
             }
             //else
             //{
             //    this.SearchParams = new Collection<SearchParams>();
             //}
          }
          // ObjectSyntax
          {
             int size = reader.ReadInt32();
             if (size > 0)
             {
                 //this.ObjectSyntax = new Collection<ObjectSyntax>();
                 for(int i=0;i<size;i++){
                     ObjectSyntax o = new ObjectSyntax();
                     this.Model.Add(o);
                     o.ReadBinary(reader, aggregationCallback);
                     if (aggregationCallback != null)
                        aggregationCallback(o);
                     this.AddObjectSyntax(o);
                 }
             }
             //else
             //{
             //    this.ObjectSyntax = new Collection<ObjectSyntax>();
             //}
          }
          // ParamSyntax
          {
             int size = reader.ReadInt32();
             if (size > 0)
             {
                 //this.ParamSyntax = new Collection<ParamSyntax>();
                 for(int i=0;i<size;i++){
                     ParamSyntax o = new ParamSyntax();
                     this.Model.Add(o);
                     o.ReadBinary(reader, aggregationCallback);
                     if (aggregationCallback != null)
                        aggregationCallback(o);
                     this.AddParamSyntax(o);
                 }
             }
             //else
             //{
             //    this.ParamSyntax = new Collection<ParamSyntax>();
             //}
          }
          // DatabaseSource
          {
             int size = reader.ReadInt32();
             if (size > 0)
             {
                 //this.DatabaseSource = new Collection<DatabaseSource>();
                 for(int i=0;i<size;i++){
                     DatabaseSource o = new DatabaseSource();
                     this.Model.Add(o);
                     o.ReadBinary(reader, aggregationCallback);
                     if (aggregationCallback != null)
                        aggregationCallback(o);
                     this.AddDatabaseSource(o);
                 }
             }
             //else
             //{
             //    this.DatabaseSource = new Collection<DatabaseSource>();
             //}
          }
       }
       
       public void WriteBinary(BinaryWriter writer)
       {
          // Properties
          writer.Write(Name);
          writer.Write(Version);
       
          // ObjectContent
          List<ObjectContent> objectcontent = this.ObjectContent.ToList();
          writer.Write(objectcontent.Count);
          if (objectcontent.Count > 0)
          {
              foreach (var col in objectcontent)
                  col.WriteBinary(writer);
          }
          // SearchParams
          List<SearchParams> searchparams = this.SearchParams.ToList();
          writer.Write(searchparams.Count);
          if (searchparams.Count > 0)
          {
              foreach (var col in searchparams)
                  col.WriteBinary(writer);
          }
          // ObjectSyntax
          List<ObjectSyntax> objectsyntax = this.ObjectSyntax.ToList();
          writer.Write(objectsyntax.Count);
          if (objectsyntax.Count > 0)
          {
              foreach (var col in objectsyntax)
                  col.WriteBinary(writer);
          }
          // ParamSyntax
          List<ParamSyntax> paramsyntax = this.ParamSyntax.ToList();
          writer.Write(paramsyntax.Count);
          if (paramsyntax.Count > 0)
          {
              foreach (var col in paramsyntax)
                  col.WriteBinary(writer);
          }
          // DatabaseSource
          List<DatabaseSource> databasesource = this.DatabaseSource.ToList();
          writer.Write(databasesource.Count);
          if (databasesource.Count > 0)
          {
              foreach (var col in databasesource)
                  col.WriteBinary(writer);
          }
       }
       
       
       /// <summary>
       /// Convertie l'instance en élément XML
       /// </summary>
       /// <param name="parent">Élément parent reçevant le nouveau noeud</param>
       /// <returns>Text XML du document</returns>
       public string ToXml(XmlElement parent)
       {
          XmlElement curMember = null;
          XmlDocument doc = null;
          // Element parent ?
          if (parent != null)
          {
              doc = parent.OwnerDocument;
          }
          else
          {
              doc = new XmlDocument();
              parent = doc.CreateElement("root");
              doc.AppendChild(parent);
          }
       
          //Ecrit au format XML
          XmlElement cur = doc.CreateElement("Project");
          parent.AppendChild(cur);
              
          //
          // Fields
          //
          
          // Assigne le membre Name
          if (name != null)
          {
              curMember = doc.CreateElement("Name");
              curMember.AppendChild(doc.CreateTextNode(name.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre Version
          if (version != null)
          {
              curMember = doc.CreateElement("Version");
              curMember.AppendChild(doc.CreateTextNode(version.ToString()));
              cur.AppendChild(curMember);
          }
          
          //
          // Compositions
          //
       
          // ObjectContent
          {
             List<ObjectContent> objectcontent = this.ObjectContent.ToList();
             curMember = doc.CreateElement("ObjectContent");
             if (objectcontent.Count > 0)
             {
                 foreach (var col in objectcontent)
                     col.ToXml(curMember);
             }
             cur.AppendChild(curMember);
          }
       
          // SearchParams
          {
             List<SearchParams> searchparams = this.SearchParams.ToList();
             curMember = doc.CreateElement("SearchParams");
             if (searchparams.Count > 0)
             {
                 foreach (var col in searchparams)
                     col.ToXml(curMember);
             }
             cur.AppendChild(curMember);
          }
       
          // ObjectSyntax
          {
             List<ObjectSyntax> objectsyntax = this.ObjectSyntax.ToList();
             curMember = doc.CreateElement("ObjectSyntax");
             if (objectsyntax.Count > 0)
             {
                 foreach (var col in objectsyntax)
                     col.ToXml(curMember);
             }
             cur.AppendChild(curMember);
          }
       
          // ParamSyntax
          {
             List<ParamSyntax> paramsyntax = this.ParamSyntax.ToList();
             curMember = doc.CreateElement("ParamSyntax");
             if (paramsyntax.Count > 0)
             {
                 foreach (var col in paramsyntax)
                     col.ToXml(curMember);
             }
             cur.AppendChild(curMember);
          }
       
          // DatabaseSource
          {
             List<DatabaseSource> databasesource = this.DatabaseSource.ToList();
             curMember = doc.CreateElement("DatabaseSource");
             if (databasesource.Count > 0)
             {
                 foreach (var col in databasesource)
                     col.ToXml(curMember);
             }
             cur.AppendChild(curMember);
          }
       
          parent.AppendChild(cur);
          return doc.InnerXml;
       }
       
       /// <summary>
       /// Initialise l'instance depuis des données XML
       /// </summary>
       /// <param name="element">Élément contenant les information sur l'objet</param>
       /// <param name="aggregationCallback">Permet d'appliquer des modifications aux entités importées par aggrégation</param>
       /// <remarks>Seuls les éléments existants dans le noeud Xml son importés dans l'objet</remarks>
       public void FromXml(XmlElement element, EntityCallback aggregationCallback)
       {
          foreach (XmlElement m in element.ChildNodes)
          {
              string property_value = m.InnerText.Trim();
              // charge les paramètres
              switch (m.Name)
              {
                //
                // Fields
                //
       
                // Assigne le membre Name
                case "Name":
                {
                   this.name = property_value;
                }
                break;
                // Assigne le membre Version
                case "Version":
                {
                   this.version = property_value;
                }
                break;
       
                //
                // Compositions
                //
                
                // Assigne la collection ObjectContent
                case "ObjectContent":
                   {
                      foreach (XmlElement c in m.ChildNodes)
                      {
                         if("ObjectContent" == c.Name){
                             ObjectContent value = new ObjectContent();
                             value.FromXml(c, aggregationCallback);
                             if (aggregationCallback != null)
                                aggregationCallback(value);
                             this.AddObjectContent(value);
                         }
                      }
                   }
                   break;
                // Assigne la collection SearchParams
                case "SearchParams":
                   {
                      foreach (XmlElement c in m.ChildNodes)
                      {
                         if("SearchParams" == c.Name){
                             SearchParams value = new SearchParams();
                             value.FromXml(c, aggregationCallback);
                             if (aggregationCallback != null)
                                aggregationCallback(value);
                             this.AddSearchParams(value);
                         }
                      }
                   }
                   break;
                // Assigne la collection ObjectSyntax
                case "ObjectSyntax":
                   {
                      foreach (XmlElement c in m.ChildNodes)
                      {
                         if("ObjectSyntax" == c.Name){
                             ObjectSyntax value = new ObjectSyntax();
                             value.FromXml(c, aggregationCallback);
                             if (aggregationCallback != null)
                                aggregationCallback(value);
                             this.AddObjectSyntax(value);
                         }
                      }
                   }
                   break;
                // Assigne la collection ParamSyntax
                case "ParamSyntax":
                   {
                      foreach (XmlElement c in m.ChildNodes)
                      {
                         if("ParamSyntax" == c.Name){
                             ParamSyntax value = new ParamSyntax();
                             value.FromXml(c, aggregationCallback);
                             if (aggregationCallback != null)
                                aggregationCallback(value);
                             this.AddParamSyntax(value);
                         }
                      }
                   }
                   break;
                // Assigne la collection DatabaseSource
                case "DatabaseSource":
                   {
                      foreach (XmlElement c in m.ChildNodes)
                      {
                         if("DatabaseSource" == c.Name){
                             DatabaseSource value = new DatabaseSource();
                             value.FromXml(c, aggregationCallback);
                             if (aggregationCallback != null)
                                aggregationCallback(value);
                             this.AddDatabaseSource(value);
                         }
                      }
                   }
                   break;
       			}
          }
       }
       
       #endregion // Serialization
       
       #region IEntityPersistent
       public IEntityFactory Factory{get;set;}
       
       public string TableName { get{ return "T_PROJECT";} }
       
       public static string[] PrimaryIdentifier = {"Name", "Version"};
       public string[] GetPrimaryIdentifier() { return PrimaryIdentifier; }
       
       // Identifiants
       public bool CompareIdentifier(IEntityPersistent e)
       {
           Project b = e as Project;
           if(b==null)
             return false;
           return (this.Name == b.Name && this.Version == b.Version);
       }
       
       public void Load()
       {
          // Aucunes propriétés
       }
       
       public object LoadAssociations(string name)
       {
          
       
          if(name == "ObjectContent")
             return LoadObjectContent();
       
          return null;
       }
       
       public int Delete()
       {
          
          string query = "DELETE FROM T_PROJECT WHERE  [Name] = "+Factory.ParseType(this.Name)+" and  [Version] = "+Factory.ParseType(this.Version)+"";
          return Factory.Query(query);
       }
       
       public void Insert(string add_params = "", string add_values = "")
       {
          
          string query = "INSERT INTO T_PROJECT ([Name], [Version]$add_params$) VALUES( " + Factory.ParseType(this.Name) + ", " + Factory.ParseType(this.Version) + "$add_values$)";
       
       
          query = query.Replace("$add_params$", add_params);
          query = query.Replace("$add_values$", add_values);
       
          Factory.Query(query);
       
       }
       
       public int Update(string add_params = "")
       {
             // Aucunes propriétés
          return 0;
       }
       
       // Project(0,1) <-> (0,*)ObjectContent
       
       public IEnumerable<ObjectContent> LoadObjectContent()
       {
          string query = "SELECT [Object_Content_Id] FROM T_OBJECT_CONTENT WHERE [Name] = "+Factory.ParseType(this.Name)+"and [Version] = "+Factory.ParseType(this.Version)+"";
       
          Factory.Query(query, reader =>
          {
              List<ObjectContent> updatedEntities = new List<ObjectContent>();
       
              while (reader.Read())
              {
                // obtient l'identifiant
                String Id = "";
       
                if (reader["Object_Content_Id"] == null)
                   continue;
                Id = reader["Object_Content_Id"].ToString();
                
                // obtient l'objet de reference
                ObjectContent _entity = (from p in Factory.GetReferences().OfType<ObjectContent>() where p.Id == Id select p).FirstOrDefault();
       
                if ( _entity == null)
                {
                    _entity = new ObjectContent();
                    _entity.Factory = this.Factory;
                    _entity.Id = Id;
                    _entity = Factory.GetReference(_entity) as ObjectContent;//mise en cache
                }
                
                // Recharge les données depuis la BDD
                _entity.Load();
                
                // Ajoute la reference à la collection
                this.AddObjectContent(_entity);
                
                // liste les entités trouvées
                updatedEntities.Add(_entity);
       
              }
              
               // Supprime les entités qui ne sont plus en base de données
               ObjectContent[] restEntities = this.ObjectContent.Where(p => p.EntityState != Lib.EntityState.Deleted).Except(updatedEntities).ToArray();
               for (int i = 0; i < restEntities.Length; i++)
               {
                   restEntities[i].EntityState = Lib.EntityState.Deleted; // indique la suppression en base
                   this.Model.Remove(restEntities[i]); // retire du model
               }
               
              return 0;
          });
       
          return ObjectContent;
       }
       
       // Réinitialise l'identifiant primaire
       public void RaiseIdentity()
       {
          Name = String.Empty;
          Version = String.Empty;
       }
       
       // Obtient l'identifiant primaire depuis un curseur SQL
       public void PickIdentity(object _reader)
       {
          DbDataReader reader = _reader as DbDataReader;
          if (reader["Name"] != null)
             Name = reader["Name"].ToString();
       
          if (reader["Version"] != null)
             Version = reader["Version"].ToString();
       }
       
       // Obtient les propriétés depuis un curseur SQL
       public void PickProperties(object _reader)
       {
          DbDataReader reader = _reader as DbDataReader;
       }
       #endregion // IEntityPersistent
       #region Validation
       #region IDataErrorInfo
       // Validation globale de l'entité
       public string Error
       {
          get
          {
              string all_mess = "";
              string msg;
              all_mess += ((msg = this["Name"]) != String.Empty) ? (GetPropertyDesc("Name") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["Version"]) != String.Empty) ? (GetPropertyDesc("Version") + " :\n\t" + msg + "\n") : String.Empty;
              return all_mess;
          }
       }
       
       // Validation par propriété
       public string this[string propertyName]
       {
          get
          {
              string code;
              CheckField(propertyName, out code);
              
              if (String.IsNullOrEmpty(code) == false)
                  return GetPropertyDesc(propertyName) + ":\n" + code;
                  
              return String.Empty;
          }
       }
       
       public static string GetClassDesc()
       {
          return "Information sur le projet";
       }
       
       public static string GetPropertyDesc(string propertyName)
       {
          switch (propertyName)
          {
       
              case "Name":
                  return "Nom";
       
              case "Version":
                  return "Version";
          }
          return "";
       }
       #endregion
       
       #region IEntityValidable
       // Test la validité de tous les champs
       public void Validate(){
          string errorCode;
       
          if(CheckField("Name", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("Version", out errorCode) == false)
             throw new ApplicationException(errorCode);
       }
       
       // Test la validité d'un champ
       public bool CheckField(string propertyName, out string errorCode){
           errorCode = String.Empty;
           
           switch (propertyName)
           {
               case "Name":
                 // Obligatoire
                 if(this.Name == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 break;
       
               case "Version":
                 // Obligatoire
                 if(this.Version == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 break;
       
           }
           
           return true;
       }
       #endregion
       #endregion // Validation
      }

}