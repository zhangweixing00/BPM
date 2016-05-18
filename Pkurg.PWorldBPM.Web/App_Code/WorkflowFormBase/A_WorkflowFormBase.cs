#define DEBUG
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common;

/// <summary>
///流程审批页面基类
/// </summary>
public class A_WorkflowFormBase : UPageBase
{
    public string AppID;

    protected WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    public string CustomActivityName
    {
        get
        {
            if (ViewState["CustomActivityName"] == null)
            {
                return "";
            }
            return ViewState["CustomActivityName"].ToString();
        }
        set
        {
            ViewState["CustomActivityName"] = value;
        }
    }

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

    private static object lockobj = new object();

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


    public A_WorkflowFormBase()
    {
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
    /// 是否已有用户审批
    /// </summary>
    /// <returns></returns>
    public bool IsApprovaled()
    {
        try
        {
            var kItem = WorkflowHelper.GetWorklistItemWithSN(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
            if (kItem != null)
            {
                return false;
            }
            return true;
        }
        catch
        {
            return true;
        }
    }

    /// <summary>
    /// 表单的通用控件会签、集团会签、附件上传
    /// </summary>
    public List<UControlBase> CommonControlList { get; set; }

    protected void Approval(string action)
    {
        if (IsApprovaled())
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('改审批环节已结束'); window.opener.location.href=window.opener.location.href; ", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
            return;
        }
#if DEBUG
        LoggerR.logger.Debug("Begin Approval");
#endif
        string option = GetApproveOpinion();
        if (string.IsNullOrEmpty(option))
        {
            option = action;
        }

        bool isSuccess = false;
        if (action == "提交")
        {
            if (!BeforeWorkflowApproval(ref action, ref option))
            {
                return;
            }
            isSuccess = new BPMBase(_BPMContext.ProcID).BackToPreApprover(_BPMContext.Sn, "同意", option, CustomActivityName);
        }
        else
        {

            if (!BeforeWorkflowApproval(ref action, ref option))
            {
                return;
            }
#if DEBUG
            LoggerR.logger.Debug("BeforeWorkflowApproval Finished");
#endif
            isSuccess = new BPMBase(_BPMContext.ProcID).Approval(_BPMContext.Sn, action, option, "0", CustomActivityName);
        }

        if (!AfterWorkflowApproval(action, option, isSuccess))
        {
            return;
        }
#if DEBUG
        LoggerR.logger.Debug("AfterWorkflowApproval Finished");
#endif
        if (isSuccess)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批成功'); if(window.opener){window.opener.location.href=window.opener.location.href;}", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " if(window.opener){ window.opener=null;} window.open('', '_self', '');window.close();", true);
        }
    }


    /// <summary>
    /// 审批前处理
    /// </summary>
    /// <param name="action">动作</param>
    /// <param name="option">意见</param>
    /// <returns></returns>
    protected virtual bool BeforeWorkflowApproval(ref string action, ref string option)
    {
        return true;
    }
    /// <summary>
    /// 审批后处理
    /// </summary>
    /// <param name="action">动作</param>
    /// <param name="option">意见</param>
    /// <param name="isSuccess">审批结果</param>
    /// <returns></returns>
    protected virtual bool AfterWorkflowApproval(string action, string option, bool isSuccess)
    {
        return true;
    }



    #region 意见框处理

    private string GetApproveOpinion()
    {
        if (K2_TaskItem == null)
        {
            return "";
        }

        string opinion = "";
        List<IApprovalBox> options = GetOptions();
        foreach (var item in options)
        {
            if (item.Node.ToLower().Split(',').ToList().Contains(K2_TaskItem.ActivityInstanceDestination.Name.ToLower()))
            {
                opinion = item.OptionText;
                break;
            }
        }
        return opinion;

    }

    private List<IApprovalBox> GetOptions()
    {
        List<IApprovalBox> options = new List<IApprovalBox>();

        if (CommonControlList == null || CommonControlList.Count == 0)
        {
            LoadCommonControl(null);
        }
        foreach (var item in CommonControlList)
        {
            if (item is IApprovalBox)
            {
                options.Add(item as IApprovalBox);
            }
        }
        return options;
    }

    private void LoadCommonControl(ControlCollection controls)
    {
        if (controls == null)
        {
            if (CommonControlList == null)
            {
                CommonControlList = new List<UControlBase>();
            }
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

    #endregion

    //protected override void OnPreRender(EventArgs e)
    //{
    //        base.OnPreRender(e);
    //        try
    //        {
    //            WorklistItem taskItem = WorkflowHelper.GetWorklistItemWithSN(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);

    //        }
    //        catch (Exception)
    //        {
    //            //hf_OpId.Value = "-1";
    //        }
    //}

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);

        ///审批界面显示
        if (!string.IsNullOrEmpty(Request.QueryString["sn"]))
        {
        }
        else
        {
            ExceptionHander.GoToErrorPage("该审批环节已结束", null);
        }

        if (_BPMContext.ProcInst != null)
        {
            FormId = _BPMContext.ProcInst.FormId;
            StartDeptId = _BPMContext.ProcInst.StartDeptCode;
            Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(StartDeptId);
            if (deptInfo != null)
            {
                StartDeptName = deptInfo.DepartName;
            }
        }
    }

    public string StartDeptName { get; set; }

    protected  NameValueCollection Convert(List<K2_DataFieldInfo> dfInfos)
    {
        NameValueCollection dataFields = new NameValueCollection();

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
    /// <summary>
    /// 提示
    /// </summary>
    /// <param name="msg"></param>
    public void Alert(string msg)
    {
        DisplayMessage.ExecuteJs(string.Format("alert('{0}');", msg));
    }
}
