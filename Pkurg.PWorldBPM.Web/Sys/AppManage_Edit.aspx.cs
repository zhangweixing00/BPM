using System;
using System.Data;
using Pkurg.PWorld.Business.Common;
using Pkurg.PWorldBPM.Business.Controls;

public partial class Sys_AppManage_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["AppID"]))
            {
                Bind(Request.QueryString["AppID"]);
            }
            else
            {
                Response.Redirect("~/Sys/AppManage_Add.aspx");
            }
        }
    }
    private void Bind(string appId)
    {
        DataTable dataTable = new DataTable();
        dataTable = WF_AppDictManager.GetAppDictByAppID(appId);
        txtAppName.Text = dataTable.Rows[0]["AppName"].ToString();
        txtWorkFlowName.Text = dataTable.Rows[0]["WorkFlowName"].ToString();
        txtFormName.Text = dataTable.Rows[0]["FormName"].ToString();
        hideAppName.Value = dataTable.Rows[0]["AppName"].ToString();
        cbIsOpen.Checked = dataTable.Rows[0]["IsOpen"] != null && dataTable.Rows[0]["IsOpen"].ToString() == "1";
    }

    protected void Save(string appId, string appName, string workFlowName, string formName)
    {
        string appNameBefore = hideAppName.Value;
        if (appNameBefore == appName)
        {
            WF_AppDictManager.EditAppDict(appId, appName, workFlowName, formName,cbIsOpen.Checked);
            DisplayMessage.ExecuteJs("alert('保存成功！');window.location.href='AppManage_List.aspx';");
        }
        else
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            DataTable dataTableVerification = new DataTable();
            dataTableVerification = dataProvider.ExecuteDataTable(string.Format("select 1 from [WF_AppDict] where AppName='{0}'", appName), CommandType.Text);
            if (dataTableVerification.Rows.Count > 0)
            {
                DisplayMessage.ExecuteJs("alert('应用名称已经存在流程定义重复！')");

            }
            else
            {
                WF_AppDictManager.EditAppDict(appId, appName, workFlowName, formName,cbIsOpen.Checked);
                DisplayMessage.ExecuteJs("alert('保存成功！');window.location.href='AppManage_List.aspx';");
            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string appId = Request.QueryString["AppID"];
        string appName = txtAppName.Text;
        string workFlowName = txtWorkFlowName.Text;
        string formName = txtFormName.Text;
        Save(appId, appName, workFlowName, formName);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Sys/AppManage_List.aspx");
    }
}