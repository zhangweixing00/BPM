namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///印章申请（商业公司）
    /// </summary>
    public class Appflow_3012 :AppFlowBase
    {
        //获取印章申请（商业公司）的表单信息
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.SealOfBC bll = new Pkurg.PWorldBPM.Business.BIZ.SealOfBC();
            Pkurg.PWorldBPM.Business.BIZ.SealOfBCInfo model = bll.GetSealOfBCInfo(formId);

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