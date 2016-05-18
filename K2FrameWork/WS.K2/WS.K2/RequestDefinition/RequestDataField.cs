using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2.RequestDefinition
{
    [Serializable]
    [XmlType("Field")]
    public class RequestDataField
    {
        [XmlAttribute("OrderNo")]
        public string OrderNo { get; set; }

        [XmlAttribute("Code")]
        public string Code { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }
    }
}