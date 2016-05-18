using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Sys_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();

            //Test Code
            //BPMHelp help = new BPMHelp();
            //string val = help.GetSerialNumber("QSD");
        }
    }

    private void BindList()
    {
        //使用对象初始化器,初始化一些示范数据。
        //实际这个数据源来自数据库。
        var data = new List<Demo> { 
            new Demo{ ID=1,Name="Name1", Remark="Remark1"},
            new Demo{ ID=2,Name="Name2", Remark="Remark2"},
            new Demo{ ID=3,Name="Name3", Remark="Remark3"},
            new Demo{ ID=4,Name="Name4", Remark="Remark4"},
            new Demo{ ID=5,Name="Name5", Remark="Remark5"},
            new Demo{ ID=6,Name="Name6", Remark="Remark6"}
        };

        rptList.DataSource = data;
        rptList.DataBind();
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Sys/Edit.aspx");
    }

    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            //从数据库删除
        }
        else if (e.CommandName == "Edit")
        {
            //跳转到编辑页面
            Response.Redirect("~/Sys/Edit.aspx?ID=" + e.CommandArgument.ToString());
        }
        else if (e.CommandName == "View")
        {
            //跳转到查看页面
            Response.Redirect("~/Sys/View.aspx?ID=" + e.CommandArgument.ToString());
        }
    }
}


/// <summary>
/// 自定义类，映射数据库的表
/// </summary>
public class Demo
{
    public int ID
    {
        get;
        set;
    }

    public string Name
    {
        get;
        set;
    }

    public string Remark
    {
        get;
        set;
    }
}