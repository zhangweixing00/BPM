using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using System.Data;
using Utility;
using Model;

namespace OrgWebSite.Admin.RuleDesign
{
    /// <summary>
    /// Summary description for RuleDesignHandler
    /// </summary>
    public class RuleDesignHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Form["action"]))
            {
                String retString = string.Empty;
                switch (context.Request.Form["action"])
                {
                    case "getOrganization":
                        retString = GetOrganization();
                        break;
                    case "getOrgUsers":
                        retString = GetOrgUsers(context);
                        break;
                    case "getOrg":
                        retString = GetOrg(context);
                        break;
                    case "getProcess":
                        retString = GetProcess(context);
                        break;
                    case "saveSPNode":
                        retString = SaveSPNode(context);
                        break;
                    case "getApproveRuleTable":
                        retString = GetApproveRuleTable(context);
                        break;
                }
                HttpContext.Current.Response.Write(retString);
            }
        }

        /// <summary>
        /// 取得所有组织JSON
        /// </summary>
        /// <returns></returns>
        private string GetOrganization()
        {
            try
            {
                DepartmentBLL bll = new DepartmentBLL();
                //DataTable dt = bll.GetSortDepartment("0e4e8081-d9cb-4d32-9d6e-334e9a6d449c");
                DataTable dt = bll.GetOrgStruct();
                EasyUIJsonConvert.result = new System.Text.StringBuilder();
                EasyUIJsonConvert.GetTreeJsonByTable(dt, "DeptCode", "Department", "ParentCode", "R0");
                return EasyUIJsonConvert.result.ToString();
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RuleDesignHandler.GetOrganization", DBManager.GetCurrentUserAD());
                return "Fail";
            }
        }


        private string GetOrgUsers(HttpContext context)
        {
            try
            {
                UserProfileBLL bll = new UserProfileBLL();
                IList<UserProfileInfo> userInfo = bll.GetAllUserProfile("");
                DataTable dt = DataTableExtension.ToDataTable<UserProfileInfo>(userInfo);
                string Id = context.Request.Form["id"];
                string text = context.Request.Form["text"];
                return EasyUIJsonConvert.GetComboBoxJson(dt, Id, text);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RuleDesignHandler.GetOrgUsers", DBManager.GetCurrentUserAD());
                return "Fail";
            }
        }

        /// <summary>
        /// 取得组织信息
        /// </summary>
        private string GetOrg(HttpContext context)
        {
            try
            {
                OrganizationBLL bll = new OrganizationBLL();
                IList<OrganizationInfo> orgList = bll.GetOrgList();
                DataTable dt = DataTableExtension.ToDataTable<OrganizationInfo>(orgList);
                string Id = context.Request.Form["id"];
                string text = context.Request.Form["text"];
                return EasyUIJsonConvert.GetComboBoxJson(dt, Id, text);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RuleDesignHandler.GetOrganization", DBManager.GetCurrentUserAD());
                return "Fail";
            }
        }


        /// <summary>
        /// 取得流程信息
        /// </summary>
        /// <returns></returns>
        private string GetProcess(HttpContext context)
        {
            try
            {
                ProcessTypeBLL bll = new ProcessTypeBLL();
                IList<ProcessTypeInfo> procList = bll.GetProcessType();
                DataTable dt = DataTableExtension.ToDataTable<ProcessTypeInfo>(procList);
                string Id = context.Request.Form["id"];
                string text = context.Request.Form["text"];
                return EasyUIJsonConvert.GetComboBoxJson(dt, Id, text);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RuleDesignHandler.GetProcess", DBManager.GetCurrentUserAD());
                return "Fail";
            }
        }


        /// <summary>
        /// 保存审批节点
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string SaveSPNode(HttpContext context)
        {
            string notification = context.Request.Form["notification"];
            string nodeName = context.Request.Form["nodeName"];
            string url = context.Request.Form["url"];
            string wayBack = context.Request.Form["waybacknodeId"];

            ProcessNodeInfo info = new ProcessNodeInfo();
            info.ProcessID = context.Request.Form["ProcessID"];
            info.State = true;
            
            info.ApproveRule = 1;
            
            info.DeclineRule = 1;

            info.NodeName = nodeName;
            info.IsAllowMeet = true;
            info.IsAllowEndorsement = true;
            info.Notification = notification;
            info.WayBack = 1;   //退回方式去除

            info.URL = url;
            info.WayBackNodeID = Guid.Empty;
            //if (string.IsNullOrEmpty(ddlWayBack.SelectedValue))
            //    info.WayBackNodeID = Guid.Empty;
            //else
            //    info.WayBackNodeID = new Guid(ddlWayBack.SelectedValue);
            info.CreatedBy = context.Request.Form["createdBy"];
            info.OrderNo = 1;

            //ProcessRuleBLL bll = new ProcessRuleBLL();
            //bll.InsertProcessNode(info);

            return Guid.NewGuid().ToString();
        }


        /// <summary>
        /// 取得审批表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetApproveRuleTable(HttpContext context)
        {
            return string.Empty;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}