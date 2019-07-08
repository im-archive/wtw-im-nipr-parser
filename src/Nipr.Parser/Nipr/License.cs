using System.Xml.Serialization;
using System.Collections.Generic;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("LICENSE")]
    public class License
    {
        [XmlElement("LICENSE_NUM")]
        public string LicenseNumber { get; set; } = string.Empty;

        [XmlElement("DATE_UPDATED")]
        public string DateUpdated { get; set; } = string.Empty;

        [XmlElement("DATE_ISSUE_LICENSE_ORIG")]
        public string IssuedDate { get; set; } = string.Empty;

        [XmlElement("DATE_EXPIRE_LICENSE")]
        public string ExpirationDate { get; set; } = string.Empty;

        [XmlElement("LICENSE_CLASS")]
        public string LicenseClass { get; set; } = string.Empty;

        [XmlElement("LICENSE_CLASS_CODE")]
        public string LicenseClassCode { get; set; } = string.Empty;

        [XmlElement("RESIDENCY_STATUS")]
        public string ResidencyStatus { get; set; } = string.Empty;

        [XmlElement("ACTIVE")]
        public string Active { get; set; } = string.Empty;

        [XmlElement("ADHS")]
        public Adhs Adhs { get; set; } = new Adhs();

        [XmlArray("DETAILS")]
        [XmlArrayItem("DETAIL", typeof(LicenseDetail))]
        public List<LicenseDetail> LinesOfAuthority { get; set; } = new List<LicenseDetail>();
    }
}