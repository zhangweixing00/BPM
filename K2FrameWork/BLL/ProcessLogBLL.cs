using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using System.Data;

namespace BLL
{
    public class ProcessLogBLL
    {
        //创建dal连接
        private static readonly IProcessLogDAL dal = DALFactory.DataAccess.CreateProcessLogDAL();

        /// <summary>
        /// 取得流程审批记录
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public IList<ProcessLogInfo> GetProcessLogList(string formId)
        {
            DataSet ds = dal.GetProcessLogList(formId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<ProcessLogInfo> pliList = new List<ProcessLogInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProcessLogInfo info = new ProcessLogInfo();
                    info.ProcessLogId = new Guid(dr["ProcessLogId"].ToString());
                    info.FormId = formId;
                    info.ActivityName = dr["ActivityName"].ToString();
                    info.ApproverName = dr["ApproverName"].ToString();
                    info.ApproverID = dr["ApproverID"].ToString();
                    info.ApprovePosition = dr["ApprovePosition"].ToString();
                    info.Operation = dr["Operation"].ToString();
                    info.Comments = dr["Comments"].ToString();
                    info.Operatetime = Convert.ToDateTime(dr["Operatetime"]);
                    info.State = Convert.ToInt32(dr["State"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.Description = dr["Description"].ToString();
                    pliList.Add(info);
                }
                return pliList;
            }
            return null;
        }

        /// <summary>
        /// 添加Log
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool InsertProcessLog(ProcessLogInfo info)
        {
            return dal.InsertProcessLog(info);
        }
    }
}
