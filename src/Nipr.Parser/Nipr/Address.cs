using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("STATE")]
    public class StateAddress
    {
        [XmlAttribute("name")]
        public string State { get; set; } = string.Empty;

        [XmlElement("ADDRESS")]
        public List<Address> Addresses { get; set; } = new List<Address>();
    }

    [XmlRoot("ADDRESS")]
    public class Address
    {
        [XmlElement("DATE_UPDATED")]
        public string DateUpdated { get; set; } = string.Empty;

        [XmlElement("ADDRESS_TYPE_CODE")]
        public string TypeCode { get; set; } = string.Empty;

        [XmlElement("ADDR_TYPE")]
        public string Type { get; set; } = string.Empty;

        [XmlElement("ADDR_LINE_1")]
        public string Line1 { get; set; } = string.Empty;

        [XmlElement("ADDR_LINE_2")]
        public string Line2 { get; set; } = string.Empty;

        [XmlElement("ADDR_LINE_3")]
        public string Line3 { get; set; } = string.Empty;

        [XmlElement("NAME_CITY")]
        public string City { get; set; } = string.Empty;

        [XmlElement("NAME_STATE")]
        public string State { get; set; } = string.Empty;

        [XmlElement("ZIP")]
        public string ZipCode { get; set; } = string.Empty;

        [XmlElement("COUNTRY")]
        public string Country { get; set; } = string.Empty;
    }
}