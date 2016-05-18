using System;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Manage;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;
using SourceCode.Workflow.Client;

public partial class Workflow_EditPage_E_InstructionApprove : System.Web.UI.Page
{
    public string className = "Workflow_EditPage_E_InstructionOfPKURG";
    public string ContractID = null;
    //public Employee CurrentEmployee = new Employee();
    public WF_InstructionOfPKURG wf_Instruction = new WF_InstructionOfPKURG();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    BFApprovalRecord bfApproval = new BFApprovalRecord();

    Employee currentEmployee = new Employee();
    public Employee CurrentEmployee
    {
        get
        {
            return Session["CurrentEmployee"] as Employee;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        string methodName = "Page_Load";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        if (!IsPostBack)
        {
            string currentUser = new IdentityUser().GetEmployee().LoginId;
            UploadAttachments1._BPMContext.LoginId = currentUser;

            ViewState["loginName"] = currentUser.ToLower().Replace(@"k2:founder\", "").Replace(@"founder\", "");
            WorkflowHelper.CurrentUser = ViewState["loginName"].ToString();
            BFEmployee bfEmployee = new BFEmployee();
            EmployeeAdditional employeeaddInfo = bfEmployee.GetEmployeeAdditionalByLoginName(ViewState["loginName"].ToString());

            currentEmployee = bfEmployee.GetEmployeeByEmployeeCode(employeeaddInfo.EmployeeCode);
            Session["CurrentEmployee"] = currentEmployee;

            if (!string.IsNullOrEmpty(Request.QueryString["sn"]))
            {
                sn.Value = Request.QueryString["sn"];
                WorklistItem listItem = WorkflowHelper.GetWorklistItemWithSN(sn.Value);
                taskID.Value = listItem.ID.ToString();
                nodeID.Value = listItem.ActivityInstanceDestination.ActID.ToString();
                nodeName.Value = listItem.ActivityInstanceDestination.Name;
                InitApproveOpinion(nodeName.Value);

            }



            if (!string.IsNullOrEmpty(Request.QueryString["FormID"]))
            {
                ViewState["FormID"] = Request.QueryString["FormID"];
                InintData();
            }
            else
            {
                ContractID = BPMHelp.GetSerialNumber("SQ_"); ;
                tbNumber.Text = ContractID;
                ViewState["FormID"] = ContractID;
            }
            //tbPerson.Text = CurrentEmployee.EmployeeName;


        }

        Countersign1.SimulateUser = ViewState["loginName"].ToString();
        FlowRelated1.SimulateUser = ViewState["loginName"].ToString();
        Options.Visible = Countersign1.K2_TaskItem != null;
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }
    private void InintData()
    {
        string methodName = "InintData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            InstructionOfPkurg instructionOfPkurg = wf_Instruction.GetInstructionOfPkurgById(ViewState["FormID"].ToString());
            if (instructionOfPkurg != null)
            {
                cblSecurityLevel.SelectedValue = instructionOfPkurg.SecurityLevel.ToString();
                cblUrgentLevel.SelectedValue = instructionOfPkurg.UrgenLevel != null ? instructionOfPkurg.UrgenLevel.ToString() : "0";
                tbData.Text = ((DateTime)instructionOfPkurg.Date).ToString("yyyy-MM-dd");
                tbPerson.Text = instructionOfPkurg.UserName;
                tbDepartName.Text = instructionOfPkurg.DeptName;

                tbPhone.Text = instructionOfPkurg.Mobile;
                tbTheme.Text = instructionOfPkurg.ReportTitle;
                tbContent.Text = instructionOfPkurg.ReportContent;
                instructionOfPkurg.ReportTitle = tbTheme.Text;
                tbNumber.Text = instructionOfPkurg.ReportCode;

                WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(instructionOfPkurg.FormId);
                //this.ApproveOpinionUC1.CurrentNode = nodeName.Value;
                //this.ApproveOpinionUC1.InstanceId = workFlowInstance.InstanceId;
                AddSign1.ProcId = workFlowInstance.InstanceId;
                #region 审批意见框
                ApproveOpinionUCDeptleader.InstanceId = workFlowInstance.InstanceId;
                ApproveOpinionUCRealateDept.InstanceId = workFlowInstance.InstanceId;
                ApproveOpinionUCLeader.InstanceId = workFlowInstance.InstanceId;
                ApproveOpinionUCCEO.InstanceId = workFlowInstance.InstanceId;

                if (ApproveOpinionUCDeptleader.CurrentNodeName == nodeName.Value)
                {
                    ApproveOpinionUCDeptleader.CurrentNode = true;
                }

                if (ApproveOpinionUCRealateDept.CurrentNodeName == nodeName.Value)
                {
                    ApproveOpinionUCRealateDept.CurrentNode = true;
                }

                if (ApproveOpinionUCLeader.CurrentNodeName == nodeName.Value)
                {
                    ApproveOpinionUCLeader.CurrentNode = true;
                }

                if (ApproveOpinionUCCEO.CurrentNodeName == nodeName.Value)
                {
                    ApproveOpinionUCCEO.CurrentNode = true;
                }
                #endregion

                //查询已经添加的附件
                //UploadFilesUC1.BindAttachmentListByCode(ViewState["FormID"].ToString());
                UploadAttachments1.ProcId = workFlowInstance.InstanceId;
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

        //nodeName.Value==""
        if (nodeName == this.ApproveOpinionUCDeptleader.CurrentNodeName)
        {
            opinion = ((TextBox)ApproveOpinionUCDeptleader.FindControl("tbDeptLeaderOpion")).Text;
        }
        if (nodeName == this.ApproveOpinionUCRealateDept.CurrentNodeName)
        {
            opinion = ((TextBox)ApproveOpinionUCRealateDept.FindControl("tbDeptLeaderOpion")).Text;
        }
        if (nodeName == this.ApproveOpinionUCLeader.CurrentNodeName)
        {
            opinion = ((TextBox)ApproveOpinionUCLeader.FindControl("tbDeptLeaderOpion")).Text;
        }
        if (nodeName == this.ApproveOpinionUCCEO.CurrentNodeName)
        {
            opinion = ((TextBox)ApproveOpinionUCCEO.FindControl("tbDeptLeaderOpion")).Text;
        }
        return opinion;

    }

    private void InitApproveOpinion(string nodeName)
    {
        TextBox opinion = new TextBox();

        //nodeName.Value==""
        if (nodeName == this.ApproveOpinionUCDeptleader.CurrentNodeName)
        {
            opinion = ((TextBox)ApproveOpinionUCDeptleader.FindControl("tbDeptLeaderOpion"));
        }
        if (nodeName == this.ApproveOpinionUCRealateDept.CurrentNodeName)
        {
            opinion = ((TextBox)ApproveOpinionUCRealateDept.FindControl("tbDeptLeaderOpion"));
        }
        if (nodeName == this.ApproveOpinionUCLeader.CurrentNodeName)
        {
            opinion = ((TextBox)ApproveOpinionUCLeader.FindControl("tbDeptLeaderOpion"));
        }
        if (nodeName == this.ApproveOpinionUCCEO.CurrentNodeName)
        {
            opinion = ((TextBox)ApproveOpinionUCCEO.FindControl("tbDeptLeaderOpion"));
        }
        hf_OpId.Value= opinion.ClientID;

    }

    //批准
    protected void Save_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);

        if (workFlowInstance != null)
        {
            UploadAttachments1.SaveAttachment(id);
            Countersign1.SaveAndSubmit();
            //
            string action = "同意";
            bool isSuccess = WorkflowHelper.ApproveProcess(sn.Value, action);

            string Opinion = GetApproveOpinion(nodeName.Value);
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
                    if (WorkflowHelper.GetProcessInstance(int.Parse(workFlowInstance.WfInstanceId)).Status1 == ProcessInstance.Status.Completed)
                    {
                        wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "2", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), DateTime.Now, CurrentEmployee);

                        if (wf_Instruction.UpdateStatus(id, "04", workFlowInstance.WfInstanceId))
                        {
                            WebCommon.Show(this, Resources.Message.ApplicationReviewSucess);
                            Response.Redirect("~/Workflow/ToDoWorkList.aspx", false);
                        }
                    }
                    else
                    {
                        wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);

                        if (wf_Instruction.UpdateStatus(id, "03", workFlowInstance.WfInstanceId))
                        {
                            WebCommon.Show(this, Resources.Message.ApplicationReviewSucess);
                            Response.Redirect("~/Workflow/ToDoWorkList.aspx", false);
                        }
                    }
                }


            }

        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["BackUrl"].ToString(), false);
    }
    //拒绝
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        string id = ViewState["FormID"].ToString();

        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);
        if (workFlowInstance != null)
        {

            UploadAttachments1.SaveAttachment(id);

            string action = "不同意";
            bool isSuccess = WorkflowHelper.ApproveProcess(sn.Value, action);
            string Opinion = GetApproveOpinion(nodeName.Value);
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
                    UpdateByUserName = CurrentEmployee.EmployeeName

                };

                bfApproval.AddApprovalRecord(appRecord);
                wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);

                if (wf_Instruction.UpdateStatus(id, "01", workFlowInstance.WfInstanceId))
                {
                    WebCommon.Show(this, Resources.Message.ApplicationReviewSucess);
                    Response.Redirect("~/Workflow/ToDoWorkList.aspx", false);
                }
            }



        }

        //WorkflowHelper.ApproveProcess
        //insert data to business object
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(id);

        //AddSign1.BeginAddSign(workFlowInstance.InstanceId, Request["sn"]);
        //if (workFlowInstance != null)
        //{
        //    UploadFilesUC1.SaveAttachment(id, CurrentEmployee);
        //    Countersign1.SaveAndSubmit();

        //    //
        //    WorkflowHelper.ForwardToNextApprover(sn.Value, Request.QueryString["u"], "founder\\zhangweixing");

        //    string Opinion = GetApproveOpinion(nodeName.Value);
        //    string ApproveResult = "同意";
        //    string OpinionType = "";
        //    string IsSign = "1";
        //    string DelegateUserName = "";
        //    string DelegateUserCode = "";

        //    var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
        //    {
        //        ApprovalId = Guid.NewGuid().ToString(),
        //         WFTaskID = int.Parse(taskID.Value),
        //        FormId = id,
        //        InstanceId = workFlowInstance.InstanceId,
        //        Opinion = Opinion,
        //        ApproveAtTime = DateTime.Now,
        //        ApproveByUserCode = CurrentEmployee.EmployeeCode,
        //        ApproveByUserName = CurrentEmployee.EmployeeName,
        //        ApproveResult = ApproveResult,
        //        OpinionType = OpinionType,
        //        CurrentActiveName = nodeName.Value,
        //        IsSign = IsSign,
        //        CurrentActiveID = nodeID.Value,
        //        DelegateUserName = DelegateUserName,
        //        DelegateUserCode = DelegateUserCode,
        //        CreateAtTime = DateTime.Now,
        //        CreateByUserCode = CurrentEmployee.EmployeeCode,
        //        CreateByUserName = CurrentEmployee.EmployeeName,
        //        UpdateAtTime = DateTime.Now,
        //        UpdateByUserCode = CurrentEmployee.EmployeeCode,
        //        UpdateByUserName = CurrentEmployee.EmployeeName,
        //        FinishedTime = DateTime.Now

        //    };

        //    if (bfApproval.AddApprovalRecord(appRecord))
        //    {
        //        wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);

        //        if (wf_Instruction.UpdateStatus(id, "03", workFlowInstance.WfInstanceId))
        //        {
        //            WebCommon.Show(this, Resources.Message.ApplicationReviewSucess);
        //            Response.Redirect("~/Workflow/ToDoWorkList.aspx", false);
        //        }

        //    }
        //}

    }
}
