using System;

public partial class UserControls_Header : System.Web.UI.UserControl
{
    LoginUser bll = new LoginUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
   
}