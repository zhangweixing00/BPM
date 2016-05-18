using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FormTemplateControlInfo
    {
        public Guid ID { get; set; }
        public Guid FormTemplateID { get; set; }
        public Guid ControlID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
