using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2
{
    [Serializable]
    [XmlType("DataField")]
    public class CDataField
    {
        public CDataField()
        {
            this.Key = string.Empty;
            this.Value = string.Empty;
            this.Type = string.Empty;
        }

        public CDataField(string key, string value, string type)
        {
            this.Key = key;
            this.Value = value;
            this.Type = type;
        }

        [XmlElement("Key")]
        public string Key { get; set; }
        [XmlElement("Value")]
        public string Value { get; set; }

        /// <summary>
        /// DF XF
        /// </summary>
        [XmlElement("Type")]
        public string Type { get; set; }
    }
}