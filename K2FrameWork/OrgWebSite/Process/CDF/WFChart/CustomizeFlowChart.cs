using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using System.Collections;
using K2Utility;
using Model;
using BLL;
using K2.Controls;

namespace OrgWebSite.Process.CDF.WFChart
{
    public class CustomizeFlowChart : BasePage
    {
        /// <summary>
        /// Json->XML
        /// </summary>
        /// <param name="flowChartJson"></param>
        /// <returns></returns>
        public string FlowChartJsonToXml(string flowChartJson, string opratorcode)
        {
            return string.Empty;
            try
            {
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                object[] flowList = (object[])(jsonSerializer.DeserializeObject(flowChartJson));
                object nodeId, ftitle, isChekced, isCounterSign, nodeState, nodeType, rolecode, rolename, username, usercode, deptname, deptcode, arrayCounter;    //保存反序列化后的值
                ArrayList FlowChartList = new ArrayList();
                bool isReview = false;

                ////判断是否是代申请
                if (!string.IsNullOrEmpty(opratorcode) && !string.IsNullOrEmpty(EmployeeCode) && !opratorcode.Equals(EmployeeCode, StringComparison.OrdinalIgnoreCase))
                    isReview = true;

                if (flowList != null)
                {
                    //产生CustomizeFlowInfo类
                    for (int i = 0; i < flowList.Length; i++)
                    {
                        Dictionary<string, object> obj = (Dictionary<string, object>)(flowList[i]);
                        obj.TryGetValue("nodeId", out nodeId);
                        obj.TryGetValue("ftitle", out ftitle);
                        obj.TryGetValue("isChekced", out isChekced);
                        obj.TryGetValue("isCounterSign", out isCounterSign);
                        obj.TryGetValue("nodeState", out nodeState);
                        obj.TryGetValue("username", out username);
                        obj.TryGetValue("usercode", out usercode);
                        obj.TryGetValue("deptname", out deptname);
                        obj.TryGetValue("deptcode", out deptcode);
                        obj.TryGetValue("arrayCounter", out arrayCounter);
                        obj.TryGetValue("nodeType", out nodeType);
                        obj.TryGetValue("roleCode", out rolecode);
                        obj.TryGetValue("roleName", out rolename);


                        CustomizeFlowInfo flowInfo = new CustomizeFlowInfo();
                        flowInfo.ftitle = ftitle.ToString();
                        flowInfo.isChekced = Convert.ToBoolean(isChekced);
                        flowInfo.nodeType = nodeType.ToString();
                        if (!flowInfo.nodeType.Equals("User") && rolecode != null && rolename != null)
                        {
                            flowInfo.roleCode = rolecode.ToString();
                            flowInfo.roleName = rolename.ToString();
                        }
                        else
                        {
                            flowInfo.roleCode = "";
                            flowInfo.roleName = "";
                        }
                        flowInfo.isCounterSign = Convert.ToBoolean(isCounterSign);
                        flowInfo.nodeState = Convert.ToInt32(nodeState);
                        flowInfo.username = username.ToString();
                        flowInfo.usercode = usercode.ToString();
                        flowInfo.deptcode = deptcode.ToString();
                        flowInfo.deptname = deptname.ToString();
                        flowInfo.arrayCounter = arrayCounter as Array;
                        FlowChartList.Add(flowInfo);
                    }

                    //生成XML
                    if (FlowChartList != null && FlowChartList.Count != 0)
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlElement xmlEle;
                        XmlElement childEleRoot;            //根子节点(Approval)
                        XmlElement childEle;                //子节点

                        //创建根节点
                        xmlEle = xmlDoc.CreateElement("Approvals");
                        xmlDoc.AppendChild(xmlEle);

                        //Biz_Employee empolyeeDac = new Biz_Employee();
                        UserProfileBLL empolyeeDac = new UserProfileBLL();
                        UserProfileInfo empolyee = empolyeeDac.GetUserProfileByADAccount(opratorcode);

                        if (empolyee != null)
                        {
                            //创建提交人节点
                            childEleRoot = xmlDoc.CreateElement("Originator");
                            xmlEle.AppendChild(childEleRoot);
                            childEleRoot.SetAttribute("Email", empolyee.Email);
                            childEleRoot.SetAttribute("Tel", empolyee.BlackBerry);
                            childEleRoot.InnerText = empolyee.ID.ToString();

                            if (isReview)
                            {
                                //创建子节点
                                childEleRoot = xmlDoc.CreateElement("Approval");
                                xmlEle.AppendChild(childEleRoot);
                                childEleRoot.SetAttribute("state", "wait");
                                childEleRoot.SetAttribute("name", string.Empty);       //Title属性
                                childEleRoot.SetAttribute("step", string.Empty);
                                childEleRoot.SetAttribute("meetState", "N");
                                childEleRoot.SetAttribute("nodeType", "User");
                                childEleRoot.SetAttribute("RoleName", string.Empty);
                                childEleRoot.SetAttribute("RoleCode", string.Empty);
                                childEleRoot.SetAttribute("IsConfirm", "Y");
                                childEle = xmlDoc.CreateElement("Destinations");    //节点人标签
                                childEleRoot.AppendChild(childEle);
                                empolyee = empolyeeDac.GetUserProfileByADAccount(opratorcode);
                                childEle.InnerText = opratorcode;
                                if (string.IsNullOrEmpty(empolyee.Email))
                                    childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                else
                                    childEle.SetAttribute("Email", empolyee.Email);

                                if (string.IsNullOrEmpty(empolyee.BlackBerry))
                                    childEle.SetAttribute("Tel", "");
                                else
                                    childEle.SetAttribute("Tel", empolyee.BlackBerry);

                                if (!string.IsNullOrEmpty(empolyee.CHName))
                                    childEle.SetAttribute("UserNames", empolyee.CHName);

                                List<UserDepartmentInfo> udiList = empolyeeDac.GetDepartmentByUserID(empolyee.ID.ToString());
                                UserDepartmentInfo info = udiList.Find(delegate(UserDepartmentInfo tmp)
                                {
                                    if (tmp.IsMain)
                                        return true;
                                    else
                                        return false;
                                });
                                if (info != null)
                                {
                                    childEle.SetAttribute("DeptNames", info.DeptName);
                                    childEle.SetAttribute("DeptCodes", info.DeptCode.ToString());
                                }
                                else
                                {
                                    childEle.SetAttribute("DeptNames", string.Empty);
                                    childEle.SetAttribute("DeptCodes", string.Empty);
                                }
                            }

                            foreach (CustomizeFlowInfo cfi in FlowChartList)
                            {
                                //创建子节点
                                childEleRoot = xmlDoc.CreateElement("Approval");
                                xmlEle.AppendChild(childEleRoot);

                                //状态属性
                                if (cfi.nodeState == 0)
                                    childEleRoot.SetAttribute("state", "complete");
                                else if (cfi.nodeState == 1)
                                    childEleRoot.SetAttribute("state", "current");
                                else
                                    childEleRoot.SetAttribute("state", "wait");

                                childEleRoot.SetAttribute("nodeType", cfi.nodeType);

                                if (!cfi.nodeType.Equals("User"))
                                {
                                    childEleRoot.SetAttribute("RoleName", cfi.roleName);
                                    childEleRoot.SetAttribute("RoleCode", cfi.roleCode);
                                }
                                else
                                {
                                    childEleRoot.SetAttribute("RoleName", string.Empty);
                                    childEleRoot.SetAttribute("RoleCode", string.Empty);
                                }

                                childEleRoot.SetAttribute("name", cfi.ftitle);       //Title属性
                                childEleRoot.SetAttribute("step", string.Empty);

                                //取得该节点所有的审批人
                                string[] destinations;
                                if (cfi.nodeType.Equals("User"))
                                    destinations = cfi.usercode.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                //else
                                //    destinations = new Biz_T_SubClass().GetUserCodeByRoleCode(cfi.roleCode).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);      //角色的情况
                                //for (int i = 0; i < destinations.Length; i++)
                                //{
                                //    childEle = xmlDoc.CreateElement("Destinations");    //节点人标签
                                //    childEleRoot.AppendChild(childEle);

                                //    empolyee = empolyeeDac.GetEmployeeByEmployeeCode(destinations[i]);
                                //    childEle.InnerText = destinations[i];

                                //    //姓名
                                //    if (!string.IsNullOrEmpty(empolyee.EmployeeName))
                                //        childEle.SetAttribute("UserNames", empolyee.EmployeeName);

                                //    // 部门名称
                                //    if (!string.IsNullOrEmpty(empolyee.FirstDeptName) && !string.IsNullOrEmpty(empolyee.SecondDeptName))
                                //        childEle.SetAttribute("DeptNames", empolyee.FirstDeptName + "." + empolyee.SecondDeptName);
                                //    else if (!string.IsNullOrEmpty(empolyee.FirstDeptName))
                                //        childEle.SetAttribute("DeptNames", empolyee.FirstDeptName);
                                //    else if (!string.IsNullOrEmpty(empolyee.SecondDeptName))
                                //        childEle.SetAttribute("DeptNames", empolyee.SecondDeptName);

                                //    //部门代码
                                //    if (!string.IsNullOrEmpty(empolyee.FirstDeptCode) && !string.IsNullOrEmpty(empolyee.SecondDeptCode))
                                //        childEle.SetAttribute("DeptCodes", empolyee.FirstDeptCode + "." + empolyee.SecondDeptCode);
                                //    else if (!string.IsNullOrEmpty(empolyee.FirstDeptCode))
                                //        childEle.SetAttribute("DeptCodes", empolyee.FirstDeptCode);
                                //    else if (!string.IsNullOrEmpty(empolyee.SecondDeptCode))
                                //        childEle.SetAttribute("DeptCodes", empolyee.SecondDeptCode);

                                //    if (string.IsNullOrEmpty(empolyee.Email))
                                //        childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                //    else
                                //        childEle.SetAttribute("Email", empolyee.Email);

                                //    if (string.IsNullOrEmpty(empolyee.Mobile_Phone))
                                //        childEle.SetAttribute("Tel", "");
                                //    else
                                //        childEle.SetAttribute("Tel", empolyee.Mobile_Phone);
                                //}

                                //会签
                                if (cfi.arrayCounter.Length != 0)
                                {
                                    bool isSign = false;        //标示是否有新会签

                                    object counterSignCodes, counterSignNames, counterDeptName, counterDeptCode, signEnabled;
                                    object[] tmpCounterSigns = new object[cfi.arrayCounter.Length];                            //用户保存抽象的Array
                                    CustomizeCounterSign[] resultSign = new CustomizeCounterSign[cfi.arrayCounter.Length];       //保存读取后的结果
                                    cfi.arrayCounter.CopyTo(tmpCounterSigns, 0);                                               //将Array中的内容Copy到tmpCounterSigns中
                                    for (int j = 0; j < tmpCounterSigns.Length; j++)
                                    {
                                        //读取内容
                                        Dictionary<string, object> counter = (Dictionary<string, object>)(tmpCounterSigns[j]);
                                        CustomizeCounterSign tmpSign = new CustomizeCounterSign();
                                        counter.TryGetValue("counterSignCodes", out counterSignCodes);
                                        counter.TryGetValue("counterSignNames", out counterSignNames);
                                        counter.TryGetValue("counterDeptName", out counterDeptName);
                                        counter.TryGetValue("counterDeptCode", out counterDeptCode);
                                        counter.TryGetValue("enabled", out signEnabled);

                                        //将结果依次保存到数组中
                                        tmpSign.counterSignCodes = counterSignCodes.ToString();
                                        tmpSign.counterSignNames = counterSignNames.ToString();
                                        tmpSign.counterDeptName = counterDeptName.ToString();
                                        tmpSign.counterDeptCode = counterDeptCode.ToString();
                                        tmpSign.enabled = Convert.ToBoolean(signEnabled);

                                        childEle = xmlDoc.CreateElement("Meet");        //创建会签人结点
                                        childEleRoot.AppendChild(childEle);
                                        empolyee = empolyeeDac.GetUserProfileByADAccount(tmpSign.counterSignCodes);
                                        if (tmpSign.enabled)        //存在新的会签
                                        {
                                            childEle.SetAttribute("state", "A");
                                            if (string.IsNullOrEmpty(empolyee.Email))
                                                childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                            else
                                                childEle.SetAttribute("Email", empolyee.Email);

                                            if (string.IsNullOrEmpty(empolyee.BlackBerry))
                                                childEle.SetAttribute("Tel", "");
                                            else
                                                childEle.SetAttribute("Tel", empolyee.BlackBerry);
                                            isSign = true;
                                        }
                                        else
                                        {
                                            childEle.SetAttribute("state", "C");
                                            if (string.IsNullOrEmpty(empolyee.Email))
                                                childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                            else
                                                childEle.SetAttribute("Email", empolyee.Email);

                                            if (string.IsNullOrEmpty(empolyee.BlackBerry))
                                                childEle.SetAttribute("Tel", "");
                                            else
                                                childEle.SetAttribute("Tel", empolyee.BlackBerry);
                                        }

                                        childEle.InnerText = empolyee.ID.ToString();
                                    }

                                    if (isSign)         //是否有新会签属性
                                        childEleRoot.SetAttribute("meetState", "Y");
                                    else
                                        childEleRoot.SetAttribute("meetState", "N");
                                }
                                else
                                {
                                    childEleRoot.SetAttribute("meetState", "N");
                                }
                            }
                            return xmlDoc.InnerXml;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogUtil.Log.Error(ex.Message);
                return null;
            }
        }

        public string FlowChartJsonToXml_Approve(string flowChartJson, string opratorcode)
        {
            return string.Empty;
            try
            {
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                object[] flowList = (object[])(jsonSerializer.DeserializeObject(flowChartJson));
                object nodeId, ftitle, isChekced, isCounterSign, nodeState, nodeType, rolecode, rolename, username, usercode, deptname, deptcode, arrayCounter,url;    //保存反序列化后的值
                ArrayList FlowChartList = new ArrayList();
                bool isReview = false;

                if (flowList != null)
                {
                    //产生CustomizeFlowInfo类
                    for (int i = 0; i < flowList.Length; i++)
                    {
                        Dictionary<string, object> obj = (Dictionary<string, object>)(flowList[i]);
                        obj.TryGetValue("nodeId", out nodeId);
                        obj.TryGetValue("ftitle", out ftitle);
                        obj.TryGetValue("isChekced", out isChekced);
                        obj.TryGetValue("isCounterSign", out isCounterSign);
                        obj.TryGetValue("nodeState", out nodeState);
                        obj.TryGetValue("username", out username);
                        obj.TryGetValue("usercode", out usercode    );
                        obj.TryGetValue("deptname", out deptname);
                        obj.TryGetValue("deptcode", out deptcode);
                        obj.TryGetValue("arrayCounter", out arrayCounter);
                        obj.TryGetValue("nodeType", out nodeType);
                        obj.TryGetValue("roleCode", out rolecode);
                        obj.TryGetValue("roleName", out rolename);
                        obj.TryGetValue("URL", out url);


                        CustomizeFlowInfo flowInfo = new CustomizeFlowInfo();
                        flowInfo.ftitle = ftitle.ToString();
                        flowInfo.isChekced = Convert.ToBoolean(isChekced);
                        flowInfo.nodeType = nodeType.ToString();
                        if (!flowInfo.nodeType.Equals("User") && rolecode != null && rolename != null)
                        {
                            flowInfo.roleCode = rolecode.ToString();
                            flowInfo.roleName = rolename.ToString();
                        }
                        else
                        {
                            flowInfo.roleCode = "";
                            flowInfo.roleName = "";
                        }
                        flowInfo.isCounterSign = Convert.ToBoolean(isCounterSign);
                        flowInfo.nodeState = Convert.ToInt32(nodeState);
                        flowInfo.username = username.ToString();
                        flowInfo.usercode = usercode.ToString();
                        flowInfo.arrayCounter = arrayCounter as Array;
                        FlowChartList.Add(flowInfo);
                    }

                    //生成XML
                    if (FlowChartList != null && FlowChartList.Count != 0)
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlElement xmlEle;
                        XmlElement childEleRoot;            //根子节点(Approval)
                        XmlElement childEle;                //子节点

                        //创建根节点
                        xmlEle = xmlDoc.CreateElement("Approvals");
                        xmlDoc.AppendChild(xmlEle);

                        //Biz_Employee empolyeeDac = new Biz_Employee();
                        //T_Employee empolyee = empolyeeDac.GetEmployeeByEmployeeCode(opratorcode);
                        UserProfileBLL empolyeeDac = new UserProfileBLL();
                        UserProfileInfo empolyee = empolyeeDac.GetUserProfileByADAccount(opratorcode);

                        if (empolyee != null)
                        {
                            //创建提交人节点
                            childEleRoot = xmlDoc.CreateElement("Originator");
                            xmlEle.AppendChild(childEleRoot);
                            childEleRoot.SetAttribute("Email", empolyee.Email);
                            childEleRoot.SetAttribute("Tel", empolyee.BlackBerry);
                            childEleRoot.InnerText = empolyee.ADAccount;

                            if (isReview)
                            {
                                //创建子节点
                                childEleRoot = xmlDoc.CreateElement("Approval");
                                xmlEle.AppendChild(childEleRoot);
                                childEleRoot.SetAttribute("state", "wait");
                                childEleRoot.SetAttribute("name", string.Empty);       //Title属性
                                childEleRoot.SetAttribute("step", string.Empty);
                                childEleRoot.SetAttribute("meetState", "N");
                                childEleRoot.SetAttribute("nodeType", "User");
                                childEleRoot.SetAttribute("RoleName", string.Empty);
                                childEleRoot.SetAttribute("RoleCode", string.Empty);
                                childEle = xmlDoc.CreateElement("Destinations");    //节点人标签
                                childEleRoot.AppendChild(childEle);
                                empolyee = empolyeeDac.GetUserProfileByADAccount(opratorcode);
                                childEle.InnerText = opratorcode;
                                if (string.IsNullOrEmpty(empolyee.Email))
                                    childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                else
                                    childEle.SetAttribute("Email", empolyee.Email);

                                if (string.IsNullOrEmpty(empolyee.BlackBerry))
                                    childEle.SetAttribute("Tel", "");
                                else
                                    childEle.SetAttribute("Tel", empolyee.BlackBerry);

                                if (!string.IsNullOrEmpty(empolyee.BlackBerry))
                                    childEle.SetAttribute("UserNames", empolyee.BlackBerry);

                                List<UserDepartmentInfo> udiList = empolyeeDac.GetDepartmentByUserID(empolyee.ID.ToString());
                                UserDepartmentInfo info = udiList.Find(delegate(UserDepartmentInfo tmp)
                                {
                                    if (tmp.IsMain)
                                        return true;
                                    else
                                        return false;
                                });
                                if (info != null)
                                {
                                    childEle.SetAttribute("DeptNames", info.DeptName);
                                    childEle.SetAttribute("DeptCodes", info.DeptCode.ToString());
                                }
                                else
                                {
                                    childEle.SetAttribute("DeptNames", string.Empty);
                                    childEle.SetAttribute("DeptCodes", string.Empty);
                                }
                            }

                            foreach (CustomizeFlowInfo cfi in FlowChartList)
                            {
                                //创建子节点
                                childEleRoot = xmlDoc.CreateElement("Approval");
                                xmlEle.AppendChild(childEleRoot);

                                //状态属性
                                if (cfi.nodeState == 0)
                                    childEleRoot.SetAttribute("state", "complete");
                                else if (cfi.nodeState == 1)
                                    childEleRoot.SetAttribute("state", "current");
                                else
                                    childEleRoot.SetAttribute("state", "wait");

                                childEleRoot.SetAttribute("nodeType", cfi.nodeType);

                                if (!cfi.nodeType.Equals("User"))
                                {
                                    childEleRoot.SetAttribute("RoleName", cfi.roleName);
                                    childEleRoot.SetAttribute("RoleCode", cfi.roleCode);
                                }
                                else
                                {
                                    childEleRoot.SetAttribute("RoleName", string.Empty);
                                    childEleRoot.SetAttribute("RoleCode", string.Empty);
                                }

                                childEleRoot.SetAttribute("name", cfi.ftitle);       //Title属性
                                childEleRoot.SetAttribute("step", string.Empty);

                                //取得该节点所有的审批人
                                string[] destinations;
                                if (cfi.nodeType.Equals("User"))
                                    destinations = cfi.usercode.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                else
                                    destinations = new string[] { "" };
                                //else
                                //    destinations = new Biz_T_SubClass().GetUserCodeByRoleCode(cfi.roleCode).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);      //角色的情况
                                for (int i = 0; i < destinations.Length; i++)
                                {
                                    childEle = xmlDoc.CreateElement("Destinations");    //节点人标签
                                    childEleRoot.AppendChild(childEle);

                                    empolyee = empolyeeDac.GetUserProfileByADAccount(destinations[i]);
                                    childEle.InnerText = destinations[i];

                                    //姓名
                                    if (!string.IsNullOrEmpty(empolyee.CHName))
                                        childEle.SetAttribute("UserNames", empolyee.CHName);

                                    List<UserDepartmentInfo> udiList = empolyeeDac.GetDepartmentByUserID(empolyee.ID.ToString());
                                    if (udiList == null)
                                    {
                                        childEle.SetAttribute("DeptNames", string.Empty);
                                        childEle.SetAttribute("DeptCodes", string.Empty);
                                    }
                                    else
                                    {
                                        UserDepartmentInfo info = udiList.Find(delegate(UserDepartmentInfo tmp)
                                        {
                                            if (tmp.IsMain)
                                                return true;
                                            else
                                                return false;
                                        });
                                        if (info != null)
                                        {
                                            childEle.SetAttribute("DeptNames", info.DeptName);
                                            childEle.SetAttribute("DeptCodes", info.DeptCode.ToString());
                                        }
                                        else
                                        {
                                            childEle.SetAttribute("DeptNames", string.Empty);
                                            childEle.SetAttribute("DeptCodes", string.Empty);
                                        }
                                    }
                                    if (string.IsNullOrEmpty(empolyee.Email))
                                        childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                    else
                                        childEle.SetAttribute("Email", empolyee.Email);

                                    if (string.IsNullOrEmpty(empolyee.BlackBerry))
                                        childEle.SetAttribute("Tel", "");
                                    else
                                        childEle.SetAttribute("Tel", empolyee.BlackBerry);
                                }

                                //会签
                                if (cfi.arrayCounter.Length != 0)
                                {
                                    bool isSign = false;        //标示是否有新会签

                                    object counterSignCodes, counterSignNames, counterDeptName, counterDeptCode, signEnabled;
                                    object[] tmpCounterSigns = new object[cfi.arrayCounter.Length];                            //用户保存抽象的Array
                                    CustomizeCounterSign[] resultSign = new CustomizeCounterSign[cfi.arrayCounter.Length];       //保存读取后的结果
                                    cfi.arrayCounter.CopyTo(tmpCounterSigns, 0);                                               //将Array中的内容Copy到tmpCounterSigns中
                                    for (int j = 0; j < tmpCounterSigns.Length; j++)
                                    {
                                        //读取内容
                                        Dictionary<string, object> counter = (Dictionary<string, object>)(tmpCounterSigns[j]);
                                        CustomizeCounterSign tmpSign = new CustomizeCounterSign();
                                        counter.TryGetValue("counterSignCodes", out counterSignCodes);
                                        counter.TryGetValue("counterSignNames", out counterSignNames);
                                        counter.TryGetValue("counterDeptName", out counterDeptName);
                                        counter.TryGetValue("counterDeptCode", out counterDeptCode);
                                        counter.TryGetValue("enabled", out signEnabled);

                                        //将结果依次保存到数组中
                                        tmpSign.counterSignCodes = counterSignCodes.ToString();
                                        tmpSign.counterSignNames = counterSignNames.ToString();
                                        tmpSign.counterDeptName = counterDeptName.ToString();
                                        tmpSign.counterDeptCode = counterDeptCode.ToString();
                                        tmpSign.enabled = Convert.ToBoolean(signEnabled);

                                        childEle = xmlDoc.CreateElement("Meet");        //创建会签人结点
                                        childEleRoot.AppendChild(childEle);
                                        empolyee = empolyeeDac.GetUserProfileByADAccount(tmpSign.counterSignCodes);
                                        if (tmpSign.enabled)        //存在新的会签
                                        {
                                            childEle.SetAttribute("state", "A");
                                            if (string.IsNullOrEmpty(empolyee.Email))
                                                childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                            else
                                                childEle.SetAttribute("Email", empolyee.Email);

                                            if (string.IsNullOrEmpty(empolyee.BlackBerry))
                                                childEle.SetAttribute("Tel", "");
                                            else
                                                childEle.SetAttribute("Tel", empolyee.BlackBerry);
                                            isSign = true;
                                        }
                                        else
                                        {
                                            childEle.SetAttribute("state", "C");
                                            if (string.IsNullOrEmpty(empolyee.Email))
                                                childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                            else
                                                childEle.SetAttribute("Email", empolyee.Email);

                                            if (string.IsNullOrEmpty(empolyee.BlackBerry))
                                                childEle.SetAttribute("Tel", "");
                                            else
                                                childEle.SetAttribute("Tel", empolyee.BlackBerry);
                                        }

                                        childEle.InnerText = empolyee.ID.ToString();
                                    }

                                    if (isSign)         //是否有新会签属性
                                        childEleRoot.SetAttribute("meetState", "Y");
                                    else
                                        childEleRoot.SetAttribute("meetState", "N");
                                }
                                else
                                {
                                    childEleRoot.SetAttribute("meetState", "N");
                                }
                            }
                            return xmlDoc.InnerXml;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogUtil.Log.Error(ex.Message);
                return null;
            }
        }

        public string FlowChartJsonToXml_ReSubmit(string flowChartJson, string opratorcode)
        {
            return string.Empty;
            try
            {
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                object[] flowList = (object[])(jsonSerializer.DeserializeObject(flowChartJson));
                object nodeId, ftitle, isChekced, isCounterSign, nodeState, nodeType, rolecode, rolename, username, usercode, deptname, deptcode, arrayCounter;    //保存反序列化后的值
                ArrayList FlowChartList = new ArrayList();
                bool isReview = false;

                ////判断是否是代申请
                if (!string.IsNullOrEmpty(opratorcode) && !string.IsNullOrEmpty(EmployeeCode) && !opratorcode.Equals(EmployeeCode, StringComparison.OrdinalIgnoreCase))
                    isReview = true;

                if (flowList != null)
                {
                    //产生CustomizeFlowInfo类
                    for (int i = 0; i < flowList.Length; i++)
                    {
                        Dictionary<string, object> obj = (Dictionary<string, object>)(flowList[i]);
                        obj.TryGetValue("nodeId", out nodeId);
                        obj.TryGetValue("ftitle", out ftitle);
                        obj.TryGetValue("isChekced", out isChekced);
                        obj.TryGetValue("isCounterSign", out isCounterSign);
                        obj.TryGetValue("nodeState", out nodeState);
                        obj.TryGetValue("username", out username);
                        obj.TryGetValue("usercode", out usercode);
                        obj.TryGetValue("deptname", out deptname);
                        obj.TryGetValue("deptcode", out deptcode);
                        obj.TryGetValue("arrayCounter", out arrayCounter);
                        obj.TryGetValue("nodeType", out nodeType);
                        obj.TryGetValue("roleCode", out rolecode);
                        obj.TryGetValue("roleName", out rolename);


                        CustomizeFlowInfo flowInfo = new CustomizeFlowInfo();
                        flowInfo.ftitle = ftitle.ToString();
                        flowInfo.isChekced = Convert.ToBoolean(isChekced);
                        flowInfo.nodeType = nodeType.ToString();
                        if (!flowInfo.nodeType.Equals("User") && rolecode != null && rolename != null)
                        {
                            flowInfo.roleCode = rolecode.ToString();
                            flowInfo.roleName = rolename.ToString();
                        }
                        else
                        {
                            flowInfo.roleCode = "";
                            flowInfo.roleName = "";
                        }
                        flowInfo.isCounterSign = Convert.ToBoolean(isCounterSign);
                        flowInfo.nodeState = Convert.ToInt32(nodeState);
                        flowInfo.username = username.ToString();
                        flowInfo.usercode = usercode.ToString();
                        flowInfo.arrayCounter = arrayCounter as Array;
                        FlowChartList.Add(flowInfo);
                    }

                    //生成XML
                    if (FlowChartList != null && FlowChartList.Count != 0)
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlElement xmlEle;
                        XmlElement childEleRoot;            //根子节点(Approval)
                        XmlElement childEle;                //子节点

                        //创建根节点
                        xmlEle = xmlDoc.CreateElement("Approvals");
                        xmlDoc.AppendChild(xmlEle);

                        //Biz_Employee empolyeeDac = new Biz_Employee();
                        //T_Employee empolyee = empolyeeDac.GetEmployeeByEmployeeCode(opratorcode);
                        UserProfileBLL empolyeeDac = new UserProfileBLL();
                        UserProfileInfo empolyee = empolyeeDac.GetUserProfileByADAccount(opratorcode);

                        if (empolyee != null)
                        {
                            //创建提交人节点
                            childEleRoot = xmlDoc.CreateElement("Originator");
                            xmlEle.AppendChild(childEleRoot);
                            childEleRoot.SetAttribute("Email", empolyee.Email);
                            childEleRoot.SetAttribute("Tel", empolyee.BlackBerry);
                            childEleRoot.InnerText = empolyee.BlackBerry;

                            if (isReview)
                            {
                                //创建子节点
                                childEleRoot = xmlDoc.CreateElement("Approval");
                                xmlEle.AppendChild(childEleRoot);
                                childEleRoot.SetAttribute("state", "wait");
                                childEleRoot.SetAttribute("name", string.Empty);       //Title属性
                                childEleRoot.SetAttribute("step", string.Empty);
                                childEleRoot.SetAttribute("meetState", "N");
                                childEleRoot.SetAttribute("nodeType", "User");
                                childEleRoot.SetAttribute("RoleName", string.Empty);
                                childEleRoot.SetAttribute("RoleCode", string.Empty);
                                childEleRoot.SetAttribute("IsConfirm", "Y");
                                childEle = xmlDoc.CreateElement("Destinations");    //节点人标签
                                childEleRoot.AppendChild(childEle);
                                empolyee = empolyeeDac.GetUserProfileByADAccount(opratorcode);
                                childEle.InnerText = opratorcode;
                                if (string.IsNullOrEmpty(empolyee.Email))
                                    childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                else
                                    childEle.SetAttribute("Email", empolyee.Email);

                                if (string.IsNullOrEmpty(empolyee.BlackBerry))
                                    childEle.SetAttribute("Tel", "");
                                else
                                    childEle.SetAttribute("Tel", empolyee.BlackBerry);

                                if (!string.IsNullOrEmpty(empolyee.BlackBerry))
                                    childEle.SetAttribute("UserNames", empolyee.BlackBerry);

                                List<UserDepartmentInfo> udiList = empolyeeDac.GetDepartmentByUserID(empolyee.ID.ToString());
                                UserDepartmentInfo info = udiList.Find(delegate(UserDepartmentInfo tmp)
                                {
                                    if (tmp.IsMain)
                                        return true;
                                    else
                                        return false;
                                });
                                if (info != null)
                                {
                                    childEle.SetAttribute("DeptNames", info.DeptName);
                                    childEle.SetAttribute("DeptCodes", info.DeptCode.ToString());
                                }
                                else
                                {
                                    childEle.SetAttribute("DeptNames", string.Empty);
                                    childEle.SetAttribute("DeptCodes", string.Empty);
                                }
                            }

                            foreach (CustomizeFlowInfo cfi in FlowChartList)
                            {
                                //创建子节点
                                childEleRoot = xmlDoc.CreateElement("Approval");
                                xmlEle.AppendChild(childEleRoot);

                                //状态属性
                                if (cfi.nodeState == 0)
                                    childEleRoot.SetAttribute("state", "complete");
                                else if (cfi.nodeState == 1)
                                    childEleRoot.SetAttribute("state", "current");
                                else
                                    childEleRoot.SetAttribute("state", "wait");

                                childEleRoot.SetAttribute("nodeType", cfi.nodeType);

                                if (!cfi.nodeType.Equals("User"))
                                {
                                    childEleRoot.SetAttribute("RoleName", cfi.roleName);
                                    childEleRoot.SetAttribute("RoleCode", cfi.roleCode);
                                }
                                else
                                {
                                    childEleRoot.SetAttribute("RoleName", string.Empty);
                                    childEleRoot.SetAttribute("RoleCode", string.Empty);
                                }

                                childEleRoot.SetAttribute("name", cfi.ftitle);       //Title属性
                                childEleRoot.SetAttribute("step", string.Empty);

                                //取得该节点所有的审批人
                                string[] destinations;
                                if (cfi.nodeType.Equals("User"))
                                    destinations = cfi.usercode.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                //else
                                //    destinations = new Biz_T_SubClass().GetUserCodeByRoleCode(cfi.roleCode).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);      //角色的情况
                                //for (int i = 0; i < destinations.Length; i++)
                                //{
                                //    childEle = xmlDoc.CreateElement("Destinations");    //节点人标签
                                //    childEleRoot.AppendChild(childEle);

                                //    empolyee = empolyeeDac.GetEmployeeByEmployeeCode(destinations[i]);
                                //    childEle.InnerText = destinations[i];

                                //    //姓名
                                //    if (!string.IsNullOrEmpty(empolyee.EmployeeName))
                                //        childEle.SetAttribute("UserNames", empolyee.EmployeeName);

                                //    // 部门名称
                                //    if (!string.IsNullOrEmpty(empolyee.FirstDeptName) && !string.IsNullOrEmpty(empolyee.SecondDeptName))
                                //        childEle.SetAttribute("DeptNames", empolyee.FirstDeptName + "." + empolyee.SecondDeptName);
                                //    else if (!string.IsNullOrEmpty(empolyee.FirstDeptName))
                                //        childEle.SetAttribute("DeptNames", empolyee.FirstDeptName);
                                //    else if (!string.IsNullOrEmpty(empolyee.SecondDeptName))
                                //        childEle.SetAttribute("DeptNames", empolyee.SecondDeptName);

                                //    //部门代码
                                //    if (!string.IsNullOrEmpty(empolyee.FirstDeptCode) && !string.IsNullOrEmpty(empolyee.SecondDeptCode))
                                //        childEle.SetAttribute("DeptCodes", empolyee.FirstDeptCode + "." + empolyee.SecondDeptCode);
                                //    else if (!string.IsNullOrEmpty(empolyee.FirstDeptCode))
                                //        childEle.SetAttribute("DeptCodes", empolyee.FirstDeptCode);
                                //    else if (!string.IsNullOrEmpty(empolyee.SecondDeptCode))
                                //        childEle.SetAttribute("DeptCodes", empolyee.SecondDeptCode);

                                //    if (string.IsNullOrEmpty(empolyee.Email))
                                //        childEle.SetAttribute("Email", "NOMEETAPPROVER");
                                //    else
                                //        childEle.SetAttribute("Email", empolyee.Email);

                                //    if (string.IsNullOrEmpty(empolyee.Mobile_Phone))
                                //        childEle.SetAttribute("Tel", "");
                                //    else
                                //        childEle.SetAttribute("Tel", empolyee.Mobile_Phone);
                                //}
                            }
                            return xmlDoc.InnerXml;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogUtil.Log.Error(ex.Message);
                return null;
            }
        }


        /// <summary>
        /// XML->JSON
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public string FlowChartXmlToJson(string xmlString, bool isView)
        {
            return string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(xmlString.Trim()))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlString);
                    //xmlDoc.Load(Server.MapPath("~/Process/CDF/ApproveXML.xml"));
                    XmlNodeList nodeList = xmlDoc.SelectNodes("Approvals/Approval");    //取得所有环节结点
                    ArrayList counterSignList = new ArrayList();                        //保存会签人
                    ArrayList flowList = new ArrayList();                               //保存流程
                    foreach (XmlNode node in nodeList)
                    {
                        counterSignList.Clear();
                        CustomizeFlowInfo cfi = new CustomizeFlowInfo();
                        if (node.Attributes["state"].Value.Equals("wait"))
                            cfi.nodeState = 3;
                        else if (node.Attributes["state"].Value.Equals("current"))
                        {
                            cfi.nodeState = 1;
                        }
                        else if (node.Attributes["state"].Value.Equals("complete"))
                        {
                            cfi.nodeState = 0;
                        }

                        if (node.Attributes["name"] != null)
                            cfi.ftitle = node.Attributes["name"].Value;
                        else
                            cfi.ftitle = string.Empty;
                        cfi.username = cfi.usercode = cfi.deptcode = cfi.deptname = string.Empty;
                        cfi.isOpen = true;

                        if (node.Attributes["RoleCode"] == null)
                            cfi.roleCode = "";
                        else
                            cfi.roleCode = node.Attributes["RoleCode"].Value;

                        if (node.Attributes["RoleName"] == null)
                            cfi.roleName = "";
                        else
                            cfi.roleName = node.Attributes["RoleName"].Value;

                        cfi.nodeType = node.Attributes["nodeType"].Value;

                        if (node.Attributes["NodeId"] != null)
                            cfi.nodeId = node.Attributes["NodeId"].Value;
                        else
                            cfi.nodeId = Guid.NewGuid().ToString();

                        if (node.Attributes["URL"] != null)
                            cfi.URL = node.Attributes["URL"].Value;
                        else
                            cfi.URL = "Process/CDF/Approve.aspx";

                        //取得
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            if (node.ChildNodes[i].Name.Equals("Destinations"))
                            {
                                if (node.ChildNodes[i].Attributes["UserNames"] != null)
                                    cfi.username = node.ChildNodes[i].Attributes["UserNames"].Value;
                                else
                                    cfi.usercode = string.Empty;

                                cfi.usercode = node.ChildNodes[i].InnerText + ";";

                                if (node.ChildNodes[i].Attributes["DeptNames"] != null)
                                    cfi.deptname = node.ChildNodes[i].Attributes["DeptNames"].Value;
                                else
                                    cfi.deptname = string.Empty;

                                if (node.ChildNodes[i].Attributes["DeptCodes"] != null)
                                    cfi.deptcode = node.ChildNodes[i].Attributes["DeptCodes"].Value;
                                else
                                    cfi.deptcode = string.Empty;
                            }
                            else if (node.ChildNodes[i].Name.Equals("Meet"))
                            {
                                XmlNode meet = node.ChildNodes[i];
                                CustomizeCounterSign tmpCcs = new CustomizeCounterSign();
                                if (meet.Attributes["state"].Value.Equals("A", StringComparison.CurrentCultureIgnoreCase))
                                    tmpCcs.enabled = true;
                                else
                                    tmpCcs.enabled = false;

                                tmpCcs.counterSignCodes = meet.InnerText;
                                UserProfileBLL bll = new UserProfileBLL();
                                
                                //Biz_Employee empolyeeDac = new Biz_Employee();
                                //T_Employee empolyee = empolyeeDac.GetEmployeeByEmployeeCode(meet.InnerText);

                                UserProfileBLL empolyeeDac = new UserProfileBLL();
                                UserProfileInfo empolyee = empolyeeDac.GetUserProfileByADAccount(meet.InnerText);

                                tmpCcs.counterSignNames = empolyee.CHName;
                                //tmpCcs.counterDeptName = empolyee.FirstDeptName + "." + empolyee.SecondDeptName;
                                //tmpCcs.counterDeptCode = empolyee.FirstDeptCode + "." + empolyee.SecondDeptCode;
                                tmpCcs.counterDeptName = "";
                                tmpCcs.counterDeptCode = "";
                                counterSignList.Add(tmpCcs);
                            }
                        }
                        cfi.arrayCounter = counterSignList.ToArray();
                        flowList.Add(cfi);      //添加列表
                    }
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    return jsonSerializer.Serialize(flowList);      //Json编码
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                LogUtil.Log.Error(ex.Message);
                return string.Empty;
            }
        }

        public string FlowChartXmlToJson_ReSubmit(string xmlString, bool isView)
        {
            return string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(xmlString.Trim()))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlString);
                    //xmlDoc.Load(Server.MapPath("~/Process/CDF/ApproveXML.xml"));
                    XmlNodeList nodeList = xmlDoc.SelectNodes("Approvals/Approval");    //取得所有环节结点
                    ArrayList counterSignList = new ArrayList();                        //保存会签人
                    ArrayList flowList = new ArrayList();                               //保存流程
                    foreach (XmlNode node in nodeList)
                    {
                        counterSignList.Clear();
                        CustomizeFlowInfo cfi = new CustomizeFlowInfo();
                        if (node.Attributes["IsConfirm"] != null && node.Attributes["IsConfirm"].Value == "Y")
                            continue;

                        if (node.Attributes["state"].Value.Equals("wait"))
                            cfi.nodeState = 3;
                        else if (node.Attributes["state"].Value.Equals("current"))
                        {
                            cfi.nodeState = 1;
                        }
                        else if (node.Attributes["state"].Value.Equals("complete"))
                        {
                            cfi.nodeState = 0;
                        }

                        if (node.Attributes["name"] != null)
                            cfi.ftitle = node.Attributes["name"].Value;
                        else
                            cfi.ftitle = string.Empty;
                        cfi.username = cfi.usercode = cfi.deptcode = cfi.deptname = string.Empty;
                        cfi.isOpen = true;

                        if (node.Attributes["RoleCode"] == null)
                            cfi.roleCode = "";
                        else
                            cfi.roleCode = node.Attributes["RoleCode"].Value;

                        if (node.Attributes["RoleName"] == null)
                            cfi.roleName = "";
                        else
                            cfi.roleName = node.Attributes["RoleName"].Value;

                        cfi.nodeType = node.Attributes["nodeType"].Value;

                        //取得
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            if (node.ChildNodes[i].Name.Equals("Destinations"))
                            {
                                if (node.ChildNodes[i].Attributes["UserNames"] != null)
                                    cfi.username = node.ChildNodes[i].Attributes["UserNames"].Value;
                                else
                                    cfi.usercode = string.Empty;

                                cfi.usercode = node.ChildNodes[i].InnerText + ";";

                                if (node.ChildNodes[i].Attributes["DeptNames"] != null)
                                    cfi.deptname = node.ChildNodes[i].Attributes["DeptNames"].Value;
                                else
                                    cfi.deptname = string.Empty;

                                if (node.ChildNodes[i].Attributes["DeptCodes"] != null)
                                    cfi.deptcode = node.ChildNodes[i].Attributes["DeptCodes"].Value;
                                else
                                    cfi.deptcode = string.Empty;
                            }
                            else if (node.ChildNodes[i].Name.Equals("Meet"))
                            {
                                XmlNode meet = node.ChildNodes[i];
                                CustomizeCounterSign tmpCcs = new CustomizeCounterSign();
                                if (meet.Attributes["state"].Value.Equals("A", StringComparison.CurrentCultureIgnoreCase))
                                    tmpCcs.enabled = true;
                                else
                                    tmpCcs.enabled = false;

                                tmpCcs.counterSignCodes = meet.InnerText;
                                //Biz_Employee empolyeeDac = new Biz_Employee();
                                //T_Employee empolyee = empolyeeDac.GetEmployeeByEmployeeCode(meet.InnerText);
                                UserProfileBLL empolyeeDac = new UserProfileBLL();
                                UserProfileInfo empolyee = empolyeeDac.GetUserProfileByADAccount(meet.InnerText);
                                tmpCcs.counterSignNames = empolyee.CHName;
                                //tmpCcs.counterDeptName = empolyee.FirstDeptName + "." + empolyee.SecondDeptName;
                                //tmpCcs.counterDeptCode = empolyee.FirstDeptCode + "." + empolyee.SecondDeptCode;
                                tmpCcs.counterDeptName = "";
                                tmpCcs.counterDeptCode = "";
                                counterSignList.Add(tmpCcs);
                            }
                        }
                        cfi.arrayCounter = counterSignList.ToArray();
                        flowList.Add(cfi);      //添加列表
                    }
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    return jsonSerializer.Serialize(flowList);      //Json编码
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                LogUtil.Log.Error(ex.Message);
                return string.Empty;
            }
        }
    }
}