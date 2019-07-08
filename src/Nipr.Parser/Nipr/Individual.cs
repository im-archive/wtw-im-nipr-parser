using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("INDIVIDUAL")]
    public class Individual
    {
        [XmlElement("ENTITY_BIOGRAPHIC")]
        public EntityBiographic EntityBiographic { get; set; } = new EntityBiographic();

        [XmlElement("PRODUCER_LICENSING")]
        public ProducerLicensing ProducerLicensing { get; set; } = new ProducerLicensing();
    }
}