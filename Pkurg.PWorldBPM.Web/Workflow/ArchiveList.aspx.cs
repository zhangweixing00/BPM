using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Workflow_ArchiveList : UPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int page = 1;

            if (!string.IsNullOrEmpty(Request.QueryString["title"]))
            {
                tbxTitle.Text = Request.QueryString["title"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["date1"]))
            {
                tbxBeginTime.Value = Request.QueryString["date1"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["date2"]))
            {
                tbxEndTime.Value = Request.QueryString["date2"];
            }

            BindOADataList(page);
            BindBPMDataList(page);
        }
    }

    protected void BindOADataList(int page = 0)
    {
        string founderUser = GetEmployee().FounderLoginId;
        string proName = tbxTitle.Text.ToString().Trim();
        string startTime = tbxBeginTime.Value.ToString().Trim();
        string endTime = tbxEndTime.Value.ToString().Trim();

        int count = 0;
        string sourceOA = "OA";
        if (page != 0)
        {
            this.AspNetPager1.CurrentPageIndex = page;
        }

        DataTable dt = BPMHelp.GetArchiveList(founderUser, sourceOA, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, proName, startTime, endTime, out count);

        //count = BPMHelp.GetArchiveCount(founderUser, sourceOA, proName, startTime, endTime);
        this.AspNetPager1.RecordCount = count;
        lblOAList.DataSource = dt;
        lblOAList.DataBind();
    }

    protected void BindBPMDataList(int page = 0)
    {
        string user = GetEmployee().LoginId;
        string proName = tbxTitle.Text.ToString().Trim();
        string startTime = tbxBeginTime.Value.ToString().Trim();
        string endTime = tbxEndTime.Value.ToString().Trim();

        int count = 0;
        string sourceBPM = "BPM";
        if (page != 0)
        {
            this.AspNetPager2.CurrentPageIndex = page;
        }

        if (user.Contains("\\"))
        {
            int beginIndex = user.LastIndexOf("\\");
            user = user.Substring(beginIndex + 1, user.Length - beginIndex - 1);
        }
        System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
        w.Start();
        DataTable dt = BPMHelp.GetArchiveList_bpm(user, sourceBPM, this.AspNetPager2.CurrentPageIndex, this.AspNetPager2.PageSize, proName, startTime, endTime, out count);
        //count = BPMHelp.GetArchiveCount(user, sourceBPM, proName, startTime, endTime);
        w.Stop();
        Trace.Write(w.Elapsed.ToString());
        LoggerR.logger.DebugFormat("1:{0}", w.Elapsed.ToString());
        this.AspNetPager2.RecordCount = count;
        lblBPMList.DataSource = dt;
        lblBPMList.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int page = 1;
        BindOADataList(page);
        BindBPMDataList(page);
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindOADataList();
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        BindBPMDataList();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        int page = 1;
        BindOADataList(page);
        BindBPMDataList(page);
    }

    protected void lblBPMList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Authorize")
        {
            string arguments = e.CommandArgument.ToString();
            int index = arguments.IndexOf(",");
            string procId = arguments.Substring(0, index);
            string procName = arguments.Substring(index + 1, arguments.Length - 1 - index);
            string url = "AuthorizationSelectPersons.aspx?ProcID="
                        + procId + "&ProcName=" + procName + "&title=" + tbxTitle.Text.Trim() + "&date1=" + tbxBeginTime.Value + "&date2=" + tbxEndTime.Value;
            Response.Redirect(url, false);
        }
    }


    protected void lblBPMList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblProId = (Label)e.Item.FindControl("lblProId");
            HyperLink hyperLink = (HyperLink)e.Item.FindControl("hyperLink");
            string parameter = "";
            string url = "~/Workflow/ViewPage/V_" + BPMHelp.GetUrl(lblProId.Text, out parameter);
            if (url.ToLower().EndsWith(".ascx"))
            {
                url = System.Configuration.ConfigurationManager.AppSettings["ViewProcInstPageUrl"].ToString();
            }
            hyperLink.NavigateUrl = url + "?ID=" + parameter;
        }
    }
}