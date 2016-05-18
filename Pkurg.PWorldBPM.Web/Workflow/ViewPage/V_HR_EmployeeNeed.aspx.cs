using System;
using System.Linq;

[BPM(AppId = "3016")]
public partial class Workflow_ViewPage_V_HR_EmployeeNeed : V_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.HR_EmployeeNeed info = BizContext.HR_EmployeeNeed.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbAge.Text = info.Age;
                tbDeptName.Text = info.DeptName;
                tbReason.Text = info.Reason;
                tbEducation.Text = info.Education;
                tbSex.Text = info.Sex;
                tbSpecialty.Text = info.Specialty;
                tbTitle.Text = info.Title;
                tbWorkingLifetim.Text = info.WorkingLifetime;

                tbReportCode.Text = info.FormID;
                tbDateTime.Text = info.DateTime;
                tbPosition.Text = info.Position;
                tbNumber.Text = info.Number;
                tbMajorDuty.Text = info.MajorDuty.Replace(" ", "&nbsp;").Replace("\n", "<br/>"); 
                tbProfessionalAbility.Text = info.ProfessionalAbility.Replace(" ", "&nbsp;").Replace("\n", "<br/>"); ;
                tbWorkTime.Text = info.WorkTime;

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
