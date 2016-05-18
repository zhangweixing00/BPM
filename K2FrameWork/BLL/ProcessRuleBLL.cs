using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using System.Data;

namespace BLL
{
    public class ProcessRuleBLL
    {
        //创建dal连接
        private static readonly IProcessRuleDAL dal = DALFactory.DataAccess.CreateProcessRuleDAL();

        /// <summary>
        /// 取得某支流程下所有的流程节点信息
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IList<ProcessNodeInfo> GetProcessNodeByProcessID(string processId)
        {
            //Guid ProcessID = Guid.Empty;
            //try
            //{
            //    ProcessID = new Guid(processId);
            //}
            //catch
            //{
            //    return null;
            //}
            DataSet ds = dal.GetProcessNodeByProcessID(processId);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<ProcessNodeInfo> pnList = new List<ProcessNodeInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProcessNodeInfo info = new ProcessNodeInfo();
                    info.NodeID = new Guid(dr["NodeID"].ToString());
                    info.ProcessID = processId;
                    info.NodeName = dr["NodeName"].ToString();
                    info.IsAllowMeet = Convert.ToBoolean(dr["IsAllowMeet"]);
                    info.IsAllowEndorsement = Convert.ToBoolean(dr["IsAllowEndorsement"]);
                    info.Notification = dr["Notification"].ToString();
                    info.WayBack = Convert.ToInt32(dr["WayBack"]);
                    info.IsAllowSpecialApproval = Convert.ToBoolean(dr["IsAllowSpecialApproval"]);
                    info.ApproveRule = Convert.ToInt32(dr["ApproveRule"]);
                    info.DeclineRule = Convert.ToInt32(dr["DeclineRule"]);
                    info.State = Convert.ToBoolean(dr["State"]);
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    pnList.Add(info);
                }
                return pnList;
            }

            return null;
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public bool InsertProcessNode(ProcessNodeInfo pn)
        {
            return dal.InsertProcessNode(pn);
        }


        /// <summary>
        /// 创建审批节点的规则
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="displayName"></param>
        /// <param name="tableName"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool InsertApproveNodeRule(string nodeID, string displayName, string tableName, string expression, string spName)
        {
            Guid NodeID = Guid.Empty;
            try
            {
                NodeID = new Guid(nodeID);
            }
            catch
            {
                return false;
            }
            return dal.InsertApproveNodeRule(NodeID, displayName, tableName, expression, spName);
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
        public bool UpdateApproveNodeRule(string Id, string nodeID, string displayName, string tableName, string expression, string spName)
        {
            Guid NodeID = Guid.Empty;
            Guid ID = Guid.Empty;
            try
            {
                NodeID = new Guid(nodeID);
                ID = new Guid(Id);
            }
            catch
            {
                return false;
            }
            return dal.UpdateApproveNodeRule(ID, NodeID, displayName, tableName, expression, spName);
        }

        /// <summary>
        /// 插入申请节点
        /// </summary>
        /// <param name="rn"></param>
        /// <returns></returns>
        public bool InsertRequestNode(RequestNodeInfo rn)
        {
            return dal.InsertRequestNode(rn);
        }

        /// <summary>
        /// 通过流程ID取得所有申请节点
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IList<RequestNodeInfo> GetRequestNodeByProcessID(string processId)
        {
            //Guid ProcessID = Guid.Empty;
            //try
            //{
            //    ProcessID = new Guid(processId);
            //}
            //catch
            //{
            //    return null;
            //}
            DataSet ds = dal.GetRequestNodeByProcessID(processId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<RequestNodeInfo> rnList = new List<RequestNodeInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RequestNodeInfo info = new RequestNodeInfo();
                    info.NodeID = new Guid(dr["NodeID"].ToString());
                    info.NodeName = dr["NodeName"].ToString();
                    info.ProcessID = dr["ProcessID"].ToString();
                    info.Expression = dr["Expression"].ToString();
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.State = Convert.ToBoolean(dr["State"]);
                    rnList.Add(info);
                }
                return rnList;
            }
            return null;
        }

        /// <summary>
        /// 插入申请节点规则表
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="keyName"></param>
        /// <param name="tableName"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool InsertRequestNodeRule(string nodeID, string keyName, string tableName, string expression)
        {
            Guid RequestNodeID = Guid.Empty;
            try
            {
                RequestNodeID = new Guid(nodeID);
            }
            catch
            {
                return false;
            }
            return dal.InsertRequestNodeRule(RequestNodeID, keyName, tableName, expression);
        }

