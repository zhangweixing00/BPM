using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCode.ERP;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Entites.BIZ.ERP;

public partial class Workflow_EditPage_E_ERP_PaymentApplication : UPageBase
{

    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];

    string strApprovers = string.Empty;
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

            ListItem item = ddlDepartName.Items.FindByValue(formDataInfo.StartDeptId);
            if (item != null)
            {
                ddlDepartName.SelectedIndex = ddlDepartName.Items.IndexOf(item);
            }
            cbChairman.Checked = formDataInfo.IsCheckedChairman == 1;

            StartDeptId = formDataInfo.StartDeptId;

            ListItem departItem = ddlDepartName.Items.FindByValue(formDataInfo.StartDeptId);
            if (departItem == null)
            {
                ExceptionHander.GoToErrorPage();
                return;
            }
            departItem.Selected = true;


            //Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(formDataInfo.StartDeptId);
            //tbDepartName.Text = deptInfo.DepartName;
            Countersign1.CounterSignDeptId = formDataInfo.StartDeptId;

            LoadRelationPerson();

            if (!string.IsNullOrEmpty(formDataInfo.LeadersSelected))
            {
                string[] cbDatas = formDataInfo.LeadersSelected.Split(',');
                foreach (var cbItem in cbDatas)
                {
                    ListItem listItem = cbRelatonUsers.Items.FindByValue(cbItem);
                    listItem.Selected = true;
                }
            }

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

    private void LoadRelationPerson()
    {
        StringBuilder showUsers = new StringBuilder();
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        cbRelatonUsers.Items.Clear();
        if (ddlDepartName.Items.Count == 0)
        {
            //showUsers.Append("没有配置相关角色");
            //cbPayer.Enabled = false;
        }
        else
        {

            string startDeptId = ddlDepartName.SelectedItem.Value;
            Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(startDeptId);
            string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(startDeptId);
            DataTable dtDept = bfurd.GetSelectRoleUser(CompanyCode, "付款申请工程师");
            if (dtDept != null && dtDept.Rows.Count != 0)
            {

                foreach (DataRow rowItem in dtDept.Rows)//EmployeeCode
                {
                    //showUsers.AppendFormat("{0},", rowItem["EmployeeName"].ToString());

                    cbRelatonUsers.Items.Add(new ListItem()
                    {
                        Text = rowItem["EmployeeName"].ToString(),
                        Value = rowItem["LoginName"].ToString()
                    });
                }
                // showUsers.Append("选择相关人员意见");
                //cbPayer.Enabled = true;
                //cbPayer.Text = string.Format("相关人员（{0}）", showUsers.ToString().Trim(','));
                //return;
            }
            else
            {
                // showUsers.Append("没有配置相关角色");
                // cbPayer.Enabled = false;
            }
        }
        // cbPayer.Text = showUsers.ToString();
    }

    /// <summary>
    /// 保存表单
    /// </summary>
    /// <returns></returns>
    private PaymentApplicationInfo SaveFormData()
    {
        bool isEdit = false;
        PaymentApplicationInfo info = null;
        try
        {
            info = PaymentApplication.GetPaymentApplicationInfo(FormId);
            if (info == null)
            {
                info = new PaymentApplicationInfo();
                info.FormId = FormId;
                info.ErpFormId = HttpContext.Current.Request["erpFormId"];
                info.ErpFormType = HttpContext.Current.Request["erpFormType"];
                //info.StartDeptId = HttpContext.Current.Request["startDeptId"];
                info.StartDeptId = ddlDepartName.SelectedItem.Value;
                info.IsOverContract = cblisoverCotract.Checked ? 1 : 0;
                info.IsCheckedChairman = cbChairman.Checked ? 1 : 0;
                info.Amount = HttpContext.Current.Request["amount"];
            }
            else
            {
                isEdit = true;
                info.FormId = FormId;
                info.IsOverContract = cblisoverCotract.Checked ? 1 : 0;
                info.StartDeptId = ddlDepartName.SelectedItem.Value;
                info.IsCheckedChairman = cbChairman.Checked ? 1 : 0;
                info.ApproveResult = "";
                //info.ErpFormId = HttpContext.Current.Request["erpFormId"];
                //info.ErpFormType = HttpContext.Current.Request["erpFormType"];
                //info.StartDeptId = HttpContext.Current.Request["startDeptId"];
            }
            StringBuilder cbDatas = new StringBuilder();
            foreach (ListItem item in cbRelatonUsers.Items)
            {
                if (item.Selected)
                {
                    cbDatas.AppendFormat("{0},", item.Value);
                }

            }
            info.LeadersSelected = cbDatas.ToString().Trim(',');

            if (!isEdit)
            {
                PaymentApplication.InsertPaymentApplication(info);
            }
            else
            {
                PaymentApplication.UpdatePaymentApplication(info);
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
        //存储过程的四个参数

        string startDeptId = ddlDepartName.SelectedItem.Value;
        //部门【条件1】
        string startDeptName = ddlDepartName.SelectedItem.Text;
        //定义合同类型的取值
       // string erpFormType = HttpContext.Current.Request["erpFormType"];
        //根据formTitle中的第七位取两位得到合同类型【条件2】
        string type = PaymentApplication_Common.GetErpFormTitle(this).Substring(5,2);
        //得到金额amount，需要先判断改单子是打开拟稿的还是新打开的，根据ID来判断是哪一类【条件3】
        string amounts=null;
        //用info和_BPMContext.ProcID是否为空来判断是打开的拟稿还是新打开的有什么区别

        //如果为null或者为空时，则为新打开的页面，新打开页面的amount值
        if (_BPMContext.ProcID == null || _BPMContext.ProcID=="")
        {
            amounts = HttpContext.Current.Request["amount"];
        }
        //如果不为null,则为打开的拟稿 
        else
        {    
            PaymentApplicationInfo info = PaymentApplication.GetPaymentApplicationInfo(FormId);
            amounts=info.Amount;
        }
        //是否在计划内【条件4】
        string isInPlan = cblIsInPan.SelectedItem.Value;



        NameValueCollection dataFields = new NameValueCollection();
        Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(startDeptId);
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);

        StringBuilder firstFieldBuilder = new StringBuilder();
        foreach (ListItem item in cbRelatonUsers.Items)
        {
            if (item.Selected)
            {
                firstFieldBuilder.AppendFormat("K2:Founder\\{0},", item.Value);
            }
        }
        string firstField = firstFieldBuilder.ToString().Trim(',');
        dataFields.Add("RelatedPersonnel", !string.IsNullOrEmpty(firstField) ? firstField : "noapprovers");

        bool flag = true;//标记datafields内的变量是否均赋值
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
                    Alert(Page, item + "会签部门负责人尚未配置！");
                }
            }
        }
        string financialManagementDepartmentCode = BPMHelp.GetDeptIDByOtherIDAndName(deptInfo.DepartCode, "财务管理部");
        if (string.IsNullOrEmpty(financialManagementDepartmentCode))
        {
            flag = false;
            Alert(Page, "财务管理部尚未设置！");
        }
        else
        {
            string financialManagementDepartmentInfoManager = GetRoleUsers(financialManagementDepartmentCode, "部门负责人");
            if (string.IsNullOrEmpty(financialManagementDepartmentInfoManager))
            {
                flag = false;
                Alert(Page, "财务管理部门负责人尚未配置！");
            }
            //else
            //{
            //    if (string.IsNullOrEmpty(GetRoleUsers(financialManagementDepartmentInfo.DepartCode, "主管副总裁")))
            //    {
            //        flag = false;
            //        Alert(Page, "财务管理部门主管副总裁尚未配置！");
            //    }
            //}
        }
        if (string.IsNullOrEmpty(GetRoleUsers(CompanyCode, "总裁")))
        {
            flag = false;
            Alert(Page, "公司总裁尚未配置！");
        }
        if (string.IsNullOrEmpty(GetRoleUsers(PKURGICode, "总裁")))
        {
            flag = false;
            Alert(Page, "集团总裁尚未配置！");
        }
        StringBuilder leaders = new StringBuilder();
        StringBuilder Viceleaders = new StringBuilder();
        StringBuilder deptsofGroup = new StringBuilder();

        StringBuilder leaderofgroup = new StringBuilder();
        StringBuilder AssistantPresident = new StringBuilder();
        StringBuilder VicePresident = new StringBuilder();

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

        //集团审批人员
        if (cblisoverCotract.Checked)
        {
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
            dataFields.Add("CEOGroup", FilterDataField2(GetRoleUsers(PKURGICode, "总裁")));
            dataFields.Add("VicePresident", FilterDataField2(VicePresident));
            dataFields.Add("AssistantPresident", FilterDataField2(AssistantPresident));
            dataFields.Add("leadersofgroup", FilterDataField2(leaderofgroup));
        }

        if (cbChairman.Checked)
        {
            dataFields.Add("chairman", FilterDataField2(GetRoleUsers(CompanyCode, "董事长")));
        }
        else
        {
            dataFields.Add("chairman", "noapprovers");
        }
        string CQCompanyCode = "S374";
        string CPDeptCode = "S366-S976-S219";
        string JSDeptCode = "S366-S976-S782";
        string SCKFDeptCode = "S366-S976-S860";
        //如果是重庆公司或者是北京北大资源物业公司，则进行判断
        if (StartDeptId.Contains(CQCompanyCode) || StartDeptId.Contains(CPDeptCode) || StartDeptId.Contains(JSDeptCode) || StartDeptId.Contains(SCKFDeptCode))
        {
            //判断“分管副总裁”“分管财务副总裁”“总裁”需要审批还是不需要审批
            int grade = ERP_PaymentApplication_Grade.GetERP_PaymentApplication_GradeInfo(type, startDeptName, isInPlan, amounts);
            //如果grade为别的值，则抛出异常
            if (grade == -1)
            {
                //throw new InvalidDataException("输入的参数不正确！");
                grade = 111;
            }

            //分管财务副总裁审批
            dataFields.Add("financialManagementViceleaders", (grade - (grade / 100 * 100)) / 10 == 1 ? this.FilterDataField2(GetRoleUsers(financialManagementDepartmentCode, "主管副总裁")) : "noapprovers");
            //分管副总裁审批【因为总是会审批，所以不需要进行grade的判断】
            if ((grade / 100) == 1)
            {
                dataFields.Add("Viceleaders", this.FilterDataField2(Viceleaders));
                dataFields.Add("leaders", this.FilterDataField2(leaders));
            }
            //总裁审批
            dataFields.Add("CEO", (grade - (grade / 100 * 100) - ((grade - grade / 100 * 100) / 10 * 10)) == 1 ? this.FilterDataField2(GetRoleUsers(CompanyCode, "总裁")) : "noapprovers");
        }
        else
        {
            //分管财务副总裁审批
            dataFields.Add("financialManagementViceleaders",FilterDataField2(GetRoleUsers(financialManagementDepartmentCode, "主管副总裁")));
            //分管副总裁审批
            dataFields.Add("Viceleaders", this.FilterDataField2(Viceleaders));
            dataFields.Add("leaders", this.FilterDataField2(leaders));
            //总裁审批
            dataFields.Add("CEO",FilterDataField2(GetRoleUsers(CompanyCode, "总裁")));
        }

        dataFields.Add("StandingViceCEO", FilterDataField2(GetRoleUsers(CompanyCode, "常务副总裁")));
        

        dataFields.Add("financialManagement", FilterDataField2(GetRoleUsers(financialManagementDepartmentCode, "部门负责人")));
        dataFields.Add("CounterSignUsers", Countersign1.GetCounterSignUsers());
        dataFields.Add("DeptManager", GetRoleUsers(deptInfo.DepartCode, "部门负责人"));
        dataFields.Add("IsOverContract", cblisoverCotract.Checked ? "1" : "0");
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

    public string StartDeptId
    {
        get
        {
            return ViewState["StartDeptId"].ToString();
        }
        set
        {
            ViewState["StartDeptId"] = value;
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

                SetUserControlInstance();
            }
            else
            {
                string erpFormId = HttpContext.Current.Request["erpFormId"];
                string erpFormType = HttpContext.Current.Request["erpFormType"];

                if (string.IsNullOrEmpty(erpFormId)
               || string.IsNullOrEmpty(erpFormType))
                {
                    //参数错误
                    ExceptionHander.GoToErrorPage();
                    return;
                }
                if (Pkurg.PWorldBPM.Business.BIZ.ERP.ERP_Common.IsExsitRunFlow(Request["erpFormId"], ERP_WF_T_Name.ERP_PaymentApplication))
                {
                    ERP_CallbackResultType resultType = new ContractApproval_Service().NotifyStartAdvance(Request["erpFormId"], true);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip_BPM + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
                    return;
                }

                ContractID = BPMHelp.GetSerialNumber("ERP_FK_");
                FormId = ContractID;
                StartDeptId = ddlDepartName.SelectedItem.Value;
                LoadRelationPerson();
                Countersign1.CounterSignDeptId = StartDeptId;
            }
        }
        //如果起始部门为“营销管理部”，则计划内外的控件隐藏
        if (ddlDepartName.SelectedItem.Text.Contains("营销"))
        {
            //cblIsInPan.Visible = true;
            IsInPan.Visible = true;
        }
        else
        {
            //cblIsInPan.Visible = false; 
            IsInPan.Visible = false;
            cblIsInPan.SelectedItem.Value = "0";
        }
    }

    private void SetUserControlInstance()
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
        FlowRelated1.ProcId = workFlowInstance.InstanceId;
        Countersign1.ProcId = workFlowInstance.InstanceId;
        UploadAttachments1.ProcId = workFlowInstance.InstanceId;
        hfInstanceId.Value = workFlowInstance.InstanceId;
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
                workFlowInstance.AppId = "10105";
                workFlowInstance.CreateDeptCode = CurrentEmployee.DepartCode;
                workFlowInstance.CreateDeptName = CurrentEmployee.DepartName;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.FormTitle = PaymentApplication_Common.GetErpFormTitle(this);
                _BPMContext.ProcID = workFlowInstance.InstanceId;
            }
            else
            {
                isEdit = true;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.AppId = "10105";
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
        PaymentApplicationInfo dataInfo = SaveFormData();

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
        int wfInstanceId = 0; //process instance id
        string id = ViewState["FormID"].ToString();

        if (!BeforeSubmit())
        {
            return;
        }
        PaymentApplicationInfo dataInfo = SaveFormData();
        //Countersign1.SaveData(true);//会签数据保存

        if (dataInfo != null)
        {
            UploadAttachments1.SaveAttachment(FormId);
            // Countersign1.SaveAndSubmit();//会签数据保存

            #region 工作流参数
            NameValueCollection dataFields = SetWFParams();
            if (dataFields == null)
            {
                return;
            }
            #endregion



            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            AppDict appInfo = new Pkurg.BPM.Services.AppDictService().GetByAppId("10105");
            if (appInfo == null)
            {
                Alert("提交失败");
                return;
            }
            WorkflowHelper.StartProcess(appInfo.WorkFlowName, FormId, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance("1", DateTime.Now, wfInstanceId.ToString()))
                {
                    SaveWorkItem();
                    //发布时释放代码
                    if (!AfterWorkflowStart(wfInstanceId))
                    {
                        return;
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功');window.opener.location.href=window.opener.location.href;", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null;window.open('', '_self', '');window.close();", true);

                    return;
                }
            }
        }

        Alert("提交失败");

        //WorkflowHelper.ApproveProcess
        //insert data to business object
    }

    private bool AfterWorkflowStart(int wfInstanceId)
    {
        PaymentApplicationInfo info = PaymentApplication.GetPaymentApplicationInfo(FormId);
        ERP_CallbackResultType resultType = new ERP_PaymentApplication_Service().NotifyStartAdvance(info.ErpFormId, true);
        if (resultType != ERP_CallbackResultType.调用成功)
        {
            //删除流程实例
            new WF_WorkFlowInstance().DeleteWorkFlowInstance(_BPMContext.ProcID);

            //撤销已发起的流程
            WorkflowManage.StopWorkflow(wfInstanceId);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip+ "'); window.opener=null;window.open('', '_self', '');window.close();", true);
            return false;
        }
        return true;
    }

    private bool BeforeSubmit()
    {
        string erpFormCode = Request["erpFormId"];
        if (!string.IsNullOrEmpty(_BPMContext.ProcID))
        {
            erpFormCode = PaymentApplication.GetPaymentApplicationInfoByInstanceId(_BPMContext.ProcID).ErpFormId;
        }
        ERP_CallbackResultType resultType = new ERP_PaymentApplication_Service().NotifyStartAdvance(erpFormCode, false);
        if (resultType != ERP_CallbackResultType.调用成功)
        {
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

    #endregion

    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
        LoadRelationPerson();
    }
    protected void cbPayer_CheckedChanged(object sender, EventArgs e)
    {
        cbRelatonUsers.Visible = cbPayer.Checked;
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
        else
        {
            DisplayMessage.ExecuteJs("window.close();");
        }
    }
}
