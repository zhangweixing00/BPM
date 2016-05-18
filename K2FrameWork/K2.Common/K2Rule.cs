using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Web.UI;

namespace K2.Common
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
        public static string GetApproveNodeXML(string processId, string group, string xml)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xmlDeclare = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);

            XmlElement elementRoot = xDoc.CreateElement("Rule");
            xDoc.AppendChild(elementRoot);

            DataTable dt = GetApproveRuleGroupByProcessID(processId, group);    //取得某个流程，分组下的审批表
            if (dt != null && dt.Rows.Count > 0)
            {
                XmlElement groupEle = xDoc.CreateElement("Group");
                groupEle.SetAttribute("Name", dt.Rows[0]["GroupName"].ToString());
                elementRoot.AppendChild(groupEle);

                foreach (DataRow dr in dt.Rows)
                {
                    XmlElement xmlEle = xDoc.CreateElement("RuleTable");
                    xmlEle.SetAttribute("OrderNo", dr["OrderNo"].ToString());
                    groupEle.AppendChild(xmlEle);

                    //执行存储过程找到审批表的唯一行ID
                    string reqID = ExecuteRequestProc(dr["RequestSPName"].ToString(), processId, group, dr["OrderNo"].ToString(), xml);

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
        public static string GetApproveXML(string approveNodeXml, string xml)
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
                        triplet.First = processNodeId;
                        triplet.Second = GetProcessNodeApprover(processNodeId, xml);
                        triplet.Third = GetProcessNodeURL(processNodeId);
                        //Pair pair = new Pair();
                        //pair.First = processNodeId;
                        //pair.Second = GetProcessNodeApprover(processNodeId, xml);
                        nodeUserList.Add(triplet);
                    }
                }
                retXml = CreateApproveXML(nodeUserList, xml);    //创建审批XML
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
                DataSet ds = SQLHelper.ExecuteDataset(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);
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
        private static DataTable GetApproveRuleGroupByProcessID(string processId,string group)
        {
            try
            {
                string sql = "SProc_Admin_GetApproveRuleByProcecssGroup";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",processId),
                                          new SqlParameter("@Group",group)
                                     };
                DataSet ds = SQLHelper.ExecuteDataset(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);
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
                SqlDataReader dr = SQLHelper.ExecuteReader(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);

                if (dr.Read())
                {
                    ret = dr[0].ToString();
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
                string sql = "SProc_Admin_GetRuleByRuleTableName";
                SqlParameter[] parms ={
                                          new SqlParameter("@TableName",tableName),
                                          new SqlParameter("@RequestKeyName",keyName),
                                          new SqlParameter("@xml",xml)
                                     };
                DataSet ds = SQLHelper.ExecuteDataset(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);
                /*---------创建表--------------*/
                DataTable resultDT = new DataTable();
                resultDT.Columns.Add(new DataColumn("RequestNodeID"));
                resultDT.Columns.Add(new DataColumn("ProcessNodeID"));
                resultDT.Columns.Add(new DataColumn("IsApprove"));
                resultDT.Columns.Add(new DataColumn("Expression"));

                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        if (ds.Tables[i].Rows.Count != 0)
                        {
                            DataRow dr = resultDT.NewRow();
                            dr["RequestNodeID"] = ds.Tables[i].Rows[0][0].ToString();
                            dr["ProcessNodeID"] = ds.Tables[i].Rows[0][1].ToString();
                            dr["IsApprove"] = Boolean.Parse(ds.Tables[i].Rows[0][2].ToString() == "1" ? "true" : "false");
                            dr["Expression"] = ds.Tables[i].Rows[0][3].ToString();
                            resultDT.Rows.Add(dr);
                        }
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
                SqlDataReader dr = SQLHelper.ExecuteReader(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);
                if (dr.Read())
                {
                    return dr.GetString(0);
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
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
                SqlDataReader dr = SQLHelper.ExecuteReader(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);
                if (dr.Read())
                {
                    return dr.GetString(0);
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
                DataSet ds = SQLHelper.ExecuteDataset(ConnectionStringTransaction, CommandType.StoredProcedure, sql, parms);
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
        private static string CreateApproveXML(List<Triplet> pairList, string xml)
        {
            //加载传入的XML（申请信息）
            XmlDocument inputXml = new XmlDocument();
            inputXml.LoadXml(xml);

            XmlDocument xDoc = new XmlDocument();
            XmlElement root = xDoc.CreateElement("Approvals");  //创建跟节点
            xDoc.AppendChild(root);

            XmlElement originator = xDoc.CreateElement("Originator");   //创建Originator节点
            originator.SetAttribute("Email", inputXml.SelectSingleNode("Request/OperatorEmail").FirstChild.Value);
            originator.SetAttribute("Tel", inputXml.SelectSingleNode("Request/OperatorTel").FirstChild.Value);
            originator.InnerText = inputXml.SelectSingleNode("Request/OperatorAD").FirstChild.Value;
            root.AppendChild(originator);

            foreach (Triplet p in pairList)
            {
                XmlElement node = xDoc.CreateElement("Approval");
                node.SetAttribute("state", "wait");
                node.SetAttribute("name", "审批节点");
                node.SetAttribute("step", "");
                node.SetAttribute("meetState", "N");
                node.SetAttribute("nodeType", "User");
                node.SetAttribute("NodeID", p.First.ToString());
                node.SetAttribute("URL", p.Third.ToString());  //取得审批URL

                //添加子对象
                string[] users = p.Second.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (users != null && users.Length > 0)
                {
                    foreach (string user in users)
                    {
                        XmlElement child = xDoc.CreateElement("Destinations");
                        DataTable dt = GetUserProfileInfoByAdAccount(user);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            child.SetAttribute("UserNames", dt.Rows[0]["CHName"].ToString());
                            child.SetAttribute("DeptNames", inputXml.SelectSingleNode("Request/DepartmentName").FirstChild.Value);
                            child.SetAttribute("DeptCodes", inputXml.SelectSingleNode("Request/Department").FirstChild.Value);
                            child.SetAttribute("Email", dt.Rows[0]["Email"].ToString());
                            child.SetAttribute("Tel", dt.Rows[0]["CellPhone"].ToString());
                            child.SetAttribute("Gone", "Y");
                            child.InnerText = user;
                        }
                        node.AppendChild(child);
                    }
                }
                root.AppendChild(node);
            }
            return xDoc.InnerXml;
        }
    }
}
