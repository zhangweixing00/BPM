using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;
using SourceCode.Workflow.Client;

public partial class Workflow_ApprovePage_A_HR_CadresOrRemoval : UPageBase
{
    CadresOrRemoval Aitems = new CadresOrRemoval();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();
    string GroupCode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string HRDeptCode = System.Configuration.ConfigurationManager.AppSettings["HRDeptCode"];
    string DefaultApprover = System.Configuration.ConfigurationManager.AppSettings["DefaultApprover"];
    public string FormID
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

    protected void Page_Load(object sender, EventArgs e)
    {
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
            InitLeader();
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

    private void InitLeader()
    {
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(lblApprovers.Text);
        if (nodeName.Value == "人力意见" || nodeName.Value == "相关部门意见"||nodeName.Value.Contains("董事意见"))
        {
            lbChairman.Text = InitLabel(xmldoc, "//Chairman");

            if (nodeName.Value == "人力意见" || nodeName.Value == "相关部门意见")
            {
                lbDirector2.Text = InitLabel(xmldoc, "//Director1");
                lbDirector2.Text += InitLabel(xmldoc, "//Director2");
                lbDirector2.Text += InitLabel(xmldoc, "//Director3");
                lbDirector2.Text += InitLabel(xmldoc, "//Director4");
                if (nodeName.Value.Contains("人力意见"))
                {
                    lbDeptManager.Text = InitLabel(xmldoc, "//DeptManager");
                }
            }
        }
    }
    private string InitLabel(XmlDocument xmldoc, string NodeTemp) 
    {
        string strLabel="";
        if (xmldoc.SelectSingleNode(NodeTemp) != null)
        {
            strLabel = "(" + xmldoc.SelectSingleNode(NodeTemp).Attributes["Name"].Value + ")审批";
        }
        return strLabel;
    }


    private void BindFormData()
    {
        try
        {
            CadresOrRemovalInfo obj = Aitems.Get(FormID);
            if (obj != null)
            {
                InitApproveList2(obj.IsGroup);
                tbReportCode.Text = obj.FormID;
                tbCadresName.Text = obj.CadresName;
                tbLocationCompanyDeptJob.Text = obj.LocationCompanyDeptJob;
                tbCadresCompanyDeptJob.Text = obj.CadresCompanyDeptJob;
                tbCadresContent.Text = obj.CadresContent;
                tbRemovalName.Text = obj.RemovalName;
                tbLocationCompanyDeptJobR.Text = obj.LocationCompanyDeptJobR;
                tbRemovalCompanyDeptjob.Text = obj.RemovalCompanyDeptjob;
                tbRemovalContent.Text = obj.RemovalContent;
                lblApprovers.Text = obj.LeadersSelected;

                if (!string.IsNullOrEmpty(obj.chkCadresOrRemoval))
                {
                    tbCadre.Visible = obj.chkCadresOrRemoval != "1" ? true : false;
                    tbRemoval.Visible = obj.chkCadresOrRemoval != "0" ? true : false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //批准
    protected void Agree_Click(object sender, EventArgs e)
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormID);
        bool IsSubmit = true;

        if(IsSubmit)
        {
            if (workFlowInstance != null)
            {
                UploadAttachments1.SaveAttachment(FormID);
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
                       FormID = FormID,
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
        
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormID);
        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(FormID);
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
                   FormID = FormID,
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
                    UpdateByUserName = CurrentEmployee.EmployeeName
                };
                bfApproval.AddApprovalRecord(appRecord);
                ViewState["IsSubmit"] = true;
                wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);

                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
            }
        }
    }

    //提交
    protected void Submit_Click(object sender, EventArgs e)
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormID);

        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(FormID);
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
                   FormID = FormID,
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

                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                }
            }
        }
    }

    private void SaveData(string ID, string wfStatus)
    {
        CadresOrRemovalInfo obj = null;
        try
        {
            obj = Aitems.Get(ID);
            obj.FormID = ViewState["FormID"].ToString();

            Aitems.Update(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
        options.Add(OpinionDeptManager);
        options.Add(OpinionHRDeptManager);
        options.Add(OpinionDirector1);
        options.Add(OpinionDirector2);
        options.Add(OpinionDirector3);
        options.Add(OpinionDirector4);
        options.Add(OpinionChairman);
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
                lbDirector.Text = "分管领导意见：";
                lbPresident.Text = "CEO意见：";
                lbTitle.Text = "";
            }
        }
    }
}