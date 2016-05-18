using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Context;
using SourceCode.Workflow.Client;

/// <summary>
///移动APP流程操作基类，重写请以Appflow_[appid]命名
///star
/// </summary>
public class AppFlowBase
{
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();

    protected BPMContext _BPMContext { get; set; }
    public AppFlowBase()
    {
        _BPMContext = new BPMContext();
        _BPMContext.OrgService = new Pkurg.PWorldBPM.Common.Services.OrgService();
        _BPMContext.ProcService = new Pkurg.PWorldBPM.Common.Services.BPMProcService();
    }

    /// <summary>
    /// 供接口调用
    /// </summary>
    /// <param name="instanceId"></param>
    /// <returns></returns>
    public DetailInfo GetFlowInfoById(string instanceId)
    {
        var context = DBContext.GetSysContext();
        var instance = context.WF_WorkFlowInstance.FirstOrDefault(x => x.InstanceID == instanceId);
        DetailInfo info = new DetailInfo();
        var appInfo = context.WF_AppDict.FirstOrDefault(x => x.AppId == instance.AppID);
        info.FlowType = appInfo == null ? "" : appInfo.AppName;
        info.FormTitle = instance.FormTitle;
        info.StartDeptName = instance.CreateDeptName;
        info.StartTime = instance.SumitTime.Value.ToString();
        info.StartUserName = instance.CreateByUserName;
        info.Content = GetWorkflowContent(instance.FormID);
        if (!string.IsNullOrEmpty(info.Content))
        {
            //支持换行，制表位 by yanghechun 2015-12-30
            info.Content = info.Content.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
        }
        info.DetailUrl = GetDetailUrl(instanceId, appInfo);
        info.StepInfos = GetStepInfos(instanceId);
        info.AttachmentInfos = context.BPM_Attachment.Where(x => x.FormID == instance.FormID && x.IsDel == 0).OrderBy(x => x.CreateAtTime).ToList();
        string rootUrl = "http://" + HttpContext.Current.Request.Url.Authority;
        foreach (var item in info.AttachmentInfos)
        {
            item.URL = string.Format("{0}{1}", rootUrl, item.URL);
        }
        return GetWorkflowInfo(info);
    }

    #region 获取流程详情

    private string GetDetailUrl(string instanceId, Pkurg.PWorldBPM.Business.Sys.WF_AppDict appInfo)
    {
        string rootUrl = "http://" + HttpContext.Current.Request.Url.Authority;
        return string.Format("{0}/Workflow/ViewPage/V_{1}?id={2}", rootUrl, appInfo.FormName, instanceId);
    }

    private List<DetailStepInfo> GetStepInfos(string instanceId)
    {
        List<DetailStepInfo> stepInfos = new List<DetailStepInfo>();
        var context = DBContext.GetSysContext();
        var approvalList = context.WF_Approval_Record.OrderBy(x => x.ApproveAtTime).Where(x => x.InstanceID == instanceId && x.CurrentActiveName != "拟稿");
        var groupedList = approvalList.GroupBy(x => x.CurrentActiveName).ToDictionary(x => x.Key).OrderBy(x => x.Value.Max(y => y.ApproveAtTime));
        foreach (var item in groupedList)
        {
            stepInfos.Add(new DetailStepInfo()
            {
                Name = item.Key,
                Options = GetOptions(item.Value.ToList())
            });
        }
        return stepInfos;
    }

    private List<DetailStepUserOption> GetOptions(List<Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record> list)
    {
        List<DetailStepUserOption> options = new List<DetailStepUserOption>();
        foreach (var item in list)
        {
            options.Add(new DetailStepUserOption()
            {
                UserName = item.ApproveByUserName,
                Option = item.Opinion,
                ApprovalTime = item.ApproveAtTime.Value
            });
        }
        return options;
    }

    #endregion

    #region 审批

    string activityName = "";
    protected bool CommonApproval(string sn, string option, string action)
    {
        if (string.IsNullOrWhiteSpace(option))
        {
            option = action;
        }
        activityName = GetCurrentActiveName(sn);
        bool isAddSign = new Workflow_Common().IsAddSign(sn, _BPMContext.CurrentUser.LoginId);
        bool isChangeSign = new Workflow_Common().IsChangeSign(sn, _BPMContext.CurrentUser.LoginId);
        bool isSuccess = false;
        if (!isAddSign)
        {
            isSuccess = WorkflowHelper.ApproveProcess(sn, action, _BPMContext.CurrentUser.ApprovalUser);
        }
        else if (isChangeSign)
        {
            isSuccess = WorkflowHelper.ApproveProcess(sn, action, _BPMContext.CurrentUser.ApprovalUser);
        }
        else
        {
            isSuccess = WorkflowHelper.BackToPreApprover(sn, _BPMContext.CurrentUser.ApprovalUser);

        }
        if (isSuccess)
        {
            AddApprovalOption(sn, action, option, isAddSign ? "1" : "0", activityName);
            return true;
        }
        return false;
    }

