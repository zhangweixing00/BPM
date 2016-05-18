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
///ERP_Instruction 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ERP_Instruction :ERP_Common
{
    public ERP_Instruction()
    {
        CallbackStatus = System.Configuration.ConfigurationManager.AppSettings["ToErpResult_Instruction"];
        FormType = ERP_FormType.INSTRUCTION;
    }

    protected override string GetErpFormIdByWorkflowId(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        string erpFormCode = Pkurg.PWorldBPM.Business.BIZ.ERP.Instruction.GetInstructionInfoByWfId(k2_workflowId.ToString()).ErpFormId;
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
        string erpFormCode = Pkurg.PWorldBPM.Business.BIZ.ERP.Instruction.GetInstructionInfoByInstanceId(instanceID).ErpFormId;
        return NotifyStopByERPCode(erpFormCode);
    }
}
