using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2
{
    [Serializable]
    [XmlType("Activity")]
    public class ApprovalChainActivity
    {
        public ApprovalChainActivity()
        {
            this.ID = Guid.NewGuid().ToString();
            this.Name = string.Empty;
            this.Order = true;
            this.Sequence = 0;
            this.WebUrl = string.Empty;
            this.Status = EnumStatus.Available.ToString();
            this.ActionResult = string.Empty;
            this.ActionWeight = string.Empty;
            this.ActionWeightType = EnumActionWeightType.N.ToString();
            this.Type = EnumActivityType.SP.ToString();
        }

        public ApprovalChainActivity(string id, string name, bool order, string webUrl, string type)
        {
            this.ID = id;
            this.Name = name;
            this.Order = order;
            this.Sequence = 0;
            this.WebUrl = webUrl;
            this.Status = EnumStatus.Available.ToString();
            this.ActionResult = string.Empty;
            this.ActionWeight = string.Empty;
            this.ActionWeightType = EnumActionWeightType.N.ToString();
            this.Type = type;
        }

        public ApprovalChainActivity(string id, string name, bool order, string webUrl, string type, string actionWeightType, string actionWeight)
        {
            this.ID = id;
            this.Name = name;
            this.Order = order;
            this.Sequence = 0;
            this.WebUrl = webUrl;
            this.Status = EnumStatus.Available.ToString();
            this.ActionResult = string.Empty;
            this.ActionWeight = actionWeight;
            this.ActionWeightType = actionWeightType;
            this.Type = type;
        }

        public void AddDestination(ApprovalChainDestination approvalChainDestination)
        {
            if (ApprovalChainDestination == null)
            {
                ApprovalChainDestination = new List<ApprovalChainDestination>();
            }
            approvalChainDestination.Sequence = ApprovalChainDestination.Count + 1;
            ApprovalChainDestination.Add(approvalChainDestination);
        }

        public bool RemoveDestination(ApprovalChainDestination approvalChainDestination)
        {
            return ApprovalChainDestination.Remove(approvalChainDestination);
        }

        #region ICollection<Activity> Members
        [XmlElement("ID")]
        public string ID { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Sequence")]
        public int Sequence { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Order")]
        public bool Order { get; set; }

        [XmlElement("ActionWeightType")]
        public string ActionWeightType { get; set; }

        [XmlElement("ActionWeight")]
        public string ActionWeight { get; set; }

        [XmlElement("WebUrl")]
        public string WebUrl { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }

        [XmlElement("ActionResult")]
        public string ActionResult { get; set; }


        [XmlElement("Destination")]
        public List<ApprovalChainDestination> ApprovalChainDestination
        {
            get;
            set;
        }
        #endregion
    }
}