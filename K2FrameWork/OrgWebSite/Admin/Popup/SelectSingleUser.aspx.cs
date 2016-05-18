using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Data;
using BLL;

namespace OrgWebSite.Admin.Popup
{
    public partial class SelectSingleUser : BasePage
    {
        public string DeptCode
        {
            get
            {
                if (Request.QueryString["deptCode"] != null)
                    return Request.QueryString["deptCode"];

                return "";
            }
        }

        public string Account
        {
            get
            {
                if (Request.QueryString["account"] != null)
                    return Request.QueryString["account"];

                return "";
            }
        }

        public string SelectType
        {
            get
            {
                if (Request.QueryString["selectType"] != null)
                    return Request.QueryString["selectType"];

                return "All";
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
            //DataSet ds = DBManager.GetUserByType(DeptCode, SelectType, txtFilter.Text);
            UserProfileBLL bll = new UserProfileBLL();
            DataSet ds = bll.GetUserByType(DeptCode, SelectType, txtFilter.Text);
            gvUser.DataSource = ds;
            gvUser.DataBind();
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

        protected void btnChoose_Click(object sender, EventArgs e)
        {
            string returnValue = "retValue;";
            int rowIndex = Convert.ToInt32(hfSelectUser.Value);
            rowIndex = rowIndex - gvUser.PageSize * gvUser.PageIndex;
            GridViewRow gvr = gvUser.Rows[rowIndex];
            HiddenField hfCHName = (HiddenField)(gvr.FindControl("hfCHName"));
            HiddenField hfADAccount = (HiddenField)(gvr.FindControl("hfADAccount"));

            returnValue += hfCHName.Value + ";" + hfADAccount.Value.Replace("\\", "\\\\");
            litScript.Text = "<script>window.returnValue='" + returnValue + "';window.close();</script>";
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet ds = gvUser.DataSource as DataSet;
            DataTable dt = ds.Tables[0];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string adAccount = dt.Rows[e.Row.RowIndex]["ADAccount"].ToString();
                RadioButton rbtn = (RadioButton)(e.Row.FindControl("rbtnUser"));
                if (adAccount == Account)
                {
                    rbtn.Checked = true;
                    hfSelectUser.Value = e.Row.RowIndex.ToString();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}