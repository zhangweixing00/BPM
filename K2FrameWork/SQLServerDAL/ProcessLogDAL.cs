using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;
using Utility;
using DBUtility;
using System.Data.SqlClient;
using Model;

namespace SQLServerDAL
{
    public class ProcessLogDAL : IProcessLogDAL
    {
        /// <summary>
        /// 取得流程审批记录
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public DataSet GetProcessLogList(string formId)
        {
            try
            {
                string sql = "SProc_Admin_GetProcessLogList";
                SqlParameter[] parms ={
                                        new SqlParameter("@FormId",formId)
                                   };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "ProcessLogDAL.GetProcessLogList", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 添加审批记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool InsertProcessLog(ProcessLogInfo info)
        {
            try
            {
                string sql = "SProc_Admin_InsertProcessLog";
                SqlParameter[] parms ={
                                          new SqlParameter("@FormId",info.FormId),
                                          new SqlParameter("@ActivityName",info.ActivityName),
                                          new SqlParameter("@ApproverName",info.ApproverName),
                                          new SqlParameter("@ApproverID",info.ApproverID),
                                          new SqlParameter("@ApprovePosition",info.ApprovePosition),
                                          new SqlParameter("@Operation",info.Operation),
                                          new SqlParameter("@Comments",info.Comments),
                                          new SqlParameter("@Operatetime",info.Operatetime),
                                          new SqlParameter("@CreatedBy", DBManager.GetCurrentUserAD())
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "ProcessLogDAL.InsertProcessLog", DBManager.GetCurrentUserAD());
                return false;
            }
        }
    }
}
