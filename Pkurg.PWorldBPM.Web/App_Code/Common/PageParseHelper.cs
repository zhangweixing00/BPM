using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.Common.Info;

/// <summary>
///ControlParseHelper 的摘要说明
/// </summary>

public class PageParseHelper
{
    string bindAttrName = "CBind";

    public PageParseHelper(List<FormDataInfo> dataInfos)
    {
        DataInfos = dataInfos;
    }

    public List<FormDataInfo> DataInfos { get; set; }

    //针对Page
    public void CollectionValue(Page page)
    {
        foreach (Control item in page.Form.Controls)
        {
            CollectionValue(item);
        }
    }

    public void BindValue(Page page)
    {
        foreach (Control item in page.Form.Controls)
        {
            BindValue(item);
        }
    }

    //针对控件
    public void CollectionValue(Control mainControl)
    {
        foreach (Control control in mainControl.Controls)
        {
            if (control is WebControl)
            {
                if (((WebControl)control).Attributes[bindAttrName] != null)
                {
                    FormDataInfo item = new FormDataInfo();
                    DataInfos.Add(item);
                    string paramName = ((WebControl)control).Attributes[bindAttrName];
                    string format = ((WebControl)control).Attributes["DataFormat"] ?? "";
                    item.ParamName = paramName;
                    item.ControlType = control.GetType().FullName;
                    LoadControlValue(control, item, format);
                }
            }
            if (control.HasControls())
            {
                //bool flag = true;
                //IIsolation isolation = control as IIsolation;
                //if (isolation != null)
                //{
                //    flag = isolation.CollectingDown;
                //}
                //if (flag)
                //{
                CollectionValue(control);
                // }
            }

        }
    }
    private void LoadControlValue(Control control, FormDataInfo item, string format)
    {
        if (control is TextBox)
        {
            TextBox saveControl = control as TextBox;
            item.TxtValue = string.IsNullOrEmpty(format) ? saveControl.Text : string.Format(format, saveControl.Text);
        }
        else if (control is Label)
        {
            Label saveControl = control as Label;
            item.TxtValue = string.IsNullOrEmpty(format) ? saveControl.Text : string.Format(format, saveControl.Text);
        }
        else if (control is CheckBox)
        {
            CheckBox saveControl = control as CheckBox;
            item.complexDatas.Add(new ComplexData()
            {
                Checked = saveControl.Checked,
                Text = saveControl.Text
            });
        }
        else if (control is CheckBoxList)
        {
            CheckBoxList saveControl = control as CheckBoxList;
            item.complexDatas.Clear();
            foreach (ListItem subControl in saveControl.Items)
            {
                item.complexDatas.Add(new ComplexData()
                {
                    Checked = subControl.Selected,
                    Text = subControl.Text,
                    Value = subControl.Value
                });
            }
        }
        else if (control is DropDownList)
        {
            DropDownList saveControl = control as DropDownList;
            foreach (ListItem subControl in saveControl.Items)
            {
                item.complexDatas.Add(new ComplexData()
                {
                    Checked = subControl.Selected,
                    Text = subControl.Text,
                    Value = subControl.Value
                });
            }
        }
        else if (control is RadioButtonList)
        {
            RadioButtonList saveControl = control as RadioButtonList;
            foreach (ListItem subControl in saveControl.Items)
            {
                item.complexDatas.Add(new ComplexData()
                {
                    Checked = subControl.Selected,
                    Text = subControl.Text,
                    Value = subControl.Value
                });
            }
        }
        else if (control is Image)
        {
            Image saveControl = control as Image;
            item.TxtValue = saveControl.ImageUrl;

        }
        else if (control is HiddenField)
        {
            HiddenField saveControl = control as HiddenField;
            item.TxtValue = saveControl.Value;

        }
    }


    public void BindValue(Control mainControl)
    {
        IEnumerable<Control> enumerable =
            from ct in mainControl.Controls.OfType<Control>()
            where ct.HasControls() || (!ct.HasControls() && ct is IAttributeAccessor && !string.IsNullOrEmpty(((IAttributeAccessor)ct).GetAttribute(bindAttrName)))
            select ct;
        foreach (Control current in enumerable)
        {
            if (current is WebControl)
            {
                if (((WebControl)current).Attributes[bindAttrName] != null)
                {
                    string paramName = ((WebControl)current).Attributes[bindAttrName];
                    // string areaId = ((WebControl)current).Attributes[area] ?? "";

                    FormDataInfo item = DataInfos.Find(x => x.ParamName == paramName && x.ControlType == current.GetType().FullName);
                    if (item != null)
                    {
                        SetControlValue(current, item);
                    }
                }
            }


            this.BindValue(current);

        }
    }
    public void SetControlValue(Control control, FormDataInfo item)
    {
        if (control is TextBox)
        {
            TextBox saveControl = control as TextBox;
            saveControl.Text = item.TxtValue;
        }
        else if (control is Label)
        {
            Label saveControl = control as Label;
            saveControl.Text = item.TxtValue;
        }
        else if (control is CheckBox)
        {
            CheckBox saveControl = control as CheckBox;
            if (item.complexDatas.Count > 0)
            {
                saveControl.Text = item.complexDatas[0].Text;
                saveControl.Checked = item.complexDatas[0].Checked;
            }
        }
        else if (control is CheckBoxList)
        {
            CheckBoxList saveControl = control as CheckBoxList;
            saveControl.Items.Clear();
            foreach (ComplexData dataItem in item.complexDatas)
            {
                saveControl.Items.Add(new ListItem()
                {
                    Text = dataItem.Text,
                    Value = dataItem.Value,
                    Selected = dataItem.Checked
                });
            }
        }
        else if (control is DropDownList)
        {
            DropDownList saveControl = control as DropDownList;
            saveControl.Items.Clear();
            foreach (ComplexData dataItem in item.complexDatas)
            {
                saveControl.Items.Add(new ListItem()
                {
                    Text = dataItem.Text,
                    Value = dataItem.Value,
                    Selected = dataItem.Checked
                });
            }
        }
        else if (control is RadioButtonList)
        {
            RadioButtonList saveControl = control as RadioButtonList;
            saveControl.Items.Clear();
            foreach (ComplexData dataItem in item.complexDatas)
            {
                saveControl.Items.Add(new ListItem()
                {
                    Text = dataItem.Text,
                    Value = dataItem.Value,
                    Selected = dataItem.Checked
                });
            }
        }
        else if (control is Image)
        {
            Image saveControl = control as Image;
            saveControl.ImageUrl = item.TxtValue;
        }
        else if (control is HiddenField)
        {
            HiddenField saveControl = control as HiddenField;
            saveControl.Value = item.TxtValue;
        }
    }
}