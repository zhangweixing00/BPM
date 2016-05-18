using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Utility;
using Model;
using System.Collections.Generic;
using BLL;

namespace OrgWebSite.Admin.Popup
{
    public partial class DeptUserAdd : BasePage
    {
        protected string deptCode
        {
            get
            {
                try
                {
                    return Request.QueryString["deptCode"].Trim();
                }
                catch
                {
                    return null;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            //DataTable dt = DBManager.GetUserProfileOutDept(deptCode,txtFilter.Text);
            UserProfileBLL bll = new UserProfileBLL();
            IList<UserProfileInfo> upList = bll.GetUserProfileOutDept(deptCode, txtFilter.Text);
            gvUser.DataSource = upList;
            gvUser.DataBind();
        }

        protected void btnChoose_Click(object sender, EventArgs e)
        {
            string usercode = hfSelectedUser.Value;

            //DBManager.AddDeptUser(deptCode, usercode);
            UserProfileBLL bll = new UserProfileBLL();
            bll.AddDeptUser(deptCode, usercode);

            litScript.Text = "<script>window.returnValue='';window.close();</script>";
        }

        protected void lbReload_Click(object sender, EventArgs e)
        {
            BindData();
        }

        public string GetWorkPlace(string wp)
        {
            if (wp.ToUpper() == "BJ")
                return "北京";

            if (wp.ToUpper() == "HK")
                return "香港";

            if (wp.ToUpper() == "ALL")
                return "所有";

            return wp;

        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfUserCode = (HiddenField)(e.Row.FindControl("hfUserCode"));
                string userCode = hfUserCode.Value;
                CheckBox chkUser = (CheckBox)(e.Row.FindControl("chkUser"));
                if (hfSelectedUser.Value.Contains(userCode + ";"))
                {
                    chkUser.Checked = true;
                }
            }
        }
    }
}
