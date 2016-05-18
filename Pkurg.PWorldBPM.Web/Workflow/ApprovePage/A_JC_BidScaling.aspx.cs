using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;
using SourceCode.Workflow.Client;

public partial class Workflow_ApprovePage_A_JC_BidScaling : UPageBase
{
    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];

    string DefaultApprover = System.Configuration.ConfigurationManager.AppSettings["DefaultApprover"];

    public string className = "Workflow_ApprovePage_A_JC_BidScaling";

    JC_BidScaling bs = new JC_BidScaling();
    JC_BidScalingList bsl = new JC_BidScalingList();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();

    protected void Page_Load(object sender, EventArgs e)
    {
        string methodName = "Page_Load";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);

        //防止二次提交
        ScriptManager.RegisterOnSubmitStatement(Page, typeof(Page), "Go_disabled", "disabledButton('lbAgree');disabledButton('lbReject');disabledButton('lbSubmit');");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Clear_disabled", "enableButton('lbAgree');enableButton('lbReject');enableButton('lbSubmit');", true);
        if (!IsPostBack)
        {
            ViewState["IsSubmit"] = false;
            if (!string.IsNullOrEmpty(Request.QueryString["sn"]))
            {
                WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
                sn.Value = Request.QueryString["sn"];
                WorklistItem listItem = WorkflowHelper.GetWorklistItemWithSN(sn.Value, "founder\\" + _BPMContext.CurrentUser.LoginId);
                taskID.Value = listItem.ID.ToString();
                nodeID.Value = listItem.ActivityInstanceDestination.ActID.ToString();
                nodeName.Value = listItem.ActivityInstanceDestination.Name;
                InitApproveOpinion(nodeName.Value);

                if (tbDepartName.Text.Contains("S363") || nodeName.Value.Contains("集团") || nodeName.Value.Contains("执行主任"))
                {
                    tbCompany.Visible = false;
                    tbGroup.Visible = true;
                    tbURL.Visible = true;
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                ViewState["FormID"] = _BPMContext.ProcInst.FormId;
                BindFormData();
                BindSelectUnit();
                hfViewUrl.Value = GetViewUrl();//获取查看页面
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
            ShowButton();
        }      
    }

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
        if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name == "集团会签")
        {
            UnOptions.Visible = false;
            lbAgree.Text = "提交";
            ChangeSign1.Visible = true;
        }
    }

    private void BindFormData()
    {
        string methodName = "BindFormData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            JC_BidScalingInfo obj = bs.GetBidScaling(ViewState["FormID"].ToString());
            if (obj != null)
            {
                tbDepartName.Text = obj.DeptName;;
                tbTitle.Text = obj.Title;
                tbDateTime.Text = obj.DateTime;
                tbContent.Text = obj.Content;
                tbEntranceTime.Text = obj.EntranceTime;
                cblIsAccreditByGroup.SelectedValue = obj.IsAccreditByGroup != null ? obj.IsAccreditByGroup.ToString() : "-1";

                if (cblIsAccreditByGroup.SelectedValue=="1")
                {
                    if ( nodeName.Value.Contains("招标委员会"))
                    {
                        tbCompany.Visible = false;
                        tbGroup.Visible = true;
                        tbURL.Visible = true;
                    }
                }
                if (obj.DeptName.Contains("开封"))
                {
                    cblFirstLevel.Visible = true;
                    cblFirstLevel.SelectedValue = obj.FirstLevel != null ? obj.FirstLevel.ToString() : "-1";
                }
                tbFirstUnit.Text = obj.FirstUnit;
                tbSecondUnit.Text = obj.SecondUnit;
                string StartDeptId = obj.StartDeptCode;
                if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) == PKURGICode)
                {
                    trCounterSign.Visible = false;
                    lbIsImpowerProject.Visible = false;
                    cblIsAccreditByGroup.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

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

    private List<UserControls_ApproveOpinionUC> GetOptions()
    {
        List<UserControls_ApproveOpinionUC> options = new List<UserControls_ApproveOpinionUC>();
        options.Add(ApproveOpinionUC1);
        options.Add(ApproveOpinionUC2);
        options.Add(OpinionExecutiveDirector);
        options.Add(ApproveOpinionUC3);
        options.Add(ApproveOpinionUC4);
        return options;
    }

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

    //批准
    protected void Agree_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);
        bool IsSubmit = true;
        if (nodeName.Value == "招标委员会意见" || nodeName.Value == "招标委员会主任意见" || nodeName.Value == "执行主任")
        {
            if (cblSelectUnit.SelectedIndex == -1)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('请选择入围单位'); window.opener.location.href=window.opener.location.href; ", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "return false;", true);
                IsSubmit = false;
            }
            else
            {
                SaveSelectUnit();
                if (nodeName.Value == "招标委员会主任意见")
                SaveScalingResult();
            }
        }
        
        if(IsSubmit)
        {
            if (workFlowInstance != null)
            {
                UploadAttachments1.SaveAttachment(id);
                string action = "同意";
                bool isSuccess = WorkflowHelper.ApproveProcess(sn.Value, action, "founder\\" + _BPMContext.CurrentUser.LoginId);
                string Opinion = "";
                if (GetApproveOpinion(nodeName.Value) == null || GetApproveOpinion(nodeName.Value) == "")
                {
                    Opinion = "同意";
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

                    if (bfApproval.AddApprovalRecord(appRecord))
                    {
                        ViewState["IsSubmit"] = true;
                        wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);

                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                    }
                }
            }
        }
    }

    //拒绝
    protected void Reject_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);
        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(id);
            string action = "不同意";
            ChangeResultToUnAgree();
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
            if (isSuccess && !(bool)ViewState["IsSubmit"])
            {
                NameValueCollection dataFields = new NameValueCollection();
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
                ViewState["IsSubmit"] = true;
                wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);

                //if (jc.UpdateStatus(id, "01"))
                //{ }
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);

            }
        }
    }

    //提交
    protected void Submit_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);

        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(id);
            bool isSuccess = WorkflowHelper.BackToPreApprover(sn.Value, "founder\\" + _BPMContext.CurrentUser.LoginId);

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
            if (isSuccess && !(bool)ViewState["IsSubmit"])
            {
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

                if (bfApproval.AddApprovalRecord(appRecord))
                {
                    wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);

                    //if (jc.UpdateStatus(id, "03"))
                    //{}
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                }
            }
        }
    }

    private void SaveSelectUnit()
    {
        string methodName = "SaveSelectUnit";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);

        JC_BidScalingListInfo obj = new JC_BidScalingListInfo();
        string BidCommittee = string.Empty;
        try
        {
            obj.FormID = ViewState["FormID"].ToString();
            obj.UserCode = _BPMContext.CurrentUser.Id;
            obj.UserName = _BPMContext.CurrentUser.Name;
            obj.CreatTime = DateTime.Now.ToShortDateString();
            
            if (cblSelectUnit.SelectedIndex != -1)
            {
                obj.SelectResult = cblSelectUnit.SelectedIndex.ToString();
            }
            bsl.InsertBidScalingList(obj);

            BidCommittee = obj.UserCode+","+bs.GetBidScaling(obj.FormID).BidCommittee;
            bs.UpdateBidCommittee(obj.FormID, BidCommittee);
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    private void SaveScalingResult()
    {
        string methodName = "SaveScalingResult";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);

        string ScalingResult = string.Empty;
        try
        {
            if (cblSelectUnit.SelectedIndex == 0)
            {
                ScalingResult = tbFirstUnit.Text.ToString();
            }
            else if (cblSelectUnit.SelectedIndex == 1)
            {
                ScalingResult = tbSecondUnit.Text.ToString();
            }

            bs.UpdateScalingResult(ViewState["FormID"].ToString(), ScalingResult);
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    private void BindSelectUnit()
    {
        string methodName = "BindSelectUnit";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            if (nodeName.Value != "招标委员会意见" && nodeName.Value != "招标委员会主任意见" && nodeName.Value != "执行主任")
            {
                tdSelectUnit.Visible = false;
            }
            
            string BidCommittee = bs.GetBidScaling(ViewState["FormID"].ToString()).BidCommittee;
            foreach (string UserCode in BidCommittee.Split(','))
            {
                if (UserCode != null && !string.IsNullOrEmpty(UserCode))
                { 
                    JC_BidScalingListInfo obj = bsl.GetBidScalingList(ViewState["FormID"].ToString(),UserCode);
                    if (obj != null)
                    {
                        if (obj.SelectResult == "0")
                        {
                            lblFirstList.Text = lblFirstList.Text + obj.UserName + "&nbsp;&nbsp;&nbsp;&nbsp;" + obj.CreatTime + "<br/>";
                        }
                        else if (obj.SelectResult == "1")
                        {
                            lblSecondList.Text = lblSecondList.Text + obj.UserName + "&nbsp;&nbsp;&nbsp;&nbsp;" +obj.CreatTime + "<br/>";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    public void ChangeResultToUnAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "0");
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
    }

    void Alert(Page page, object message)
    {
        StringBuilder sb = new StringBuilder();
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message) + sb.ToString();
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    public string GetViewUrl()
    {
        string AUrl = Request.Url.ToString();
        string ViewUrl = "";
        string StrTemp = AUrl.Substring(0, AUrl.IndexOf("ApprovePage"));
        ViewUrl = StrTemp + "ViewPage/V_JC_BidScaling.aspx?ID=" + Request.QueryString["id"];
        return ViewUrl;
    }
}