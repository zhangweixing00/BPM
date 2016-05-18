using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using BLL;
using Model;
using System.Collections;

namespace OrgWebSite.Admin.Popup
{
    public partial class UserExtAction : BasePage
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
            //DataTable dt = DBManager.GetAllExtProp();
            UserProfileBLL bll = new UserProfileBLL();
            IList<UserProfileExtPropertyInfo> dt = bll.GetAllExtProp();
            gvForms.DataSource = dt;
            gvForms.DataBind();

            hfSelectedExtPropID.Value = "-1";
            txtProName.Text = string.Empty;
            txtProDes.Text = string.Empty;
            txtProCom.Text = string.Empty;
        }

        protected void gvForms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvForms.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void gvForms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;
            string formCode = e.CommandArgument.ToString();

            switch (commandName)
            {
                case "E":
                    EditForm(formCode, e);
                    break;
            }
        }

        private void EditForm(string extPropID, GridViewCommandEventArgs e)
        {
            btnSave.Text = "保存";
            hfSelectedExtPropID.Value = extPropID;

            //取得选择的页面
            GridViewRow row = ((e.CommandSource as LinkButton).Parent.Parent as GridViewRow);
            if (row != null)
            {
                txtProName.Text = Server.HtmlDecode(row.Cells[1].Text);
                txtProDes.Text = Server.HtmlDecode(row.Cells[2].Text);
                txtProCom.Text = Server.HtmlDecode(row.Cells[3].Text);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserProfileBLL bll = new UserProfileBLL();
            if (hfSelectedExtPropID.Value != "-1")
            {
                //更新记录
                //DBManager.UpdateExtPropById(hfSelectedExtPropID.Value, txtProName.Text, txtProDes.Text, txtProCom.Text);
                bll.UpdateExtPropById(hfSelectedExtPropID.Value, txtProName.Text, txtProDes.Text, txtProCom.Text);
            }
            else
            {
                //添加记录
                //DBManager.AddExtProp(txtProName.Text, txtProDes.Text, txtProCom.Text, Page.User.Identity.Name);
                bll.AddExtProp(txtProName.Text, txtProDes.Text, txtProCom.Text);
            }
            BindData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string formCodes = "";
            for (int i = 0; i < gvForms.Rows.Count; i++)
            {
                CheckBox chkUser = (CheckBox)(gvForms.Rows[i].FindControl("chkForm"));
                if (chkUser.Checked)
                {
                    HiddenField hfFormCode = (HiddenField)(gvForms.Rows[i].FindControl("hfFormCode"));
                    formCodes += hfFormCode.Value + ";";
                }
            }

            if (formCodes.LastIndexOf(';') == (formCodes.Length - 1))
                formCodes = formCodes.Remove(formCodes.LastIndexOf(';'));

            if (formCodes != "")
            {
                //DBManager.DeleteExtProp(formCodes);
                UserProfileBLL bll = new UserProfileBLL();
                bll.DeleteExtProp(formCodes);
                BindData();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            txtProName.Text = string.Empty;
            txtProDes.Text = string.Empty;
            txtProCom.Text = string.Empty;
            btnSave.Text = "添加";
        }
    }
}