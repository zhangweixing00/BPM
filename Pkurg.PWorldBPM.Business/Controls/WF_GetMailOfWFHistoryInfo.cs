using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_GetMailOfWFHistoryInfo
    {
        public DateTime SendTime { get; set; }
        public string InstanceID { get; set; }
        public string SN { get; set; }
        public string FormTitle { get; set; }
        public string ApproveLeader { get; set; }
        public string Status { get; set; }
        public string ApproveLeaderEmail { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
