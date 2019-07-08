using System.Xml.Serialization;

namespace Nipr.Parser.Common
{
    [XmlRoot("ERROR")]
    public class Error
    {
        [XmlElement("DESCRIPTION")]
        [XmlText]
        public string Description { get; set; } = string.Empty;
    }
}