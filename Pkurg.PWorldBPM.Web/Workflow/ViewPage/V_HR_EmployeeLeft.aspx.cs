using System;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3015")]
public partial class Workflow_ViewPage_V_HR_EmployeeLeft 
    : V_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            ///加载业务数据
            var info = BizContext.HR_EmployeeLeft.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(info.DeptID);
                StartDeptId = info.DeptID;
                tbEmployeeName.Text = info.EmployeeName;
                tbDept.Text = info.DeptName;
                tbPosition.Text = info.Position;
                tbLeftType.SelectedValue = info.LeftType;
                cbIsActiveLeft.Checked = bool.Parse(info.IsInitiativeLeft);
                TextBox1.Text = info.Handover ?? "";
                TextBox2.Text = info.Recipient ?? "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ///加载页面数据
            string instId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(instId))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;
                InitFormData();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }
    }
}
