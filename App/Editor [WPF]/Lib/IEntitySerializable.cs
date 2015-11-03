using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Lib
{
    public interface IEntitySerializable : IEntity
    {
        /// <summary>
        /// Lit les propriétés de l'entité depuis un flux de données binaire
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="aggregationCallback"></param>
        void ReadBinary(BinaryReader reader, EntityCallback aggregationCallback);
        /// <summary>
        /// Ecrit les propriétés de l'entité depuis un flux de données binaire
        /// </summary>
        /// <param name="writer"></param>
        void WriteBinary(BinaryWriter writer);
        /// <summary>
        /// Initialise les propriétés de l'entité depuis un flux de données XML
        /// </summary>
        /// <param name="element"></param>
        /// <param name="aggregationCallback"></param>
        void FromXml(XmlElement element, EntityCallback aggregationCallback);
        /// <summary>
        /// Initialise les propriétés de l'entité dans un élément XML
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        string ToXml(XmlElement parent);
    }
}