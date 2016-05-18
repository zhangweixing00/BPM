using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DelegationInfo
    {

        public string ProcName { get; set; }
        public string ActivityName { get; set; }
        public bool Conditions { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedByUser { get; set; }
        public string Remark { get; set; }
        public string State { get; set; }
    }
}
