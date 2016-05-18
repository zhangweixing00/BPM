namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///合同单（投资所属企业）
    /// </summary>
    public class Appflow_3006  : AppFlowBase
    {
        //获取合同单（投资所属企业）的表单信息
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfEToI bll = new Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfEToI();
            Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfEToIInfo model = bll.GetContractAuditOfEToIInfo(formId);

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