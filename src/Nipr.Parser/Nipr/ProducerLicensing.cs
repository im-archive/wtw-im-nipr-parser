using System.Xml.Serialization;
using System.Collections.Generic;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("PRODUCER_LICENSING")]
    public class ProducerLicensing
    {
        [XmlArray("LICENSE_INFORMATION")]
        [XmlArrayItem("STATE", typeof(StateLicense))]
        public List<StateLicense> StateLicenses { get; set; } = new List<StateLicense>();

        [XmlElement("CLEARANCE_CERTIFICATION_INFO")]
        public string ClearanceCertificationInfo { get; set; } = string.Empty;

        [XmlArray("REGULATORY_ACTION")]
        [XmlArrayItem("STATE", typeof(RegulatoryAction))]
        public List<RegulatoryAction> RegulatoryActions { get; set; } = new List<RegulatoryAction>();

        [XmlElement("NASD_EXAM", typeof(NasdExam))]
        public NasdExam NasdExam { get; set; } = new NasdExam();
    }
}