using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3018")]
public partial class Workflow_ApprovePage_A_HR_EmployeeRegular : A_WorkflowFormBase
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
                tbDeptName.Text = info.DeptName;
                tbEntryTime.Text = info.EntryTime;
                tbProbationPeriod.Text = info.ProbationPeriod;
                tbProbationPeriodStart.Text = info.ProbationPeriodStart;
                tbProbationPeriodEnd.Text = info.ProbationPeriodEnd;
                tbPostLevel.Text = info.PostLevel;
                tbPost.Text = info.Post;
                tbAchievement.Text = info.Achievement;
                tbSign.Text = info.Sign;
                tbSignDate.Value = info.SignDate;
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
                ddlWorkCompletion.Text = info.WorkCompletion;
                tbAdvantage.Text = info.Advantage;
                tbDisadvantage.Text = info.Disadvantage;
                tbSuggest.Text = info.Suggest;
                tbQualityScore.Text = info.QualityScore;
                tbAchievementScore.Text = info.AchievementScore;
                tbTatolScore.Text = info.TatolScore;
                ddlIsAgreeRegular.SelectedValue = info.IsAgreeRegular;
            }
            if (K2_TaskItem.ActivityInstanceDestination.Name == "员工意见")
            {
                tbSignDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                tbSign.Text = CurrentEmployee.EmployeeName;
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
        if (K2_TaskItem.ActivityInstanceDestination.Name == "员工意见")
        {
            tbPost.ReadOnly = false;
            tbAchievement.ReadOnly = false;
            tbSign.ReadOnly = false;
            tbSignDate.Disabled = false;
        }
        else
        {
            tb360.Visible = true;
            tbSignDate.Disabled = true;
        }

        if (K2_TaskItem.ActivityInstanceDestination.Name == "用人部门意见")
        {
            ddlWorkCompletion.Enabled = true;
            ddlIsAgreeRegular.Enabled = true;
            tbAdvantage.ReadOnly = false;
            tbDisadvantage.ReadOnly = false;
            tbSuggest.ReadOnly = false;
        }
        else
        {
            ddlWorkCompletion.Enabled = false;
            ddlIsAgreeRegular.Enabled = false;
        }

        ShowButton();
        SetMenu();
    }

    protected override bool BeforeWorkflowApproval(ref string action, ref string option)
    {
        uploadAttachments.SaveAttachment(FormId);
        switch (action)
        {
            case "提交":
                action = "同意";
                break;
            case "不同意":
                ChangeResultToUnAgree();
                break;
            default:
                break;
        }
        option = string.IsNullOrEmpty(option) ? action : option;
        return true;
    }

    //批准
    protected void Agree_Click(object sender, EventArgs e)
    {
        string action = "同意";
        if (K2_TaskItem.ActivityInstanceDestination.Name == "员工意见")
        {
            if (string.IsNullOrEmpty(tbPost.Text) || string.IsNullOrEmpty(tbAchievement.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('请填写现任职位和试用期关键工作业绩自述');", true);
                return;
            }
        }
        SaveFormData();
        Approval(action);
    }
    //拒绝
    protected void Reject_Click(object sender, EventArgs e)
    {
        string action = "不同意";
        SaveFormData();
        Approval(action);
    }
    //提交
    protected void Submit_Click(object sender, EventArgs e)
    {
        string action = "提交";
        SaveFormData();
        Approval(action);
    }

    /// <summary>
    /// 更改结果
    /// </summary>
    public void ChangeResultToUnAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "0");
        WorkflowManage.ModifyDataField(_BPMContext.Sn, dataFields);
    }

    /// <summary>
    /// 按钮设置1
    /// </summary>
    private void SetMenu()
    {
        if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name == "集团会签")
        {
            UnOptions.Visible = false;
            lbAgree.Text = "提交";
        }
    }

    /// <summary>
    /// 按钮设置2
    /// </summary>
    private void ShowButton()
    {
        if (string.IsNullOrEmpty(Request.QueryString["sn"]))
        {
            Options.Visible = false;
            UnOptions.Visible = false;
            ASOptions.Visible = false;
        }
        else
        {
            bool isAddSign = new Workflow_Common().IsAddSign(Request.QueryString["sn"], _BPMContext.CurrentUser.LoginId);
            if (isAddSign)
            {
                Options.Visible = false;
                UnOptions.Visible = false;
                ASOptions.Visible = true;
            }
            else
            {
                Options.Visible = true;
                UnOptions.Visible = true;
                ASOptions.Visible = false;
            }
        }
    }

    /// <summary>
    /// 执行过程中更新参数
    /// </summary>
    private void UpdateWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();
        switch (K2_TaskItem.ActivityInstanceDestination.Name)
        {
            //示例
            //case "步骤":
            //    dataFields.Add("对应流程参数",Workflow_Common.GetRoleUsers("部门ID", "角色"));
            //    break;
            default:
                break;
        }

        if (dataFields.Count != 0 && !string.IsNullOrEmpty(_BPMContext.Sn))
        {
            WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, _BPMContext.CurrentUser.ApprovalUser);
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

    protected void SaveFormData()
    {
        if (K2_TaskItem.ActivityInstanceDestination.Name == "员工意见")
        {
            var info = BizContext.HR_EmployeeRegular.FirstOrDefault(x => x.FormID == FormId);

            if (info != null)
            {
                info.Post = tbPost.Text;
                info.Achievement = tbAchievement.Text;
                info.Sign = tbSign.Text;
                info.SignDate = tbSignDate.Value;
            }
            BizContext.SubmitChanges();
        }

        if (K2_TaskItem.ActivityInstanceDestination.Name == "用人部门意见")
        {
            var info = BizContext.HR_EmployeeRegular.FirstOrDefault(x => x.FormID == FormId);

            if (info != null)
            {
                info.WorkCompletion = ddlWorkCompletion.SelectedValue;
                info.Advantage = tbAdvantage.Text;
                info.Disadvantage = tbDisadvantage.Text;
                info.Suggest = tbSuggest.Text;
                info.QualityScore = tbQualityScore.Text;
                info.AchievementScore = tbAchievementScore.Text;
                info.TatolScore = SumScore();
                info.IsAgreeRegular = ddlIsAgreeRegular.SelectedValue;
            }
            BizContext.SubmitChanges();
        }
    }

    private string  SumScore()
    {
        float Score = 0;
        Score = Score + float.Parse(tbQualityScore.Text);
        Score = Score + float.Parse(tbAchievementScore.Text);
        Score = Score / 2;
        string[] array = Score.ToString().Split('.');
        return array[0];
    }
    protected void ddlWorkCompletion_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbAchievementScore.Text = ddlWorkCompletion.SelectedValue;
        tbTatolScore.Text = SumScore();
    }
}
