using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("BIOGRAPHIC")]
    public class Biographic
    {
        [XmlElement("ID_SSN")]
        public string Ssn { get; set; } = string.Empty;

        [XmlElement("NAME_LAST")]
        public string LastName { get; set; } = string.Empty;

        [XmlElement("NAME_FIRST")]
        public string FirstName { get; set; } = string.Empty;

        [XmlElement("NAME_MIDDLE")]
        public string MiddleName { get; set; } = string.Empty;

        [XmlElement("NAME_SUFFIX")]
        public string Suffix { get; set; } = string.Empty;

        [XmlElement("DATE_BIRTH")]
        public string DateOfBirth { get; set; } = string.Empty;

        [XmlElement("NPN")]
        public string Npn { get; set; } = string.Empty;

        [XmlArray("OTHER_NAME")]
        [XmlArrayItem("STATE", typeof(StateOtherName))]
        public List<StateOtherName> OtherNames { get; set; } = new List<StateOtherName>();
    }
}