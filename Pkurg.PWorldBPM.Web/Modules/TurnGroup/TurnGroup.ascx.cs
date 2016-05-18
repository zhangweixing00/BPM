using System;
using Pkurg.PWorldBPM.Common;

public partial class Modules_TurnGroup_TurnGroup : UControlBase
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
        div_content.InnerHtml = string.Format("<a  href='#' onclick='TurnGroup(\"{0}\",\"{1}\")'>转出到组</a>", ProcId, _BPMContext.Sn);
        base.OnPreRender(e);
    }
}