using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Data;
using BLL;
using Model;

namespace OrgWebSite.Admin
{
    public partial class UserManage : BasePage
    {
        public int totalRecordCount = 0;  //总共多少条记录
        int intPageIndex = 0; //当前页索引

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            //DataTable dt = DBManager.GetAllUserProfile(txtFilter.Text);
            UserProfileBLL bll = new UserProfileBLL();
            IList<UserProfileInfo> upList = bll.GetAllUserProfile(txtFilter.Text);
            if (upList != null)
            {
                gvUser.DataSource = upList;
                gvUser.DataBind();
                Common.BindBoundControl(upList, this.gvUser, intPageIndex, this.gvUser.PageSize, totalRecordCount);               
            }
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string userIDs = hfSelectedUser.Value;
            hfSelectedUser.Value = "";


            if (userIDs.LastIndexOf(',') == (userIDs.Length - 1))
                userIDs = userIDs.Remove(userIDs.LastIndexOf(','));

            //DBManager.DeleteUserProfile(userIDs, "-1");
            UserProfileBLL bll = new UserProfileBLL();
            bll.DeleteUserProfile(userIDs);
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            intPageIndex = e.NewPageIndex;
            BindData();
            //绑定跳转文本框
            TextBox tbPage = (TextBox)gvUser.BottomPagerRow.FindControl("txt_PageIndex");
            tbPage.Text = (intPageIndex + 1).ToString();
        }

        protected void lbReload_Click(object sender, EventArgs e)
        {
            BindData();
        }

        public string GetWorkPlace(string wp)
        {
            //if (wp.ToUpper() == "BJ")
            //    return "北京";

            //if (wp.ToUpper() == "SH")
            //    return "上海";

            //if (wp.ToUpper() == "HK")
            //    return "香港";

            //if (wp.ToUpper() == "ALL")
            //    return "所有";

            return wp;

        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataTable dt = gvUser.DataSource as DataTable;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfUserCode = (HiddenField)(e.Row.FindControl("hfUserCode"));
                string userCode = hfUserCode.Value;
                CheckBox chkUser = (CheckBox)(e.Row.FindControl("chkUser"));
                if (hfSelectedUser.Value.Contains(userCode + ","))
                {
                    chkUser.Checked = true;
                }
            }
        }

        protected void btnSynchronous_Click(object sender, EventArgs e)
        {
            //List<ADUserInfo> adUserInfos = new List<ADUserInfo>();
            //ImportData(General.GetConstValue("ADPath"), General.GetConstValue("ADUser"), General.GetConstValue("ADPass"), ref adUserInfos);

            //foreach (ADUserInfo userInfo in adUserInfos)
            //{
            //    DBManager.ImportUserProfile(userInfo.Account, userInfo.DisplayName, userInfo.Email);
            //}
        }
    }
}