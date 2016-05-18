using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WS.K2
{
    public class K2DBHelper
    {
        /// <summary>
        /// 记录GetApproveXML日志
        /// </summary>
        /// <param name="requestor"></param>
        /// <param name="operate"></param>
        /// <param name="processName"></param>
        /// <param name="groupName"></param>
        /// <param name="keyXml"></param>
        /// <param name="data"></param>
        public static void RecordGetApproveXML(string requestor, string operate, string processName, string groupName, string keyXml, string data,string retXml)
        {
            try
            {
                string sql = "P_K2_GetStartProcessXMLLog";

                SqlParameter[] paras = { 
                                        new SqlParameter("@Requestor",requestor),
                                        new SqlParameter("@Operater",operate), 
                                        new SqlParameter("@ProcessName",processName), 
                                        new SqlParameter("@GroupName",groupName),
                                        new SqlParameter("@KeyXml",keyXml),
                                        new SqlParameter("@Data",data),
                                        new SqlParameter("@ReturnValue",retXml)
                                   };

                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, paras);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="method"></param>
        /// <param name="createdby"></param>
        public static void RecoreErrorProfile(Exception ex, string method, string createdby)
        {
            try
            {
                string sql = "SProc_AddErrorProfile";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ErrorMsg",ex.Message),
                                        new SqlParameter("@ErrorSource",ex.Source), 
                                        new SqlParameter("@ErrorStackTrace",ex.StackTrace), 
                                        new SqlParameter("@ErrorMethod",method),
                                        new SqlParameter("@CreatedBy",createdby)
                                   };

                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, paras);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get WorkList
        /// </summary>
        /// <returns></returns>
        public static string GetWorkList(string actionerName, string folio, string source, string applicant)
        {
            try
            {
                string sql = "P_K2_GetWorklist";

                SqlParameter[] paras = { 
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@Source", source),
                                        new SqlParameter("@ActionerName", actionerName),
                                        new SqlParameter("@Originator", applicant)
                                   };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return Common.ConvertDataSetToXML(ds);
            }
            catch
            {
                throw;
            }
        }

        public static string GetMyApplication(string actionerName, string folio, string startTime, string endTime, string source)
        {
            try
            {
                string sql = "P_K2_GetMyApplication";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserName", actionerName),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@StartTime", startTime),
                                        new SqlParameter("@EndTime", endTime),
                                        new SqlParameter("@Source", source)
                                   };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return Common.ConvertDataSetToXML(ds);
            }
            catch
            {
                throw;
            }
        }

        public static string GetMyJoined(string actionerName, string folio, string startTime, string endTime, string applicant, string source)
        {
            try
            {
                string sql = "P_K2_GetMyDoc";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ActionerName", actionerName),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@StartTime", startTime),
                                        new SqlParameter("@EndTime", endTime),
                                        new SqlParameter("@Source", source),
                                        new SqlParameter("@Originator", applicant)
                                   };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return Common.ConvertDataSetToXML(ds);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 记录审批日志
        /// </summary>
        /// <param name="ProcInstID"></param>
        /// <param name="Approver"></param>
        /// <param name="ApproverAD"></param>
        /// <param name="Comons"></param>
        /// <param name="ApproveTime"></param>
        /// <param name="Actions"></param>
        /// <returns></returns>
        public static int AddApproveLog(int ParentProcInstID,int ProcInstID,string Action, string XMLData, string Approver)
        {
            try
            {
                string sql = "P_K2_AddProcessApproveLog";

                SqlParameter[] paras = { 
                                            new SqlParameter("@ParentProcInstID",ParentProcInstID)
                                           ,new SqlParameter("@ProcInstID",ProcInstID)
                                           ,new SqlParameter("@Action", Action)
                                           ,new SqlParameter("@XMLData", XMLData)
                                           ,new SqlParameter("@Approver", Approver)
                                        };
                return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, paras);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取审批日志
        /// </summary>
        /// <param name="ProcInstID"></param>
        /// <returns></returns>
        public static string GetApproveLog(int ProcInstID)
        {
            try
            {
                string sql = "P_K2_GetApproveLog";

                SqlParameter[] paras = { 
                                            new SqlParameter("@ProcInstID",ProcInstID)
                                        };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return Common.ConvertDataSetToXML(ds);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 添加流程数据和业务数据的关联
        /// </summary>
        /// <param name="ProcInstID"></param>
        /// <param name="ProcessName"></param>
        /// <param name="BusinessFormID"></param>
        /// <param name="BusinessFormFolio"></param>
        /// <returns></returns>
        public static int AddBusinessInfo(int parentProcInstID, string ProcessName, string Folio, string XMLData, string CreatedBy, string infoSource, string key)
        {
            try
            {
                string sql = "P_K2_AddBusinessInfo";

                SqlParameter[] paras = { 
                                           new SqlParameter("@ParentProcInstID",parentProcInstID)
                                           ,new SqlParameter("@ProcessName",ProcessName)
                                           ,new SqlParameter("@Folio",Folio)
                                           ,new SqlParameter("@XMLData",XMLData)
                                           ,new SqlParameter("@CreatedBy",CreatedBy)
                                           ,new SqlParameter("@Source",infoSource)
                                           ,new SqlParameter("@Key",key)
                                        };
                return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, paras);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取主流程的所有活动的子流程ID
        /// </summary>
        /// <param name="ProcInstID">主流程实例ID</param>
        /// <returns>主流程和所有子流程ID，格式：1_2_3_4</returns>
        public static string GetActiveChildrens(int ProcInstID)
        {
            try
            {
                string sql = "P_K2_GetActiveChildrens";

                SqlParameter[] paras = { 
                                            new SqlParameter("@SrcProcInstID",ProcInstID)
                                        };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取子流程的主流程实例ID
        /// </summary>
        /// <param name="ProcInstID">子流程实例ID</param>
        /// <returns>主流程实例ID</returns>
        public static int GetRootParentsID(int ProcInstID)
        {
            try
            {
                string sql = "P_K2_GetRootParents";

                SqlParameter[] paras = { 
                                            new SqlParameter("@DstProcInstID",ProcInstID)
                                        };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取最新审批链信息
        /// </summary>
        /// <param name="parentProcInstID">主流程实例ID</param>
        /// <returns>流程最新的XML</returns>
        public static string GetProcessCurrentXMLValue(int parentProcInstID)
        {
            try
            {
                string sql = "P_K2_GetProcessCurrentXMLValue";

                SqlParameter[] paras = { 
                                            new SqlParameter("@SrcProcInstID",parentProcInstID)
                                        };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return Convert.ToString(ds.Tables[0].Rows[0][0]);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取原始审批链信息
        /// </summary>
        /// <param name="parentProcInstID">主流程实例ID</param>
        /// <returns>原始审批链信息XML</returns>
        public static string GetProcessOriginalXMLValue(int parentProcInstID)
        {
            try
            {
                string sql = "P_K2_GetProcessOriginalXMLValue";

                SqlParameter[] paras = { 
                                            new SqlParameter("@SrcProcInstID",parentProcInstID)
                                        };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return Convert.ToString(ds.Tables[0].Rows[0][0]);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取有效的代理转发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet GetActiveDelegations()
        {
            try
            {
                string sql = "P_K2_GetActiveDelegations";

                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql);
                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取我的代理转发规则
        /// </summary>
        /// <returns></returns>
        public static DataSet GetMyDelegations(string ProcName, string FromUser, string ToUser, string Status, string StartDate, string EndDate)
        {
            try
            {
                string sql = "SProc_GetDelegation";
                SqlParameter[] paras = { 
                                           new SqlParameter("@ProcName",ProcName),
	                                        new SqlParameter("@FromUser",FromUser),
	                                        new SqlParameter("@ToUser",ToUser),
	                                        new SqlParameter("@Status",Status),
	                                        new SqlParameter("@StartDate",StartDate),
	                                        new SqlParameter("@EndDate",EndDate)
                                        };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取已经被代理过的流程记录
        /// </summary>
        /// <returns></returns>
        public static DataSet GetDelegationsLog(string sn)
        {
            try
            {
                string sql = "SProc_GetDelegationLog";
                SqlParameter[] paras = { 
                                            new SqlParameter("@SN",sn)
                                        };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 添加流程代理记录
        /// </summary>
        /// <param name="ProcInstID"></param>
        /// <param name="ParentProcInstID"></param>
        /// <param name="SN"></param>
        /// <param name="FromUsr"></param>
        /// <param name="ToUser"></param>
        /// <param name="DeleteActionor"></param>
        public static void AddDelegationsLog(int ProcInstID, int ParentProcInstID, string SN, string FromUsr, string ToUser, string DeleteActionor)
        {
            try
            {
                string sql = "SProc_AddDelegationLog";
                SqlParameter[] paras = { 
                                            new SqlParameter("@ProcInstID",ProcInstID)
                                           ,new SqlParameter("@ParentProcInstID",ParentProcInstID)
                                           ,new SqlParameter("@SN",SN)
                                           ,new SqlParameter("@FromUsr",FromUsr)
                                           ,new SqlParameter("@ToUser",ToUser)
                                           ,new SqlParameter("@DeleteActionor",DeleteActionor)
                                        };

                SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, paras);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 删除过期的代理规则
        /// </summary>
        /// <returns></returns>
        public static void DeleteExpiredDelegations()
        {
            try
            {
                string sql = "SProc_UpdateExpiredDelegations";

                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 通过用户AD取得用户信息
        /// </summary>
        /// <param name="userAd"></param>
        /// <returns></returns>
        public static DataSet GetEmployeeInfoByUserAd(string userAd)
        {
            try
            {
                string sql = "P_K2_GetEmployeeInfoByUserAD";
                SqlParameter[] parms ={
                                          new SqlParameter("@UserAD",userAd)
                                     };
                return SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 取审批人AD（与上个存储过程的区别是，人无须配置部门）
        /// </summary>
        /// <param name="userAd"></param>
        /// <returns></returns>
        public static DataSet GetUserProfileInfoByUserAd(string userAd)
        {
            try
            {
                string sql = "SProc_Admin_GetUserProfileByUserAd";
                SqlParameter[] parms ={
                                          new SqlParameter("@UserAd",userAd)
                                     };
                return SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过ID取得审批节点名称
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static string GetProcessNodeName(string nodeId)
        {
            try
            {
                string sql = "P_K2_GetProcessNodeNameByNodeID";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",nodeId)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                return "DefaultNode";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 取得当前节点退回节点ID
        /// </summary>
        /// <param name="currentActivityID">当前节点ID</param>
        /// <returns></returns>
        public static string GetTargetActivityID(string currentActivityID)
        {
            try
            {
                string activityId = string.Empty;
                string sql = "P_K2_GetTargetActivityID";
                SqlParameter[] parms ={
                                          new SqlParameter("@CurrentActivityID",currentActivityID)
                                     };
                SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.StoredProcedure, sql, parms);
                if (sdr.Read())
                {
                    activityId = sdr["NodeID"].ToString();
                }
                return activityId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 取得指定审批节点信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static DataSet GetProcessNodeByNodeID(string nodeId)
        {
            try
            {
                string sql = "P_K2_GetProcessNodeByNodeID";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",nodeId)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds;
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void AddActSlot(int procInstID, int parentProcInstID, string activityID, string activityName, string account, string type)
        {
            try
            {
                string sql = "P_K2_AddActSlot";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ProcInstID",procInstID),
                                        new SqlParameter("@ParentProcInstID",parentProcInstID), 
                                        new SqlParameter("@ActivityID",activityID), 
                                        new SqlParameter("@ActivityName",activityName), 
                                        new SqlParameter("@Account",account),
                                        new SqlParameter("@Type",type)
                                   };

                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, paras);
            }
            catch
            {
                throw;
            }
        }

        #region 测试使用
        public static void AddTemp(string folio, string type)
        {
            try
            {
                string sql = "P_K2_AddTemp";

                SqlParameter[] paras = { 
                                        new SqlParameter("@Folio",folio),
                                        new SqlParameter("@Type",type)
                                   };

                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, paras);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}