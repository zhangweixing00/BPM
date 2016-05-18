using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ProcessLogInfo
    {
        public Guid ProcessLogId { get; set; }
        public string FormId { get; set; }
        public string ActivityName { get; set; }
        public string ApproverName { get; set; }
        public string ApproverID { get; set; }
        public string ApprovePosition { get; set; }
        public string Operation { get; set; }
        public string Comments { get; set; }
        public DateTime Operatetime { get; set; }
        public int State { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
    }
}
