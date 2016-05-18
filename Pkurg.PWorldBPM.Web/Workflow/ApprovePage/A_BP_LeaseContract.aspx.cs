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

public partial class Workflow_ApprovePage_A_BP_LeaseContract : UPageBase
{
    public string className = "Workflow_ApprovePage_A_BP_LeaseContract";
    //控制founder\\zybpmadmin和founder\\zybpm的配置问题
    string DefaultApprover = System.Configuration.ConfigurationManager.AppSettings["DefaultApprover"];

    ContractClass ContractClass = new ContractClass();
    BP_LeaseContract lc = new BP_LeaseContract();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();

    protected void Page_Load(object sender, EventArgs e)
    {
        //防止二次提交
        ScriptManager.RegisterOnSubmitStatement(Page, typeof(Page), "Go_disabled", "disabledButton('lbAgree');disabledButton('lbReject');disabledButton('lbSubmit');");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Clear_disabled", "enableButton('lbAgree');enableButton('lbReject');enableButton('lbSubmit');", true);


        string methodName = "Page_Load";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
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
                //修改合同编号 2015-08-05 by yanghechun
                trContract_No.Visible = nodeName.Value == "经办人确认生效";
            }
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                ViewState["FormID"] = _BPMContext.ProcInst.FormId;
                BindFormData();
                hfViewUrl.Value = GetViewUrl();//获取查看页面
                if (nodeName.Value.Contains("集团"))
                {
                    tbCompany.Visible = false;
                    tbGroup.Visible = true;
                }
                else
                {
                    tbCompany.Visible = true;
                    tbGroup.Visible = false;
                }
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
    }

    private void BindFormData()
    {
        string methodName = "BindFormData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            BP_LeaseContractInfo obj = lc.GetLeaseContract(ViewState["FormID"].ToString());
            if (obj != null)
            {
                cblSecurityLevel.SelectedValue = obj.SecurityLevel != null ? obj.SecurityLevel.ToString() : "-1";
                cblUrgentLevel.SelectedValue = obj.UrgenLevel != null ? obj.UrgenLevel.ToString() : "-1";
                tbData.Text = ((DateTime)obj.Date).ToString("yyyy-MM-dd");
                tbPerson.Text = obj.UserName;
                tbDepartName.Text = obj.DeptName;
                tbPhone.Text = obj.Mobile;
                tbTitle.Text = obj.ReportTitle;
                tbContent.Text = obj.Url;
                tbReportCode.Text = obj.ReportCode;
                tbReason.Text = obj.Reason;
                tbRemark.Text = obj.Remark;
                cblDecorationContract.SelectedValue = obj.DecorationContract != null ? obj.DecorationContract.ToString() : "-1";
                cblServiceContract.SelectedValue = obj.ServiceContract != null ? obj.ServiceContract.ToString() : "-1";
                cblCompensationContract.SelectedValue = obj.CompensationContract != null ? obj.CompensationContract.ToString() : "-1";
                cblModificationContract.SelectedValue = obj.ModificationContract != null ? obj.ModificationContract.ToString() : "-1";
                cblSupplementContract.SelectedValue = obj.SupplementContract != null ? obj.SupplementContract.ToString() : "-1";
                cblLesseeContract.SelectedValue = obj.LesseeContract != null ? obj.LesseeContract.ToString() : "-1";
                
                hf_biz_type.Value = obj.BizType.ToString();
                hf_biz_id.Value = obj.BizID.ToString();
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
        options.Add(ApproveOpinionUC3);
        options.Add(ApproveOpinionUC4);
        options.Add(ApproveOpinionUC5);
        options.Add(ApproveOpinionUC6);
        options.Add(ApproveOpinionUC7);
        options.Add(ApproveOpinionUC8);
        options.Add(ApproveOpinionUC9);
        options.Add(ApproveOpinionUC10);
        options.Add(ApproveOpinionUC11);
        options.Add(ApproveOpinionUC12);
        options.Add(ApproveOpinionUC13);
        options.Add(ApproveOpinionUC14);
        options.Add(ApproveOpinionUC15);
        options.Add(ApproveOpinionUC16);
        options.Add(ApproveOpinionUC17);
        options.Add(ApproveOpinionUC18);
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

        if (workFlowInstance != null)
        {
            //修改合同编号 2015-08-05 by yanghechun
            //2015-8-5 经办人确认生效
            //更新合同编号
            if (nodeName.Value == "经办人确认生效")
            {
                if (txtContract_No.Text.Trim() == "")
                {
                    Alert(Page, "合同编号不能为空,请输入！");
                    txtContract_No.Focus();
                    return;
                }
                try
                {
                    ContractClass contractClass = new ContractClass();
                    int biz_type = Convert.ToInt16(hf_biz_type.Value);
                    int biz_id = Convert.ToInt16(hf_biz_id.Value);
                    contractClass.UpdateContractNo(biz_type, biz_id, txtContract_No.Text.Trim());
                }
                catch
                {
                }
            }

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


    //拒绝
    protected void Reject_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);
        if (workFlowInstance != null)
        {
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

    //取消
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(ViewState["BackUrl"].ToString(), false);
    }

    public string GetUrl()
    {
        return tbContent.Text.ToString();
    }

    public string GetViewUrl()
    {
        string AUrl = Request.Url.ToString();
        string ViewUrl = "";
        string StrTemp = AUrl.Substring(0, AUrl.IndexOf("ApprovePage"));
        ViewUrl = StrTemp + "ViewPage/V_BP_LeaseContract.aspx?ID=" + Request.QueryString["id"];
        return ViewUrl;
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
}