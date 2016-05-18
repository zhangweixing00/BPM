using System;
using System.Web.UI;
using SourceCode.Workflow.Client;

public partial class Workflow_MainPage : UWorkflowPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //不参与回发数据处理,需每次加载，ViewState会自动填充
        LoadForm();
    }


    private void LoadForm()
    {
        if (FromControl != null)
        {
            phForm.Controls.Add(FromControl);
        }
    }

    protected void lbSave_Click(object sender, EventArgs e)
    {
        FromControl.SaveFormData();
        if (_BPMContext.ProcInst == null)
        {
            _BPMContext.ProcID = Guid.NewGuid().ToString();
            _BPMContext.ProcInst = new Pkurg.BPM.Services.WorkFlowInstanceService().Save(new Pkurg.BPM.Entities.WorkFlowInstance()
            {
                WfStatus = "0",
                FormTitle = FromControl.ProcName,
                InstanceId = _BPMContext.ProcID,
                AppId = AppInfo.AppId,
                FormId = FromControl.SerialNumber,
                CreateByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
                CreateByUserName = _BPMContext.CurrentPWordUser.EmployeeName,
                CreateDeptCode = _BPMContext.CurrentPWordUser.DepartCode,
                CreateDeptName = _BPMContext.CurrentPWordUser.DepartName,
                 CreateAtTime=DateTime.Now
            }).ToContextInfo();
        }
        else
        {
            //
            _BPMContext.ProcInst.ProcName = FromControl.ProcName;
        }

        _BPMContext.Save();

        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('保存完成')", true);
    }
    protected void lbSubmit_Click(object sender, EventArgs e)
    {
        int procId = 0;

        FromControl.AppendParams();

        //未发起
        if (_BPMContext.ProcInst == null || _BPMContext.ProcInst.Status == "0")
        {
            WorkflowHelper.CurrentUser = _BPMContext.CurrentUser.LoginId;
            bool isSuccess = WorkflowHelper.StartProcess(AppInfo.WorkFlowName, DateTime.Now.ToString(), FromControl.FlowParams.WorkflowFieldDatas, ref procId);

            if (!isSuccess)
            {
                return;
            }

            FromControl.SaveFormData();


            if (_BPMContext.ProcInst == null)
            {
                _BPMContext.ProcID = Guid.NewGuid().ToString();
                _BPMContext.ProcInst = new Pkurg.BPM.Services.WorkFlowInstanceService().Save(new Pkurg.BPM.Entities.WorkFlowInstance()
                {
                    FormTitle = FromControl.ProcName,
                    InstanceId = _BPMContext.ProcID,
                    AppId = AppInfo.AppId,
                    FormId = FromControl.SerialNumber,
                    CreateByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
                    CreateByUserName = _BPMContext.CurrentPWordUser.EmployeeName,
                    CreateDeptCode = _BPMContext.CurrentPWordUser.DepartCode,
                    CreateDeptName = _BPMContext.CurrentPWordUser.DepartName,
                    CreateAtTime = DateTime.Now
                }).ToContextInfo();
            }

            _BPMContext.ProcInst.WorkflowId = procId.ToString();
            _BPMContext.ProcInst.Status = "1";
            _BPMContext.ProcInst.StartTime = DateTime.Now;


            new Pkurg.BPM.Services.ApprovalRecordService().Save(new Pkurg.BPM.Entities.ApprovalRecord()
            {
                InstanceId = _BPMContext.ProcInst.ProcId,
                ApproveByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
                ApproveByUserName = _BPMContext.CurrentUser.Name,
                ApproveAtTime = DateTime.Now,
                FinishedTime = DateTime.Now,
                ApprovalId = Guid.NewGuid().ToString()
            });


            _BPMContext.Save();
        }
        else if (_BPMContext.ProcInst.Status == "4")
        {
            WorklistItem item = WorkflowHelper.GetWorklistItemWithSN(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
            bool isSuccess = WorkflowHelper.ApproveProcess(_BPMContext.Sn, item.Actions[0].Name, _BPMContext.CurrentUser.LoginId);

            if (isSuccess)
            {
                new Pkurg.BPM.Services.ApprovalRecordService().Save(new Pkurg.BPM.Entities.ApprovalRecord()
                {
                    InstanceId = _BPMContext.ProcInst.ProcId,
                    ApproveByUserCode = _BPMContext.CurrentUser.Id,
                    ApproveByUserName = _BPMContext.CurrentUser.Name,
                    ApproveAtTime = DateTime.Now,
                    FinishedTime = DateTime.Now,
                      ApprovalId = Guid.NewGuid().ToString(),
                    CurrentActiveName = item.ActivityInstanceDestination.Name,
                    Opinion = "重新发起"
                });

                FromControl.SaveFormData();
                _BPMContext.ProcInst.Status = "1";
                _BPMContext.Save();
            }
        }

        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功'); window.close();", true);
    }
}