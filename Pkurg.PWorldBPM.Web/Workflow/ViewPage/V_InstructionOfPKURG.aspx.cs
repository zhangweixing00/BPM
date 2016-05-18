using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;

public partial class Workflow_ViewPage_V_InstructionOfPKURG : System.Web.UI.Page
{
    public WF_InstructionOfPKURG wf_Instruction = new WF_InstructionOfPKURG();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    public string className = "Workflow_ViewPage_V_InstructionOfPKURG";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                ViewState["InstanceID"] = Request.QueryString["ID"];
                InintData();
            }
        }
    }

    private void InintData()
    {
        string methodName = "InintData";
        try
        {
            WorkFlowInstance Instance = wf_WorkFlowInstance.GetWorkFlowInstanceById(ViewState["InstanceID"].ToString());
            InstructionOfPkurg item = wf_Instruction.GetInstructionOfPkurgById(Instance.FormId.ToString());
            if (item != null)
            {
                cblSecurityLevel.SelectedValue = item.SecurityLevel.ToString();
                cblUrgentLevel.SelectedValue = item.UrgenLevel != null ? item.UrgenLevel.ToString() : "0";
                tbData.Text = ((DateTime)item.Date).ToString("yyyy-MM-dd");
                tbPerson.Text = item.UserName;
                tbDepartName.Text = item.DeptName;
                tbPhone.Text = item.Mobile;
                tbTheme.Text = item.ReportTitle;
                tbContent.Text = item.ReportContent;
                tbNumber.Text = item.ReportCode;
                cbIsReport.Checked = item.IsReport == 1 ? true : false;
            }

            FlowRelated1.ProcId = ViewState["InstanceID"].ToString();
            Countersign1.ProcId = ViewState["InstanceID"].ToString();
            UploadAttachments1.ProcId = ViewState["InstanceID"].ToString();

            ApproveOpinionUCDeptleader.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUCRealateDept.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUCLeader.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUCCEO.InstanceId = ViewState["InstanceID"].ToString();

        }
        catch (Exception ex)
        {

            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }
}
