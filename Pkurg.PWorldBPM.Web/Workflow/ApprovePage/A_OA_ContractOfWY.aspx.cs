using System;
using System.Collections.Specialized;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;
using SourceCode.Workflow.Client;

[BPM(AppId = "3024")]
public partial class Workflow_ApprovePage_A_OA_ContractOfWY 
    : A_WorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["sn"]))
            {
                WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
                sn.Value = Request.QueryString["sn"];
                WorklistItem listItem = WorkflowHelper.GetWorklistItemWithSN(sn.Value, "founder\\" + _BPMContext.CurrentUser.LoginId);
                taskID.Value = listItem.ID.ToString();
                nodeID.Value = listItem.ActivityInstanceDestination.ActID.ToString();
                nodeName.Value = listItem.ActivityInstanceDestination.Name;
            }
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
        //初始化勾选部门
        InitCheckButton();
        ShowButton();
        SetMenu();

        //判断分公司和物业集团显示
        if (StartDeptId.Contains("S366-S976"))
        {
            Company.Visible = false;
            Group.Visible = true;
            Group1.Visible = false;
            IsReportToWY.Visible = false;
        }
        else if (!StartDeptId.Contains("S366-S976") && cblIsReportToWY.SelectedItem.Value == "1")
        {
            Company.Visible = true;
            Company1.Visible = true;
            Group.Visible = true;
            IsReportToGroup.Visible = false;
        }
        else if (!StartDeptId.Contains("S366-S976") && cblIsReportToWY.SelectedItem.Value == "0")
        {
            Company.Visible = true;
            Company1.Visible = false;
            Group.Visible = false;
            IsReportToGroup.Visible = false;
        }
    }

    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            ///加载业务数据
            var info = BizContext.OA_ContractOfWY.FirstOrDefault(x => x.FormID == FormId);

            cblSecurityLevel.SelectedValue = info.SecurityLevel;
            cblUrgenLevel.SelectedValue = info.UrgenLevel;
            tbReportCode.Text = info.FormID;

            tbDepartName.Text = info.DeptName;
            tbDateTime.Text = info.DateTime;
            tbUserName.Text = info.UserName;
            tbMobile.Text = info.Mobile;
            cblIsReportToWY.SelectedValue = info.IsReportToWY;
            cblIsReportToGroup.SelectedValue = info.IsReportToGroup;
            ddlContractType1.Text = info.ContractTypeName1;
            ddlContractType2.Text = info.ContractTypeName2;
            ddlContractType3.Text = info.ContractTypeName3;
            tbContractSum.Text = info.ContractSum;
            cblIsSupplementProtocol.SelectedValue = info.IsSupplementProtocol;
            tbSupplementProtocol.Text = info.IsSupplementProtocolText;
            cblIsFormatContract.SelectedValue = info.IsFormatContract;
            cblIsNormText.SelectedValue = info.IsNormText;
            cblIsBidding.SelectedValue = info.IsBidding;
            ddlContractSubject.Text = info.ContractSubjectName;
            tbContractSubject1.Text = info.ContractSubjectName2;
            tbContractSubject2.Text = info.ContractSubjectName3;
            tbContractSubject3.Text = info.ContractSubjectName4;
            tbContractTitle.Text = info.ContractTitle;
            tbContractContent.Text = info.ContractContent;
            //初始化勾选部门
            InitCheckBoxList(info.LeadersSelected);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region  自定义参数

    /// <summary>
    /// 初始化勾选框
    /// </summary>
    /// <param name="cblCheck"></param>
    private void InitCheckBoxList(string cblCheck)
    {
        if (!string.IsNullOrEmpty(cblCheck))
        {
            string[] array = cblCheck.Split(',');
            cbAP.Checked = array[0].Substring(0, 1) == "0" ? false : true;
            cbVP.Checked = array[1].Substring(0, 1) == "0" ? false : true;
        }
    }
    /// <summary>
    /// 初始化在部门负责人审批时的可勾选控件
    /// </summary>
    private void InitCheckButton()
    {
        if (K2_TaskItem.ActivityInstanceDestination.Name == "部门负责人意见" || K2_TaskItem.ActivityInstanceDestination.Name == "集团部门负责人意见")
        {
            cbAP.Enabled = true;
            cbVP.Enabled = true;
            
            if (StartDeptId.Contains("S366-S976"))
            {
                cblIsReportToGroup.Enabled = true;
                Countersign_Group1.IsCanEdit = true;
            }
            else
            {
                cblIsReportToWY.Enabled = true;
                Countersign1.IsCanEdit = true;
            }
        }
        else
        {
            cbAP.Enabled = false;
            cbVP.Enabled = false;
            Countersign1.IsCanEdit = false;
            Countersign_Group1.IsCanEdit = false;
        }
    }

    /// <summary>
    /// 保存数据[使用linq]
    /// </summary>
    private void SaveData()
    {
        var info = BizContext.OA_ContractOfWY.FirstOrDefault(x => x.FormID == FormId);
        //info.LeadersSelected = lblApprovers.Text;
        info.IsReportToGroup = cblIsReportToGroup.SelectedValue;
        info.IsReportToWY = cblIsReportToWY.SelectedValue;
        info.LeadersSelected = SaveLeadersSelected();

        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(info.FormID);
        Countersign1.ProcId = workFlowInstance.InstanceId;
        Countersign1.SaveData(true);
        Countersign_Group1.ProcId = workFlowInstance.InstanceId;
        Countersign_Group1.SaveData(true);

        BizContext.SubmitChanges();
    }
    /// <summary>
    /// 保存勾选框数据
    /// </summary>
    /// <returns></returns>
    private string SaveLeadersSelected()
    {
        string strLeadersSelected = "";
        strLeadersSelected = (cbAP.Checked ? "1" : "0") + ":AP";
        strLeadersSelected += "," + (cbVP.Checked ? "1" : "0") + ":VP";
        return strLeadersSelected;
    }

    #endregion

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
        SaveData();
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
        SaveData();
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
}
