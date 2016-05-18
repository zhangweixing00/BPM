using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RequestNodeRuleInfo
    {
        public Guid ID { get; set; }
        public Guid RequestNodeID { get; set; }
        public string KeyName { get; set; }
        public string TableName { get; set; }
        public string ConditionExpression { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
