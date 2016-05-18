using System;
using System.Data;
using System.Linq;
using Pkurg.PWorldBPM.Business.Controls;

public partial class Sys_ProcessEndManage_E : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["appId"]))
            {
                Bind(Request.QueryString["appId"]);
            }
        }
    }
    private void Bind(string appId)
    {
        DataTable dataTable = ProcessEndManage.GetAppEndByAppID(appId);
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            txtAddressToLink.Text = dataTable.Rows[0]["AddressToLink"].ToString();
            txtClassName.Text = dataTable.Rows[0]["ClassName"].ToString();
        }
    }

    protected void Save(string AppCode, string AddressToLink, string ClassName)
    {
        var dataProvider = DBContext.GetSysContext();
        var info = dataProvider.WF_GetRelatedLinks.FirstOrDefault(x => x.AppCode == AppCode);
        if (info == null)
        {
            dataProvider.WF_GetRelatedLinks.InsertOnSubmit(new Pkurg.PWorldBPM.Business.Sys.WF_GetRelatedLinks()
                {
                    AppCode = AppCode,
                    AddressToLink=AddressToLink,
                    ClassName=ClassName,
                    CreateTime=DateTime.Now
                });
        }
        else
        {
            info.AddressToLink = AddressToLink;
            info.ClassName = ClassName;
        }

        dataProvider.SubmitChanges();
        ProcessEndManage.EditEndInfos(Request.QueryString["appId"], AddressToLink, ClassName);
        DisplayMessage.ExecuteJs("alert('保存成功！');window.location.href='ProcessEndManage.aspx';");

        //DataProvider dataProvider = new DataProvider();
        //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        //DataTable dataTableVerification = new DataTable();
        //dataTableVerification = dataProvider.ExecuteDataTable(string.Format("select 1 from [WF_GetRelatedLinks] where AddressToLink='{0}'and CLassName='{1}'", AddressToLink,ClassName), CommandType.Text);
        //ProcessEndManage.AlterEndInfos(Request.QueryString["appId"].ToString());
        //DataTable dataTableAlter=new DataTable();
        //dataTableAlter=dataProvider.ExecuteDataTable(string.Format("select 1 from [Wf_GetRelatedLinks] where appId='{0}'",AppCode),CommandType.Text);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string AppCode = Request.QueryString["appId"];
        string AddressToLink = txtAddressToLink.Text;
        string ClassName = txtClassName.Text;
        Save(AppCode, AddressToLink, ClassName);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Sys/ProcessEndManage.aspx");
    }
}