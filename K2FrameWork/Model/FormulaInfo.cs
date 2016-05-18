using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FormulaInfo
    {
        public Guid ID { get; set; }
        public string GroupName { get; set; }
        public string Formula { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
