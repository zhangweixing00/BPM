using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using Model;
using System.Data;
using Utility;
using System.Configuration;

namespace OrgWebSite.Admin.RuleDesign
{
    /// <summary>
    /// Summary description for AutoCompleteHandler
    /// </summary>
    public class AutoCompleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String retString = string.Empty;
            switch (context.Request.QueryString["action"])
            {
                case "searchUsers":
                    retString = GetUsersByParams(context);
                    break;
                case "searchRoles":
                    retString = GetRolesByParams(context);
                    break;
            }
            HttpContext.Current.Response.Write(retString);
        }


        /// <summary>
        /// 通过参数取得用户信息
        /// </summary>
        /// <returns></returns>
        private string GetUsersByParams(HttpContext context)
        {
            try
            {
                string name_startsWith = context.Request.QueryString["name_startsWith"];
                string maxRows = context.Request.QueryString["maxRows"];
                string orgID = context.Request.QueryString["orgID"];
                string strMain = context.Request.QueryString["is_main"];
                if (string.IsNullOrEmpty(name_startsWith) || string.IsNullOrEmpty(maxRows) || string.IsNullOrEmpty(strMain))
                {
                    return "Fail";
                }
                else
                {
                    bool isMain = strMain == "true" ? true : false;
                    UserProfileBLL bll = new UserProfileBLL();
                    DataTable dt = bll.GetUsersByFilter(maxRows, orgID, name_startsWith, isMain);
                    return EasyUIJsonConvert.GetAutoCompleteJson(dt);
                }
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RuleDesignHandler.GetUsersByParams", DBManager.GetCurrentUserAD());
                return "Fail";
            }
        }

        /// <summary>
        /// 通过参数取得角色信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetRolesByParams(HttpContext context)
        {
            try
            {
                string name_startsWith = context.Request.QueryString["name_startsWith"];
                string maxRows = context.Request.QueryString["maxRows"];
                string orgID = context.Request.QueryString["orgID"];
                string strMain = context.Request.QueryString["is_main"];

                if (string.IsNullOrEmpty(name_startsWith) || string.IsNullOrEmpty(maxRows) || string.IsNullOrEmpty(strMain))
                {
                    return "Fail";
                }
                else
                {
                    bool isMain = strMain == "true" ? true : false;
                    RoleBLL bll = new RoleBLL();
                    DataTable dt = bll.GetRolesByOrgParams(orgID, name_startsWith, maxRows);
                    return EasyUIJsonConvert.GetAutoCompleteRoleJson(dt);
                }
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RuleDesignHandler.GetRolesByParams", DBManager.GetCurrentUserAD());
                return "Fail";
            }
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