using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using BLL;
using Model;

namespace OrgWebSite.Admin
{
    public partial class RoleManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            //DataTable dt = DBManager.GetRoles("");
            RoleBLL bll = new RoleBLL();
            IList<RoleInfo> roleList = bll.GetRoles(string.Empty);
            gvRole.DataSource = roleList;
            gvRole.DataBind();
            hfSelectedRoleCode.Value = "-1";
        }

        protected void gvRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRole.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string roleCodes = "";
            for (int i = 0; i < gvRole.Rows.Count; i++)
            {
                CheckBox chkUser = (CheckBox)(gvRole.Rows[i].FindControl("chkRole"));
                if (chkUser.Checked)
                {
                    HiddenField hfRoleCode = (HiddenField)(gvRole.Rows[i].FindControl("hfRoleCode"));
                    roleCodes += hfRoleCode.Value + ";";
                }
            }

            if (!string.IsNullOrEmpty(roleCodes) && roleCodes.LastIndexOf(';') == (roleCodes.Length - 1))
                roleCodes = roleCodes.Remove(roleCodes.LastIndexOf(';'));

            if (!string.IsNullOrEmpty(roleCodes))
            {
                //DBManager.DeleteRoles(roleCodes);
                RoleBLL bll = new RoleBLL();
                bll.DeleteRoles(roleCodes);
                BindData();
            }
        }

        protected void lbReload_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}