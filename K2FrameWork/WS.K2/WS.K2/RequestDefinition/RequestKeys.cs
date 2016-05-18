using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2.RequestDefinition
{
    [Serializable]
    [XmlType("Keys")]
    public class RequestKeys
    {
        [XmlElement("Key")]
        public RequestKeysField Key { get; set; }
    }
}