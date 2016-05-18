using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Web.UI;

namespace WS.K2
{
    public sealed class K2Rule
    {
        public static readonly string ConnectionStringTransaction = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        /// <summary>
        /// 取得审批节点的XML
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string GetApproveNodeXML(string processName, string group, string xml)
        {
            string processId = string.Empty;
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xmlDeclare = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);

            XmlElement elementRoot = xDoc.CreateElement("Rule");
            xDoc.AppendChild(elementRoot);

            DataTable dt = GetApproveRuleGroupByProcessID(processName, group);    //取得某个流程，分组下的审批表

            if (dt != null && dt.Rows.Count > 0)
            {
                processId = dt.Rows[0]["ProcessID"].ToString();
                XmlElement groupEle = xDoc.CreateElement("Group");
                groupEle.SetAttribute("Name", dt.Rows[0]["GroupName"].ToString());
                elementRoot.AppendChild(groupEle);

                foreach (DataRow dr in dt.Rows)
                {
                    XmlElement xmlEle = xDoc.CreateElement("RuleTable");
                    xmlEle.SetAttribute("OrderNo", dr["OrderNo"].ToString());
                    groupEle.AppendChild(xmlEle);

                    string reqID = ExecuteRequestProc(dr["RequestSPName"].ToString(), processId, group, dr["OrderNo"].ToString(), xml);  //执行存储过程找到审批表的唯一行ID

                    //通过规则表名取得具体的审批规则
                    DataTable ruleDT = GetRuleByRequestRuleTableName(dr["RuleTableName"].ToString(), reqID, xml);

                    if (ruleDT != null)
                    {
                        foreach (DataRow idr in ruleDT.Rows)
                        {
                            XmlElement innerRoot = xDoc.CreateElement("Node");
                            xmlEle.AppendChild(innerRoot);

                            XmlElement xmlReq = xDoc.CreateElement("RequestNodeID");
                            xmlReq.InnerText = idr["RequestNodeID"].ToString();
                            innerRoot.AppendChild(xmlReq);

                            XmlElement xmlProc = xDoc.CreateElement("ProcessNodeID");
                            xmlProc.InnerText = idr["ProcessNodeID"].ToString();
                            innerRoot.AppendChild(xmlProc);

                            XmlElement xmlApprove = xDoc.CreateElement("IsApprove");
                            xmlApprove.InnerText = idr["IsApprove"].ToString();
                            innerRoot.AppendChild(xmlApprove);
                        }
                    }
                }
                string retXml = string.Empty;   //存放结果集
                RuleExpression.Expression expression = new RuleExpression.Expression();
                int result = expression.clac(dt.Rows[0]["Formula"].ToString(), xDoc.InnerXml, out retXml);

                //表示返回正确
                if (result == 0)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(retXml);
                    return doc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='" + result.ToString() + "']").OuterXml;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 取得审批XML
        /// </summary>
        /// <param name="approveNodeXml"></param>
        /// <returns></returns>
        public static string GetApproveXML(string requestor, string approveNodeXml, string xml, string infoSource)
        {
            string retXml = string.Empty;       //返回XML

            List<Triplet> nodeUserList = new List<Triplet>();    //保存节点人员对应关系

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(approveNodeXml);

            //循环查询每个Node节点
            XmlNodeList nodeList = xDoc.SelectNodes("RuleTable/Node");
            if (nodeList != null && nodeList.Count > 0)
            {
                foreach (XmlNode node in nodeList)
                {
                    //是否需要写入节点
                    if (node.ChildNodes[2].FirstChild.Value == "True")
                    {
                        //查询该节点的
                        string processNodeId = node.ChildNodes[1].FirstChild.Value; //取得审批节点的ID
                        Triplet triplet = new Triplet();
                        //triplet.First = K2DBHelper.GetProcessNodeName(processNodeId);
                        triplet.First = processNodeId;
                        triplet.Second = GetProcessNodeApprover(processNodeId, xml);        //需要修改取得审批人的方法
                        triplet.Third = GetProcessNodeURL(processNodeId);
                        nodeUserList.Add(triplet);
                    }
                }
                retXml = CreateApproveXML(requestor, nodeUserList, xml, infoSource);    //创建审批XML
            }
            return retXml;
        }


