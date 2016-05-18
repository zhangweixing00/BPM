using System.Collections.Generic;
using System.Linq;

namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///自定义流程
    /// </summary>
    public class Appflow_3011:AppFlowBase
    {
        protected override string GetWorkflowContent(string formId)
        {
            var bizInfo = DBContext.GetBizContext().OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == formId);
            if (bizInfo!=null)
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
            var bizInfo = bizContext.OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == _BPMContext.ProcInst.FormId);

            var currentStep = context.WF_Custom_InstanceItems.FirstOrDefault(x => x.StepID == bizInfo.CurrentStepId.Value);
            List<CustomWorkflowUserInfo> userinfos = currentStep.PartUsers.ToUserList();
            var currentParter = userinfos.FirstOrDefault(x => x.UserInfo.LoginName == _BPMContext.CurrentUser.LoginId);
            if (currentParter != null)
            {
                currentParter.IsApproval = true;
                currentStep.PartUsers = userinfos.ToXml();
                context.SubmitChanges();
            }

            if (userinfos.Count(x => !x.IsApproval) == 0)
            {
                //本步骤所有人已经执行审批

                ///更新当前自定义实例步骤
                if (action == "同意")
                {
                    var list = CustomWorkflowDataProcess.GetWorkItemsData(_BPMContext.ProcID, "");
                    var nextInfo = list.Where(x => x.StepID > bizInfo.CurrentStepId.Value && !string.IsNullOrEmpty(x.PartUsers)).OrderBy(x => x.OrderId).FirstOrDefault();
                    if (nextInfo != null)
                    {
                        bizInfo.CurrentStepId = nextInfo.StepID;
                    }
                    else
                    {
                        bizInfo.CurrentStepId = -1;//没有后续节点，流程结束
                    }
                }
                else
                {
                    bizInfo.CurrentStepId = -1;//不同意，流程结束
                }
                bizContext.SubmitChanges();
            }
            return true;
        }
        protected override string GetCurrentActiveName(string sn)
        {
            var info = DBContext.GetBizContext().OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == _BPMContext.ProcInst.FormId);
            var list = CustomWorkflowDataProcess.GetWorkItemsData(_BPMContext.ProcID, _BPMContext.ProcInst.FormId);
            string currentActivityName = DBContext.GetSysContext().WF_Custom_InstanceItems.FirstOrDefault(x => x.StepID == info.CurrentStepId).StepName;
            return currentActivityName;
        }
    }
}