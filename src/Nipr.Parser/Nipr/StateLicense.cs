using System.Xml.Serialization;
using System.Collections.Generic;

namespace Nipr.Parser.Nipr
{
[XmlRoot("STATE")]
    public class StateLicense
    {
        [XmlAttribute("name")]
        public string State { get; set; } = string.Empty;

        [XmlElement("LICENSE", typeof(License))]
        public List<License> Licenses { get; set; } = new List<License>();

        [XmlArray("APPOINTMENT_INFORMATION")]
        [XmlArrayItem("APPOINTMENT", typeof(Appointment))]
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}