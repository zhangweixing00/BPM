using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;
using SourceCode.Workflow.Client;

public partial class Workflow_ApprovePage_A_ERP_ContractApproval
    : UPageBase
{

    string DefaultApprover = System.Configuration.ConfigurationManager.AppSettings["DefaultApprover"];

    /// <summary>
    /// 加载表单
    /// </summary>
    private void InitFormData()
    {
        try
        {
            ContractApprovalInfo info = ContractApproval.GetModel(FormId);
            if (info != null)
            {
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(info.StartDeptId);
                ddlDepartName.Items.Insert(0, deptInfo.Remark);
                ddlDepartName.SelectedIndex = 0;

                cblisoverCotract.Checked = info.IsOverContract.Value == 1;
                cbIsReportResource.Checked = info.IsReportToResource.Value == 1;
                cbIsReportFounder.Checked = info.IsReportToFounder.Value == 1;
                //StartDeptId = info.StartDeptId;
            }
            ///加载业务数据
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

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
            WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitStartDeptment();
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

            ///审批界面显示
            if (!string.IsNullOrEmpty(Request.QueryString["sn"]))
            {
                sn.Value = Request.QueryString["sn"];

                WorklistItem listItem = WorkflowHelper.GetWorklistItemWithSN(sn.Value, "founder\\" + _BPMContext.CurrentUser.LoginId);
                taskID.Value = listItem.ID.ToString();
                nodeID.Value = listItem.ActivityInstanceDestination.ActID.ToString();
                nodeName.Value = listItem.ActivityInstanceDestination.Name;
                InitApproveOpinion(nodeName.Value);
            }
            else
            {
                ExceptionHander.GoToErrorPage("该审批环节已结束", null);
            }

        }

        ShowButton();

        SetMenu();
    }

    private void InitStartDeptment()
    {
        if (!IsPostBack)
        {
            Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(_BPMContext.CurrentUser.MainDeptId);
            BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
            Pkurg.PWorld.Entities.TList<Department> deptInfo2 = bfurd.GetDeptListByEmployeeCodeAndRoleName(_BPMContext.CurrentPWordUser.EmployeeCode, "部门成员");

            foreach (Department DeptItem in deptInfo2)
            {
                ddlDepartName.Items.Add(new ListItem()
                {
                    Text = DeptItem.Remark,
                    Value = DeptItem.DepartCode
                });
            }
            if (deptInfo2.Count == 0)
            {
                ddlDepartName.Items.Add(new ListItem()
                {
                    Text = deptInfo.Remark,
                    Value = deptInfo.DepartCode
                });
            }
        }
    }

    public string FormTitle
    {
        get
        {
            return ViewState["FormTitle"].ToString();
        }
        set
        {
            ViewState["FormTitle"] = value;
        }
    }

    #region 页面基础设置
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

    /// <summary>
    /// 如果涉及到集团会签，必须设置节点名称为集团会签
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
    /// 按钮设置
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
            bool isAddSign = new Workflow_Common().IsAddSign(Request.QueryString["sn"], uploadAttachments._BPMContext.CurrentUser.LoginId);
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

    #endregion

    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();

    #region 意见框处理

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
        options.Add(Option_1326);
        options.Add(Option_1327);
        options.Add(Option_1328);
        options.Add(Option_1329);
        options.Add(Option_1330);
        options.Add(Option_1331);
        options.Add(Option_1332);
        options.Add(Option_1333);
        options.Add(Option_1334);
        options.Add(Option_1335);
        options.Add(Option_1343);
        options.Add(Option_1336);
        options.Add(Option_1337);
        options.Add(Option_1338);
        options.Add(Option_1339);
        options.Add(Option_1340);
        options.Add(Option_1341);

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

    #endregion

    /// <summary>
    /// 更改结果
    /// </summary>
    public void ChangeResultToUnAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "0");
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);

    }

    //批准
    protected void Agree_Click(object sender, EventArgs e)
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);

        if (workFlowInstance != null)
        {
            uploadAttachments.SaveAttachment(FormId);

            UpdateWFParams();
            string action = "同意";
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

                if (bfApproval.AddApprovalRecord(appRecord))
                {
                    wf_WorkFlowInstance.UpdateNowStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                }
            }
        }
    }
    //拒绝
    protected void Reject_Click(object sender, EventArgs e)
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
        if (workFlowInstance != null)
        {
            uploadAttachments.SaveAttachment(FormId);
            UpdateWFParams();
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
            uploadAttachments.SaveAttachment(id);

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
                wf_WorkFlowInstance.UpdateNowStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
        }
    }


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
            //Response.Redirect("/Workflow/ToDoList.aspx");
            //Server.Transfer("~");
        }
    }
}
