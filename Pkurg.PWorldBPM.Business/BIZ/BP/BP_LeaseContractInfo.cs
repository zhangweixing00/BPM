using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
    public class BP_LeaseContractInfo
    {
        public string FormID { get; set; }
        public short? SecurityLevel { get; set; }
        public short? UrgenLevel { get; set; }
        public string ReportCode { get; set; }
        public string StartDeptCode { get; set; }
        public string DeptName { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public DateTime? Date { get; set; }
        public string ReportTitle { get; set; }
        public int? BizType { get; set; }
        public int? BizID { get; set; }
        public short? ApproveFlag { get; set; }
        public string Reason { get; set; }
        public string Url { get; set; }
        public short? DecorationContract { get; set; }
        public short? ServiceContract { get; set; }
        public short? CompensationContract { get; set; }
        public short? ModificationContract { get; set; }
        public short? SupplementContract { get; set; }
        public short? LesseeContract { get; set; }
        public string Remark { get; set; }
        public string CreateByUserCode { get; set; }
        public string CreateByUserName { get; set; }
        public DateTime? CreateAtTime { get; set; }
        public string UpdateByUserCode { get; set; }
        public string UpdateByUserName { get; set; }
        public DateTime? SumitTime { get; set; }
        public string ApproveStatus { get; set; }
    }
}
