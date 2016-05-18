using System;
using System.Linq;

[BPM(AppId = "3021")]
public partial class Workflow_ViewPage_V_HR_SalaryAdjust : E_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.HR_SalaryAdjust info = BizContext.HR_SalaryAdjust.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbReportCode.Text = info.FormID;
                tbAnnualSalary.Text = info.AnnualSalary;
                tbDeptName.Text = info.DeptName;
                tbEffectiveDate.Text = info.EffectiveDate;
                tbPost.Text = info.Post;
                tbRatio.Text = info.Ratio;
                tbReason.Text = info.Reason;
                tbSalary.Text = info.Salary;
                tbToAnnualSalary.Text = info.ToAnnualSalary;
                tbToDeptName.Text = info.ToDeptName;
                tbToPost.Text = info.ToPost;
                tbToRatio.Text = info.ToRatio;
                tbToSalary.Text = info.ToSalary;
                tbUserName.Text = info.UserName;
                tbWorkPlace.Text = info.WorkPlace;
                hfApprovers.Value = info.LeadersSelected;

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
                lbDirector.Text = "分管领导意见：";
                lbPresident.Text = "CEO意见：";
                lbTitle.Text = "";
            }
            else if (IsGroup == "2")
            {
                trRDeptManager.Visible = false;
            }
            else if (IsGroup == "3")
            {
                lbDirector.Text = "分管领导意见：";
                lbPresident.Text = "CEO意见：";
                trRDeptManager.Visible = false;
                lbTitle.Text = "";
            }
        }
    }
}
