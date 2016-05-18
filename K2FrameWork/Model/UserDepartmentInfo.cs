using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserDepartmentInfo
    {
        public string OrgName { get; set; }
        public Guid OrgID { get; set; }
        public Guid DeptCode { get; set; }
        public string DeptName { get; set; }
        public bool IsMain { get; set; }
    }
}
