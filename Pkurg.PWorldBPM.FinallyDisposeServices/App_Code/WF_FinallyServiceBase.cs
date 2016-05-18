using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Pkurg.PWorldBPM.FinallyDisposeServices;

/// <summary>
///回调服务的基类
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class WF_FinallyServiceBase : FinallyDisposeServiceBase
{
    protected override bool BeforeDoServiceEvent(int k2_workflowId)
    {
        try
        {
            string isPassLog = "更新实例结果：";
            if (!dataFields.ContainsKey("IsPass"))
            {
                isPassLog += "isPass不存在";
                Logger.logger.Debug(isPassLog);
                return false;
            }
            var context = DBContext.GetSysContext();
            var instInfo = context.WF_WorkFlowInstance.FirstOrDefault(x => x.WFInstanceId == k2_workflowId.ToString());
            if (instInfo == null)
            {
                isPassLog += "实例不存在";
                Logger.logger.Debug(isPassLog);
                return false;
            }
            instInfo.IsPass = dataFields["IsPass"] == "1" ? 1 : 0;
            context.SubmitChanges();
            isPassLog += (instInfo.IsPass.Value == 1 ? "通过" : "拒绝");
            Logger.logger.Debug(isPassLog);
            return true;
        }
        catch (Exception ex)
        {
            Logger.logger.DebugFormat("InsertResult-e:{0}\r\n{1}", ex.Message,ex.StackTrace);
            return false;
        }

    }
}
