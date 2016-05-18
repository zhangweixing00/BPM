using System;
using System.Collections.Specialized;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3019")]
public partial class Workflow_ApprovePage_A_HR_EmployeeTransfer : A_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected void InitFormData()
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
                tbSalary.Text = info.Salary;
                tbRatio.Text = info.Ratio;
                tbAnnualSalary.Text = info.AnnualSalary;
                tbToSalary.Text = info.ToSalary;
                tbToRatio.Text = info.ToRatio;
                tbToAnnualSalary.Text = info.ToAnnualSalary;
                cblTransferReason.SelectedValue = info.TransferReason;
                tbRemark.Text = info.Remark;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void SaveFormData()
    {
        var info = BizContext.HR_EmployeeTransfer.FirstOrDefault(x => x.FormID == FormId);

        if (info != null)
        {
            info.TransferReason = cblTransferReason.SelectedValue;
        }
        BizContext.SubmitChanges();
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
            if (K2_TaskItem.ActivityInstanceDestination.Name == "员工意见")
            {
                cblTransferReason.Enabled = true;
            }
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
        Approval(action);
        SaveFormData();
    }
    //拒绝
    protected void Reject_Click(object sender, EventArgs e)
    {
        string action = "不同意";
        Approval(action);
    }
    //提交
    protected void Submit_Click(object sender, EventArgs e)
    {
        string action = "提交";
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

    //WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    //BFApprovalRecord bfApproval = new BFApprovalRecord();

    #region 执行过程中更新参数

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
    #endregion

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
