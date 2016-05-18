using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_ViewPage_V_JC_PurchasePaint : System.Web.UI.Page
{
    PurchasePaint Vitems = new PurchasePaint();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                ViewState["InstanceID"] = Request.QueryString["ID"];
                BindFormData();
            }
        }
    }

    private void BindFormData()
    {
        try
        {
            WorkFlowInstance Instance = wf_WorkFlowInstance.GetWorkFlowInstanceById(ViewState["InstanceID"].ToString());
            if (Instance == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            PurchasePaintInfo obj = Vitems.Get(Instance.FormId.ToString());
            if (obj == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            if (obj != null)
            {
                tbReportCode.Text = obj.FormID;
                tbDepartName.Text = obj.DeptName;
                lbDeptCode.Text = obj.DeptCode;
                tbDateTime.Text = obj.DateTime;
                tbUserName.Text = obj.UserName;
                tbMobile.Text = obj.Mobile;
                tbTitle.Text = obj.Title;
                tbContent.Text = obj.Content.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
            }
            FlowRelated1.ProcId = ViewState["InstanceID"].ToString();
            Countersign1.ProcId = ViewState["InstanceID"].ToString();
            UploadAttachments1.ProcId = ViewState["InstanceID"].ToString();
            #region 审批意见框
            OpinionDeptManager.InstanceId = ViewState["InstanceID"].ToString();
            OpinionCountersign.InstanceId = ViewState["InstanceID"].ToString();
            OpinionPresident.InstanceId = ViewState["InstanceID"].ToString();
            OpinionGroupAuditor.InstanceId = ViewState["InstanceID"].ToString();
            OpinionGroupProjectLeader.InstanceId = ViewState["InstanceID"].ToString();
            OpinionGroupDeptLeader.InstanceId = ViewState["InstanceID"].ToString();
            OpinionGroupDeptManager.InstanceId = ViewState["InstanceID"].ToString();
            OpinionGroupLeader.InstanceId = ViewState["InstanceID"].ToString();
            OpinionGroupAuditor2.InstanceId = ViewState["InstanceID"].ToString();
            OpinionGroupReviewer.InstanceId = ViewState["InstanceID"].ToString();
            #endregion

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}