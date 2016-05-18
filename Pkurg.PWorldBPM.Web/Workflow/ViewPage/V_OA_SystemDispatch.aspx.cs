using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.OA;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;
using Pkurg.PWorldBPM.Entites.BIZ.OA;

public partial class Workflow_ViewPage_V_OA_SystemDispatch : System.Web.UI.Page
{
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    public string className = "Workflow_ViewPage_V_OA_SystemDispatch";

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
            SystemDispatchInfo info = SystemDispatch.GetSystemDispatchInfoByInstanceId(ViewState["InstanceID"].ToString());
            if (info != null)
            {
                cblSecurityLevel.SelectedIndex = Convert.ToInt32(info.SecurityLevel);
                cblUrgenLevel.SelectedIndex = Convert.ToInt32(info.UrgenLevel);
                tbDepartName.Text = info.DeptName;
                tbDateTime.Text = info.DateTime;
                tbDateTime.Text = info.DateTime;
                tbUserName.Text = info.UserName;
                tbMobile.Text = info.Mobile;
                tbTitle.Text = info.Title;
                tbContent.Text = info.Content.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                cblRedHeadDocument.SelectedIndex = int.Parse(info.RedHeadDocument);
                cblIsPublish.SelectedIndex = int.Parse(info.IsPublish);
                cblSecurityLevel.SelectedIndex = int.Parse(info.SecurityLevel);
                cblUrgenLevel.SelectedIndex = int.Parse(info.UrgenLevel);
                tbReportCode.Text = info.FormId;
            }

            FlowRelated1.ProcId = ViewState["InstanceID"].ToString();
            Countersign1.ProcId = ViewState["InstanceID"].ToString();
            UploadAttachments1.ProcId = ViewState["InstanceID"].ToString();

            OpinionDeptleader.InstanceId = ViewState["InstanceID"].ToString(); 
            OpinionRealateDept.InstanceId = ViewState["InstanceID"].ToString();
            OpinionTopLeaders.InstanceId = ViewState["InstanceID"].ToString();
            OpinionCEO.InstanceId = ViewState["InstanceID"].ToString();
            OpinionChairman.InstanceId = ViewState["InstanceID"].ToString();
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }
}
