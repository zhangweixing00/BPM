using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Entites.BIZ.ERP;
using SourceCode.Workflow.Client;

public partial class Workflow_ApprovePage_A_ERP_PaymentApplication : UPageBase
{
    public string ContractID = null;

    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();

    string DefaultApprover = System.Configuration.ConfigurationManager.AppSettings["DefaultApprover"];

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            //string currentUser = new IdentityUser().GetEmployee().LoginId;
            //UploadAttachments1._BPMContext.LoginId = currentUser;
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

        //Countersign1.SimulateUser = ViewState["loginName"].ToString();
        //FlowRelated1.SimulateUser = ViewState["loginName"].ToString();

        ShowButton();

        SetMenu();
    }

    private void SetMenu()
    {
        if (K2_TaskItem!=null&&K2_TaskItem.ActivityInstanceDestination.Name=="集团相关部门意见")
        {
            UnOptions.Visible = false;
            lbAgree.Text = "提交";
        }
    }

    //取消调用该函数20151016
    private void LoadRelationPerson(string startDeptId)
    {
        //StringBuilder showUsers = new StringBuilder();
        //BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();

        //Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(startDeptId);
        //string companyCode = deptInfo.DepartCode.Substring(0, deptInfo.DepartCode.LastIndexOf('-'));
        //DataTable dtDept = bfurd.GetSelectRoleUser(companyCode, "付款申请工程师");
        //if (dtDept != null && dtDept.Rows.Count != 0)
        //{
        //    cbRelatonUsers.Items.Clear();
        //    foreach (DataRow rowItem in dtDept.Rows)//EmployeeCode
        //    {
        //        //showUsers.AppendFormat("{0},", rowItem["EmployeeName"].ToString());

        //        cbRelatonUsers.Items.Add(new ListItem()
        //        {
        //            Text = rowItem["EmployeeName"].ToString(),
        //            Value = rowItem["LoginName"].ToString()
        //        });
        //    }
        //    return;
        //}
        //else
        //{
        //    showUsers.Append("没有配置相关角色");
        //}

        //cbPayer.Text = showUsers.ToString();
    }
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
    //private void InintData()
    //{
    //    string methodName = "InintData";
    //    Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
    //    try
    //    {
    //        InitFormData();
    //        if (instructionOfPkurg != null)
    //        {
    //            tbDepartName.Text = instructionOfPkurg.DeptName;
    //            WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(instructionOfPkurg.FormId);
    //            AddSign1.ProcId = workFlowInstance.InstanceId;
    //            #region 审批意见框
    //            ApproveOpinionUCDeptleader.InstanceId = workFlowInstance.InstanceId;
    //            ApproveOpinionUCRealateDept.InstanceId = workFlowInstance.InstanceId;
    //            ApproveOpinionUCLeader.InstanceId = workFlowInstance.InstanceId;
    //            ApproveOpinionUCCEO.InstanceId = workFlowInstance.InstanceId;

    //            if (ApproveOpinionUCDeptleader.CurrentNodeName == nodeName.Value)
    //            {
    //                ApproveOpinionUCDeptleader.CurrentNode = true;
    //            }

    //            if (ApproveOpinionUCRealateDept.CurrentNodeName == nodeName.Value)
    //            {
    //                ApproveOpinionUCRealateDept.CurrentNode = true;
    //            }

    //            if (ApproveOpinionUCLeader.CurrentNodeName == nodeName.Value)
    //            {
    //                ApproveOpinionUCLeader.CurrentNode = true;
    //            }

    //            if (ApproveOpinionUCCEO.CurrentNodeName == nodeName.Value)
    //            {
    //                ApproveOpinionUCCEO.CurrentNode = true;
    //            }
    //            #endregion

    //            //查询已经添加的附件
    //            //UploadFilesUC1.BindAttachmentListByCode(ViewState["FormID"].ToString());
    //            UploadAttachments1.ProcId = workFlowInstance.InstanceId;
    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
    //        throw ex;
    //    }
    //    Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    //}

    /// <summary>
    /// 加载表单
    /// </summary>
    private void InitFormData()
    {
        try
        {
            PaymentApplicationInfo formDataInfo = PaymentApplication.GetPaymentApplicationInfo(FormId);
            if (formDataInfo.IsOverContract == 1)
            {
                cblisoverCotract.Checked = true;
            }
            else
            {
                cblisoverCotract.Checked = false;
            }
            Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(formDataInfo.StartDeptId);
            tbDepartName.Text = deptInfo.Remark;
            cbChairman.Checked = formDataInfo.IsCheckedChairman == 1;

            if (formDataInfo.StartDeptId.Contains("S972"))
            {
                lbPresident.Text = "总经理意见：";
            }
            else
            {
                lbPresident.Text = "总裁意见：";
            }
            //LoadRelationPerson(formDataInfo.StartDeptId);
            //cbRelatonUsers.Visible = cbPayer.Checked;
            //if (!string.IsNullOrEmpty(formDataInfo.LeadersSelected))
            //{
            //    string[] cbDatas = formDataInfo.LeadersSelected.Split(',');
            //    foreach (var cbItem in cbDatas)
            //    {
            //        ListItem listItem = cbRelatonUsers.Items.FindByValue(cbItem);
            //        listItem.Selected = true;
            //    }
            //}
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
        options.Add(Option_1);
        options.Add(Option_2);
        options.Add(Option_3);
        options.Add(Option_4);
        options.Add(Option_5);
        options.Add(Option_6);
        options.Add(Option_7);
        options.Add(Option_8);
        options.Add(Option_9);
        options.Add(Option_0);
        options.Add(Option_11);
        options.Add(Option_10);
        options.Add(Option_12);
        options.Add(Option_13);
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
            //Countersign1.SaveAndSubmit();
            //
            string action = "同意";
            //Countersign1.SaveData(true);
            //Countersign1.SaveAndSubmit();
           // UpdateWFParams();

            string optionDefault = "";
            //if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name == "集团相关部门意见")
            //{
            //     optionDefault ="无意见";
            //}
            //else
            //{
                optionDefault = "同意";
           // }
            
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
                   
                    wf_WorkFlowInstance.UpdateNowStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                }
            }


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
                wf_WorkFlowInstance.UpdateNowStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
        }
    }

    void Alert(Page page, object message)
    {
        StringBuilder sb = new StringBuilder();
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message) + sb.ToString();
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }
}
