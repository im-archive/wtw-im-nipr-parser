using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("PRODUCER")]
    public class Producer
    {
        [XmlElement("INDIVIDUAL")]
        public Individual Individual { get; set; } = new Individual();
    }
}