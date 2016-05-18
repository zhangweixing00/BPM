using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using System.Data;
using Model;
using Utility;
using IDAL;

namespace SQLServerDAL
{
    public class CustomFlowDAL : ICustomFlowDAL
    {
        /// <summary>
        /// 添加自定义流程
        /// </summary>
        /// <param name="cf"></param>
        /// <returns></returns>
        public bool AddCustomFlow(CustomFlow cf)
        {
            try
            {
                string sql = "SProc_InsertCustomFlow";
                SqlParameter[] parms ={
                                      new SqlParameter("@FormId",cf.FormId),
                                      new SqlParameter("@ProcessId",cf.ProcessId),
                                      new SqlParameter("@SubmitId",cf.SubmitId),
                                      new SqlParameter("@Applicant",cf.Applicant),
                                      new SqlParameter("@Priority",cf.Priority),
                                      new SqlParameter("@Urgent",cf.Urgent),
                                      new SqlParameter("@IsEmail",cf.IsEmail),
                                      new SqlParameter("@IsSMS",cf.IsSMS),
                                      new SqlParameter("@AppReason",cf.AppReason),
                                      new SqlParameter("@AttachIds",cf.AttachIds),
                                      new SqlParameter("@ApproveXML",cf.jqFlowChart),
                                      new SqlParameter("@State",cf.State),
                                      new SqlParameter("@Operator",cf.Operator),
                                      new SqlParameter("@CreatedBy",cf.CreatedBy),
                                      new SqlParameter("@ProcessState",cf.ProcessState),
                                      new SqlParameter("@DeptCode",cf.DeptCode)
                                 };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, System.Data.CommandType.StoredProcedure, sql, parms);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.AddCustomFlow", DBManager.GetCurrentUserAD());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得业务大类
        /// </summary>
        /// <returns></returns>
        public DataTable GetBusinessClass()
        {
            try
            {
                string sql = "SProc_GetBusinessClass";
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.GetBusinessClass", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 取得业务小类
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public DataTable GetBusinessSubClass(string classId)
        {
            try
            {
                string sql = "SProc_GetBusinessSubClass";
                SqlParameter[] parms ={
                                      new SqlParameter("@ClassId",classId)
                                 };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.GetBusinessSubClass", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 以业务小类获取文档
        /// </summary>
        /// <param name="subClassId"></param>
        /// <returns></returns>
        public DataTable GetDocBySubClassId(string subClassCode)
        {
            try
            {
                string sql = "SProc_GetDocBySubClassCode";
                SqlParameter[] parms ={
                                      new SqlParameter("@SubClassCode",subClassCode)
                                 };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.GetDocBySubClassId", DBManager.GetCurrentUserAD());
                return null;
            }
        }


        /// <summary>
        /// 取得业务数据
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public DataTable GetCustomFlowByFormId(string FormId)
        {
            try
            {
                string sql = "SProc_GetCustomFlowByFormId";
                SqlParameter[] parms ={
                                      new SqlParameter("@FormId",FormId)
                                 };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.GetCustomFlowByFormId", DBManager.GetCurrentUserAD());
                return null;
            }
        }


        /// <summary>
        /// 通过ProcessId取得表单信息
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataTable GetCustomFlowByProcessId(string processId)
        {
            try
            {
                string sql = "SProc_GetCustomFlowByProcessId";
                SqlParameter[] parms ={
                                      new SqlParameter("@ProcessId",processId)
                                 };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.GetCustomFlowByProcessId", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 通过InstanceID取得审批XML
        /// </summary>
        /// <param name="procInsId"></param>
        /// <returns></returns>
        public string GetApproveXMLByProcInsID(string procInsId)
        {
            try
            {
                string sql = "SProc_GetApproveXMLByProcInsID";
                SqlParameter[] parms ={
                                      new SqlParameter("@ProcInsID",procInsId)
                                 };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["Value"].ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.GetApproveXMLByProcInsID", DBManager.GetCurrentUserAD());
                return string.Empty;
            }
        }

        /// <summary>
        /// 更新自定义流程业务表
        /// </summary>
        /// <param name="cf"></param>
        /// <returns></returns>
        public bool UpdateCustomFlowByFormId(CustomFlow cf)
        {
            try
            {
                string sql = "SProc_UpdateCustomflowByFormID";
                SqlParameter[] parms ={
                                      new SqlParameter("@FormId",cf.FormId),
                                      new SqlParameter("@SubmitId",cf.SubmitId),
                                      new SqlParameter("@Applicant",cf.Applicant),
                                      new SqlParameter("@Priority",cf.Priority),
                                      new SqlParameter("@Urgent",cf.Urgent),
                                      new SqlParameter("@IsEmail",cf.IsEmail),
                                      new SqlParameter("@IsSMS",cf.IsSMS),
                                      new SqlParameter("@BBCategoryCode",cf.BBCategoryCode),
                                      new SqlParameter("@BSCategoryCode",cf.BSCategoryCode),
                                      new SqlParameter("@AppReason",cf.AppReason),
                                      new SqlParameter("@AttachIds",cf.AttachIds),
                                      new SqlParameter("@ApproveXML",cf.jqFlowChart),
                                      new SqlParameter("@State",cf.State),
                                      new SqlParameter("@Operator",cf.Operator),
                                      new SqlParameter("@ProcessState",cf.ProcessState),
                                      new SqlParameter("@CreatedBy",cf.CreatedBy),
                                      new SqlParameter("@AppExplain",cf.AppExplain)
                                 };
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms) > 0;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.UpdateCustomFlowByFormId", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 审批时更新附件
        /// </summary>
        /// <param name="attachIds"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        public bool UpdateCustomFlowStatusByAttachIds(string attachIds, string formId)
        {
            try
            {
                string sql = "SProc_UpdateCustomFlowStatus";
                SqlParameter[] parms ={
                                      new SqlParameter("@FormId",formId),
                                      new SqlParameter("@AttachIds",attachIds)
                                 };
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms) > 0;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.UpdateCustomFlowStatusByAttachIds", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 修改附件表的相应附件的状态
        /// </summary>
        /// <param name="attachIds"></param>
        /// <returns></returns>
        public bool UpdateAttachStatusByAttachAttachCodes(string attachcodes)
        {
            try
            {
                string sql = "SProc_UpdateAttachStatusByCodes";
                SqlParameter[] parms ={
                                     new SqlParameter("@attachCodes",attachcodes)
                                 };
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms) > 0;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.UpdateAttachStatusByAttachAttachCodes", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 更新流程状态
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="processStatus"></param>
        /// <returns></returns>
        public bool UpdateCostomFlowStatusByFormId(string formId, string processStatus)
        {
            try
            {
                string sql = "SProc_UpdateCustomFlowStatusByFormId";
                SqlParameter[] parms ={
                                      new SqlParameter("@FormId",formId),
                                      new SqlParameter("@ProcessStatus",processStatus)
                                 };
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms) > 0;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "CustomFlowDAL.UpdateCostomFlowStatusByFormId", DBManager.GetCurrentUserAD());
                return false;
            }
        }
    }
}