    private bool AddApprovalOption(string sn, string action, string opinion, string isSign, string activityName)
    {
        string DelegateUserName = "";
        string DelegateUserCode = "";
        string currentActiveName = activityName;
        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
        {
              ApprovalID = Guid.NewGuid().ToString(),

             FormID = _BPMContext.ProcInst.FormId,
             InstanceID = _BPMContext.ProcInst.ProcId,
            CurrentActiveName = currentActiveName,

            ApproveResult = action,//审批结果
            Opinion = opinion,//用户填写的审批意见
             ISSign = isSign,
              
          
            OpinionType = "",
             CurrentActiveID = "0",
            DelegateUserName = DelegateUserName,
            DelegateUserCode = DelegateUserCode,
            CreateAtTime = DateTime.Now,
            CreateByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
            CreateByUserName = _BPMContext.CurrentPWordUser.EmployeeName,
            UpdateAtTime = DateTime.Now,
            UpdateByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
            UpdateByUserName = _BPMContext.CurrentPWordUser.EmployeeName,
            ApproveAtTime = DateTime.Now,
            ApproveByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
            ApproveByUserName = _BPMContext.CurrentPWordUser.EmployeeName,
            FinishedTime = DateTime.Now,
            ApproveStatus = IsFromPC ? "" : "Mobile"
        };

        bfApproval.AddApprovalRecord(appRecord);
        wf_WorkFlowInstance.UpdateNowStatus(_BPMContext.ProcInst.WorkflowId, "1", "0", currentActiveName, 0, null, _BPMContext.CurrentUser.PWordUser);
        return true;
    }

    protected virtual string GetCurrentActiveName(string sn)
    {
        WorklistItem listItem = null;
        try
        {
            listItem = WorkflowHelper.GetWorklistItemWithSN(sn, _BPMContext.CurrentUser.ApprovalUser);
            return listItem.ActivityInstanceDestination.Name;
        }
        catch
        {
            return "";
        }
    }

    public bool StartApproval(string sn, string instanceId, string option, string action)
    {
        if (action == "不同意" && !new Workflow_Common().IsAddSign(sn, _BPMContext.CurrentUser.LoginId))
        {
            if (!ChangeResultToUnAgree(sn))
            {
                return false;
            }
        }
        if (Approval(sn, instanceId, option, action))
        {
            return AfterApproval(instanceId, option, action);
        }
        return false;
    }

    /// <summary>
    /// 更改结果
    /// </summary>
    private bool ChangeResultToUnAgree(string sn)
    {
        try
        {
            if (WorkflowHelper.GetProcessInstanceAllDataFields(int.Parse(_BPMContext.WorkflowId)).AllKeys.Contains("IsPass"))
            {
                NameValueCollection dataFields = new NameValueCollection();
                dataFields.Add("IsPass", "0");
                return WorkflowManage.ModifyDataField(sn, dataFields);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion

    /// <summary>
    /// 根据不同流程，重写此方法可返回该流程的详情
    /// </summary>
    /// <param name="formId"></param>
    /// <returns></returns>
    protected virtual string GetWorkflowContent(string formId)
    {
        return "";
    }

    /// <summary>
    /// 根据不同流程，重写此方法可自定义审批
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="instanceId"></param>
    /// <param name="option"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    protected virtual bool Approval(string sn, string instanceId, string option, string action)
    {
        return CommonApproval(sn, option, action);
    }

    /// <summary>
    /// 可重新定义流程审批完成后操作
    /// </summary>
    /// <param name="instanceId"></param>
    /// <param name="option"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    protected virtual bool AfterApproval(string instanceId, string option, string action)
    {
        return true;
    }

    /// <summary>
    /// 根据不同流程，自定义返回的结果
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    protected virtual DetailInfo GetWorkflowInfo(DetailInfo info)
    {
        return info;
    }
    //加签
    protected bool IsAddSign()
    {
        return new Workflow_Common().IsAddSign(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
    }
    //转签
    protected bool IsChangeSign()
    {
        return new Workflow_Common().IsChangeSign(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
    }

    public virtual bool ChangeSign(string fromUserCode, string toUserCode, string sn, string instanceId, string remark)
    {
        try
        {
            string action = "转签";
            activityName = GetCurrentActiveName(sn);
            WorkflowHelper.ForwardToNextApprover_Change(_BPMContext.Sn, fromUserCode, toUserCode);
            return AddApprovalOption(sn, action, remark, "2", activityName) && AfterAddOrChangeSign(fromUserCode, toUserCode, instanceId,action, remark);
        }
        catch (Exception)
        {
            return false;
        }
    }
    public virtual bool AddSign(string fromUserCode, string toUserCode, string sn, string instanceId, string remark)
    {
        try
        {
            string action = "加签";
            //if (string.IsNullOrWhiteSpace(remark))
            //{
            //    remark = action;
            //}
            //置为空不显示
            activityName = GetCurrentActiveName(sn);
            WorkflowHelper.ForwardToNextApprover(_BPMContext.Sn, fromUserCode, toUserCode);
            return AddApprovalOption(sn, action, remark, "2", activityName) && AfterAddOrChangeSign(fromUserCode, toUserCode, instanceId,action, remark);
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected virtual bool AfterAddOrChangeSign(string fromUserCode, string toUserCode, string instanceId, string action,string remark)
    {
        return true;
    }

    public bool IsFromPC { get; set; }
    public Object AssistObject { get; set; }
}