using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("ENTITY_BIOGRAPHIC")]
    public class EntityBiographic
    {
        [XmlElement("BIOGRAPHIC")]
        public Biographic Biographic { get; set; } = new Biographic();

        [XmlArray("ADDRESSES")]
        [XmlArrayItem("STATE", typeof(StateAddress))]
        public List<StateAddress> StateAddresses { get; set; } = new List<StateAddress>();

        [XmlArray("CONTACT_INFOS")]
        [XmlArrayItem("STATE", typeof(ContactInfos))]
        public List<ContactInfos> ContactInfos { get; set; } = new List<ContactInfos>();
    }
}