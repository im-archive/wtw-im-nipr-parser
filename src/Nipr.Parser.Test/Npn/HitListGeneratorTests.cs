using Xunit;
using Shouldly;
using Nipr.Parser.Npn;
using Nipr.Parser.Common;

namespace Nipr.Parser.Test.Npn
{
    public class HitListGeneratorTests
    {
        [Fact]
        public void generates_hitlist_with_individual()
        {
            var name = "Some Name";
            var individual = new Individual { Name = name };
            var hitlist = HitList.Generate(individual);

            hitlist.ShouldNotBeNull();
            hitlist.TransactionType.ShouldNotBeNull();
            hitlist.TransactionType.Type.ShouldBe(TransactionTypes.HitList);
            hitlist.Individual.ShouldNotBeNull();
            hitlist.Error.ShouldBeNull();

            hitlist.Individual.ShouldBe(individual);
            hitlist.Individual.Name.ShouldBe(name);
        }

        [Fact]
        public void generates_empty_hitlist()
        {
            var hitlist = HitList.GenerateEmpty();

            hitlist.ShouldNotBeNull();
            hitlist.TransactionType.ShouldNotBeNull();
            hitlist.TransactionType.Type.ShouldBe(TransactionTypes.HitList);
            hitlist.Individual.ShouldNotBeNull();
            hitlist.Error.ShouldBeNull();
        }

        [Fact]
        public void generates_error_hitlist()
        {
            var message = "Test Error Message";
            var hitlist = HitList.GenerateError(message);

            hitlist.ShouldNotBeNull();
            hitlist.TransactionType.ShouldNotBeNull();
            hitlist.TransactionType.Type.ShouldBe(TransactionTypes.Error);
            hitlist.Individual.ShouldBeNull();
            hitlist.Error.ShouldNotBeNull();
            hitlist.Error.Description.ShouldBe(message);
        }
    }
}