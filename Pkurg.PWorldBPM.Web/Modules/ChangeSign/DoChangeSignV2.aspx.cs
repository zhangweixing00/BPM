using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;

public partial class Modules_ChangeSign_DoChangeSignV2 : UPageBase
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
        int result = 0;
        try
        {
            string userDomainName = e.CommandArgument.ToString();
            if (userDomainName.ToLower().Contains("founder\\"))
            {
                userDomainName = "founder\\" + userDomainName;
            }

            string Opinion = Request["optionTxt"];

            var appInfo = AppflowFactory.GetAppflow(_BPMContext.ProcID);
            appInfo.IsFromPC = true;
            bool isSuccess = appInfo.ChangeSign(_BPMContext.CurrentUser.LoginId, userDomainName, _BPMContext.Sn, _BPMContext.ProcID, Opinion);
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

     protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        Refresh(tbUserName.Text, tbLoginName.Text);
    }
}