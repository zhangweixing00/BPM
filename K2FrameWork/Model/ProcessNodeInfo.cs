using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ProcessNodeInfo
    {
        public Guid NodeID { get; set; }
        public string ProcessID { get; set; }
        public string NodeName { get; set; }
        public bool IsAllowMeet { get; set; }
        public bool IsAllowEndorsement { get; set; }
        public string Notification { get; set; }
        public int WayBack { get; set; }
        public bool IsAllowSpecialApproval { get; set; }
        public int ApproveRule { get; set; }
        public int DeclineRule { get; set; }
        public bool State { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string URL { get; set; }
        public Guid WayBackNodeID { get; set; }
        public int OrderNo { get; set; }
        public string WeightedType { get; set; }
        public string SamplingRate { get; set; }
        public string DepartName { get; set; }
        public string DepartCode { get; set; }
    }
}
