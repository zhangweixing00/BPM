using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;
using SourceCode.Workflow.Client;

public partial class Workflow_ApprovePage_A_OA_SealOfBC : UPageBase
{
    SealOfBC Aitems = new SealOfBC();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();
    string GroupCode = System.Configuration.ConfigurationManager.AppSettings["GroupCode"];
    string GroupOfficeCode = System.Configuration.ConfigurationManager.AppSettings["GroupGMDCode"];
    string BCCode = System.Configuration.ConfigurationManager.AppSettings["BCCode"];
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
    public string StartDeptId
    {
        get
        {
            return lbDeptCode.Text;
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
            //InitLeader();
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
        if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name == "会签")
        {
            UnOptions.Visible = false;
            lbAgree.Text = "提交";
        }
        if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name == "公章管理员盖章")
        {
            UnOptions.Visible = false;
            lbAgree.Text = "盖章";
        }
    }

    private void InitLeader()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(StartDeptId, "部门负责人");
        DataTable dtDirector = bfurd.GetSelectRoleUser(StartDeptId, "主管副总裁");
        DataTable dtGeneralManager = bfurd.GetSelectRoleUser(BCCode, "总裁");
        DataTable dtPresident = bfurd.GetSelectRoleUser(GroupCode, "总裁");
        DataTable dtGroupOffice = bfurd.GetSelectRoleUser(GroupOfficeCode, "部门负责人");
        DataTable dtSealManager = bfurd.GetSelectRoleUser(BCCode, "公章管理员");

        if (dtDeptManager.Rows.Count != 0)
        {
            lbDeptManager.Text = "(" + dtDeptManager.Rows[0]["EmployeeName"].ToString() + ")审核";
        }
        if (dtDirector.Rows.Count != 0)
        {
            lbDirector.Text = "(" + dtDirector.Rows[0]["EmployeeName"].ToString() + ")审核";
        }
        if (dtGeneralManager.Rows.Count != 0)
        {
            lbGeneralManager.Text = "(" + dtGeneralManager.Rows[0]["EmployeeName"].ToString() + ")审核";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbPresident.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtGroupOffice.Rows.Count != 0)
        {
            lbGroupOffice.Text = "(" + dtGroupOffice.Rows[0]["EmployeeName"].ToString() + ")复核";
        }
        if (dtSealManager.Rows.Count != 0)
        {
            lbSealManager.Text = "(" + dtSealManager.Rows[0]["EmployeeName"].ToString() + ")盖章";
        }
    }

    private void BindFormData()
    {
        try
        {
            SealOfBCInfo obj = Aitems.Get(FormID);
            if (obj != null)
            {
                tbReportCode.Text = obj.FormID;
                tbDepartName.Text = obj.DeptName;
                lbDeptCode.Text = obj.DeptCode;
                tbDateTime.Text = obj.DateTime;
                tbUserName.Text = obj.UserName;
                tbMobile.Text = obj.Mobile;
                tbTitle.Text = obj.Title;
                tbContent.Text = obj.Content;
                cblSecurityLevel.SelectedValue = obj.SecurityLevel != null ? obj.SecurityLevel.ToString() : "-1";
                cblUrgenLevel.SelectedValue = obj.UrgenLevel != null ? obj.UrgenLevel.ToString() : "-1";
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
        SealOfBCInfo obj = null;
        try
        {
            obj = Aitems.Get(ID);
            obj.FormID = ViewState["FormID"].ToString();
            obj.LeadersSelected = lblApprovers.Text;
            obj.Content = tbContent.Text;

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
        options.Add(OpinionDirector);
        options.Add(OpinionGeneralManager);
        options.Add(OpinionPresident);
        options.Add(OpinionSealManager);
        options.Add(OpinionGroupOffice);
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

    private static string GetRoleUsers(string dept, string roleName)
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        StringBuilder dataInfos = new StringBuilder();
        DataTable dtDept = bfurd.GetSelectRoleUser(dept, roleName);
        if (dtDept != null && dtDept.Rows.Count != 0)
        {
            foreach (DataRow rowItem in dtDept.Rows)
            {
                dataInfos.AppendFormat("K2:Founder\\{0},", rowItem["LoginName"].ToString());
            }
        }
        return dataInfos.ToString().Trim(',');
    }
}