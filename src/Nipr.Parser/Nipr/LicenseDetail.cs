using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("DETAIL")]
    public class LicenseDetail
    {
        [XmlElement("LOA")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("LOA_CODE")]
        public string Code { get; set; } = string.Empty;

        [XmlElement("AUTHORITY_ISSUE_DATE")]
        public string AuthorityIssueDate { get; set; } = string.Empty;

        [XmlElement("STATUS")]
        public string Status { get; set; } = string.Empty;

        [XmlElement("STATUS_REASON")]
        public string StatusReason { get; set; } = string.Empty;

        [XmlElement("STATUS_REASON_DATE")]
        public string StatusReasonDate { get; set; } = string.Empty;

        [XmlElement("CE_COMPLIANCE")]
        public string CeCompliance { get; set; } = string.Empty;

        [XmlElement("CE_RENEWAL_DATE")]
        public string CeRenewalDate { get; set; } = string.Empty;

        [XmlElement("CE_CREDITS_NEEDED")]
        public string CeCreditsNeeded { get; set; } = string.Empty;
    }
}