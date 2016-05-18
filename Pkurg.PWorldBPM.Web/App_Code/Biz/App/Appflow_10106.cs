namespace Pkurg.PWorldBPM.Web.Biz.App
{
    /// <summary>
    /// 集采电梯审批
    /// </summary>
    public class Appflow_10106 : AppFlowBase
    {
        protected override string GetWorkflowContent(string formId)
        {
            //获取集采电梯的表单
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_ElevatorOrder bll = new Pkurg.PWorldBPM.Business.BIZ.JC.JC_ElevatorOrder();
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_ElevatorOrderInfo model = bll.GetElevatorOrder(formId);
            if (model != null)
            {
                return model.Note;
            }
            return "";
        }   

    }
}