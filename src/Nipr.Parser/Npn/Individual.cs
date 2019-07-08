using System.Xml.Serialization;

namespace Nipr.Parser.Npn
{
    [XmlRoot("INDIVIDUAL")]
    public class Individual
    {
        [XmlElement("NAME")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("ID_ENTITY")]
        public string Ssn { get; set; } = string.Empty;

        [XmlElement("NPN")]
        public string Npn { get; set; } = string.Empty;

        [XmlElement("STATE_RESIDENT")]
        public string StateResident { get; set; } = string.Empty;

        [XmlElement("DATE_BIRTH")]
        public string DateOfBirth { get; set; } = string.Empty;
    }
}