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

namespace OrgWebSite.Admin.Popup
{
    public partial class SelectDeptMuti : BasePage
    {
        private string DeptCode
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["DeptCode"]))
                {
                    return Request.QueryString["DeptCode"];
                }
                return string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            hfSelectedDept.Value = string.Empty;
            DepartmentBLL bll = new DepartmentBLL();
            IList<DepartmentInfo> dept = bll.GetDepartmentInfo();
            //DataTable dt = DBManager.GetDepartmentInfo();
            if (dept != null)
            {
                gvDept.DataSource = dept;
                gvDept.DataBind();
            }
        }

        protected void gvDept_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(DeptCode))
                {
                    string[] deptCode = DeptCode.Split(new char[] { ';' });
                    HiddenField hfDeptCode = e.Row.FindControl("hfDeptCode") as HiddenField;
                    if (hfDeptCode != null)
                    {
                        for (int i = 0; i < deptCode.Length; i++)
                        {
                            if (hfDeptCode.Value.Equals(deptCode[i]))
                            {
                                CheckBox cbDept = e.Row.FindControl("chkDept") as CheckBox;
                                if (cbDept != null)
                                {
                                    cbDept.Checked = true;
                                    hfSelectedDept.Value += deptCode[i] + ";";
                                    HiddenField hfDeptName = e.Row.FindControl("hfDeptName") as HiddenField;
                                    if (hfSelectedDeptName != null)
                                        hfSelectedDeptName.Value += hfDeptName.Value + ";";
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}