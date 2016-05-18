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
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_EditPage_E_OA_InstructionOfGroup : UPageBase
{
    public string className = "Workflow_EditPage_E_OA_InstructionOfGroup";

    InstructionOfGroup Eitems = new InstructionOfGroup();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    string GroupCode = System.Configuration.ConfigurationManager.AppSettings["GroupCode"];
    string DutyFreeShopCode = System.Configuration.ConfigurationManager.AppSettings["DutyFreeShopCode"];
    string DutyFreeDeptCode = System.Configuration.ConfigurationManager.AppSettings["DutyFreeDeptCode"];
    public string StartDeptId
    {
        get
        {
            return ddlDepartName.SelectedItem.Value;
        }
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

    protected override void OnPreRender(EventArgs e)
    {
        //防止二次提交
        ScriptManager.RegisterOnSubmitStatement(Page, typeof(Page), "Go_disabled", "disabledButton('lbSave');disabledButton('lbSubmit');disabledButton('lbClose');");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Clear_disabled", "enableButton('lbSave');enableButton('lbSubmit');enableButton('lbClose');", true);

        if (!IsPostBack)
        {
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitDepartName();
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(Request.QueryString["id"]);
                if (info != null)
                {
                    ViewState["FormID"] = info.FormId;
                    BindFormData();
                    SetUserControlInstance();
                }
            }
            else
            {
                FormId = BPMHelp.GetSerialNumber("OA_QS_");
                tbReportCode.Text = FormId;
                UpdatedTextBox.Value = DateTime.Now.ToShortDateString();
                tbUserName.Text = CurrentEmployee.EmployeeName;
                tbMobile.Text = CurrentEmployee.MobilePhone;
                cbAP.Checked = true;
                cbVP.Checked = true;
                cbPresident.Checked=true;
            }
            InitLeader();
            InitCheckBoxList();
            Countersign1.CounterSignDeptId = GroupCode;//集团作为会签基准部门
        }
    }

    protected void InitDepartName()
    {
        Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(_BPMContext.CurrentUser.MainDeptId);
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        Pkurg.PWorld.Entities.TList<Department> deptInfo2 = bfurd.GetDeptListByEmployeeCodeAndRoleName(_BPMContext.CurrentPWordUser.EmployeeCode, "部门成员");

        ddlDepartName.Items.Add(new ListItem()
        {
            Text = deptInfo.Remark,
            Value = deptInfo.DepartCode
        });

        foreach (Department deptItem in deptInfo2)
        {
            if (deptInfo.DepartCode != deptItem.DepartCode)
            {
                ListItem item = new ListItem()
                {
                    Text = deptItem.Remark,
                    Value = deptItem.DepartCode
                };
                ddlDepartName.Items.Add(item);
            }
        }
    }

    private void InitCheckBoxList()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtLeaders = bfurd.GetSelectRoleUser(GroupCode, "总办会成员");
        DataTable dtRelatedManager = bfurd.GetSelectRoleUser(GroupCode, "免税店条线负责人");

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
        foreach (DataRow user in dtRelatedManager.Rows)
        {
            ListItem li = new ListItem();
            li.Value = "K2:Founder\\" + user["LoginName"].ToString();
            li.Text = user["EmployeeName"].ToString();
            if (!cblRelatedManager.Items.Contains(li))
            {
                cblRelatedManager.Items.Add(li);
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            XmlDocument xmldoc = new XmlDocument();
            InstructionOfGroupInfo formDataInfo = Eitems.Get(FormId);
            if (!string.IsNullOrEmpty(formDataInfo.LeadersSelected))
            {
                xmldoc.LoadXml(formDataInfo.LeadersSelected);
            }

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
            XmlNode NodeRelatedManager = xmldoc.SelectSingleNode("//RelatedManager");
            if (NodeRelatedManager != null && NodeRelatedManager.Attributes["ID"].Value.Length > 0)
            {
                foreach (string UserGuid in NodeRelatedManager.Attributes["ID"].Value.Split(','))
                {
                    for (int i = 0; i < cblRelatedManager.Items.Count; i++)
                    {
                        if (cblRelatedManager.Items[i].Value == UserGuid)
                        {
                            cblRelatedManager.Items[i].Selected = true;
                        }
                    }
                }
            }
            XmlNode NodeChairman = xmldoc.SelectSingleNode("//Chairman");
            if (NodeChairman != null && NodeChairman.Attributes["ID"].Value != "noapprovers")
            {
                cbChairman.Checked = true;
            }
            else
            {
                cbChairman.Checked = false;
            }
            XmlNode NodePresident = xmldoc.SelectSingleNode("//President");
            if (NodePresident != null && NodePresident.Attributes["ID"].Value != "noapprovers")
            {
                cbPresident.Checked = true;
            }
            else
            {
                cbPresident.Checked = false;
            }
            XmlNode NodeVP = xmldoc.SelectSingleNode("//VicePresident");
            if (NodeVP != null && NodeVP.Attributes["ID"].Value != "noapprovers")
            {
                cbVP.Checked = true;
            }
            else
            {
                cbVP.Checked = false;
            }
            XmlNode NodeAP = xmldoc.SelectSingleNode("//AssiPresident");
            if (NodeAP != null && NodeAP.Attributes["ID"].Value != "noapprovers")
            {
                cbAP.Checked = true;
            }
            else
            {
                cbAP.Checked = false;
            }
            XmlNode NodeDutyFreeManager = xmldoc.SelectSingleNode("//DutyFreeManager");
            if (NodeDutyFreeManager != null && NodeDutyFreeManager.Attributes["ID"].Value != "noapprovers")
            {
                cbDutyFreeManager.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < cblDirectors.Items.Count; i++)
            {
                cblDirectors.Items[i].Selected = false;
            }
            for (int i = 0; i < cblRelatedManager.Items.Count; i++)
            {
                cblRelatedManager.Items[i].Selected = false;
            }
        }
    }

    private void InitLeader()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(StartDeptId, "部门负责人");
        DataTable dtPresident = bfurd.GetSelectRoleUser(GroupCode, "总裁");
        DataTable dtChairman = bfurd.GetSelectRoleUser(GroupCode, "董事长");
        DataTable dtDutyFreeManager = bfurd.GetSelectRoleUser(DutyFreeDeptCode, "部门负责人");

        if (dtDeptManager.Rows.Count != 0)
        {
            lbDeptManager.Text = "(" + dtDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbPresident.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtChairman.Rows.Count != 0)
        {
            lbChairman.Text = "(" + dtChairman.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtDutyFreeManager.Rows.Count != 0)
        {
            lbDutyFreeManager.Text = "(" + dtDutyFreeManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) != GroupCode)
        {
            trDept.Visible = false;
        }
        else
        {
            trDept.Visible = true;
        }
        if (StartDeptId.Substring(0, 4) == DutyFreeShopCode)
        {
            trDutyFree.Visible = true;
            trDutyFreeOpinion.Visible = true;
            cbAP.Checked = false;
            cbVP.Checked = false;
            cbPresident.Checked = false;
        }
    }

    private void BindFormData()
    {
        try
        {
            InstructionOfGroupInfo obj = Eitems.Get(ViewState["FormID"].ToString());

            lbIsReport.Text = string.IsNullOrEmpty(obj.RelatedFormID) ? "0" : "1";
            if (lbIsReport.Text == "0")
            {
                ListItem item = ddlDepartName.Items.FindByValue(obj.DeptCode);
                if (item != null)
                {
                    ddlDepartName.SelectedIndex = ddlDepartName.Items.IndexOf(item);
                }
            }
            else
            {
                ddlDepartName.Items.Clear();
                string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(obj.DeptCode);
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(CompanyCode);

                ListItem item = new ListItem { Text = deptInfo.DepartName, Value = CompanyCode };
                ddlDepartName.Items.Add(item);
                ddlDepartName.Enabled = false;
            }
            tbReportCode.Text = obj.FormID;
            tbUserName.Text = obj.UserName;
            tbMobile.Text = obj.Mobile;
            tbTitle.Text = obj.Title;
            tbContent.Text = obj.Content;
            UpdatedTextBox.Value = !string.IsNullOrEmpty(obj.DateTime) ? obj.DateTime : DateTime.Now.ToShortDateString(); 
            cblSecurityLevel.SelectedValue = obj.SecurityLevel != null ? obj.SecurityLevel.ToString() : "-1";
            cblUrgenLevel.SelectedValue = obj.UrgenLevel != null ? obj.UrgenLevel.ToString() : "-1";
            cbIsReport.Checked = obj.IsReport == "1" ? true : false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetUserControlInstance()
    {
        string instId = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(instId))
        {
            FlowRelated1.ProcId = instId;
            Countersign1.ProcId = instId;
            
            UploadAttachments1.ProcId = instId;
            hfInstanceId.Value = instId;
        }
    }

    private InstructionOfGroupInfo SaveData(string ID, string wfStatus)
    {
        SaveWFParams();
        InstructionOfGroupInfo obj = null;
        try
        {
            UploadAttachments1.SaveAttachment(ViewState["FormID"].ToString());
            obj = Eitems.Get(ID);
            bool isEdit = false;
            if (obj == null)
            {
                obj = new InstructionOfGroupInfo();
                obj.FormID = ViewState["FormID"].ToString();
            }
            else
            {
                isEdit = true;
                obj.FormID = ViewState["FormID"].ToString();
                //obj.ApproveStatus = wfStatus;
            }
            
            obj.DeptCode = ddlDepartName.SelectedItem.Value.ToString();
            obj.DeptName = ddlDepartName.SelectedItem.Text;
            obj.UserName = tbUserName.Text;
            obj.Mobile = tbMobile.Text;
            obj.Title = tbTitle.Text;
            obj.DateTime = UpdatedTextBox.Value;
            obj.Content = tbContent.Text;
            obj.LeadersSelected = lblApprovers.Text;
            if (cblSecurityLevel.SelectedIndex != -1)
            {
                obj.SecurityLevel = cblSecurityLevel.SelectedValue.ToString();
            }
            if (cblUrgenLevel.SelectedIndex != -1)
            {
                obj.UrgenLevel = cblUrgenLevel.SelectedValue.ToString();
            }
            obj.IsReport = cbIsReport.Checked ? "1" : "0";
            if (!isEdit)
            {
                Eitems.Insert(obj);
            }
            else
            {
                Eitems.Update(obj);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return obj;
    }

    private bool SaveWorkFlowInstance(InstructionOfGroupInfo obj, string WfStatus, DateTime? SumitTime, string WfInstanceId)
    {
        bool result = false;
        WorkFlowInstance workFlowInstance = null;
        try
        {
            workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(obj.FormID);
            bool isEdit = false;
            if (workFlowInstance == null)
            {
                workFlowInstance = new WorkFlowInstance();
                workFlowInstance.InstanceId = Guid.NewGuid().ToString();
                workFlowInstance.CreateDeptCode = ddlDepartName.SelectedItem.Value.ToString();
                workFlowInstance.CreateDeptName = ddlDepartName.SelectedItem.Text;
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.AppId = "3003";
            }
            else
            {
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                isEdit = true;
            }
            workFlowInstance.FormId = obj.FormID;
            workFlowInstance.FormTitle = obj.Title;
            workFlowInstance.WfStatus = WfStatus;
            if (SumitTime != null)
            {
                workFlowInstance.SumitTime = SumitTime;
            }

            if (WfInstanceId != "")
            {
                workFlowInstance.WfInstanceId = WfInstanceId;
            }

            if (!isEdit)
            {
                result = wf_WorkFlowInstance.AddWorkFlowInstance(workFlowInstance);
            }
            else
            {
                result = wf_WorkFlowInstance.UpdateWorkFlowInstance(workFlowInstance);
            }
            FlowRelated1.ProcId = workFlowInstance.InstanceId;
            Countersign1.ProcId = workFlowInstance.InstanceId;
            Countersign1.SaveData(true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return result;
    }

    /// <summary>
    /// 保存审批人员参数
    /// </summary>
    private string SaveWFParams()
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmleLeaders = xmldoc.CreateElement("Leaders");
        xmldoc.AppendChild(xmleLeaders);
        List<string> ApproverList = new List<string>();
        string LeaderTemp = string.Empty;
        List<string> countersigns = Countersign1.Result.Split(',').ToList();
        bool flag = true;//标记

        if (string.IsNullOrEmpty(GetRoleUsers(GroupCode, "总裁")))
        {
            flag = false;
            Alert(Page, "公司总裁尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(GroupCode, "流程审核人")))
        {
            flag = false;
            Alert(Page, "流程审核人尚未配置！");
        }
        
        if (this.cbChairman.Checked)
        {
            LeaderTemp = GetRoleUsers(GroupCode, "董事长");
            XmlElement xmleChairman = xmldoc.CreateElement("Chairman");
            xmleLeaders.AppendChild(xmleChairman);
            xmleChairman.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }
        if (this.cbPresident.Checked)
        {
            LeaderTemp = GetRoleUsers(GroupCode, "总裁");
            XmlElement xmlePresident = xmldoc.CreateElement("President");
            xmleLeaders.AppendChild(xmlePresident);
            xmlePresident.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }
        if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) == GroupCode)
        {
            countersigns.Add(StartDeptId); 
        }
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
        countersigns.Remove(StartDeptId); 
        if (!string.IsNullOrEmpty(Countersign1.Result))
        {
            LeaderTemp = FilterDataField2(Countersign1.GetCounterSignUsers());
            XmlElement xmleCountersign = xmldoc.CreateElement("CounterSignUsers");
            xmleLeaders.AppendChild(xmleCountersign);
            xmleCountersign.SetAttribute("ID", LeaderTemp);
        }

        if (1 == 1)
        {
            LeaderTemp = GetRoleUsers(GroupCode, "流程审核人");
            XmlElement xmleWorkflowAuditor = xmldoc.CreateElement("WorkflowAuditor");
            xmleLeaders.AppendChild(xmleWorkflowAuditor);
            xmleWorkflowAuditor.SetAttribute("ID", LeaderTemp);
        }

        if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) == GroupCode)
        {
            LeaderTemp = GetRoleUsers(StartDeptId, "部门负责人");
            XmlElement xmleDeptManager = xmldoc.CreateElement("DeptManager");
            xmleLeaders.AppendChild(xmleDeptManager);
            xmleDeptManager.SetAttribute("ID", LeaderTemp);
        }
        if (StartDeptId.Substring(0, 4) == DutyFreeShopCode)
        {
            LeaderTemp = GetRoleUsers(DutyFreeDeptCode, "部门负责人");
            XmlElement xmleDutyFreeManagerr = xmldoc.CreateElement("DutyFreeManager");
            xmleLeaders.AppendChild(xmleDutyFreeManagerr);
            xmleDutyFreeManagerr.SetAttribute("ID", LeaderTemp);
        }
        if (StartDeptId.Substring(0, 4) == DutyFreeShopCode && cblRelatedManager.SelectedIndex != -1)
        {
            LeaderTemp = string.Empty;
            XmlElement xmlRelatedManager = xmldoc.CreateElement("RelatedManager");
            xmleLeaders.AppendChild(xmlRelatedManager);

            for (int i = 0; i < cblRelatedManager.Items.Count; i++)
            {
                if (cblRelatedManager.Items[i].Selected && !ApproverList.Contains(cblRelatedManager.Items[i].Value))
                {
                    ApproverList.Add(cblRelatedManager.Items[i].Value);
                    LeaderTemp += cblRelatedManager.Items[i].Value + ",";
                }
            }
            xmlRelatedManager.SetAttribute("ID", LeaderTemp);
        }

        if (!flag)
        {
            lblApprovers.Text = null;
        }
        else
        {
            lblApprovers.Text = xmleLeaders.OuterXml;
        }
        return lblApprovers.Text;
    }

    /// <summary>
    /// 设置流程参数
    /// </summary>
    /// <returns></returns>
    private NameValueCollection SetWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(lblApprovers.Text);
        XmlNode NodeTemp;
        string LeaderTemp;

        NodeTemp = xmldoc.SelectSingleNode("//Chairman");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("Chairman", LeaderTemp);

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

        NodeTemp = xmldoc.SelectSingleNode("//CounterSignUsers");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("CounterSignUsers", LeaderTemp);

        NodeTemp = xmldoc.SelectSingleNode("//WorkflowAuditor");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("WorkflowAuditor", LeaderTemp);

        NodeTemp = xmldoc.SelectSingleNode("//DeptManager");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("DeptManager", LeaderTemp);
        NodeTemp = xmldoc.SelectSingleNode("//RelatedManager");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("RelatedManager", LeaderTemp);
        NodeTemp = xmldoc.SelectSingleNode("//DutyFreeManager");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("DutyFreeManager", LeaderTemp);
        dataFields.Add("IsReport", cbIsReport.Checked ? "1" : "0");
        dataFields.Add("IsPass", "1");
        return dataFields;
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        InstructionOfGroupInfo obj = SaveData(id, "00");
        if (obj != null)
        {
            if (SaveWorkFlowInstance(obj, "0", null, ""))
            {
                Alert(Page, "保存成功！");
            }
        }
        else
        {
            Alert(Page, "保存失败");
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        string SaveVerification = SaveWFParams();
        if (string.IsNullOrEmpty(SaveVerification))
        {
            return;
        }
        #region 工作流参数
        NameValueCollection dataFields = SetWFParams();
        if (dataFields == null)
        {
            return;
        }
        #endregion

        InstructionOfGroupInfo obj = SaveData(id, "02");
        if (obj != null)
        {
            //开启流程
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            AppDict appInfo = new Pkurg.BPM.Services.AppDictService().GetByAppId("3003");
            if (appInfo == null)
            {
                Alert("提交失败");
                return;
            }
            int wfInstanceId = 0; //process instance id
            WorkflowHelper.StartProcess(@"K2Workflow\OA_InstructionOfGroup", id, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance(obj, "1", DateTime.Now, wfInstanceId.ToString()))
                {

                    if (Eitems.UpdateStatus(id, "02"))
                    {
                        string Opinion = "";
                        string ApproveResult = "同意";
                        string OpinionType = "";
                        string IsSign = "0";
                        string DelegateUserName = "";
                        string DelegateUserCode = "";
                        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(obj.FormID);

                        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
                        {
                             ApprovalID = Guid.NewGuid().ToString(),
                
                            FormID = id,
                            InstanceID = workFlowInstance.InstanceId,
                            Opinion = Opinion,
                            ApproveAtTime = DateTime.Now,
                            ApproveByUserCode = CurrentEmployee.EmployeeCode,
                            ApproveByUserName = CurrentEmployee.EmployeeName,
                            ApproveResult = ApproveResult,
                            OpinionType = OpinionType,
                            CurrentActiveName = "拟稿",
                            ISSign = IsSign,
                
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
                        BFApprovalRecord bfApproval = new BFApprovalRecord();
                        bfApproval.AddApprovalRecord(appRecord);
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                }
            }
        }
        Alert("提交失败");
    }
    public void Alert(string msg)
    {
        DisplayMessage.ExecuteJs(string.Format("alert('{0}');", msg));
    }
    /// <summary>
    /// 终止
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        if (_BPMContext.ProcInst != null)
        {
            new WF_WorkFlowInstance().UpdateNowStatusByFormID(FormId, "5");
            DisplayMessage.ExecuteJs("alert('操作成功'); window.close();");
        }
        else
        {
            DisplayMessage.ExecuteJs("window.close();");
        }
    }

    void Alert(Page page, object message)
    {
        StringBuilder sb = new StringBuilder();
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message) + sb.ToString();
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    /// <summary>
    /// 执行JS
    /// </summary>
    /// <param name="page"></param>
    /// <param name="js"></param>
    public static void RunJs(Page page, string js)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
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

    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        //Countersign1.Refresh();
        InitLeader();
        InitCheckBoxList();
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
}
