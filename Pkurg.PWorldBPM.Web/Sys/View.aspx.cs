using System;

public partial class Sys_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //修改模式
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                Bind(Convert.ToInt16(Request.QueryString["ID"]));
            }

        }
    }

    private void Bind(int id )
    {
        //根据ID获取到数据库的记录

        lblName.Text = "Name1";
        lblRemark.Text = "Remark1";
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Sys/List.aspx");
    }
}