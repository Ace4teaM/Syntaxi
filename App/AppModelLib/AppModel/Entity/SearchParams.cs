/*
   Extension de la classe d'entité SearchParams

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
using Serial = System.Int32;

namespace AppModel.Entity
{
    /// <summary>
    /// Implémente la définition de l'entité
    /// </summary>
   [Serializable]

    public partial class SearchParams : IEntity, ISerializable, IEntitySerializable, INotifyPropertyChanged, IDataErrorInfo, IEntityValidable    {
         #region Constructor
         public SearchParams(){
            // InputDir
            this.inputdir = String.Empty;
            // InputFilter
            this.inputfilter = String.Empty;
            // Recursive
            this.recursive = new Boolean();
            // GroupName
            this.groupname = String.Empty;
         }
         
         // copie
         public SearchParams(SearchParams src) : this(){
            Copy(this, src);
         }

         public SearchParams(String inputdir, String inputfilter, bool recursive, string groupname) : this(){
            this.inputdir = inputdir;
            this.inputfilter = inputfilter;
            this.recursive = recursive;
            this.groupname = groupname;
         }
         #endregion // Constructor
         
          public string EntityName { get{ return "SearchParams"; } }

         // clone
         public IEntity Clone(){
            return Copy(new SearchParams(), this);
         }

         // copie
         public IEntity Copy(IEntity _dst,IEntity _src){
            SearchParams src = _src as SearchParams;
            SearchParams dst = _dst as SearchParams;
            if(src==null || dst==null)
               return null;
               
            // InputDir
            dst.inputdir = src.inputdir;
            // InputFilter
            dst.inputfilter = src.inputfilter;
            // Recursive
            dst.recursive = src.recursive;
            // GroupName
            dst.groupname = src.groupname;
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
         // Dossier de recherche
         protected String inputdir;
         public String InputDir { get{ return inputdir; } set{ inputdir = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("InputDir")); } }
         // Filtre de recherche sur les nom de fichiers
         protected String inputfilter;
         public String InputFilter { get{ return inputfilter; } set{ inputfilter = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("InputFilter")); } }
         // Récursif
         protected bool recursive;
         public bool Recursive { get{ return recursive; } set{ recursive = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Recursive")); } }
         // Groupe de syntaxe
         protected string groupname;
         public string GroupName { get{ return groupname; } set{ groupname = value;  if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("GroupName")); } }
         #endregion // Fields

         #region Associations

         //
         // Project
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
             result += "GroupName = " + GroupName + Environment.NewLine;
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
            info.AddValue("GroupName", GroupName, typeof(string));
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
          InputDir =  reader.ReadString();
          InputFilter =  reader.ReadString();
          Recursive =  reader.ReadBoolean();
          GroupName =  reader.ReadString();
       }
       
       public void WriteBinary(BinaryWriter writer)
       {
          // Properties
          writer.Write(InputDir);
          writer.Write(InputFilter);
          writer.Write(Recursive);
          writer.Write(GroupName);
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
          XmlElement cur = doc.CreateElement("SearchParams");
          parent.AppendChild(cur);
              
          //
          // Fields
          //
          
          // Assigne le membre InputDir
          if (inputdir != null)
          {
              curMember = doc.CreateElement("InputDir");
              curMember.AppendChild(doc.CreateTextNode(inputdir.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre InputFilter
          if (inputfilter != null)
          {
              curMember = doc.CreateElement("InputFilter");
              curMember.AppendChild(doc.CreateTextNode(inputfilter.ToString()));
              cur.AppendChild(curMember);
          }
       
          // Assigne le membre Recursive
          curMember = doc.CreateElement("Recursive");
          curMember.AppendChild(doc.CreateTextNode(recursive.ToString()));
          cur.AppendChild(curMember);
       
          // Assigne le membre GroupName
          if (groupname != null)
          {
              curMember = doc.CreateElement("GroupName");
              curMember.AppendChild(doc.CreateTextNode(groupname.ToString()));
              cur.AppendChild(curMember);
          }
          
          //
          // Compositions
          //
       
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
       
                // Assigne le membre InputDir
                case "InputDir":
                {
                   this.inputdir = property_value;
                }
                break;
                // Assigne le membre InputFilter
                case "InputFilter":
                {
                   this.inputfilter = property_value;
                }
                break;
                // Assigne le membre Recursive
                case "Recursive":
                {
                   bool value;
                   if(bool.TryParse(property_value,out value)==false)
                      this.Recursive = new Boolean();
                   else
                      this.Recursive = value;
                }
                break;
                // Assigne le membre GroupName
                case "GroupName":
                {
                   this.groupname = property_value;
                }
                break;
       
                //
                // Compositions
                //
                
       			}
          }
       }
       
       #endregion // Serialization

       #region Validation
       #region IDataErrorInfo
       // Validation globale de l'entité
       public string Error
       {
          get
          {
              string all_mess = "";
              string msg;
              all_mess += ((msg = this["InputDir"]) != String.Empty) ? (GetPropertyDesc("InputDir") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["InputFilter"]) != String.Empty) ? (GetPropertyDesc("InputFilter") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["Recursive"]) != String.Empty) ? (GetPropertyDesc("Recursive") + " :\n\t" + msg + "\n") : String.Empty;
              all_mess += ((msg = this["GroupName"]) != String.Empty) ? (GetPropertyDesc("GroupName") + " :\n\t" + msg + "\n") : String.Empty;
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
          return "Paramètres d'une recherche";
       }
       
       public static string GetPropertyDesc(string propertyName)
       {
          switch (propertyName)
          {
       
              case "InputDir":
                  return "Dossier de recherche";
       
              case "InputFilter":
                  return "Filtre de recherche sur les nom de fichiers";
       
              case "Recursive":
                  return "Récursif";
       
              case "GroupName":
                  return "Groupe de syntaxe";
          }
          return "";
       }
       #endregion
       
       #region IEntityValidable
       // Test la validité de tous les champs
       public void Validate(){
          string errorCode;
       
          if(CheckField("InputDir", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("InputFilter", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("Recursive", out errorCode) == false)
             throw new ApplicationException(errorCode);
          if(CheckField("GroupName", out errorCode) == false)
             throw new ApplicationException(errorCode);
       }
       
       // Test la validité d'un champ
       public bool CheckField(string propertyName, out string errorCode){
           errorCode = String.Empty;
           
           switch (propertyName)
           {
               case "InputDir":
                 // Obligatoire
                 if(this.InputDir == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 break;
       
               case "InputFilter":
                 // Obligatoire
                 if(this.InputFilter == null){
                   errorCode = "NOT_NULL_RESTRICTION";
                   return false;
                 }
                 break;
       
               case "Recursive":
                 break;
       
               case "GroupName":
                 if(this.GroupName == null)
                   break;
                 break;
       
           }
           
           return true;
       }
       #endregion
       #endregion // Validation
      }

}