        /// <summary>
        /// 通过ProcessID取得规则分组
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        private static DataTable GetApproveRuleGroupByProcessID(string processId)
        {
            try
            {
                string sql = "SProc_Admin_GetApproveRuleGroupByProcessID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
                //DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "", parms);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 通过ProcessID取得规则分组
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static DataTable GetApproveRuleGroupByProcessID(string processId, string group)
        {
            try
            {
                string sql = "SProc_Admin_GetApproveRuleByProcecssGroup";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId),
                                          new SqlParameter("@Group",group)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetApproveRuleGroupDataByProcessID(string ProcessID)
        {
            try
            {
                string sql = "SELECT ID,OrderNo,GroupName,ProcessID,RuleTableName,RequestSPName FROM dbo.ApproveRuleGroup  where ProcessID ='" + ProcessID.ToString() + "'";
               
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetAllProcessType()
        {
            try
            {
                string sql = " select ID,ProcessType from dbo.ProcessType";

                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 执行请求角色存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static string ExecuteRequestProc(string spName, string processId, string groupName, string orderNo, string xml)
        {
            string ret = "Member";
            try
            {
                string sql = spName;
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId),
                                          new SqlParameter("@GroupName",groupName),
                                          new SqlParameter("@OrderNo",orderNo),
                                          new SqlParameter("@xml",xml)
                                     };
                //SqlDataReader dr = SQLHelper.ExecuteReader(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);

                //if (dr.Read())
                //{
                //    ret = dr[0].ToString();
                //}
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
                if (ds != null & ds.Tables.Count > 0)
                {
                    ret = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return ret;
        }

        /// <summary>
        /// 生成请求规则表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private static DataTable GetRuleByRequestRuleTableName(string tableName, string keyName, string xml)
        {
            try
            {
                string sql = "P_K2_Admin_GetRuleByRuleTableName";
                SqlParameter[] parms ={
                                          new SqlParameter("@TableName",tableName),
                                          new SqlParameter("@RequestKeyName",keyName),
                                          new SqlParameter("@xml",xml)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);

                /*---------创建表--------------*/
                DataTable resultDT = new DataTable();
                resultDT.Columns.Add(new DataColumn("RequestNodeID"));
                resultDT.Columns.Add(new DataColumn("ProcessNodeID"));
                resultDT.Columns.Add(new DataColumn("IsApprove"));
                resultDT.Columns.Add(new DataColumn("Expression"));

                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    for (int i = 0; i < ds.Tables.Count; i++)
                //    {
                //        if (ds.Tables[i].Rows.Count != 0)
                //        {
                //            DataRow dr = resultDT.NewRow();
                //            dr["RequestNodeID"] = ds.Tables[i].Rows[0][0].ToString();
                //            dr["ProcessNodeID"] = ds.Tables[i].Rows[0][1].ToString();
                //            dr["IsApprove"] = Boolean.Parse(ds.Tables[i].Rows[0][2].ToString() == "1" ? "true" : "false");
                //            dr["Expression"] = ds.Tables[i].Rows[0][3].ToString();
                //            resultDT.Rows.Add(dr);
                //        }
                //    }
                //}

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow d in ds.Tables[0].Rows)
                    {
                        DataRow dr = resultDT.NewRow();
                        dr["ProcessNodeID"] = d[0].ToString();
                        dr["RequestNodeID"] = d[1].ToString();
                        if (d[5].ToString() == "0" || d[5].ToString() == "1")
                            dr["IsApprove"] = d[5].ToString() == "1" ? true : false;
                        else
                            dr["IsApprove"] = Convert.ToBoolean(d[5].ToString()) == true ? true : false;
                        dr["Expression"] = d[6].ToString();
                        resultDT.Rows.Add(dr);
                    }
                }
                return resultDT;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 取得每个节点的审批人
        /// </summary>
        /// <param name="processNodeId"></param>
        /// <returns>审批人AD账号</returns>
        private static string GetProcessNodeApprover(string processNodeId, string xml)
        {
            try
            {
                string sql = "SProc_Admin_GetApprover";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessNodeID",processNodeId),
                                          new SqlParameter("@xml",xml)
                                     };
                //SqlDataReader dr = SQLHelper.ExecuteReader(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);
                //if (dr.Read())
                //{
                //    return dr.GetString(0);
                //}
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
                if (ds != null & ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                //return string.Empty;
                return ConfigurationManager.AppSettings["DefaultApprover"];
            }
            return ConfigurationManager.AppSettings["DefaultApprover"];
        }



        /// <summary>
        /// 取得审批节点URL
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        private static string GetProcessNodeURL(string nodeId)
        {
            try
            {
                string sql = "SProc_Admin_GetApprovalURL";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessNodeID",nodeId),
                                     };
                //SqlDataReader dr = SQLHelper.ExecuteReader(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);
                //if (dr.Read())
                //{
                //    return dr.GetString(0);
                //}
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
                if (ds != null & ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                //return "Process/Approve.aspx";
            }
            return "Process/Approve.aspx";
        }

        /// <summary>
        /// 通过AD查询用户信息
        /// </summary>
        /// <param name="ad"></param>
        /// <returns></returns>
        private static DataTable GetUserProfileInfoByAdAccount(string ad)
        {
            try
            {
                string sql = "SProc_Admin_GetUserProfileByADAccount";
                SqlParameter[] parms ={
                                          new SqlParameter("@ADAccount",ad)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 通过节点与人员对应关系生成审批XML
        /// </summary>
        /// <param name="pairList"></param>
        /// <returns></returns>
        private static string CreateApproveXML(string requestor, List<Triplet> pairList, string xml, string infoSource)
        {
            ApprovalChainProcess process = new ApprovalChainProcess("自定义流程", "");
            process.DefinitionType = infoSource;
            ApprovalChainData data = new ApprovalChainData();
            process.AddData(data);

            //发起人节点
            ApprovalChainActivity activity = new ApprovalChainActivity(Guid.NewGuid().ToString(), "发起人", true, "http://localhost:3373/Resubmit.aspx?SN=", EnumActivityType.SP.ToString());
            activity.Status = EnumStatus.Completed.ToString();
            activity.ActionWeightType = EnumActionWeightType.N.ToString();
            ApprovalChainDestination originator = new ApprovalChainDestination("", requestor, "", "", EnumDestinationUserType.User.ToString());
            originator.Status = EnumStatus.Completed.ToString();
            activity.AddDestination(originator);
            process.AddActivity(activity);

            foreach (Triplet tl in pairList)
            {
                //string nodeName = K2DBHelper.GetProcessNodeName(tl.First.ToString());
                DataSet ds = K2DBHelper.GetProcessNodeByNodeID(tl.First.ToString());
                if (ds != null)
                {
                    string nodeName = ds.Tables[0].Rows[0]["NodeName"].ToString();
                    string weightedType = ds.Tables[0].Rows[0]["WeightedType"].ToString();
                    string samplingRate = string.Empty;
                    if (ds.Tables[0].Rows[0]["SamplingRate"] != DBNull.Value)
                        samplingRate = ds.Tables[0].Rows[0]["SamplingRate"].ToString();

                    activity = new ApprovalChainActivity(tl.First.ToString(), nodeName, true, tl.Third.ToString(), EnumActivityType.SP.ToString());
                    string[] users = tl.Second.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    ApprovalChainDestination des = new ApprovalChainDestination();

                    if (weightedType == EnumActionWeightType.P.ToString())
                    {
                        activity.ActionWeightType = weightedType;
                        activity.ActionWeight = samplingRate;
                        foreach (string user in users)
                        {
                            ds = K2DBHelper.GetEmployeeInfoByUserAd(user);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                string Id = ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
                                string email = ds.Tables[0].Rows[0]["Email"].ToString();
                                string name = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                                des.AddDestinationUser(new ApprovalChainDestinationUser(Id, user, name, email, EnumDestinationUserType.User.ToString()));
                            }
                        }
                        activity.AddDestination(des);
                    }
                    else if (weightedType == EnumActionWeightType.R.ToString())
                    {
                        activity.ActionWeightType = weightedType;
                        activity.ActionWeight = "1";
                        foreach (string user in users)
                        {
                            ds = K2DBHelper.GetEmployeeInfoByUserAd(user);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                string Id = ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
                                string email = ds.Tables[0].Rows[0]["Email"].ToString();
                                string name = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                                des.AddDestinationUser(new ApprovalChainDestinationUser(Id, user, name, email, EnumDestinationUserType.User.ToString()));
                            }
                        }
                        activity.AddDestination(des);
                    }
                    else
                    {
                        activity.ActionWeightType = weightedType;
                        foreach (string user in users)
                        {
                            ds = K2DBHelper.GetEmployeeInfoByUserAd(user);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                string Id = ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
                                string email = ds.Tables[0].Rows[0]["Email"].ToString();
                                string name = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                                activity.AddDestination(new ApprovalChainDestination(Id, user, name, email, EnumDestinationUserType.User.ToString()));
                            }
                        }
                    }
                    process.AddActivity(activity);
                }
            }

            string processApprovalChain = SerializationHelper.Serialize(process);

            return processApprovalChain;
        }
    }
}