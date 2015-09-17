using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Lib
{
    public interface IEntitySerializable : IEntity
    {
        void ReadBinary(BinaryReader reader, EntityCallback aggregationCallback);
        void WriteBinary(BinaryWriter writer);
        void FromXml(XmlElement element, EntityCallback aggregationCallback);
        string ToXml(XmlElement parent);
    }
}