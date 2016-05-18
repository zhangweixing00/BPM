namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///流程单（资源集团所属企业）
    /// </summary>
    public class Appflow_3004: AppFlowBase
    {
        //获取流程单（资源集团所属企业）的表单
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.InstructionOfEToG bll = new Pkurg.PWorldBPM.Business.BIZ.InstructionOfEToG();
            Pkurg.PWorldBPM.Business.BIZ.InstructionOfEToGInfo model = bll.GetInstructionOfEToGInfo(formId);
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