namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///Appflow_2005 的摘要说明
    /// </summary>
    public class Appflow_2005 : AppFlowBase
    {
        //招标需求（北大资源）
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_ProjectTenderGroup bll = new Pkurg.PWorldBPM.Business.BIZ.JC.JC_ProjectTenderGroup();
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_ProjectTenderGroupInfo model = bll.GetJC_ProjectTenderGroupInfo(formId);
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