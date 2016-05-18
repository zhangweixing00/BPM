using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common;

/// <summary>
///流程发起页面基类
/// </summary>
public class E_WorkflowFormBase : UPageBase
{
    public string AppID;

    protected WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    /// <summary>
    /// 流程FormId
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
    /// 流程标题
    /// </summary>
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


    /// <summary>
    /// 发起部门
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


    public E_WorkflowFormBase()
    {
        CommonControlList = new List<UControlBase>();
        Object[] objs = GetType().GetCustomAttributes(typeof(BPMAttribute), true);
        if (objs != null && objs.Length > 0)
        {
            BPMAttribute attribute = objs[0] as BPMAttribute;
            this.AppID = attribute.AppId;
        }
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (string.IsNullOrEmpty(AppID))
        {
            throw new NotImplementedException("请设置APPID");
        }
    }

    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected virtual void InitFormData()
    {

    }

    /// <summary>
    /// 保存表单上的数据到数据库
    /// </summary>
    protected virtual void SaveFormData()
    {

    }
    /// <summary>
    /// 标题设定
    /// </summary>
    /// <returns></returns>
    protected virtual string GetFormTitle()
    {
        if (!string.IsNullOrEmpty(FormTitle))
        {
            return FormTitle;
        }
        throw new NotImplementedException("请设置发起标题");
    }


    /// <summary>
    /// 表单的通用控件会签、集团会签、附件上传
    /// </summary>
    public List<UControlBase> CommonControlList { get; set; }

    /// <summary>
    /// 保存通用控件数据：会签、集团会签、附件上传
    /// </summary>
    private void LoadCommonControl(ControlCollection controls)
    {
        if (controls == null)
        {
            controls = Page.Controls;
        }

        foreach (Control item in controls)
        {
            if (item is UControlBase)
            {
                CommonControlList.Add(item as UControlBase);
            }
            else if (item.HasControls() && (item.Controls.Count > 0))
            {
                LoadCommonControl(item.Controls);
            }
        }
    }

    private void SaveCommonControl()
    {
        if (CommonControlList == null)
        {
            CommonControlList = new List<UControlBase>();
            LoadCommonControl(null);

            // return;
        }
        foreach (var item in CommonControlList)
        {
            item.SaveControlData();
        }
    }

    /// <summary>
    /// 流程保存
    /// </summary>
    protected void Save()
    {
        SaveFormData();
        SaveWorkFlowInstance();
        LoadCommonControl(null);
        SaveCommonControl();
        AfterSaveInstance();
    }

    protected virtual void AfterSaveInstance()
    {

    }

    #region SaveWorkFlowInstance
    /// <summary>
    /// 保存流程实例
    /// </summary>
    /// <returns></returns>
    private string SaveWorkFlowInstance()
    {
        string startDeptName = "";
        if (string.IsNullOrWhiteSpace(StartDeptId))
        {
            StartDeptId = CurrentEmployee.DepartCode;
            startDeptName = CurrentEmployee.DepartName;
        }
        else
        {
            Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(StartDeptId);
            if (deptInfo != null)
            {
                startDeptName = deptInfo.Remark;
            }
        }

        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
        if (workFlowInstance == null)
        {
            workFlowInstance = new WorkFlowInstance();
            workFlowInstance.InstanceId = Guid.NewGuid().ToString();
            workFlowInstance.CreateAtTime = DateTime.Now;
            workFlowInstance.AppId = AppID;
            workFlowInstance.FormId = FormId;
            workFlowInstance.CreateDeptCode = StartDeptId;// CurrentEmployee.DepartCode;
            workFlowInstance.CreateDeptName = startDeptName;
            workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
            workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
            workFlowInstance.FormTitle = GetFormTitle();
            workFlowInstance.WfStatus = "0";
            wf_WorkFlowInstance.AddWorkFlowInstance(workFlowInstance);

            SaveWorkItem(workFlowInstance.InstanceId);
            _BPMContext.ProcID = workFlowInstance.InstanceId;
        }
        else
        {
            workFlowInstance.CreateDeptCode = StartDeptId;
            workFlowInstance.CreateDeptName = startDeptName;
            workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
            workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
            workFlowInstance.FormTitle = GetFormTitle();
            wf_WorkFlowInstance.UpdateWorkFlowInstance(workFlowInstance);
        }

        return workFlowInstance == null ? "" : workFlowInstance.InstanceId;
    }

    private bool SaveWorkItem(string instanceId)
    {
        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
        {
             ApprovalID = Guid.NewGuid().ToString(),

           FormID = FormId,
            InstanceID = instanceId,
            Opinion = "",
            ApproveAtTime = DateTime.Now,
            ApproveResult = "",//开始
            OpinionType = "",
            CurrentActiveName = "拟稿",
 ISSign = "0",
            CurrentActiveID = "0",
            DelegateUserName = "",
            DelegateUserCode = "",
            CreateAtTime = DateTime.Now,
            UpdateAtTime = DateTime.Now,
            FinishedTime = DateTime.Now,
            ApproveByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
            ApproveByUserName = _BPMContext.CurrentPWordUser.EmployeeName
        };

        return new BFApprovalRecord().AddApprovalRecord(appRecord);
    }

