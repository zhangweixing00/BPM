using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ApproveRuleGroupInfo
    {
        public Guid ID { get; set; }
        public string  ProcessID { get; set; }
        public string RuleTableName { get; set; }
        public string RequestSPName { get; set; }
        public string GroupName { get; set; }
        public int OrderNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
