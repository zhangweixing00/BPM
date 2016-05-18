using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.WorkFlowRule
{
    public class Attachement
    {
        #region Void

        public void Delete(Guid guid)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                WR_Attachment obj = db.WR_Attachment.Where(p => p.Attachment_GUID == guid && p.Record_Status == 0).FirstOrDefault();
                if (obj != null)
                {
                    db.WR_Attachment.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                WR_Attachment obj = db.WR_Attachment.Where(p => p.ID == id && p.Record_Status == 0).FirstOrDefault();
                if (obj != null)
                {
                    db.WR_Attachment.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                }
            }
        }

        public void DeleteByRuleId(int ruleId)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                var items = db.WR_Attachment.Where(p => p.Record_Status == 0 && p.Rule_ID == ruleId);
                foreach (var item in items)
                {
                    db.WR_Attachment.DeleteOnSubmit(item);
                }
                db.SubmitChanges();
            }
        }

        public void Insert(List<WR_Attachment> attachments)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                db.WR_Attachment.InsertAllOnSubmit(attachments);
                db.SubmitChanges();
            }
        }

        #endregion

        #region Get

        /// <summary>
        /// 根据RuleId获取附件列表
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public List<WR_Attachment> GetListByRuleId(int ruleId)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.WR_Attachment.Where(p => p.Record_Status == 0 && p.Rule_ID == ruleId).ToList();
            }
        }

        #endregion
    }
}
