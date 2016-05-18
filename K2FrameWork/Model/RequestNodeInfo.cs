using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RequestNodeInfo
    {
        public Guid NodeID { get; set; }
        public string ProcessID { get; set; }
        public string NodeName { get; set; }
        public string Expression { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool State { get; set; }
    }
}
