using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_ViewPage_V_OA_InstructionOfPKURGI : System.Web.UI.Page
{
    InstructionOfPKURGI Vitems = new InstructionOfPKURGI();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    string GroupCode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                ViewState["InstanceID"] = Request.QueryString["ID"];
                BindFormData();
            }
            Countersign1.CounterSignDeptId = GroupCode;//集团作为会签基准部门
            if (!lbDeptCode.Text.Contains(GroupCode))
            {
                trDept.Visible = false;
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
            InstructionOfPKURGIInfo obj = Vitems.Get(Instance.FormId.ToString());
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
                //tbContent.Text = Server.HtmlDecode( obj.Content);
                tbContent.Text = obj.Content.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                cbIsReport.Checked = obj.IsReport == "1" ? true : false;
                cblSecurityLevel.SelectedValue = obj.SecurityLevel != null ? obj.SecurityLevel.ToString() : "-1";
                cblUrgenLevel.SelectedValue = obj.UrgenLevel != null ? obj.UrgenLevel.ToString() : "-1";
            }
            FlowRelated1.ProcId = ViewState["InstanceID"].ToString();
            Countersign1.ProcId = ViewState["InstanceID"].ToString();
            UploadAttachments1.ProcId = ViewState["InstanceID"].ToString();
            #region 审批意见框
            OpinionDeptManager.InstanceId = ViewState["InstanceID"].ToString();
            OpinionCountersign.InstanceId = ViewState["InstanceID"].ToString();
            OpinionAP.InstanceId = ViewState["InstanceID"].ToString();
            OpinionVP.InstanceId = ViewState["InstanceID"].ToString();
            OpinionDirectors.InstanceId = ViewState["InstanceID"].ToString();
            OpinionPresident.InstanceId = ViewState["InstanceID"].ToString();
            OpinionChairman.InstanceId = ViewState["InstanceID"].ToString();
            #endregion

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}