using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2
{
    [Serializable]
    [XmlType("User")]
    public class ApprovalChainDestinationUser
    {
        public ApprovalChainDestinationUser()
        {
            this.ID = string.Empty;
            this.Account = string.Empty;
            this.Name = string.Empty;
            this.Email = string.Empty;
            this.Type = EnumDestinationUserType.User.ToString();
            this.Executed = false;
            this.ActionResult = string.Empty;
        }

        public ApprovalChainDestinationUser(string id, string account, string name, string email, string type)
        {
            this.ID = id;
            this.Account = account;
            this.Name = name;
            this.Email = email;
            this.Type = type;
            this.Executed = false;
            this.ActionResult = string.Empty;
        }

        [XmlElement("ID")]
        public string ID { get; set; }

        [XmlElement("Account")]
        public string Account { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Executed")]
        public bool Executed { get; set; }

        [XmlElement("ActionResult")]
        public string ActionResult { get; set; }
    }
}