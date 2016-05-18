using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2.RequestDefinition
{
    [Serializable]
    [XmlType("Data")]
    public class RequestData
    {
        [XmlElement("Field")]
        public RequestDataField Field { get; set; }
    }
}