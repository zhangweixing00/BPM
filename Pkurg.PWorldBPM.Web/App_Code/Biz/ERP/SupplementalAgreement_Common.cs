using System.Web;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;

/// <summary>
///SupplementalAgreement_Common 的摘要说明
/// </summary>
public class SupplementalAgreement_Common
{
    public static string GetPoUrl()
    {
        string erpPoId = HttpContext.Current.Request["erpPoId"];
        string id=HttpContext.Current.Request["ID"];
        if (!string.IsNullOrEmpty(id))
        {
            SupplementalAgreementInfo saInfo = Pkurg.PWorldBPM.Business.BIZ.ERP.SupplementalAgreement.GetModelByInstId(id);
            if (saInfo==null)
            {
                return "";
            }
            erpPoId = saInfo.RelationContract;
        }
        else
        {
            if (string.IsNullOrEmpty(erpPoId))
            {
                return "";
            }
        }

        string baseUrl = "/Workflow/ViewPage/V_ERP_ContractApproval.aspx?id=";
        ContractApprovalInfo info = Pkurg.PWorldBPM.Business.BIZ.ERP.ContractApproval.GetRecentlyInfoByERPCode(erpPoId);
        if (info != null)
        {
            WorkFlowInstance instance = new WF_WorkFlowInstance().GetWorkFlowInstanceByFormId(info.FormID);
            return baseUrl + instance.InstanceId;
        }

        return HttpUtility.HtmlEncode("javascript:alert('BPM系统中没有其相关合同');this.close();");
    }
}