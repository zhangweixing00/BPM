namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///Appflow_2006 的摘要说明
    /// </summary>
    public class Appflow_2006 : AppFlowBase
    {
        //特殊事项申请单
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_TenderSpecialItem bll = new Pkurg.PWorldBPM.Business.BIZ.JC.JC_TenderSpecialItem();
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_TenderSpecialItemInfo model = bll.GetJC_TenderSpecialItemInfo(formId);
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