using System;
using System.Text;
using Pkurg.PWorldBPM.Common;

/// <summary>
/// 目前仅针对审批页面
/// </summary>
public partial class Modules_Menu_WF_Menu : UControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder content = new StringBuilder();

        if (K2_TaskItem != null)
        {
            bool isAddSign = IsAddSign(_BPMContext.Sn);
            bool isChangeSign = IsChangeSign(_BPMContext.Sn);
            if (isAddSign)
            {
                //处理加签按钮
                content.AppendFormat(linkFormat, "提交", "addSignback", 1);
            }
                //是否需要
            else if(isChangeSign)
            {
                //处理转签按钮
                content.AppendFormat(linkFormat, "提交", "changeSignback", 1);
            }
            else
            {
                //正常审批按钮
                for (int index = 0; index < K2_TaskItem.Actions.Count; index++)
                {
                    string actionName = K2_TaskItem.Actions[index].Name;
                    if (actionName == "更新")
                    {
                        continue;
                    }
                    content.AppendFormat(linkFormat, actionName, actionName, 1);
                }
                if (K2_TaskItem.ActivityInstanceDestination.Name == "会签")
                {
                    c_AddSignDeptInner.Visible = true;
                    //content.AppendFormat(linkFormat, "部门内处理", "addSignInner", 0);
                }
                else
                {
                    c_AddSign.Visible = true;
                    //content.AppendFormat(linkFormat, "加签", "addsign", 0);
                }
            }
        }
        //content.AppendFormat(linkFormat, "关闭", "close", 0);

        Options.InnerHtml = content.ToString();
    }

    public bool IsAddSign(string sn)
    {
        try
        {
            string addApproversBy_Value="";
            bool isOk=WorkflowHelper.GetActivityDataField(sn, "AddApproversBy", _BPMContext.CurrentUser.LoginId, out addApproversBy_Value);
            if (isOk&&!string.IsNullOrEmpty(addApproversBy_Value))
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool IsChangeSign(string sn)
    {
        try
        {
            string changeApproversBy_Value = "";
            bool isOk = WorkflowHelper.GetActivityDataField(sn, "ChangeApproversBy", _BPMContext.CurrentUser.LoginId, out changeApproversBy_Value);
            if (isOk && !string.IsNullOrEmpty(changeApproversBy_Value))
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    string linkFormat = "<li><a href='#' onclick='DoAction(\"{1}\",\"{2}\")'>{0}</a></li>";

    public bool IsStart { get; set; }
}