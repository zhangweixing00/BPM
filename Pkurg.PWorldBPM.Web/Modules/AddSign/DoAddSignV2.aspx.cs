using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;

public partial class Modules_AddSign_DoAddSignV2 : UPageBase
{
    #region 声明

    BFPmsPermission pms = new BFPmsPermission();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Refresh("", "");
        }


    }


    protected void lbSelected_Command(object sender, CommandEventArgs e)
    {
        int result = 0;
        try
        {
            string userDomainName = e.CommandArgument.ToString();
            //DisplayMessage.ExecuteJs("alert('" + userDomainName + "')");
            //return;
            if (userDomainName.ToLower().Contains("founder\\"))
            {
                userDomainName = "founder\\" + userDomainName;
            }
            //2015-1-26 对加签的已办做增加Item处理，去掉为空意见优化
            string Opinion = Request["optionTxt"];
            var appInfo = AppflowFactory.GetAppflow(_BPMContext.ProcID);
            appInfo.IsFromPC = true;
            bool isSuccess = appInfo.AddSign(_BPMContext.CurrentUser.LoginId, userDomainName,_BPMContext.Sn, _BPMContext.ProcID, Opinion);
            if (isSuccess)
            {
                result = 1;
            }
        }
        catch (Exception)
        {

        }
        DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", result));
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Refresh(tbUserName.Text, tbLoginName.Text);
    }

    private void Refresh(string userName, string loginName)
    {
        BFPmsUserRoleDepartment ur = new BFPmsUserRoleDepartment();
        IList<VEmployeeAndAdditionalInfo> ds = ur.GetNotSelectRoleUser("0");

        string deptIdString = Request["deptid"];
        if (string.IsNullOrEmpty(deptIdString) || hd_SelectType.Value=="1")
        {
            //加签
        }
        else
        {
            //部门内处理
            BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
            DataTable dtUsers = bfurd.GetSelectRoleUser(deptIdString, "部门成员");

            
            if (dtUsers != null && dtUsers.Rows.Count>0)
            {
                ds = new List<VEmployeeAndAdditionalInfo>();
                foreach (DataRow item in dtUsers.Rows)
                {
                    ds.Add(new VEmployeeAndAdditionalInfo()
                    {
                        LoginName = item["LoginName"].ToString(),
                        DepartName = item["DepartName"].ToString(),
                        EmployeeName = item["EmployeeName"].ToString()
                    });
                }
                if (dtUsers.Rows.Count <= 0)
                {
                    ds = ds.Where(x => x.DepartCode == deptIdString).ToList();
                }
            }
            else 
            {
                ds = ds.Where(x => x.DepartCode == deptIdString).ToList();
            }

            //gvData.DataSource = dtUsers;
            //gvData.DataBind();
            //return;
        }

        if (!string.IsNullOrEmpty(userName))
        {
            ds = ds.Where(x => x.EmployeeName.Contains(userName)).ToList();
        }
        if (!string.IsNullOrEmpty(loginName))
        {
            ds = ds.Where(x => x.LoginName.Contains(loginName)).ToList();
        }
        gvData.DataSource = ds;
        gvData.DataBind();
    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        Refresh(tbUserName.Text, tbLoginName.Text);
    }
    protected void btnMore_Click(object sender, EventArgs e)
    {
        hd_SelectType.Value = "1";
        Refresh(tbUserName.Text, tbLoginName.Text);
    }
}
