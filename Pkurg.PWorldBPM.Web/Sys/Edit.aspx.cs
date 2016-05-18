using System;

public partial class Sys_Edit : System.Web.UI.Page
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
            else
            { 
                //新建模式，新建没有ID参数
            }
        }
    }

    private void Bind(int id)
    {
       //根据ID获取到数据库的记录

        txtName.Text = "Name1";
        txtRemark.Text = "Remark1";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //保存到数据库

        Response.Redirect("~/Sys/List.aspx");
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Sys/List.aspx");
    }
}