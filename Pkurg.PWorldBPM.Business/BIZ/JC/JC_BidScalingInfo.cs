using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
    public class JC_BidScalingInfo
    {
        public string FormID { get; set; }
        public string StartDeptCode { get; set; }
        public string Title { get; set; }
        public string DeptName { get; set; }
        public string UserName { get; set; }
        public string DateTime { get; set; }
        public string Content { get; set; }
        public string EntranceTime { get; set; }
        public string IsAccreditByGroup { get; set; }
        public string FirstLevel { get; set; }
        public string FirstUnit { get; set; }
        public string SecondUnit { get; set; }
        public string ScalingResult { get; set; }
        public string BidCommittee { get; set; }
        public string IsApproval { get; set; }
    }
}
