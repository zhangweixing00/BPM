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
using Pkurg.PWorldBPM.Common.Log;

public partial class Workflow_EditPage_E_JC_BidScaling : UPageBase
{
    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];

    public string className = "Workflow_EditPage_E_JC_BidScaling";

    JC_BidScaling bs = new JC_BidScaling();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

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
                FormId = BPMHelp.GetSerialNumber("ZBDB_");
                tbDateTime.Text = DateTime.Now.ToShortDateString();
            }
            string StartDeptId = ddlDepartName.SelectedItem.Value;
            if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) == PKURGICode)
            {
                trCounterSign.Visible = false;
                lbIsImpowerProject.Visible = false;
                cblIsAccreditByGroup.Visible = false;
            }
            if (ddlDepartName.SelectedItem.Text.Contains("开封"))
            {
                cblFirstLevel.Visible = true;
                cblFirstLevel.SelectedIndex = 0;
            }
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
                ddlDepartName.Items.Add(new ListItem()
                {
                    Text = deptItem.Remark,
                    Value = deptItem.DepartCode
                });
            }
        }
    }

    private void BindFormData()
    {
        string methodName = "BindFormData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            JC_BidScalingInfo obj = bs.GetBidScaling(ViewState["FormID"].ToString());
            ListItem item = ddlDepartName.Items.FindByValue(obj.StartDeptCode);
            if (item != null)
            {
                ddlDepartName.SelectedIndex = ddlDepartName.Items.IndexOf(item);
            }
            tbTitle.Text = obj.Title;
            tbDateTime.Text = obj.DateTime;
            tbContent.Text = obj.Content;
            UpdatedTextBox.Value = obj.EntranceTime;
            tbFirstUnit.Text = obj.FirstUnit;
            tbSecondUnit.Text = obj.SecondUnit;
            cblIsAccreditByGroup.SelectedValue = obj.IsAccreditByGroup != null ? obj.IsAccreditByGroup.ToString() : "-1";
            cblFirstLevel.SelectedValue = obj.FirstLevel != null ? obj.FirstLevel.ToString() : "-1";
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    private void SetUserControlInstance()
    {
        string instId = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(instId))
        {
            FlowRelated1.ProcId = instId;
            Countersign1.ProcId = instId;
            Countersign_Group1.ProcId = instId;
            UploadAttachments1.ProcId = instId;
            hfInstanceId.Value = instId;
        }
    }

    private JC_BidScalingInfo SaveData(string ID, string wfStatus)
    {
        string methodName = "SaveData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);

        JC_BidScalingInfo obj = null;
        try
        {
            UploadAttachments1.SaveAttachment(ViewState["FormID"].ToString());
            obj = bs.GetBidScaling(ID);
            bool isEdit = false;
            if (obj == null)
            {
                obj = new JC_BidScalingInfo();
                obj.FormID = ViewState["FormID"].ToString();
            }
            else
            {
                isEdit = true;
                obj.FormID = ViewState["FormID"].ToString();
                //obj.ApproveStatus = wfStatus;
            }
            
            obj.StartDeptCode = ddlDepartName.SelectedItem.Value.ToString();
            obj.DeptName = ddlDepartName.SelectedItem.Text;
            obj.UserName = _BPMContext.CurrentUser.Name;
            obj.Title = tbTitle.Text;
            obj.DateTime = tbDateTime.Text;
            obj.Content = tbContent.Text;
            obj.EntranceTime = UpdatedTextBox.Value;
            obj.FirstUnit = tbFirstUnit.Text;
            obj.SecondUnit = tbSecondUnit.Text;

            if (cblIsAccreditByGroup.SelectedIndex != -1)
            {
                obj.IsAccreditByGroup = cblIsAccreditByGroup.SelectedValue.ToString();
            }
            if (cblFirstLevel.SelectedIndex != -1)
            {
                obj.FirstLevel = cblFirstLevel.SelectedValue.ToString();
            }
            if (!isEdit)
            {
                bs.InsertBidScaling(obj);
            }
            else
            {
                bs.UpdateBidScaling(obj);
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

    private bool SaveWorkFlowInstance(JC_BidScalingInfo obj, string WfStatus, DateTime? SumitTime, string WfInstanceId)
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
                workFlowInstance.CreateDeptCode = ddlDepartName.SelectedItem.Value.ToString();
                workFlowInstance.CreateDeptName = ddlDepartName.SelectedItem.Text;
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.AppId = "1003";
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
            string StartDeptId = ddlDepartName.SelectedItem.Value;
            if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) != PKURGICode)
            {
                Countersign1.ProcId = workFlowInstance.InstanceId;
                Countersign1.SaveData(true);
                Countersign_Group1.ProcId = workFlowInstance.InstanceId;
                Countersign_Group1.SaveData(true);
            }
            else
            {
                Countersign_Group1.ProcId = workFlowInstance.InstanceId;
                Countersign_Group1.SaveData(true);
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

        string ExecutiveDirector = string.Empty;//执行主任
        String Members = string.Empty;//招标委员会成员
        String Director = string.Empty;//招标委员会主任

        //验证部分步骤的审批人是否尚未配置
        bool flag = true;
        if (string.IsNullOrEmpty(GetRoleUsers(PKURGICode, "执行主任")))
        {
            flag = false;
            Alert(Page, "执行主任尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(CompanyCode, "招标委员会成员")))
        {
            flag = false;
            Alert(Page, "公司招标委员会成员尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(CompanyCode, "招标委员会主任")))
        {
            flag = false;
            Alert(Page, "公司招标委员会主任尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(PKURGICode, "招标委员会成员")))
        {
            flag = false;
            Alert(Page, "集团招标委员会成员尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(PKURGICode, "招标委员会主任")))
        {
            flag = false;
            Alert(Page, "集团招标委员会主任尚未配置！");
        }

        List<string> countersigns = Countersign1.Result.Split(',').ToList();
        List<string> countersigns_group = Countersign_Group1.Result.Split(',').ToList();

        if ((CompanyCode != PKURGICode && cblIsAccreditByGroup.SelectedItem.Value == "1") || CompanyCode == PKURGICode)
        {
            ExecutiveDirector = GetRoleUsers(PKURGICode, "执行主任");
        }
        if (CompanyCode != PKURGICode && cblIsAccreditByGroup.SelectedIndex == 0)
        {
            Members = GetRoleUsers(CompanyCode, cblFirstLevel.SelectedIndex == 0 ? "招标委员会成员(一级)" : "招标委员会成员");
            Director = GetRoleUsers(CompanyCode, "招标委员会主任");
        }
        if (CompanyCode != PKURGICode && cblIsAccreditByGroup.SelectedIndex == 1)
        {
            Members = GetRoleUsers(PKURGICode, "招标委员会成员") + "," + GetRoleUsers(CompanyCode, "招标委员会主任");
            Director = GetRoleUsers(PKURGICode, "招标委员会主任");
        }
        if (CompanyCode == PKURGICode)
        {
            Members = GetRoleUsers(PKURGICode, "招标委员会成员");
            Director = GetRoleUsers(PKURGICode, "招标委员会主任");
        }
        dataFields.Add("CounterSignUsers", FilterDataField(Countersign1.GetCounterSignUsers()));
        dataFields.Add("CounterSignUsers_Group", FilterDataField(Countersign_Group1.GetCounterSignUsers()));
        dataFields.Add("Members", FilterDataField(Members));
        dataFields.Add("Director", FilterDataField(Director));
        dataFields.Add("ExecutiveDirector", FilterDataField(ExecutiveDirector));

        //dataFields.Add("IsGroup", companyCode == "B04-D319"?"yes":"no");
        //dataFields.Add("IsReport", cblIsAccreditByGroup.SelectedIndex == 1?"yes":"no");
        dataFields.Add("IsPass", "1");
        if (!flag)
        {
            dataFields = null;
        }
        return dataFields;
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        JC_BidScalingInfo obj = SaveData(id, "00");
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

        #region 工作流参数
        NameValueCollection dataFields = SetWFParams();
        if (dataFields == null)
        {
            return;
        }
        #endregion
        int wfInstanceId = 0; //process instance id
        string id = ViewState["FormID"].ToString();

        JC_BidScalingInfo obj = SaveData(id, "02");
        if (obj != null)
        {
            //开启流程
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            WorkflowHelper.StartProcess(@"K2Workflow\JC_BidScaling", id, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance(obj, "1", DateTime.Now, wfInstanceId.ToString()))
                {

                    if (bs.UpdateStatus(id, "02"))
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
        string StartDeptId = ddlDepartName.SelectedItem.Value;
        if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) == PKURGICode)
        {
            trCounterSign.Visible = false;
        }
        else
        { 
            Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
            Countersign1.Refresh();
        }
        if (ddlDepartName.SelectedItem.Text.Contains("开封"))
        {
            cblFirstLevel.Visible = true;
        }
        else
        {
            cblFirstLevel.Visible = false;
        }
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
}
