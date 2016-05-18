using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
///ControlHelper 的摘要说明
/// </summary>
public class ControlHelper
{
    public enum ProcessType
    {
        EA=0,//可读=0,
        VA,//可见
        UEA,
        UVA
    }

    public string ActivityName { get; set; }
	public ControlHelper(string activityName)
	{
        ActivityName = activityName;
	}

    public ProcessType CurrentType { get; set; }

    public void SettingPagePesmission(Page page)
    {
        SettingPagePesmission(page, ProcessType.EA);
        SettingPagePesmission(page, ProcessType.VA);
    }

    public void SettingPagePesmission(Page page,ProcessType type)
    {
        CurrentType = type;
        string enableControl = type.ToString();
        foreach (Control control in page.Form.Controls)
        {
            if (control is WebControl)
            {
                if (((WebControl)control).Attributes[enableControl] != null)
                {
                    SetControlStatus(control, ((WebControl)control).Attributes[enableControl]);
                    //control.Visible = false;
                }
                else
                {
                    if (control.HasControls())
                    {
                        SettingControlPesmission(control);
                    }
                }
            }
            else
            {
                if (control.HasControls())
                {
                    SettingControlPesmission(control);
                }
            }
        }
    }

    private void SetControlStatus(Control c, string names)
    {
        if (names.Split('|').ToList().Contains(ActivityName.ToLower())||names=="*")
        {
            if (CurrentType==ProcessType.EA)
            {
                ((WebControl)c).Enabled = true;
            }
            if (CurrentType==ProcessType.VA)
            {
                ((WebControl)c).Visible = true;
            }
        }
        else
        {
            if (CurrentType == ProcessType.EA)
            {
                ((WebControl)c).Enabled = false;
            }
            if (CurrentType == ProcessType.VA)
            {
                ((WebControl)c).Visible = false;
            }
        }
    }

    private void SettingControlPesmission(Control control)
    {
        string enableControl = CurrentType.ToString();
        foreach (Control childControl in control.Controls)
        {
            if (childControl is WebControl)
            {
                if (((WebControl)childControl).Attributes[enableControl] != null)
                {
                    SetControlStatus(childControl, ((WebControl)childControl).Attributes[enableControl]);
                }
                else
                {
                    if (childControl.HasControls())
                    {
                        SettingControlPesmission(childControl);
                    }
                }
            }
            else
            {
                if (childControl.HasControls())
                {
                    SettingControlPesmission(childControl);
                }
            }
        }
    }
}