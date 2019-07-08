using System.Xml.Serialization;
using Nipr.Parser.Common;

namespace Nipr.Parser.Nipr
{
    [XmlRoot("PDB")]
    public class Pdb : IResponse
    {
        [XmlElement("TRANSACTION_TYPE")]
        public TransactionType TransactionType { get; set; }

        [XmlElement("ERROR")]
        public Error Error { get; set; }

        [XmlElement("PRODUCER")]
        public Producer Producer { get; set; }

        public static Pdb Generate(Biographic biographic)
        {
            var pdb = Pdb.GenerateEmpty();
            pdb.Producer.Individual.EntityBiographic.Biographic = biographic;
            return pdb;
        }

        public static Pdb GenerateEmpty()
        {
            return new Pdb
            {
                TransactionType = new TransactionType { Type = TransactionTypes.Pdb },
                Producer = new Producer()
            };
        }

        public static Pdb GenerateError(string message)
        {
            return new Pdb
            {
                TransactionType = new TransactionType { Type = TransactionTypes.Error },
                Error = new Error { Description = message }
            };
        }
    }
}