using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.WorkFlowRule
{
    /// <summary>
    /// 收藏
    /// </summary>
    public class Focus
    {
        #region Void

        public void Insert(WR_Focus model)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                bool flag = db.WR_Focus.Where(p => p.Record_Status == 0 && p.Rule_ID == model.Rule_ID && p.Created_By == model.Created_By).Any();
                if (!flag)
                {
                    db.WR_Focus.InsertOnSubmit(model);
                    db.SubmitChanges();
                }
            }
        }

        public void Delete(string type, int id, string createdBy)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                WR_Focus model = db.WR_Focus.Where(p => p.Record_Status == 0 && p.Rule_ID == id && p.Created_By == createdBy).FirstOrDefault();
                if (model != null)
                {
                    db.WR_Focus.DeleteOnSubmit(model);
                    db.SubmitChanges();
                }
            }
        }

        #endregion

        #region Get

        public bool CheckIsFocus(string type, int id, string createdBy)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.WR_Focus.Where(p => p.Record_Status == 0 && p.Rule_ID == id && p.Created_By == createdBy).Any();
            }
        }


        /// <summary>
        /// 获取收藏制度
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<V_WR_RuleFocus> GetRuleFocusList(int pageIndex, int pageCount, string userCode)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {

                return db.V_WR_RuleFocus
                    .Where(p => p.Record_Status == 0 && p.Focus_By == userCode)
                    .OrderByDescending(p => p.Created_On).Skip(pageCount * (pageIndex - 1)).Take(pageCount).ToList();
            }
        }

        /// <summary>
        /// 获取收藏制度的总数
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int GetRuleFocusCount(string userCode)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.V_WR_RuleFocus
                    .Where(p => p.Record_Status == 0 && p.Focus_By == userCode)
                    .Count();
            }
        }

        #endregion
    }
}
