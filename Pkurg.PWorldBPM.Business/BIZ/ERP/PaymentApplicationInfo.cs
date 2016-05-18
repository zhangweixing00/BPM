using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.Entites.BIZ.ERP
{
    public class PaymentApplicationInfo
    {
        public string FormId { get; set; }
        public int IsOverContract { get; set; }
        public string ErpFormId { get; set; }
        public string ErpFormType { get; set; }
        public string StartDeptId { get; set; }
        public int IsCheckedChairman { get; set; }
        //public string LeaderSelect { get; set; }
        public string ApproveResult { get; set; }
        public DateTime? CreateTime { get; set; }
        public string LeadersSelected { get; set; }
        public string Amount { get; set; }
    }
}
