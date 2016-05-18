using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ApproveNodeRuleInfo
    {
        public Guid ID { get; set; }
        public Guid NodeID { get; set; }
        public string KeyName { get; set; }
        public string TableName { get; set; }
        public string ConditionExpression { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string SPName { get; set; }
        public string URL { get; set; }
    }
}
