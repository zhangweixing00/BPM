using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Context;
using SourceCode.Workflow.Client;

/// <summary>
///BPM基础调用接口
///by star
/// </summary>
public class BPMBase
{
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    BFApprovalRecord bfApproval = new BFApprovalRecord();

    private BPMContext _BPMContext { get; set; }
    public WorkFlowInstance workFlowInstance { get; set; }

    public BPMBase(string instanceId)
    {
        _BPMContext = new BPMContext();
        workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceById(instanceId);
    }

    /// <summary>
    /// 通用审批接口
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="action"></param>
    /// <param name="opinion"></param>
    /// <param name="isSign"></param>
    /// <returns></returns>
    public bool Approval(string sn, string action, string opinion, string isSign = "0", string customActiveName = "")
    {
        WorklistItem listItem = null;
        try
        {
            listItem = WorkflowHelper.GetWorklistItemWithSN(sn, _BPMContext.CurrentUser.ApprovalUser);
        }
        catch
        {
            return false;
        }
        string currentActiveName = string.IsNullOrWhiteSpace(customActiveName) ? listItem.ActivityInstanceDestination.Name : customActiveName;

        bool isSuccess = WorkflowHelper.ApproveProcess(sn, action, _BPMContext.CurrentUser.ApprovalUser);


        if (isSuccess)
        {
            AddApprovalOption(action, opinion, isSign, currentActiveName);
            return true;
        }
        return false;
    }


    /// <summary>
    /// 反加签（提交步骤）
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="action"></param>
    /// <param name="opinion"></param>
    /// <returns></returns>
    public bool BackToPreApprover(string sn, string action, string opinion, string customActiveName = "")
    {
        if (string.IsNullOrWhiteSpace(opinion))
        {
            opinion = action;
        }
        WorklistItem listItem = null;
        try
        {
            listItem = WorkflowHelper.GetWorklistItemWithSN(sn, _BPMContext.CurrentUser.ApprovalUser);
        }
        catch
        {
            return false;
        }
        string currentActiveName = string.IsNullOrWhiteSpace(customActiveName) ? listItem.ActivityInstanceDestination.Name : customActiveName;

        bool isSuccess = WorkflowHelper.BackToPreApprover(sn, _BPMContext.CurrentUser.ApprovalUser);

        if (isSuccess)
        {
            AddApprovalOption(action, opinion, "2", currentActiveName);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 通一增加审批意见，不涉及流程引擎
    /// </summary>
    /// <param name="action"></param>
    /// <param name="opinion"></param>
    /// <param name="isSign"></param>
    /// <param name="currentActiveName"></param>
    private void AddApprovalOption(string action, string opinion, string isSign, string currentActiveName)
    {
        string DelegateUserName = "";
        string DelegateUserCode = "";
        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
        {
             ApprovalID = Guid.NewGuid().ToString(),

            FormID = workFlowInstance.FormId,
            InstanceID = workFlowInstance.InstanceId,
            CurrentActiveName = currentActiveName,

            ApproveResult = action,//审批结果
            Opinion = opinion,//用户填写的审批意见
            ISSign = isSign,


            OpinionType = "",

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
            FinishedTime = DateTime.Now
        };
        bfApproval.AddApprovalRecord(appRecord);
        wf_WorkFlowInstance.UpdateNowStatus(workFlowInstance.WfInstanceId, "1", "0", currentActiveName, 0, null, _BPMContext.CurrentUser.PWordUser);
    }
}
