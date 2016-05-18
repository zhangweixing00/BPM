using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.IO;
using System.Text;
using System.Linq.Expressions;

public partial class Workflow_ArchiveListV2 : UPageBase
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
            BindFlowTypes();
            BindOADataList(page);
            BindBPMDataList(page);
        }
    }

    private void BindFlowTypes()
    {
        var infos = DBContext.GetSysContext().WF_AppDict.ToList();
        infos.Insert(0, new Pkurg.PWorldBPM.Business.Sys.WF_AppDict() { AppId = "-1", AppName = "全部" });
        ddlflowTypes.DataSource = infos;
        ddlflowTypes.DataTextField = "AppName";
        ddlflowTypes.DataValueField = "AppId";
        ddlflowTypes.DataBind();
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
        var context = DBContext.GetSysContext();

        string user = GetEmployee().LoginId;
        string proName = tbxTitle.Text.ToString().Trim();
        string startTime = tbxBeginTime.Value.ToString().Trim();
        string endTime = tbxEndTime.Value.ToString().Trim();

        string createrName = tbStarter.Text;
        string createrDeptName=tbStartDept.Text;

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

        int pageSize = this.AspNetPager2.PageSize;


        Expression<Func<Pkurg.PWorldBPM.Business.Sys.V_ArchiveProc_OA_BPM, bool>> query = x =>
            x.userCode == user
            && x.Source == sourceBPM
             && (ddlStatus.SelectedIndex == 0 || x.IsPass.ToString() == ddlStatus.SelectedItem.Value)
            // && (ddlflowTypes.SelectedIndex == 0 || x..ToString() == ddlStatus.SelectedItem.Value)
              && (string.IsNullOrWhiteSpace(createrName) || x.CreatorName.Contains(createrName))
              && (string.IsNullOrWhiteSpace(createrDeptName) || x.CreatorDeptName.Contains(createrDeptName))

            // && (string.IsNullOrWhiteSpace(proName) || x.ProcName.Contains(proName))
            //  && (string.IsNullOrWhiteSpace(startTime) || x.StartTime.HasValue && x.StartTime.Value > DateTime.Parse(startTime))
            //  && (string.IsNullOrWhiteSpace(endTime) || x.StartTime.HasValue && x.StartTime.Value < DateTime.Parse(endTime))
            //&& (string.IsNullOrWhiteSpace(endTime) || x.StartTime.HasValue && x.StartTime.Value < DateTime.Parse(endTime))
              ;
        ;
        //StringBuilder log=new StringBuilder();
        //StringWriter sw = new StringWriter(log);
        //context.Log = sw;
        //Response.Write(DateTime.Now.ToString("HH:mm:ss mmm"));
        System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
        w.Start();

        var dataInfos = context.V_ArchiveProc_OA_BPM.Where(query).Distinct();

        this.AspNetPager2.RecordCount = dataInfos.Count();
        var pagedList = dataInfos.OrderByDescending(x => x.StartTime)
              .Skip((this.AspNetPager2.CurrentPageIndex - 1) * pageSize).Take(pageSize);
        //Response.Write(DateTime.Now.ToString("HH:mm:ss mmm"));

        w.Stop();
        Trace.Write(w.Elapsed.ToString());
        LoggerR.logger.DebugFormat("2.1:{0}", w.Elapsed.ToString());
        w.Restart();

        lblBPMList.DataSource = pagedList.ToList();
        w.Stop();
        Trace.Write(w.Elapsed.ToString());
        LoggerR.logger.DebugFormat("2.2:{0}", w.Elapsed.ToString());
        lblBPMList.DataBind();
        //sw.Close();
        //Response.Write(log);
        // File.AppendAllText("", log.ToString());
    }
    public string GetStatusText(string status)
    {
        switch (status)
        {
            case "1":
                return "通过";
            case "0":
                return "拒绝";
            case "5":
                return "强制结束";
            default:
                return "未知";
        }
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