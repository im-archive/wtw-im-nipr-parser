using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("ADHS")]
    public class Adhs
    {
        [XmlElement("HOME_STATE")]
        public string Details { get; set; } = string.Empty;
    }
}