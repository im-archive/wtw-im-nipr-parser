using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("NASD_EXAM")]
    public class NasdExam
    {
        [XmlElement("DETAILS")]
        public string Details { get; set; } = string.Empty;
    }
}