using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;
using SourceCode.Workflow.Client;

public partial class Workflow_ApprovePage_A_JCElevatorOrder : UPageBase
{
    string DefaultApprover = System.Configuration.ConfigurationManager.AppSettings["DefaultApprover"];

    public string className = "Workflow_ApprovePage_A_JCElevatorOrder";

    Order order = new Order();
    JC_ElevatorOrder jc = new JC_ElevatorOrder();
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
                lblLoginName.Text = _BPMContext.CurrentUser.LoginId;
                WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
                sn.Value = Request.QueryString["sn"];
                WorklistItem listItem = WorkflowHelper.GetWorklistItemWithSN(sn.Value, "founder\\" + _BPMContext.CurrentUser.LoginId);
                taskID.Value = listItem.ID.ToString();
                nodeID.Value = listItem.ActivityInstanceDestination.ActID.ToString();
                nodeName.Value = listItem.ActivityInstanceDestination.Name;
                InitApproveOpinion(nodeName.Value);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                ViewState["FormID"] = _BPMContext.ProcInst.FormId;
                BindFormData();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
            ShowButton();
        }
        //Countersign1.SimulateUser = lblLoginName.Text.ToString();        
    }

    private void ShowButton()
    {
        if (string.IsNullOrEmpty(Request.QueryString["sn"]))
        {
            Options.Visible = false;
            ASOptions.Visible = false;
        }
        else
        {
            bool isAddSign = new Workflow_Common().IsAddSign(Request.QueryString["sn"], lblLoginName.Text.ToString());
            string counterSign = "会签";
            if (counterSign.Equals(nodeName.Value))
            {
                ASOptions.Visible = false;
                Options.Visible = true;
                DisAgreeOption.Visible = false;
                AgreeOption.Visible = true;
                lbAgree.Text = "提交";

                //会签里的加签
                if (isAddSign)
                {
                    Options.Visible = false;
                    ASOptions.Visible = true;
                }
            }
            else if (isAddSign)
            {
                Options.Visible = false;
                ASOptions.Visible = true;
            }
            else
            {
                Options.Visible = true;
                ASOptions.Visible = false;
            }

            //研发设计和项目运营只能同意
            //2014-12-23 yanghechun
            if (nodeName.Value == "研发设计部负责人" || nodeName.Value == "项目运营部负责人")
            {
                lbAgree.Text = "提交";
                AgreeOption.Visible = true;
                DisAgreeOption.Visible = false;
            }
        }
    }

    private void BindFormData()
    {
        string methodName = "BindFormData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            JC_ElevatorOrderInfo jcInfo = jc.GetElevatorOrder(ViewState["FormID"].ToString());
            if (jcInfo != null)
            {
                cblSecurityLevel.SelectedValue = jcInfo.SecurityLevel.ToString();
                cblUrgentLevel.SelectedValue = jcInfo.UrgenLevel != null ? jcInfo.UrgenLevel.ToString() : "0";
                tbData.Text = ((DateTime)jcInfo.Date).ToString("yyyy-MM-dd");
                tbPerson.Text = jcInfo.UserName;
                tbDepartName.Text = jcInfo.DeptName;
                tbPhone.Text = jcInfo.Mobile;
                tbTitle.Text = jcInfo.ReportTitle;
                tbOrderType.Text = jcInfo.OrderType.ToString(); ;
                tbOrderID.Text = jcInfo.OrderID.ToString();
                tbContent.Text = jcInfo.Url;
                tbNumber.Text = jcInfo.ReportCode;
                txtMaxCost.Text = jcInfo.MaxCost.HasValue ? FormatMoney(jcInfo.MaxCost.Value.ToString()) : "";
                tbNote.Text = jcInfo.Note;
                //add 2014-12-23
                Countersign1.CounterSignDeptId = jcInfo.StartDeptCode;
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
        options.Add(DeptManagerApproveOpinion);
        options.Add(RealateDeptApproveOpinion);
        options.Add(CityCompanyLeaderApproveOpinion);
        options.Add(JCFirstApprovalApproveOpinion);
        options.Add(DesignerApproveOpinion);
        options.Add(ProjectOperatorApproveOpinion);
        options.Add(JCReApprovalApproveOpinion);
        options.Add(PurchaserApproveOpinion);
        options.Add(COOApproveOpinion);
        options.Add(JCMakeOrderApproveOpinion);
        options.Add(JCFinalApprovalApproveOpinion);
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
        //获取当前工作流
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);

        if (workFlowInstance != null)
        {
            //Countersign1.SaveAndSubmit();
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
            //Countersign1.SaveAndSubmit();
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

    public string GetJCUrl()
    {
        return tbContent.Text.ToString();
    }

    public void ChangeResultToUnAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "0");
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
    }
    /// <summary>
    /// 格式化:给数字字符串加上千位逗号
    /// </summary>
    /// <param name="num">数字</param>
    /// <returns>正则匹配,支持小数</returns>
    string FormatMoney(string num)
    {
        string str = "";

        if (!string.IsNullOrEmpty(num) && Convert.ToDecimal(num) < 0)
        {
            str = "-";
        }

        string newStr = string.Empty;
        Regex reg = new Regex(@"(\d+?)(\d{3})*(\.\d+|$)");
        Match m = reg.Match(num);
        newStr += m.Groups[1].Value;
        for (int i = 0; i < m.Groups[2].Captures.Count; i++)
        {
            newStr += "," + m.Groups[2].Captures[i].Value;
        }
        newStr += m.Groups[3].Value;
        return str + newStr;
    }
}