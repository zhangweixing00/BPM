using System;
using Pkurg.PWorldBPM.Common;

public partial class Modules_AddSign_AddSign : UControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (K2_TaskItem != null && !K2_TaskItem.ActivityInstanceDestination.Name.EndsWith("会签") && K2_TaskItem.ActivityInstanceDestination.Name != "流程审核员审核")
        {
            this.Visible = true;
        }
        else
        {
            this.Visible = false;
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        div_content.InnerHtml= string.Format("<a  href='#' onclick='AddSign(\"{0}\",\"{1}\")'>加签</a>", ProcId, _BPMContext.Sn);
        base.OnPreRender(e);
    }
    //public void BeginAddSign(string instId,string sn)
    //{
    //    DisplayMessage.ExecuteJs(string.Format("AddSign('{0}','{1}')", instId,sn));
    //}

    public string ClientId { get; set; }
}