using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using K2Utility;
using BLL;
using Model;


namespace Sohu.OA.Web
{
    /// <summary>
    ///  create:康亚兵
    ///  date：2011-09-06
    ///  description：角色管理
    /// </summary>
    public partial class MenuRoleAdd : BasePage
    {
        public string RoleCode
        {
            get
            {
                if (Request.QueryString["roleid"] != null)
                    return Request.QueryString["roleid"];

                return "";
            }
        }

        public string Action
        {
            get
            {
                if (Request.QueryString["oper"] != null)
                    return Request.QueryString["oper"];

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
                    lblOperName.Text = "修改角色";
                    RoleBLL bll = new RoleBLL();
                    RoleInfo info = bll.GetRoleByRoleCode(RoleCode);
                    txtRoleName.Text = info.RoleName;
                    Description.Text = info.Desciption;
                    ddlOrg.SelectedValue = info.OrgID.ToString();
                    ddlProcess.SelectedValue = info.ProcessCode.ToString();
                }
                else if (Action == "add")
                {
                    lblOperName.Text = "添加角色";
                    //lblRoleName.Text = "*";
                }
            }
        }
        private void BindOrg()
        {
            OrganizationBLL bll = new OrganizationBLL();
            ddlOrg.DataSource = bll.GetOrgList();
            ddlOrg.DataBind();
            ddlOrg.Items.Insert(0, new ListItem("选择", ""));
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

            ddlProcess.Items.Insert(0, new ListItem("选择", ""));
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            RoleBLL bll = new RoleBLL();
            if (Action.ToUpper() == "EDIT")
            {
                if (bll.UpdateRole(RoleCode, txtRoleName.Text, ddlProcess.SelectedValue, ddlOrg.SelectedValue,Description.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('更新成功');</script>");
                }
                else
                {
                    ExecAlertScritp("更新失败！");
                }
            }
            else
            {
                if (bll.AddNewRole(txtRoleName.Text, ddlProcess.SelectedValue, ddlOrg.SelectedValue, Description.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('添加成功');</script>");
                }
                else
                {
                    ExecAlertScritp("添加失败！");
                }
            }

        }
        /// <summary>
        /// 添加角色或者修改角色保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnSave_Click(object sender, ImageClickEventArgs e)
        //{
            //if (RoleCode.Text.ToString().Trim() == "")
            //{
            //    ExecAlertScritp("角色编码不能为空！");
            //    return;
            //}
            //if (RoleName.Text.ToString().Trim() == "")
            //{
            //    ExecAlertScritp("角色名称不能为空！");
            //    return;
            //}
            //string operFlag = Request.QueryString["oper"].ToString();
            //if (operFlag == "add")
            //{
            //    if (RoleCode.Text.ToString().Trim().Contains("&") || RoleCode.Text.ToString().Trim().Contains(";"))
            //    {
            //        ExecAlertScritp("角色编码不能包含特殊符号 & ; ！");
            //        return;
            //    }
            //    if (Biz.Biz_T_Role.biz_t_role.IsRoleCodeExist(RoleCode.Text.Trim()))
            //    {
            //        ExecAlertScritp("角色编码已存在！");
            //        return;
            //    }
            //    if (Biz.Biz_T_Role.biz_t_role.IsRoleNameExist(RoleName.Text.Trim()))
            //    {
            //        ExecAlertScritp("角色名称已存在！");
            //        return;
            //    }
            //    if (Biz.Biz_T_Role.biz_t_role.InsertRole(this.role_detail))
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('添加成功');</script>");
            //    }
            //    else
            //    {
            //        ExecAlertScritp("添加失败！");
            //    }
            //}
            //else if (operFlag == "edit")
            //{
            //    if (this.hfRoleNameValue.Value != RoleName.Text.ToString().Trim())
            //    {
            //        if (Biz.Biz_T_Role.biz_t_role.IsRoleNameExist(RoleName.Text.Trim()))
            //        {
            //            ExecAlertScritp("角色名称已存在！");
            //            return;
            //        }
            //    }
            //    if (Biz.Biz_T_Role.biz_t_role.UpdateRole(this.role_detail, Request.QueryString["roleid"].ToString()))
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('更新成功');</script>");
            //    }
            //    else
            //    {
            //        ExecAlertScritp("更新失败！");
            //    }
            //}
        //}
    }
}