using System.Xml.Serialization;

namespace Nipr.Parser.Common
{
    [XmlRoot("TRANSACTION_TYPE")]
    public class TransactionType
    {
        [XmlElement("TYPE")]
        [XmlText]
        public string Type { get; set; } = string.Empty;
    }
}