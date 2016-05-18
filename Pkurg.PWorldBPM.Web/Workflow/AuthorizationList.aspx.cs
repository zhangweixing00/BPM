using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Workflow_AuthorizationList : UPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int page = 1;
            BindDataList1(page);
            BindDataList2(page);

            BindDataList3(page);
            BindDataList4(page);
        }
    }

    #region BPM

    //绑定我授权的列表
    protected void BindDataList1(int page = 0)
    {
        string user = GetEmployee().Id;
        int type = 1;
        string procName = tbxTitle.Text.ToString().Trim();
        string startTime1 = tbxBeginTime.Value.ToString().Trim();
        string startTime2 = tbxEndTime.Value.ToString().Trim();
        int count = 0;
        if (page != 0)
        {
            this.AspNetPager1.CurrentPageIndex = page;
        }
        count = (int)BPMHelp.GetAuthorCount(type, user, procName, startTime1, startTime2);
        this.AspNetPager1.RecordCount = count;
        DataTable dt = BPMHelp.GetAuthorList(type, user, procName, startTime1, startTime2, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);

        lblList1.DataSource = dt;
        lblList1.DataBind();

    }

    //绑定我被授权的列表
    protected void BindDataList2(int page = 0)
    {
        string user = GetEmployee().Id;
        int type = 0;
        string procName = tbxTitle.Text.ToString().Trim();
        string startTime1 = tbxBeginTime.Value.ToString().Trim();
        string startTime2 = tbxEndTime.Value.ToString().Trim();
        int count = 0;
        if (page != 0)
        {
            this.AspNetPager2.CurrentPageIndex = page;
        }
        count = (int)BPMHelp.GetAuthorCount(type, user, procName, startTime1, startTime2);
        this.AspNetPager2.RecordCount = count;
        DataTable dt = BPMHelp.GetAuthorList(type, user, procName, startTime1, startTime2, this.AspNetPager2.CurrentPageIndex, this.AspNetPager2.PageSize);

        lblList2.DataSource = dt;
        lblList2.DataBind();

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindDataList1();
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        BindDataList2();
    }

    //撤销授权
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        List<Authorization> items = new List<Authorization>();
        if (ViewState["SelectItems"] != null)
        {
            items = (List<Authorization>)ViewState["SelectItems"];
        }
        foreach (var item in items)
        {
            BPMHelp.DeleteAuthorizations(item.AuthorizationID);
        }
        Response.Redirect("AuthorizationList.aspx", false);
    }

    protected void lblList1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //绑定已经选中的Checkbox
        if (ViewState["SelectItems"] != null)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                List<Authorization> items = new List<Authorization>();
                items = (List<Authorization>)ViewState["SelectItems"];
                CheckBox cbxChoose = (CheckBox)e.Item.FindControl("cbxChoose");
                Label lblAuthorizationID = (Label)e.Item.FindControl("lblAuthorizationID");
                if (items.Any(p => p.AuthorizationID == lblAuthorizationID.Text))
                {
                    cbxChoose.Checked = true;
                }
            }
        }

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink hyperLink = (HyperLink)e.Item.FindControl("hyperLink");
            Label lblProcId = (Label)e.Item.FindControl("lblProcId");
            string instanceId = "";
            string url = BPMHelp.GetUrl(lblProcId.Text, out instanceId);
            //此处url存在问题，页面存在路径以及参数未知
            hyperLink.NavigateUrl = "~/Workflow/ViewPage/V_" + url + "?ID=" + instanceId;
        }
    }

    protected void lblList2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink hyperLink = (HyperLink)e.Item.FindControl("hyperLink2");
            Label lblProcId = (Label)e.Item.FindControl("lblProcId2");
            string instanceId = "";
            string url = "~/Workflow/ViewPage/V_" + BPMHelp.GetUrl(lblProcId.Text, out instanceId);
            if (url.ToLower().EndsWith(".ascx"))
            {
                url = System.Configuration.ConfigurationManager.AppSettings["ViewProcInstPageUrl"].ToString();
            }
            hyperLink.NavigateUrl = url + "?ID=" + instanceId;
        }
    }

    //选中checkbox
    public void cbxChoose_CheckedChanged(object sender, EventArgs e)
    {
        List<Authorization> items = new List<Authorization>();

        if (ViewState["SelectItems"] != null)
        {
            items = (List<Authorization>)ViewState["SelectItems"];
        }

        CheckBox cbxChoose = (CheckBox)sender;
        Label lblAuthorizationID = (Label)cbxChoose.Parent.FindControl("lblAuthorizationID");
        //如果存在
        if (items.Any(p => p.AuthorizationID == lblAuthorizationID.Text))
        {
            int index = items.FindIndex(p => p.AuthorizationID == lblAuthorizationID.Text);
            items.RemoveAt(index);
        }
        else
        {
            items.Add(new Authorization { AuthorizationID = lblAuthorizationID.Text });
        }
        ViewState["SelectItems"] = items;
    }

    #endregion

    #region OA

    //绑定我授权的列表
    protected void BindDataList3(int page = 0)
    {
        string user = GetEmployee().FounderLoginId;
        int type = 1;
        string procName = tbxTitle.Text.ToString().Trim();
        string startTime1 = tbxBeginTime.Value.ToString().Trim();
        string startTime2 = tbxEndTime.Value.ToString().Trim();
        if (page != 0)
        {
            this.AspNetPager3.CurrentPageIndex = page;
        }
        int totalCount = 0;
        DataTable dt = BPMHelp.GetOAAuthorizationList(type, user, procName, startTime1, startTime2, this.AspNetPager3.CurrentPageIndex, this.AspNetPager3.PageSize, out totalCount);
        this.AspNetPager3.RecordCount = totalCount;
        rpt3.DataSource = dt;
        rpt3.DataBind();

    }

    //绑定我被授权的列表
    protected void BindDataList4(int page = 0)
    {
        string user = GetEmployee().FounderLoginId;
        int type = 0;
        string procName = tbxTitle.Text.ToString().Trim();
        string startTime1 = tbxBeginTime.Value.ToString().Trim();
        string startTime2 = tbxEndTime.Value.ToString().Trim();
        if (page != 0)
        {
            this.AspNetPager4.CurrentPageIndex = page;
        }
        int totalCount = 0;
        DataTable dt = BPMHelp.GetOAAuthorizationList(type, user, procName, startTime1, startTime2, this.AspNetPager4.CurrentPageIndex, this.AspNetPager4.PageSize, out totalCount);
        this.AspNetPager4.RecordCount = totalCount;
        rpt4.DataSource = dt;
        rpt4.DataBind();

    }

    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        BindDataList3();
    }

    protected void AspNetPager4_PageChanged(object sender, EventArgs e)
    {
        BindDataList4();
    }

    //撤销授权OA
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        List<Authorization> items = new List<Authorization>();
        if (ViewState["SelectOAItems"] != null)
        {
            items = (List<Authorization>)ViewState["SelectOAItems"];
        }
        foreach (var item in items)
        {
            BPMHelp.DeleteOAAuthorizations(item.AuthorizationID);
        }
        Response.Redirect("AuthorizationList.aspx", false);
    }

    protected void rpt3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //绑定已经选中的Checkbox
        if (ViewState["SelectOAItems"] != null)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                List<Authorization> items = new List<Authorization>();
                items = (List<Authorization>)ViewState["SelectOAItems"];
                CheckBox cbxChoose2 = (CheckBox)e.Item.FindControl("cbxChoose2");
                Label lblAuthorizationID = (Label)e.Item.FindControl("lblAuthorizationID");
                if (items.Any(p => p.AuthorizationID == lblAuthorizationID.Text))
                {
                    cbxChoose2.Checked = true;
                }
            }
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink hyperLink = (HyperLink)e.Item.FindControl("hyperLink");
            Label lblProcId = (Label)e.Item.FindControl("lblProcId");

            string oaUrl = System.Configuration.ConfigurationManager.AppSettings["OAURL"];
            string oa = string.Format(oaUrl + "/{0}Modules/Workflow/ProcessEventPath.ashx?caseID={1}&taskID={2}&actionType=3", "OAWeb/FounderOAResourceGroup/", lblProcId.Text, "1");
            hyperLink.NavigateUrl = oa;
        }
    }

    protected void rpt4_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink hyperLink = (HyperLink)e.Item.FindControl("hyperLink2");
            Label lblProcId = (Label)e.Item.FindControl("lblProcId2");

            string oaUrl = System.Configuration.ConfigurationManager.AppSettings["OAURL"];
            string oa = string.Format(oaUrl + "/{0}Modules/Workflow/ProcessEventPath.ashx?caseID={1}&taskID={2}&actionType=3", "OAWeb/FounderOAResourceGroup/", lblProcId.Text, "1");
            hyperLink.NavigateUrl = oa;
        }
    }

    //选中checkbox
    public void cbxChoose2_CheckedChanged(object sender, EventArgs e)
    {
        List<Authorization> items = new List<Authorization>();

        if (ViewState["SelectOAItems"] != null)
        {
            items = (List<Authorization>)ViewState["SelectOAItems"];
        }

        CheckBox cbxChoose = (CheckBox)sender;
        Label lblAuthorizationID = (Label)cbxChoose.Parent.FindControl("lblAuthorizationID");
        //如果存在
        if (items.Any(p => p.AuthorizationID == lblAuthorizationID.Text))
        {
            int index = items.FindIndex(p => p.AuthorizationID == lblAuthorizationID.Text);
            items.RemoveAt(index);
        }
        else
        {
            items.Add(new Authorization { AuthorizationID = lblAuthorizationID.Text });
        }
        ViewState["SelectOAItems"] = items;
    }

    #endregion

    //查询按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        int page = 1;
        BindDataList1(page);
        BindDataList2(page);
        BindDataList3(page);
        BindDataList4(page);
    }
}

[Serializable]
public class Authorization
{
    public string AuthorizationID
    {
        get;
        set;
    }
}