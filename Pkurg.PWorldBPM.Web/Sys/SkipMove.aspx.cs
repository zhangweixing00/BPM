using System;
using System.Collections.Generic;

public partial class Sys_SkipMove : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.DataSource = Anode.getlist();
        GridView1.DataBind();
    }
}
public class Anode
{
    public int i { get; set; }
    public string b { get; set; }
    public static List<Anode> getlist()
    {
        List<Anode> list = new List<Anode>();
        list.Add(new Anode()
        {
            i = 1,
            b = "q"
        });
        list.Add(new Anode()
        {
            i = 2,
            b = "e"
        });
        return list;
    }
}
