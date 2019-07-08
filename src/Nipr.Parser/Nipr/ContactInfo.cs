using System.Xml.Serialization;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("CONTACT_INFOS")]
    public class ContactInfos
    {
        [XmlAttribute("name")]
        public string State { get; set; } = string.Empty;

        [XmlElement("CONTACT_INFO", typeof(ContactInfo))]
        public ContactInfo ContactInfo { get; set; } = new ContactInfo();
    }

    [XmlRoot("CONTACT_INFO")]
    public class ContactInfo
    {
        [XmlElement("BUSINESS_PHONE")]
        public string Phone { get; set; } = string.Empty;

        [XmlElement("BUSINESS_PHONE_UPDATE_DATE")]
        public string PhoneUpdatedDate { get; set; } = string.Empty;

        [XmlElement("BUSINESS_EMAIL")]
        public string Email { get; set; } = string.Empty;

        [XmlElement("BUSINESS_EMAIL_UPDATE_DATE")]
        public string EmailUpdatedDate { get; set; } = string.Empty;

        [XmlElement("FAX")]
        public string Fax { get; set; } = string.Empty;

        [XmlElement("FAX_UPDATE_DATE")]
        public string FaxUpdatedDate { get; set; } = string.Empty;
    }
}