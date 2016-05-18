using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.Business.Controls;

public partial class Sys_ProcessesManage_List :UPageBase
{
    readonly string K2ServerName = ConfigurationManager.AppSettings["K2ServerName"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Query();
        }
    }

    public static int sumcount;

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = 1;
        Query();
    }

    private void Query()
    {
        string endTime = "";
        if (!string.IsNullOrEmpty(tbxEndTime.Value.ToString()))
        {
            endTime = (Convert.ToDateTime(tbxEndTime.Value.ToString()).AddDays(1)).ToString();
        }
        string companyCode = GetUserManageCompanyCode();

        int userRight = GetUserRight();
        if (string.IsNullOrEmpty(companyCode) && userRight == 0)
        {
            DisplayMessage.ExecuteJs("alert('无权查询！');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
            return;
        }

        int count = 0;
        DataTable dataTable = Sys_ProcessManage.GetInstanceListByParamsPaged(tbxTitle.Text.Trim(), txtCreateName.Text.Trim(), tbxBeginTime.Value.Trim().ToString(),
          endTime, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, tbxStatus.Text.Trim(), tbxNumBer.Text.Trim(), companyCode,userRight,out count);

        if (dataTable.Rows.Count > 0)
        {
            AspNetPager1.RecordCount = count;
            this.rptList.DataSource = dataTable;
            this.rptList.DataBind();
            lblshow.Visible = false;
            rptList.Visible = true;
            AspNetPager1.Visible = true;
        }
        else
        {
            lblshow.Visible = true;
            rptList.Visible = false;
            AspNetPager1.Visible = false;
        }
    }

    private string GetUserManageCompanyCode()
    {
        Pkurg.PWorld.Entities.TList<Pkurg.PWorld.Entities.Department> deptInfos = new Pkurg.PWorld.Business.Permission.BFPmsUserRoleDepartment().
            GetDeptListByEmployeeCodeAndRoleName(_BPMContext.CurrentPWordUser.EmployeeCode, "实例管理员");
        if (deptInfos.Count==0)
        {
            return "";
        }
        StringBuilder companyCode = new StringBuilder();
        companyCode.Append(",");
        foreach (var item in deptInfos)
        {
            companyCode.AppendFormat("{0},", item.DepartCode);
        }
        return companyCode.ToString();
    }

    private int GetUserRight()
    {
        if (IdentityUser.CheckPermission("SuperAdmins"))
        {
            return -1;
        }
        else if (IdentityUser.CheckPermission("OAAdmin"))
        {
            return 1;
        }
        else if (IdentityUser.CheckPermission("ERPAdmin"))
        {
            return 2;
        }
        return 0;
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Query();
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblProId = (Label)e.Item.FindControl("lblProId");
            HyperLink hyperLink = (HyperLink)e.Item.FindControl("hyperLink");
            hyperLink.NavigateUrl = "/Workflow/ViewPage/ViewPageHandler.ashx?ID=" + lblProId.Text;
        }
    }
}
