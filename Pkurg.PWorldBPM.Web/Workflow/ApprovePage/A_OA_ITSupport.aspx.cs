using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3014")]
public partial class Workflow_ApprovePage_A_OA_ITSupport
    : A_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected void InitFormData()
    {
        try
        {
            var info = BizContext.OA_ITSupport_Form.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbDepartName.Text = info.DeptName;
                tbCompany.Text = info.CompanyName;
                tbDate.Text = info.DateTime.ToString();
                tbEmail.Text = info.Email;
                tbPerson.Text = info.UserName;
                tbPhone.Text = info.Mobile;

                tbQuestion.Text = "--";
                if (info.QuestionId.HasValue && info.QuestionId.Value != -1)
                {
                    var qInfo = BizContext.V_ITSupport_Question.FirstOrDefault(x => x.Id == info.QuestionId.Value);
                    if (qInfo != null)
                    {
                        tbQuestion.Text = qInfo.QuestionKey;
                    }
                }
                tbType.Text = "--";
                if (info.STypeId.HasValue && info.STypeId.Value != -1)
                {
                    var qInfo = BizContext.V_ITSupport_Catalog.FirstOrDefault(x => x.Id == info.STypeId.Value);
                    if (qInfo != null)
                    {
                        tbType.Text = qInfo.Name;
                    }
                }

                tbContent.Text = info.ContentTxt;

                lbFormTitle.Text = FormTitle;
                CurrentStep = info.CurrentStepId;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public string CurrentStep
    {
        get
        {
            if (ViewState["CurrentStep"] == null)
            {
                var list = BizContext.OA_ITSupport_Step.Where(x => x.FormID == FormId);
                ViewState["CurrentStep"] = list.OrderBy(x => x.OrderId).FirstOrDefault().Id;
            }
            return ViewState["CurrentStep"].ToString();
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
            currentNodeName = CustomWorkflowHelper.SuperNodeName + "," + currentActivityName;
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
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }

        ShowButton();
    }



    public string NextUser { get; set; }

    protected override bool BeforeWorkflowApproval(ref string action, ref string option)
    {
        uploadAttachments.SaveAttachment(FormId);
        switch (action)
        {
            case "领取":
                option = string.IsNullOrEmpty(option) ? "已领取" : option;
                break;
            case "处理":
                option = string.IsNullOrEmpty(option) ? "处理完成" : option;
                break;
            case "驳回":
                option = string.IsNullOrEmpty(option) ? "处理完成" : option;
                // action = "处理";
                return ChangeResultToUnAgree();
            case "提交":
                NextUser = WorkflowHelper.GetBackToPreApproverUser(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
                NextUser = NextUser.Replace("founder\\", "");
                option = string.IsNullOrEmpty(option) ? "处理完成" : option;
                break;
            default:
                break;
        }
        return true;

    }
    protected override bool AfterWorkflowApproval(string action, string option, bool isSuccess)
    {
        if (isSuccess)
        {
            ///对于加签和转签
            ///对于一步转签造成的流程结束在流程结束时关闭步骤
            //if (action == "提交" && string.IsNullOrEmpty(NextUser))
            //{
            //    return true;
            //}

            ///更新当前步骤审批人处理记录
            ///

            var formInfo = BizContext.OA_ITSupport_Form.FirstOrDefault(x => x.FormID == FormId);
            var currentStep = BizContext.OA_ITSupport_Step.FirstOrDefault(x => x.Id == formInfo.CurrentStepId);
            currentStep.FinishTime = DateTime.Now;

            if (action == "领取")
            {
                string stepId = Guid.NewGuid().ToString();
                BizContext.OA_ITSupport_Step.InsertOnSubmit(new Pkurg.PWorldBPM.Business.BIZ.OA_ITSupport_Step()
                {
                    Id = stepId,
                    FormID = FormId,
                    InstanceId = _BPMContext.ProcID,
                    StartTime = DateTime.Now,
                    OrderId = currentStep.OrderId.Value + 1,
                    StartType = (int)ITSupportStatus.处理,
                    ProcessGroupId = currentStep.ProcessGroupId
                });

                BizContext.OA_ITSupport_Step_Users.InsertOnSubmit(new OA_ITSupport_Step_Users()
                {
                    StepId = stepId,
                    UserCode = _BPMContext.CurrentPWordUser.EmployeeCode
                    ,
                    LoginName = _BPMContext.CurrentUser.LoginId
                });

                formInfo.CurrentStepId = stepId;
            }
            else if (action == "提交" && !string.IsNullOrEmpty(NextUser))
            {
                string stepId = Guid.NewGuid().ToString();
                BizContext.OA_ITSupport_Step.InsertOnSubmit(new Pkurg.PWorldBPM.Business.BIZ.OA_ITSupport_Step()
                {
                    Id = stepId,
                    FormID = FormId,
                    InstanceId = _BPMContext.ProcID,
                    StartTime = DateTime.Now,
                    OrderId = currentStep.OrderId.Value + 1,
                    StartType = (int)ITSupportStatus.处理,
                    ProcessGroupId = currentStep.ProcessGroupId
                });
                var stepUser = SysContext.V_Pworld_UserInfo.FirstOrDefault(x => x.LoginName == NextUser);

                BizContext.OA_ITSupport_Step_Users.InsertOnSubmit(new OA_ITSupport_Step_Users()
                {
                    StepId = stepId,
                    UserCode = stepUser.EmployeeCode,
                    LoginName = stepUser.LoginName
                });

                formInfo.CurrentStepId = stepId;
            }
            else//处理或驳回
            {
                formInfo.CurrentStepId = "-1";
                if (action == "处理")
                {
                    formInfo.ProcessResult = 1;
                }
                if (action == "驳回")
                {
                    formInfo.ProcessResult = 2;
                }
            }

            BizContext.SubmitChanges();
        }
        return true;
    }
    /// <summary>
    /// 正式发布父类后可去掉
    /// </summary>
    /// <returns></returns>
    public bool IsApprovaled()
    {
        try
        {
            var kItem = WorkflowHelper.GetWorklistItemWithSN(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
            if (kItem != null)
            {
                return false;
            }
            return true;
        }
        catch
        {
            return true;
        }
    }
    protected void lbGet_Group_Click(object sender, EventArgs e)
    {
        string action = "领取";
        if (IsApprovaled())
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('手慢了，该工单已被领取了！'); window.opener.location.href=window.opener.location.href; ", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
            return;
        }
        Approval(action);
    }
    protected void lbFinish_Click(object sender, EventArgs e)
    {
        string action = "处理";
        Approval(action);
    }
    protected void lbUnFinish_Click(object sender, EventArgs e)
    {
        string action = "驳回";
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
    /// 按钮设置,加签后转签不可用，所以不涉及加签和转签嵌套的情况
    /// </summary>
    private void ShowButton()
    {
        if (string.IsNullOrEmpty(Request.QueryString["sn"]))
        {
            // Options_GroupStep.Visible = Options_Single.Visible = Options_Submit.Visible = false;
            SetButtons(null);
        }
        else
        {
            if (K2_TaskItem == null || K2_TaskItem.ActivityInstanceDestination == null)
            {
                SetButtons(null);
                return;
            }
            if (K2_TaskItem.ActivityInstanceDestination.Name == "待领取")
            {
                SetButtons(Options_GroupStep);
                AddSign1.Visible = false;//加签不可用
            }
            else
            {
                bool isAddSign = new Workflow_Common().IsAddSign(Request.QueryString["sn"], _BPMContext.CurrentUser.LoginId);
                if (isAddSign)
                {
                    SetButtons(Options_Submit);
                }
                else
                    SetButtons(Options_Single);
            }
        }

    }

    public void SetButtons(System.Web.UI.HtmlControls.HtmlGenericControl control)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl[] controls = { Options_GroupStep, Options_Single, Options_Submit };
        foreach (var item in controls)
        {
            item.Visible = control != null && item.ID == control.ID;
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
