using System;
using System.Web.UI;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_Approval : UWorkflowPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //不参与回发数据处理,需每次加载，ViewState会自动填充
        LoadForm();

        string cUser = Request["u"];
        if (!string.IsNullOrEmpty(cUser))
        {
            _BPMContext.LoginId = cUser;
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
        //
       // hf_OpId.Value = FromControl.ApprovalTextBoxId;
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        hf_OpId.Value = FromControl.ApprovalTextBoxId;
    }
    private void LoadForm()
    {
        if (FromControl != null)
        {
            phForm.Controls.Add(FromControl);
        }
    }

    //protected void lbUnAgree_Click(object sender, EventArgs e)
    //{
    //    Execute(1);
    //}
    //protected void lbAgree_Click(object sender, EventArgs e)
    //{
    //    Execute(0);
    //}

    //private void Execute(int option)
    //{
    //    Employee currentUserInfo = _BPMContext.CurrentPWordUser;

    //    FromControl.AppendParams();

    //    WorklistItem item = WorkflowHelper.GetWorklistItemWithSN(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
    //    bool isSuccess = WorkflowHelper.ApproveProcess(_BPMContext.Sn, item.Actions[option].Name, _BPMContext.CurrentUser.LoginId);

    //    if (isSuccess)
    //    {
    //        new Pkurg.BPM.Services.ApprovalRecordService().Save(new Pkurg.BPM.Entities.ApprovalRecord()
    //        {
    //            InstanceId = _BPMContext.ProcInst.ProcId,
    //            ApproveByUserCode = currentUserInfo.EmployeeCode,
    //            ApproveByUserName = currentUserInfo.EmployeeName,
    //            ApproveAtTime = DateTime.Now,
    //            FinishedTime = DateTime.Now,
    //             ApprovalID = Guid.NewGuid().ToString(),
    //            CurrentActiveName = item.ActivityInstanceDestination.Name,
    //            Opinion = FromControl.ApprovalText
    //        });

    //        FromControl.Save();

    //        if (WorkflowHelper.GetProcessInstance(int.Parse(_BPMContext.ProcInst.WorkflowId)).Status1 == ProcessInstance.Status.Completed)
    //        {
    //            _BPMContext.ProcInst.Status = "2";
    //            _BPMContext.ProcInst.EndTime = DateTime.Now;
    //        }

    //        if (option == 1)
    //        {
    //            _BPMContext.ProcInst.Status = "4";
    //        }

    //        _BPMContext.Save();
    //    }
    //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批完成'); window.close();", true);
    //}

    protected void btnDoAction_Click(object sender, EventArgs e)
    {
        string actionName = hf_ActionName.Value;

        if (string.IsNullOrEmpty(actionName))
        {
            return;
        }
        string Opinion = FromControl.ApprovalText;
        string ApproveResult = actionName;
        string OpinionType = "";
        string IsSign = "0";//

        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
        {
             ApprovalID = Guid.NewGuid().ToString(),
            WFTaskID = K2_TaskItem.ID,
            FormID = _BPMContext.ProcInst.FormId,
            InstanceID = _BPMContext.ProcInst.ProcId,
            Opinion = Opinion,
            ApproveAtTime = DateTime.Now,
            ApproveByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
            ApproveByUserName = _BPMContext.CurrentPWordUser.EmployeeName,
            ApproveResult = ApproveResult,
            OpinionType = OpinionType,
            CurrentActiveName = K2_TaskItem.ActivityInstanceDestination.Name,
            ISSign = IsSign,
             CurrentActiveID = K2_TaskItem.ActivityInstanceDestination.ID.ToString(),
            DelegateUserName = "",
            DelegateUserCode = "",
            CreateAtTime = DateTime.Now,
            CreateByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
            CreateByUserName = _BPMContext.CurrentPWordUser.EmployeeName,
            UpdateAtTime = DateTime.Now,
            UpdateByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
            UpdateByUserName = _BPMContext.CurrentPWordUser.EmployeeName
        };

        //审批前设置参数
        FromControl.AppendParams();

        bool isSuccess = false;

        if ("addSignback" == actionName)//加签提交
        {
            isSuccess = WorkflowHelper.BackToPreApprover(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
            IsSign = "2";//
        }
            //转签提交【是否需要】
        else if ("changeSignback" == actionName)
        {
            isSuccess = WorkflowHelper.BackToNextApprover(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
            IsSign = "3";
        }
        else
        {
            //普通审批
            isSuccess = WorkflowHelper.ApproveProcess(_BPMContext.Sn, actionName, _BPMContext.CurrentUser.LoginId);
        }

        if (isSuccess)
        {
            new BFApprovalRecord().AddApprovalRecord(appRecord);

            ///保存数据（主要是流程审核人数据）
            FromControl.SaveFormData();

            new WF_WorkFlowInstance().UpdateStatus(
                _BPMContext.WorkflowId,
                actionName == "不同意" ? "4" : "1",//更新流程状态
                K2_TaskItem.ActivityInstanceDestination.ID.ToString(),
                K2_TaskItem.ActivityInstanceDestination.Name,
                K2_TaskItem.ID,
                null,
                _BPMContext.CurrentPWordUser);

            ///更新流程状态
            //if (actionName == "不同意")
            //{
            //    _BPMContext.ProcInst.Status = "4";
            //}
            _BPMContext.Save();

            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('审批完成'); window.close();", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('操作失败，请重试'); ", true);

        }
    }
}