using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2
{
    [Serializable]
    [XmlType("Destination")]
    public class ApprovalChainDestination
    {
        public ApprovalChainDestination()
        {
            this.ID = Guid.NewGuid().ToString();
            this.ActivityID = string.Empty;
            this.Status = EnumStatus.Available.ToString();
            this.Sequence = 0;
            this.ActionResult = string.Empty;
            this.ActualActionResult = string.Empty;
            this.ActualUserID = string.Empty;
        }

        public ApprovalChainDestination(string id, string account, string name, string email, string type)
        {
            this.ID = Guid.NewGuid().ToString();
            this.ActivityID = string.Empty;
            this.Status = EnumStatus.Available.ToString();
            this.Sequence = 0;
            this.ActionResult = string.Empty;
            this.ActualActionResult = string.Empty;
            this.ActualUserID = string.Empty;

            this.AddDestinationUser(new ApprovalChainDestinationUser(id, account, name, email, type));
        }

        public void AddActivity(ApprovalChainActivity acivity)
        {
            if (ApprovalChainActivitys == null)
            {
                ApprovalChainActivitys = new List<ApprovalChainActivity>();
            }
            acivity.Sequence = ApprovalChainActivitys.Count + 1;
            ApprovalChainActivitys.Add(acivity);
        }

        public bool RemoveActivity(ApprovalChainActivity acivity)
        {
            return ApprovalChainActivitys.Remove(acivity);
        }

        public void AddDestinationUser(ApprovalChainDestinationUser user)
        {
            if (ApproveChainDestinationUsers == null)
            {
                ApproveChainDestinationUsers = new List<ApprovalChainDestinationUser>();
            }
            ApproveChainDestinationUsers.Add(user);
        }

        public bool RemoveDestinationUser(ApprovalChainDestinationUser user)
        {
            return ApproveChainDestinationUsers.Remove(user);
        }

        #region ICollection<DestinationUser> Members
        [XmlElement("ID")]
        public string ID { get; set; }

        [XmlElement("ActivityID")]
        public string ActivityID { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }

        [XmlElement("Sequence")]
        public int Sequence { get; set; }

        [XmlElement("ActionResult")]
        public string ActionResult { get; set; }

        [XmlElement("ActualActionResult")]
        public string ActualActionResult { get; set; }

        [XmlElement("ActualUserID")]
        public string ActualUserID { get; set; }

        [XmlElement("User")]
        public List<ApprovalChainDestinationUser> ApproveChainDestinationUsers
        {
            get;
            set;
        }

        [XmlElement("Activity")]
        public List<ApprovalChainActivity> ApprovalChainActivitys
        {
            get;
            set;
        }
        #endregion
    }
}