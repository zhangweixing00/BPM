using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using BLL;
using Model;

namespace OrgWebSite.Admin
{
    public partial class ProcessNodeManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProcessType();
                BindProcessNode();
            }
            else
            {
                if (Request.Form["__EVENTTARGET"] == "lbReload")
                {
                    lbReload_Click(null, null);
                }
            }
        }

        /// <summary>
        /// 绑定流程类别
        /// </summary>
        private void BindProcessType()
        {
            ProcessTypeBLL bll = new ProcessTypeBLL();
            string loginName = Page.User.Identity.Name.Split('\\')[1];
            ddlProcessType.DataSource = WebCommon.GetDeptListByEmployeeCode(loginName);
            ddlProcessType.DataBind();
        }

        /// <summary>
        /// 绑定流程节点
        /// </summary>
        private void BindProcessNode()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            gvProcessNodes.DataSource = bll.GetProcessNodeByProcessID(ddlProcessType.SelectedValue);
            gvProcessNodes.DataBind();
        }

        protected void lbReload_Click(object sender, EventArgs e)
        {
            BindProcessType();
            BindProcessNode();
        }

        protected void ddlProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProcessNode();
        }
    }
}