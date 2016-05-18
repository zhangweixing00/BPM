using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;
using SourceCode.Workflow.Client;

public partial class Workflow_ApprovePage_A_JC_TenderSpecialItem : UPageBase
{
    string DefaultApprover = System.Configuration.ConfigurationManager.AppSettings["DefaultApprover"];

    /// <summary>
    /// 加载表单
    /// </summary>
    private void InitFormData(string formId)
    {
        try
        {
            JC_TenderSpecialItemInfo info = JC_TenderSpecialItem.GetJC_TenderSpecialItemInfoByFormID(FormId);
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (info != null)
                {
                    //加载业务数据
                    cblSecurityLevel.SelectedIndex = int.Parse(info.SecurityLevel);
                    cblUrgenLevel.SelectedIndex = int.Parse(info.UrgenLevel);
                    tbDateTime.Text = info.Date ?? "";
                    tbUserName.Text = info.UserName;
                    tbMobile.Text = info.Tel;
                    tbTitle.Text = info.Title;
                    tbContent.Text = info.Substance;
                    tbRemark.Text = info.Remark;
                    cblIsImpowerProject.SelectedIndex = int.Parse(info.IsAccreditByGroup);
                    tbReportCode.Text = info.FormID;
                    ddlDepartName.Text = info.DeptName;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //防止二次提交
        ScriptManager.RegisterOnSubmitStatement(Page, typeof(Page), "Go_disabled", "disabledButton('lbAgree');disabledButton('lbReject');disabledButton('lbSubmit');");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Clear_disabled", "enableButton('lbAgree');enableButton('lbReject');enableButton('lbSubmit');", true);

        if (!IsPostBack)
        {
            //加载页面
            string instId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(instId))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                //初始化表单数据
                InitFormData(FormId);
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }

            ViewState["IsSubmit"] = false;
            //审批界面显示
            if (!string.IsNullOrEmpty(Request.QueryString["sn"]))
            {
                sn.Value = Request.QueryString["sn"];

                WorklistItem listItem = WorkflowHelper.GetWorklistItemWithSN(sn.Value, "founder\\" + _BPMContext.CurrentUser.LoginId);
                taskID.Value = listItem.ID.ToString();
                nodeID.Value = listItem.ActivityInstanceDestination.ActID.ToString();
                nodeName.Value = listItem.ActivityInstanceDestination.Name;
                InitApproveOpinion(nodeName.Value);
                
                //获取查看页面
                hfViewUrl.Value = GetViewUrl();
                if (nodeName.Value.Contains("集团") || nodeName.Value.Contains("执行主任"))
                {
                    tbCompany.Visible = false;
                    tbCompanyCommittee.Visible = false;
                    tbGroup.Visible = true;
                    tbGroupCommittee.Visible = true;
                }
                else
                {
                    tbCompany.Visible = true;
                    tbGroup.Visible = false;
                    tbGroupCommittee.Visible = false;
                    tbCompanyCommittee.Visible = cblIsImpowerProject.SelectedItem.Value == "1" ? true : false;
                }
            }
            else
            {
                ExceptionHander.GoToErrorPage("该审批环节已结束", null);
            }
        }
        //显示按钮
        ShowButton();
        //设置菜单
        SetMenu();
    }

