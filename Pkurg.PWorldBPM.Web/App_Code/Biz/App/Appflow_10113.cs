namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///Appflow_10113 的摘要说明
    /// </summary>
    public class Appflow_10113 : AppFlowBase
    {
        //招标需求申请（城市公司）
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_ProjectTenderCityCompany bll = new Pkurg.PWorldBPM.Business.BIZ.JC.JC_ProjectTenderCityCompany();
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_ProjectTenderCityCompanyInfo model = bll.GetJC_ProjectTenderCityCompanyInfo(formId);
            if (model != null)
            {
                return model.Substance;
            }
            else
            {        
                return "";
            }
        }
    }
}