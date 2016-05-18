using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PositionInfo
    {
        public Guid ID { get; set; }
        public string PositionName { get; set; }
        public string DeptCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
