using System;
using System.Configuration;
using System.Data;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Sys_ProcessDate : UPageBase
{
    readonly string K2ServerName = ConfigurationManager.AppSettings["K2ServerName"];
    ProcessHistoryBLL processHistoryBLL = new Pkurg.PWorldBPM.Business.Workflow.ProcessHistoryBLL();
    //Get_K2_ProcInstDataAudit
    protected void Page_Load(object sender, EventArgs e)
    {
        string InstanceID = Request.QueryString["InstanceID"];
        int procInstID = Convert.ToInt32(Request.QueryString["procInstID"]);
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["InstanceID"]))
            {
                BindList(InstanceID);
                LoadDataFields(procInstID);
            }

        }
    }

    private void BindList(string InstanceID)
    {

        DataTable dt = processHistoryBLL.GetProcessHistoryList(InstanceID);
        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
        {
            string formID = dt.Rows[0]["FormID"].ToString();
            if (!string.IsNullOrEmpty(formID))
            {
                SourceCode.Workflow.Management.WorklistItems items = WorkflowManage.GetNextApprover(formID);
                if (items != null)
                {
                    foreach (SourceCode.Workflow.Management.WorklistItem item in items)
                    {
                        string[] nameArray = item.Actioner.Name.Split('\\');
                        if (nameArray != null && nameArray.Length > 1)
                        {
                            string domainID = nameArray[1];
                            DataTable employeeDataTable = processHistoryBLL.GetEmployeeInfo(domainID);
                            DataRow dataRow = dt.NewRow();
                            dt.Rows.Add(dataRow);
                            dataRow["ApproveByUserName"] = employeeDataTable == null ? string.Empty : employeeDataTable.Rows[0]["EmployeeName"].ToString();
                            dataRow["DepartName"] = employeeDataTable == null ? string.Empty : employeeDataTable.Rows[0]["DepartName"].ToString();
                            dataRow["FinishedTime"] = new DateTime(9999, 12, 31);
                            dataRow["ApproveResult"] = "送达";
                            dataRow["CurrentActiveName"] = item.ActivityName;
                            dataRow["ISSign"] = "0";
                        }
                    }
                }
            }
        }
        rpViewHistory.DataSource = dt;
        rpViewHistory.DataBind();

    }

    /// <summary>
    /// 绑定默认DataFields
    /// </summary>
    /// <param name="procInstID"></param>
    private void LoadDataFields(int procInstID)
    {
        int count = 0;
        DataTable dt = BPMHelp.Get_K2_ProcInstDataAudit(procInstID,out count);
        try
        {
            rptList.DataSource = dt;
            rptList.DataBind();
        }
        catch (Exception ex)
        {
            lblException.Text = "异常信息:" + ex.Message;
            lblException.Visible = true;
        }
    }

    /// <summary>
    /// 自定义类
    /// </summary>
    public class BPMListItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

}