namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///合同单（资源集团）
    /// </summary>
    public class Appflow_3007 : AppFlowBase
    {
        //获取合同单（资源集团）的表单信息
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfGroup bll = new Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfGroup();
            Pkurg.PWorldBPM.Business.BIZ.OA.ContractAuditOfGroupInfo model = bll.GetContractAuditOfGroupInfo(formId);

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