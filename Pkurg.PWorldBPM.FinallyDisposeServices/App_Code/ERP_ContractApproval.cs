using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Pkurg.PWorldBPM.FinallyDisposeServices;

/// <summary>
///ERP合同审批回调中间层服务
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ERP_ContractApproval : ERP_Common
{
    public ERP_ContractApproval()
    {
        CallbackStatus = System.Configuration.ConfigurationManager.AppSettings["ToErpResult_ContractApproval"];
        FormType = ERP_FormType.PO;
    }

    protected override string GetErpFormIdByWorkflowId(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        string erpFormCode = Pkurg.PWorldBPM.Business.BIZ.ERP.ContractApproval.GetModelByWfId(k2_workflowId.ToString()).ErpFormId;
        return erpFormCode;
    }


    /// <summary>
    /// 合同特殊接口：验证工作流状态
    /// </summary>
    /// <param name="erpFormCode"></param>
    /// <returns></returns>
    [WebMethod]
    public ERP_CallbackResultType VerifyERPWFStatus(string erpFormCode)
    {
        Logger.logger.DebugFormat("验证工作流状态：erpFormCode:{0}", erpFormCode);
        try
        {
            ERP_CallbackResultInfo erpback = InvokeService_ERP.InvokeServiceAdvance(new CallbackInfo()
           {
               FormCode = erpFormCode,
               FormType = "PO",
               Status = "",
               ArrayParam = new List<string>() { "CHECK_WF" } //
           });

            Logger.logger.DebugFormat("erpback:{0}", erpback.Log);
            return erpback.ResultType;
        }
        catch (Exception ex)
        {
            Logger.logger.DebugFormat("NO:ex:{0}\r\n{1}", ex.Message, ex.StackTrace);
            return ERP_CallbackResultType.ERP服务器异常;
        }

    }

    /// <summary>
    /// 终止流程通知
    /// </summary>
    /// <param name="erpFormCode"></param>
    /// <returns></returns>
    [WebMethod]
    public ERP_CallbackResultType NotifyStop(string instanceID)
    {
        string erpFormCode = Pkurg.PWorldBPM.Business.BIZ.ERP.ContractApproval.GetModelByInstId(instanceID).ErpFormId;
        return NotifyStopByERPCode(erpFormCode);
    }
}
