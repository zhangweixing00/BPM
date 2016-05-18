using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ.OA;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Entites.BIZ.OA;
using SourceCode.Workflow.Client;

public partial class Workflow_ApprovePage_A_OA_SystemDispatch : UPageBase
{
    string DefaultApprover = System.Configuration.ConfigurationManager.AppSettings["DefaultApprover"];

    public string ContractID = null;
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();

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
                sn.Value = Request.QueryString["sn"];

                WorklistItem listItem = WorkflowHelper.GetWorklistItemWithSN(sn.Value, "founder\\" + _BPMContext.CurrentUser.LoginId);
                taskID.Value = listItem.ID.ToString();
                nodeID.Value = listItem.ActivityInstanceDestination.ActID.ToString();
                nodeName.Value = listItem.ActivityInstanceDestination.Name;
                InitApproveOpinion(nodeName.Value);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                FormId = _BPMContext.ProcInst.FormId;
                InitFormData();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }
        ShowButton();
        SetMenu();
    }
    private void SetMenu()
    {
        if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name == "会签")
        {
            UnOptions.Visible = false;
            lbAgree.Text = "提交";
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
            bool isAddSign = new Workflow_Common().IsAddSign(Request.QueryString["sn"], UploadAttachments1._BPMContext.CurrentUser.LoginId);
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

    /// <summary>
    /// 加载表单
    /// </summary>
    private void InitFormData()
    {
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                SystemDispatchInfo info = SystemDispatch.GetSystemDispatchInfo(FormId);
                cblSecurityLevel.SelectedIndex = Convert.ToInt32(info.SecurityLevel);
                cblUrgenLevel.SelectedIndex = Convert.ToInt32(info.UrgenLevel);
                tbDepartName.Text = info.DeptName;
                tbDateTime.Text = info.DateTime;
                tbDateTime.Text = info.DateTime;
                tbUserName.Text = info.UserName;
                tbMobile.Text = info.Mobile;
                tbTitle.Text = info.Title;
                tbContent.Text = info.Content;
                cblRedHeadDocument.SelectedIndex = int.Parse(info.RedHeadDocument);
                cblIsPublish.SelectedIndex = int.Parse(info.IsPublish);
                cblSecurityLevel.SelectedIndex = int.Parse(info.SecurityLevel);
                cblUrgenLevel.SelectedIndex = int.Parse(info.UrgenLevel);
                tbReportCode.Text = info.FormId;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
        options.Add(OpinionDeptleader);
        options.Add(OpinionRealateDept);
        options.Add(OpinionTopLeaders);
        options.Add(OpinionCEO);
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

    //批准
    protected void Agree_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);

        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(id);
            string action = "同意";
            string optionDefault  = "同意";

            bool isSuccess = WorkflowHelper.ApproveProcess(sn.Value, action, "founder\\" + _BPMContext.CurrentUser.LoginId);
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
    //拒绝
    protected void Reject_Click(object sender, EventArgs e)
    {

        string id = ViewState["FormID"].ToString();

        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);
        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(id);

            string action = "不同意";
            //ChangeResultToUnAgree();
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
                ViewState["IsSubmit"] = true;
                bfApproval.AddApprovalRecord(appRecord);
                wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
        }

        //WorkflowHelper.ApproveProcess
        //insert data to business object
    }
    //提交
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
    /// 无流程审核人不执行
    /// </summary>
    private void UpdateWFParams()
    {
        if (K2_TaskItem.ActivityInstanceDestination.Name == "流程审核员审核")
        {
            NameValueCollection dataFields = new NameValueCollection();

            StringBuilder leaders = new StringBuilder();
            StringBuilder Viceleaders = new StringBuilder();
            StringBuilder deptsofGroup = new StringBuilder();

            StringBuilder leaderofgroup = new StringBuilder();
            StringBuilder AssistantPresident = new StringBuilder();
            StringBuilder VicePresident = new StringBuilder();

            foreach (var item in Countersign1.Result.Split(','))
            {
                string leadersTmp = GetRoleUsers(item, "主管助理总裁");
                if (!leaders.ToString().Contains(leadersTmp))
                {
                    leaders.AppendFormat("{0},", leadersTmp);
                }
                string ViceleadersTmp = GetRoleUsers(item, "主管副总裁");
                if (!Viceleaders.ToString().Contains(ViceleadersTmp))
                {
                    Viceleaders.AppendFormat("{0},", ViceleadersTmp);
                }
                string deptsofGroupTmp = GetRoleDepts(item, "集团主管部门");
                if (!deptsofGroup.ToString().Contains(deptsofGroupTmp))
                {
                    deptsofGroup.AppendFormat("{0},", deptsofGroupTmp);
                }
            }

            dataFields.Add("leaders", FilterDataField(leaders));
            dataFields.Add("Viceleaders", FilterDataField(Viceleaders));

            foreach (var item in deptsofGroup.ToString().Trim(',').Split(','))
            {
                string leaderofgroupTmp = GetRoleUsers(item, "部门负责人");
                if (!leaderofgroup.ToString().Contains(leaderofgroupTmp))
                {
                    leaderofgroup.AppendFormat("{0},", leaderofgroupTmp);
                }
                string AssistantPresidentTmp = GetRoleUsers(item, "主管助理总裁");
                if (!AssistantPresident.ToString().Contains(AssistantPresidentTmp))
                {
                    AssistantPresident.AppendFormat("{0},", AssistantPresidentTmp);
                }
                string VicePresidentTmp = GetRoleUsers(item, "主管副总裁");
                if (!VicePresident.ToString().Contains(VicePresidentTmp))
                {
                    VicePresident.AppendFormat("{0},", VicePresidentTmp);
                }

            }

            dataFields.Add("leadersofgroup", FilterDataField(leaderofgroup));
            dataFields.Add("AssistantPresident", FilterDataField(AssistantPresident));
            dataFields.Add("VicePresident", FilterDataField(VicePresident));

            WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
        }
    }

    public void ChangeResultToUnAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "0");
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
    }

    private string FilterDataField(string dataField_old)
    {
        string dataField = dataField_old.Trim(',');
        if (string.IsNullOrEmpty(dataField))
        {
            dataField = "noapprovers";
        }
        return dataField;
    }

    private string FilterDataField(StringBuilder dataField_old)
    {
        return FilterDataField(dataField_old.ToString().Trim(','));
    }

    private string GetRoleDepts(string item, string role)
    {
        StringBuilder dataInfos = new StringBuilder();
        BFCountersignRoleDepartment counterSignHelper = new BFCountersignRoleDepartment();
        DataTable dtDept = counterSignHelper.GetSelectCountersignDepartment(item, role);
        if (dtDept != null && dtDept.Rows.Count != 0)
        {
            foreach (DataRow rowItem in dtDept.Rows)
            {
                dataInfos.AppendFormat("{0},", rowItem["DepartCode"].ToString());
            }
        }
        return dataInfos.ToString().Trim(',');
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["BackUrl"].ToString(), false);
    }

    void Alert(Page page, object message)
    {
        StringBuilder sb = new StringBuilder();
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message) + sb.ToString();
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }
}
