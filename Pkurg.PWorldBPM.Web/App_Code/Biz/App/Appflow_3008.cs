namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///合同单（资源集团所属企业）
    /// </summary> 
    public class Appflow_3008 : AppFlowBase
    {
        //获取合同单（资源集团所属企业）的表单信息
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfEToG bll = new Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfEToG();
            Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfEToGInfo model = bll.GetContractAuditOfEToGInfo(formId);

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