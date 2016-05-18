using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IDAL;
using Utility;
using DBUtility;
using System.Data.SqlClient;
using Model;

namespace SQLServerDAL
{
    public class ProcessRuleDAL : IProcessRuleDAL
    {
        /// <summary>
        /// 取得某支流程下所有的流程节点信息
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetProcessNodesByProcessID(string  processId)
        {
            try
            {
                string sql = "SProc_Admin_GetProcessNodeByProcessID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetProcessNodeByProcessID", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }


        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public bool InsertProcessNode(ProcessNodeInfo pn)
        {
            try
            {
                string sql = "P_K2_Admin_InsertProcessNode";
                SqlParameter[] parms ={
                                       new SqlParameter("@ProcessID",pn.ProcessID),
                                       new SqlParameter("@NodeName",pn.NodeName),
                                       new SqlParameter("@IsAllowMeet",pn.IsAllowMeet),
                                       new SqlParameter("@IsAllowEndorsement",pn.IsAllowEndorsement),
                                       new SqlParameter("@Notification",pn.Notification),
                                       new SqlParameter("@WayBack",pn.WayBack),
                                       new SqlParameter("@IsAllowSpecialApproval",pn.IsAllowSpecialApproval),
                                       new SqlParameter("@ApproveRule",pn.ApproveRule),
                                       new SqlParameter("@DeclineRule",pn.DeclineRule),
                                       new SqlParameter("@State",pn.State),
                                       new SqlParameter("@CreatedBy",pn.CreatedBy),
                                       new SqlParameter("@URL",pn.URL),
                                       new SqlParameter("@WayBackNodeID",""),
                                       new SqlParameter("@OrderNo",pn.OrderNo),
                                       new SqlParameter("@WeightedType",pn.WeightedType),
                                       new SqlParameter("@SamplingRate",""),
                                       new SqlParameter("@DepartName",string.Empty),
                                       new SqlParameter("@DepartCode",string.Empty)
                                     };
                if (pn.WayBackNodeID == Guid.Empty)
                    parms[12].Value = DBNull.Value;
                else
                    parms[12].Value = pn.WayBackNodeID;
                if (string.IsNullOrEmpty(pn.SamplingRate))
                    parms[15].Value = DBNull.Value;
                else
                    parms[15].Value = pn.SamplingRate;

                if (string.IsNullOrEmpty(pn.DepartName))
                    parms[16].Value = DBNull.Value;
                else
                    parms[16].Value = pn.DepartName;

                if (string.IsNullOrEmpty(pn.DepartCode))
                    parms[17].Value = DBNull.Value;
                else
                    parms[17].Value = pn.DepartCode;

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建审批节点的规则
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="displayName"></param>
        /// <param name="tableName"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool InsertApproveNodeRule(Guid nodeID, string displayName, string tableName, string expression, string spName)
        {
            try
            {
                string sql = "SProc_Admin_InsertApproveNodeRule";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",nodeID),
                                          new SqlParameter("@KeyName",displayName),
                                          new SqlParameter("@TableName",tableName),
                                          new SqlParameter("@ConditionExpression",expression),
                                          new SqlParameter("@CreatedBy",""),
                                          new SqlParameter("@SPName",spName)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 更新审批节点的规则
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="nodeID"></param>
        /// <param name="displayName"></param>
        /// <param name="tableName"></param>
        /// <param name="expression"></param>
        /// <param name="spName"></param>
        /// <returns></returns>
        public bool UpdateApproveNodeRule(Guid ID, Guid nodeID, string displayName, string tableName, string expression, string spName)
        {
            try
            {
                string sql = "SProc_Admin_UpdateApproveNodeRule";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",ID),
                                          new SqlParameter("@NodeID",nodeID),
                                          new SqlParameter("@KeyName",displayName),
                                          new SqlParameter("@TableName",tableName),
                                          new SqlParameter("@ConditionExpression",expression),
                                          new SqlParameter("@SPName",spName)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.UpdateApproveNodeRule", HttpContext.Current.User.Identity.Name);
                return false;
            }
        }

        /// <summary>
        /// 插入申请节点
        /// </summary>
        /// <param name="rn"></param>
        /// <returns></returns>
        public bool InsertRequestNode(RequestNodeInfo rn)
        {
            try
            {
                string sql = "SProc_Admin_InsertRequestNode";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",rn.ProcessID),
                                          new SqlParameter("@NodeName",rn.NodeName),
                                          new SqlParameter("@Expression",rn.Expression),
                                          new SqlParameter("@CreatedBy","")
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                // DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.InsertRequestNode", HttpContext.Current.User.Identity.Name);
                return false;
            }
        }

        /// <summary>
        /// 通过流程ID取得所有申请节点
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetRequestNodeByProcessID(string  processId)
        {
            try
            {
                string sql = "SProc_Admin_GetRequestNodeByProcessID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetRequestNodeByProcessID", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 插入申请节点规则表
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="keyName"></param>
        /// <param name="tableName"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool InsertRequestNodeRule(Guid nodeID, string keyName, string tableName, string expression)
        {
            try
            {
                string sql = "SProc_Admin_InsertRequestNodeRule";
                SqlParameter[] parms ={
                                          new SqlParameter("@RequestNodeID",nodeID),
                                          new SqlParameter("@KeyName",keyName),
                                          new SqlParameter("@TableName",tableName),
                                          new SqlParameter("@ConditionExpression",expression),
                                          new SqlParameter("@CreatedBy", "")
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.InsertRequestNodeRule", HttpContext.Current.User.Identity.Name);
                return false;
            }
        }

        /// <summary>
        /// 更新申请节点规则表
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="nodeId"></param>
        /// <param name="keyName"></param>
        /// <param name="tableName"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool UpdateRequestNodeRule(Guid Id, Guid nodeId, string keyName, string tableName, string expression)
        {
            try
            {
                string sql = "SProc_Admin_UpdateRequestNodeRule";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",Id),
                                          new SqlParameter("@NodeID",nodeId),
                                          new SqlParameter("@KeyName",keyName),
                                          new SqlParameter("@TableName",tableName),
                                          new SqlParameter("@ConditionExpression",expression),
                                          new SqlParameter("@CreatedBy", "")
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.InsertRequestNodeRule", HttpContext.Current.User.Identity.Name);
                return false;
            }
        }

        /// <summary>
        /// 取得指定的审批节点
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public DataSet GetApproveNodesByNodeIds(string nodeIds)
        {
            try
            {
                string sql = "SProc_Admin_GetApproveNodesByNodeIDs";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeIDs",nodeIds)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetApproveNodesByNodeIds", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 取得指定的审批节点
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public DataSet GetRequestNodesByNodeIds(string nodeIds)
        {
            try
            {
                string sql = "SProc_Admin_GetRequestNodesByNodeIDs";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeIDs",nodeIds)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetRequestNodesByNodeIds", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 插入ApproveRule表以及ApproveRuleGroup表
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="spName"></param>
        /// <param name="tableName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool InsertApproveRule(string xml, string spName, string tableName, string groupName, string  processId)
        {
            try
            {
                string sql = "SProc_Admin_InsertApproveRule";
                SqlParameter[] parms ={
                                          new SqlParameter("@xml",xml),
                                          new SqlParameter("@SPName",spName),
                                          new SqlParameter("@TableName",tableName),
                                          new SqlParameter("@GroupName",groupName),
                                          new SqlParameter("@CreatedBy",""),
                                          new SqlParameter("@ProcessID",processId)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.InsertApproveRule", HttpContext.Current.User.Identity.Name);
                return false;
            }
        }
       
        /// <summary>
        /// 通过ProcesID取得审批表
        /// </summary>
        /// <param name="processID">流程ID</param>
        /// <returns></returns>
        public DataSet GetApproveTableByProcessID(string  processID, string groupName)
        {
            try
            {
                string sql = "P_K2_Admin_GetApproveTableByProcessID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processID),
                                          new SqlParameter("@GroupName",groupName)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetApproveTableByProcessID", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 新建审批规则逻辑表达式
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="formula"></param>
        /// <returns></returns>
        public bool InsertFormula(string processID, string groupName, string formula)
        {
            try
            {
                string sql = "P_K2_Admin_InsertFormula";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processID),
                                          new SqlParameter("@GroupName",groupName),
                                          new SqlParameter("@Formula",formula),
                                          new SqlParameter("@CreatedBy","")
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.InsertFormula", HttpContext.Current.User.Identity.Name);
                return false;
            }
        }

        /// <summary>
        /// 通过NodeId获得流程入口节点信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public DataSet GetRequestNodeByNodeId(Guid nodeId)
        {
            try
            {
                string sql = "SProc_Admin_GetRequestNodeByNodeId";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",nodeId)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetRequestNodeByNodeId", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateRequestNode(RequestNodeInfo info)
        {
            try
            {
                string sql = "SProc_Admin_UpdateRequestNode";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",info.NodeID),
                                          new SqlParameter("@NodeName",info.NodeName),
                                          new SqlParameter("@Expression",info.Expression)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.UpdateRequestNode", HttpContext.Current.User.Identity.Name);
                return false;
            }
        }

        /// <summary>
        /// 更新审批节点
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateProcessNode(ProcessNodeInfo info)
        {
            try
            {
                string sql = "P_K2_Admin_UpdateProcessNode";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeName",info.NodeName),
                                          new SqlParameter("@IsAllowMeet",info.IsAllowMeet),
                                          new SqlParameter("@IsAllowEndorsement",info.IsAllowEndorsement),
                                          new SqlParameter("@Notification",info.Notification),
                                          new SqlParameter("@WayBack",info.WayBack),
                                          new SqlParameter("@IsAllowSpecialApproval",info.IsAllowSpecialApproval),
                                          new SqlParameter("@ApproveRule",info.ApproveRule),
                                          new SqlParameter("@DeclineRule",info.DeclineRule),
                                          new SqlParameter("@NodeID",info.NodeID),
                                          new SqlParameter("@URL",info.URL),
                                          new SqlParameter("@WayBackNodeID",""),
                                          new SqlParameter("@OrderNo",info.OrderNo),
                                          new SqlParameter("@WeightedType",info.WeightedType),
                                          new SqlParameter("@SamplingRate",""),
                                          new SqlParameter("@DepartName",""),
                                          new SqlParameter("@DepartCode","")
                                     };
                if (info.WayBackNodeID == Guid.Empty)
                    parms[10].Value = DBNull.Value;
                else
                    parms[10].Value = info.WayBackNodeID;
                if (string.IsNullOrEmpty(info.SamplingRate))
                    parms[13].Value = DBNull.Value;
                else
                    parms[13].Value = info.SamplingRate;

                //所属部门
                if (string.IsNullOrEmpty(info.DepartName))
                    parms[14].Value = DBNull.Value;
                else
                    parms[14].Value = info.DepartName;

                if (string.IsNullOrEmpty(info.DepartCode))
                    parms[15].Value = DBNull.Value;
                else
                    parms[15].Value = info.DepartCode;

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.UpdateProcessNode", HttpContext.Current.User.Identity.Name);
                return false;
            }
        }

        /// <summary>
        /// 通过NodeID取得节点信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public DataSet GetProcessNodeByNodeID(Guid nodeId)
        {
            try
            {
                string sql = "SProc_Admin_GetProcessNodeByNodeID";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",nodeId)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetProcessNodeByNodeID", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 通过ProcessID取得流程审批节点
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public DataSet GetProcessNodeByProcessID(string  ProcessID)
        {
            try
            {
                string sql = "P_K2_GetProcessNodeByProcessID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",ProcessID)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetProcessNodeByNodeID", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 取得某个流程的审批表组
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public DataSet GetApproveRuleGroupNameByProcessType(string  processID)
        {
            try
            {
                string sql = "SProc_Admin_GetApproveRuleGroupNameByProcessType";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processID)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetApproveRuleGroupNameByProcessType", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 取得审批表达式
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public DataSet GetFormulaByGroupName(string processId, string groupName)
        {
            try
            {
                string sql = "P_K2_Admin_GetFormulaByGroupName";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId),
                                          new SqlParameter("@GroupName",groupName)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetFormulaByGroupName", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 通过NodeID取得请求节点规则
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public DataSet GetRequestRuleByNodeID(Guid nodeId)
        {
            try
            {
                string sql = "SProc_Admin_GetRequestRuleByNodeID";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",nodeId)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetRequestRuleByNodeID", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 通过NodeID取得审批节点规则
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public DataSet GetApproveNodeRuleByNodeID(Guid nodeId)
        {
            try
            {
                string sql = "SProc_Admin_GetApproveNodeRuleByNodeID";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",nodeId)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetRequestRuleByNodeID", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 通过流程ID和分组名取得流程分组信息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetApproveRuleGroupByGroupNameAndProcessID(string groupName, string  processId)
        {
            try
            {
                string sql = "SProc_Admin_GetApproveRuleGroupByGroupNameAndProcessID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId),
                                          new SqlParameter("@GroupName",groupName)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetApproveRuleGroupByGroupNameAndProcessID", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 取得审批表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetApproveTableByTableName(string tableName, string  processId)
        {
            try
            {
                string sql = "SProc_Admin_GetApproveTableByTableName";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId),
                                          new SqlParameter("@TableName",tableName)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetApproveTableByTableName", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 取得扩展审批表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetApproveTableExtendByTableName(string tableName, string  processId)
        {
            try
            {
                string sql = "P_K2_Admin_GetApproveTableExtendByTableName";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId),
                                          new SqlParameter("@TableName",tableName)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.GetApproveTableByTableName", HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// 验证表达式
        /// </summary>
        /// <param name="sQLValue"></param>
        /// <returns></returns>
        public string ValidateExpression(string sQLValue)
        {
            try
            {
                string message = string.Empty;
                SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sQLValue, null);

                while (sdr.Read())
                {
                    message = sdr.GetValue(0).ToString();
                }

                return message;
            }
            catch (Exception ex)
            {
                //DBManager.RecoreErrorProfile(ex, "ProcessRuleDAL.ValidateExpression", HttpContext.Current.User.Identity.Name);
                return ex.Message;
            }
        }

        /// <summary>
        /// 删除审批节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool DeleteProcessNode(string nodeId)
        {
            try
            {
                string sql = "P_K2_DeleteProcessNodeByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",nodeId)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除请求节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool DeleteRequestNode(string nodeId)
        {
            try
            {
                string sql = "P_K2_DeleteRequestNodeByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@NodeID",nodeId)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除规则表
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool DeleteRuleTableByID(Guid tableId)
        {
            try
            {
                string sql = "P_K2_Admin_DeleteApproveRuleTableByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",tableId)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 取得审批节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetNodeIDsByTableID(Guid tableId)
        {
            try
            {
                string sql = "P_K2_Admin_GetNodeIDsByTableID";
                SqlParameter[] parms ={
                                          new SqlParameter("@TableID",tableId)
                                     };
                return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 取得审批节点审批属性
        /// </summary>
        /// <param name="requestNodeId"></param>
        /// <param name="processNodeId"></param>
        /// <returns></returns>
        public bool GetIsApproveByNodeIDs(Guid requestNodeId, Guid processNodeId, string ruleTableName)
        {
            try
            {
                string sql = "P_K2_Admin_GetIsApproveByNodeIDs";
                SqlParameter[] parms ={
                                          new SqlParameter("@RequestNodeID",requestNodeId),
                                          new SqlParameter("@ProcessNodeID",processNodeId),
                                          new SqlParameter("@RuleTableName",ruleTableName)
                                     };
                SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (sdr.Read())
                {
                    return Convert.ToBoolean(sdr[0]);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 取得规则表信息
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetRuleInfoByTableID(Guid tableId)
        {
            try
            {
                string sql = "P_K2_Admin_GetRuleInfoByTableID";
                SqlParameter[] parms ={
                                          new SqlParameter("@TableID",tableId)
                                     };
                return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 取得PWorld所有部门信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetPWorldDepartment()
        {
            try
            {
                string sql = "P_K2_GetPWorldDepartment";
                return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, null);
            }
            catch
            {
                return null;
            }
        }
    }
}
