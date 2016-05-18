using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.WorkFlowRule
{
    /// <summary>
    /// 流程门户设置
    /// </summary>
    public class Setting
    {
        public string GetValueByName(string name)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                WR_Setting obj = db.WR_Setting.Where(p => p.Record_Status == 0 && p.Name == name).FirstOrDefault();
                return obj != null ? obj.Value : "";
            }
        }

        public List<WR_Setting> GetSettingByCategory(string category)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.WR_Setting.Where(p => p.Record_Status == 0).ToList();
            }
        }
    }
}
