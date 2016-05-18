using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.Sys;

public partial class Modules_BaseData_EmployeeOperation : UPageBase
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
    /// <summary>
    /// 刷新页面
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="loginName"></param>
    private void Refresh(string userName, string loginName)
    {
        BFPmsUserRoleDepartment ur = new BFPmsUserRoleDepartment();
        IList<VEmployeeAndAdditionalInfo> ds = ur.GetNotSelectRoleUser("0");

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

    protected void lbSelected_Command(object sender, CommandEventArgs e)
    {
        string result = "";
        try
        {
            string userDomainName = e.CommandArgument.ToString();
            var uInfo= SysContext.V_Pworld_UserInfo.FirstOrDefault(x => x.LoginName == userDomainName);
            result = JsonHelper.JsonSerializer<V_Pworld_UserInfo>(uInfo);
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
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        Refresh(tbUserName.Text, tbLoginName.Text);
    }
}