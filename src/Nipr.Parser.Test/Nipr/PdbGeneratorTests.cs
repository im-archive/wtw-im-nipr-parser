using Xunit;
using Shouldly;
using Nipr.Parser.Nipr;
using Nipr.Parser.Common;

namespace Nipr.Parser.Test.Nipr
{
    public class PdbGeneratorTests
    {
        [Fact]
        public void generates_pdb_from_biographic()
        {
            var firstName = "John";
            var lastName = "Doe";
            var biographic = new Biographic { LastName = lastName, FirstName = firstName };
            var pdb = Pdb.Generate(biographic);

            pdb.ShouldNotBeNull();
            pdb.TransactionType.ShouldNotBeNull();
            pdb.Producer.ShouldNotBeNull();
            pdb.Error.ShouldBeNull();

            pdb.TransactionType.Type.ShouldBe(TransactionTypes.Pdb);
            pdb.Producer.Individual.EntityBiographic.Biographic.ShouldBe(biographic);
            pdb.Producer.Individual.EntityBiographic.Biographic.LastName.ShouldBe(lastName);
            pdb.Producer.Individual.EntityBiographic.Biographic.FirstName.ShouldBe(firstName);
        }

        [Fact]
        public void generates_empty_pdb()
        {
            var pdb = Pdb.GenerateEmpty();

            pdb.ShouldNotBeNull();
            pdb.TransactionType.ShouldNotBeNull();
            pdb.Producer.ShouldNotBeNull();
            pdb.Error.ShouldBeNull();

            pdb.TransactionType.Type.ShouldBe(TransactionTypes.Pdb);
            pdb.Producer.Individual.EntityBiographic.Biographic.FirstName.ShouldBeEmpty();
        }

        [Fact]
        public void generates_error_pdb()
        {
            var message = "Test Error Message";
            var pdb = Pdb.GenerateError(message);

            pdb.ShouldNotBeNull();
            pdb.TransactionType.ShouldNotBeNull();
            pdb.Producer.ShouldBeNull();
            pdb.Error.ShouldNotBeNull();

            pdb.TransactionType.Type.ShouldBe(TransactionTypes.Error);
            pdb.Error.Description.ShouldBe(message);
        }
    }
}