/*
   Extension de la classe d'entité ObjectContent

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

    public partial class ObjectContent : IEntity, ISerializable, IEntitySerializable, INotifyPropertyChanged, IEntityPersistent, IDataErrorInfo, IEntityValidable    {
         #region Constructor
         public ObjectContent(){

            // ParamContent
            //this.paramcontent = new Collection<ParamContent>();
            // Id
            this.id = String.Empty;
            // ObjectType
            this.objecttype = String.Empty;
            // Filename
            this.filename = String.Empty;
            // Position
            this.position = new Int32();
         }
         
         // copie
         public ObjectContent(ObjectContent src) : this(){
            Copy(this, src);
         }

         public ObjectContent(String id, String objecttype, String filename, int position) : this(){
            this.id = id;
            this.objecttype = objecttype;
            this.filename = filename;
            this.position = position;
         }
         #endregion // Constructor
         
          public string EntityName { get{ return "ObjectContent"; } }

         // clone
         public IEntity Clone(){
            return Copy(new ObjectContent(), this);
         }

         // copie
         public IEntity Copy(IEntity _dst,IEntity _src){
            ObjectContent src = _src as ObjectContent;
            ObjectContent dst = _dst as ObjectContent;
            if(src==null || dst==null)
               return null;
               
            // Id
            dst.id = src.id;
            // ObjectType
            dst.objecttype = src.objecttype;
            // Filename
            dst.filename = src.filename;
            // Position
            dst.position = src.position;
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
         protected int position;
         public int Position { get{ return position; } set{ position = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Position")); } }
         #endregion // Fields

         #region Associations

         //
         // Project
         // 

         protected Project project;
         public virtual Project Project { get{ return project; } set{ project = value; if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Project"));  } }

         //
         // ParamContent
         // 

         // Simple association
         public virtual IEnumerable<ParamContent> ParamContent
         {
             get { return this.Model.Objs.OfType<ParamContent>().Where(p=>p.ObjectContent == this); }
         }
         public void AddParamContent(ParamContent obj)
         {
            this.Model.Add(obj);
            obj.ObjectContent = this;
            obj.Factory = this.Factory;
            obj.Model = this.Model;//assure l'initialisation (normalement par this.Model.Add)
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("ParamContent")); 
         }
   
         public void RemoveParamContent(ParamContent obj)
         {
            obj.Model.Remove(obj);
            obj.ObjectContent = null;
            obj.Factory = this.Factory;
            obj.Model = null;
            if (this.PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs("ParamContent")); 
         }
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
       /// <summary>
       /// Initialise l'instance depuis les données d'un flux binaire
       /// </summary>
       /// <param name="reader">Flux binaire</param>
       /// <param name="aggregationCallback">Permet d'appliquer des modifications aux entités importées par aggrégation</param>
       /// <remarks>Seuls les éléments existants dans le noeud Xml son importés dans l'objet</remarks>
       public void ReadBinary(BinaryReader reader, EntityCallback aggregationCallback)
       {
          // Properties
          Id =  reader.ReadString();
          ObjectType =  reader.ReadString();
          Filename =  reader.ReadString();
          Position =  reader.ReadInt32();
       
          // ParamContent
          {
             int size = reader.ReadInt32();
             if (size > 0)
             {
                 //this.ParamContent = new Collection<ParamContent>();
                 for(int i=0;i<size;i++){
                     ParamContent o = new ParamContent();
                     this.Model.Add(o);
                     o.ReadBinary(reader, aggregationCallback);
                     if (aggregationCallback != null)
                        aggregationCallback(o);
                     this.AddParamContent(o);
                 }
             }
             //else
             //{
             //    this.ParamContent = new Collection<ParamContent>();
             //}
          }
       }
       
       public void WriteBinary(BinaryWriter writer)
       {
          // Properties
          writer.Write(Id);
          writer.Write(ObjectType);
          writer.Write(Filename);
          writer.Write(Position);
       
          // ParamContent
          List<ParamContent> paramcontent = this.ParamContent.ToList();
          writer.Write(paramcontent.Count);
          if (paramcontent.Count > 0)
          {
              foreach (var col in paramcontent)
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
          XmlElement cur = doc.CreateElement("ObjectContent");
          parent.AppendChild(cur);
              
          //
          // Fields
          //
          
          // Assigne le membre Id
          if (id != null)
          {
              curMember = doc.CreateElement("Id");
              curMember.AppendChild(doc.CreateTextNode(id.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre ObjectType
          if (objecttype != null)
          {
              curMember = doc.CreateElement("ObjectType");
              curMember.AppendChild(doc.CreateTextNode(objecttype.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre Filename
          if (filename != null)
          {
              curMember = doc.CreateElement("Filename");
              curMember.AppendChild(doc.CreateTextNode(filename.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre Position
          curMember = doc.CreateElement("Position");
          curMember.AppendChild(doc.CreateTextNode(position.ToString()));
          cur.AppendChild(curMember);
          
          //
          // Compositions
          //
       
          // ParamContent
          {
             List<ParamContent> paramcontent = this.ParamContent.ToList();
             curMember = doc.CreateElement("ParamContent");
             if (paramcontent.Count > 0)
             {
                 foreach (var col in paramcontent)
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
       
                // Assigne le membre Id
                case "Id":
                {
                   this.id = property_value;
                }
                break;
                // Assigne le membre ObjectType
                case "ObjectType":
                {
                   this.objecttype = property_value;
                }
                break;
                // Assigne le membre Filename
                case "Filename":
                {
                   this.filename = property_value;
                }
                break;
                // Assigne le membre Position
                case "Position":
                {
                   int value;
                   if(int.TryParse(property_value,out value)==false)
                      this.Position = new Int32();
                   else
                      this.Position = value;
                }
                break;
       
                //
                // Compositions
                //
                
                // Assigne la collection ParamContent
                case "ParamContent":
                   {
                      foreach (XmlElement c in m.ChildNodes)
                      {
                         if("ParamContent" == c.Name){
                             ParamContent value = new ParamContent();
                             value.FromXml(c, aggregationCallback);
                             if (aggregationCallback != null)
                                aggregationCallback(value);
                             this.AddParamContent(value);
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
       
       public string TableName { get{ return "T_OBJECT_CONTENT";} }
       
       public static string[] PrimaryIdentifier = {"Id"};
       public string[] GetPrimaryIdentifier() { return PrimaryIdentifier; }
       
       // Identifiants
       public bool CompareIdentifier(IEntityPersistent e)
       {
           ObjectContent b = e as ObjectContent;
           if(b==null)
             return false;
           return (this.Id == b.Id);
       }
       
       public void Load()
       {
          string query = "SELECT [ObjectType] , [Filename] , [FilePosition] FROM T_OBJECT_CONTENT WHERE [Object_Content_Id] = "+Factory.ParseType(this.Id)+"";
          Factory.QueryObject(query, this);
       }
       
       public object LoadAssociations(string name)
       {
          
       
          if(name == "ParamContent")
             return LoadParamContent();
       
          return null;
       }
       
       public int Delete()
       {
          
          string query = "DELETE FROM T_OBJECT_CONTENT WHERE  [Object_Content_Id] = "+Factory.ParseType(this.Id)+"";
          return Factory.Query(query);
       }
       
       public void Insert(string add_params = "", string add_values = "")
       {
          
          string query = "INSERT INTO T_OBJECT_CONTENT ([Object_Content_Id], [ObjectType], [Filename], [FilePosition]$add_params$) VALUES( " + Factory.ParseType(this.Id) + ", " + Factory.ParseType(this.ObjectType) + ", " + Factory.ParseType(this.Filename) + ", " + Factory.ParseType(this.Position) + "$add_values$)";
       
       
          query = query.Replace("$add_params$", add_params);
          query = query.Replace("$add_values$", add_values);
       
          Factory.Query(query);
       
       }
       
       public int Update(string add_params = "")
       {
             string query = "UPDATE T_OBJECT_CONTENT SET [ObjectType] = "+Factory.ParseType(this.ObjectType)+", [Filename] = "+Factory.ParseType(this.Filename)+", [FilePosition] = "+Factory.ParseType(this.Position)+"$add_params$ WHERE [Object_Content_Id] = "+Factory.ParseType(this.Id)+"";
       
       
          query = query.Replace("$add_params$", add_params);
          
          return Factory.Query(query);
       }
       
       // ObjectContent(0,1) <-> (0,*)ParamContent
       
       public IEnumerable<ParamContent> LoadParamContent()
       {
          string query = "SELECT [Param_Content_Id] FROM T_PARAM_CONTENT WHERE [Object_Content_Id] = "+Factory.ParseType(this.Id)+"";
       
          Factory.Query(query, reader =>
          {
              List<ParamContent> updatedEntities = new List<ParamContent>();
       
              while (reader.Read())
              {
                // obtient l'identifiant
                String Id = "";
       
                if (reader["Param_Content_Id"] == null)
                   continue;
                Id = reader["Param_Content_Id"].ToString();
                
                // obtient l'objet de reference
                ParamContent _entity = (from p in Factory.GetReferences().OfType<ParamContent>() where p.Id == Id select p).FirstOrDefault();
       
                if ( _entity == null)
                {
                    _entity = new ParamContent();
                    _entity.Factory = this.Factory;
                    _entity.Id = Id;
                    _entity = Factory.GetReference(_entity) as ParamContent;//mise en cache
                }
                
                // Recharge les données depuis la BDD
                _entity.Load();
                
                // Ajoute la reference à la collection
                this.AddParamContent(_entity);
                
                // liste les entités trouvées
                updatedEntities.Add(_entity);
       
              }
              
               // Supprime les entités qui ne sont plus en base de données
               ParamContent[] restEntities = this.ParamContent.Where(p => p.EntityState != Lib.EntityState.Deleted).Except(updatedEntities).ToArray();
               for (int i = 0; i < restEntities.Length; i++)
               {
                   restEntities[i].EntityState = Lib.EntityState.Deleted; // indique la suppression en base
                   this.Model.Remove(restEntities[i]); // retire du model
               }
               
              return 0;
          });
       
          return ParamContent;
       }
       
       // Réinitialise l'identifiant primaire
       public void RaiseIdentity()
       {
          Id = String.Empty;
       }
       
       // Obtient l'identifiant primaire depuis un curseur SQL
       public void PickIdentity(object _reader)
       {
          DbDataReader reader = _reader as DbDataReader;
          if (reader["Object_Content_Id"] != null)
             Id = reader["Object_Content_Id"].ToString();
       }
       
       // Obtient les propriétés depuis un curseur SQL
       public void PickProperties(object _reader)
       {
          DbDataReader reader = _reader as DbDataReader;
          if (reader["ObjectType"] != null)
             ObjectType = reader["ObjectType"].ToString();
       
          if (reader["Filename"] != null)
             Filename = reader["Filename"].ToString();
       
          if (reader["FilePosition"] != null)
             Position = int.Parse(reader["FilePosition"].ToString());
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
              all_mess += ((msg = this["Id"]) != String.Empty) ? (GetPropertyDesc("Id") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["ObjectType"]) != String.Empty) ? (GetPropertyDesc("ObjectType") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["Filename"]) != String.Empty) ? (GetPropertyDesc("Filename") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["Position"]) != String.Empty) ? (GetPropertyDesc("Position") + " :\n\t" + msg + "\n") : String.Empty;
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
          return "";
       }
       
       public static string GetPropertyDesc(string propertyName)
       {
          switch (propertyName)
          {
       
              case "Id":
                  return "Identifiant";
       
              case "ObjectType":
                  return "Type d'objet";
       
              case "Filename":
                  return "Emplacement du fichier source";
       
              case "Position":
                  return "Position de départ dans le fichier source";
          }
          return "";
       }
       #endregion
       
       #region IEntityValidable
       // Test la validité de tous les champs
       public void Validate(){
          string errorCode;
       
          if(CheckField("Id", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("ObjectType", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("Filename", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("Position", out errorCode) == false)
             throw new ApplicationException(errorCode);
       }
       
       // Test la validité d'un champ
       public bool CheckField(string propertyName, out string errorCode){
           errorCode = String.Empty;
           
           switch (propertyName)
           {
               case "Id":
                 // Obligatoire
                 if(this.Id == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 return AppModel.Format.Guid.Validate(this.Id.ToString(),ref errorCode);
       
               case "ObjectType":
                 // Obligatoire
                 if(this.ObjectType == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 return AppModel.Format.Name.Validate(this.ObjectType.ToString(),ref errorCode);
       
               case "Filename":
                 // Obligatoire
                 if(this.Filename == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 break;
       
               case "Position":
                 break;
       
           }
           
           return true;
       }
       #endregion
       #endregion // Validation
      }

}