        /// <summary>
        /// 更新申请节点规则表
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="keyName"></param>
        /// <param name="tableName"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool UpdateRequestNodeRule(string Id, string nodeID, string keyName, string tableName, string expression)
        {
            Guid NodeID = Guid.Empty;
            Guid ID = Guid.Empty;
            try
            {
                NodeID = new Guid(nodeID);
            }
            catch
            {
                return false;
            }
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    ID = new Guid(Id);
                }
                catch
                {
                    return false;
                }
            }
            return dal.UpdateRequestNodeRule(ID, NodeID, keyName, tableName, expression);
        }

        /// <summary>
        /// 取得指定的审批节点
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public IList<ProcessNodeInfo> GetApproveNodesByNodeIds(string nodeIds)
        {
            DataSet ds = dal.GetApproveNodesByNodeIds(nodeIds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<ProcessNodeInfo> pnList = new List<ProcessNodeInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProcessNodeInfo info = new ProcessNodeInfo();
                    info.NodeID = new Guid(dr["NodeID"].ToString());
                    info.ProcessID = dr["ProcessID"].ToString();
                    info.NodeName = dr["NodeName"].ToString();
                    info.IsAllowMeet = Convert.ToBoolean(dr["IsAllowMeet"]);
                    info.IsAllowEndorsement = Convert.ToBoolean(dr["IsAllowEndorsement"]);
                    info.Notification = dr["Notification"].ToString();
                    info.WayBack = Convert.ToInt32(dr["WayBack"]);
                    info.IsAllowSpecialApproval = Convert.ToBoolean(dr["IsAllowSpecialApproval"]);
                    info.ApproveRule = Convert.ToInt32(dr["ApproveRule"]);
                    info.DeclineRule = Convert.ToInt32(dr["DeclineRule"]);
                    info.State = Convert.ToBoolean(dr["State"]);
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    pnList.Add(info);
                }
                return pnList;
            }
            return null;
        }


        /// <summary>
        /// 取得指定的审批节点
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public IList<RequestNodeInfo> GetRequestNodesByNodeIds(string nodeIds)
        {
            DataSet ds = dal.GetRequestNodesByNodeIds(nodeIds);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<RequestNodeInfo> rnList = new List<RequestNodeInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RequestNodeInfo info = new RequestNodeInfo();
                    info.NodeID = new Guid(dr["NodeID"].ToString());
                    info.NodeName = dr["NodeName"].ToString();
                    info.ProcessID = dr["ProcessID"].ToString();
                    info.Expression = dr["Expression"].ToString();
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.State = Convert.ToBoolean(dr["State"]);
                    rnList.Add(info);
                }
                return rnList;
            }
            return null;
        }

        /// <summary>
        /// 插入ApproveRule表
        /// </summary>
        /// <returns></returns>
        public bool InsertApproveRule(string xml, string spName, string tableName, string groupName, string processId)
        {
            //Guid ProcessID = Guid.Empty;
            //try
            //{
            //    ProcessID = new Guid(processId);
            //}
            //catch
            //{
            //    return false;
            //}
            string bxml = xml.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
            return dal.InsertApproveRule(bxml, spName, tableName, groupName, processId);
        }

        /// <summary>
        /// 通过ProcesID取得审批表
        /// </summary>
        /// <param name="processID">流程ID</param>
        /// <returns></returns>
        public IList<ApproveRuleGroupInfo> GetApproveTableByProcessID(string processID, string groupName)
        {
            //Guid ProcessID = Guid.Empty;
            //try
            //{
            //    ProcessID = new Guid(processID);
            //}
            //catch
            //{
            //    return null;
            //}
            DataSet ds = dal.GetApproveTableByProcessID(processID, groupName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<ApproveRuleGroupInfo> argList = new List<ApproveRuleGroupInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ApproveRuleGroupInfo info = new ApproveRuleGroupInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.ProcessID = dr["ProcessID"].ToString();
                    info.RuleTableName = dr["RuleTableName"].ToString();
                    info.RequestSPName = dr["RequestSPName"].ToString();
                    info.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);

                    argList.Add(info);
                }
                return argList;
            }
            return null;
        }

        /// <summary>
        /// 新建审批规则逻辑表达式
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="formula"></param>
        /// <returns></returns>
        public bool InsertFormula(string processId, string groupName, string formula)
        {
            return dal.InsertFormula(processId, groupName, formula);
        }

        /// <summary>
        /// 通过NodeId获得流程入口节点信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public RequestNodeInfo GetRequestNodeByNodeId(string nodeId)
        {
            Guid NodeID = Guid.Empty;
            try
            {
                NodeID = new Guid(nodeId);
            }
            catch (Exception ex)
            {
                return null;
            }
            DataSet ds = dal.GetRequestNodeByNodeId(NodeID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                RequestNodeInfo info = new RequestNodeInfo();
                info.NodeID = new Guid(dr["NodeID"].ToString());
                info.NodeName = dr["NodeName"].ToString();
                info.ProcessID =dr["ProcessID"].ToString();
                info.Expression = dr["Expression"].ToString();
                info.CreatedBy = dr["CreatedBy"].ToString();
                info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                info.State = Convert.ToBoolean(dr["State"]);
                return info;
            }
            return null;
        }

        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateRequestNode(RequestNodeInfo info)
        {
            return dal.UpdateRequestNode(info);
        }

        /// <summary>
        /// 通过NodeID取得节点信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public ProcessNodeInfo GetProcessNodeByNodeID(string nodeId)
        {
            Guid NodeID = Guid.Empty;
            try
            {
                NodeID = new Guid(nodeId);
            }
            catch
            {
                return null;
            }
            DataSet ds = dal.GetProcessNodeByNodeID(NodeID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProcessNodeInfo info = new ProcessNodeInfo();
                    info.NodeID = new Guid(dr["NodeID"].ToString());
                    info.ProcessID = dr["ProcessID"].ToString();
                    info.NodeName = dr["NodeName"].ToString();
                    info.IsAllowMeet = Convert.ToBoolean(dr["IsAllowMeet"]);
                    info.IsAllowEndorsement = Convert.ToBoolean(dr["IsAllowEndorsement"]);
                    info.Notification = dr["Notification"].ToString();
                    info.WayBack = Convert.ToInt32(dr["WayBack"]);
                    info.IsAllowSpecialApproval = Convert.ToBoolean(dr["IsAllowSpecialApproval"]);
                    info.ApproveRule = Convert.ToInt32(dr["ApproveRule"]);
                    info.DeclineRule = Convert.ToInt32(dr["DeclineRule"]);
                    info.State = Convert.ToBoolean(dr["State"]);
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.URL = dr["URL"].ToString();
                    if (!string.IsNullOrEmpty(dr["WayBackNodeID"].ToString()))
                        info.WayBackNodeID = new Guid(dr["WayBackNodeID"].ToString());
                    else
                        info.WayBackNodeID = Guid.Empty;
                    info.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    info.WeightedType = dr["WeightedType"].ToString();
                    if (dr["SamplingRate"] == DBNull.Value)
                        info.SamplingRate = string.Empty;
                    else
                        info.SamplingRate = dr["SamplingRate"].ToString();

                    //增加所属部门选择
                    if (dr["DepartName"] == DBNull.Value)
                        info.DepartName = string.Empty;
                    else
                        info.DepartName = dr["DepartName"].ToString();

                    if (dr["DepartCode"] == DBNull.Value)
                        info.DepartCode = string.Empty;
                    else
                        info.DepartCode = dr["DepartCode"].ToString();
                    return info;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过processId取得流程审批节点
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public List<ProcessNodeInfo> GetProcessNodesByProcessID(string processId)
        {
            List<ProcessNodeInfo> pnList = new List<ProcessNodeInfo>();
            //Guid ProcessID = Guid.Empty;
            //try
            //{
            //    ProcessID = new Guid(processId);
            //}
            //catch
            //{
            //    return null;
            //}

            DataSet ds = dal.GetProcessNodesByProcessID(processId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProcessNodeInfo info = new ProcessNodeInfo();
                    info.NodeID = new Guid(dr["NodeID"].ToString());
                    info.ProcessID = dr["ProcessID"].ToString();
                    info.NodeName = dr["NodeName"].ToString();
                    info.IsAllowMeet = Convert.ToBoolean(dr["IsAllowMeet"]);
                    info.IsAllowEndorsement = Convert.ToBoolean(dr["IsAllowEndorsement"]);
                    info.Notification = dr["Notification"].ToString();
                    info.WayBack = Convert.ToInt32(dr["WayBack"]);
                    info.IsAllowSpecialApproval = Convert.ToBoolean(dr["IsAllowSpecialApproval"]);
                    info.ApproveRule = Convert.ToInt32(dr["ApproveRule"]);
                    info.DeclineRule = Convert.ToInt32(dr["DeclineRule"]);
                    info.State = Convert.ToBoolean(dr["State"]);
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.URL = dr["URL"].ToString();
                    if (dr["WayBackNodeID"] == DBNull.Value)
                        info.WayBackNodeID = Guid.Empty;
                    else
                        info.WayBackNodeID = new Guid(dr["WayBackNodeID"].ToString());
                    info.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    pnList.Add(info);
                }
            }

            return pnList;
        }

        /// <summary>
        /// 更新审批节点
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateProcessNode(ProcessNodeInfo info)
        {
            return dal.UpdateProcessNode(info);
        }

        /// <summary>
        /// 取得某个流程的审批表组
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public IList<ApproveRuleGroupInfo> GetApproveRuleGroupNameByProcessType(string processID)
        {
            //Guid ProcessID = Guid.Empty;
            //try
            //{
            //    ProcessID = new Guid(processID);
            //}
            //catch
            //{
            //    return null;
            //}
            DataSet ds = dal.GetApproveRuleGroupNameByProcessType(processID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<ApproveRuleGroupInfo> argi = new List<ApproveRuleGroupInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ApproveRuleGroupInfo info = new ApproveRuleGroupInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.ProcessID = dr["ProcessID"].ToString();
                    info.RequestSPName = dr["RequestSPName"].ToString();
                    info.RuleTableName = dr["RuleTableName"].ToString();
                    info.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    info.GroupName = dr["GroupName"].ToString();
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    argi.Add(info);
                }
                return argi;
            }
            return null;
        }

        /// <summary>
        /// 取得审批表达式
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public IList<FormulaInfo> GetFormulaByGroupName(string processId, string groupName)
        {
            DataSet ds = dal.GetFormulaByGroupName(processId, groupName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<FormulaInfo> fiList = new List<FormulaInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    FormulaInfo info = new FormulaInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.GroupName = dr["GroupName"].ToString();
                    info.Formula = dr["Formula"].ToString();
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    fiList.Add(info);
                }
                return fiList;
            }
            return null;
        }

        /// <summary>
        /// 通过NodeID取得请求节点规则
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public RequestNodeRuleInfo GetRequestRuleByNodeID(string nodeId)
        {
            Guid NodeID = Guid.Empty;
            try
            {
                NodeID = new Guid(nodeId);
            }
            catch
            {
                return null;
            }
            DataSet ds = dal.GetRequestRuleByNodeID(NodeID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RequestNodeRuleInfo info = new RequestNodeRuleInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.KeyName = dr["KeyName"].ToString();
                    info.RequestNodeID = new Guid(dr["RequestNodeID"].ToString());
                    info.TableName = dr["TableName"].ToString();
                    info.ConditionExpression = dr["ConditionExpression"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    return info;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过NodeID取得审批节点规则
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public ApproveNodeRuleInfo GetApproveNodeRuleByNodeID(string nodeId)
        {
            Guid NodeID = Guid.Empty;
            try
            {
                NodeID = new Guid(nodeId);
            }
            catch
            {
                return null;
            }
            DataSet ds = dal.GetApproveNodeRuleByNodeID(NodeID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ApproveNodeRuleInfo info = new ApproveNodeRuleInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.NodeID = new Guid(dr["NodeID"].ToString());
                    info.KeyName = dr["KeyName"].ToString();
                    info.TableName = dr["TableName"].ToString();
                    info.ConditionExpression = dr["ConditionExpression"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.SPName = dr["SPName"].ToString();
                    return info;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过流程ID和分组名取得流程分组信息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IList<ApproveRuleGroupInfo> GetApproveRuleGroupByGroupNameAndProcessID(string groupName, string processId)
        {
            //Guid ProcessID = Guid.Empty;
            //try
            //{
            //    ProcessID = new Guid(processId);
            //}
            //catch
            //{
            //    return null;
            //}
            DataSet ds = dal.GetApproveRuleGroupByGroupNameAndProcessID(groupName, processId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<ApproveRuleGroupInfo> argList = new List<ApproveRuleGroupInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ApproveRuleGroupInfo info = new ApproveRuleGroupInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.ProcessID = dr["ProcessID"].ToString();
                    info.RuleTableName = dr["RuleTableName"].ToString();
                    info.RequestSPName = dr["RequestSPName"].ToString();
                    info.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);

                    argList.Add(info);
                }
                return argList;
            }
            return null;
        }

        /// <summary>
        /// 取得审批表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetApproveTableByTableName(string tableName, string processId)
        {
            //Guid ProcessID = Guid.Empty;
            //try
            //{
            //    ProcessID = new Guid(processId);
            //}
            //catch
            //{
            //}
            DataSet ds = dal.GetApproveTableByTableName(tableName, processId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// 取得扩展审批表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetApproveTableExtendByTableName(string tableName, string processId)
        {
            //Guid ProcessID = Guid.Empty;
            //try
            //{
            //    ProcessID = new Guid(processId);
            //}
            //catch
            //{
            //}
            DataSet ds = dal.GetApproveTableExtendByTableName(tableName, processId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// 验证表达式
        /// </summary>
        /// <param name="sQLValue"></param>
        /// <returns></returns>
        public string ValidateExpression(string sQLValue)
        {
            return dal.ValidateExpression(sQLValue);
        }

        /// <summary>
        /// 删除流程审批节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool DeleteProcessNode(string nodeId)
        {
            return dal.DeleteProcessNode(nodeId);
        }

        /// <summary>
        /// 删除请求节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool DeleteRequestNode(string nodeId)
        {
            return dal.DeleteRequestNode(nodeId);
        }

        /// <summary>
        /// 删除规则表
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool DeleteRuleTableByID(string tableId)
        {
            Guid TableID = Guid.Empty;
            try
            {
                TableID = new Guid(tableId);
            }
            catch
            {
                return false;
            }
            return dal.DeleteRuleTableByID(TableID);
        }

        /// <summary>
        /// 取得审批节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetNodeIDsByTableID(string tableId)
        {
            Guid TableID = Guid.Empty;
            try
            {
                TableID = new Guid(tableId);
            }
            catch
            {
                return null;
            }
            return dal.GetNodeIDsByTableID(TableID);
        }

        /// <summary>
        /// 取得审批节点审批属性
        /// </summary>
        /// <param name="requestNodeId"></param>
        /// <param name="processNodeId"></param>
        /// <returns></returns>
        public bool GetIsApproveByNodeIDs(string requestNodeId, string processNodeId, string ruleTableName)
        {
            Guid RequestNodeID = Guid.Empty;
            try
            {
                RequestNodeID = new Guid(requestNodeId);
            }
            catch
            {
                return false;
            }
            Guid ProcessNodeID = Guid.Empty;
            try
            {
                ProcessNodeID = new Guid(processNodeId);
            }
            catch
            {
                return false;
            }

            return dal.GetIsApproveByNodeIDs(RequestNodeID, ProcessNodeID, ruleTableName);
        }

        /// <summary>
        /// 取得规则表信息
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetRuleInfoByTableID(string tableId)
        {
            Guid TableID = Guid.Empty;
            try
            {
                TableID = new Guid(tableId);
            }
            catch
            {
                return null;
            }
            return dal.GetRuleInfoByTableID(TableID);
        }


        /// <summary>
        /// 取得所有PWorld部门信息
        /// </summary>
        /// <returns></returns>
        public List<PWorldDeptInfo> GetPWorldDepartment()
        {
            DataSet ds = dal.GetPWorldDepartment();
            List<PWorldDeptInfo> lPdi = new List<PWorldDeptInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PWorldDeptInfo pdi = new PWorldDeptInfo();
                    if (dr["DepartCode"] != DBNull.Value || dr["DepartName"] != DBNull.Value)
                    {
                        pdi.DepartCode = dr["DepartCode"].ToString();
                        pdi.DepartName = dr["DepartName"].ToString();
                        pdi.DepartFullName = dr["Remark"].ToString();
                        lPdi.Add(pdi);
                    }
                }
            }
            return lPdi;
        }
    }
}
