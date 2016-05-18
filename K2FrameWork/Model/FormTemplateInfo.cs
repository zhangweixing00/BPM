using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FormTemplateInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Html { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
