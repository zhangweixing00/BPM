using System;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Common;

public partial class Modules_ApprovalBox_ApprovalBox_IT : UControlBase, IApprovalBox
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (K2_TaskItem != null)
        {
            try
            {
                div_Hidden.InnerHtml = string.Format("<input id='hf_OpId' type='hidden' value='{0}'/>", tb_Opinion.ClientID);

            }
            catch (Exception ex)
            {
                LoggerR.logger.DebugFormat("AB.ex:{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }
        else
        {
            tb_Opinion.Visible = false;
        }
        //LoggerR.logger.Debug("AB");
        div_Opinion.InnerHtml = GetCurrentActivityOption();
    }
    public string Node { get; set; }

    private string _option;

    public string OptionText
    {
        get { return tb_Opinion.Text; }
        set { _option = value; }
    }
    public string OptionId
    {
        get
        {
            return tb_Opinion.ClientID;
        }
        set
        {

        }
    }

    public string GetCurrentActivityOption()
    {
        if (_BPMContext.ProcInst == null)
        {
            return "";
        }
        var sysContext = DBContext.GetSysContext();
        var optionList = sysContext.WF_Approval_Record.Where(x => x.InstanceID == _BPMContext.ProcID).OrderBy(x => x.ApproveAtTime).ToList();

        StringBuilder content = new StringBuilder();
        string lineItemFormat = @"<div class='OptionItem'>
                                    {0}
                                  <div><span>{1}</span>      {2}{3}</div>
                                  </div>";

        foreach (var item in optionList)
        {
            if (!string.IsNullOrEmpty(item.Opinion))
            {
                content.AppendFormat(lineItemFormat, item.Opinion,
                    SignPicHelper.GetSignPic(item.ApproveByUserCode, item.ApproveByUserName), item.ApproveAtTime
                , (!string.IsNullOrEmpty(item.ApproveStatus) && item.ApproveStatus == "Mobile" ? "<span style='color:#808080;' title='通过移动OA审批'> (来自移动审批)</span>" : ""));
            }
        }

        return content.ToString();
    }

    protected override void LoadViewState(object savedState)
    {
        Object[] datas = savedState as Object[];
        base.LoadViewState(datas[0]);

        if (datas[1] != null)
        {
            Node = datas[1].ToString();
        }

    }

    protected override object SaveViewState()
    {
        Object[] datas = new Object[]
        {
            base.SaveViewState(),
            Node
        };
        return datas;
    }
}