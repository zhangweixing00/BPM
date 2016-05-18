using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DepartmentInfo
    {
        public int ID { get; set; }
        public Guid OrgID { get; set; }
        public string Code { get; set; }
        public string DeptCode { get; set; }
        public string ParentCode { get; set; }
        public int ParentID { get; set; }
        public string DeptName { get; set; }
        public string Abbreviation { get; set; }
        public int Levels { get; set; }
        public string DeptTypeCode { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public int OrderNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ParentDepartment { get; set; }
        public bool IsMainDept { get; set; }
    }
}
