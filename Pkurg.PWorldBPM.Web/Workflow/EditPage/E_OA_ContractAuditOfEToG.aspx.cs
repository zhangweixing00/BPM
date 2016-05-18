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
using Pkurg.PWorldBPM.Business.BIZ.OA;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_EditPage_E_OA_ContractAuditOfEToG : UPageBase
{
    ContractAuditOfEToG Eitems = new ContractAuditOfEToG();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    //定义StartDeptId以及FormId
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

    /// <summary>
    /// 加载页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //防止二次提交
        ScriptManager.RegisterOnSubmitStatement(Page, typeof(Page), "Go_disabled", "disabledButton('lbSave');disabledButton('lbSubmit');disabledButton('lbClose');");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Clear_disabled", "enableButton('lbSave');enableButton('lbSubmit');enableButton('lbClose');", true);

        if (!IsPostBack)
        {
            InitDepartName();
            //合同类型
            ddlContractType1.DataSource = ContractTypeInfosHelper.GetFirstContractTypeInfos();
            ddlContractType1.DataTextField = "value";
            ddlContractType1.DataValueField = "key";
            ddlContractType1.DataBind();
            //合同主体
            ddlContractSubject.DataSource = ContractSubjectHelper.GetContractSubjectInfos();
            ddlContractSubject.DataTextField = "value";
            ddlContractSubject.DataValueField = "key";
            ddlContractSubject.DataBind();

            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(Request.QueryString["id"]);
                if (info != null)
                {
                    ViewState["FormID"] = info.FormId;
                    //绑定表单数据
                    BindFormDate();
                    //设置用户控制实例
                    SetUserControlInstance();
                }
            }
            else
            {
                //投资公司的编码前缀为I
                FormId = BPMHelp.GetSerialNumber("OA_EHT_");
                tbReportCode.Text = FormId;
                UpdatedTextBox.Value = DateTime.Now.ToShortDateString();
                tbUserName.Text = CurrentEmployee.EmployeeName;
                tbMobile.Text = CurrentEmployee.MobilePhone;
                cbPresident.Checked = true;
                cbCFO.Checked = true;
                cbAP.Checked = true;
                cbVP.Checked = true;

                if (ddlContractType1.Items.Count != 0)
                {
                    ddlContractType1.SelectedIndex = 0;
                    ddlContractType2.DataSource = ContractTypeInfosHelper.GetSecondContractTypeInfos(ddlContractType1.SelectedItem.Value);
                    ddlContractType2.DataTextField = "value";
                    ddlContractType2.DataValueField = "key";
                    ddlContractType2.DataBind();
                    if (ddlContractType2.Items.Count != 0)
                    {
                        ddlContractType2.SelectedIndex = 0;
                        ddlContractType3.DataSource = ContractTypeInfosHelper.GetThirdContractTypeInfos(ddlContractType2.SelectedItem.Value);
                        ddlContractType3.DataTextField = "value";
                        ddlContractType3.DataValueField = "key";
                        ddlContractType3.DataBind();
                    }
                }
            }
            InitLeader();
            InitCheckBoxList();
        }
    }
    /// <summary>
    /// 初始化领导
    /// </summary>
    private void InitLeader()
    {
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(StartDeptId, "部门负责人");
        DataTable dtEVP = bfurd.GetSelectRoleUser(CompanyCode, "常务副总裁");
        DataTable dtCFO = bfurd.GetSelectRoleUser(CompanyCode, "主管财务领导");
        DataTable dtPresident = bfurd.GetSelectRoleUser(CompanyCode, "总裁");
        DataTable dtChairman = bfurd.GetSelectRoleUser(CompanyCode, "董事长");

        if (dtDeptManager.Rows.Count != 0)
        {
            lbDeptManager.Text = "(" + dtDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtCFO.Rows.Count != 0)
        {
            lbCFO.Text = "(" + dtCFO.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        else
        {
            lbCFO.Text = "尚未配置";
        }
        if (dtEVP.Rows.Count != 0)
        {
            lbEVP.Text = "(" + dtEVP.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbPresident.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtChairman.Rows.Count != 0)
        {
            lbChairman.Text = "(" + dtChairman.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
    }
    /// <summary>
    /// 初始化CheckBoxList
    /// </summary>
    private void InitCheckBoxList()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        string PresidentOfficeCode = BPMHelp.GetDeptIDByOtherIDAndName(StartDeptId, "总办会");
        DataTable dtLeaders = bfurd.GetSelectRoleUser(PresidentOfficeCode, "部门成员");
        DataTable dtDeptDirectors = bfurd.GetSelectRoleUser(StartDeptId, "部门经理");

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

        foreach (DataRow user in dtDeptDirectors.Rows)
        {
            ListItem li = new ListItem();
            li.Value = "K2:Founder\\" + user["LoginName"].ToString();
            li.Text = user["EmployeeName"].ToString();
            if (!cblDeptDirectors.Items.Contains(li))
            {
                cblDeptDirectors.Items.Add(li);
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            //将选择的领导存储在xml中
            XmlDocument xmldoc = new XmlDocument();
            ContractAuditOfEToGInfo formDataInfo = Eitems.Get(FormId);
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
            XmlNode NodeDeptDirectors = xmldoc.SelectSingleNode("//DeptDirectors");
            if (NodeDeptDirectors != null && NodeDeptDirectors.Attributes["ID"].Value.Length > 0)
            {
                foreach (string UserGuid in NodeDeptDirectors.Attributes["ID"].Value.Split(','))
                {
                    for (int i = 0; i < cblDeptDirectors.Items.Count; i++)
                    {
                        if (cblDeptDirectors.Items[i].Value == UserGuid)
                        {
                            cblDeptDirectors.Items[i].Selected = true;
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
            XmlNode NodeEVP = xmldoc.SelectSingleNode("//EVP");
            if (NodeEVP != null && NodeEVP.Attributes["ID"].Value != "noapprovers")
            {
                cbEVP.Checked = true;
            }
            else
            {
                cbEVP.Checked = false;
            }
            XmlNode NodeCFO = xmldoc.SelectSingleNode("//CFO");
            if (NodeCFO != null && NodeCFO.Attributes["ID"].Value != "noapprovers")
            {
                cbCFO.Checked = true;
            }
            else
            {
                cbCFO.Checked = false;
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
    /// 初始化起始部门名称
    /// </summary>
    private void InitDepartName()
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
    /// <summary>
    /// 绑定表单数据
    /// </summary>
    private void BindFormDate()
    {
        try
        {
            //根据formid得到合同表单数据
            ContractAuditOfEToGInfo obj = Eitems.Get(ViewState["FormID"].ToString());
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
                string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(CompanyCode);

                ListItem item = new ListItem { Text = deptInfo.DepartName, Value = CompanyCode };
                ddlDepartName.Items.Add(item);
                ddlDepartName.Enabled = false;
            }

            //保存数据
            tbReportCode.Text = obj.FormID;
            tbUserName.Text = obj.UserName;
            tbMobile.Text = obj.Mobile;
            UpdatedTextBox.Value = obj.DateTime;
            //合同类型
            ListItem li1 = ddlContractType1.Items.FindByValue(obj.ContractType1);
            if (li1 != null)
            {
                li1.Selected = true;
            }
            ddlContractType2.DataSource = ContractTypeInfosHelper.GetSecondContractTypeInfos(ddlContractType1.SelectedItem.Value);
            ddlContractType2.DataTextField = "value";
            ddlContractType2.DataValueField = "key";
            ddlContractType2.DataBind();

            ListItem li2 = ddlContractType2.Items.FindByValue(obj.ContractType2);
            if (li2 != null)
            {
                li2.Selected = true;
            }
            if (ddlContractType2.SelectedItem != null)
            {
                ddlContractType3.DataSource = ContractTypeInfosHelper.GetThirdContractTypeInfos(ddlContractType2.SelectedItem.Value);
                ddlContractType3.DataTextField = "value";
                ddlContractType3.DataValueField = "key";
                ddlContractType3.DataBind();
                ListItem li3 = ddlContractType3.Items.FindByValue(obj.ContractType3);
                if (li3 != null)
                {
                    li3.Selected = true;
                }
            }

            tbContractSum.Text = obj.ContractSum;
            cblIsSupplementProtocol.SelectedValue = obj.IsSupplementProtocol;
            tbSupplementProtocol.Text = obj.IsSupplementProtocolText;
            cblIsFormatContract.SelectedValue = obj.IsFormatContract;
            cblIsNormText.SelectedValue = obj.IsNormText;
            cblIsBidding.SelectedValue = obj.IsBidding;
            cblIsEstateProject.SelectedValue = obj.IsEstateProject;
            ddlEstateProjectName.SelectedValue = obj.EstateProjectName;
            ddlEstateProjectNum.SelectedValue = obj.EstateProjectNum;
            //合同主体
            ddlContractSubject.SelectedValue = obj.ContractSubject;
            tbContractSubject1.Text = obj.ContractSubjectName2;
            tbContractSubject2.Text = obj.ContractSubjectName3;
            tbContractSubject3.Text = obj.ContractSubjectName4;

            tbContractTitle.Text = obj.ContractTitle;
            tbContractContent.Text = obj.ContractContent;
            cblSecurityLevel.SelectedIndex = int.Parse(obj.SecurityLevel.ToString());
            cblUrgenLevel.SelectedIndex = int.Parse(obj.UrgenLevel.ToString());
            cbIsReport.Checked = obj.IsReport == "1" ? true : false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 设置用户控制实例
    /// </summary>
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
    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="wfStatus"></param>
    /// <returns></returns>
    private ContractAuditOfEToGInfo SaveData(string ID, string wfStatus)
    {
        SaveWFParams();
        ContractAuditOfEToGInfo obj = null;
        try
        {
            UploadAttachments1.SaveAttachment(ViewState["FormID"].ToString());
            obj = Eitems.Get(ID);
            bool isEdit = false;
            if (obj == null)
            {
                obj = new ContractAuditOfEToGInfo();
                obj.FormID = ViewState["FormID"].ToString();
            }
            else
            {
                isEdit = true;
                obj.FormID = ViewState["FormID"].ToString();
                //obj.ApproveStatus = wfStatus;
            }
            //存储数据
            //LINQ
            //var info = BizContext.OA_ContractAuditOfEToG.FirstOrDefault(x => x.FormID == FormId);
            //if (info == null)
            //{
            //    BizContext.OA_ContractAuditOfEToG.InsertOnSubmit(new Pkurg.PWorldBPM.Business.BIZ.OA_ContractAuditOfEToG()
            //    {
            //        FormID = FormId
            //    });
            //}
            //else
            //{
            //    info.DeptName = "";
            //}
            //BizContext.SubmitChanges();


            obj.DeptCode = ddlDepartName.SelectedItem.Value.ToString();
            obj.DeptName = ddlDepartName.SelectedItem.Text;
            obj.UserName = tbUserName.Text;
            obj.DateTime = UpdatedTextBox.Value;
            obj.Mobile = tbMobile.Text;
            //新添加的存储数据
            //合同类型
            //obj.ContractType2 = ddlContractType2.SelectedItem.Value;
            obj.ContractType1 = ddlContractType1.SelectedItem.Value;
            obj.ContractType2 = ddlContractType2.SelectedItem != null ? ddlContractType2.SelectedItem.Value : "";
            obj.ContractType3 = ddlContractType3.SelectedItem != null ? ddlContractType3.SelectedItem.Value : "";

            //obj.ContractTypeName2 = ddlContractType2.SelectedItem.Text;
            obj.ContractTypeName1 = ddlContractType1.SelectedItem.Text;
            obj.ContractTypeName2 = ddlContractType2.SelectedItem != null ? ddlContractType2.SelectedItem.Text : "";
            obj.ContractTypeName3 = ddlContractType3.SelectedItem != null ? ddlContractType3.SelectedItem.Text : "";

            obj.ContractSum = tbContractSum.Text;
            //需要和下面的两个一样进行判断吗？
            obj.IsSupplementProtocol = cblIsSupplementProtocol.SelectedValue.ToString();
            obj.IsSupplementProtocolText = tbSupplementProtocol.Text;
            obj.IsFormatContract = cblIsFormatContract.SelectedValue.ToString();
            obj.IsNormText = cblIsNormText.SelectedValue.ToString();
            obj.IsBidding = cblIsBidding.SelectedValue.ToString();
            obj.IsEstateProject = cblIsEstateProject.SelectedValue.ToString();
            obj.EstateProjectName = ddlEstateProjectName.SelectedValue.ToString();
            obj.EstateProjectNameText = ddlEstateProjectName.SelectedItem.Text;
            obj.EstateProjectNum = ddlEstateProjectNum.SelectedValue.ToString();
            obj.EstateProjectNumText = ddlEstateProjectNum.SelectedItem.Text;
            //合同主体
            obj.ContractSubject = ddlContractSubject.SelectedItem.Value;
            obj.ContractSubjectName = ddlContractSubject.SelectedItem.Text;
            obj.ContractSubjectName2 = tbContractSubject1.Text;
            obj.ContractSubjectName3 = tbContractSubject2.Text;
            obj.ContractSubjectName4 = tbContractSubject3.Text;

            obj.ContractTitle = tbContractTitle.Text;
            obj.ContractContent = tbContractContent.Text;
            obj.LeadersSelected = lblApprovers.Text;
            obj.SecurityLevel = cblSecurityLevel.SelectedIndex.ToString();
            obj.UrgenLevel = cblUrgenLevel.SelectedIndex.ToString();
            obj.IsReport = cbIsReport.Checked ? "1" : "0";
            obj.RelatedFormID = string.Empty;
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
    /// <summary>
    /// 保存工作流实例
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="WfStatus"></param>
    /// <param name="SumitTime"></param>
    /// <param name="WfInstanceId"></param>
    /// <returns></returns>
    private bool SaveWorkFlowInstance(ContractAuditOfEToGInfo obj, string WfStatus, DateTime? SumitTime, string WfInstanceId)
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
                workFlowInstance.AppId = "3008";
            }
            else
            {
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                isEdit = true;
            }
            workFlowInstance.FormId = obj.FormID;
            workFlowInstance.FormTitle = obj.ContractTitle;
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

        string LawDeptCode = BPMHelp.GetDeptIDByOtherIDAndName(StartDeptId, "法务部");
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);

        if (string.IsNullOrEmpty(GetRoleUsers(CompanyCode, "总裁")))
        {
            flag = false;
            Alert(Page, "总裁尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(CompanyCode, "流程审核人")))
        {
            flag = false;
            Alert(Page, "流程审核人尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(CompanyCode, "公章管理员")))
        {
            flag = false;
            Alert(Page, "公章管理员尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(CompanyCode, "档案管理员")))
        {
            flag = false;
            Alert(Page, "档案管理员尚未配置！");
        }
        //需要去重，所以顺序从后向前
        //董事长意见
        if (this.cbChairman.Checked)
        {
            LeaderTemp = GetRoleUsers(CompanyCode, "董事长");
            XmlElement xmleChairman = xmldoc.CreateElement("Chairman");
            xmleLeaders.AppendChild(xmleChairman);
            xmleChairman.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }
        //总裁意见
        if (this.cbPresident.Checked)
        {
            LeaderTemp = GetRoleUsers(CompanyCode, "总裁");
            XmlElement xmlePresident = xmldoc.CreateElement("President");
            xmleLeaders.AppendChild(xmlePresident);
            xmlePresident.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }

        if (this.cbEVP.Checked)
        {
            LeaderTemp = GetRoleUsers(CompanyCode, "常务副总裁");
            XmlElement xmleEVP = xmldoc.CreateElement("EVP");
            xmleLeaders.AppendChild(xmleEVP);
            xmleEVP.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }
        if (this.cbCFO.Checked)
        {
            LeaderTemp = GetRoleUsers(CompanyCode, "主管财务领导");
            XmlElement xmleCFO = xmldoc.CreateElement("CFO");
            xmleLeaders.AppendChild(xmleCFO);
            xmleCFO.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }
        //相关部门主管副总裁
        countersigns.Add(StartDeptId);
        countersigns.Add(LawDeptCode);
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
        //法务部意见
        if (1 == 1)
        {
            LeaderTemp = GetRoleUsers(LawDeptCode, "部门负责人");
            XmlElement xmleWorkflowAuditor = xmldoc.CreateElement("LawDeptManager");
            xmleLeaders.AppendChild(xmleWorkflowAuditor);
            xmleWorkflowAuditor.SetAttribute("ID", LeaderTemp);
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
        //流程审核员审核
        if (1 == 1)
        {
            LeaderTemp = GetRoleUsers(CompanyCode, "流程审核人");
            XmlElement xmleWorkflowAuditor = xmldoc.CreateElement("WorkflowAuditor");
            xmleLeaders.AppendChild(xmleWorkflowAuditor);
            xmleWorkflowAuditor.SetAttribute("ID", LeaderTemp);
        }
        //经办部门负责人
        if (cbDeptManager.Checked)
        {
            LeaderTemp = GetRoleUsers(StartDeptId, "部门负责人");
            XmlElement xmleDeptManager = xmldoc.CreateElement("DeptManager");
            xmleLeaders.AppendChild(xmleDeptManager);
            xmleDeptManager.SetAttribute("ID", LeaderTemp);
        }
        if (cblDeptDirectors.SelectedIndex != -1)
        {
            LeaderTemp = string.Empty;
            XmlElement xmlDeptDirectors = xmldoc.CreateElement("DeptDirectors");
            xmleLeaders.AppendChild(xmlDeptDirectors);

            for (int i = 0; i < cblDeptDirectors.Items.Count; i++)
            {
                if (cblDeptDirectors.Items[i].Selected)
                {
                    LeaderTemp += cblDeptDirectors.Items[i].Value + ",";
                }
            }
            xmlDeptDirectors.SetAttribute("ID", LeaderTemp);
        }
        //后面三个审批步骤放在一起
        if (cbIsReport.Checked == false)
        {
            //法务复核意见
            if (!string.IsNullOrEmpty(GetRoleUsers(CompanyCode, "合同法务复核员")))
            {
                LeaderTemp = GetRoleUsers(CompanyCode, "合同法务复核员");
            }
            else
            {
                LeaderTemp = GetRoleUsers(LawDeptCode, "部门负责人");
            }
            XmlElement xmleLawAudit = xmldoc.CreateElement("LawAuditOpinion");
            xmleLeaders.AppendChild(xmleLawAudit);
            xmleLawAudit.SetAttribute("ID", LeaderTemp);
            //印章管理员盖章
            LeaderTemp = GetRoleUsers(CompanyCode, "公章管理员");
            XmlElement xmleSealManager = xmldoc.CreateElement("SealManager");
            xmleLeaders.AppendChild(xmleSealManager);
            xmleSealManager.SetAttribute("ID", LeaderTemp);
            //档案管理员归档
            LeaderTemp = GetRoleUsers(CompanyCode, "档案管理员");
            XmlElement xmleFileManager = xmldoc.CreateElement("FileManager");
            xmleLeaders.AppendChild(xmleFileManager);
            xmleFileManager.SetAttribute("ID", LeaderTemp);
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
    /// 过滤datefield
    /// </summary>
    /// <param name="dataField"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 弹出提示框
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

        //董事长意见
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

        NodeTemp = xmldoc.SelectSingleNode("//EVP");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("EVP", LeaderTemp);

        NodeTemp = xmldoc.SelectSingleNode("//CFO");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("CFO", LeaderTemp);

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
        //法务部意见
        NodeTemp = xmldoc.SelectSingleNode("//LawDeptManager");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("LawDeptManager", LeaderTemp);
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
        //流程审核员审核
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
        //经办部门负责人意见
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
        NodeTemp = xmldoc.SelectSingleNode("//DeptDirectors");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("DeptDirectors", LeaderTemp);

        //法务复核意见
        NodeTemp = xmldoc.SelectSingleNode("//LawAuditOpinion");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("LawAuditOpinion", LeaderTemp);
        //印章管理员盖章
        NodeTemp = xmldoc.SelectSingleNode("//SealManager");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("SealManager", LeaderTemp);
        //档案管理员归档
        NodeTemp = xmldoc.SelectSingleNode("//FileManager");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("FileManager", LeaderTemp);
        //是否上报
        dataFields.Add("IsReport", cbIsReport.Checked ? "1" : "0");
        dataFields.Add("IsPass", "1");
        return dataFields;
    }
    /// <summary>
    /// 保存方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        ContractAuditOfEToGInfo obj = SaveData(id, "00");
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
    /// <summary>
    /// 提交事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        //合同验证
        if (NotifyCompanyAndContractType())
        {
            return;
        }

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

        ContractAuditOfEToGInfo obj = SaveData(id, "02");
        if (obj != null)
        {
            //开启流程
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            AppDict appInfo = new Pkurg.BPM.Services.AppDictService().GetByAppId("3008");
            if (appInfo == null)
            {
                Alert("提交失败");
                return;
            }
            int wfInstanceId = 0; //process instance id
            WorkflowHelper.StartProcess(@"K2Workflow\OA_ContractAuditOfEToG", id, dataFields, ref wfInstanceId);
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
    //验证合同主体【公司验证】以及合同类型
    private bool NotifyCompanyAndContractType()
    {
        if (VerifyCompany() && ddlContractType1.SelectedItem.Text == "采购类合同（房地产开发）")
        {
            StringBuilder sb = new StringBuilder();
            string message = ddlContractSubject.SelectedItem.Text + "已经在ERP上线。“采购类合同（房地产开发）”类型的合同请在ERP系统中提交。谢谢";
            string js = string.Format(@"alert('{0}');", message) + sb.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ajax", js, true);
            return true;
        }
        return false;
    }
    //验证公司【合同主体】
    private bool VerifyCompany()
    {
        DataSet ds = new DataSet();
        string filePath = Server.MapPath("~/Files/xml/ERPLaunchCompany.xml");
        //XmlDocument doc = new XmlDocument();
        //doc.Load(filePath);
        //var node = doc.SelectSingleNode("");
        //return node != null;
        
        ds.ReadXml(filePath);

        if (ds.Tables.Count > 0)
        {
            foreach (DataRow company in ds.Tables[0].Rows)
            {
                if (ddlContractSubject.SelectedItem.Text == company["value"].ToString())
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void Alert(string msg)
    {
        DisplayMessage.ExecuteJs(string.Format("alert('{0}');", msg));
    }
    /// <summary>
    /// 删除终止事件
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
    /// <summary>
    /// 起始部门变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
        InitLeader();
        InitCheckBoxList();
    }
    private string FilterDataField2(StringBuilder dataField)
    {
        return FilterDataField2(dataField.ToString().Trim(','));
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
    /// <summary>
    /// 点击事件1
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlContractType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlContractType2.DataSource = ContractTypeInfosHelper.GetSecondContractTypeInfos(ddlContractType1.SelectedItem.Value);
        ddlContractType2.DataTextField = "value";
        ddlContractType2.DataValueField = "key";
        ddlContractType2.DataBind();
        if (ddlContractType2.Items.Count != 0)
        {
            ddlContractType2.SelectedIndex = 0;
            ddlContractType3.DataSource = ContractTypeInfosHelper.GetThirdContractTypeInfos(ddlContractType2.SelectedItem.Value);
            ddlContractType3.DataTextField = "value";
            ddlContractType3.DataValueField = "key";
            ddlContractType3.DataBind();
        }
    }
    /// <summary>
    /// 点击事件2
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlContractType2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlContractType3.DataSource = ContractTypeInfosHelper.GetThirdContractTypeInfos(ddlContractType2.SelectedItem.Value);
        ddlContractType3.DataTextField = "value";
        ddlContractType3.DataValueField = "key";
        ddlContractType3.DataBind();
    }
}