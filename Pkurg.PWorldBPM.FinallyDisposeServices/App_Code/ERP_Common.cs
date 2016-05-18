using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Pkurg.PWorldBPM.FinallyDisposeServices;

/// <summary>
///ERP统一回调服务的基类
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ERP_Common : WF_FinallyServiceBase
{
    /// <summary>
    /// 是否为测试系统
    /// 测试系统不发邮件
    /// </summary>
    public bool IsDebug { get; set; }

    private string callbackStatus;

    public string CallbackStatus
    {
        get
        {
            if (string.IsNullOrEmpty(callbackStatus))
            {
                callbackStatus = System.Configuration.ConfigurationManager.AppSettings["ToErpResult_default"];
            }
            return callbackStatus;
        }
        set { callbackStatus = value; }
    }

    public ERP_FormType FormType { get; set; }

    public string FormCode { get; set; }

    public ERP_Common()
    {
        FormType = ERP_FormType.UnKnown;
        string debugString = System.Configuration.ConfigurationManager.AppSettings["IsDebug"];
        IsDebug = string.IsNullOrEmpty(debugString) || debugString == "1";
    }
    /// <summary>
    /// 返回当前表单类型
    /// </summary>
    /// <returns></returns>
    protected virtual ERP_FormType GetErpFormType()
    {
        if (FormType == ERP_FormType.UnKnown)
        {
            throw new NotImplementedException("请直接返回FormType的方法");

        }
        return FormType;
    }

    protected virtual string GetErpFormIdByWorkflowId(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        if (string.IsNullOrEmpty(FormCode))
        {
            throw new NotImplementedException("请实现根据流程ID获得ERPID的方法");
        }
        return FormCode;
    }

    /// <summary>
    /// 流程结束准备通知前处理
    /// </summary>
    /// <param name="k2_workflowId"></param>
    /// <param name="dataFields"></param>
    /// <returns></returns>
    protected virtual bool BeforeNotify(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        return true;
    }

    /// <summary>
    /// 流程结束准备通知后处理
    /// </summary>
    /// <param name="k2_workflowId"></param>
    /// <param name="dataFields"></param>
    /// <param name="resultInfo"></param>
    /// <returns></returns>
    protected virtual bool AfterNotify(int k2_workflowId, SerializableDictionary<string, string> dataFields, ERP_CallbackResultInfo resultInfo)
    {
        return true;
    }

    /// <summary>
    /// 可以继续覆盖重写
    /// </summary>
    /// <param name="k2_workflowId"></param>
    /// <param name="dataFields"></param>
    /// <returns></returns>
    public override Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo DoServiceEvent(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        Logger.logger.DebugFormat("Params:{0},{1}", k2_workflowId, dataFields.Keys.Count);
        foreach (KeyValuePair<string, string> item in dataFields)
        {
            Logger.logger.DebugFormat("df:{0}-{1}", item.Key, item.Value);
        }
        Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo info = new Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo();
        try
        {
            string erpFormCode = GetErpFormIdByWorkflowId(k2_workflowId, dataFields);
            string[] toErpString = CallbackStatus.Split(',');
            string resultString = dataFields["IsPass"] == "1" ? toErpString[0] : toErpString[1];
            Logger.logger.DebugFormat("Proc:{0},{1}", erpFormCode, resultString);
            if (BeforeNotify(k2_workflowId, dataFields))
            {
                ERP_CallbackResultInfo resultInfo = InvokeService_ERP.InvokeServiceAdvance(new CallbackInfo()
                 {
                     FormCode = erpFormCode,
                     FormType = GetErpFormType().ToString(),
                     Status = resultString
                 });

                AfterNotify(k2_workflowId, dataFields, resultInfo);
                if (resultInfo.ResultType==ERP_CallbackResultType.调用成功)
                {
                    info.IsSuccess = true;
                }
                else
                {
                    info.IsSuccess = false;
                    info.ExecException = "接口调用成功，erp接口返回失败："+resultInfo.resultXml;
                }
            }
            else
            {
                info.IsSuccess = false;
            }
        }
        catch (Exception ex)
        {
            info.ExecException = ex.StackTrace;
            info.IsSuccess = false;
        }
        Logger.logger.DebugFormat("Result:{0}", info.IsSuccess);
        Logger.logger.DebugFormat("Result-e:{0}", info.ExecException);
        if (!IsDebug)
        {
            DoSendEmail(k2_workflowId);
        }

        return info;
    }


    /// <summary>
    /// 正式系统要开启
    /// </summary>
    /// <param name="k2_workflowId"></param>
    protected void DoSendEmail(int k2_workflowId)
    {
        try
        {
            MailService mailService = new MailService();
            bool eResult = mailService.SendEndEmail(k2_workflowId);
            Logger.logger.DebugFormat("Email--Result:{0}", eResult);
        }
        catch (Exception ex)
        {
            Logger.logger.DebugFormat("Email--Result:{0}", false);
            Logger.logger.DebugFormat("Email--Result-e:{0}", ex.StackTrace);
        }
    }

    /// <summary>
    /// 一般erp流程发起验证和通知方式
    /// </summary>
    /// <param name="erpFormCode"></param>
    /// <param name="isSubmit"></param>
    /// <returns></returns>
    [WebMethod]
    public  virtual ERP_CallbackResultType NotifyStartAdvance(string erpFormCode, bool isSubmit)
    {
        Logger.logger.DebugFormat("NotifyStartAdvance_erpFormCode:{0},{1}", erpFormCode,isSubmit);
        try
        {
            string[] toErpString = CallbackStatus.Split(',');
            ERP_CallbackResultInfo erpback = InvokeService_ERP.InvokeServiceAdvance(new CallbackInfo()
            {
                FormCode = erpFormCode,
                FormType = GetErpFormType().ToString(),
                Status = !isSubmit?"":toErpString[2],
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

    /// <summary>
    /// 终止流程通知
    /// </summary>
    /// <param name="erpFormCode"></param>
    /// <returns></returns>
    [WebMethod]
    public virtual ERP_CallbackResultType NotifyStopByERPCode(string erpFormCode)
    {
        Logger.logger.DebugFormat("NotifyStopByERPCode_erpFormCode:{0}", erpFormCode);
        try
        {
            ERP_CallbackResultInfo erpback = InvokeService_ERP.InvokeServiceAdvance(new CallbackInfo()
            {
                FormCode = erpFormCode,
                FormType = FormType.ToString(),
                Status = "",
                ArrayParam = new List<string>() { "FORCE" }
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


}
public enum ERP_FormType
{
    PAYMENT_APPLY,
    INSTRUCTION,
    PO,
    SUPPLMENT,
    C2CPBAL,
    UnKnown,
    POJS
}
