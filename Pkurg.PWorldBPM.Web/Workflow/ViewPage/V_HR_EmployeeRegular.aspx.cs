using System;
using System.Linq;
using System.Web;

[BPM(AppId = "3018")]
public partial class Workflow_ViewPage_V_HR_EmployeeRegular : V_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.HR_EmployeeRegular info = BizContext.HR_EmployeeRegular.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                InitApproveList2(info.IsGroup);

                tbReportCode.Text = info.FormID;
                tbUserName.Text = info.UserName;
                tbLoginID.Value = info.LoginID;
                tbDeptName.Text = info.DeptName;
                tbEntryTime.Text = info.EntryTime;
                tbProbationPeriod.Text = info.ProbationPeriod;
                tbProbationPeriodStart.Text = info.ProbationPeriodStart;
                tbProbationPeriodEnd.Text = info.ProbationPeriodEnd;
                tbPostLevel.Text = info.PostLevel;
                tbPost.Text = info.Post;
                tbAchievement.Text = info.Achievement;
                tbSign.Text = info.Sign;
                tbSignDate.Text = info.SignDate;
                tbQualityScore1.Text = info.QualityScore1;
                tbQualityScore2.Text = info.QualityScore2;
                tbQualityScore3.Text = info.QualityScore3;
                tbQualityScore4.Text = info.QualityScore4;
                tbQualityScore5.Text = info.QualityScore5;
                tbQualityScore6.Text = info.QualityScore6;
                tbQualityScore7.Text = info.QualityScore7;
                tbQualityScore8.Text = info.QualityScore8;
                tbQualityScore9.Text = info.QualityScore9;
                tbQualityScore10.Text = info.QualityScore10;
                tbWorkCompletion.Text = info.WorkCompletion;
                tbAdvantage.Text = info.Advantage;
                tbDisadvantage.Text = info.Disadvantage;
                tbSuggest.Text = info.Suggest;
                tbQualityScore.Text = info.QualityScore;
                tbAchievementScore.Text = info.AchievementScore;
                tbTatolScore.Text = info.TatolScore;
                ddlIsAgreeRegular.Text = info.IsAgreeRegular;
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
        if (new IdentityUser().GetEmployee().LoginId != tbLoginID.Value)
        {
            tb360.Visible = true;
        }
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            tb360.Visible = false;
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
