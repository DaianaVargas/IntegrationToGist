using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace IntegrationToGist.Helpers
{
    public class DinamicJson: DynamicObject
    {
        #region Enum

        private enum JsonType
        {
            @string, number, boolean, @object, array, @null
        }

        #endregion

        #region Constructor

        public DinamicJson()
        {
            xml = new XElement("root", CreateTypeAttr(JsonType.@object));
            jsonType = JsonType.@object;
        }

        private DinamicJson(XElement element, JsonType type)
        {
            Debug.Assert(type == JsonType.array || type == JsonType.@object);

            xml = element;
            jsonType = type;
        }

        #endregion

        #region Fields

        readonly XElement xml;
        readonly JsonType jsonType;

        #endregion

        #region Public Methods
        
        /// <summary>from JsonSring to DynamicJson</summary>
        public static dynamic Parse(string json)
        {
            return Parse(json, Encoding.Unicode);
        }

        /// <summary>from JsonSring to DynamicJson</summary>
        public static dynamic Parse(string json, Encoding encoding)
        {
            using (var reader = JsonReaderWriterFactory.CreateJsonReader(encoding.GetBytes(json), XmlDictionaryReaderQuotas.Max))
            {
                return ToValue(XElement.Load(reader));
            }
        }

        /// <summary>from JsonSringStream to DynamicJson</summary>
        public static dynamic Parse(Stream stream)
        {
            using (var reader = JsonReaderWriterFactory.CreateJsonReader(stream, XmlDictionaryReaderQuotas.Max))
            {
                return ToValue(XElement.Load(reader));
            }
        }

        /// <summary>from JsonSringStream to DynamicJson</summary>
        public static dynamic Parse(Stream stream, Encoding encoding)
        {
            using (var reader = JsonReaderWriterFactory.CreateJsonReader(stream, encoding, XmlDictionaryReaderQuotas.Max, _ => { }))
            {
                return ToValue(XElement.Load(reader));
            }
        }

        #endregion

        #region Private Methods

        private static dynamic ToValue(XElement element)
        {
            var type = (JsonType)Enum.Parse(typeof(JsonType), element.Attribute("type").Value);
            switch (type)
            {
                case JsonType.boolean:
                    return (bool)element;
                case JsonType.number:
                    return (double)element;
                case JsonType.@string:
                    return (string)element;
                case JsonType.@object:
                case JsonType.array:
                    return new DinamicJson(element, type);
                case JsonType.@null:
                default:
                    return null;
            }
        }
        
        private static XAttribute CreateTypeAttr(JsonType type)
        {
            return new XAttribute("type", type.ToString());
        }

        #endregion        
    }
}