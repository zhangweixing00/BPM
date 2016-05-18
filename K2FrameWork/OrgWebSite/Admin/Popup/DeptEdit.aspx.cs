using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Utility;
using BLL;
using Model;

namespace OrgWebSite.Admin.Popup
{
    public partial class DeptEdit : BasePage, ICallbackEventHandler
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

        protected string orgCode
        {
            get
            {
                try
                {
                    return Request.QueryString["orgCode"].Trim();
                }
                catch
                {
                    return null;
                }
            }
        }

        private string action
        {
            get
            {
                return Request.QueryString["action"].Trim();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfAction.Value = action;
                BindOrderNO();
                BindDept();
                BindDeptType();
                if (deptCode != null && deptCode.Length > 0 && action == "edit")
                {
                    Page.Title = "部门编辑";
                    InitDeptInfo();
                }
            }
        }

        int subDeptCount = 0;
        private void BindOrderNO()
        {
            //DataTable dt = DBManager.GetSubDepartments(deptCode);
            DepartmentBLL deptBLL = new DepartmentBLL();
            IList<DepartmentInfo> dept = deptBLL.GetSubDepartments(orgCode, deptCode);
            if (dept != null && dept.Count > 0)
            {
                txtOrderNo.Text = dept[0].OrderNo.ToString();
            }
        }

        private void BindDeptType()
        {
            //DataTable dt = DBManager.GetDeptType();
            DepartmentBLL deptBLL = new DepartmentBLL();
            DataTable dt = deptBLL.GetDeptType();
            ddlDeptType.DataSource = dt;
            ddlDeptType.DataTextField = "DeptType";
            ddlDeptType.DataValueField = "DeptTypeCode";
            ddlDeptType.DataBind();

            ddlDeptType.Items.Insert(0, new ListItem("Choose", ""));
            ddlDeptType.SelectedIndex = 0;
        }

        private void BindDept()
        {
            //DataTable dt = DBManager.GetSortDepartment();
            DepartmentBLL bll = new DepartmentBLL();
            DataTable dt = bll.GetSortDepartment(orgCode);

            ddlDepts.DataSource = dt;
            ddlDepts.DataTextField = "sortname";
            ddlDepts.DataValueField = "DeptCode";
            ddlDepts.DataBind();

            ddlDepts.Items.Insert(0, new ListItem("Root", "R0"));
            ddlDepts.SelectedIndex = ddlDepts.Items.IndexOf(ddlDepts.Items.FindByValue(deptCode));
            hfOriDeptCode.Value = deptCode;
        }

        private void InitDeptInfo()
        {
            //Department currentDepartment = DBManager.GetDepartmentInfo(deptCode);

            DepartmentBLL bll = new DepartmentBLL();
            DepartmentInfo currentDepartment = bll.GetDepartmentInfo(deptCode);

            txtCode.Text = currentDepartment.Code;
            txtDepartment.Text = currentDepartment.DeptName;
            txtAbbreviation.Text = currentDepartment.Abbreviation;
            rblState.SelectedIndex = rblState.Items.IndexOf(rblState.Items.FindByValue(currentDepartment.State.ToString()));
            ddlDepts.SelectedIndex = ddlDepts.Items.IndexOf(ddlDepts.Items.FindByValue(currentDepartment.ParentCode));
            ddlDeptType.SelectedIndex = ddlDeptType.Items.IndexOf(ddlDeptType.Items.FindByValue(currentDepartment.DeptTypeCode));
            txtOrderNo.Text = currentDepartment.OrderNo.ToString();
            hfOriDeptCode.Value = currentDepartment.ParentCode;

            //DataTable dt = DBManager.GetSubDepartments(currentDepartment.ParentCode);
            //IList<DepartmentInfo> dt = bll.GetSubDepartments(orgCode, deptCode);

            if (currentDepartment.ParentCode == "R0")
            {
                rblState.Enabled = false;
                ddlDepts.Enabled = false;
                txtOrderNo.Enabled = false;
            }
            else
            {
                rblState.Enabled = true;
                ddlDepts.Enabled = true;
                txtOrderNo.Enabled = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool ret = false;
            if (deptCode != null && deptCode.Length > 0)
            {
                if (action == "new")
                {
                    DepartmentInfo department = new DepartmentInfo();
                    department.DeptCode = deptCode;
                    department.ParentCode = ddlDepts.SelectedValue;
                    department.Code = txtCode.Text;
                    department.DeptName = txtDepartment.Text;
                    department.Abbreviation = txtAbbreviation.Text;
                    department.DeptTypeCode = ddlDeptType.SelectedValue;
                    department.OrderNo = int.Parse(txtOrderNo.Text);
                    department.OrgID = new Guid(orgCode);

                    //DBManager.CreateDepartment(department, txtOrderNo.Text, action);
                    DepartmentBLL bll = new DepartmentBLL();
                    ret = bll.CreateDepartment(department, txtOrderNo.Text, action);
                }
                else if (action == "edit")
                {
                    //Department department = DBManager.GetDepartmentInfo(deptCode);
                    DepartmentInfo department = new DepartmentInfo();
                    department.DeptCode = deptCode;
                    department.Code = txtCode.Text;
                    department.DeptName = txtDepartment.Text;
                    department.Abbreviation = txtAbbreviation.Text;
                    department.State = Convert.ToInt32(rblState.SelectedValue);
                    department.ParentCode = ddlDepts.SelectedValue;
                    department.DeptTypeCode = ddlDeptType.SelectedValue;
                    department.OrgID = new Guid(orgCode);
                    if (txtOrderNo.Enabled)
                        department.OrderNo = int.Parse(txtOrderNo.Text);
                    else
                        department.OrderNo = 0;

                    //DBManager.UpdateDepartment(department, txtOrderNo.Text, action);
                    DepartmentBLL bll = new DepartmentBLL();
                    ret = bll.UpdateDepartment(department, txtOrderNo.Text, action);
                }
            }

            if (ret)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>top.ymPrompt.doHandler('ok', false);</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>top.ymPrompt.doHandler('failed', false);</script>");
            }
        }

        #region ICallbackEventHandler Members

        int count = 0;
        public string GetCallbackResult()
        {
            return count.ToString(); ;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            //DataTable dt = DBManager.GetSubDepartments(eventArgument);
            DepartmentBLL bll = new DepartmentBLL();
            IList<DepartmentInfo> dt = bll.GetSubDepartments(orgCode, deptCode);

            if (dt != null)
            {
                count = dt.Count;
            }
        }

        #endregion
    }
}