using System;
using System.IO;
using Shouldly;
using Xunit;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Nipr.Parser;

namespace Nipr.Parser.Test
{
    public class SerializerTests : IDisposable
    {
        private const string XmlInputPath = "simple.xml";
        private const string XmlOutputPath = "simple-new.xml";
        private const string FromText = "the other side";
        private const string ValueText = "hello";
        private const string StringifiedA = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<data xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n  <item from=\"the other side\">hello</item>\r\n</data>";
        private const string StringifiedB = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<data xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <item from=\"the other side\">hello</item>\r\n</data>";

        [Fact]
        public void deserialize_from_file()
        {
            var data = Serializer.Deserialize<FakeData>(XmlInputPath);

            data.ShouldSatisfyAllConditions(
                "No return objects should be null",
                () => data.ShouldNotBeNull(),
                () => data.Item.ShouldNotBeNull()
            );

            data.Item.ShouldSatisfyAllConditions(
                "Item propertys should meet expected values",
                () => data.Item.From.ShouldBe(FromText),
                () => data.Item.Value.ShouldBe(ValueText)
            );
        }

        [Fact]
        public async Task deserialize_async_from_file()
        {
            var data = await Serializer.DeserializeAsync<FakeData>(XmlInputPath);

            data.ShouldSatisfyAllConditions(
                "No return objects should be null",
                () => data.ShouldNotBeNull(),
                () => data.Item.ShouldNotBeNull()
            );

            data.Item.ShouldSatisfyAllConditions(
                "Item propertys should meet expected values",
                () => data.Item.From.ShouldBe(FromText),
                () => data.Item.Value.ShouldBe(ValueText)
            );
        }

        [Fact]
        public void serialize_to_file()
        {
            var newdata = new FakeData { Item = new FakeItem { From = FromText, Value = ValueText } };

            File.Exists(XmlOutputPath).ShouldBeFalse($"{XmlOutputPath} already exists");

            Serializer.Serialize(newdata, XmlOutputPath);

            File.Exists(XmlOutputPath).ShouldBeTrue($"{XmlOutputPath} does not exist");

            var data = Serializer.Deserialize<FakeData>(XmlOutputPath);

            data.ShouldSatisfyAllConditions(
                "No return objects should be null",
                () => data.ShouldNotBeNull(),
                () => data.Item.ShouldNotBeNull()
            );

            data.Item.ShouldSatisfyAllConditions(
                "Item propertys should meet expected values",
                () => data.Item.From.ShouldBe(FromText),
                () => data.Item.Value.ShouldBe(ValueText)
            );
        }

        [Fact]
        public async Task serialize_async_to_file()
        {
            var newdata = new FakeData { Item = new FakeItem { From = FromText, Value = ValueText } };

            File.Exists(XmlOutputPath).ShouldBeFalse($"{XmlOutputPath} already exists");

            await Serializer.SerializeAsync(newdata, XmlOutputPath);

            File.Exists(XmlOutputPath).ShouldBeTrue($"{XmlOutputPath} does not exist");

            var data = await Serializer.DeserializeAsync<FakeData>(XmlOutputPath);

            data.ShouldSatisfyAllConditions(
                "No return objects should be null",
                () => data.ShouldNotBeNull(),
                () => data.Item.ShouldNotBeNull()
            );

            data.Item.ShouldSatisfyAllConditions(
                "Item propertys should meet expected values",
                () => data.Item.From.ShouldBe(FromText),
                () => data.Item.Value.ShouldBe(ValueText)
            );
        }

        [Fact]
        public void parse_from_string()
        {
            var xml = File.ReadAllText(XmlInputPath);
            var data = Serializer.Parse<FakeData>(xml);

            data.ShouldSatisfyAllConditions(
                "No return objects should be null",
                () => data.ShouldNotBeNull(),
                () => data.Item.ShouldNotBeNull()
            );

            data.Item.ShouldSatisfyAllConditions(
                "Item propertys should meet expected values",
                () => data.Item.From.ShouldBe(FromText),
                () => data.Item.Value.ShouldBe(ValueText)
            );
        }

        [Fact]
        public async Task parse_asyn_from_string()
        {
            var xml = File.ReadAllText(XmlInputPath);
            var data = await Serializer.ParseAsync<FakeData>(xml);

            data.ShouldSatisfyAllConditions(
                "No return objects should be null",
                () => data.ShouldNotBeNull(),
                () => data.Item.ShouldNotBeNull()
            );

            data.Item.ShouldSatisfyAllConditions(
                "Item propertys should meet expected values",
                () => data.Item.From.ShouldBe(FromText),
                () => data.Item.Value.ShouldBe(ValueText)
            );
        }

        [Fact]
        public void stringify_from_obj()
        {
            var data = new FakeData { Item = new FakeItem { From = "the other side", Value = "hello" } };
            var xml = Serializer.Stringify(data);
            (xml.Equals(StringifiedA) || xml.Equals(StringifiedB)).ShouldBeTrue();
        }

        [Fact]
        public async Task stringify_async_from_obj()
        {
            var data = new FakeData { Item = new FakeItem { From = "the other side", Value = "hello" } };
            var xml = await Serializer.StringifyAsync(data);
            (xml.Equals(StringifiedA) || xml.Equals(StringifiedB)).ShouldBeTrue();
        }

        public void Dispose()
        {
            if (File.Exists(XmlOutputPath)) File.Delete(XmlOutputPath);
        }
    }

    [XmlRoot("data")]
    public class FakeData
    {
        [XmlElement("item")]
        public FakeItem Item { get; set; }

    }

    [XmlRoot("item")]
    public class FakeItem
    {
        [XmlAttribute("from")]
        public string From { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}