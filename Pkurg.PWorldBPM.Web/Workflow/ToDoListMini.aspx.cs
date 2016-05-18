using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Pkurg.PWorldTemp.WorkflowCommon;

public partial class ToDoListMini : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    public string GetApprovalPageUrl(string status)
    {
        if (status == "拟稿" || status == "发起申请")
        {
            return System.Configuration.ConfigurationManager.AppSettings["StartPageUrl"];
        }
        return System.Configuration.ConfigurationManager.AppSettings["ApprovalPageUrl"];
    }

    public string GetApprovalPageUrl(string type,
        string vpath, string instId, string taskID,//oa
        string status, string formName, string sn)
    {
        if (type == "0")
        {
            //OA
            string oaUrl = System.Configuration.ConfigurationManager.AppSettings["OAURL"];
            return string.Format(oaUrl + "/{0}Modules/Workflow/ProcessEventPath.ashx?caseID={1}&taskID={2}&actionType=1&ref=mini"
                , vpath, instId, taskID);
        }
        //BPM

        return string.Format("/Workflow/ApprovePage/ApprovePageHandler.ashx?sn={0}&id={1}&status={2}", sn, instId, HttpUtility.UrlEncode(status));
    }

    public void BindData()
    {
        //erp
        try
        {
            BindErpList();
        }
        catch
        { }
        //oa ,bpm

        try
        {
            DataTable dt = GetDataInfos();

            DataView dv = dt.DefaultView;
            lblCount.Text = dt.Rows.Count.ToString();
            gvDataList.DataSource = dv.ToTable();
            gvDataList.DataBind();
        }
        catch
        {
        }
    }

    /// <summary>
    /// ERP数据源
    /// </summary>
    /// <returns></returns>
    void BindErpList()
    {
        DataSet ds = Erp2OA.GetViews();
        if (ds != null)
        {
            DataTable flows = new DataTable();

            flows.Columns.Add("FlowUrl");
            flows.Columns.Add("FlowTitle");
            flows.Columns.Add("FlowType");
            flows.Columns.Add("FlowDateTime");

            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            if (dt1 != null && dt1.Rows.Count > 0 && Convert.ToInt32(dt1.Rows[0]["CNT"]) > 0)
            {
                //资源控股  
                DataRow row = flows.NewRow();
                row["FlowUrl"] = System.Configuration.ConfigurationManager.AppSettings["ERP2OA1"];
                row["FlowTitle"] = "在资源控股ERP系统中有(" + dt1.Rows[0]["CNT"].ToString() + ")个流程需要您审批";
                row["FlowType"] = "ERP合同审批流程";
                row["FlowDateTime"] = DateTime.Now.ToString();

                flows.Rows.Add(row);
            }
            if (dt2 != null && dt2.Rows.Count > 0 && Convert.ToInt32(dt2.Rows[0]["CNT"]) > 0)
            {
                //资源集团  
                DataRow row = flows.NewRow();
                row["FlowUrl"] = System.Configuration.ConfigurationManager.AppSettings["ERP2OA2"];
                row["FlowTitle"] = "在资源集团ERP系统中有(" + dt2.Rows[0]["CNT"].ToString() + ")个流程需要您审批";
                row["FlowType"] = "ERP合同审批流程";
                row["FlowDateTime"] = DateTime.Now.ToString();

                flows.Rows.Add(row);
            }
            rptERP.DataSource = flows;
            rptERP.DataBind();
        }
    }

    public DataTable GetDataInfos()
    {
        //string loginName = JsHelper.TrimString(HttpContext.Current.User.Identity.Name.ToLower());
        IdentityUser identityUser = new IdentityUser();
        string loginName = identityUser.GetEmployee().LoginId;

        DataProvider dataProvider = new DataProvider();
        dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@loginid",System.Data.SqlDbType.NVarChar,100)
        };
        parameters[0].Value = loginName;
        return dataProvider.ExecutedProcedure("GetToDoList", parameters);
    }

    protected string FormatDateTime(object dt)
    {
        if (dt != null && !string.IsNullOrEmpty(dt.ToString()))
        {
            return Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
        }
        return "";
    }

    //OA刷新
    protected void btRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void lbtnReload_Click(object sender, EventArgs e)
    {
        BindData();
    }
}
