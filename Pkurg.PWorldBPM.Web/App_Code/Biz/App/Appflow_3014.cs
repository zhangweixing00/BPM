using System;
using System.Collections.Generic;
using System.Linq;
using Pkurg.PWorldBPM.Business.BIZ;

namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///IT
    /// </summary>
    public class Appflow_3014 : AppFlowBase
    {
        protected override string GetWorkflowContent(string formId)
        {
            var bizInfo = DBContext.GetBizContext().OA_ITSupport_Form.FirstOrDefault(x => x.FormID == formId);
            if (bizInfo != null)
            {
                return bizInfo.ContentTxt;
            }
            return "";
        }

        protected override bool Approval(string sn, string instance, string option, string action)
        {
            return base.Approval(sn, instance, option, action);
        }

        protected override bool AfterApproval(string instanceId, string option, string action)
        {
            ///对于加签
            if (IsAddSign())
            {
                return true;
            }
            var context = DBContext.GetSysContext();
            var bizContext = DBContext.GetBizContext();
            ///更新当前步骤审批人处理记录
            ///       
            var bizInfo = bizContext.OA_ITSupport_Form.FirstOrDefault(x => x.FormID == _BPMContext.ProcInst.FormId);

            var currentStep = bizContext.OA_ITSupport_Step.FirstOrDefault(x => x.Id == bizInfo.CurrentStepId);
            currentStep.FinishTime = DateTime.Now;
            currentStep.StartType = (int)ITSupportStatus.已转出;

            ITSupportStatus nextStatus = ITSupportStatus.结束;
            switch (action)
            {
                case "转出":
                    nextStatus = ITSupportStatus.待领取;
                    break;
                case "领取":
                    nextStatus = ITSupportStatus.处理;
                    break;
                default:
                    break;
            }
            if (nextStatus != ITSupportStatus.结束)
            {
                string stepId = Guid.NewGuid().ToString();
                if (nextStatus == ITSupportStatus.待领取)
                {
                    bizInfo.ProcessGroupId = AssistObject.ToString();//更换组
                }

                bizContext.OA_ITSupport_Step.InsertOnSubmit(new Pkurg.PWorldBPM.Business.BIZ.OA_ITSupport_Step()
                {
                    Id = stepId,
                    FormID = _BPMContext.ProcInst.FormId,
                    InstanceId = _BPMContext.ProcID,
                    StartTime = DateTime.Now,
                    OrderId = currentStep.OrderId.Value + 1,
                    StartType = (int)nextStatus,
                    ProcessGroupId = bizInfo.ProcessGroupId
                });

                List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo> stepUsers = new List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo>();
                if (nextStatus == ITSupportStatus.待领取)
                {
                    stepUsers = ITSupportCommon.GetUserListByGroupId(int.Parse(AssistObject.ToString()));
                    NotifyUsers(stepUsers);
                }
                else if (nextStatus == ITSupportStatus.处理)
                {
                    stepUsers.Add(new Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo()
                    {
                        LoginName = _BPMContext.CurrentUser.LoginId,
                        EmployeeCode = _BPMContext.CurrentPWordUser.EmployeeCode
                    });
                }

                foreach (var item in stepUsers)
                {
                    bizContext.OA_ITSupport_Step_Users.InsertOnSubmit(new OA_ITSupport_Step_Users()
                    {
                        StepId = stepId,
                        UserCode = item.EmployeeCode,
                        LoginName = item.LoginName
                    });
                }

                bizInfo.CurrentStepId = stepId;//更换步骤
            }
            else//处理或驳回
            {
                bizInfo.CurrentStepId = "-1";
                if (action == "处理")
                {
                    bizInfo.ProcessResult = 1;
                }
                if (action == "驳回")
                {
                    bizInfo.ProcessResult = 2;
                }
            }

            bizContext.SubmitChanges();

            return true;
        }

        private void NotifyUsers(List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo> stepUsers)
        {
            try
            {
                new System.Threading.Tasks.Task(() =>
                    {
                        string notifyString = string.Format("有支持单待领取：{0}", this._BPMContext.ProcInst.ProcName);
                        foreach (var item in stepUsers)
                        {
                            new WebServiceInvokeHelper().Invoke("http://172.25.20.43:3553/Service.asmx", "Notify", "Service",
                                new string[] { item.LoginName, notifyString });
                        }
                    });

            }
            catch (Exception)
            {


            }
        }



        protected override bool AfterAddOrChangeSign(string fromUserCode, string toUserCode, string instanceId, string action, string remark)
        {
            var context = DBContext.GetSysContext();
            var bizContext = DBContext.GetBizContext();
            ///更新当前步骤审批人处理记录
            ///       
            var bizInfo = bizContext.OA_ITSupport_Form.FirstOrDefault(x => x.FormID == _BPMContext.ProcInst.FormId);

            var currentStep = bizContext.OA_ITSupport_Step.FirstOrDefault(x => x.Id == bizInfo.CurrentStepId);
            currentStep.FinishTime = DateTime.Now;

            string stepId = Guid.NewGuid().ToString();
            bizContext.OA_ITSupport_Step.InsertOnSubmit(new Pkurg.PWorldBPM.Business.BIZ.OA_ITSupport_Step()
            {
                Id = stepId,
                FormID = _BPMContext.ProcInst.FormId,
                InstanceId = _BPMContext.ProcID,
                StartTime = DateTime.Now,
                OrderId = currentStep.OrderId.Value + 1,
                StartType = (int)(action == "加签" ? ITSupportStatus.加签处理 : ITSupportStatus.转签处理),
                ProcessGroupId = bizInfo.ProcessGroupId
            });

            var nextUser = context.V_Pworld_UserInfo.FirstOrDefault(x => x.LoginName == toUserCode);
            bizContext.OA_ITSupport_Step_Users.InsertOnSubmit(new OA_ITSupport_Step_Users()
            {
                StepId = stepId,
                UserCode = nextUser.EmployeeCode,
                LoginName = toUserCode
            });

            bizInfo.CurrentStepId = stepId;

            bizContext.SubmitChanges();

            return true;
        }

    }
}