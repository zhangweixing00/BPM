using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Common.Log;
using Pkurg.BPM.Data;
using Pkurg.BPM.Services;
using Pkurg.PWorldBPM.Business.Context;


namespace Pkurg.PWorldBPM.Business.Workflow
{
    public class BFApprovalRecord
    {
        /// <summary>
        /// 根据根据工作流实例ID与步骤名得到审批步骤
        /// </summary>
        /// <param name="WflInstanceId"></param>
        /// <param name="CheckByUserRole"></param>
        /// <returns></returns>
        public IQueryable<Sys.WF_Approval_Record> GetApprovalRecordByWFLInstanceId(string WflInstanceId, string CurrentActive)
        {
            return DBContext.GetSysContext().WF_Approval_Record.Where(x => x.InstanceID == WflInstanceId
                && x.CurrentActiveName == CurrentActive)
                .OrderBy(x => x.ApproveAtTime);
        }

        #region 审批步骤数据更新
        public bool AddApprovalRecord(Sys.WF_Approval_Record rd)
        {
            try
            {
                var context = DBContext.GetSysContext();
                context.WF_Approval_Record.InsertOnSubmit(rd);
                context.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
