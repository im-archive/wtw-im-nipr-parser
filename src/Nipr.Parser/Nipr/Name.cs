using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("STATE")]
    public class StateOtherName
    {
        [XmlAttribute("name")]
        public string State { get; set; } = string.Empty;

        [XmlElement("NAME")]
        public Name Name { get; set; } = new Name();
    }

    [XmlRoot("NAME")]
    public class Name
    {
        [XmlAttribute("type")]
        public string Type { get; set; } = "Alias";

        [XmlText]
        public string Value { get; set; } = string.Empty;
    }
}