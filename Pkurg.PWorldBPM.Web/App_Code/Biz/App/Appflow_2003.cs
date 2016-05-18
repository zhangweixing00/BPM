namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///Appflow_2003 的摘要说明
    /// </summary>
    public class Appflow_2003 : AppFlowBase
    {
        //入围审批表
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_FinalistApproval bll = new Pkurg.PWorldBPM.Business.BIZ.JC.JC_FinalistApproval();
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_FinalistApprovalInfo model = bll.GetJC_FinalistApprovalInfo(formId);
            if (model != null)
            {
                return model.CheckStatus;
            }
            else
            {
                return "";
            }
        }
    }
}