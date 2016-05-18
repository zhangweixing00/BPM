namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///Appflow_3002 的摘要说明
    /// </summary>
    public class Appflow_3002 : AppFlowBase
    {
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.InstructionOfEToI bll = new Pkurg.PWorldBPM.Business.BIZ.InstructionOfEToI();
            Pkurg.PWorldBPM.Business.BIZ.InstructionOfEToIInfo model = bll.GetInstructionOfEToIInfo(formId);

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