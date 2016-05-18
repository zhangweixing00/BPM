using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ControlInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public string Json { get; set; }
        public string Html { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
    }
}
