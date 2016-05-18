using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;

public partial class Workflow_ViewPage_V_BP_LeaseContract : System.Web.UI.Page
{
    public BP_LeaseContract lc = new BP_LeaseContract();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    public string className = "Workflow_ViewPage_V_BP_LeaseContract";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                ViewState["InstanceID"] = Request.QueryString["ID"];
                BindFormData();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                tbCompany.Visible = false;
                tbGroup.Visible = true;
            }
        }
    }

    private void BindFormData()
    {
        string methodName = "BindFormData";
        try
        {
            WorkFlowInstance Instance = wf_WorkFlowInstance.GetWorkFlowInstanceById(ViewState["InstanceID"].ToString());
            if (Instance == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            BP_LeaseContractInfo item = lc.GetLeaseContract(Instance.FormId.ToString());
            if (item == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            if (item != null)
            {
                cblSecurityLevel.SelectedValue = item.SecurityLevel != null ? item.SecurityLevel.ToString() : "-1";
                cblUrgentLevel.SelectedValue = item.UrgenLevel != null ? item.UrgenLevel.ToString() : "-1";
                tbData.Text = ((DateTime)item.Date).ToString("yyyy-MM-dd");
                tbPerson.Text = item.UserName;
                tbDepartName.Text = item.DeptName;
                tbPhone.Text = item.Mobile;
                tbTitle.Text = item.ReportTitle;
                tbContent.Text = item.Url;
                tbReportCode.Text = item.ReportCode;
                tbReason.Text = item.Reason;
                tbRemark.Text = item.Remark;
                cblDecorationContract.SelectedValue = item.DecorationContract != null ? item.DecorationContract.ToString() : "-1";
                cblServiceContract.SelectedValue = item.ServiceContract != null ? item.ServiceContract.ToString() : "-1";
                cblCompensationContract.SelectedValue = item.CompensationContract != null ? item.CompensationContract.ToString() : "-1";
                cblModificationContract.SelectedValue = item.ModificationContract != null ? item.ModificationContract.ToString() : "-1";
                cblSupplementContract.SelectedValue = item.SupplementContract != null ? item.SupplementContract.ToString() : "-1";
                cblLesseeContract.SelectedValue = item.LesseeContract != null ? item.LesseeContract.ToString() : "-1";
            }

            #region 审批意见框

            ApproveOpinionUC1.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC2.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC3.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC4.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC5.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC6.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC7.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC8.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC9.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC10.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC11.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC12.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC13.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC14.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC13.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC16.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC17.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC18.InstanceId = ViewState["InstanceID"].ToString();
            #endregion

        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    public string GetUrl()
    {
        return tbContent.Text.ToString();
    }
    
}