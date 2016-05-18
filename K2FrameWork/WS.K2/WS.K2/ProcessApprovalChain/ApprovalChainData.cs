using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2
{
    [Serializable]
    [XmlType("Data")]
    public class ApprovalChainData
    {
        public ApprovalChainData()
        {

        }

        public void Add()
        {
        }

        public bool Remove()
        {
            return true;
        }

        #region ICollection<Data> Members
        #endregion
    }
}