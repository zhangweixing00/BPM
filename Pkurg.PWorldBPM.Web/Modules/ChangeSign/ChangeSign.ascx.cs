using System;
using Pkurg.PWorldBPM.Common;

public partial class Modules_ChangeSign_ChangeSign : UControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Visible = IsOpen();
        //if (K2_TaskItem.ActivityInstanceDestination.Name =="集团会签" || K2_TaskItem.ActivityInstanceDestination.Name == "集团采购管理部意见" || K2_TaskItem.ActivityInstanceDestination.Name == "集团相关部门意见")
        //{
        //    this.Visible = IsOpen();
        //}
        //else
        //{
        //    this.Visible = false;
        //}

    }

    public bool IsOpen()
    {
        if (K2_TaskItem == null || _BPMContext.ProcInst == null)
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(_BPMContext.ProcInst.AppCode))
        {
            return true;
        }
        string activityName = K2_TaskItem.ActivityInstanceDestination.Name;

        switch (_BPMContext.ProcInst.AppCode)
        {
            case "3014":
                return true;
            case "1003":
            case "2003":
            case "10113":
            case "2005":
            case "2006":
                if (activityName == "集团会签" || activityName == "集团采购管理部意见" || activityName == "集团相关部门意见")
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    protected override void OnPreRender(EventArgs e)
    {
        div_content.InnerHtml = string.Format("<a  href='#' onclick='ChangeSign(\"{0}\",\"{1}\")'>转签</a>", ProcId, _BPMContext.Sn);
        base.OnPreRender(e);
    }
    public string ClientId { get; set; }
}