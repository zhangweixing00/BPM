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
    public partial class RoleUserAction : BasePage
    {
        private string RoleCode
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["RoleCode"]))
                    return Request.QueryString["RoleCode"];
                return string.Empty;
            }
        }

        private string UserCode
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["UserCode"]))
                    return Request.QueryString["UserCode"];
                return string.Empty;
            }
        }

        private string Op
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["op"]))
                    return Request.QueryString["op"];
                return string.Empty;
            }
        }

        private string ID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    hfID.Value = Request.QueryString["ID"];
                }
                else
                {
                    hfID.Value = Guid.Empty.ToString();
                }
                return hfID.Value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(UserCode) && !string.IsNullOrEmpty(RoleCode) && !string.IsNullOrEmpty(Op) && !string.IsNullOrEmpty(ID))
                {
                    Bind();
                }
            }
        }

        /// <summary>
        ///  绑定
        /// </summary>
        private void Bind()
        {
            if (Op.Equals("Edit", StringComparison.CurrentCultureIgnoreCase))   //编辑状态
            {
                //DataSet ds = DBManager.GetRoleUser(RoleCode, UserCode);
                RoleBLL bll = new RoleBLL();
                DataSet ds = bll.GetRoleUser(RoleCode, UserCode);
                if (ds != null && ds.Tables.Count > 0)
                {
                    txtRoleName.Text = ds.Tables[0].Rows[0]["RoleName"].ToString();
                    txtRoleUser.Text = ds.Tables[0].Rows[0]["CHName"].ToString();
                    txtDeptName.Text = ds.Tables[0].Rows[0]["DeptName"].ToString();
                    hfDeptCode.Value = ds.Tables[0].Rows[0]["DeptCode"].ToString();
                    hfRoleUser.Value = ds.Tables[0].Rows[0]["upad"].ToString();
                    txtExpand1.Text = ds.Tables[0].Rows[0]["ExpandField1"].ToString();
                    txtExpand2.Text = ds.Tables[0].Rows[0]["ExpandField2"].ToString();
                    txtExpand3.Text = ds.Tables[0].Rows[0]["ExpandField3"].ToString();
                    txtExpand4.Text = ds.Tables[0].Rows[0]["ExpandField4"].ToString();
                    ddlDutyRegion.Text = ds.Tables[0].Rows[0]["DutyRegion"].ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //保存结果
            //bool ret = DBManager.UpdateRoleUserByID(int.Parse(hfID.Value), hfRoleUser.Value, txtDeptName.Text, hfDeptCode.Value, string.Empty, string.Empty, txtExpand1.Text, txtExpand2.Text, txtExpand3.Text, txtExpand4.Text);
            RoleBLL bll = new RoleBLL();
            bool ret = bll.UpdateRoleUserByID(hfID.Value, hfRoleUser.Value, txtDeptName.Text, hfDeptCode.Value, ddlDutyRegion.Text, string.Empty, string.Empty, txtExpand1.Text, txtExpand2.Text, txtExpand3.Text, txtExpand4.Text);
            if (ret)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "JS", "<script>top.ymPrompt.doHandler('ok', true);</script>");
            }
            else
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "JS", "alert('保存失败');", true);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "JS", "<script>top.ymPrompt.doHandler('failed', true);</script>");
            }
        }
    }
}