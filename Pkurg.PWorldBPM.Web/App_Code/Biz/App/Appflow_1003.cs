namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///招标定标
    /// </summary>
    public class Appflow_1003 : AppFlowBase
    {
        protected override string GetWorkflowContent(string formId)
        {
            //获取招标定标的表单
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_BidScaling bll = new Pkurg.PWorldBPM.Business.BIZ.JC.JC_BidScaling();
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_BidScalingInfo model = bll.GetBidScalingInfo(formId);
            if (model != null)
            {
                return model.Content;
            }
            else
            {
                return "";
            }
        }
    }
}