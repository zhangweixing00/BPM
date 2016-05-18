using System;
using System.Collections.Specialized;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3026")]
public partial class Workflow_ApprovePage_A_OA_SealOfEWY : A_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.OA_SealOfEWY info = BizContext.OA_SealOfEWY.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbReportCode.Text = info.FormID;
                cblSecurityLevel.SelectedValue = info.SecurityLevel;
                cblUrgenLevel.SelectedValue = info.UrgenLevel;
                tbUserName.Text = info.UserName;
                tbDeptName.Text = info.DeptName;
                tbDateTime.Text = info.DateTime;
                tbTitle.Text = info.Title;
                cblRemark.SelectedValue = info.Remark;
                tbContent.Text = info.Content;
                InitCheckBoxList(info.LeadersSelected);
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
        InitLeader();
        ShowButton();
        SetMenu();
    }


    protected override bool BeforeWorkflowApproval(ref string action, ref string option)
    {
        UploadAttachments1.SaveAttachment(FormId);
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
            ASOptions.Visible = false;
        }
        else
        {
            bool isAddSign = new Workflow_Common().IsAddSign(Request.QueryString["sn"], _BPMContext.CurrentUser.LoginId);
            if (isAddSign)
            {
                Options.Visible = false;
                ASOptions.Visible = true;
            }
            else
            {
                Options.Visible = true;
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
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        NameValueCollection dataFields = new NameValueCollection();
        if (K2_TaskItem.ActivityInstanceDestination.Name == "部门负责人意见")
        {
            if (cbVP.Checked)
            {
                dataFields.Add("VicePresident", Workflow_Common.GetRoleUsers(StartDeptId + "," + Countersign1.Result, "主管副总经理"));
            }
            else
            {
                dataFields.Add("VicePresident", "noapprovers");
            }
            if (cbPresident.Checked)
            {
                dataFields.Add("VicePresident", Workflow_Common.GetRoleUsers(CompanyCode, "总经理"));
            }
            else
            {
                dataFields.Add("President", "noapprovers");
            }
        }

        if (dataFields.Count != 0 && !string.IsNullOrEmpty(_BPMContext.Sn))
        {
            WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, _BPMContext.CurrentUser.ApprovalUser);
        }
    }
    #endregion


    /// <summary>
    /// 初始化领导名称
    /// </summary>
    private void InitLeader()
    {
        if (K2_TaskItem.ActivityInstanceDestination.Name == "部门负责人意见")
        {
            cbVP.Enabled = true;
            cbPresident.Enabled = true;
            Countersign1.IsCanEdit = true;
        }
    }

    private void InitCheckBoxList(string cblCheck)
    {
        if (!string.IsNullOrEmpty(cblCheck))
        {
            string[] array = cblCheck.Split(',');
            cbVP.Checked = array[1].Substring(0, 1) == "0" ? false : true;
            cbPresident.Checked = array[2].Substring(0, 1) == "0" ? false : true;
        }
    }
}
