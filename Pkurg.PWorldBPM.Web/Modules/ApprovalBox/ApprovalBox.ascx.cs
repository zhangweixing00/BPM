using System;
using System.Data;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Business.Controls;
using Pkurg.PWorldBPM.Common;

public partial class Modules_ApprovalBox_ApprovalBox : UControlBase, IApprovalBox
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (K2_TaskItem != null)
        {
            try
            {
                string actionName = K2_TaskItem.ActivityInstanceDestination.Name;
                //LoggerR.logger.Debug("AB1");
                if (!string.IsNullOrEmpty(Node))
                {
                    tb_Opinion.Visible = Node.Split(',').ToList().Contains(actionName);
                    //LoggerR.logger.Debug("AB2");
                    if (tb_Opinion.Visible)
                    {
                        //LoggerR.logger.DebugFormat("AB2.51");
                        //LoggerR.logger.DebugFormat("AB2.52:{0}", tb_Opinion.ClientID);
                        div_Hidden.InnerHtml = string.Format("<input id='hf_OpId' type='hidden' value='{0}'/>", tb_Opinion.ClientID);
                        //LoggerR.logger.Debug("AB3");
                    }
                }
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
        StringBuilder content = new StringBuilder();
        string lineItemFormat = @"<div class='OptionItem'>
                                    {0}
                                  <div><span>{1}</span>      {2}{3}</div>
                                  </div>";
        DataTable dt = new WF_ApprovalBox().GetWorkItems(_BPMContext.ProcID, "," + Node + ",");
        //LoggerR.logger.Debug(dt == null ? "AB:dtnull" : "dt");
        foreach (DataRow item in dt.Rows)
        {
            if (item["Opinion"] == null || string.IsNullOrEmpty(item["Opinion"].ToString()))
            {

            }
            else
            {
                string option = item["Opinion"] == null || string.IsNullOrEmpty(item["Opinion"].ToString()) ? item["ApproveResult"].ToString() : item["Opinion"].ToString();
                content.AppendFormat(lineItemFormat, item["Opinion"],
                    SignPicHelper.GetSignPic(item["ApproveByUserCode"] == null ? "" : item["ApproveByUserCode"].ToString(), item["ApproveByUserName"].ToString()), item["ApproveAtTime"]
                ,(item["ApproveStatus"] != null && item["ApproveStatus"].ToString() == "Mobile" ? "<span style='color:#808080;' title='通过移动OA审批'> (来自移动审批)</span>" : ""));
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