using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using BLL;
using Model;

namespace OrgWebSite.Admin.Popup
{
    public partial class RoleAction : BasePage
    {
        public string RoleCode
        {
            get
            {
                if (Request.QueryString["roleCode"] != null)
                    return Request.QueryString["roleCode"];

                return "";
            }
        }

        public string Action
        {
            get
            {
                if (Request.QueryString["action"] != null)
                    return Request.QueryString["action"];

                return "";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProcess();
                BindOrg();
                if (Action.ToUpper() == "EDIT")
                {
                    //DataTable dt = DBManager.GetRoleByRoleCode(RoleCode);

                    RoleBLL bll = new RoleBLL();
                    RoleInfo info = bll.GetRoleByRoleCode(RoleCode);
                    txtRoleName.Text = info.RoleName;
                    ddlProcess.SelectedIndex = ddlProcess.Items.IndexOf(ddlProcess.Items.FindByValue(info.ProcessCode.ToString()));
                }
            }
        }

        private void BindOrg()
        {
            OrganizationBLL bll = new OrganizationBLL();
            ddlOrg.DataSource = bll.GetOrgList();
            ddlOrg.DataBind();
            ddlOrg.Items.Insert(0, new ListItem("Choose", ""));
        }

        private void BindProcess()
        {
            //DataTable dt = DBManager.GetProcessType();
            ProcessTypeBLL bll = new ProcessTypeBLL();
            IList<ProcessTypeInfo> ptfList = bll.GetProcessType();
            ddlProcess.DataSource = ptfList;
            ddlProcess.DataTextField = "ProcessType";
            ddlProcess.DataValueField = "ID";
            ddlProcess.DataBind();

            ddlProcess.Items.Insert(0, new ListItem("Choose", ""));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            RoleBLL bll = new RoleBLL();
            if (Action.ToUpper() == "EDIT")
                //DBManager.UpdateRole(RoleCode, txtRoleName.Text, ddlProcess.SelectedValue);
                bll.UpdateRole(RoleCode, txtRoleName.Text, ddlProcess.SelectedValue, ddlOrg.SelectedValue,"");
            else
                //DBManager.AddNewRole(txtRoleName.Text, ddlProcess.SelectedValue);
                bll.AddNewRole(txtRoleName.Text, ddlProcess.SelectedValue, ddlOrg.SelectedValue,"");

            litScript.Text = "<script>window.returnValue='';window.close();</script>";
        }
    }
}