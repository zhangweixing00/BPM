using System;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "10111")]
public partial class Workflow_ApprovePage_V_ERP_ContractFinalAccount 
    : V_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            var info = BizContext.ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(info.StartDeptId);
                lbDeptName.Text = deptInfo.Remark;
                StartDeptId = info.StartDeptId;
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
