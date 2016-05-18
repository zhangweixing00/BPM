using System;
using System.Data;
using System.Data.SqlClient;
using Pkurg.PWorld.Business.Common;

public partial class Sys_AppManage_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Start()
    {
        string strAppName = txtAppName.Text;
        string strWorkFlowName = txtWorkFlowName.Text;
        string strFormName = txtFormName.Text;
        int isOpen = cbIsOpen.Checked ? 1 : 0;
        DataProvider dataProvider = new DataProvider();
        dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        DataTable dataTable = new DataTable();
        dataTable = dataProvider.ExecuteDataTable("select * from [WF_AppDict]", CommandType.Text);
        int AppId = Convert.ToInt32(dataTable.Rows[dataTable.Rows.Count - 1]["AppId"]);
        DataTable dataTableVerification = new DataTable();
        dataTableVerification = dataProvider.ExecuteDataTable(string.Format("select 1 from [WF_AppDict] where AppName='{0}'", strAppName), CommandType.Text);
        if (dataTableVerification.Rows.Count > 0)
        {
            DisplayMessage.ExecuteJs("alert('应用名称不能与已经存在流程定义重复！')");
        }
        else
        {
            SqlCommand sqlCommandInsert = new SqlCommand();
            dataProvider.ExecuteNonQuery(string.Format("insert into [WF_AppDict] values ({0},'{1}',1,'{2}','{3}','{4}')", AppId + 1, strAppName, strWorkFlowName, strFormName,isOpen), CommandType.Text);
            DisplayMessage.ExecuteJs("alert('新增流程定义成功！');window.location.href='AppManage_List.aspx';");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Start();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Sys/AppManage_List.aspx");
    }
}