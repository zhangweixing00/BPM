using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2
{
    [Serializable]
    [XmlType("Process")]
    public class ApprovalChainProcess
    {
        public ApprovalChainProcess()
        {
            this.Name = string.Empty;
            this.Status = EnumStatus.Available.ToString();
            this.DefinitionType = string.Empty;
            this.ActionResult = string.Empty;
        }

        public ApprovalChainProcess(string name, string definitionType)
        {
            this.Name = name;
            this.Status = EnumStatus.Available.ToString();
            this.DefinitionType = definitionType;
            this.ActionResult = string.Empty;
        }

        public void AddActivity(ApprovalChainActivity acivity)
        {
            if (ApprovalChainActivitys == null)
            {
                this.ApprovalChainActivitys = new List<ApprovalChainActivity>();
            }
            acivity.Sequence = ApprovalChainActivitys.Count + 1;
            ApprovalChainActivitys.Add(acivity);
        }

        public void AddData(ApprovalChainData data)
        {
            if (ApprovalChainData == null)
            {
                this.ApprovalChainData = new List<ApprovalChainData>();
            }
            ApprovalChainData.Add(data);
        }

        public bool RemoveActivity(ApprovalChainActivity acivity)
        {
            //TODO:activity后续节点的Sequence依次减1
            //int seq = acivity.Sequence;
            //foreach (ApprovalChainActivity app in ApprovalChainActivitys)
            //{
            //    if (app.Sequence > seq)
            //    {
            //        app.Sequence--;
            //    }
            //}
            //ApprovalChainActivitys.Sort(Sort);
            return ApprovalChainActivitys.Remove(acivity);
        }

        public bool RemoveData(ApprovalChainData data)
        {
            return ApprovalChainData.Remove(data);
        }

        private int Sort(ApprovalChainActivity a1, ApprovalChainActivity a2)
        {
            return a1.Sequence.CompareTo(a2.Sequence);
        }

        #region ICollection<Process> Members
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("DefinitionType")]
        public string DefinitionType { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }

        [XmlElement("ActionResult")]
        public string ActionResult { get; set; }

        [XmlElement("Activity")]
        public List<ApprovalChainActivity> ApprovalChainActivitys
        {
            get;
            set;
        }

        [XmlElement("Data")]
        public List<ApprovalChainData> ApprovalChainData
        {
            get;
            set;
        }
        #endregion
    }
}