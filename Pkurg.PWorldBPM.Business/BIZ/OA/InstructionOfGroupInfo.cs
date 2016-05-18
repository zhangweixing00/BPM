using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.BIZ
{
    public class InstructionOfGroupInfo
    {
        public string FormID { get; set; }
        public string SecurityLevel { get; set; }
        public string UrgenLevel { get; set; }
        public string DeptName { get; set; }
        public string DeptCode { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string DateTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string LeadersSelected { get; set; }
        public string IsReport { get; set; }
        public string RelatedFormID { get; set; }
        public string IsApproval { get; set; }
    }
}
