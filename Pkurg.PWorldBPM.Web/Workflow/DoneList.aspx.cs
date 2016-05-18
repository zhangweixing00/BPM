using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;
using Pkurg.PWorldTemp.WorkflowCommon;

public partial class DoneList : UPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["CU"] = "founder\\zhaoguanyu";
        if (!IsPostBack)
        {
            tbUser.Text = GetEmployee().LoginId;
            BindData(tbUser.Text);
        }
    }
    public string GetApprovalPageUrl(string type,
          string vpath, string instId, string taskID,//oa
          string status, string formName)
    {
        if (type == "0")
        {
            //OA
            return string.Format("http://oa.founder.com/{0}/Modules/Workflow/ProcessEventPath.ashx?caseID={1}&taskID={2}&actionType=2&ref=mini"
                , vpath, instId, taskID);
        }
        //BPM

        string urlParams = string.Format("id={0}", instId, "");
        string urlPage = formName;
        if (formName.ToLower().Contains(".ascx"))
        {
            urlPage = System.Configuration.ConfigurationManager.AppSettings["ViewProcInstPageUrl"];
        }
        else
        {

            urlPage = "ViewPage/V_" + urlPage;

        }
        return string.Format("{0}?{1}", urlPage, urlParams);
    }
    public void BindData(string user)
    {
        if (!string.IsNullOrEmpty(user))
        {
            user = user.ToLower().Replace("founder\\", "");
            DataTable dt=GetDataInfos(user);
            DataView dv = dt.DefaultView;

            string proName = tbxTitle.Text.ToString().Trim();
            string startTime = tbxBeginTime.Value.ToString().Trim();
            string endTime = tbxEndTime.Value.ToString().Trim();

            StringBuilder searchSql = new StringBuilder();

            if (!string.IsNullOrEmpty(proName))
            {
                if (!Workflow_Common.ValidateQuery(proName))
                {
                    DisplayMessage.ExecuteJs("alert('查询字符无效');");
                    return;
                }
                searchSql.AppendFormat("FormTitle like '%{0}%'", proName);
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                if (!string.IsNullOrEmpty(searchSql.ToString()))
                {
                    searchSql.Append(" and ");
                }
                searchSql.AppendFormat("ApproveAtTime>'{0}'", startTime);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                if (!string.IsNullOrEmpty(searchSql.ToString()))
                {
                    searchSql.Append( " and ");
                }
                searchSql.AppendFormat("ApproveAtTime<'{0}'", endTime);
            }

            dv.RowFilter = searchSql.ToString();

            gvDataList.DataSource = dv.ToTable();
            gvDataList.DataBind();
        }
    }

    public string GetCurrentUser()
    {
        return tbUser.Text;
    }

    public DataTable GetDataInfos(string loginName)
    {
        DataProvider dataProvider = new DataProvider();
        dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@loginid",System.Data.SqlDbType.NVarChar,100)
        };
        parameters[0].Value = loginName;
        return dataProvider.ExecutedProcedure("wf_usp_GetDoneList", parameters);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void gvDataList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDataList.PageIndex = e.NewPageIndex;
        BindData(tbUser.Text);
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData(tbUser.Text);
        gvDataList.PageIndex = 0;
    }
}