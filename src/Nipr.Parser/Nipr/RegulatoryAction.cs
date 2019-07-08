using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("STATE")]
    public class RegulatoryAction
    {
        [XmlAttribute("name")]
        public string State { get; set; } = string.Empty;

        [XmlElement("ACTION_ID")]
        public string ActionId { get; set; } = string.Empty;

        [XmlElement("ORIGIN_OF_ACTION")]
        public string OriginOfAction { get; set; } = string.Empty;

        [XmlElement("REASON_FOR_ACTION")]
        public string ReasonForAction { get; set; } = string.Empty;

        [XmlElement("DISPOSITION")]
        public string Disposition { get; set; } = string.Empty;

        [XmlElement("DATE_OF_ACTION")]
        public string DateOfAction { get; set; } = string.Empty;

        [XmlElement("EFFECTIVE_DATE")]
        public string EffectiveDate { get; set; } = string.Empty;

        [XmlElement("ENTER_DATE")]
        public string EnterDate { get; set; } = string.Empty;

        [XmlElement("FILE_REF")]
        public string FileRef { get; set; } = string.Empty;

        [XmlElement("PENALTY_FINE_FORFEITURE")]
        public string PenaltyFineForfeiture { get; set; } = string.Empty;

        [XmlElement("LENGTH_OF_ORDER")]
        public string LengthOfOrder { get; set; } = string.Empty;
    }
}