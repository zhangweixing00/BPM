using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Entites.BIZ.OA
{
    public class SystemDispatchInfo
    {
        public string ReportCode;
        public string FormId { get; set; }
        public string SecurityLevel { get; set; }
        public string UrgenLevel { get; set; }
        public string StartDeptId { get; set; }
        public string DeptName { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string DateTime { get; set; }
        public string RedHeadDocument { get; set; }
        public string IsPublish { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string LeadersSelected { get; set; }
        public string ApproveResult { get; set; }        
    }
}
