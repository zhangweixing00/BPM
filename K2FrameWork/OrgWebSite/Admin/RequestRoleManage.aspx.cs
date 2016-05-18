using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using K2Utility;

namespace OrgWebSite.Admin
{
    public partial class RequestRoleManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProcessType();
                BindRequestNode();
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
            string loginName = Page.User.Identity.Name.Split('\\')[1];
            //string loginName = "dengli";
            ddlProcessType.DataSource = WebCommon.GetDeptListByEmployeeCode(loginName);
            ddlProcessType.DataBind();
        }

        /// <summary>
        /// 绑定GridView数据
        /// </summary>
        private void BindRequestNode()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            gvRequestNodes.DataSource = bll.GetRequestNodeByProcessID(ddlProcessType.SelectedValue);
            gvRequestNodes.DataBind();
        }

        protected void lbReload_Click(object sender, EventArgs e)
        {
            BindProcessType();
            BindRequestNode();
        }

        protected void ddlProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRequestNode();
        }
    }
}