using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RoleUserInfo
    {
        public Guid ID { get; set; }
        public Guid RoleCode { get; set; }
        public Guid UserCode { get; set; }
        public string ADAccount { get; set; }
        public string MainRoleName { get; set; }
        public string MainRoleCode { get; set; }
        public string ExpandField1 { get; set; }
        public string ExpandField2 { get; set; }
        public string ExpandField3 { get; set; }
        public string ExpandField4 { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string RoleName { get; set; }
        public string CHName { get; set; }
    }
}
