using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Xml;

namespace WS.K2
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://K2Framework.webservice/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class K2WS : System.Web.Services.WebService
    {
        /// <summary>
        /// 获取流程审批节点XML
        /// </summary>
        /// <param name="requestor">申请人AD</param>
        /// <param name="operate">操作人AD</param>
        /// <param name="processName">要获得的流程名称</param>
        /// <param name="groupName">要获得的分组名称</param>
        /// <param name="keyXml">申请节点关键字XML</param>
        /// <param name="data">表达式关键字XML</param>
        /// <returns></returns>
        [WebMethod(Description = "获取审批链")]
        public string GetStartProcessXML(string requestor, string operate, string processName, string groupName, string keyXml, string data, string infoSource)
        {
            try
            {
                DataSet dsr, osr;
                if (requestor.ToLower() == operate.ToLower())
                {
                    dsr = K2DBHelper.GetEmployeeInfoByUserAd(requestor);
                    osr = dsr;
                }
                else
                {
                    dsr = K2DBHelper.GetEmployeeInfoByUserAd(requestor);
                    osr = K2DBHelper.GetEmployeeInfoByUserAd(operate);
                }

                if (dsr.Tables.Count == 0 || osr.Tables.Count == 0)
                {
                    throw new Exception("未取得申请人或操作人信息！");
                    //return string.Empty;
                }

                if (dsr.Tables[0].Rows.Count == 0 || osr.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("未取得申请人或操作人信息！");
                    //return string.Empty;
                }

                RequestDefinition.Request request = new RequestDefinition.Request();
                request.RequestorAD = requestor;
                request.OperatorAD = operate;
                request.Department = dsr.Tables[0].Rows[0]["DepartCode"].ToString();
                request.DepartmentName = dsr.Tables[0].Rows[0]["DepartName"].ToString();
                request.RequestorEmail = dsr.Tables[0].Rows[0]["Email"].ToString();
                request.RequestorTel = dsr.Tables[0].Rows[0]["OfficePhone"].ToString();
                request.WorkSpace = "ALL";      //由于PWorld没有Based地点概念，故此处写为ALL
                request.Operator = osr.Tables[0].Rows[0]["EmployeeName"].ToString();
                request.Requestor = dsr.Tables[0].Rows[0]["EmployeeName"].ToString();
                request.OperatorEmail = osr.Tables[0].Rows[0]["Email"].ToString();
                request.OperatorTel = osr.Tables[0].Rows[0]["OfficePhone"].ToString();

                string xml = SerializationHelper.Serialize(request);
                xml = xml.Insert(xml.IndexOf("</Request>"), keyXml + data);
                string approveNodeXml = WS.K2.K2Rule.GetApproveNodeXML(processName, groupName, xml);
                string retXml = WS.K2.K2Rule.GetApproveXML(requestor, approveNodeXml, xml, infoSource);   //取得最终的xml

                //记录日志
                K2DBHelper.RecordGetApproveXML(requestor, operate, processName, groupName, keyXml, data, retXml);

                return retXml;
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetStartProcessXML", requestor);
                return ex.Message;
            }
        }
        [WebMethod(Description = "获取审批链")]
        public string GetStartProcessXML(string requestor, string operate,string startDeptCode, string processName, string groupName, string keyXml, string data, string infoSource)
        {
            try
            {
                DataSet dsr, osr;
                if (requestor.ToLower() == operate.ToLower())
                {
                    dsr = K2DBHelper.GetEmployeeInfoByUserAd(requestor);
                    osr = dsr;
                }
                else
                {
                    dsr = K2DBHelper.GetEmployeeInfoByUserAd(requestor);
                    osr = K2DBHelper.GetEmployeeInfoByUserAd(operate);
                }

                if (dsr.Tables.Count == 0 || osr.Tables.Count == 0)
                {
                    throw new Exception("未取得申请人或操作人信息！");
                    //return string.Empty;
                }

                if (dsr.Tables[0].Rows.Count == 0 || osr.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("未取得申请人或操作人信息！");
                    //return string.Empty;
                }

                RequestDefinition.Request request = new RequestDefinition.Request();
                request.RequestorAD = requestor;
                request.OperatorAD = operate;
                if (startDeptCode != "")
                {
                    request.Department = startDeptCode;
                }
                else
                {
                    request.Department = dsr.Tables[0].Rows[0]["DepartCode"].ToString();
                }
                request.DepartmentName = dsr.Tables[0].Rows[0]["DepartName"].ToString();
                request.RequestorEmail = dsr.Tables[0].Rows[0]["Email"].ToString();
                request.RequestorTel = dsr.Tables[0].Rows[0]["OfficePhone"].ToString();
                request.WorkSpace = "ALL";      //由于PWorld没有Based地点概念，故此处写为ALL
                request.Operator = osr.Tables[0].Rows[0]["EmployeeName"].ToString();
                request.Requestor = dsr.Tables[0].Rows[0]["EmployeeName"].ToString();
                request.OperatorEmail = osr.Tables[0].Rows[0]["Email"].ToString();
                request.OperatorTel = osr.Tables[0].Rows[0]["OfficePhone"].ToString();

                string xml = SerializationHelper.Serialize(request);
                xml = xml.Insert(xml.IndexOf("</Request>"), keyXml + data);
                string approveNodeXml = WS.K2.K2Rule.GetApproveNodeXML(processName, groupName, xml);
                string retXml = WS.K2.K2Rule.GetApproveXML(requestor, approveNodeXml, xml, infoSource);   //取得最终的xml

                //记录日志
                K2DBHelper.RecordGetApproveXML(requestor, operate, processName, groupName, keyXml, data, retXml);

                return retXml;
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetStartProcessXML", requestor);
                return ex.Message;
            }
        }

        [WebMethod(Description = "得到流程信息")]
        public string GetApproveRuleGroupByprocName(string procName, string group, string user)
        {
            string  processNo = "";
            try
            {
                DataTable dt = WS.K2.K2Rule.GetApproveRuleGroupByProcessID(procName, group);
                if (dt.Rows.Count>0)
                {
                    processNo = dt.Rows[0]["OrderNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetApproveRuleGroupBy", user);
                return ex.Message;
            }
            return processNo.ToString();
        }

        [WebMethod(Description = "得到规则表数据")]
        public DataTable GetApproveRuleGroupData(string ProcessID)
        {
            DataTable dt=null;
            try
            {
                dt = WS.K2.K2Rule.GetApproveRuleGroupDataByProcessID(ProcessID);
                
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt; ;
        }

        [WebMethod(Description = "得到公司列表数据")]
        public DataTable GetAllProcessType()
        {
            DataTable dt = null;
            try
            {
                dt = WS.K2.K2Rule.GetAllProcessType();

            }
            catch (Exception ex)
            {
                return null;
            }
            return dt; ;
        }
        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="processName">流程名称：Project\Process1</param>
        /// <param name="folio">流程流水号</param>
        /// <param name="nvcDataFields">GetStartProcessXML方法返回的XML</param>
        /// <returns>流程实例ID</returns>
        [WebMethod(Description = "发起新流程")]
        public string StartProcess(string procName, string user, string folio, string processApproveChain, string infoSource, string key)
        {
            int ProcInstID = -1;
            try
            {
                CDataFields dataFields = new CDataFields("ProcessApprovalChain", processApproveChain, "XF");

                //发起流程
                ProcInstID = K2Helper.StartProcess(procName, user, folio, dataFields);

                //更新用以标识用户是否可以取回任务的DataField
                K2Helper.UpdateProcessDataFields(ProcInstID, new CDataFields("CallBackProcInstID", ProcInstID.ToString(), "DF"));

                K2DBHelper.AddBusinessInfo(ProcInstID, procName, folio, processApproveChain, user, infoSource, key);
                K2DBHelper.AddApproveLog(ProcInstID, ProcInstID, "Submit", processApproveChain, user);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.StartProcess", user);
                return ex.Message;
            }
            return ProcInstID.ToString();
        }

        /// <summary>
        /// 重新提交流程
        /// </summary>
        /// <param name="sn">流程SN,必选</param>
        /// <param name="action">流程操作,必选
        /// Approve,//同意
        /// Decline,//拒绝
        /// </param>
        /// <param name="currentUser">当前审批人AD账号</param>
        /// <param name="processApproveChain"></param>
        /// <param name="parentProcInstID">主流程ProcInstID</param>
        /// <returns>成功：Success；失败：错误信息</returns>
        [WebMethod(Description = "流程拒绝到申请人后，申请人重新发起流程")]
        //edit by lee 2012-7-13
        //public string ReStartProcess(string sn, string action, string currentUser, string processApproveChain, string infoSource)
        //{
        //    try
        //    {
        //        int parentProcInstID = K2DBHelper.GetRootParentsID(Convert.ToInt32(sn.Split('_')[0]));

        //        CDataFields dataFields;
        //        if (!string.IsNullOrEmpty(processApproveChain))
        //        {
        //            ApprovalChainProcess process = SerializationHelper.Deserialize<ApprovalChainProcess>(processApproveChain);
        //            process.ApprovalChainActivitys[0].ApprovalChainDestination[0].ActionResult = action;
        //            process.ApprovalChainActivitys[0].ApprovalChainDestination[0].ActualUserID = currentUser;
        //            dataFields = new CDataFields("ProcessApprovalChain", SerializationHelper.Serialize(process), "XF");
        //            K2Helper.UpdateProcessDataFields(parentProcInstID, dataFields);
        //        }
        //        else
        //        {
        //            return "Failed! processApproveChain is empty";
        //        }

        //        //获取Approve流程中的Destinations
        //        CDataFields dataFieldDestinations = new CDataFields("Destinations", "", "DF");
        //        K2Helper.GetProcessDataFields(sn, currentUser, dataFieldDestinations);
        //        string xmlDestination = dataFieldDestinations.DataFieldLists[0].Value;
        //        ApprovalChainDestination destinations = SerializationHelper.Deserialize<ApprovalChainDestination>(xmlDestination);
        //        destinations.ActionResult = action;
        //        destinations.ActualUserID = currentUser;
        //        dataFieldDestinations.DataFieldLists[0].Value = SerializationHelper.Serialize(destinations);
        //        K2Helper.ExecuteProcess(sn, "", "", currentUser, dataFieldDestinations);

        //        //更新用以标识用户是否可以取回任务的DataField
        //        K2Helper.UpdateProcessDataFields(parentProcInstID, new CDataFields("CallBackProcInstID", sn.Split('_')[0], "DF"));

        //        K2DBHelper.AddApproveLog(parentProcInstID, Convert.ToInt32(sn.Split('_')[0]), action, processApproveChain, currentUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.ReStartProcess", currentUser);
        //        return ex.Message;
        //    }
        //    return "Success";
        //}
        public string ReStartProcess(string sn, string action, string currentUser, string processApproveChain, string infoSource)
        {
            try
            {
                int parentProcInstID = K2DBHelper.GetRootParentsID(Convert.ToInt32(sn.Split('_')[0]));

                CDataFields dataFields;
                if (!string.IsNullOrEmpty(processApproveChain))
                {
                    ApprovalChainProcess process = SerializationHelper.Deserialize<ApprovalChainProcess>(processApproveChain);
                    process.ApprovalChainActivitys[0].ActionResult = action;
                    process.ApprovalChainActivitys[0].ApprovalChainDestination[0].ActionResult = action;
                    process.ApprovalChainActivitys[0].ApprovalChainDestination[0].ActualUserID = currentUser;
                    process.ApprovalChainActivitys[0].ApprovalChainDestination[0].ApproveChainDestinationUsers[0].Executed = true;
                    process.ApprovalChainActivitys[0].ApprovalChainDestination[0].ApproveChainDestinationUsers[0].ActionResult = action;
                    dataFields = new CDataFields("ProcessApprovalChain", SerializationHelper.Serialize(process), "XF");
                    K2Helper.UpdateProcessDataFields(parentProcInstID, dataFields);
                }
                else
                {
                    return "Failed! processApproveChain is empty";
                }

                //获取Approve流程中的Destinations
                CDataFields dataFieldDestinations = new CDataFields("Destinations", "", "DF");
                K2Helper.GetProcessDataFields(sn, currentUser, dataFieldDestinations);
                string xmlDestination = dataFieldDestinations.DataFieldLists[0].Value;
                ApprovalChainDestination destinations = SerializationHelper.Deserialize<ApprovalChainDestination>(xmlDestination);
                destinations.ActionResult = action;
                destinations.ActualUserID = currentUser;
                dataFieldDestinations.DataFieldLists[0].Value = SerializationHelper.Serialize(destinations);

                //K2Helper.ExecuteProcess(sn, "", "", currentUser, dataFieldDestinations);

                //更新用以标识用户是否可以取回任务的DataField
                K2Helper.UpdateProcessDataFields(parentProcInstID, new CDataFields("CallBackProcInstID", sn.Split('_')[0], "DF"));
                //edit by lee
                K2Helper.ExecuteProcess(sn, "", "", currentUser, dataFieldDestinations);

                K2DBHelper.AddApproveLog(parentProcInstID, Convert.ToInt32(sn.Split('_')[0]), action, processApproveChain, currentUser);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.ReStartProcess", currentUser);
                return ex.Message;
            }
            return "Success";
        }

        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="sn">流程SN,必选</param>
        /// <param name="action">流程操作,必选
        /// Approve,//同意
        /// Decline,//拒绝
        /// </param>
        /// <param name="currentUser">当前审批人AD账号</param>
        /// <param name="activityXML">CreateActivityDetinations方法返回的XML值</param>
        /// <param name="parentProcInstID">主流程ProcInstID</param>
        /// <returns>成功：Success；失败：错误信息</returns>
        [WebMethod(Description = "审批流程")]
        public string ExecuteProcess(string sn, string action, string currentUser, string activityXML)
        {
            try
            {
                int parentProcInstID = K2DBHelper.GetRootParentsID(Convert.ToInt32(sn.Split('_')[0]));

                //获取Approve流程中的Destinations
                CDataFields dataFieldDestinations = new CDataFields("Destinations", "", "DF");
                //获取Approve流程中的节点信息
                dataFieldDestinations.AddDataField(new CDataField("SubApprovalChain", "", "DF"));
                K2Helper.GetProcessDataFields(sn, currentUser, dataFieldDestinations);
                string xmlDestination = dataFieldDestinations.DataFieldLists[0].Value;
                string xmlActivity = dataFieldDestinations.DataFieldLists[1].Value;

                ApprovalChainDestination destinations = SerializationHelper.Deserialize<ApprovalChainDestination>(xmlDestination);

                ApprovalChainActivity activity = SerializationHelper.Deserialize<ApprovalChainActivity>(xmlActivity);
                //权重类型
                string actionWeightType = activity.ActionWeightType;
                if (actionWeightType=="")
                {
                    actionWeightType = "N";
                }

                //如果权重类型是百分比的情况下并且执行的操作不是Approve，则默认操作为Decline
                if (actionWeightType.Equals(EnumActionWeightType.P.ToString(), StringComparison.CurrentCultureIgnoreCase)
                    && !action.Equals("Approve", StringComparison.CurrentCultureIgnoreCase))
                {
                    action = "Decline";
                }

                #region 如果执行的是退回到某个节点的操作
                if (action.Equals("GoBack", StringComparison.CurrentCultureIgnoreCase))
                {
                    //当前审批节点ID
                    string currentActivityID = destinations.ActivityID;
                    //要退回到的节点ID并且是没有权重控制的情况下
                    string targetActivityID = K2DBHelper.GetTargetActivityID(currentActivityID);
                    if (!string.IsNullOrEmpty(targetActivityID)
                        && (actionWeightType.Equals(EnumActionWeightType.N.ToString()) || actionWeightType.Equals(EnumActionWeightType.R.ToString())))
                    {
                        //获取原始审批链
                        string originalProcessApproveChain = K2DBHelper.GetProcessOriginalXMLValue(parentProcInstID);

                        XmlDocument xmlProcess = new XmlDocument();
                        xmlProcess.LoadXml(originalProcessApproveChain);

                        //获取要退回到的节点
                        XmlNode nodeActivity = xmlProcess.SelectSingleNode("/Process/Activity[ID='" + targetActivityID + "' and Status='Completed']");
                        if (nodeActivity != null)
                        {
                            while (true)
                            {
                                nodeActivity.SelectSingleNode("./Status").InnerText = "Available";
                                nodeActivity.SelectSingleNode("./ActionResult").InnerText = "";
                                XmlNodeList nodeListDestinations = nodeActivity.SelectNodes("./Destination");
                                foreach (XmlNode des in nodeListDestinations)
                                {
                                    des.SelectSingleNode("./Status").InnerText = "Available";
                                    des.SelectSingleNode("./ActionResult").InnerText = "";
                                    des.SelectSingleNode("./ActualActionResult").InnerText = "";
                                    //删除加签\会签信息
                                    XmlNode nodeDestinationActivity = des.SelectSingleNode("./Activity");
                                    if (nodeDestinationActivity != null)
                                    {
                                        des.RemoveChild(nodeDestinationActivity);
                                    }
                                }

                                if (nodeActivity.NextSibling != null && nodeActivity.NextSibling.Name.Equals("Activity", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    nodeActivity = nodeActivity.NextSibling;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            //更新原始审批链的状态
                            K2Helper.UpdateProcessDataFields(parentProcInstID, new CDataFields("ProcessApprovalChain", xmlProcess.OuterXml, "XF"));
                        }
                        else
                        {
                            //如果执行的是退回到某个节点的操作，但是没有配置退回到哪个节点，则退回到发起人
                            action = "Decline";
                        }

                    }
                    else
                    {
                        //如果执行的是退回到某个节点的操作，但是没有配置退回到哪个节点，则退回到发起人
                        action = "Decline";
                    }
                }
                #endregion

                //只有在执行同意操作并且(没有权重控制或在权重控制类型为R时)的前提下传递过来的activityXML才做为有效的加签、会签信息
                if (action.Equals("Approve", StringComparison.CurrentCultureIgnoreCase)
                    && !string.IsNullOrEmpty(activityXML)
                    && (actionWeightType.Equals(EnumActionWeightType.N.ToString()) || actionWeightType.Equals(EnumActionWeightType.R.ToString())))
                {
                    destinations.AddActivity(SerializationHelper.Deserialize<ApprovalChainActivity>(activityXML));
                }

                //只有在执行同意操作并且(没有权重控制或在权重控制类型为R时)的前提下传递过来的activityXML才做为有效的加签、会签信息
                //if (action.Equals("Approve", StringComparison.CurrentCultureIgnoreCase)
                //    && !string.IsNullOrEmpty(activityXML)
                //    )
                //{
                //    destinations.AddActivity(SerializationHelper.Deserialize<ApprovalChainActivity>(activityXML));
                //}

                //修改User节点的状态，记录User的Action
                ApprovalChainDestinationUser user = destinations.ApproveChainDestinationUsers.Find(d => d.Account.Equals(currentUser, StringComparison.CurrentCultureIgnoreCase));
                if (user != null)
                {
                    user.Executed = true;
                    user.ActionResult = action;
                }

                destinations.ActionResult = action;
                destinations.ActualUserID += ";" + currentUser;

                dataFieldDestinations = new CDataFields("Destinations", "", "DF");
                dataFieldDestinations.DataFieldLists[0].Value = SerializationHelper.Serialize(destinations);

                //  K2Helper.ExecuteProcess(sn, "", "", currentUser, dataFieldDestinations);

                //更新用以标识用户是否可以取回任务的DataField
                K2Helper.UpdateProcessDataFields(parentProcInstID, new CDataFields("CallBackProcInstID", sn.Split('_')[0], "DF"));
                //edit by lee
                K2Helper.ExecuteProcess(sn, "", "", currentUser, dataFieldDestinations);

                K2DBHelper.AddApproveLog(parentProcInstID, Convert.ToInt32(sn.Split('_')[0]), action, activityXML, currentUser);

                K2DBHelper.AddActSlot(Convert.ToInt32(sn.Split('_')[0]), parentProcInstID, activity.ID, activity.Name, currentUser, activity.Type);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.ExecuteProcess(SN=" + sn + ";action=" + action + ";currentUser=" + currentUser + ";activityXML=" + activityXML + ")", currentUser);
                return ex.Message;
            }
            return "Success";
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="activityName">节点名称,可以为空</param>
        /// <param name="order">节点是顺序审批还是非顺序审批，不可为空</param>
        /// <param name="webUrl">在该节点使用的审批页面的Url：http://localhost:3373/Approve.aspx?SN= ，可以为空</param>
        /// <param name="type">节点类型，不可为空
        /// JQB,//前加签
        /// JQA,//后加签
        /// HQ,//会签
        /// </param>
        /// <returns>只有节点信息的XML</returns>
        [WebMethod(Description = "创建节点")]
        public string CreateActivity(string activityName, bool order, string webUrl, string type, string actionWeightType, string actionWeight)
        {
            ApprovalChainActivity activity = new ApprovalChainActivity(Guid.NewGuid().ToString(), activityName, order, webUrl, type, actionWeightType, actionWeight);
            return SerializationHelper.Serialize(activity);
        }

        /// <summary>
        /// 给节点添加审批人
        /// </summary>
        /// <param name="activityXML">CreateActivity方法返回的节点的XML</param>
        /// <param name="userID">User ID，可以为空</param>
        /// <param name="account">用户AD 账号，不可为空</param>
        /// <param name="userName">用户姓名，可以为空</param>
        /// <param name="email">用户邮件，如果有邮件提醒，则不可为空</param>
        /// <param name="userType">Destination 类型
        /// User,//用户
        /// </param>
        /// <returns>完整的节点信息XML</returns>
        [WebMethod(Description = "给节点添加审批人")]
        public string CreateActivityDetinations(string activityXML, string userID, string account, string userName, string email, string userType)
        {
            ApprovalChainActivity activity = SerializationHelper.Deserialize<ApprovalChainActivity>(activityXML);
            ApprovalChainDestination originator = new ApprovalChainDestination(userID, account, userName, email, userType);
            activity.AddDestination(originator);
            return SerializationHelper.Serialize(activity);
        }

        /// <summary>
        /// 获取当前节点下审批人信息
        /// </summary>
        /// <param name="sn">流程SN</param>
        /// <param name="currentUser">当前审批人</param>
        /// <returns>Destination节的信息</returns>
        [WebMethod(Description = "获取当前审批人信息")]
        public string GetApproverInfo(string sn, string currentUser)
        {
            try
            {
                //更新用以标识用户是否可以取回任务的DataField
                int parentProcInstID = K2DBHelper.GetRootParentsID(Convert.ToInt32(sn.Split('_')[0]));
                K2Helper.UpdateProcessDataFields(parentProcInstID, new CDataFields("CallBackProcInstID", "0", "DF"));

                //获取Approve流程中的Destinations
                CDataFields dataFieldLists = new CDataFields("Destinations", "", "DF");
                K2Helper.GetProcessDataFields(sn, currentUser, dataFieldLists);
                return dataFieldLists.DataFieldLists[0].Value;
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetApproverInfo", currentUser);
                return null;
            }
        }

        /// <summary>
        /// 获取当前节点信息
        /// </summary>
        /// <param name="sn">流程SN</param>
        /// <param name="currentUser">当前审批人</param>
        /// <returns>节点的信息</returns>
        [WebMethod(Description = "获取当前节点信息")]
        public string GetCurrentActivityInfo(string sn, string currentUser)
        {
            try
            {
                //更新用以标识用户是否可以取回任务的DataField
                int parentProcInstID = K2DBHelper.GetRootParentsID(Convert.ToInt32(sn.Split('_')[0]));
                K2Helper.UpdateProcessDataFields(parentProcInstID, new CDataFields("CallBackProcInstID", "0", "DF"));

                //获取Approve流程中的节点信息
                CDataFields dataFieldLists = new CDataFields("SubApprovalChain", "", "DF");
                K2Helper.GetProcessDataFields(sn, currentUser, dataFieldLists);
                ApprovalChainActivity activity = SerializationHelper.Deserialize<ApprovalChainActivity>(dataFieldLists.DataFieldLists[0].Value);
                activity.ApprovalChainDestination = null;
                return SerializationHelper.Serialize(activity);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetCurrentActivityInfo", currentUser);
                return null;
            }
        }

        /// <summary>
        /// 获取当前节点下审批人信息
        /// </summary>
        /// <param name="sn">流程SN</param>
        /// <param name="currentUser">当前审批人</param>
        /// <param name="parentProcInstID">主流程ID</param>
        /// <returns>Process节的信息</returns>
        [WebMethod(Description = "流程拒绝到申请人后，通过该方法获取整个审批链")]
        public string GetOriginalApproveChain(string sn, string currentUser)
        {
            try
            {
                //更新用以标识用户是否可以取回任务的DataField
                int parentProcInstID = K2DBHelper.GetRootParentsID(Convert.ToInt32(sn.Split('_')[0]));
                K2Helper.UpdateProcessDataFields(parentProcInstID, new CDataFields("CallBackProcInstID", "0", "DF"));

                //获取Approve流程中的Destinations
                CDataFields dataFieldLists = new CDataFields("ProcessApprovalChain", "", "XF");
                K2Helper.GetProcessDataFields(sn, currentUser, dataFieldLists);
                return dataFieldLists.DataFieldLists[0].Value;
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetOriginalApproveChain", currentUser);
                return null;
            }
        }

        /// <summary>
        /// Get WorkList
        /// </summary>
        /// <param name="pagenum">分页中的第几页</param>
        /// <param name="pagesize">每页显示记录数</param>
        /// <param name="procFullName">流程名称，过滤条件，可以为空</param>
        /// <param name="actionerName">任务所属人员AD账号，不可为空</param>
        /// <param name="folio">流水号，过滤条件，可以为空</param>
        /// <returns></returns>
        [WebMethod(Description = "获取待办任务列表")]
        public string GetWorkList(string actionerName, string folio, string source, string applicant)
        {
            try
            {
                return K2DBHelper.GetWorkList(actionerName, folio, source, applicant);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetWorkList", actionerName);
                throw ex;
            }
        }

        /// <summary>
        /// 获取我发起的任务
        /// </summary>
        /// <param name="actionerName">发起人员AD账号，不可为空</param>
        /// <param name="folio">流水号，过滤条件，可以为空</param>
        /// <param name="startTime">开始时间，过滤条件，可以为空</param>
        /// <param name="endTime">结束时间，过滤条件，可以为空</param>
        /// <param name="pagenum">分页中的第几页</param>
        /// <param name="pagesize">每页显示记录数</param>
        /// <param name="procFullName">流程名称，过滤条件，可以为空</param>
        /// <returns></returns>
        [WebMethod(Description = "获取已发起任务列表")]
        public string GetMyApplication(string actionerName, string folio, string startTime, string endTime, string source)
        {
            try
            {
                return K2DBHelper.GetMyApplication(actionerName, folio, startTime, endTime, source);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetMyApplication", actionerName);
                throw ex;
            }
        }

        /// <summary>
        /// 获取已办任务
        /// </summary>
        /// <param name="actionerName">处理人员AD账号，不可为空</param>
        /// <param name="folio">流水号，过滤条件，可以为空</param>
        /// <param name="startTime">开始时间，过滤条件，可以为空</param>
        /// <param name="endTime">结束时间，过滤条件，可以为空</param>
        /// <param name="pagenum">分页中的第几页</param>
        /// <param name="pagesize">每页显示记录数</param>
        /// <param name="procFullName">流程名称，过滤条件，可以为空</param>
        /// <param name="submitor">发起人员AD账号，过滤条件，可以为空</param>
        /// <returns></returns>
        [WebMethod(Description = "获取已处理任务列表")]
        public string GetMyJoined(string actionerName, string folio, string startTime, string endTime, string applicant, string source)
        {
            try
            {
                return K2DBHelper.GetMyJoined(actionerName, folio, startTime, endTime, applicant, source);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetMyJoined", actionerName);
                throw ex;
            }
        }

        /// <summary>
        /// 获取最新审批链信息
        /// </summary>
        /// <param name="ProcInstID">主流程实例ID</param>
        /// <returns>流程最新的XML</returns>
        [WebMethod(Description = "获取最新的审批链")]
        public string GetProcessCurrentXMLValue(int parentProcInstID)
        {
            try
            {
                return K2DBHelper.GetProcessCurrentXMLValue(parentProcInstID);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetProcessCurrentXMLValue", "");
                return null;
            }
        }

        /// <summary>
        /// 获取原始审批链信息
        /// </summary>
        /// <param name="ProcInstID">主流程实例ID</param>
        /// <returns>原始审批链信息XML</returns>
        [WebMethod(Description = "获取原始的审批链，如果有跳单操作时，使用该方法获取原始审批链")]
        public string GetProcessOriginalXMLValue(int parentProcInstID)
        {
            try
            {
                return K2DBHelper.GetProcessOriginalXMLValue(parentProcInstID);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetProcessOriginalXMLValue", "");
                return null;
            }
        }

        [WebMethod(Description = "删除审批节点")]
        public string DeleteActivity(string processApproveChainXML, string activityID)
        {
            ApprovalChainProcess process = SerializationHelper.Deserialize<ApprovalChainProcess>(processApproveChainXML);
            process.RemoveActivity(process.ApprovalChainActivitys.Find(a => a.ID.Equals(activityID, StringComparison.CurrentCultureIgnoreCase) && a.Status.Equals("Available", StringComparison.CurrentCultureIgnoreCase)));

            return SerializationHelper.Serialize(process);
        }

        /// <summary>
        /// 更新流程变量
        /// </summary>
        /// <param name="parentrocInstID">主流程实例ID</param>
        /// <param name="processApproveChain">要更新的审批链</param>
        [WebMethod(Description = "跳单审批时，使用该方法将删除审批节点后的审批链更新，在ExecuteProcess方法前执行")]
        public string UpdateProcessApproveChain(int parentrocInstID, string processApproveChain)
        {
            try
            {
                CDataFields dataFields = new CDataFields("ProcessApprovalChain", processApproveChain, "XF");

                K2Helper.UpdateProcessDataFields(parentrocInstID, dataFields);
                return "Sucess";
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.UpdateProcessApproveChain", "");
                return ex.Message;
            }
        }

        [WebMethod(Description = "供测试用")]
        public void AddTemp(string type, string folio)
        {
            try
            {
                K2DBHelper.AddTemp(folio, type);
            }
            catch (Exception ex)
            {
                K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.AddTemp", "");
                throw ex;
            }
        }
        #region 暂时没有使用到以下方法，请不要删除
        ///// <summary>
        ///// 任务移交
        ///// </summary>
        ///// <param name="sn">任务SN</param>
        ///// <param name="sourceUser">源用户</param>
        ///// <param name="targetUser">目标用户</param>
        //[WebMethod]
        //public string RedirectWorkListItem(string sn, string sourceUser, string targetUser)
        //{
        //    try
        //    {
        //        K2Helper.RedirectWorkListItem(sn, sourceUser, targetUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.RedirectWorkListItem", sourceUser);
        //        return ex.Message;
        //    }
        //    return "Success";
        //}

        ///// <summary>
        ///// 更新流程变量
        ///// </summary>
        ///// <param name="sn">流程SN</param>
        ///// <param name="nvcDataFields">要更新的变量</param>
        //[WebMethod(MessageName = "UpdateProcessDataFieldsBySN")]
        //public string UpdateProcessDataFieldsBySN(string sn, string currentUser, string dataFields)
        //{
        //    try
        //    {
        //        K2Helper.UpdateProcessDataFields(sn, currentUser, SerializationHelper.Deserialize<CDataFields>(dataFields));
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.UpdateProcessDataFieldsBySN", currentUser);
        //        return ex.Message;
        //    }
        //    return "Success";
        //}

        ///// <summary>
        ///// 更新流程变量
        ///// </summary>
        ///// <param name="sn">流程实例ID</param>
        ///// <param name="nvcDataFields">要更新的变量</param>
        //[WebMethod(MessageName = "UpdateProcessDataFieldsByInstID")]
        //public string UpdateProcessDataFieldsByInstID(int procInstID, string dataFields)
        //{
        //    try
        //    {
        //        K2Helper.UpdateProcessDataFields(procInstID, SerializationHelper.Deserialize<CDataFields>(dataFields));
        //        return "Sucess";
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.UpdateProcessDataFieldsByInstID", "");
        //        return ex.Message;
        //    }
        //}

        ///// <summary>
        ///// 获取流程变量
        ///// </summary>
        ///// <param name="sn"></param>
        ///// <param name="FieldNames"></param>
        ///// <returns></returns>
        //[WebMethod(MessageName = "GetProcessDataFieldsBySN")]
        //public string GetProcessDataFieldsBySN(string sn, string currentUser, string dataFields)
        //{
        //    try
        //    {
        //        //更新用以标识用户是否可以取回任务的DataField
        //        int parentProcInstID = K2DBHelper.GetRootParentsID(Convert.ToInt32(sn.Split('_')[0]));
        //        K2Helper.UpdateProcessDataFields(parentProcInstID, new CDataFields("CallBackProcInstID", "0", "DF"));

        //        CDataFields dataFieldLists = SerializationHelper.Deserialize<CDataFields>(dataFields);
        //        K2Helper.GetProcessDataFields(sn, currentUser, dataFieldLists);
        //        return SerializationHelper.Serialize(dataFieldLists);
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetProcessDataFieldsBySN", currentUser);
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 更新流程变量
        ///// </summary>
        ///// <param name="sn">流程实例ID</param>
        ///// <param name="nvcDataFields">要获取的变量的List</param>
        ///// <returns>变量的List</returns>
        //[WebMethod(MessageName = "GetProcessDataFieldsByInstID")]
        //public string GetProcessDataFieldsByInstID(int procInstID, string dataFields)
        //{
        //    try
        //    {
        //        //更新用以标识用户是否可以取回任务的DataField
        //        int parentProcInstID = K2DBHelper.GetRootParentsID(procInstID);
        //        K2Helper.UpdateProcessDataFields(parentProcInstID, new CDataFields("CallBackProcInstID", "0", "DF"));

        //        CDataFields dataFieldLists = SerializationHelper.Deserialize<CDataFields>(dataFields);
        //        K2Helper.GetProcessDataFields(procInstID, dataFieldLists);
        //        return SerializationHelper.Serialize(dataFieldLists);
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.GetProcessDataFieldsByInstID", "");
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 恢复任务
        ///// </summary>
        ///// <param name="sn">任务SN</param>
        ///// <param name="currentUser"></param>
        //[WebMethod]
        //public string ReleaseWorkListItem(string sn, string currentUser)
        //{
        //    try
        //    {
        //        K2Helper.ReleaseWorkListItem(sn, currentUser);
        //        return "Sucess";
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.ReleaseWorkListItem", currentUser);
        //        return ex.Message;
        //    }
        //}

        ///// <summary>
        ///// 委托代办
        ///// </summary>
        ///// <param name="sn"></param>
        ///// <param name="sourceUser"></param>
        ///// <param name="targetUser"></param>
        //[WebMethod]
        //public string DelegateWorkListItem(string sn, string sourceUser, string targetUser)
        //{
        //    try
        //    {
        //        return K2Helper.DelegateWorkListItem(sn, sourceUser, targetUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.DelegateWorkListItem", sourceUser);
        //        return ex.Message;
        //    }
        //}

        ///// <summary>
        ///// 挂起
        ///// </summary>
        ///// <param name="sn"></param>
        ///// <param name="sourceUser"></param>
        ///// <param name="second"></param>
        //[WebMethod]
        //public string SleepWorkListItem(string sn, string sourceUser, int second)
        //{
        //    try
        //    {
        //        K2Helper.SleepWorkListItem(sn, sourceUser, second);
        //        return "Sucess";
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.SleepWorkListItem", sourceUser);
        //        throw ex;
        //    }
        //}       

        ///// <summary>
        ///// 撤消任务
        ///// </summary>
        ///// <param name="parentProcInstID">主流程实例ID</param>
        //[WebMethod]
        //public string CancelWorkListItem(int parentProcInstID)
        //{
        //    try
        //    {
        //        // 获取主流程的所有活动的子流程ID
        //        string procInstIDs = K2DBHelper.GetActiveChildrens(parentProcInstID);
        //        //将所有流程实例都Goto到结束节点
        //        K2Helper.CancelProcInstance(procInstIDs);
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.CancelWorkListItem", "");
        //        return ex.Message;
        //    }
        //    return "Success";
        //}

        ///// <summary>
        ///// 驳回到任意一个节点
        ///// </summary>
        ///// <param name="parentProcInstID">主流程实例ID</param>
        ///// <param name="currentProcInstID">任务的ID</param>
        ///// <param name="activityName">要驳回到的主流程的节点</param>
        //[WebMethod]
        //public string RejectWorkListItem(int parentProcInstID, string activityName)
        //{
        //    try
        //    {
        //        //获取主流程的目前所有活动的子流程ID
        //        string procInstIDs = K2DBHelper.GetActiveChildrens(parentProcInstID);

        //        //将主流程Goto到指定的节点
        //        K2Helper.GotoActivity(parentProcInstID, activityName);

        //        //将主流程Goto之前存在的子流程Goto到结束节点
        //        if (procInstIDs.IndexOf('_') != -1)
        //        {
        //            K2Helper.CancelProcInstance(procInstIDs.Substring(procInstIDs.IndexOf('_') + 1));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.RejectWorkListItem", "");
        //        return ex.Message;
        //    }
        //    return "Success";
        //}

        ///// <summary>
        ///// 任务取回
        ///// </summary>
        ///// <param name="parentProcInstID">主流程实例ProcInstID</param>
        ///// <param name="currentProcInstID">当前流程实例ProcInstId</param>
        ///// <param name="activityName">取回到哪个节点</param>
        ///// <returns>取回结果信息</returns>
        //[WebMethod]
        //public string CallBackWorkListItem(int parentProcInstID, int currentProcInstID, string activityName)
        //{
        //    try
        //    {
        //        CDataFields dataFieldLists = new CDataFields("CallBackProcInstID", "0", "DF");

        //        //获取用以标识用户是否可以取回任务的DataField：CallBackProcInstID
        //        K2Helper.GetProcessDataFields(parentProcInstID, dataFieldLists);

        //        //如果CallBackProcInstID中的值和当前流程实例的ProcInstID相等，则表示可以召回
        //        if (Convert.ToInt32(dataFieldLists.DataFieldLists[0].Value) == currentProcInstID)
        //        {
        //            return this.RejectWorkListItem(parentProcInstID, activityName);
        //        }
        //        //如果CallBackProcInstID中的值等于0，则表示该任务已经被下一个审批人查看
        //        else if (Convert.ToInt32(dataFieldLists.DataFieldLists[0].Value) == 0)
        //        {
        //            return "下一个审批人已经查看任务";
        //        }
        //        //如果CallBackProcInstID中的值和当前流程实例的ProcInstID不相等，则表示该任务已经被下一个审批人审批
        //        else if (Convert.ToInt32(dataFieldLists.DataFieldLists[0].Value) != currentProcInstID)
        //        {
        //            return "下一个审批人已经完成任务审批";
        //        }
        //        else
        //        {
        //            return "Failure";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.CallBackWorkListItem", "");
        //        return ex.Message;
        //    }
        //}

        ///// <summary>
        ///// 重新提交流程，可以使用ExecuteProcess方法替代该方法
        ///// </summary>
        ///// <param name="parentProcInstID">主流程实例ID</param>
        ///// <param name="activityName">重新提交到的节点</param>
        ///// <param name="dataFields">需要更新的变量</param>
        ///// <returns>提交结果</returns>
        //[WebMethod]
        //public string ReSubmit(int parentProcInstID, string activityName, string dataFields)
        //{
        //    try
        //    {
        //        //更新用以标识用户是否可以取回任务的DataField
        //        CDataFields dataFieldLists = SerializationHelper.Deserialize<CDataFields>(dataFields);
        //        CDataField df = new CDataField();
        //        df.Key = "CallBackProcInstID";
        //        df.Value = parentProcInstID.ToString();
        //        df.Type = "DF";
        //        dataFieldLists.AddDataField(df);

        //        K2Helper.UpdateProcessDataFields(parentProcInstID, dataFieldLists);

        //        K2Helper.GotoActivity(parentProcInstID, activityName);
        //    }
        //    catch (Exception ex)
        //    {
        //        K2DBHelper.RecoreErrorProfile(ex, "K2HelperWS.ReSubmit", "");
        //        return ex.Message;
        //    }
        //    return "Success";
        //}

        ///// <summary>
        ///// 供代理流程服务使用
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public string DelegateWorkList()
        //{
        //    try
        //    {
        //        K2Helper.DelegateWorkList();
        //        return "Sucess";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        #endregion
    }
}