using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ.OA;
using Pkurg.PWorldBPM.Business.Workflow;
using SourceCode.Workflow.Client;

public partial class Workflow_ApprovePage_A_OA_ContractAuditOfGroup : UPageBase
{
    ContractAuditOfGroup Aitems = new ContractAuditOfGroup();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();
    string GroupCode = System.Configuration.ConfigurationManager.AppSettings["GroupCode"];
    string DefaultApprover = System.Configuration.ConfigurationManager.AppSettings["DefaultApprover"];
    string FWDeptCode = System.Configuration.ConfigurationManager.AppSettings["GroupLawDeptCode"];
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
            InitCheckBoxList();
            InitCheckButton();
            InitLeader();
            Countersign1.CounterSignDeptId = GroupCode;//集团作为会签基准部门
        }
    }
    /// <summary>
    /// 初始化领导
    /// </summary>
    private void InitLeader()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(StartDeptId, "部门负责人");
        DataTable dtPresident = bfurd.GetSelectRoleUser(GroupCode, "总裁");
        DataTable dtChairman = bfurd.GetSelectRoleUser(GroupCode, "董事长");

        if (dtDeptManager.Rows.Count != 0)
        {
            lbDeptManager.Text = "(" + dtDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbCEO.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtChairman.Rows.Count != 0)
        {
            lbChairman.Text = "(" + dtChairman.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) != GroupCode)
        {
            trDept.Visible = false;
        }
    }
    /// <summary>
    /// 初始化检查按钮
    /// </summary>
    private void InitCheckButton()
    {
        if (nodeName.Value == "流程审核员审核")
        {
            cbAP.Enabled = true;
            cbVP.Enabled = true;
            cbChairman.Enabled = true;
            cbCEO.Enabled = true;
            cbIsReport.Enabled = true;
            cblDirectors.Visible = true;
            lbDirector.Visible = true;
            Countersign1.IsCanEdit = true;
            tbContractContent.ReadOnly = false;
            tbContractTitle.ReadOnly = false;
        }
        else
        {
            Countersign1.IsCanEdit = false;
        }
    }
    /// <summary>
    /// 初始化CheckBoxList
    /// </summary>
    private void InitCheckBoxList()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtLeaders = bfurd.GetSelectRoleUser(GroupCode, "总办会成员");

        foreach (DataRow user in dtLeaders.Rows)
        {
            ListItem li = new ListItem();
            li.Value = "K2:Founder\\" + user["LoginName"].ToString();
            li.Text = user["EmployeeName"].ToString();
            if (!cblDirectors.Items.Contains(li))
            {
                cblDirectors.Items.Add(li);
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            XmlDocument xmldoc = new XmlDocument();
            ContractAuditOfGroupInfo formDataInfo = Aitems.Get(FormID);
            if (!string.IsNullOrEmpty(formDataInfo.LeadersSelected))
            {
                xmldoc.LoadXml(formDataInfo.LeadersSelected);
            }
            //其他董事意见【其他总办会领导意见】
            XmlNode NodeLeaders = xmldoc.SelectSingleNode("//Directors");
            if (NodeLeaders != null && NodeLeaders.Attributes["ID"].Value.Length > 0)
            {
                foreach (string UserGuid in NodeLeaders.Attributes["ID"].Value.Split(','))
                {
                    for (int i = 0; i < cblDirectors.Items.Count; i++)
                    {
                        if (cblDirectors.Items[i].Value == UserGuid)
                        {
                            cblDirectors.Items[i].Selected = true;
                        }
                    }
                }
            }
            //董事长意见
            //XmlNode NodeChairman = xmldoc.SelectSingleNode("//Chairman");
            //if (NodeChairman != null && NodeChairman.Attributes["ID"].Value != "noapprovers")
            //{
            //    cbChairman.Checked = true;
            //}
            //XmlNode NodePresident = xmldoc.SelectSingleNode("//President");
            //if (NodePresident != null && NodePresident.Attributes["ID"].Value != "noapprovers")
            //{
            //    cbPresident.Checked = true;
            //}

            //相关部门主管副副总裁
            XmlNode NodeVP = xmldoc.SelectSingleNode("//VicePresident");
            if (NodeVP != null && NodeVP.Attributes["ID"].Value != "noapprovers")
            {
                cbVP.Checked = true;
            }
            //相关部门主管助理总裁
            XmlNode NodeAP = xmldoc.SelectSingleNode("//AssiPresident");
            if (NodeAP != null && NodeAP.Attributes["ID"].Value != "noapprovers")
            {
                cbAP.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < cblDirectors.Items.Count; i++)
            {
                cblDirectors.Items[i].Selected = false;
            }
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
    }
    /// <summary>
    /// 绑定表单数据
    /// </summary>
    private void BindFormData()
    {
        try
        {
            ContractAuditOfGroupInfo obj = Aitems.Get(FormID);
            if (obj != null)
            {
                //保存数据
                tbReportCode.Text = obj.FormID;
                cblSecurityLevel.SelectedIndex = int.Parse(obj.SecurityLevel.ToString());
                cblUrgenLevel.SelectedIndex = int.Parse(obj.UrgenLevel.ToString());
                tbDepartName.Text = obj.DeptName;
                lbDeptCode.Text = obj.DeptCode;
                tbUserName.Text = obj.UserName;
                tbMobile.Text = obj.Mobile;
                tbDateTime.Text = obj.DateTime;
                //合同类型
                if (obj.ContractType1 == "00")
                {
                    ddlContractType1.Text = "";
                }
                else
                {
                    ddlContractType1.Text = obj.ContractTypeName1;
                }
                ddlContractType2.Text = obj.ContractTypeName2;
                ddlContractType3.Text = obj.ContractTypeName3;

                tbContractSum.Text = obj.ContractSum;
                cblIsSupplementProtocol.SelectedValue = obj.IsSupplementProtocol;
                tbSupplementProtocol.Text = obj.IsSupplementProtocolText;
                cblIsFormatContract.SelectedValue = obj.IsFormatContract;
                cblIsNormText.SelectedValue = obj.IsNormText;
                cblIsBidding.SelectedValue = obj.IsBidding;
                cblIsEstateProject.SelectedValue = obj.IsEstateProject;
                if (obj.EstateProjectName == "0")
                {
                    ddlEstateProjectName.Text = "";
                }
                else
                {
                    ddlEstateProjectName.Text = obj.EstateProjectNameText;
                }
                if (obj.EstateProjectNum == "0")
                {
                    ddlEstateProjectNum.Text = "";
                }
                else
                {
                    ddlEstateProjectNum.Text = obj.EstateProjectNumText;
                }
                //合同主体
                if (obj.ContractSubject == "00000")
                {
                    ddlContractSubject.Text = "";
                }
                else
                {
                    ddlContractSubject.Text = obj.ContractSubjectName;
                }
                tbContractSubject1.Text = obj.ContractSubjectName2;
                tbContractSubject2.Text = obj.ContractSubjectName3;
                tbContractSubject3.Text = obj.ContractSubjectName4;

                tbContractTitle.Text = obj.ContractTitle;
                tbContractContent.Text = obj.ContractContent;

                cbIsReport.Checked = obj.IsReport == "1" ? true : false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 初始化审批意见表：tbDeptLeaderOpion如何来的
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
    /// 批准事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Agree_Click(object sender, EventArgs e)
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormID);
        bool IsSubmit = true;
        if (nodeName.Value == "流程审核员审核")
        {
            SaveWFParams();
            UpdateWFParams();
            SaveData(ViewState["FormID"].ToString(), "");
        }

        if (IsSubmit)
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
    /// <summary>
    /// 拒绝事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                    UpdateByUserName = CurrentEmployee.EmployeeName,
                    FinishedTime = DateTime.Now
                };
                bfApproval.AddApprovalRecord(appRecord);
                ViewState["IsSubmit"] = true;
                wf_WorkFlowInstance.UpdateStatus(workFlowInstance.WfInstanceId, "1", nodeID.Value, nodeName.Value, int.Parse(taskID.Value), null, CurrentEmployee);

                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); window.opener.location.href=window.opener.location.href; ", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
            }
        }
    }
    /// <summary>
    /// 提交事件
    /// </summary>
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
    /// <summary>
    /// 将结果变为不同意
    /// </summary>
    public void ChangeResultToUnAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "0");
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
    }
    /// <summary>
    /// 得到审批意见
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
        options.Add(OpinionDeptManager);
        options.Add(OpinionCountersign);
        options.Add(OpinionLawDept);
        options.Add(OpinionAP);
        options.Add(OpinionLawAP);
        options.Add(OpinionVP);
        options.Add(OpinionDirectors);
        options.Add(OpinionCEO);
        options.Add(OpinionChairman);
        options.Add(OpinionStartToFinallyContract);
        options.Add(OpinionLawAuditOpinion);
        options.Add(OpinionSealAdministrator);
        options.Add(OpinionFileManager);
        return options;
    }
    /// <summary>
    /// 提示框
    /// </summary>
    /// <param name="page"></param>
    /// <param name="message"></param>
    void Alert(Page page, object message)
    {
        StringBuilder sb = new StringBuilder();
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message) + sb.ToString();
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private void SaveData(string ID, string wfStatus)
    {
        ContractAuditOfGroupInfo obj = null;
        try
        {
            obj = Aitems.Get(ID);
            obj.FormID = ViewState["FormID"].ToString();
            obj.LeadersSelected = lblApprovers.Text;
            obj.IsReport = cbIsReport.Checked ? "1" : "0";
            obj.ContractContent = tbContractContent.Text;
            obj.ContractTitle = tbContractTitle.Text;

            Aitems.Update(obj);

            WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(obj.FormID);
            Countersign1.ProcId = workFlowInstance.InstanceId;
            Countersign1.SaveData(true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 更新流程参数
    /// </summary>
    private void UpdateWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(lblApprovers.Text);
        XmlNode NodeTemp;
        string LeaderTemp;
        //董事长意见
        //NodeTemp = xmldoc.SelectSingleNode("//Chairman");
        //if (NodeTemp != null)
        //{
        //    LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        //}
        //else
        //{
        //    LeaderTemp = "noapprovers";
        //}
        //dataFields.Add("Chairman", LeaderTemp);
        //总裁意见
        NodeTemp = xmldoc.SelectSingleNode("//President");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("President", LeaderTemp);
        //其他董事意见【总办会，其他总办领导】
        NodeTemp = xmldoc.SelectSingleNode("//Directors");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("Directors", LeaderTemp);
        //相关部门主管副总裁意见
        NodeTemp = xmldoc.SelectSingleNode("//VicePresident");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("VicePresident", LeaderTemp);
        //相关部门主管助理总裁意见
        NodeTemp = xmldoc.SelectSingleNode("//AssiPresident");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("AssiPresident", LeaderTemp);
        //会签【相关部门意见】
        NodeTemp = xmldoc.SelectSingleNode("//CounterSignUsers");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("CounterSignUsers", LeaderTemp);

        //是否上报
        dataFields.Add("IsReport", cbIsReport.Checked ? "1" : "0");
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, DefaultApprover);
    }
    /// <summary>
    /// 保存流程参数
    /// </summary>
    /// <returns></returns>
    private string SaveWFParams()
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmleLeaders = xmldoc.CreateElement("Leaders");
        xmldoc.AppendChild(xmleLeaders);
        List<string> ApproverList = new List<string>();
        string LeaderTemp = string.Empty;
        List<string> countersigns = Countersign1.Result.Split(',').ToList();

        //需要去重，所以顺序从后向前
        //董事长意见
        //if (this.cbChairman.Checked)
        //{
        //    LeaderTemp = GetRoleUsers(GroupCode, "董事长");
        //    XmlElement xmleChairman = xmldoc.CreateElement("Chairman");
        //    xmleLeaders.AppendChild(xmleChairman);
        //    xmleChairman.SetAttribute("ID", LeaderTemp);
        //    ApproverList.Add(LeaderTemp);
        //}
        //总裁意见
        if (this.cbCEO.Checked)
        {
            LeaderTemp = GetRoleUsers(GroupCode, "总裁");
            XmlElement xmlePresident = xmldoc.CreateElement("President");
            xmleLeaders.AppendChild(xmlePresident);
            xmlePresident.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }
        //相关部门主管副副总裁
        if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) == GroupCode)
        {
            countersigns.Add(StartDeptId);
        }
        countersigns.Add(FWDeptCode);
        if (cbVP.Checked)
        {
            LeaderTemp = string.Empty;
            XmlElement xmlVP = xmldoc.CreateElement("VicePresident");
            xmleLeaders.AppendChild(xmlVP);
            foreach (var item in countersigns)
            {
                string LeaderVPTemp = GetRoleUsers(item, "主管副总裁");
                if (!string.IsNullOrEmpty(LeaderVPTemp) && !ApproverList.Contains(LeaderVPTemp))
                {
                    ApproverList.Add(LeaderVPTemp);
                    LeaderTemp += LeaderVPTemp + ",";
                }
            }
            LeaderTemp = LeaderTemp.TrimEnd(',');
            xmlVP.SetAttribute("ID", LeaderTemp);
        }
        //相关部门主管助理总裁
        if (cbAP.Checked)
        {
            LeaderTemp = string.Empty;
            XmlElement xmlAP = xmldoc.CreateElement("AssiPresident");
            xmleLeaders.AppendChild(xmlAP);
            foreach (var item in countersigns)
            {
                string LeaderAPTemp = GetRoleUsers(item, "主管助理总裁");
                if (!string.IsNullOrEmpty(LeaderAPTemp) && !ApproverList.Contains(LeaderAPTemp))
                {
                    ApproverList.Add(LeaderAPTemp);
                    LeaderTemp += LeaderAPTemp + ",";
                }
            }
            LeaderTemp = LeaderTemp.TrimEnd(',');
            xmlAP.SetAttribute("ID", LeaderTemp);
        }
        //其他董事意见【其他总办会领导意见】
        if (cblDirectors.SelectedIndex != -1)
        {
            LeaderTemp = string.Empty;
            XmlElement xmlDirectors = xmldoc.CreateElement("Directors");
            xmleLeaders.AppendChild(xmlDirectors);

            for (int i = 0; i < cblDirectors.Items.Count; i++)
            {
                if (cblDirectors.Items[i].Selected && !ApproverList.Contains(cblDirectors.Items[i].Value))
                {
                    ApproverList.Add(cblDirectors.Items[i].Value);
                    LeaderTemp += cblDirectors.Items[i].Value + ",";
                }
            }
            xmlDirectors.SetAttribute("ID", LeaderTemp);
        }
        //会签【相关部门意见】
        countersigns.Remove(StartDeptId);
        if (!string.IsNullOrEmpty(Countersign1.Result))
        {
            LeaderTemp = FilterDataField2(Countersign1.GetCounterSignUsers());
            XmlElement xmleCountersign = xmldoc.CreateElement("CounterSignUsers");
            xmleLeaders.AppendChild(xmleCountersign);
            xmleCountersign.SetAttribute("ID", LeaderTemp);
        }

        lblApprovers.Text = xmleLeaders.OuterXml;
        return lblApprovers.Text;
    }

    /// <summary>
    /// 过滤DataField
    /// </summary>
    /// <param name="dataField_old"></param>
    /// <returns></returns>
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
    private string FilterDataField2(string dataField)
    {
        dataField = dataField.Trim(',');

        if (!dataField.Contains(','))
        {
            if (string.IsNullOrEmpty(dataField))
            {
                dataField = "noapprovers";
            }
        }
        else//多个审批人
        {
            string nowApprovers = "";
            List<string> nowApproverList = dataField.Split(',').ToList();
            foreach (var item in nowApproverList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    nowApprovers = nowApprovers + item + ",";
                }
            }
            dataField = nowApprovers == "" ? "noapprovers" : nowApprovers.Trim(',');
        }

        return dataField;
    }
    private string FilterDataField2(StringBuilder dataField)
    {
        return FilterDataField2(dataField.ToString().Trim(','));
    }
    /// <summary>
    /// 得到用户角色
    /// </summary>
    /// <param name="dept"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
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