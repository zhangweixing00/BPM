using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
///OA_InstructionOfEToI 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class OA_InstructionOfEToI : Pkurg.PWorldBPM.FinallyDisposeServices.FinallyDisposeServiceBase
{

    public OA_InstructionOfEToI()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    public override Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo DoServiceEvent(int k2_workflowId, Pkurg.PWorldBPM.FinallyDisposeServices.SerializableDictionary<string, string> dataFields)
    {
        Logger.logger.DebugFormat("OA_InstructionOfEToI_Params:{0},{1}", k2_workflowId, dataFields["IsReport"] == "1" ? "上报" : "不上报");

        Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo info = new Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo();
        try
        {
            if (dataFields["IsReport"] == "1")
            {
                Pkurg.PWorldBPM.Business.BIZ.InstructionOfEToIInfo companyInfo = new Pkurg.PWorldBPM.Business.BIZ.InstructionOfEToI().GetInfoByWfId(k2_workflowId.ToString());
                Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstanceInfo instanceInfo = Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstances.GetWorkFlowInstanceByWfId(k2_workflowId.ToString());
                Pkurg.PWorldBPM.Business.Controls.WF_Approval_RecordInfo flowManagerInfo = Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstances.GetApproval_RecordByIdAndName(instanceInfo.InstanceID, "流程审核员审核");
                if (companyInfo != null)
                {
                    ///增加表单
                    string formId = Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstances.GetSerialNumber("OA_IQS_");
                    new Pkurg.PWorldBPM.Business.BIZ.InstructionOfPKURGI().Insert(new Pkurg.PWorldBPM.Business.BIZ.InstructionOfPKURGIInfo()
                    {
                        Content = companyInfo.Content,
                        Title = companyInfo.Title,
                        SecurityLevel = companyInfo.SecurityLevel,
                        UrgenLevel = companyInfo.UrgenLevel,
                        Mobile = companyInfo.Mobile,
                        //UserName = companyInfo.UserName,
                        UserName = flowManagerInfo.ApproveByUserName,
                        DeptCode = companyInfo.DeptCode,
                        DeptName = companyInfo.DeptName,
                        FormID = formId,
                        RelatedFormID = companyInfo.FormID
                        //relation
                    });

                    ///增加实例
                    string instanceId = Guid.NewGuid().ToString();
                    Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstances.AddWorkFlowInstance(new Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstanceInfo()
                    {
                        AppID = "3001",
                        InstanceID = instanceId,

                        FormID = formId,
                        FormTitle = instanceInfo.FormTitle,
                        WFStatus = "0",

                        CreateAtTime = DateTime.Now,
                        CreateByUserCode = flowManagerInfo.ApproveByUserCode,
                        CreateByUserName = flowManagerInfo.ApproveByUserName,
                        UpdateAtTime = DateTime.Now,
                        UpdateByUserCode = flowManagerInfo.ApproveByUserCode,
                        UpdateByUserName = flowManagerInfo.ApproveByUserName,
                        CreateDeptCode = instanceInfo.CreateDeptCode,
                        CreateDeptName = instanceInfo.CreateDeptName,
                        WFInstanceId = "0"
                    });

                    ///关联流程
                    Pkurg.PWorldBPM.Business.Controls.WF_Relation.AddRelatedFlowInfo(instanceId, instanceInfo.InstanceID, flowManagerInfo.ApproveByUserName);
                    Logger.logger.Debug("上报成功");
                }
            }
            else
            {
                info.ExecException = "无需上报";
            }

            info.IsSuccess = true;
        }
        catch (Exception ex)
        {
            info.ExecException = ex.Message + "\r\n" + ex.StackTrace;
            Logger.logger.DebugFormat("上报失败：{0}\r\n{1}", ex.Message, ex.StackTrace);
        }
        return info;
    }
}
