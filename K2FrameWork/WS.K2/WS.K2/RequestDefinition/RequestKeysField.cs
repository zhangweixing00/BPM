using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2.RequestDefinition
{
    [Serializable]
    [XmlType("Key")]
    public class RequestKeysField
    {
        [XmlAttribute("OrderNo")]
        public string OrderNo { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}