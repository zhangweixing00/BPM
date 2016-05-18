using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public partial class Workflow_AuthorizationSelectPersons : UPageBase
{
    private string employeeName;
    private string loginName;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblProcId.Text = Request.QueryString["ProcID"];
            lblProcName.Text = Request.QueryString["ProcName"];
            lblTitle.Text = Request.QueryString["title"];
            lblDate1.Text = Request.QueryString["date1"];
            lblDate2.Text = Request.QueryString["date2"];

            int page = 1;
            BindDataList(page);
        }
    }

    protected void BindDataList(int page = 0)
    {
        this.employeeName = tbxEmployeeName.Text.ToString().Trim();

        this.loginName = tbxLoginName.Text.ToString().Trim();

        if (page > 0)
        {
            this.AspNetPager1.CurrentPageIndex = page;
        }

        int count = BPMHelp.GetEmployeesCount(this.employeeName, this.loginName);
        this.AspNetPager1.RecordCount = count;
        DataTable dt = BPMHelp.GetEmployeesList(this.employeeName, this.loginName, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
        lblUserList.DataSource = dt;
        lblUserList.DataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        int page = 1;
        BindDataList(page);
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindDataList();
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        //proId,name,
        string procId = lblProcId.Text;
        string procName = lblProcName.Text;
        string userCode = GetEmployee().Id;
        string userName = GetEmployee().Name;

        List<Person> items = new List<Person>();
        if (ViewState["SelectedPersons"] != null)
        {
            items = (List<Person>)ViewState["SelectedPersons"];
        }
        //insert 
        foreach (var item in items)
        {
            if (!BPMHelp.ExistAuthorization(procId, userCode, item.EmployeeCode))
            {
                BPMHelp.InsertAuthorization(userCode, userName, procId, procName, item.EmployeeCode, item.EmployeeName);
            }
        }

        GoToPage();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        GoToPage();
    }

    void GoToPage()
    {
        string url = "ArchiveList.aspx?title=" + lblTitle.Text + "&date1=" + lblDate1.Text + "&date2=" + lblDate2.Text;
        Response.Redirect(url);
    }

    //选中CheckBox
    protected void cbUser_CheckedChanged(object sender, EventArgs e)
    {
        List<Person> items = new List<Person>();

        if (ViewState["SelectedPersons"] != null)
        {
            items = (List<Person>)ViewState["SelectedPersons"];
        }

        CheckBox cbUser = (CheckBox)sender;
        Label lblEmployeeCode = (Label)cbUser.Parent.FindControl("lblEmployeeCode");
        Label lblEmployeeName = (Label)cbUser.Parent.FindControl("lblEmployeeName");
        Label lblLoginName = (Label)cbUser.Parent.FindControl("lblLoginName");
        //如果存在
        if (items.Any(p => p.EmployeeCode == lblEmployeeCode.Text))
        {
            int index = items.FindIndex(p => p.EmployeeCode == lblEmployeeCode.Text);
            items.RemoveAt(index);
        }
        else
        {
            items.Add(new Person { EmployeeCode = lblEmployeeCode.Text, EmployeeName = lblEmployeeName.Text });
        }
        ViewState["SelectedPersons"] = items;

        StringBuilder names = new StringBuilder();
        foreach (var item in items)
        {
            names.Append(item.EmployeeName + ";");
        }

        tbxChosenEmployees.Text = names.ToString().TrimEnd(';');
    }

    //绑定已经选中的CheckBox
    protected void lblUserList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //如果有存在的
        if (ViewState["SelectedPersons"] != null)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                List<Person> items = new List<Person>();
                if (ViewState["SelectedPersons"] != null)
                {
                    items = (List<Person>)ViewState["SelectedPersons"];
                }
                CheckBox cbUser = (CheckBox)e.Item.FindControl("cbUser");
                Label lblEmployeeCode = (Label)e.Item.FindControl("lblEmployeeCode");
                if (items.Any(p => p.EmployeeCode == lblEmployeeCode.Text))
                {
                    cbUser.Checked = true;
                }
            }
        }
    }
}

/// <summary>
/// 自定义类 用于缓存当前选中人员
/// </summary>
[Serializable]
public class Person
{
    public string EmployeeCode
    {
        get;
        set;
    }
    public string EmployeeName
    {
        get;
        set;
    }
}