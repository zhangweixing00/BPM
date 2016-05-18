namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///合同单（投资）
    /// </summary>
    public class Appflow_3005: AppFlowBase
    {
        //获取合同单（投资）的表单信息
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfPKURGI bll =new Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfPKURGI();
            Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfPKURGIInfo model = bll.ContractAuditOfPKURGIInfo(formId);

            if (model != null)
            {
                return model.ContractContent;
            }
            else
            {
                return "";
            }
        }
    }
}