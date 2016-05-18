namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///流程单[投资]
    /// </summary>
    public class Appflow_3001 : AppFlowBase
    {
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.InstructionOfPKURGI bll = new Pkurg.PWorldBPM.Business.BIZ.InstructionOfPKURGI();
            Pkurg.PWorldBPM.Business.BIZ.InstructionOfPKURGIInfo model = bll.GetInstructionInfo(formId);

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