using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Pkurg.PWorldBPM.FinallyDisposeServices;
using Pkurg.PWorldBPM.Business.Workflow;

/// <summary>
///ERP_ContractFinalAccount 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ERP_ContractFinalAccount : ERP_Common
{
    //部门编号都写在web配置里，在这里需要调用
    string CGDeptCode = System.Configuration.ConfigurationManager.AppSettings["CGDeptCode"];
    static string url = System.Configuration.ConfigurationManager.AppSettings["URL"];


    //得到erp类型
    protected override ERP_FormType GetErpFormType()
    {
        return ERP_FormType.POJS;
    }

    public ERP_ContractFinalAccount()
    {
        CallbackStatus = System.Configuration.ConfigurationManager.AppSettings["ToErpResult_ContractFinalAccount"];
        FormType = ERP_FormType.POJS;
    }

    protected override bool AfterNotify(int k2_workflowId, SerializableDictionary<string, string> dataFields, ERP_CallbackResultInfo resultInfo)
    {
        var inst = DBContext.GetSysContext().WF_WorkFlowInstance.FirstOrDefault(x => x.WFInstanceId == k2_workflowId.ToString());
        var contractfinalaccountinfo = DBContext.GetBizContext().ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == inst.FormID);
        if (contractfinalaccountinfo.ERPApproveLev!=null &&contractfinalaccountinfo.ERPApproveLev == 2)
        {
            //发送邮件
            MailService mailService = new MailService();
            string emailTitle = "【备案邮件】" + "由"+inst.CreateDeptName +inst.CreateByUserName+"于" + contractfinalaccountinfo.CreateTime + "发起的" + "【合同结算】" + inst.FormTitle + "流程已审批结束！";
            string ID = url + "/Workflow/ViewPage/V_ERP_ContractFinalAccount.aspx?id=" + inst.InstanceID;

            string emailFinallyBodyFormat = @"您好！
       <br/>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + emailTitle + @"
       <br/>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;若要查看审批意见点击此处：&nbsp;&nbsp;&nbsp;&nbsp;<a href='" + ID + @"'>查看</a>
         <br/>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;谢谢
          <br/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            bool eResult = mailService.SendEmailCustom("zhouboqin@jtmail.founder.com", emailTitle, emailFinallyBodyFormat);  
            //bool eResult = mailService.SendEmailCustom("chengxiaop@jtmail.founder.com", emailTitle, emailFinallyBodyFormat);  
        } 
        return true;
    }
    protected override string GetErpFormIdByWorkflowId(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        string id = k2_workflowId.ToString();
        WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
        var inst = DBContext.GetSysContext().WF_WorkFlowInstance.FirstOrDefault(x => x.WFInstanceId == id);
        var contractfinalaccountinfo = DBContext.GetBizContext().ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == inst.FormID);

        string erpFormCode = contractfinalaccountinfo.ErpFormId;
        return erpFormCode;

    }

    [WebMethod]
    public override ERP_CallbackResultType NotifyStartAdvance(string erpFormCode, bool isSubmit)
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

    /// <summary>
    /// 正式系统要开启
    /// </summary>
    /// <param name="k2_workflowId"></param>
    private void DoSendEmail(int k2_workflowId)
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

}
