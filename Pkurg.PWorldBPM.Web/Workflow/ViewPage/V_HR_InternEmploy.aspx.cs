using System;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3022")]
public partial class Workflow_ViewPage_V_HR_InternEmploy 
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
            var info = BizContext.HR_InternEmploy.FirstOrDefault(x=>x.FormID==FormId);
            if (info != null)
            {
                StartDeptId = info.InternDeptCode;
                tbEmployeeName.Text = info.EmployeeName;
                tbPosition.Text = info.Position;
                tbDept.Text = info.InternDeptName;
                tbInternReward.Text = info.InternReward;
                tbInternDeadline.Text = info.InternDeadline;
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
