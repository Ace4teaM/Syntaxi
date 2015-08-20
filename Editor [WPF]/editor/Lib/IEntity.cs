﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Lib
{
    // Etat de modification
    public enum EntityState
    {
        Unmodified, // Valeur par défaut
        Modified,
        Added,
        Deleted
    }

    public interface IEntitySerializable
    {
        void ReadBinary(BinaryReader reader);
        void WriteBinary(BinaryWriter writer);
        void FromXml(XmlElement element);
        string ToXml(XmlElement parent);
    }
    
    public interface IEntity
    {
        IEntityFactory Factory { get; set; }
        EntityState EntityState { get; set; }
        string TableName { get; }
        
        void Insert(string add_params = "", string add_values = "");
        int Update(string add_params = "");
        int Delete();
        void Load();

        string[] GetPrimaryIdentifier();
        bool CompareIdentifier(IEntity e);
        void PickIdentity(object reader);
        void PickProperties(object reader);
        
        object LoadAssociations(string name);
    }
}