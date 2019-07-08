using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("APPOINTMENT")]
    public class Appointment
    {
        [XmlElement("COMPANY_NAME")]
        public string CompanyName { get; set; } = string.Empty;

        [XmlElement("FEIN")]
        public string FEIN { get; set; } = string.Empty;

        [XmlElement("COCODE")]
        public string CoCode { get; set; } = string.Empty;

        [XmlElement("LINE_OF_AUTHORITY")]
        public string LineOfAuthority { get; set; } = string.Empty;

        [XmlElement("LOA_CODE")]
        public string LoaCode { get; set; } = string.Empty;

        [XmlElement("STATUS")]
        public string Status { get; set; } = string.Empty;

        [XmlElement("TERMINATION_REASON")]
        public string TerminationReason { get; set; } = string.Empty;

        [XmlElement("STATUS_REASON_DATE")]
        public string StatusReasonDate { get; set; } = string.Empty;

        [XmlElement("APPONT_RENEWAL_DATE")]
        public string AppontRenewalDate { get; set; } = string.Empty;

        [XmlElement("COUNTY_CODE")]
        public string CountyCode { get; set; } = string.Empty;

        [XmlElement("Agency_Affiliations")]
        public AgencyAffiliations AgencyAffiliations { get; set; }
    }
}