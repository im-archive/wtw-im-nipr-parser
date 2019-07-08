using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Nipr.Parser
{
    public class XmlWriter : System.Xml.XmlWriter
    {
        private readonly System.Xml.XmlWriter _writer;

        public XmlWriter(TextWriter writer, XmlWriterSettings settings = null)
        {
            var inSettings = settings ?? new XmlWriterSettings
            {
                Encoding = Encoding.ASCII,
                NewLineChars = Environment.NewLine,
                ConformanceLevel = ConformanceLevel.Document,
                CheckCharacters = true,
                Indent = true,
            };

            _writer = Create(writer, inSettings);
        }

        public override void WriteStartDocument()
        {
            _writer.WriteStartDocument();
        }

        public override void WriteStartDocument(bool standalone)
        {
            _writer.WriteStartDocument(standalone);
        }

        public override void WriteEndDocument()
        {
            _writer.WriteEndDocument();
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            _writer.WriteDocType(name, pubid, sysid, subset);
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            _writer.WriteStartElement(prefix, localName, ns);
        }

        public override void WriteEndElement()
        {
            _writer.WriteFullEndElement();
        }

        public override void WriteFullEndElement()
        {
            _writer.WriteFullEndElement();
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            _writer.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteEndAttribute()
        {
            _writer.WriteEndAttribute();
        }

        public override void WriteCData(string text)
        {
            _writer.WriteCData(text);
        }

        public override void WriteComment(string text)
        {
            _writer.WriteComment(text);
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            _writer.WriteProcessingInstruction(name, text);
        }

        public override void WriteEntityRef(string name)
        {
            _writer.WriteEntityRef(name);
        }

        public override void WriteCharEntity(char ch)
        {
            _writer.WriteCharEntity(ch);
        }

        public override void WriteWhitespace(string ws)
        {
            _writer.WriteWhitespace(ws);
        }

        public override void WriteString(string text)
        {
            _writer.WriteString(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            _writer.WriteSurrogateCharEntity(lowChar, highChar);
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            _writer.WriteChars(buffer, index, count);
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            _writer.WriteRaw(buffer, index, count);
        }

        public override void WriteRaw(string data)
        {
            _writer.WriteRaw(data);
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            _writer.WriteBase64(buffer, index, count);
        }

        public override void Flush()
        {
            _writer.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            return _writer.LookupPrefix(ns);
        }

        public override WriteState WriteState => _writer.WriteState;
    }
}