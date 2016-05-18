using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Pkurg.PWorldBPM.FinallyDisposeServices;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Text;
using System.Reflection;

/// <summary>
///付款申请接口
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ERP_PaymentApplication :ERP_Common
{
    public ERP_PaymentApplication()
    {
        CallbackStatus = System.Configuration.ConfigurationManager.AppSettings["ToErpResult"];
        FormType = ERP_FormType.PAYMENT_APPLY;
    }

    protected override string GetErpFormIdByWorkflowId(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        string erpFormCode = Pkurg.PWorldBPM.Business.BIZ.ERP.PaymentApplication.GetPaymentApplicationInfoByWfId(k2_workflowId.ToString()).ErpFormId;
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
        string erpFormCode = Pkurg.PWorldBPM.Business.BIZ.ERP.PaymentApplication.GetPaymentApplicationInfoByInstanceId(instanceID).ErpFormId;
        return NotifyStopByERPCode(erpFormCode);
    }
}
