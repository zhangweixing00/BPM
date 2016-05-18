<%@ WebHandler Language="C#" Class="ViewPageForERP" %>

using System.Linq;
using System.Web;

public class ViewPageForERP : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest Request = context.Request;
        //通过APPID得到页面名称
        string appId = Request["appId"];
        //string erpId = Request["erpId"];
        string erpId = HttpContext.Current.Request["erpFormId"];
        string instanceId = "";

        switch (appId)
        {
            //合同
            case "10109":
                //case "HT":
                //通过erpid得到formid，再通过formid得到instanceid
                string formid1 = Pkurg.PWorldBPM.Business.BIZ.ERP.ContractApproval.GetContractApprovalFormidByErpId(erpId);
                if (!string.IsNullOrEmpty(formid1))
                {
                    instanceId = Pkurg.PWorldBPM.Business.BIZ.ERP.ContractApproval.GetContractApprovalInstanceIdByFormId(formid1);
                }
                else
                {
                    Alert("该合同不在BPM的相关流程中，请在ERP中查看该表单相关附件！");
                    return;
                }
                break;
            //请示报告
            case "10107":
                //case "QS":
                //通过erpid得到formid，再通过formid得到instanceid
                string formid2 = Pkurg.PWorldBPM.Business.BIZ.ERP.Instruction.GetInstructionFormidByErpId(erpId);
                if (!string.IsNullOrEmpty(formid2))
                {
                    instanceId = Pkurg.PWorldBPM.Business.BIZ.ERP.Instruction.GetInstructionInstanceIdByFormId(formid2);
                }
                else
                {
                    Alert("该请示报告不在BPM的相关流程中，请在ERP中查看该表单相关附件！");
                    return;
                }
                break;
            //付款申请
            case "10105":
                //case "FK":
                //通过erpid得到formid，再通过formid得到instanceid
                string formid3 = Pkurg.PWorldBPM.Business.BIZ.ERP.PaymentApplication.GetPaymentApplicationFormidByErpId(erpId);
                if (!string.IsNullOrEmpty(formid3))
                {
                    instanceId = Pkurg.PWorldBPM.Business.BIZ.ERP.PaymentApplication.GetPaymentApplicationInstanceIdByFormId(formid3);
                }
                else
                {
                    Alert("该付款申请不在BPM的相关流程中，请在ERP中查看该表单相关附件！");
                    return;
                }
                break;
            //补充协议
            case "2004":
                //case "SA":
                //通过erpid得到formid，再通过formid得到instanceid
                string formid4 = Pkurg.PWorldBPM.Business.BIZ.ERP.SupplementalAgreement.GetSupplementalAgreementFormidByErpId(erpId);
                if (!string.IsNullOrEmpty(formid4))
                {
                    instanceId = Pkurg.PWorldBPM.Business.BIZ.ERP.SupplementalAgreement.GetSupplementalAgreementFormidByFormId(formid4);
                }
                else
                {
                    Alert("该补充协议不在BPM的相关流程中，请在ERP中查看该表单相关附件！");
                    return;
                }
                break;
            //合同结算
            case "10111":
                var contractfinalaccountinfo = DBContext.GetBizContext().ERP_ContractFinalAccount.FirstOrDefault(x => x.ErpFormId == erpId);
                string formid5 = contractfinalaccountinfo.FormID;
                if (!string.IsNullOrEmpty(formid5))
                {
                    instanceId = DBContext.GetBizContext().ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == formid5).ToString();
                }
                else
                {
                    Alert("该合同结算不在BPM的相关流程中，请在ERP中查看该表单相关附件！");
                    return;
                }
                break;  
            default:
                break;
        }

        string pageUrl = string.Format("ViewPageHandler.ashx?id={0}", instanceId);

        context.Response.Redirect(pageUrl);
    }
    public void Alert(string msg)
    {
        HttpContext.Current.Response.Write(string.Format("<script>alert('{0}');</script>", msg));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}