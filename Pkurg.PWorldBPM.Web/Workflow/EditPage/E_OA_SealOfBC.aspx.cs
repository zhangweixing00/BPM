using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_EditPage_E_OA_SealOfBC : UPageBase
{
    public string className = "Workflow_EditPage_E_OA_SealOfBC";

    SealOfBC Eitems = new SealOfBC();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    string GroupCode = System.Configuration.ConfigurationManager.AppSettings["GroupCode"];
    string BCCode = System.Configuration.ConfigurationManager.AppSettings["BCCode"];
    string GroupOfficeCode = System.Configuration.ConfigurationManager.AppSettings["GroupGMDCode"];
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
                FormId = BPMHelp.GetSerialNumber("OA_CYZ_");
                tbReportCode.Text = FormId;
                UpdatedTextBox.Value = DateTime.Now.ToShortDateString();
                tbUserName.Text = CurrentEmployee.EmployeeName;
                tbMobile.Text = CurrentEmployee.MobilePhone;
            }
            InitLeader();
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
            SealOfBCInfo obj = Eitems.Get(ViewState["FormID"].ToString());
            ListItem item = ddlDepartName.Items.FindByValue(obj.DeptCode);
            if (item != null)
            {
                ddlDepartName.SelectedIndex = ddlDepartName.Items.IndexOf(item);
            }
            tbReportCode.Text = obj.FormID;
            tbUserName.Text = obj.UserName;
            tbMobile.Text = obj.Mobile;
            tbTitle.Text = obj.Title;
            tbDateTime.Text = obj.DateTime;
            tbContent.Text = obj.Content;
            UpdatedTextBox.Value = obj.DateTime;
            cblSecurityLevel.SelectedValue = obj.SecurityLevel != null ? obj.SecurityLevel.ToString() : "-1";
            cblUrgenLevel.SelectedValue = obj.UrgenLevel != null ? obj.UrgenLevel.ToString() : "-1";
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
            UploadAttachments1.ProcId = instId;
            hfInstanceId.Value = instId;
        }
    }

    private SealOfBCInfo SaveData(string ID, string wfStatus)
    {
        SaveWFParams();
        SealOfBCInfo obj = null;
        try
        {
            UploadAttachments1.SaveAttachment(ViewState["FormID"].ToString());
            obj = Eitems.Get(ID);
            bool isEdit = false;
            if (obj == null)
            {
                obj = new SealOfBCInfo();
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

    private bool SaveWorkFlowInstance(SealOfBCInfo obj, string WfStatus, DateTime? SumitTime, string WfInstanceId)
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
                workFlowInstance.AppId = "3012";
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
        bool flag = true;//标记

        if (string.IsNullOrEmpty(GetRoleUsers(GroupCode, "总裁")))
        {
            flag = false;
            Alert(Page, "集团总裁尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(BCCode, "总裁")))
        {
            flag = false;
            Alert(Page, "总经理尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(BCCode, "公章管理员")))
        {
            flag = false;
            Alert(Page, "公章管理员尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(GroupOfficeCode, "部门负责人")))
        {
            flag = false;
            Alert(Page, "集团办公室部门负责人尚未配置！");
        }

        if (1 == 1)
        {
            LeaderTemp = GetRoleUsers(GroupCode, "总裁");
            XmlElement xmlePresident = xmldoc.CreateElement("President");
            xmleLeaders.AppendChild(xmlePresident);
            xmlePresident.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }

        if (1 == 1)
        {
            LeaderTemp = GetRoleUsers(BCCode, "总裁");
            XmlElement xmleGeneralManager = xmldoc.CreateElement("GeneralManager");
            xmleLeaders.AppendChild(xmleGeneralManager);
            xmleGeneralManager.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }

        if (GetRoleUsers(StartDeptId, "主管副总裁")!="")
        {
            LeaderTemp = GetRoleUsers(StartDeptId, "主管副总裁");
            XmlElement xmleDirector = xmldoc.CreateElement("Director");
            xmleLeaders.AppendChild(xmleDirector);
            xmleDirector.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }

        if (1 == 1)
        {
            LeaderTemp = GetRoleUsers(StartDeptId, "部门负责人");
            XmlElement xmleDeptManager = xmldoc.CreateElement("DeptManager");
            xmleLeaders.AppendChild(xmleDeptManager);
            xmleDeptManager.SetAttribute("ID", LeaderTemp);
            ApproverList.Add(LeaderTemp);
        }

        if (1 == 1)
        {
            LeaderTemp = GetRoleUsers(GroupOfficeCode, "部门负责人");
            XmlElement xmleGroupOfficeManager = xmldoc.CreateElement("GroupOfficeManager");
            xmleLeaders.AppendChild(xmleGroupOfficeManager);
            xmleGroupOfficeManager.SetAttribute("ID", LeaderTemp);
        }

        if (1 == 1)
        {
            LeaderTemp = GetRoleUsers(GroupCode, "公章管理员");
            XmlElement xmleSealManager = xmldoc.CreateElement("SealManager");
            xmleLeaders.AppendChild(xmleSealManager);
            xmleSealManager.SetAttribute("ID", LeaderTemp);
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

        NodeTemp = xmldoc.SelectSingleNode("//GeneralManager");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("GeneralManager", LeaderTemp);

        NodeTemp = xmldoc.SelectSingleNode("//Director");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("Director", LeaderTemp);

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
        NodeTemp = xmldoc.SelectSingleNode("//GroupOfficeManager");
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("GroupOfficeManager", LeaderTemp);

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

        dataFields.Add("IsPass", "1");
        return dataFields;
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        SealOfBCInfo obj = SaveData(id, "00");
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
        string SaveVerification = SaveWFParams();
        if (string.IsNullOrEmpty(SaveVerification))
        {
            return;
        }
        NameValueCollection dataFields = SetWFParams();
        
        int wfInstanceId = 0; //process instance id
        string id = ViewState["FormID"].ToString();

        SealOfBCInfo obj = SaveData(id, "02");
        if (obj != null)
        {
            //开启流程
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            WorkflowHelper.StartProcess(@"K2Workflow\OA_SealOfBC", id, dataFields, ref wfInstanceId);
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
                }
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
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
        InitLeader();
    }
}
