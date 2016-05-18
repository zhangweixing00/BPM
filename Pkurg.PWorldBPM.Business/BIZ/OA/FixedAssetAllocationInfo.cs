using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.BIZ.OA
{
    public class FixedAssetAllocationInfo
    {
        public string ReportCode;
        public string FormId { get; set; }
        public string SecurityLevel { get; set; }
        public string UrgenLevel { get; set; }
        public string StartDeptId { get; set; }
        public string DeptName { get; set; }
        public string UserName { get; set; }
        public string CreateTime { get; set; }
        public string LeadersSelected { get; set; }
        public string ApproveResult { get; set; }
        public string DepartName { get; set; }
        public string ApplyDate { get; set; }
        public string Applicant { get; set; }
        public string Phone { get; set; }
        public string FixedName { get; set; }
        public string Quantity { get; set; }   
        public string Price { get; set; }
        public string Account { get; set; }
        public string Sum { get; set; }
        public string DeployReason { get; set; }  
    }
}
