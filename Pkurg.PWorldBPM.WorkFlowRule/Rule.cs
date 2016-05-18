using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.WorkFlowRule
{
    /// <summary>
    /// 制度业务逻辑类
    /// </summary>
    public class Rule
    {
        #region 前台页面

        /// <summary>
        /// 获取列表分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="categoryId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<V_WR_Rule> GetRuleList(int pageIndex, int pageCount, int categoryId, string key)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.V_WR_Rule
                    .Where(p => categoryId == -1 ? true : p.Category_ID == categoryId)
                    .Where(p => string.IsNullOrEmpty(key) ? true : p.Title.Contains(key))
                    .Where(p => p.Record_Status == 0)
                    .OrderByDescending(p => p.Created_On).OrderByDescending(p => p.Publish_Date).Skip(pageCount * (pageIndex - 1)).Take(pageCount).ToList();
            }
        }

        public int GetRuleCount(int categoryId, string key)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.V_WR_Rule
                    .Where(p => categoryId == -1 ? true : p.Category_ID == categoryId)
                    .Where(p => string.IsNullOrEmpty(key) ? true : p.Title.Contains(key))
                    .Where(p => p.Record_Status == 0)
                    .Count();
            }
        }

        /// <summary>
        /// 获取最新3个月发布的制度
        /// </summary>
        /// <returns></returns>
        public List<V_WR_Rule> GetTopRuleList()
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.V_WR_Rule
                    .Where(p => p.Record_Status == 0 && p.Publish_Date.HasValue && p.Publish_Date.Value > DateTime.Now.AddMonths(-3))
                    .OrderByDescending(p => p.Publish_Date.Value).ToList();
            }
        }

        #endregion

        #region Void

        public void Insert(WR_Rule model)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                db.WR_Rule.InsertOnSubmit(model);
                db.SubmitChanges();
            }
        }

        public void Update(WR_Rule model)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                WR_Rule obj = db.WR_Rule.Where(p => p.ID == model.ID && p.Record_Status == 0).FirstOrDefault();
                if (obj != null)
                {
                    obj.Modified_By = model.Modified_By;
                    obj.Modified_By_Name = model.Modified_By_Name;
                    obj.Modified_On = model.Modified_On;

                    obj.Publish_Date = model.Publish_Date;
                    obj.Category_ID = model.Category_ID;
                    obj.Title = model.Title;
                    obj.Summary = model.Summary;

                    db.SubmitChanges();
                }
            }
        }

        public void UpdateRecordStatus(WR_Rule model)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                WR_Rule obj = db.WR_Rule.Where(p => p.ID == model.ID && p.Record_Status == 0).FirstOrDefault();
                if (obj != null)
                {
                    obj.Modified_By = model.Modified_By;
                    obj.Modified_By_Name = model.Modified_By_Name;
                    obj.Modified_On = model.Modified_On;
                    obj.Record_Status = model.Record_Status;

                    //删除收藏记录
                    var items = db.WR_Focus.Where(p => p.Record_Status == 0 && p.Rule_ID == model.ID);
                    foreach (var item in items)
                    {
                        db.WR_Focus.DeleteOnSubmit(item);
                    }

                    db.SubmitChanges();
                }
            }
        }

        #endregion

        #region Get

        public WR_Rule GetModel(int id)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.WR_Rule.Where(p => p.Record_Status == 0 && p.ID == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取列表分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="userCode"></param>
        /// <param name="isRuleAdmin"></param>
        /// <param name="categoryId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<V_WR_Rule> GetViewList(int pageIndex, int pageCount, string userCode, bool isRuleAdmin, int categoryId, string key)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                string[] categoryIds = new string[] { };
                List<WR_Category> categorys = db.WR_Category.Where(p => p.Record_Status == 0 && (p.Category_Admin + ",").Contains(userCode + ",")).ToList();
                if (categorys != null && categorys.Count > 0)
                {
                    categoryIds = categorys.Select(p => p.ID.ToString()).ToArray();
                }

                return db.V_WR_Rule
                    .Where(p => p.Record_Status == 0)
                    .Where(p => isRuleAdmin ? true : categoryIds.Contains(p.Category_ID.ToString()))
                    .Where(p => categoryId == -1 ? true : p.Category_ID == categoryId)
                    .Where(p => string.IsNullOrEmpty(key) ? true : p.Title.Contains(key))
                    .OrderByDescending(p => p.Created_On).OrderByDescending(p => p.Publish_Date).Skip(pageCount * (pageIndex - 1)).Take(pageCount).ToList();
            }
        }

        /// <summary>
        /// 获取列表总数
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="isRuleAdmin"></param>
        /// <param name="category_Id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetViewListCount(string userCode, bool isRuleAdmin, int categoryId, string key)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                string[] categoryIds = new string[] { };
                List<WR_Category> categorys = db.WR_Category.Where(p => p.Record_Status == 0 && (p.Category_Admin + ",").Contains(userCode + ",")).ToList();
                if (categorys != null && categorys.Count > 0)
                {
                    categoryIds = categorys.Select(p => p.ID.ToString()).ToArray();
                }

                return db.V_WR_Rule
                    .Where(p => p.Record_Status == 0)
                    .Where(p => categoryId == -1 ? true : p.Category_ID == categoryId)
                    .Where(p => string.IsNullOrEmpty(key) ? true : p.Title.Contains(key))
                    .Where(p => isRuleAdmin ? true : categoryIds.Contains(p.Category_ID.ToString()))
                    .Count();
            }
        }

        /// <summary>
        /// 获取流程分类
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="isRuleAdmin"></param>
        /// <returns></returns>
        public List<WR_Category> GetCategoryList(string userCode, bool isRuleAdmin)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.WR_Category
                    .Where(p => p.Record_Status == 0)
                    .Where(p => isRuleAdmin ? true : ((p.Category_Admin + ",").Contains(userCode + ","))).ToList();
            }
        }

        /// <summary>
        /// 判断是否流程管理员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckIsCagegoryAdmin(string userCode)
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.WR_Category.Where(p => p.Record_Status == 0 && ((p.Category_Admin + ",").Contains(userCode + ","))).Any();
            }
        }

        #endregion
    }
}
