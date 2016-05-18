using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Pkurg.PWorldTemp.WorkflowCommon;

public partial class ToDoList : UPageBase
{
    //页面加载
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbUser.Text = GetEmployee().LoginId;
            BindData(tbUser.Text);
        }
    }
    //得到审批页面的URL，一个参数
    public string GetApprovalPageUrl(string status)
    {
        if (status == "拟稿" || status == "发起申请")
        {
            return System.Configuration.ConfigurationManager.AppSettings["StartPageUrl"];
        }
        return System.Configuration.ConfigurationManager.AppSettings["ApprovalPageUrl"];
    }
    //得到审批页面的URL，多个参数
    public string GetApprovalPageUrl(string type,
        string vpath, string instId, string taskID,//oa
        string status, string formName, string sn)
    {
        if (type == "0")
        {
            //OA
            string oaUrl = System.Configuration.ConfigurationManager.AppSettings["OAURL"];
            return string.Format(oaUrl + "/{0}Modules/Workflow/ProcessEventPath.ashx?caseID={1}&taskID={2}&actionType=1"
                , vpath, instId, taskID);
        }
        //BPM

        return string.Format("/Workflow/ApprovePage/ApprovePageHandler.ashx?sn={0}&id={1}&status={2}",sn,instId,HttpUtility.UrlEncode(status));

    }
    //数据绑定
    public void BindData(string user)
    {

        if (!string.IsNullOrEmpty(user))
        {
            user = user.ToLower().Replace("founder\\", "");
            DataTable dt = GetDataInfos(user);
            DataView dv = dt.DefaultView;

            string proName = tbxTitle.Text.ToString().Trim();
            string startTime = tbxBeginTime.Value.ToString().Trim();
            string endTime = tbxEndTime.Value.ToString().Trim();

            StringBuilder searchSql = new StringBuilder();

            if (!string.IsNullOrEmpty(proName))
            {
                searchSql.AppendFormat("FormTitle like '%{0}%'", proName);
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                if (!string.IsNullOrEmpty(searchSql.ToString()))
                {
                    searchSql.Append(" and ");
                }
                searchSql.AppendFormat("ReceiveTime>'{0}'", startTime);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                if (!string.IsNullOrEmpty(searchSql.ToString()))
                {
                    searchSql.Append(" and ");
                }
                searchSql.AppendFormat("ReceiveTime<'{0}'", endTime);
            }

            dv.RowFilter = searchSql.ToString();

            gvDataList.DataSource = dv.ToTable();
            gvDataList.DataBind();
        }
    }
    //得到表信息
    public DataTable GetDataInfos(string loginName)
    {
        DataProvider dataProvider = new DataProvider();
        dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@loginid",System.Data.SqlDbType.NVarChar,100)
        };
        parameters[0].Value = loginName;
        return dataProvider.ExecutedProcedure("GetToDoList", parameters);

    }
    //切换用户查找事件
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(tbUser.Text))
        {
            tbUser.Text = GetEmployee().LoginId;
        }
        BindData(tbUser.Text);
    }
    //GridView页面查询变化
    protected void gvDataList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDataList.PageIndex = e.NewPageIndex;
        BindData(tbUser.Text);
    }
    //查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData(tbUser.Text);
        //gvDataList.PageIndex = 0;
    }

    //OA刷新
    protected void btRefresh_Click(object sender, EventArgs e)
    {
        BindData(tbUser.Text);
    }
}