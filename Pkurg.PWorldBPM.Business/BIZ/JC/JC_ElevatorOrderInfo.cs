using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
    public class JC_ElevatorOrderInfo
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
        public string OrderType { get; set; }
        public string OrderID { get; set; }
        public string Url { get; set; }
        public decimal? MaxCost { get; set; }
        public string Note { get; set; }
        public string WFLInstanceId { get; set; }
        public string CreateByUserCode { get; set; }
        public string CreateByUserName { get; set; }
        public DateTime? CreateAtTime { get; set; }
        public string UpdateByUserCode { get; set; }
        public string UpdateByUserName { get; set; }
        public DateTime? SumitTime { get; set; }
        public string ApproveStatus { get; set; }
    }
}
