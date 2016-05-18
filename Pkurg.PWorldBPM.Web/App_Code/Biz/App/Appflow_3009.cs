namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///印章申请（投资）
    /// </summary>
    public class Appflow_3009 : AppFlowBase
    {
        //获取印章申请（投资）的表单信息
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.SealOfPKURGI bll = new Pkurg.PWorldBPM.Business.BIZ.SealOfPKURGI();
            Pkurg.PWorldBPM.Business.BIZ.SealOfPKURGIInfo model = bll.GetSealOfPKURGIInfo(formId);

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