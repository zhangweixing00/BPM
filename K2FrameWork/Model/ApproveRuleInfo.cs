using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ApproveRuleInfo
    {
        public Guid ID { get; set; }
        public string RuleTableName { get; set; }
        public Guid RequestNodeID { get; set; }
        public Guid ProcessNodeID { get; set; }
        public string IsApprove { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
