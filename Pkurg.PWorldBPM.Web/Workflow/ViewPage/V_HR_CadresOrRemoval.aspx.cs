using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_ViewPage_V_HR_CadresOrRemoval : System.Web.UI.Page
{
    CadresOrRemoval Vitems = new CadresOrRemoval();
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
            CadresOrRemovalInfo obj = Vitems.Get(Instance.FormId.ToString());
            if (obj == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            if (obj != null)
            {
                InitApproveList2(obj.IsGroup);
                tbReportCode.Text = obj.FormID;
                tbCadresName.Text = obj.CadresName;
                tbLocationCompanyDeptJob.Text = obj.LocationCompanyDeptJob.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                tbCadresCompanyDeptJob.Text = obj.CadresCompanyDeptJob.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                tbCadresContent.Text = obj.CadresContent.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                tbRemovalName.Text = obj.RemovalName;
                tbLocationCompanyDeptJobR.Text = obj.LocationCompanyDeptJobR.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                tbRemovalCompanyDeptjob.Text = obj.RemovalCompanyDeptjob.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                tbRemovalContent.Text = obj.RemovalContent.Replace(" ", "&nbsp;").Replace("\n", "<br/>");

                if (!string.IsNullOrEmpty(obj.chkCadresOrRemoval))
                {
                    tbCadre.Visible = obj.chkCadresOrRemoval != "1" ? true : false;
                    tbRemoval.Visible = obj.chkCadresOrRemoval != "0" ? true : false;
                }
            }
            UploadAttachments1.ProcId = ViewState["InstanceID"].ToString();

            OpinionDeptManager.InstanceId = ViewState["InstanceID"].ToString();
            OpinionHRDeptManager.InstanceId = ViewState["InstanceID"].ToString();
            OpinionDirector1.InstanceId = ViewState["InstanceID"].ToString();
            OpinionDirector2.InstanceId = ViewState["InstanceID"].ToString();
            OpinionDirector3.InstanceId = ViewState["InstanceID"].ToString();
            OpinionDirector4.InstanceId = ViewState["InstanceID"].ToString();
            OpinionChairman.InstanceId = ViewState["InstanceID"].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 初始化审批栏2
    /// </summary>
    private void InitApproveList2(string IsGroup)
    {
        lbDirector.Text = "相关董事意见：";
        lbPresident.Text = "董事长意见：";
        lbTitle.Text = "Peking University Resources Group Investment Company Limited";
        if (!string.IsNullOrEmpty(IsGroup))
        {
            if (IsGroup == "1")
            {
                lbDirector.Text = "分管领导意见：";
                lbPresident.Text = "CEO意见：";
                lbTitle.Text = "";
            }
        }
    }
}