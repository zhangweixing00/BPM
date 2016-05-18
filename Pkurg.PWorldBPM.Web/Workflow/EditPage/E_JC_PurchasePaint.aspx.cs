using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_EditPage_E_JC_PurchasePaint : UPageBase
{
    PurchasePaint Eitems = new PurchasePaint();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    string CGDeptCode = System.Configuration.ConfigurationManager.AppSettings["CGDeptCode"];
    string XMYYDeptCode = System.Configuration.ConfigurationManager.AppSettings["XMYYDeptCode"];
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
                FormId = BPMHelp.GetSerialNumber("JC_PP_");
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
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(StartDeptId, "部门负责人");
        DataTable dtPresident = bfurd.GetSelectRoleUser(CompanyCode, "总裁");
        DataTable dtCGDeptManager = bfurd.GetSelectRoleUser(CGDeptCode, "部门负责人");
        DataTable dtDoorManager = bfurd.GetSelectRoleUser(CGDeptCode, "入户门审核员");
        DataTable dtPaintManager = bfurd.GetSelectRoleUser(CGDeptCode, "涂料审核员");
        DataTable dtJJuManager = bfurd.GetSelectRoleUser(CGDeptCode, "洁具审核员");
        DataTable dtProjectDiretor = bfurd.GetSelectRoleUser(XMYYDeptCode, "部门副总经理");
        DataTable dtPurchaseDiretor = bfurd.GetSelectRoleUser(CGDeptCode, "集采复审员");
        DataTable dtGroupLeader = bfurd.GetSelectRoleUser(CGDeptCode, "主管副总裁");
        if (dtDeptManager.Rows.Count != 0)
        {
            lbDeptManager.Text = "(" + dtDeptManager.Rows[0]["EmployeeName"].ToString() + ")审核";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbPresident.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtCGDeptManager.Rows.Count != 0)
        {
            lbGroupDeptManager.Text = "(" + dtCGDeptManager.Rows[0]["EmployeeName"].ToString() + ")审核";
        }
        if (dtDoorManager.Rows.Count != 0)
        {
            cbDoorManager.Text = "选择 " + dtDoorManager.Rows[0]["EmployeeName"].ToString() + "(入户门)";
        }
        if (dtPaintManager.Rows.Count != 0)
        {
            cbPaintManager.Text = "选择 " + dtPaintManager.Rows[0]["EmployeeName"].ToString() + "(涂料)";
        }
        if (dtJJuManager.Rows.Count != 0)
        {
            cbJJuManager.Text = "选择 " + dtJJuManager.Rows[0]["EmployeeName"].ToString() + "(洁具)";
        }
        if (dtProjectDiretor.Rows.Count != 0)
        {
            lbProjectDiretor.Text = "(" + dtProjectDiretor.Rows[0]["EmployeeName"].ToString() + ")审核";
        }
        if (dtPurchaseDiretor.Rows.Count != 0)
        {
            lbPurchaseDiretor.Text = "(" + dtPurchaseDiretor.Rows[0]["EmployeeName"].ToString() + ")审核";
        }
        if (dtGroupLeader.Rows.Count != 0)
        {
            lbGroupLeader.Text = "(" + dtGroupLeader.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
    }

    private void BindFormData()
    {
        try
        {
            PurchasePaintInfo obj = Eitems.Get(ViewState["FormID"].ToString());
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

    private PurchasePaintInfo SaveData(string ID, string wfStatus)
    {
        PurchasePaintInfo obj = null;
        try
        {
            UploadAttachments1.SaveAttachment(ViewState["FormID"].ToString());
            obj = Eitems.Get(ID);
            bool isEdit = false;
            if (obj == null)
            {
                obj = new PurchasePaintInfo();
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

    private bool SaveWorkFlowInstance(PurchasePaintInfo obj, string WfStatus, DateTime? SumitTime, string WfInstanceId)
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
                workFlowInstance.AppId = "2007";
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
    /// 设置流程参数
    /// </summary>
    /// <returns></returns>
    private NameValueCollection SetWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();
        string LeaderTemp;
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        string GroupAuditor="";
        if (cbDoorManager.Checked)
        { 
            GroupAuditor="入户门审核员";
        }
        else if (cbPaintManager.Checked)
        {
            GroupAuditor="涂料审核员";
        }
        else if(cbJJuManager.Checked)
        {
            GroupAuditor = "洁具审核员";
        }

        LeaderTemp = GetRoleUsers(CGDeptCode, "主管副总裁");
        dataFields.Add("GroupLeader", string.IsNullOrEmpty(LeaderTemp) ? "noapprovers" : LeaderTemp);
        LeaderTemp = GetRoleUsers(CGDeptCode, "部门负责人");
        dataFields.Add("GroupDeptManager", string.IsNullOrEmpty(LeaderTemp) ? "noapprovers" : LeaderTemp);
        LeaderTemp = GetRoleUsers(XMYYDeptCode, "部门副总经理");
        dataFields.Add("GroupProjectLeader", string.IsNullOrEmpty(LeaderTemp) ? "noapprovers" : LeaderTemp); 
        LeaderTemp = GetRoleUsers(CGDeptCode, "集采复审员");
        dataFields.Add("GroupDeptLeader", string.IsNullOrEmpty(LeaderTemp) ? "noapprovers" : LeaderTemp);
        LeaderTemp = GetRoleUsers(CGDeptCode, GroupAuditor);
        dataFields.Add("GroupAuditor", string.IsNullOrEmpty(LeaderTemp) ? "noapprovers" : LeaderTemp);
        LeaderTemp = GetRoleUsers(CGDeptCode, "集采复审员");
        dataFields.Add("GroupReviewer", string.IsNullOrEmpty(LeaderTemp) ? "noapprovers" : LeaderTemp);
        LeaderTemp = GetRoleUsers(CompanyCode, "总裁");
        dataFields.Add("President", string.IsNullOrEmpty(LeaderTemp) ? "noapprovers" : LeaderTemp);
        LeaderTemp = GetRoleUsers(StartDeptId, "部门负责人");
        dataFields.Add("DeptManager", string.IsNullOrEmpty(LeaderTemp) ? "noapprovers" : LeaderTemp);
        LeaderTemp = FilterDataField2(Countersign1.GetCounterSignUsers());
        dataFields.Add("CounterSignUsers", string.IsNullOrEmpty(LeaderTemp) ? "noapprovers" : LeaderTemp);

        dataFields.Add("IsPass", "1");
        return dataFields;
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        PurchasePaintInfo obj = SaveData(id, "00");
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
        #region 工作流参数
        NameValueCollection dataFields = SetWFParams();
        if (dataFields == null)
        {
            return;
        }
        #endregion
        
       
        PurchasePaintInfo obj = SaveData(id, "02");
        if (obj != null)
        {
            //开启流程
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            AppDict appInfo = new Pkurg.BPM.Services.AppDictService().GetByAppId("3004");
            if (appInfo == null)
            {
                Alert("提交失败");
                return;
            }
            int wfInstanceId = 0; //process instance id
            WorkflowHelper.StartProcess(@"K2Workflow\JC_PurchasePaint", id, dataFields, ref wfInstanceId);
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
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
        InitLeader();
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
