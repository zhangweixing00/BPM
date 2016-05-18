using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserProfileExtPropertyInfo
    {
        public Guid UserExtPropID { get; set; }
        public string UserExtProperty { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Guid UserCode { get; set; }
        public string Value { get; set; }
    }
}
