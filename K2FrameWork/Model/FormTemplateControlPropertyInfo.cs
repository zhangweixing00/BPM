using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FormTemplateControlPropertyInfo
    {
        public Guid ID { get; set; }
        public Guid ControlID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
        public string Group { get; set; }
    }
}
