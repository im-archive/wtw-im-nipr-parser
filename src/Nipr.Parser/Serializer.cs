using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Nipr.Parser
{
    public static class Serializer
    {
        /// <summary>
        /// Serializes the object to the file at the path specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void Serialize<T>(T obj, string path)
        {
            using (var stream = new StreamWriter(path))
            using (var writer = new XmlWriter(stream))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj, ns);
            }
        }

        /// <summary>
        /// Asynchronously serializes the object to the file at the path specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task SerializeAsync<T>(T obj, string path)
        {
            await Task.Run(() =>
            {
                Serialize(obj, path);
            });
        }

        /// <summary>
        /// Deserializes an object from a file at the path specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Asynchronously deserializes an object from a file at the path specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<T> DeserializeAsync<T>(string path)
        {
            return await Task.Run(() => Deserialize<T>(path));
        }

        /// <summary>
        /// Deserializes an object from an xml string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Parse<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(XDocument.Parse(xml).CreateReader());
        }

        /// <summary>
        /// Asynchronously deserializes an object from an xml string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static async Task<T> ParseAsync<T>(string xml)
        {
            return await Task.Run(() => Parse<T>(xml));
        }

        /// <summary>
        /// Serializes the object to an xml string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Stringify<T>(T obj)
        {
            using (var stream = new StringWriter())
            using (var writer = new XmlWriter(stream))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
                return stream.ToString();
            }
        }

        /// <summary>
        /// Asynchronously serializes the object to an xml string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static async Task<string> StringifyAsync<T>(T obj)
        {
            return await Task.Run(() => Stringify(obj));
        }
    }
}