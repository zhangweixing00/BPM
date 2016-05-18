using System;
using System.Data;
using Pkurg.PWorldBPM.Business.Controls;

public partial class Sys_ProcessesStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            Bind(Request.QueryString["ID"].ToString());
        }
    }

    private void Bind(string p)
    {
        
    }

    protected void btnstop_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = WF_GetRelatedLinks.UpdateProcessesStatus(Request.QueryString["ID"].ToString());

        Response.Redirect("~/Sys/ProcessesManage_List.aspx");
    }
    protected void btnkillstop_Click(object sender, EventArgs e)
    {

    }
}