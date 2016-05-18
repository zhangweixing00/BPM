using System;
using System.Text;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;
using System.Linq;
public partial class UserControls_ApproveOpinionUC : System.Web.UI.UserControl
{
    BFApprovalRecord bfApproval = new BFApprovalRecord();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrentNodeContrl();

            BindApprovalOpinion();
        }

    }
    private void CurrentNodeContrl()
    {
        if (CurrentNode)
        {
            tbDeptLeaderOpion.Visible = true;


            this.CurrentNodeOpinionI = tbDeptLeaderOpion.Text;
        }
        else
        {
            tbDeptLeaderOpion.Visible = false;

        }

    }

    private void BindApprovalOpinion()
    {
        if (this.InstanceId != null)
        {
           var DeptLeaderList = bfApproval.GetApprovalRecordByWFLInstanceId(this.InstanceId, this.CurrentNodeName).ToList();
            StringBuilder strOpinion = new StringBuilder();
            int index = 0;
            foreach (var approval in DeptLeaderList)
            {

                index = index + 1;

                if (approval.Opinion != "")
                {
                    //修改审批意见，换行 yanghechun 2015-02-10
                    strOpinion.Append("<div style=\"clear:both;\">");
                    strOpinion.Append("<div style='float:left;margin-left:50px;'>");
                    strOpinion.Append(approval.Opinion);
                    strOpinion.Append("</div>");
                    strOpinion.Append("<br/>");
                    strOpinion.Append("<div style='float:left;margin-left:500px;'>");
                    strOpinion.Append(SignPicHelper.GetSignPic(approval.ApproveByUserCode, approval.ApproveByUserName));
                    strOpinion.Append("&nbsp;&nbsp");
                    strOpinion.Append(((DateTime)approval.ApproveAtTime).ToString("yyyy-MM-dd HH:mm"));
                    strOpinion.Append(approval.ApproveStatus == "Mobile" ? "<span style='color:#808080;' title='通过移动OA审批'> (来自移动审批)</span>" : "");
                    strOpinion.Append("</div>");
                    if (index != DeptLeaderList.Count)
                    {
                        strOpinion.Append("<br/>");
                    }
                    strOpinion.Append("</div>");
                }
            }
            lablDeptLeaderOpion.Text = strOpinion.ToString();
        }

    }

    public string InstanceId
    {
        get;
        set;
    }

    public string CurrentNodeName
    {
        get;
        set;
    }

    //public string RealateDept
    //{
    //    get;
    //    set;
    //}

    //public string Leader
    //{
    //    get;
    //    set;
    //}

    //public string CEO
    //{
    //    get;
    //    set;
    //}
    public bool CurrentNode
    {
        get;
        set;
    }
    public string CurrentNodeOpinionI
    {
        get;
        set;
    }
}