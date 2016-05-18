using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using AppCode.ERP;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Manage;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Entites.BIZ.ERP;

public partial class Workflow_EditPage_E_ERP_Instruction : UPageBase
{
    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];


    //string strApprovers = lbApprovers.Text.ToString();
    string strApprovers = string.Empty;
    /// <summary>
    /// 加载表单
    /// </summary>
    private void InitFormData()
    {
        try
        {
            InstructionInfo formDataInfo = Instruction.GetInstructionInfo(FormId);

            ListItem item = ddlDepartName.Items.FindByValue(formDataInfo.StartDeptId);
            if (item != null)
            {
                ddlDepartName.SelectedIndex = ddlDepartName.Items.IndexOf(item);
            }
            cbChairman.Checked = formDataInfo.IsCheckedChairman == 1;
            ListItem departItem = ddlDepartName.Items.FindByValue(formDataInfo.StartDeptId);
            if (departItem == null)
            {
                ExceptionHander.GoToErrorPage();
                return;
            }
            departItem.Selected = true;
            Countersign1.CounterSignDeptId = formDataInfo.StartDeptId;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitStartDeptment()
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

    /// <summary>
    /// 保存表单
    /// </summary>
    /// <returns></returns>
    private InstructionInfo SaveFormData()
    {
        bool isEdit = false;
        InstructionInfo info = null;
        try
        {
            info = Instruction.GetInstructionInfo(FormId);
            if (info == null)
            {
                info = new InstructionInfo();
                info.FormId = FormId;
                info.ErpFormId = HttpContext.Current.Request["erpFormId"];
                info.ErpFormType = HttpContext.Current.Request["erpFormType"];
                //info.StartDeptId = HttpContext.Current.Request["startDeptId"];
                info.StartDeptId = ddlDepartName.SelectedItem.Value;
                info.IsCheckedChairman = cbChairman.Checked ? 1 : 0;
            }
            else
            {
                isEdit = true;
                info.FormId = FormId;
                info.StartDeptId = ddlDepartName.SelectedItem.Value;
                info.ApproveResult = "";
                info.IsCheckedChairman = cbChairman.Checked ? 1 : 0;
                //info.ErpFormId = HttpContext.Current.Request["erpFormId"];
                //info.ErpFormType = HttpContext.Current.Request["erpFormType"];
                //info.StartDeptId = HttpContext.Current.Request["startDeptId"];
            }
            if (!isEdit)
            {
                Instruction.InsertInstructionInfo(info);
            }
            else
            {
                Instruction.UpdateInstruction(info);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return info;
    }

    /// <summary>
    /// 设置流程参数
    /// </summary>
    /// <returns></returns>
    private NameValueCollection SetWFParams()
    {
        string startDeptId = ddlDepartName.SelectedItem.Value;

        NameValueCollection dataFields = new NameValueCollection();
        Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(startDeptId);
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(startDeptId);

        bool flag = true;//标记datafields内的变量是否均赋值
        List<string> ApproverList = new List<string>();//所有参与审批的用户列表
        StringBuilder leaders = new StringBuilder();//相关部门主管助理总裁
        StringBuilder Viceleaders = new StringBuilder();//相关部门主管副总裁
        StringBuilder deptsofGroup = new StringBuilder();//集团相关部门
        StringBuilder leaderofgroup = new StringBuilder();//集团主管部门部门负责人
        StringBuilder AssistantPresident = new StringBuilder();//集团主管部门主管助理总裁
        StringBuilder VicePresident = new StringBuilder();//集团主管部门主管副总裁

        //验证部分步骤的审批人是否尚未配置
        if (string.IsNullOrEmpty(GetRoleUsers(deptInfo.DepartCode, "部门负责人")))
        {
            flag = false;
            Alert(Page, "发起部门负责人尚未配置！");
        }
        List<string> countersigns = Countersign1.Result.Split(',').ToList();
        foreach (var item in countersigns)
        {
            if (!string.IsNullOrEmpty(item))
            {
                if (string.IsNullOrEmpty(GetRoleUsers(item, "部门负责人")))
                {
                    flag = false;
                    Department countetDept = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(item);
                    Alert(Page, countetDept.Remark + "部门负责人尚未配置！");
                }
            }
        }
        if (string.IsNullOrEmpty(GetRoleUsers(CompanyCode, "总裁")))
        {
            flag = false;
            Alert(Page, "公司总裁尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(PKURGICode, "总裁")))
        {
            flag = false;
            Alert(Page, "集团CEO尚未配置！");
        }

        countersigns.Add(startDeptId);
        foreach (var item in countersigns)
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
        ///集团人员
        string IsReport = HttpContext.Current.Request["isReport"];
        if (IsReport == "Y")
        {
            dataFields.Add("CEOGroup", FilterDataField2(GetRoleUsers(PKURGICode, "总裁")));
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
            dataFields.Add("VicePresident", FilterDataField2(VicePresident));
            dataFields.Add("AssistantPresident", FilterDataField2(AssistantPresident));
            dataFields.Add("leadersofgroup", FilterDataField2(leaderofgroup));
        }
        //城市公司审批人员
        string LeaderTemp = string.Empty;
        if (cbChairman.Checked)
        {
            dataFields.Add("chairman", FilterDataField2(GetRoleUsers(CompanyCode, "董事长")));
        }
        else
        {
            dataFields.Add("chairman", "noapprovers");
        }

        dataFields.Add("CEO", FilterDataField2(GetRoleUsers(CompanyCode, "总裁")));
        dataFields.Add("StandingViceCEO", FilterDataField2(GetRoleUsers(CompanyCode, "常务副总裁")));
        dataFields.Add("Viceleaders", FilterDataField2(Viceleaders));
        dataFields.Add("leaders", FilterDataField2(leaders));
        dataFields.Add("CounterSignUsers", (Countersign1.GetCounterSignUsers()));
        dataFields.Add("DeptManager", GetRoleUsers(deptInfo.DepartCode, "部门负责人"));

        dataFields.Add("IsReport", IsReport == "Y" ? "1" : "0");
        dataFields.Add("IsPass", "1");

        if (!flag)
        {
            dataFields = null;
        }
        return dataFields;
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
    /// 支持单个审批人，多个审批人
    /// </summary>
    /// <param name="dataField"></param>
    /// <returns></returns>
    private string FilterDataField2(string dataField)
    {
        dataField = dataField.Trim(',');

        //单个审批人
        if (!dataField.Contains(','))
        {
            if (!string.IsNullOrEmpty(dataField) && strApprovers.Contains(dataField + ","))
            {
                dataField = "noapprovers";
            }

            if (string.IsNullOrEmpty(dataField))
            {
                dataField = "noapprovers";
            }
            else
            {
                if (dataField != "noapprovers")
                {
                    strApprovers = strApprovers + dataField + ",";
                }
            }
        }
        else
        {
            //多个审批人

            //多个审批人过滤完的字符串
            string nowApprovers = "";
            //多个审批人过滤前的集合
            List<string> nowApproverList = dataField.Split(',').ToList();
            foreach (var item in nowApproverList)
            {
                if (!strApprovers.Contains(item + ","))
                {
                    nowApprovers = nowApprovers + item + ",";

                    strApprovers = strApprovers + item + ",";
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

    #region 整合--by zwx

    public string ContractID = null;

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

    public WF_InstructionOfPKURG wf_Instruction = new WF_InstructionOfPKURG();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitStartDeptment();
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(Request.QueryString["id"]);
                FormId = info.FormId;
                InitFormData();
                //Countersign1.CounterSignDeptId = info.CreateDeptCode;
                SetUserControlInstance();
            }
            else
            {
                string erpFormId = HttpContext.Current.Request["erpFormId"];
                string erpFormType = HttpContext.Current.Request["erpFormType"];

                if (string.IsNullOrEmpty(erpFormId) || string.IsNullOrEmpty(erpFormType))
                {
                    //参数错误
                    ExceptionHander.GoToErrorPage();
                    return;
                }
                if (Pkurg.PWorldBPM.Business.BIZ.ERP.ERP_Common.IsExsitRunFlow(Request["erpFormId"], ERP_WF_T_Name.ERP_Instruction))
                {
                    ERP_CallbackResultType resultType = new ContractApproval_Service().NotifyStartAdvance(Request["erpFormId"], true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip_BPM + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
                    return;
                }
                FormId = BPMHelp.GetSerialNumber("ERP_QS_");
                string StartDeptId = ddlDepartName.SelectedItem.Value;
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(StartDeptId);
                Countersign1.CounterSignDeptId = StartDeptId;
            }
            InitLeader();
        }
    }

    private void SetUserControlInstance()
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
        FlowRelated1.ProcId = workFlowInstance.InstanceId;
        Countersign1.ProcId = workFlowInstance.InstanceId;
        UploadAttachments1.ProcId = workFlowInstance.InstanceId;
        hfInstanceId.Value = workFlowInstance.InstanceId;
        #region 审批意见框
        ApproveOpinionUCDeptleader.InstanceId = workFlowInstance.InstanceId;
        ApproveOpinionUCRealateDept.InstanceId = workFlowInstance.InstanceId;
        ApproveOpinionUCLeader.InstanceId = workFlowInstance.InstanceId;
        ApproveOpinionUCCEO.InstanceId = workFlowInstance.InstanceId;
        #endregion
    }

    private bool SaveWorkFlowInstance(string WfStatus, DateTime? SumitTime, string WfInstanceId)
    {
        bool result = false;
        WorkFlowInstance workFlowInstance = null;
        try
        {
            workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
            bool isEdit = false;
            if (workFlowInstance == null)
            {
                workFlowInstance = new WorkFlowInstance();
                workFlowInstance.InstanceId = Guid.NewGuid().ToString();
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.AppId = "10107";
                workFlowInstance.CreateDeptCode = CurrentEmployee.DepartCode;
                workFlowInstance.CreateDeptName = CurrentEmployee.DepartName;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.FormTitle = Instruction_Common.GetErpFormTitle(this);
            }
            else
            {
                isEdit = true;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.AppId = "10107";
            }
            workFlowInstance.FormId = FormId;
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
            Countersign1.SaveData(true);//会签数据保存
        }
        catch (Exception ex)
        {

            throw ex;
        }

        return result;
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        InstructionInfo dataInfo = SaveFormData();

        if (dataInfo != null)
        {
            UploadAttachments1.SaveAttachment(FormId);

            if (SaveWorkFlowInstance("0", null, ""))
            {
                Alert("保存完成");
            }
        }
        else
        {
            Alert("保存失败");
        }
    }

    public void Alert(string msg)
    {
        DisplayMessage.ExecuteJs(string.Format("alert('{0}');", msg));
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["BackUrl"].ToString(), false);
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        if (!BeforeSubmit())
        {
            return;
        }

        int wfInstanceId = 0; //process instance id
        string id = ViewState["FormID"].ToString();

        InstructionInfo dataInfo = SaveFormData();
        Countersign1.SaveData(true);//会签数据保存

        if (dataInfo != null)
        {
            UploadAttachments1.SaveAttachment(FormId);
            Countersign1.SaveAndSubmit();//会签数据保存

            #region 工作流参数
            NameValueCollection dataFields = SetWFParams();
            if (dataFields == null)
            {
                return;
            }
            #endregion
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            WorkflowHelper.StartProcess(@"K2Workflow\ERP_Instruction", FormId, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance("1", DateTime.Now, wfInstanceId.ToString()))
                {
                    SaveWorkItem();
                    if (!AfterWorkflowStart(wfInstanceId))
                    {
                        return;
                    }
                    DisplayMessage.ExecuteJs("alert('提交成功');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                    return;
                }
            }
        }

        Alert("提交失败");

        //WorkflowHelper.ApproveProcess
        //insert data to business object
    }
    /// <summary>
    /// 一次验证：提交流程之前验证
    /// </summary>
    /// <returns></returns>
    private bool BeforeSubmit()
    {
        string erpFormCode = Request["erpFormId"];
        if (!string.IsNullOrEmpty(_BPMContext.ProcID))
        {
            erpFormCode = Instruction.GetInstructionInfoByInstanceId(_BPMContext.ProcID).ErpFormId;
        }
        ERP_CallbackResultType resultType = new ERP_Instruction_Service().NotifyStartAdvance(erpFormCode, false);
        if (resultType != ERP_CallbackResultType.调用成功)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
            return false;
        }
        return true;
    }
    /// <summary>
    /// 二次验证：提交完成之后通知并验证
    /// </summary>
    /// <returns></returns>
    private bool AfterWorkflowStart(int wfInstanceId)
    {
        InstructionInfo info = Instruction.GetInstructionInfo(FormId);
        ERP_CallbackResultType resultType = new ERP_Instruction_Service().NotifyStartAdvance(info.ErpFormId, true);
        if (resultType != ERP_CallbackResultType.调用成功)
        {
            //删除流程实例
            new WF_WorkFlowInstance().DeleteWorkFlowInstance(_BPMContext.ProcID);

            //撤销已发起的流程
            WorkflowManage.StopWorkflow(wfInstanceId);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
            return false;
        }
        return true;
    }

    private void SaveWorkItem()
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);

        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
        {
             ApprovalID = Guid.NewGuid().ToString(),

           FormID = FormId,
            InstanceID = workFlowInstance.InstanceId,
            Opinion = "",
            ApproveAtTime = DateTime.Now,
            ApproveResult = "",//开始
            OpinionType = "",
            CurrentActiveName = "拟稿",
 ISSign = "0",

            DelegateUserName = "",
            DelegateUserCode = "",
            CreateAtTime = DateTime.Now,
            UpdateAtTime = DateTime.Now,
            FinishedTime = DateTime.Now,
            ApproveByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
            ApproveByUserName = _BPMContext.CurrentPWordUser.EmployeeName
        };

        new BFApprovalRecord().AddApprovalRecord(appRecord);
    }

    protected void InitLeader()
    {
        #region 工作流参数
        //get users by role
        BFEmployee employee = new BFEmployee();
        string currentUser = Page.User.Identity.Name.ToLower().Replace(@"founder\", "");
        EmployeeAdditional employeeadd = employee.GetEmployeeAdditionalByLoginName(currentUser);
        Employee em = employee.GetEmployeeByEmployeeCode(employeeadd.EmployeeCode);//get user info

        //get activity destination users
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDept = bfurd.GetSelectRoleUser(em.DepartCode, "部门负责人");
        DataTable dtCheck = bfurd.GetSelectRoleUser(em.CompanyCode, "流程审核人");
        DataTable dtlead = bfurd.GetSelectRoleUser(em.DepartCode, "主管领导");
        DataTable dtCEO = bfurd.GetSelectRoleUser(em.CompanyCode, "CEO");

        #endregion
    }

    protected string SaveSelectedSignLeader()
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmleLeaders = xmldoc.CreateElement("Leaders");
        xmldoc.AppendChild(xmleLeaders);

        //从“相关部门分管领导”到“集团董事长审批”中的步骤的所有参与审批的用户列表
        List<string> ApproverList = new List<string>();
        string LeaderTemp = string.Empty;
        string FinalLeaderTemp = string.Empty;

        BFEmployee employee = new BFEmployee();
        string currentUser = Page.User.Identity.Name.ToLower().Replace(@"founder\", "");
        EmployeeAdditional employeeadd = employee.GetEmployeeAdditionalByLoginName(currentUser);
        Employee em = employee.GetEmployeeByEmployeeCode(employeeadd.EmployeeCode);//get user info

        //get activity destination users
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDept = bfurd.GetSelectRoleUser(em.DepartCode, "部门负责人");

        return xmleLeaders.OuterXml;
    }

    #endregion

    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
    }

    void Alert(Page page, object message)
    {
        StringBuilder sb = new StringBuilder();
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message) + sb.ToString();
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        if (_BPMContext.ProcInst != null)
        {
            new WF_WorkFlowInstance().UpdateNowStatusByFormID(FormId, "5");
            DisplayMessage.ExecuteJs("alert('操作成功'); window.close();");
        }
    }
}
