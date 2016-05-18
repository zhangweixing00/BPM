using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Pkurg.PWorldBPM.FinallyDisposeServices;

/// <summary>
///ERP_SupplementalAgreement 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ERP_SupplementalAgreement :ERP_Common {


    protected override ERP_FormType GetErpFormType()
    {
        return ERP_FormType.SUPPLMENT;
    }

    public ERP_SupplementalAgreement()
    {
        CallbackStatus = System.Configuration.ConfigurationManager.AppSettings["ToErpResult_SUPPLEMENT"];
        FormType = ERP_FormType.SUPPLMENT;
    }

    protected override string GetErpFormIdByWorkflowId(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        string erpFormCode = Pkurg.PWorldBPM.Business.BIZ.ERP.SupplementalAgreement.GetModelByWfId(k2_workflowId.ToString()).ErpFormId;
        return erpFormCode;
    }

    /// <summary>
    /// 终止流程通知
    /// </summary>
    /// <param name="erpFormCode"></param>
    /// <returns></returns>
    [WebMethod]
    public ERP_CallbackResultType NotifyStop(string instanceID)
    {
        string erpFormCode = Pkurg.PWorldBPM.Business.BIZ.ERP.SupplementalAgreement.GetModelByInstId(instanceID).ErpFormId;
        return NotifyStopByERPCode(erpFormCode);
    }

    [WebMethod]
    public  override ERP_CallbackResultType NotifyStartAdvance(string erpFormCode, bool isSubmit)
    {
        Logger.logger.DebugFormat("NotifyStartAdvance_erpFormCode:{0},{1}", erpFormCode, isSubmit);
        try
        {
            ERP_CallbackResultInfo erpback = InvokeService_ERP.InvokeServiceAdvance(new CallbackInfo()
            {
                FormCode = erpFormCode,
                FormType = GetErpFormType().ToString(),
                Status = "处理中",
                ArrayParam = isSubmit ? new List<string>() : new List<string>() { "CHECK_STATUS" } //
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

    ///// <summary>
    ///// 终止流程通知
    ///// </summary>
    ///// <param name="erpFormCode"></param>
    ///// <returns></returns>
    //[WebMethod]
    //public override ERP_CallbackResultType NotifyStopByERPCode(string erpFormCode)
    //{
    //    Logger.logger.DebugFormat("NotifyStopByERPCode_erpFormCode:{0}", erpFormCode);
    //    try
    //    {
    //        ERP_CallbackResultInfo erpback = InvokeService_ERP.InvokeServiceAdvance(new CallbackInfo()
    //        {
    //            FormCode = erpFormCode,
    //            FormType = FormType.ToString(),
    //            Status = "未完成",
    //            ArrayParam = new List<string>() { "FORCE" }
    //        });

    //        Logger.logger.DebugFormat("erpback:{0}", erpback.Log);
    //        return erpback.ResultType;
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.logger.DebugFormat("NO:ex:{0}\r\n{1}", ex.Message, ex.StackTrace);
    //        return ERP_CallbackResultType.ERP服务器异常;
    //    }

    //}
}
