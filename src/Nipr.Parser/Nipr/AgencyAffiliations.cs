using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("Agency_Affiliations")]
    public class AgencyAffiliations
    {
        [XmlElement("Agency_Name")]
        public string AgencyName { get; set; }

        [XmlElement("FEIN_State_Co_ID")]
        public string FEINStateCoId { get; set; }
    }
}