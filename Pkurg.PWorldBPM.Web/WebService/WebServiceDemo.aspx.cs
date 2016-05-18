using System;

public partial class WebService_WebServiceDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        string INST_ID = txtINST_ID.Text;
        string USER_ID = txtUSER_ID.Text;
        string SP = txtSP.Text;
       
        WebService webService = new WebService();
        bool isSuccess = webService.CreateNewFormByInstanceIDAndEmployeeCodeWithStoredProcedure(INST_ID, USER_ID, SP);
        if (isSuccess)
        {
            Response.Write("<Script Language=JavaScript>alert('触发成功！');</Script>");
        }
    }
}