    #endregion

    /// <summary>
    /// 流程提交
    /// </summary>
    protected void Submit()
    {
        if (!BeforeWorkflowStart())
        {
            return;
        }
        var instanceInfo = SysContext.WF_WorkFlowInstance.FirstOrDefault(x => x.FormID == FormId);
        if (instanceInfo != null && instanceInfo.WFStatus != "0")
        {
            Alert("流程已发起,请关闭页面");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.close();", true);
            return;
        }
        Save();
        StartWorkFlow();
    }

    #region StartWorkFlow
    /// <summary>
    /// 开始发起流程
    /// </summary>
    /// <returns></returns>
    protected bool StartWorkFlow()
    {
        //验证APPID
        AppDict appInfo = new Pkurg.BPM.Services.AppDictService().GetByAppId(AppID);
        if (appInfo == null)
        {
            Alert("APPID不正确，提交失败");
            return false;
        }

        #region 工作流参数
        NameValueCollection dataFields = SetWFParams();
        if (dataFields == null)
        {
            return false;
        }
        #endregion

        int wfInstanceId = 0; //process instance id
        WorkflowHelper.StartProcess(appInfo.WorkFlowName, FormId, dataFields, ref wfInstanceId, _BPMContext.CurrentUser.LoginId);
        if (wfInstanceId > 0)
        {
            string instId = ChangeWorkFlowInstanceStart(wfInstanceId);
            if (!string.IsNullOrEmpty(instId))
            {
                if (!AfterWorkflowStart(wfInstanceId))
                {
                    return false;
                }
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功');if(window.opener){window.opener.location.href=window.opener.location.href;}", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "if(window.opener){ window.opener=null;}window.open('', '_self', '');window.close();", true);
                return true;
            }
        }
        return false;
    }

    private string ChangeWorkFlowInstanceStart(int wfInstanceId)
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
        if (workFlowInstance != null)
        {
            workFlowInstance.SumitTime = DateTime.Now;
            workFlowInstance.WfStatus = "1";
            workFlowInstance.WfInstanceId = wfInstanceId.ToString();
            wf_WorkFlowInstance.UpdateWorkFlowInstance(workFlowInstance);
            return workFlowInstance.InstanceId;
        }
        return "";
    }

    #region Params
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
            if (!string.IsNullOrWhiteSpace(item.Result))
            {
                continue;
            }
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

    #region DataField

    /// <summary>
    /// 初始化用户DataField
    /// </summary>
    /// <returns></returns>
    protected virtual List<K2_DataFieldInfo> LoadUserDataField()
    {
        throw new NotImplementedException("需要添加用户DataField");
    }

    /// <summary>
    /// 初始化常量DataField
    /// </summary>
    /// <returns></returns>
    protected virtual NameValueCollection LoadConstDataField()
    {
        return null;
    }

    /// <summary>
    /// 提示
    /// </summary>
    /// <param name="msg"></param>
    public void Alert(string msg)
    {
        DisplayMessage.ExecuteJs(string.Format("alert('{0}');", msg));
    }

    #endregion

    #endregion

    #endregion

    /// <summary>
    /// 删除当前流程
    /// </summary>
    /// <returns></returns>
    protected void DelWorkflow()
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
    /// 流程发起前做的处理，返回false流程不启动
    /// </summary>
    /// <returns></returns>
    protected virtual bool BeforeWorkflowStart()
    {
        return true;
    }

    /// <summary>
    /// 流程发起后做的处理
    /// </summary>
    /// <returns></returns>
    protected virtual bool AfterWorkflowStart(int wfInstanceId)
    {
        return true;
    }







    /// <summary>
    /// 获取发起部门数据源
    /// 部门名称：Remark
    /// Code：DepartCode
    /// </summary>
    /// <returns></returns>
    protected List<Department> GetStartDeptmentDataSource()
    {
        List<Department> deptments = new List<Department>();
        Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(_BPMContext.CurrentUser.MainDeptId);
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        Pkurg.PWorld.Entities.TList<Department> deptInfo2 = bfurd.GetDeptListByEmployeeCodeAndRoleName(_BPMContext.CurrentPWordUser.EmployeeCode, "部门成员");
        deptments.Add(deptInfo);
        foreach (Department item in deptInfo2)
        {
            if (!deptments.Exists(x => x.DepartCode == item.DepartCode))
            {
                deptments.Add(item);
            }
        }
        //if (deptInfo2.Exists(x => x.DepartCode == deptInfo.DepartCode))
        //{
        //    deptInfo2.Remove(deptInfo);
        //}
        // deptments.Insert(0, deptInfo);
        return deptments;
    }

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);

        //InitFormData();

        if (_BPMContext.ProcInst != null)
        {
            FormId = _BPMContext.ProcInst.FormId;
            if (!IsPostBack)
            {
                StartDeptId = _BPMContext.ProcInst.StartDeptCode;
            }
        }
    }




}
