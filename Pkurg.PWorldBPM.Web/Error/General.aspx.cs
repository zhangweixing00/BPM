using System;
using System.Web;

public partial class Error_General : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["error"]))
            {
                lblTitle.Visible = true;
                lblMsg.Text = HttpContext.Current.Server.HtmlDecode(Request.QueryString["error"]);
            }
        }
    }
}