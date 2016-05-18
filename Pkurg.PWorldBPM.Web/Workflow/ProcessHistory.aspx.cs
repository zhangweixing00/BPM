using System;
using System.Data;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_ProcessHistory : System.Web.UI.Page
{
    ProcessHistoryBLL processHistoryBLL = new Pkurg.PWorldBPM.Business.Workflow.ProcessHistoryBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        string caseID = Request.QueryString["CaseID"];
        if (!IsPostBack)
        {
            BindList(caseID);
        }
    }

    private void BindList(string caseID)
    {

        DataTable dt = processHistoryBLL.GetProcessHistoryList(caseID);
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
        rptList.DataSource = dt;
        rptList.DataBind();

    }

    private void bindNextApprover(string formID)
    {


    }
}