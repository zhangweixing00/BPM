using System;
using System.Linq;

[BPM(AppId = "3019")]
public partial class Workflow_ViewPage_V_HR_EmployeeTransfer : V_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.HR_EmployeeTransfer info = BizContext.HR_EmployeeTransfer.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                InitApproveList2(info.IsGroup);
                tbReportCode.Text = info.FormID;
                tbUserName.Text = info.UserName;
                tbSex.Text = info.Sex;
                tbEntryTime.Text = info.EntryTime;
                tbGraduation.Text = info.Graduation;
                tbEducation.Text = info.Education;
                tbFounderTime.Text = info.FounderTime;
                tbDeptName.Text = info.DeptName;
                tbPost.Text = info.Post;
                tbPostLevel.Text = info.PostLevel;
                tbToDeptName.Text = info.ToDeptName;
                tbToPost.Text = info.ToPost;
                tbToPostLevel.Text = info.ToPostLevel;
                tbLabourContractStart.Text = info.LabourContractStart;
                tbLabourContractEnd.Text = info.LabourContractEnd;
                tbToLabourContractStart.Text = info.ToLabourContractStart;
                tbToLabourContractEnd.Text = info.ToLabourContractEnd;
                tbTransferReason.Text = info.TransferReason;
                tbRemark.Text = info.Remark;

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
