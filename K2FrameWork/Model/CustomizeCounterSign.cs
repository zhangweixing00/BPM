using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CustomizeCounterSign
    {
        public bool enabled { get; set; }

        public int csState { get; set; }

        public string csTitle { get; set; }

        public string counterSignCodes { get; set; }

        public string counterSignNames { get; set; }

        public string counterDeptName { get; set; }

        public string counterDeptCode { get; set; }

        public bool isChecked { get; set; }

        public string CSNodeGuid { get; set; }
    }
}
