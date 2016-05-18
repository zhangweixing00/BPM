using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2
{
    [Serializable]
    [XmlType("DataFields")]
    public class CDataFields
    {
        public CDataFields()
        { }

        public CDataFields(string key, string value, string type)
        {
            this.AddDataField(new CDataField(key, value, type));
        }
        public void AddDataField(CDataField dataField)
        {
            if (DataFieldLists == null)
            {
                DataFieldLists = new List<CDataField>();
            }
            DataFieldLists.Add(dataField);
        }

        public bool RemoveDataField(CDataField dataField)
        {
            return DataFieldLists.Remove(dataField);
        }

        [XmlElement("DataField")]
        public List<CDataField> DataFieldLists { get; set; }
    }
}