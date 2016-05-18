using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RoleInfo
    {
        public Guid ID { get; set; }
        public string RoleName { get; set; }
        public Guid ProcessCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ProcessType { get; set; }
        public Guid OrgID { get; set; }
        public string Desciption { get; set; }
    }
}
