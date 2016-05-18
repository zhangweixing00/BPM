namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    ///流程单（资源集团）
    /// </summary>
    public class Appflow_3003 : AppFlowBase
    {
        //获取流程单（资源集团）的表单信息
        protected override string GetWorkflowContent(string formId)
        {
            Pkurg.PWorldBPM.Business.BIZ.InstructionOfGroup bll = new Pkurg.PWorldBPM.Business.BIZ.InstructionOfGroup();
            Pkurg.PWorldBPM.Business.BIZ.InstructionOfGroupInfo model = bll.GetInstructionOfGroupInfo(formId);

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