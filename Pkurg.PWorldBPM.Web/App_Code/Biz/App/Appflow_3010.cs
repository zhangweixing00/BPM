namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///印章申请（资源集团）
    /// </summary>
    public class Appflow_3010 :AppFlowBase
    {
        //获取印章申请（资源集团）的表单信息
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.SealOfGroup bll = new Pkurg.PWorldBPM.Business.BIZ.SealOfGroup();
            Pkurg.PWorldBPM.Business.BIZ.SealOfGroupInfo model = bll.GetSealOfGroupInfo(formId);

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