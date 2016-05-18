using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_EditPage_E_JC_TenderSpecialItem : UPageBase
{
    public WF_InstructionOfPKURG wf_Instruction = new WF_InstructionOfPKURG();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string CGDeptCode = System.Configuration.ConfigurationManager.AppSettings["CGDeptCode"];

    #region 整合 by star
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
    /// <summary>
    /// 起始部门ID
    /// </summary>
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
    #endregion
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Page_Load(object sender, EventArgs e)
    {
        //防止二次提交
        ScriptManager.RegisterOnSubmitStatement(Page, typeof(Page), "Go_disabled", "disabledButton('lbAgree');disabledButton('lbReject');disabledButton('lbSubmit');");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Clear_disabled", "enableButton('lbAgree');enableButton('lbReject');enableButton('lbSubmit');", true);

        if (!IsPostBack)
        {
            //初始化起始部门
            InitStartDeptment();

            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                //得到表单编号
                FormId = BPMHelp.GetSerialNumber("ZBSQ_");
                //得到起始部门列表
                StartDeptId = ddlDepartName.SelectedItem.Value;
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(StartDeptId);
                //根据登录名得到申请人的姓名，电话，以及当前时间
                tbDateTime.Text = DateTime.Now.ToString();
                tbUserName.Text = _BPMContext.CurrentPWordUser.EmployeeName;
                if (_BPMContext.CurrentPWordUser.MobilePhone == null)
                {
                    tbMobile.Text = "";
                }
                else
                {
                    tbMobile.Text = _BPMContext.CurrentPWordUser.MobilePhone;
                }

                tbReportCode.Text = FormId;
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                //根据FormId初始化表单数据
                InitFormData(FormId);
                //设置用户控制实例
                SetUserControlInstance();
                //会签 
                Countersign1.CounterSignDeptId = StartDeptId;
            }
            if (ddlDepartName.SelectedItem.Text.Contains("开封"))
            {
                cblFirstLevel.Visible = true;
            }
        }
    }

    /// <summary>
    /// 初始化用户控件
    /// </summary>
    private void SetUserControlInstance()
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
        string instrId = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(instrId))
        {
            FlowRelated1.ProcId = instrId;
            Countersign1.ProcId = instrId;
            Countersign_Group1.ProcId = instrId;
            UploadAttachments1.ProcId = instrId;
            hfInstanceId.Value = instrId;
        }
    }

    /// <summary>
    /// 加载表单    
    /// </summary>
    /// <param name="FormId"></param>
    private void InitFormData(string formId)
    {
        try
        {
            //参照例子中的getmodel检查自己所写的存储过程是否正确
            JC_TenderSpecialItemInfo info = JC_TenderSpecialItem.GetJC_TenderSpecialItemInfoByFormID(FormId);
            if (info != null)
            {
                ListItem selectItem = ddlDepartName.Items.FindByValue(info.StartDeptId);
                if (selectItem != null)
                {
                    selectItem.Selected = true;
                }
                //加载业务数据
                tbReportCode.Text = info.FormID;
                cblSecurityLevel.SelectedIndex = int.Parse(info.SecurityLevel);
                cblUrgenLevel.SelectedIndex = int.Parse(info.UrgenLevel);
                StartDeptId = info.StartDeptId;
                tbDateTime.Text = info.Date;
                tbUserName.Text = info.UserName;
                tbMobile.Text = info.Tel;
                cblIsImpowerProject.SelectedIndex = int.Parse(info.IsAccreditByGroup);
                tbTitle.Text = info.Title;
                tbContent.Text = info.Substance;
                tbRemark.Text = info.Remark;
            }
            else
            {
                tbUserName.Text = _BPMContext.CurrentPWordUser.EmployeeName;
                tbMobile.Text = _BPMContext.CurrentPWordUser.MobilePhone;
                tbDateTime.Text = DateTime.Now.ToString();
                tbReportCode.Text = FormId;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 初始化发起部门
    /// </summary>
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
            //初始化发起人所属“部门成员”的部门
            //首先初始化发起人所在部门，然后在加上发起人所属“部门成员”的部门，需要去重
            //解决了一个系统的bug[有个人，他的所在部门A“部门成员”为空，他是部门B的“部门成员“，部门A没有会签部门，部门B有，
            //初始化会签部门时，默认初始化他的所在部门A，发起部门初始化成部门B，这样导致他发流程没有会签部门可以勾选]
            foreach (Department DeptItem in deptInfo2)
            {
                if (deptInfo.DepartCode != DeptItem.DepartCode)
                {
                    ListItem item = new ListItem()
                    {
                        Text = DeptItem.Remark,
                        Value = DeptItem.DepartCode
                    };
                    ddlDepartName.Items.Add(item);
                }
            }
        }
    }

    /// <summary>
    /// 保存事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        //工作流参数
        NameValueCollection dataFields = SetWFParams();
        if (dataFields == null)
        {
            return;
        }
        JC_TenderSpecialItemInfo dataInfo = SaveFormData();

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
    /// <summary>
    /// 提交事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        var dataInfo = SaveFormData();
        //如何设置
        Countersign1.SaveData(true);//会签数据保存
        Countersign_Group1.SaveData(true);

        if (dataInfo != null)
        {
            UploadAttachments1.SaveAttachment(FormId);

            #region 工作流参数
            NameValueCollection dataFields = SetWFParams();
            if (dataFields == null)
            {
                return;
            }
            #endregion

            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            AppDict appInfo = new Pkurg.BPM.Services.AppDictService().GetByAppId("2006");
            if (appInfo == null)
            {
                Alert("提交失败");
                return;
            }
            int wfInstanceId = 0; //process instance id
            WorkflowHelper.StartProcess(appInfo.WorkFlowName, FormId, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance("1", DateTime.Now, wfInstanceId.ToString()))
                {
                    //保存工作流条目
                    SaveWorkItem();
                    DisplayMessage.ExecuteJs("alert('提交成功');");
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功');window.opener.location.href=window.opener.location.href;", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                    return;
                }
            }
        }

        Alert("提交失败");
    }

    /// <summary>
    /// 保存表单
    /// </summary>
    /// <returns></returns>
    private JC_TenderSpecialItemInfo SaveFormData()
    {
        //FormId
        JC_TenderSpecialItemInfo info = null;

        try
        {
            //检查存储过程是否正确
            info = JC_TenderSpecialItem.GetJC_TenderSpecialItemInfoByFormID(FormId);
            if (info == null)
            {
                info = new JC_TenderSpecialItemInfo()
                {
                    FormID = FormId,
                    SecurityLevel = cblSecurityLevel.SelectedIndex.ToString(),
                    UrgenLevel = cblUrgenLevel.SelectedIndex.ToString(),
                    StartDeptId = ddlDepartName.SelectedItem.Value,
                    DeptName = ddlDepartName.SelectedItem.Text,
                    Date = DateTime.Now.ToString(),
                    UserName = _BPMContext.CurrentPWordUser.EmployeeName,
                    Tel = _BPMContext.CurrentPWordUser.MobilePhone,
                    IsAccreditByGroup = cblIsImpowerProject.SelectedIndex.ToString(),
                    Title = tbTitle.Text,
                    Substance = tbContent.Text,
                    Remark = tbRemark.Text,
                };
                //插入新的表单数据
                JC_TenderSpecialItem.InsertJC_TenderSpecialItemInfo(info);
            }
            else
            {
                info.StartDeptId = ddlDepartName.SelectedItem.Value;
                info.DeptName = ddlDepartName.SelectedItem.Text;
                info.SecurityLevel = cblSecurityLevel.SelectedIndex.ToString();
                info.UrgenLevel = cblUrgenLevel.SelectedIndex.ToString();
                info.IsAccreditByGroup = cblIsImpowerProject.SelectedIndex.ToString();
                //更新表单数据
                JC_TenderSpecialItem.UpdateJC_TenderSpecialItemInfo(info);
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
        //常量DataField
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
            if (string.IsNullOrEmpty(item.Result))
            {
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
                            string currentUsers = Workflow_Common.GetRoleUsers(csDeptId, roleNameItem);
                            if (currentUsers == "noapprovers" && item.IsHaveToExsit)
                            {
                                Department counterDept = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(csDeptId);
                                Alert(counterDept.Remark + roleNameItem + "尚未配置！");
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
                    currentUsers.ForEach(x =>
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
        }
        foreach (var item in dfInfos)
        {
            dataFields.Add(item.Name, item.Result);
        }
        #endregion

        return dataFields;
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    private NameValueCollection LoadConstDataField()
    {
        //DataField，控制流程流转，因业务而添加的DataField
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "1");
        dataFields.Add("IsReportToResource", cblIsImpowerProject.SelectedItem.Value);
        return dataFields;
    }

    /// <summary>
    /// 设置变量型[用户型]DataField
    /// </summary>
    /// <returns></returns>
    private List<K2_DataFieldInfo> LoadUserDataField()
    {
        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        //起始部门
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "StartDeptManager", IsHaveToExsit = true });
        //相关部门
        dfInfos.Add(new K2_DataFieldInfo() { Name = "CounterSignUsers", RoleName = "部门负责人", DeptCode = Countersign1.Result, IsRepeatIgnore = true, IsHaveToExsit = true});
        //集团相关部门
        dfInfos.Add(new K2_DataFieldInfo() { Name = "CounterSignUsers_Group", RoleName = "部门负责人", DeptCode = Countersign_Group1.Result, IsHaveToExsit = true });
        //执行主任，如果是集团授权，则执行主任为noapprovers，反之则为ExecutiveDirector
        if (cblIsImpowerProject.SelectedItem.Value == "1")
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "ExecutiveDirector", IsHaveToExsit = false });
        }
        else
        {
            string directors = Workflow_Common.GetRoleUsersWithNoApproval(PKURGICode, "执行主任").Trim(',');
            dfInfos.Add(new K2_DataFieldInfo() { Result = Workflow_Common.FilterDataField(directors), Name = "ExecutiveDirector", IsHaveToExsit = true });
        }
        //招标委员会成员和招标委员会主任(集团授权，需要判断是一级开发还是二级开发)
        if (cblFirstLevel.SelectedIndex == 0)
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "招标委员会成员(一级)", Name = "TenderCommitteeManager", IsHaveToExsit = true });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "招标委员会成员", Name = "TenderCommitteeManager", IsHaveToExsit = true });
        }
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "招标委员会主任", Name = "TenderCommitteeChairman", IsHaveToExsit = true });
       
        //集团招标委员会成员和集团招标委员会主任(非集团授权项目)

        string leaders = Workflow_Common.GetRoleUsersWithNoApproval(CompanyCode, "招标委员会主任") + "," + Workflow_Common.GetRoleUsersWithNoApproval(PKURGICode, "招标委员会成员").Trim(',');
        dfInfos.Add(new K2_DataFieldInfo() { Result = Workflow_Common.FilterDataField(leaders), Name = "GroupTenderCommitteeManager", IsHaveToExsit = true });

        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = PKURGICode, RoleName = "招标委员会主任", Name = "GroupTenderCommitteeChairman", IsHaveToExsit = true });
        return dfInfos;
    }

    /// <summary>
    /// 保存工作流Item
    /// </summary>
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

    /// <summary>
    /// 保存工作流实例
    /// </summary>
    /// <param name="p"></param>
    /// <param name="dateTime"></param>
    /// <param name="p_2"></param>
    /// <returns></returns>
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
                //appid和应用管理创建新的管理的应用号是一致的 
                workFlowInstance.AppId = "2006";
                workFlowInstance.CreateDeptCode = ddlDepartName.SelectedItem.Value.ToString(); 
                workFlowInstance.CreateDeptName =ddlDepartName.SelectedItem.Text;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.FormTitle = tbTitle.Text;
            }
            else
            {
                isEdit = true;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.FormTitle = tbTitle.Text;
                workFlowInstance.AppId = "2006";
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
            //会签
            Countersign1.ProcId = workFlowInstance.InstanceId;
            Countersign_Group1.ProcId = workFlowInstance.InstanceId;
            Countersign1.SaveData(true);//会签数据保存
            Countersign_Group1.SaveData(true);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return result;
    }

    /// <summary>
    /// 弹出对话框方法
    /// </summary>
    /// <param name="Page"></param>
    /// <param name="p"></param>
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

    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartDeptId = ddlDepartName.SelectedItem.Value;
        if (ddlDepartName.SelectedItem.Text.Contains("开封"))
        {
            cblFirstLevel.Visible = true;
        }
        else
        {
            cblFirstLevel.Visible = false;
        }
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
    }
}