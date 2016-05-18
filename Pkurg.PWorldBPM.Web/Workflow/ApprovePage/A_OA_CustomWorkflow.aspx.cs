using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3011")]
public partial class Workflow_ApprovePage_A_OA_CustomWorkflow
    : A_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected void InitFormData()
    {
        try
        {
            var info = BizContext.OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbTheme.Text = info.Title;
                tbContent.Text = info.ContentTxt.ToHtmlString();
                tbDate.Text = info.DateTime.Value.ToShortDateString();
                tbPerson.Text = info.UserName;
                tbPhone.Text = info.Mobile;
                lbDepartName.Text = info.DeptName;
                SetCurrentStep(info.CurrentStepId.Value);
                if (!string.IsNullOrEmpty(info.SecurityLevel))
                {
                    lbFormTitle.Text = info.SecurityLevel;
                }
                else
                {
                    lbFormTitle.Text = info.Title;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetCurrentStep(long stepId)
    {
        var list = CustomWorkflowDataProcess.GetWorkItemsData(_BPMContext.ProcID, FormId);
        if (stepId <= 1)
        {

            CurrentStep = list.OrderBy(x => x.OrderId).FirstOrDefault().StepID;
        }
        else
        {
            CurrentStep = stepId;
        }
    }


    public long CurrentStep
    {
        get
        {
            if (ViewState["CurrentStep"] == null)
            {
                var list = CustomWorkflowDataProcess.GetWorkItemsData(_BPMContext.ProcID, FormId);
                ViewState["CurrentStep"] = list.OrderBy(x => x.OrderId).FirstOrDefault().StepID;
            }
            return long.Parse(ViewState["CurrentStep"].ToString());
        }
        set { ViewState["CurrentStep"] = value; }
    }

    public string GetCurrentNodeName(string stepId)
    {
        string currentNodeName = "";
        var list = CustomWorkflowDataProcess.GetWorkItemsData(_BPMContext.ProcID, FormId);
        string currentActivityName = list.FirstOrDefault(x => x.StepID.ToString() == stepId).StepName;
        if (stepId == CurrentStep.ToString())
        {
            CustomActivityName = currentActivityName;
            currentNodeName = CustomWorkflowHelper.SuperNodeName+"," + currentActivityName;
        }
        else
            currentNodeName = currentActivityName;
        //}

        return currentNodeName;
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
                InitFlowData();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }

        ShowButton();

        SetMenu();
    }

    private void InitFlowData()
    {
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = CustomWorkflowDataProcess.GetWorkItemsData(_BPMContext.ProcID, FormId);
        if (itemInfos != null)
        {
            rptList.DataSource = itemInfos;
            rptList.DataBind();
        }
    }


    protected override bool BeforeWorkflowApproval(ref string action, ref string option)
    {
        uploadAttachments.SaveAttachment(FormId);
        switch (action)
        {
            case "同意":
                option = string.IsNullOrEmpty(option) ? action : option;
                break;
            case"提交":
                if (option == "")
                {
                    option = "同意";
                }
                break;
            case "不同意":
                option = string.IsNullOrEmpty(option) ? action : option;
                return ChangeResultToUnAgree();
            default:
                break;
        }
        return true;

    }
    protected override bool AfterWorkflowApproval(string action, string option, bool isSuccess)
    {
        if (isSuccess)
        {
            ///对于加签
            if (action == "提交")
            {
                return true;
            }

            ///更新当前步骤审批人处理记录
            var currentStep = SysContext.WF_Custom_InstanceItems.FirstOrDefault(x => x.StepID == CurrentStep);
            List<CustomWorkflowUserInfo> userinfos = currentStep.PartUsers.ToUserList();
            var currentParter = userinfos.FirstOrDefault(x => x.UserInfo.LoginName == _BPMContext.CurrentUser.LoginId);
            if (currentParter != null)
            {
                currentParter.IsApproval = true;
                currentStep.PartUsers = userinfos.ToXml();
                SysContext.SubmitChanges();
            }

            if (userinfos.Count(x => !x.IsApproval) == 0)
            {
                //本步骤所有人已经执行审批

                ///更新当前自定义实例步骤
                if (action == "同意")
                {
                    var list = CustomWorkflowDataProcess.GetWorkItemsData(_BPMContext.ProcID, FormId);
                    var nextInfo = list.Where(x => x.StepID > CurrentStep&&!string.IsNullOrEmpty(x.PartUsers)).OrderBy(x => x.OrderId).FirstOrDefault();
                    var updateInfo = BizContext.OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == FormId);
                    if (nextInfo != null)
                    {
                        updateInfo.CurrentStepId = nextInfo.StepID;

                    }
                    else
                    {
                        updateInfo.CurrentStepId = -1;//没有后续节点，流程结束
                    }
                    BizContext.SubmitChanges();
                }
                else
                {
                    var updateInfo = BizContext.OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == FormId);

                    updateInfo.CurrentStepId = -1;//不同意，流程结束

                    BizContext.SubmitChanges();
                }
            }
        }


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
    public bool ChangeResultToUnAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "0");
        return WorkflowManage.ModifyDataField(_BPMContext.Sn, dataFields);
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
