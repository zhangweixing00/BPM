using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCode.ERP;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_EditPage_E_ERP_ContractApproval
    : UPageBase
{
    public WF_InstructionOfPKURG wf_Instruction = new WF_InstructionOfPKURG();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();


    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string FWDeptCode = System.Configuration.ConfigurationManager.AppSettings["FWDeptCode"];


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //throw new Exception("这有错！！！");
            InitStartDeptment();
            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("ERP_HT_");
                FormTitle = ContractApproval_Common.GetErpFormTitle(this);//设置标题
                StartDeptId = ddlDepartName.SelectedItem.Value;

                if (Pkurg.PWorldBPM.Business.BIZ.ERP.ERP_Common.IsExsitRunFlow(Request["erpFormId"], ERP_WF_T_Name.ERP_ContractApproval))
                {
                    ERP_CallbackResultType resultType = new ContractApproval_Service().NotifyStartAdvance(Request["erpFormId"], true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip_BPM + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
                    return;
                }
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;

                if (!InitFormData(FormId))
                {
                    return;
                }

                SetUserControlInstance();

                Countersign1.CounterSignDeptId = StartDeptId;
            }
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

    private void SetUserControlInstance()
    {
        string instId = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(instId))
        {
            flowRelated.ProcId = instId;
            Countersign1.ProcId = instId;
            Countersign_Group1.ProcId = instId;
            uploadAttachments.ProcId = instId;
            hfInstanceId.Value = instId;
        }
    }

    /// <summary>
    /// 加载表单
    /// </summary>
    private bool InitFormData(string formId)
    {
        try
        {
            ContractApprovalInfo info = ContractApproval.GetModel(formId);
            if (info != null)
            {
                //if (Pkurg.PWorldBPM.Business.BIZ.ERP.ERP_Common.IsExsitRunFlow(info.ErpFormId, ERP_WF_T_Name.ERP_ContractApproval))
                //{
                    
                //    ERP_CallbackResultType resultType = new ContractApproval_Service().NotifyStartAdvance(Request["erpFormId"], true);
                //    new WF_WorkFlowInstance().DeleteWorkFlowInstance(_BPMContext.ProcID);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('BPM已存在正在审批的该单据！'); window.opener=null;window.open('', '_self', '');window.close();", true);
                //    return false;
                //}

                ListItem selectedItem = ddlDepartName.Items.FindByValue(info.StartDeptId);
                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                }

                cblisoverCotract.Checked = info.IsOverContract.Value == 1;
                cbIsReportResource.Checked = info.IsReportToResource.Value == 1;
                cbIsReportFounder.Checked = info.IsReportToFounder.Value == 1;
                StartDeptId = info.StartDeptId;
            }
            ///加载业务数据
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;
    }

    /// <summary>
    /// 保存表单
    /// </summary>
    /// <returns></returns>
    private ContractApprovalInfo SaveFormData()
    {
        //FormId
        ContractApprovalInfo info = null;
        try
        {
            info = ContractApproval.GetModel(FormId);
            if (info == null)
            {
                info = new ContractApprovalInfo()
                {
                    CreateTime = DateTime.Now.ToString(),
                    IsOverContract = cblisoverCotract.Checked ? 1 : 0,
                    IsReportToResource = cbIsReportResource.Checked ? 1 : 0,
                    IsReportToFounder = cbIsReportFounder.Checked ? 1 : 0,
                    StartDeptId = ddlDepartName.SelectedItem.Value,
                    FormID = FormId,
                    ErpFormId = Request["erpFormId"],
                    ErpFormType = "PO",
                    ApproveResult = ""
                };
                ContractApproval.Add(info);
                //必须首次调用
                cbIsReportResource.SaveToDB(FormId, "10109");
            }
            else
            {

                info.IsOverContract = cblisoverCotract.Checked ? 1 : 0;
                info.IsReportToResource = cbIsReportResource.Checked ? 1 : 0;
                info.IsReportToFounder = cbIsReportFounder.Checked ? 1 : 0;
                info.StartDeptId = ddlDepartName.SelectedItem.Value;

                ContractApproval.Update(info);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return info;
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    private NameValueCollection LoadConstDataField()
    {
        //所有DataField：ActJumped,Archiver,AssistLeaders,CEO,CityAssistLeaders,CityCEO,CityChairman,CityLawDeptManager,CityViceLeaders,CounterSignNum,CounterSignUsers,DeptManager,GroupLeaders,IsOverContract,IsPass,LawDeptManager,LawLeaders,Stamper,ViceLeaders
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "1");
        dataFields.Add("IsOverContract", cblisoverCotract.Checked ? "1" : "0");
        dataFields.Add("IsReportToResource", cbIsReportResource.Checked ? "1" : "0");
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    private List<K2_DataFieldInfo> LoadUserDataField()
    {
        //string groupId = "B04-D319";

        string FWDepartCode = BPMHelp.GetDeptIDByOtherIDAndName(StartDeptId, "法务部");
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        //string startDeptId = _BPMContext.CurrentUser.MainDeptId;

        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///已存在dataField：ActJumped,,,,,,,,,CounterSignNum,CounterSignUsers,,,IsOverContract,IsPass,,,,
        ///自动生成
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "DeptManager", IsHaveToExsit = true });
        dfInfos.Add(new K2_DataFieldInfo() { Name = "CounterSignUsers", RoleName = "部门负责人", DeptCode = Countersign1.Result, IsRepeatIgnore = true, IsHaveToExsit = true, OrderId = 5 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = FWDepartCode, RoleName = "部门负责人", Name = "CityLawDeptManager", IsHaveToExsit = true, IsRepeatIgnore = true, OrderId = 4 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign1.Result + string.Format(",{0},{1},", StartDeptId, FWDepartCode), RoleName = "主管助理总裁", Name = "CityAssistLeaders", IsHaveToExsit = false, IsRepeatIgnore = true, OrderId = 3 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign1.Result + string.Format(",{0},{1},", StartDeptId, FWDepartCode), RoleName = "主管副总裁", Name = "CityViceLeaders", IsHaveToExsit = false, IsRepeatIgnore = true, OrderId = 2 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "总裁", Name = "CityCEO", IsHaveToExsit = true, OrderId = 1 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "董事长", Name = "CityChairman", IsHaveToExsit = false });

        //StringBuilder deptsofGroup = new StringBuilder();

        //foreach (var item in (Countersign1.Result + string.Format("{0},", StartDeptId)).Split(',').ToList())
        //{
        //    string deptsofGroupTmp = Workflow_Common.GetRoleDepts(item, "集团主管部门");
        //    if (!deptsofGroup.ToString().Contains(deptsofGroupTmp))
        //    {
        //        deptsofGroup.AppendFormat("{0},", deptsofGroupTmp);
        //    }
        //}
        //string groupLawDept = "B04-D319-S496";

        //dfInfos.Add(new K2_DataFieldInfo() { DeptCode = deptsofGroup.ToString().Trim(','), RoleName = "部门负责人", Name = "GroupLeaders" ,  IsHaveToExsit = true});

        //dfInfos.Add(new K2_DataFieldInfo() { Name = "CounterSignUsers_Group", Result = Countersign_Group1.GetCounterSignUsers(), IsHaveToExsit = true });

        dfInfos.Add(new K2_DataFieldInfo() { Name = "CounterSignUsers_Group", RoleName = "部门负责人", DeptCode = Countersign_Group1.Result, IsHaveToExsit = true });

        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = FWDeptCode, RoleName = "总监", Name = "LawDeptManager" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign_Group1.Result, RoleName = "主管助理总裁", Name = "AssistLeaders" });


        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = "", RoleName = "部门负责人", Name = "LawLeaders" });//集团主管法务领导跳过，代码保留
        //dfInfos.Add(new K2_DataFieldInfo() { DeptCode = groupLawDept, RoleName = "部门负责人", Name = "LawLeaders"});//集团主管法务领导


        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign_Group1.Result, RoleName = "主管副总裁", Name = "ViceLeaders" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = PKURGICode, RoleName = "总裁", Name = "CEO" });
        //dfInfos.Add(new K2_DataFieldInfo() { DeptCode = "", RoleName = "流程发起人", Name = "" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = FWDepartCode, RoleName = "部门负责人", Name = "CityLawDeptManager2" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "公章管理员", Name = "Stamper", IsHaveToExsit = true });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "档案管理员", Name = "Archiver", IsHaveToExsit = true, });


        return dfInfos;
    }
    /// <summary>
    /// 流程成功启动后操作
    /// </summary>



    #region 整合 by star

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

    public string FormTitle
    {
        get
        {
            return ViewState["FormTitle"].ToString();
        }
        set
        {
            ViewState["FormTitle"] = value;
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

    /// <summary>
    /// 设置流程标题
    /// </summary>
    /// <returns></returns>
    private string GetFormTitle()
    {
        return FormTitle;
    }

    /// <summary>
    /// 设置流程参数
    /// </summary>
    /// <returns></returns>
    private NameValueCollection SetWFParams()
    {
        ///常量DataField
        NameValueCollection dataFields = LoadConstDataField();
        if (dataFields == null)
        {
            dataFields = new NameValueCollection();
        }
        //用户DataField
        List<K2_DataFieldInfo> dfInfos = LoadUserDataField();

        dfInfos = dfInfos.OrderBy(x => x.OrderId).ToList();//排序

        #region 用户DataField
        List<string> userList = new List<string>();
        foreach (var item in dfInfos)
        {
            //if (item.Name == "CounterSignUsers")
            //{
            //    List<string> countersigns = item.DeptCode.Split(',').ToList();
            //    countersigns.RemoveAll((x) => { return string.IsNullOrEmpty(x); });

            //    if (countersigns.Count == 0)
            //    {
            //        item.Result = "noapprovers";
            //        continue;
            //    }

            //    foreach (var csDeptId in countersigns)
            //    {
            //        if (!string.IsNullOrEmpty(csDeptId))
            //        {
            //            string manager = Workflow_Common.GetRoleUsers(csDeptId, "部门负责人");
            //            if (string.IsNullOrEmpty(manager))
            //            {
            //                Department countetDept = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(csDeptId);
            //                Alert(countetDept.Remark + "部门负责人尚未配置！");
            //                return null;
            //            }
            //        }
            //    }

            //    continue;
            //}

            if (string.IsNullOrEmpty(item.RoleName) || string.IsNullOrEmpty(item.Name))
            {
                //参数错误
                ExceptionHander.GoToErrorPage("K2DataFieldInfo信息不全");
            }
            if (string.IsNullOrEmpty(item.DeptCode) || string.IsNullOrEmpty(item.DeptCode.Trim(',')))
            {
                dataFields.Add(item.Name, "noapprovers");
                continue;
            }

            string users = "";
            List<string> depts = item.DeptCode.Split(',').ToList();
            foreach (var csDeptId in depts)
            {
                if (!string.IsNullOrEmpty(csDeptId))
                {
                    foreach (var roleNameItem in item.RoleName.Split(',').ToList())
                    {
                        string currentUsers = Workflow_Common.GetRoleUsers(csDeptId, item.RoleName);
                        if (currentUsers == "noapprovers" && item.IsHaveToExsit)
                        {
                            Department countetDept = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(csDeptId);
                            Alert(countetDept.Remark + roleNameItem + "尚未配置！");
                            return null;
                        }
                        else if (currentUsers != "noapprovers" && !users.Trim(',').Split(',').ToList().Contains(currentUsers))
                        {
                            users += "," + currentUsers;
                        }
                    }

                }
            }
            users = users.Trim(',');
            if (string.IsNullOrEmpty(users))
            {
                users = "noapprovers";
            }
            if (users != "noapprovers")
            {
                List<string> currentUsers = users.Split(',').ToList();
                users = users + ",";
                currentUsers.Distinct().ToList().ForEach(x =>
                    {
                        if (userList.Contains(x) && item.IsRepeatIgnore)
                        {
                            users = users.Replace(x + ",", "");
                        }

                        if (item.OrderId > 0)
                        {
                            userList.Add(x);///只有OrderId > 0的才参与去重（OrderId > 0的是去重范围）
                        }

                    });
                if (string.IsNullOrEmpty(users.Trim(',')))
                {
                    users = "noapprovers";
                }
            }
            item.Result = users.Trim(',');
        }

        foreach (var item in dfInfos)
        {
            dataFields.Add(item.Name, item.Result);
        }

        #endregion

        return dataFields;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        var dataInfo = SaveFormData();

        if (dataInfo != null)
        {
            uploadAttachments.SaveAttachment(FormId);

            if (!string.IsNullOrEmpty(SaveWorkFlowInstance("0", null, "")))
            {
                Alert("保存完成");
            }
        }
        else
        {
            Alert("保存失败");
        }
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (!VertifyOverTime())
        {
            return;
        }

        if (!BeforeSubmit())
        {
            return;
        }
        string id = ViewState["FormID"].ToString();

        var dataInfo = SaveFormData();
        Countersign1.SaveData(true);//会签数据保存
        Countersign_Group1.SaveData(true);
        if (dataInfo != null)
        {
            uploadAttachments.SaveAttachment(FormId);
            // Countersign1.SaveAndSubmit();//会签数据保存

            #region 工作流参数
            NameValueCollection dataFields = SetWFParams();
            if (dataFields == null)
            {
                return;
            }
            #endregion


            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            AppDict appInfo = new Pkurg.BPM.Services.AppDictService().GetByAppId("10109");
            if (appInfo == null)
            {
                Alert("提交失败");
                return;
            }
            int wfInstanceId = 0; //process instance id
            WorkflowHelper.StartProcess(appInfo.WorkFlowName, FormId, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                string instId = SaveWorkFlowInstance("1", DateTime.Now, wfInstanceId.ToString());
                if (!string.IsNullOrEmpty(instId))
                {
                    SaveWorkItem();
                    if (!AfterWorkflowStart(wfInstanceId))
                    {
                        return;
                    }
                    IFrameHelper.DownloadLocalFileUrl(instId);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功');window.opener.location.href=window.opener.location.href;", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null;window.open('', '_self', '');window.close();", true);
                    return;
                }
            }
        }

        Alert("提交失败");
    }

    /// <summary>
    /// 0次验证
    /// </summary>
    /// <returns></returns>
    private bool VertifyOverTime()
    {
        string erpFormCode = Request["erpFormId"];
        if (!string.IsNullOrEmpty(_BPMContext.ProcID))
        {
            erpFormCode = ContractApproval.GetModelByInstId(_BPMContext.ProcID).ErpFormId;
        }
        ERP_CallbackResultType resultType = new ContractApproval_Service().VerifyERPWFStatus(erpFormCode);
        if (resultType != ERP_CallbackResultType.调用成功)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('流程已超时，请重新发起流程！'); window.opener=null;window.open('', '_self', '');window.close();", true);
            return false;
        }
        return true;
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
            erpFormCode = ContractApproval.GetModelByInstId(_BPMContext.ProcID).ErpFormId;
        }
        ERP_CallbackResultType resultType = new ContractApproval_Service().NotifyStartAdvance(erpFormCode, false);
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
        ContractApprovalInfo info = ContractApproval.GetModel(FormId);
        ERP_CallbackResultType resultType = new ContractApproval_Service().NotifyStartAdvance(info.ErpFormId, true);
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

    private string SaveWorkFlowInstance(string WfStatus, DateTime? SumitTime, string WfInstanceId)
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
                workFlowInstance.AppId = "10109";
                workFlowInstance.CreateDeptCode = CurrentEmployee.DepartCode;
                workFlowInstance.CreateDeptName = CurrentEmployee.DepartName;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.FormTitle = GetFormTitle();
            }
            else
            {
                isEdit = true;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.AppId = "10109";
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
            flowRelated.ProcId = workFlowInstance.InstanceId;
            Countersign1.ProcId = workFlowInstance.InstanceId;
            Countersign_Group1.ProcId = workFlowInstance.InstanceId;
            Countersign1.SaveData(true);//会签数据保存
            Countersign_Group1.SaveData(true);
        }
        catch (Exception ex)
        {

            throw ex;
        }

        return workFlowInstance == null ? "" : workFlowInstance.InstanceId;
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

    public void Alert(string msg)
    {
        DisplayMessage.ExecuteJs(string.Format("alert('{0}');", msg));
    }

    #endregion
    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
    }
}
