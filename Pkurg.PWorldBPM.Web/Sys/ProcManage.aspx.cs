using System;

public partial class Sys_ProcManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Abort(string instanceId)
    {
        //
        WorkflowManage.Abort();
    }
}