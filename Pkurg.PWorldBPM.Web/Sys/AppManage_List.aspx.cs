using System;
using System.Data;
using System.Web.UI.WebControls;
using Pkurg.PWorld.Business.Common;
using Pkurg.PWorldBPM.Business.Controls;

public partial class Sys_AppManage_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dataTable = new DataTable();
            dataTable = WF_AppDictManager.GetAppDictToDataTable();
            rptList.DataSource = dataTable;
            rptList.DataBind();
        }
    }
    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            dataProvider.ExecuteNonQuery("delete from [WF_AppDict] where AppID=" + e.CommandArgument.ToString(), CommandType.Text);
            Response.Redirect("~/Sys/AppManage_List.aspx");
        }
        else if (e.CommandName == "Edit")
        {
            //跳转到编辑页面
            Response.Redirect("~/Sys/AppManage_Edit.aspx?AppID=" + e.CommandArgument.ToString());
        }
    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Sys/AppManage_Add.aspx");
    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        string appId = txtAppID.Value.Trim();
        string appName = txtAppName.Value.Trim();
        string appWorkFlow = txtWorkFlow.Value.Trim();
        string appFormName = txtFormName.Value.Trim();

        if (appId == "应用号")
        {
            appId = "";
        }
        if (appName == "应用名称")
        {
            appName = "";
        }
        if (appWorkFlow == "工作流名称")
        {
            appWorkFlow = "";
        }
        if (appFormName == "表单名称")
        {
            appFormName = "";
        }

        DataTable dataTable = new DataTable();
        dataTable = WF_AppDictManager.GetAppListByAppIdOrAppNameOrWorkFlowNameOrFormName(appId, appName, appWorkFlow, appFormName);
        if (dataTable.Rows.Count == 0)
        {
            DisplayMessage.ExecuteJs("alert('没有匹配的流程应用！');window.location.href='AppManage_List.aspx';");
        }

        rptList.DataSource = dataTable;
        rptList.DataBind();

        txtAppID.Value = "应用号";
        txtAppName.Value = "应用名称";
        txtWorkFlow.Value = "工作流名称";
        txtFormName.Value = "表单名称";
    }
}