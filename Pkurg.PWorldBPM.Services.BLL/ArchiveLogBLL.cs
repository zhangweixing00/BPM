using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Services.DAL;

namespace Pkurg.PWorldBPM.Services.BLL
{
    public class ArchiveLogBLL
    {
        #region Void

        /// <summary>
        /// 向归档日志表进行数据操作
        /// </summary>
        /// <param name="model"></param>
        public void Operate(Archive_Log model)
        {
            using (Linq2BPMDataContext db = new Linq2BPMDataContext())
            {
                Archive_Log obj = db.Archive_Log.Where(m => m.InstanceID == model.InstanceID
                    && m.FormID == model.FormID).FirstOrDefault();
                if (obj!=null)
                {
                    // 更新
                    obj.IsSuccess = model.IsSuccess;
                    obj.UpdateTime = System.DateTime.Now;
                    db.SubmitChanges();
                }
                else
                {
                    model.CreateTime = System.DateTime.Now;
                    // 如果不存在就插入
                    db.Archive_Log.InsertOnSubmit(model);
                    db.SubmitChanges();
                }
            }
        }

        #endregion Void

    }
}
