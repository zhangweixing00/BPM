using System;
using System.Data;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.Business.Controls;

public partial class Sys_ProcessEndManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dataTable = new DataTable();
            dataTable = ProcessEndManage.GetAllEndInfos();
            rptList.DataSource = dataTable;
            rptList.DataBind();
        }
    }
    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            //跳转到编辑页面
            Response.Redirect("~/Sys/ProcessEndManage_E.aspx?appId=" + e.CommandArgument.ToString());
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Query();
    }
    private void Query()
    {
        DataTable dataTable = new DataTable();
        dataTable = ProcessEndManage.GetSomeInfoByOthers(txtAppName.Text.ToString().Trim());
        if (dataTable.Rows.Count > 0)
        {
            rptList.Visible = true;
            lblshow.Visible = false;
            this.rptList.DataSource = dataTable;
            this.rptList.DataBind();
        }
        else
        {
            lblshow.Visible = true;
            rptList.Visible = false;
        }
    }

}
