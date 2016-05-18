using System;
using System.Linq;

[BPM(AppId = "3017")]
public partial class Workflow_ViewPage_V_HR_Employment : V_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.HR_Employment info = BizContext.HR_Employment.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbReportCode.Text = info.FormID;
                tbReportCode.Text = info.FormID;
                tbDeptName.Text = info.DeptName;
                tbGoalPost.Text = info.GoalPost;
                tbPostLevel.Text = info.PostLevel;
                tbSalary.Text = info.Salary;
                tbRatio.Text = info.Ratio;
                tbAnnualSalary.Text = info.AnnualSalary;
                tbIsLabourContract.Text = info.IsLabourContract;
                tbLabourContractStart.Text = info.LabourContractStart;
                tbLabourContractEnd.Text = info.LabourContractEnd;
                tbIsProbationPeriod.Text = info.IsProbationPeriod;
                tbProbationPeriodStart.Text = info.ProbationPeriodStart;
                tbProbationPeriodEnd.Text = info.ProbationPeriodEnd;
                tbRemark.Text = info.Remark;

                InitApproveList2(info.IsGroup);
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
                InitFormData();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }
    }

    /// <summary>
    /// 初始化审批栏2
    /// </summary>
    private void InitApproveList2(string IsGroup)
    {
        lbDirector.Text = "相关董事意见：";
        lbPresident.Text = "董事长意见：";
        lbTitle.Text = "Peking University Resources Group Investment Company Limited";
        if (!string.IsNullOrEmpty(IsGroup))
        {
            if (IsGroup == "1")
            {
                lbDirector.Text = "用人部门分管领导意见：";
                lbPresident.Text = "CEO意见：";
                lbTitle.Text = "";
            }
        }
    }
}