    /// <summary>
    /// 设置菜单
    /// </summary>
    private void SetMenu()
    {
        if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name == "会签")
        {
            UnOptions.Visible = false;
            lbAgree.Text = "提交";
        }
    }
    /// <summary>
    /// 显示按钮
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
            bool isAddSign = new Workflow_Common().IsAddSign(Request.QueryString["sn"], UploadAttachments1._BPMContext.CurrentUser.LoginId);
            bool isChangeSign = new Workflow_Common().IsChangeSign(Request.QueryString["sn"], UploadAttachments1._BPMContext.CurrentUser.LoginId);
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

        //if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name == "集团会签")
        //{
        //    ChangeSign1.Visible = true;
        //}
    }

    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();
    /// <summary>
    /// 意见框处理
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    private string GetApproveOpinion(string nodeName)
    {
        string opinion = "";
        List<UserControls_ApproveOpinionUC> options = GetOptions();
        foreach (var item in options)
        {
            if (nodeName == item.CurrentNodeName)
            {
                opinion = ((TextBox)item.FindControl("tbDeptLeaderOpion")).Text;
                return opinion;
            }
        }
        return opinion;
    }

    /// <summary>
    /// 得到意见列表
    /// </summary>
    /// <returns></returns>
    private List<UserControls_ApproveOpinionUC> GetOptions()
    {
        List<UserControls_ApproveOpinionUC> options = new List<UserControls_ApproveOpinionUC>();
        //将各个领导意见添加到list中
        options.Add(OpinionOperateDeptleader);
        options.Add(OpinionRealateDept);
        options.Add(OpinionGroupRealateDept);
        options.Add(OpinionExecutiveDirector);
        options.Add(OpinionTenderCommitteeLeader);
        options.Add(OpinionTenderCommitteeChairman);
        options.Add(OpinionGroupTenderCommitteeLeader);
        options.Add(OpinionGroupTenderCommitteeChairman);
        return options;
    }
    /// <summary>
    /// 初始化审批意见
    /// </summary>
    /// <param name="p"></param>
    private void InitApproveOpinion(string nodeName)
    {
        TextBox opinion = new TextBox();

        List<UserControls_ApproveOpinionUC> options = GetOptions();
        foreach (var item in options)
        {
            item.InstanceId = _BPMContext.ProcID;
            if (nodeName == item.CurrentNodeName)
            {
                opinion = ((TextBox)item.FindControl("tbDeptLeaderOpion"));
                hf_OpId.Value = opinion.ClientID;
                item.CurrentNode = true;
            }
        }
    }

    /// <summary>
    /// 更改结果
    /// </summary>
    private void ChangeResultToUnAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "0");
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
    }
    /// <summary>
    /// 批准事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Agree_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);

        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(id);
            string action = "同意";
            string currentName = K2_TaskItem.ActivityInstanceDestination.Name;
            if (currentName == "招标委员会主任意见" || currentName == "集团招标委员会主任意见")
            {
                //如果招标委员会意见是同意，则之前的意见可以忽略，只看招标委员会主任的意见
                ChangeResultToAgree();
            }
            bool isSuccess = WorkflowHelper.ApproveProcess(sn.Value, action, "founder\\" + _BPMContext.CurrentUser.LoginId);
            string optionDefault = "同意";
            string Opinion = "";
            if (GetApproveOpinion(nodeName.Value) == null || GetApproveOpinion(nodeName.Value) == "")
            {
                Opinion = optionDefault;
            }
            else
            {
                Opinion = GetApproveOpinion(nodeName.Value);
            }
            string ApproveResult = "同意";
            string OpinionType = "";
            string IsSign = "0";
            string DelegateUserName = "";
            string DelegateUserCode = "";

            if (isSuccess && !(bool)ViewState["IsSubmit"])
            {
                var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
                {
                    //????
                     ApprovalID = Guid.NewGuid().ToString(),
                     WFTaskID = int.Parse(taskID.Value),
                    FormID = id,
                    InstanceID = workFlowInstance.InstanceId,
                    Opinion = Opinion,
                    ApproveAtTime = DateTime.Now,
                    ApproveByUserCode = CurrentEmployee.EmployeeCode,
                    ApproveByUserName = CurrentEmployee.EmployeeName,
                    ApproveResult = ApproveResult,
                    OpinionType = OpinionType,
                    CurrentActiveName = nodeName.Value,
                    ISSign = IsSign,
                    CurrentActiveID= nodeID.Value,
                    DelegateUserName = DelegateUserName,
                    DelegateUserCode = DelegateUserCode,
                    CreateAtTime = DateTime.Now,
                    CreateByUserCode = CurrentEmployee.EmployeeCode,
                    CreateByUserName = CurrentEmployee.EmployeeName,
                    UpdateAtTime = DateTime.Now,
                    UpdateByUserCode = CurrentEmployee.EmployeeCode,
                    UpdateByUserName = CurrentEmployee.EmployeeName,
                    FinishedTime = DateTime.Now
                };
                ViewState["IsSubmit"] = true;
                if (bfApproval.AddApprovalRecord(appRecord))
                {
                    wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                }
            }
        }
    }

    /// <summary>
    /// 如果招标委员会意见是同意，则之前的意见可以忽略，只看招标委员会主任的意见
    /// </summary>
    private void ChangeResultToAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "1");
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
    }
    /// <summary>
    /// 拒绝事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Reject_Click(object sender, EventArgs e)
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(FormId);
            string action = "不同意";
            //得到节点名称
            string currentName = K2_TaskItem.ActivityInstanceDestination.Name;
            if (currentName == "集团招标委员会意见" || currentName == "招标委员会意见")
            {
                action = "同意";
                ChangeResultToNext();//在招标委员会节点上不同意时触发的函数【流程扭转到下一个招标委员会主任节点】
            }
            else
            {
                ChangeResultToUnAgree();//不同意时触发的函数
            }

            bool isSuccess = WorkflowHelper.ApproveProcess(sn.Value, action, "founder\\" + _BPMContext.CurrentUser.LoginId);
            string Opinion = GetApproveOpinion(nodeName.Value);
            if (string.IsNullOrEmpty(Opinion))
            {
                Opinion = "不同意";
            }
            string ApproveResult = "不同意";
            string OpinionType = "";
            string IsSign = "0";
            string DelegateUserName = "";
            string DelegateUserCode = "";
            if (isSuccess)
            {
                var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
                {
                     ApprovalID = Guid.NewGuid().ToString(),
                     WFTaskID = int.Parse(taskID.Value),
                     FormID = FormId,
                    InstanceID = workFlowInstance.InstanceId,
                    Opinion = Opinion,
                    ApproveAtTime = DateTime.Now,
                    ApproveByUserCode = CurrentEmployee.EmployeeCode,
                    ApproveByUserName = CurrentEmployee.EmployeeName,
                    ApproveResult = ApproveResult,
                    OpinionType = OpinionType,
                    CurrentActiveName = nodeName.Value,
                    ISSign = IsSign,
                    CurrentActiveID= nodeID.Value,
                    DelegateUserName = DelegateUserName,
                    DelegateUserCode = DelegateUserCode,
                    CreateAtTime = DateTime.Now,
                    CreateByUserCode = CurrentEmployee.EmployeeCode,
                    CreateByUserName = CurrentEmployee.EmployeeName,
                    UpdateAtTime = DateTime.Now,
                    UpdateByUserCode = CurrentEmployee.EmployeeCode,
                    UpdateByUserName = CurrentEmployee.EmployeeName,
                    FinishedTime = DateTime.Now
                };

                bfApproval.AddApprovalRecord(appRecord);
                wf_WorkFlowInstance.UpdateNowStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);

        }
    }

    /// <summary>
    /// 招标委员会节点不同意时扭转到招标委员会主任节点上
    /// </summary>
    private void ChangeResultToNext()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsCancelTimeOutSkip", "1");
        dataFields.Add("IsPass", "1");
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
    }
    /// <summary>
    /// 提交事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);

        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(id);

            WorkflowHelper.BackToPreApprover(sn.Value, "founder\\" + _BPMContext.CurrentUser.LoginId);

            string Opinion = GetApproveOpinion(nodeName.Value);
            if (string.IsNullOrEmpty(Opinion))
            {
                Opinion = "同意";
            }
            string ApproveResult = "同意";
            string OpinionType = "";
            string IsSign = "2";//会签步骤
            string DelegateUserName = "";
            string DelegateUserCode = "";

            var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
            {
                 ApprovalID = Guid.NewGuid().ToString(),
                 WFTaskID = int.Parse(taskID.Value),
                FormID = id,
                InstanceID = workFlowInstance.InstanceId,
                Opinion = Opinion,
                ApproveAtTime = DateTime.Now,
                ApproveByUserCode = CurrentEmployee.EmployeeCode,
                ApproveByUserName = CurrentEmployee.EmployeeName,
                ApproveResult = ApproveResult,
                OpinionType = OpinionType,
                CurrentActiveName = nodeName.Value,
                ISSign = IsSign,
                CurrentActiveID = nodeID.Value,
                DelegateUserName = DelegateUserName,
                DelegateUserCode = DelegateUserCode,
                CreateAtTime = DateTime.Now,
                CreateByUserCode = CurrentEmployee.EmployeeCode,
                CreateByUserName = CurrentEmployee.EmployeeName,
                UpdateAtTime = DateTime.Now,
                UpdateByUserCode = CurrentEmployee.EmployeeCode,
                UpdateByUserName = CurrentEmployee.EmployeeName,
                FinishedTime = DateTime.Now
            };

            if (bfApproval.AddApprovalRecord(appRecord))
            {
                wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
        }
    }
    /// <summary>
    /// 弹出对话框事件
    /// </summary>
    /// <param name="Page"></param>
    /// <param name="p"></param>
    public void Alert(string msg)
    {
        DisplayMessage.ExecuteJs(string.Format("alert('{0}');", msg));
    }

    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        try
        {
            WorklistItem taskItem = WorkflowHelper.GetWorklistItemWithSN(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);

        }
        catch (Exception)
        {
            hf_OpId.Value = "-1";
        }
    }

    /// <summary>
    /// 取消事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["BackUrl"].ToString(), false);
    }
    /// <summary>
    /// FormId
    /// </summary>
    public string FormId
    {
        get
        {
            return ViewState["FormID"].ToString();
        }
        set
        {
            ViewState["FormID"] = value;
        }
    }

    public string GetViewUrl()
    {
        string AUrl = Request.Url.ToString();
        string ViewUrl = "";
        string StrTemp = AUrl.Substring(0, AUrl.IndexOf("ApprovePage"));
        ViewUrl = StrTemp + "ViewPage/V_JC_TenderSpecialItem.aspx?ID=" + Request.QueryString["id"];
        return ViewUrl;
    }
}