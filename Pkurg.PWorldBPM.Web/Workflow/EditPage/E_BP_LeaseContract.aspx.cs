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
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;

public partial class Workflow_EditPage_E_BP_LeaseContract : UPageBase
{
    public string className = "Workflow_EditPage_E_BP_LeaseContract";

    BP_LeaseContract lc= new BP_LeaseContract();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    ContractClass ContractClass = new ContractClass();

    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string SYDCDeptCode = System.Configuration.ConfigurationManager.AppSettings["SYDCDeptCode"];
    string CWDeptCode = System.Configuration.ConfigurationManager.AppSettings["CWDeptCode"];
    string FWDeptCode = System.Configuration.ConfigurationManager.AppSettings["FWDeptCode"];


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
        string methodName = "Page_Load";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);

        if (!IsPostBack)
        {
            InitDepartName();
            if (ddlDepartName.Items.Count < 1)
            {
                RunJs(this.Page, "alert('您没有发起该流程权限！请联系管理员');window.close();");
                return;
            }

            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(Request.QueryString["id"]);
                if (info != null)
                {
                    ViewState["FormID"] = info.FormId;
                    BindFormData();
                }
            }
            else
            {
                tbReportCode.Text = BPMHelp.GetSerialNumber("BP_");
                ViewState["FormID"] = tbReportCode.Text;
                tbPerson.Text = CurrentEmployee.EmployeeName;
                tbPhone.Text = CurrentEmployee.OfficePhone;
                UpdatedTextBox.Value = DateTime.Now.ToShortDateString();
                if (!string.IsNullOrEmpty(Request.QueryString["URL"]))
                {
                    tbContent.Text = Request.QueryString["URL"];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["TITLE"]))
                {
                    tbTitle.Text = Request.QueryString["TITLE"];
                    
                }
                if (!string.IsNullOrEmpty(Request.QueryString["bizType"]))
                {
                    tbBizType.Text = Request.QueryString["bizType"];
                    if (Request.QueryString["bizType"]=="2")
                    {
                        cblSupplementContract.SelectedValue = "0";
                        cblSupplementContract.Enabled = false;
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["bizId"]))
                {
                    tbBizID.Text = Request.QueryString["bizId"];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["approveFlag"]))
                {
                    tbApproveFlag.Text = Request.QueryString["approveFlag"];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["reason"]))
                {
                    tbReason.Text = Request.QueryString["reason"];
                }
            }
            string StartDeptId = ddlDepartName.SelectedItem.Value;
        }
    }

    protected void InitDepartName()
    {
        if (!IsPostBack)
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
                    ddlDepartName.Items.Add(new ListItem()
                    {
                        Text = deptItem.Remark,
                        Value = deptItem.DepartCode
                    });
                }
            }
        }
    }

    private void BindFormData()
    {
        string methodName = "BindFormData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            BP_LeaseContractInfo obj = lc.GetLeaseContract(ViewState["FormID"].ToString());
            cblSecurityLevel.SelectedValue = obj.SecurityLevel != null ? obj.SecurityLevel.ToString() : "-1";
            cblUrgentLevel.SelectedValue = obj.UrgenLevel != null ? obj.UrgenLevel.ToString() : "-1";
            ListItem item = ddlDepartName.Items.FindByValue(obj.StartDeptCode);
            if (item != null)
            {
                ddlDepartName.SelectedIndex = ddlDepartName.Items.IndexOf(item);
            }

            UpdatedTextBox.Value = ((DateTime)obj.Date).ToString("yyyy-MM-dd");
            tbPerson.Text = obj.UserName;
            tbPhone.Text = obj.Mobile;
            tbTitle.Text = obj.ReportTitle;
            tbReportCode.Text = obj.ReportCode;
            tbReason.Text = obj.Reason;
            tbContent.Text = obj.Url;
            tbRemark.Text = obj.Remark;
            cblDecorationContract.SelectedValue = obj.DecorationContract != null ? obj.DecorationContract.ToString() : "-1";
            cblServiceContract.SelectedValue = obj.ServiceContract != null ? obj.ServiceContract.ToString() : "-1";
            cblCompensationContract.SelectedValue = obj.CompensationContract != null ? obj.CompensationContract.ToString() : "-1";
            cblModificationContract.SelectedValue = obj.ModificationContract != null ? obj.ModificationContract.ToString() : "-1";
            cblSupplementContract.SelectedValue = obj.SupplementContract != null ? obj.SupplementContract.ToString() : "-1";
            cblLesseeContract.SelectedValue = obj.LesseeContract != null ? obj.LesseeContract.ToString() : "-1";

            WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(obj.FormID);
            #region 审批意见框
            ApproveOpinionUC1.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC2.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC3.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC4.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC5.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC6.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC7.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC8.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC9.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC10.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC11.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC12.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC13.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC14.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC15.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC16.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC17.InstanceId = workFlowInstance.InstanceId;
            ApproveOpinionUC18.InstanceId = workFlowInstance.InstanceId;
            #endregion
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    private BP_LeaseContractInfo SaveData(string ID, string wfStatus)
    {
        string methodName = "SaveData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);

        BP_LeaseContractInfo obj = null;
        try
        {
            obj = lc.GetLeaseContract(ID);

            bool isEdit = false;
            if (obj == null)
            {
                obj = new BP_LeaseContractInfo();
                obj.FormID = ViewState["FormID"].ToString();
                obj.ReportCode = ViewState["FormID"].ToString();
                obj.ApproveStatus = wfStatus;
                obj.CreateByUserCode = CurrentEmployee.EmployeeCode;
                obj.CreateAtTime = DateTime.Now;
                obj.CreateByUserName = CurrentEmployee.EmployeeName;
            }
            else
            {
                isEdit = true;
                obj.FormID = ViewState["FormID"].ToString();
                obj.ApproveStatus = wfStatus;
            }
            obj.UpdateByUserCode = CurrentEmployee.EmployeeCode;
            obj.UpdateByUserName = CurrentEmployee.EmployeeName;
            if (cblSecurityLevel.SelectedIndex != -1)
            {
                obj.SecurityLevel = short.Parse(cblSecurityLevel.SelectedValue);
            }
            if (cblUrgentLevel.SelectedIndex != -1)
            {
                obj.UrgenLevel = short.Parse(cblUrgentLevel.SelectedValue);
            }
            if (cblModificationContract.SelectedIndex != -1)
            {
                obj.ModificationContract = short.Parse(cblModificationContract.SelectedValue);
            }
            if (cblSupplementContract.SelectedIndex != -1)
            {
                obj.SupplementContract = short.Parse(cblSupplementContract.SelectedValue);
            }
            if (cblLesseeContract.SelectedIndex != -1)
            {
                obj.LesseeContract = short.Parse(cblLesseeContract.SelectedValue);
            }
            if (cblDecorationContract.SelectedIndex != -1)
            {
                obj.DecorationContract = short.Parse(cblDecorationContract.SelectedValue);
            }
            if (cblServiceContract.SelectedIndex != -1)
            {
                obj.ServiceContract = short.Parse(cblServiceContract.SelectedValue);
            }
            if (cblDecorationContract.SelectedIndex != -1)
            {
                obj.CompensationContract = short.Parse(cblDecorationContract.SelectedValue);
            }
            DateTime date;
            bool flag1 = DateTime.TryParse(UpdatedTextBox.Value, out date);
            if (flag1)
            {
                obj.Date = date;
            }

            obj.UserName = tbPerson.Text;
            obj.StartDeptCode = ddlDepartName.SelectedItem.Value.ToString();
            obj.DeptName = ddlDepartName.SelectedItem.Text;
            obj.Mobile = tbPhone.Text;
            obj.ReportTitle = tbTitle.Text;
            obj.Reason = tbReason.Text;
            obj.Remark = tbRemark.Text;
            if(tbBizType.Text.ToString() != "")
                obj.BizType = int.Parse(tbBizType.Text.ToString());
            if (tbBizID.Text.ToString() != "")
                obj.BizID = int.Parse(tbBizID.Text.ToString());
            if (tbApproveFlag.Text.ToString() != "")
                obj.ApproveFlag = short.Parse(tbApproveFlag.Text.ToString());

            if (tbContent.Text.ToString() != "")
            {
                obj.Url = tbContent.Text.ToString();
            }
            else
            {
                obj.Url = "";
            }

            if (!isEdit)
            {
                lc.InsertLeaseContract(obj);
            }
            else
            {
                lc.UpdateLeaseContract(obj);
            }

        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }

        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
        return obj;
    }

    private bool SaveWorkFlowInstance(BP_LeaseContractInfo obj, string WfStatus, DateTime? SumitTime, string WfInstanceId)
    {
        string methodName = "SaveWorkFlowInstance";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
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
                workFlowInstance.CreateDeptCode = CurrentEmployee.DepartCode;
                workFlowInstance.CreateDeptName = CurrentEmployee.DepartName;
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.AppId = "2001";
            }
            else
            {
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                isEdit = true;
            }
            workFlowInstance.FormId = obj.FormID;
            workFlowInstance.FormTitle = obj.ReportTitle;
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

            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }

        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
        return result;
    }

    protected void Save_Click(object sender, EventArgs e)
    { 
        string id = ViewState["FormID"].ToString();
        BP_LeaseContractInfo obj = SaveData(id, "00");
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
    /// 设置流程参数
    /// </summary>
    /// <returns></returns>
    private NameValueCollection SetWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();

        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();

        //动态获取待定
        string startDeptId = ddlDepartName.SelectedItem.Value;
        Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(startDeptId);
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(startDeptId);
        string EDeptCode = BPMHelp.GetDeptIDByOtherIDAndName(startDeptId, "工程管理部");
        string FDeptCode = BPMHelp.GetDeptIDByOtherIDAndName(startDeptId, "财务管理部");
        string LADeptCode = BPMHelp.GetDeptIDByOtherIDAndName(startDeptId, "法务部");

        DataTable DeptDirector = bfurd.GetSelectRoleUser(startDeptId, "总监");
        DataTable DeptAssiManager = bfurd.GetSelectRoleUser(startDeptId, "助理总经理");
        DataTable DeptManager = bfurd.GetSelectRoleUser(startDeptId, "部门负责人");
        DataTable EngineeringAuditor = bfurd.GetSelectRoleUser(EDeptCode, "租赁合同审核员");
        DataTable EngineeringManager = bfurd.GetSelectRoleUser(EDeptCode, "部门负责人");
        DataTable FinanceAuditor = bfurd.GetSelectRoleUser(FDeptCode, "租赁合同审核员");
        DataTable FinanceManager = bfurd.GetSelectRoleUser(FDeptCode, "部门负责人");
        DataTable LAAuditor = bfurd.GetSelectRoleUser(LADeptCode, "租赁合同审核员");
        DataTable LegalAffairManager = bfurd.GetSelectRoleUser(LADeptCode, "部门负责人");
        DataTable President = bfurd.GetSelectRoleUser(CompanyCode, "总裁");

        DataTable GroupCREManager = bfurd.GetSelectRoleUser(SYDCDeptCode, "部门负责人");
        DataTable GroupFinanceManager = bfurd.GetSelectRoleUser(CWDeptCode, "部门负责人");
        DataTable GroupLADirector = bfurd.GetSelectRoleUser(FWDeptCode, "副总经理");
        DataTable GroupLAManager = bfurd.GetSelectRoleUser(FWDeptCode, "部门负责人");
        DataTable GroupPresident = bfurd.GetSelectRoleUser(PKURGICode, "总裁");

        List<string> VicePresidentList = new List<string>();
        List<string> GroupAPList = new List<string>();
        List<string> GroupVPList = new List<string>();
        VicePresidentList.Add(GetRoleUsers(EDeptCode, "主管副总裁"));
        if (!VicePresidentList.Contains(GetRoleUsers(FDeptCode, "主管副总裁")))
        {
            VicePresidentList.Add(GetRoleUsers(FDeptCode, "主管副总裁"));
        }
        if (!VicePresidentList.Contains(GetRoleUsers(LADeptCode, "主管副总裁")))
        {
            VicePresidentList.Add(GetRoleUsers(LADeptCode, "主管副总裁"));
        }
        GroupAPList.Add(GetRoleUsers(SYDCDeptCode, "主管助理总裁"));
        if (!GroupAPList.Contains(GetRoleUsers(CWDeptCode, "主管助理总裁")))
        {
            GroupAPList.Add(GetRoleUsers(CWDeptCode, "主管助理总裁"));
        }
        if (!GroupAPList.Contains(GetRoleUsers(FWDeptCode, "主管助理总裁")))
        {
            GroupAPList.Add(GetRoleUsers(FWDeptCode, "主管助理总裁"));
        }
        GroupVPList.Add(GetRoleUsers(SYDCDeptCode, "主管副总裁"));
        if (!GroupVPList.Contains(GetRoleUsers(CWDeptCode, "主管副总裁")))
        {
            GroupVPList.Add(GetRoleUsers(CWDeptCode, "主管副总裁"));
        }
        if (!GroupVPList.Contains(GetRoleUsers(FWDeptCode, "主管副总裁")))
        {
            GroupVPList.Add(GetRoleUsers(FWDeptCode, "主管副总裁"));
        }

        //绑定datafields
        bool flag = true;//标记datafields内的变量是否均赋值
        if (DeptDirector != null && DeptDirector.Rows.Count > 0)
        {
            dataFields.Add("DeptDirector", "K2:Founder\\" + DeptDirector.Rows[0]["LoginName"].ToString());
        }
        else
        {
            dataFields.Add("DeptDirector", "noapprovers");
        }
        if (DeptAssiManager != null && DeptAssiManager.Rows.Count > 0)
        {
            dataFields.Add("DeptAssiManager", "K2:Founder\\" + DeptAssiManager.Rows[0]["LoginName"].ToString());
        }
        else
        {
            dataFields.Add("DeptAssiManager", "noapprovers");
        }
        if (DeptManager != null && DeptManager.Rows.Count > 0)
        {
            dataFields.Add("DeptManager", "K2:Founder\\" + DeptManager.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "您所在部门负责人尚未配置！");
        }
        if (EngineeringAuditor != null && EngineeringAuditor.Rows.Count > 0)
        {
            dataFields.Add("EngineeringAuditor", "K2:Founder\\" + EngineeringAuditor.Rows[0]["LoginName"].ToString());
        }
        else
        {
            dataFields.Add("EngineeringAuditor", "noapprovers");
        }
        if (EngineeringManager != null && EngineeringManager.Rows.Count > 0)
        {
            dataFields.Add("EngineeringManager", "K2:Founder\\" + EngineeringManager.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "工程管理部负责人尚未配置！");
        }
        if (FinanceAuditor != null && FinanceAuditor.Rows.Count > 0)
        {
            dataFields.Add("FinanceAuditor", "K2:Founder\\" + FinanceAuditor.Rows[0]["LoginName"].ToString());
        }
        else
        {
            dataFields.Add("FinanceAuditor", "noapprovers");
        }
        if (FinanceManager != null && FinanceManager.Rows.Count > 0)
        {
            dataFields.Add("FinanceManager", "K2:Founder\\" + FinanceManager.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "财务管理部负责人尚未配置！");
        }
        if (LAAuditor != null && LAAuditor.Rows.Count > 0)
        {
            dataFields.Add("LAAuditor", "K2:Founder\\" + LAAuditor.Rows[0]["LoginName"].ToString());
        }
        else
        {
            dataFields.Add("LAAuditor", "noapprovers");
        }
        if (LegalAffairManager != null && LegalAffairManager.Rows.Count > 0)
        {
            dataFields.Add("LegalAffairManager", "K2:Founder\\" + LegalAffairManager.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "法务部负责人尚未配置！");
        }
        if (President != null && President.Rows.Count > 0)
        {
            dataFields.Add("President", "K2:Founder\\" + President.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "公司总裁尚未配置！");
        }
        if (GroupCREManager != null && GroupCREManager.Rows.Count > 0)
        {
            dataFields.Add("GroupCREManager", "K2:Founder\\" + GroupCREManager.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团商业地产管理部负责人尚未配置！");
        }
        if (GroupFinanceManager != null && GroupFinanceManager.Rows.Count > 0)
        {
            dataFields.Add("GroupFinanceManager", "K2:Founder\\" + GroupFinanceManager.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团财务管理部负责人尚未配置！");
        }
        if (GroupLADirector != null && GroupLADirector.Rows.Count > 0)
        {
            dataFields.Add("GroupLADirector", "K2:Founder\\" + GroupLADirector.Rows[0]["LoginName"].ToString());
        }
        else
        {
            dataFields.Add("GroupLADirector", "noapprovers");
        }
        if (GroupLAManager != null && GroupLAManager.Rows.Count > 0)
        {
            dataFields.Add("GroupLAManager", "K2:Founder\\" + GroupLAManager.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团法务部负责人尚未配置！");
        }
        if (GroupPresident != null && GroupPresident.Rows.Count > 0)
        {
            dataFields.Add("GroupPresident", "K2:Founder\\" + GroupPresident.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团总裁尚未配置！");
        }
        dataFields.Add("VicePresident", FilterDataField2(VicePresidentList));
        dataFields.Add("GroupAP", FilterDataField2(GroupAPList));
        dataFields.Add("GroupVP", FilterDataField2(GroupVPList));
        
        dataFields.Add("BizType", tbBizType.Text.ToString());
        dataFields.Add("BizID", tbBizID.Text.ToString());
        if (cblModificationContract.SelectedIndex == 0 || cblSupplementContract.SelectedIndex == 0 || cblLesseeContract.SelectedIndex == 0||
            cblCompensationContract.SelectedIndex == 0 || cblDecorationContract.SelectedIndex == 0 || cblServiceContract.SelectedIndex == 0)
        {
            dataFields.Add("IsReport", "yes" );
        }
        else
        {
            dataFields.Add("IsReport", tbApproveFlag.Text == "1" ? "yes" : "no");
        }
        if (!flag)
        {
            dataFields = null;
        }
        return dataFields;
    }

    protected void Submit_Click(object sender, EventArgs e)
    {

        #region 工作流参数
        NameValueCollection dataFields = SetWFParams();
        if (dataFields == null)
        {
            return;
        }
        #endregion
        int wfInstanceId = 0; //process instance id
        string id = ViewState["FormID"].ToString();

        BP_LeaseContractInfo obj = SaveData(id, "02");
        if (obj != null)
        {
            //开启流程
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            WorkflowHelper.StartProcess(@"K2Workflow\BP_LeaseContract", id, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance(obj, "1", DateTime.Now, wfInstanceId.ToString()))
                {

                    if (lc.UpdateStatus(id, "02"))
                    {
                        string Opinion = "";
                        string ApproveResult = "同意";
                        string OpinionType = "";
                        string IsSign = "0";
                        string DelegateUserName = "";
                        string DelegateUserCode = "";
                        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(obj.FormID);
                        //通知系统
                        string instanceID = workFlowInstance.InstanceId;
                        string url = "http://" + Request.Url.Authority + "/Workflow/ViewPage/V_BP_LeaseContract.aspx?ID=" + instanceID;

                        try
                        {
                            ContractClass.SubmitWorkFlow(url, Convert.ToInt16(tbBizType.Text.ToString()), Convert.ToInt16(tbBizID.Text.ToString()));
                        }
                        catch
                        {
                        }

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

    public string GetUrl()
    {
        return tbContent.Text.ToString();
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

    private string FilterDataField2(List<string> dataFieldList)
    {
        string dataField = "";

        if (dataFieldList.Count!=0)
        {
            foreach (var item in dataFieldList)
            {
                if (item != "")
                {
                    dataField = dataField + item + ",";
                }
            }
        }
        if (dataField == "")
        {
            dataField = "noapprovers";
        }
        return dataField;
    }
}
