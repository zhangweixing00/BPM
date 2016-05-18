using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class OrganizationInfo
    {
        public Guid ID { get; set; }
        public string OrgName { get; set; }
        public string OrgCode { get; set; }
        public string OrgDescription { get; set; }
        public int State { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int OrderNo { get; set; }
    }
}
