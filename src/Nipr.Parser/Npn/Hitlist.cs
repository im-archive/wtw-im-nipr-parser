using System.Xml.Serialization;
using Nipr.Parser.Common;

namespace Nipr.Parser.Npn
{
    [XmlRoot("HITLIST")]
    public class HitList : IResponse
    {
        [XmlElement("TRANSACTION_TYPE")]
        public TransactionType TransactionType { get; set; }

        [XmlElement("ERROR")]
        public Error Error { get; set; }

        [XmlElement("INDIVIDUAL")]
        public Individual Individual { get; set; }

        public static HitList Generate(Individual individual)
        {
            var hitlist = HitList.GenerateEmpty();
            hitlist.Individual = individual;
            return hitlist;
        }

        public static HitList GenerateEmpty()
        {
            return new HitList
            {
                TransactionType = new TransactionType { Type = TransactionTypes.HitList },
                Individual = new Individual()
            };
        }

        public static HitList GenerateError(string message)
        {
            return new HitList
            {
                TransactionType = new TransactionType { Type = TransactionTypes.Error },
                Error = new Error { Description = message }
            };
        }
    }
}