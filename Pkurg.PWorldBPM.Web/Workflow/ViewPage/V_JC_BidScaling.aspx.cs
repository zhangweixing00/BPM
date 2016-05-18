using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;

public partial class Workflow_ViewPage_V_JC_BidScaling : System.Web.UI.Page
{
    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    public JC_BidScaling bs = new JC_BidScaling();
    JC_BidScalingList bsl = new JC_BidScalingList();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    public string className = "Workflow_ViewPage_V_JC_BidScaling";

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
                tbBidCommittee.Visible = cblIsAccreditByGroup.SelectedValue == "0" ? false : true;
                tbURL.Visible = false;
            }
            else
            {
                tbBidCommittee.Visible = cblIsAccreditByGroup.SelectedValue == "0" ? true : false;
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
            JC_BidScalingInfo obj = bs.GetBidScaling(Instance.FormId.ToString());
            if (obj == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            if (obj != null)
            {
                tbDepartName.Text = obj.DeptName; ;
                tbTitle.Text = obj.Title;
                tbDateTime.Text = obj.DateTime;
                tbContent.Text = obj.Content.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                tbEntranceTime.Text = obj.EntranceTime;
                cblIsAccreditByGroup.SelectedValue = obj.IsAccreditByGroup != null ? obj.IsAccreditByGroup.ToString() : "-1";
                
                if (obj.DeptName.Contains("开封"))
                {
                    cblFirstLevel.Visible = true;
                    cblFirstLevel.SelectedValue = obj.FirstLevel != null ? obj.FirstLevel.ToString() : "-1";
                }
                tbFirstUnit.Text = obj.FirstUnit;
                tbSecondUnit.Text = obj.SecondUnit;
                tbScalingResult.Text = obj.ScalingResult;
                string StartDeptId = obj.StartDeptCode;
                if (StartDeptId.Substring(0, StartDeptId.LastIndexOf('-')) == PKURGICode)
                {
                    trCounterSign.Visible = false;
                    lbIsImpowerProject.Visible = false;
                    cblIsAccreditByGroup.Visible = false;
                }
                BindSelectUnit(Instance.FormId.ToString());
            }
            FlowRelated1.ProcId = ViewState["InstanceID"].ToString();
            Countersign1.ProcId = ViewState["InstanceID"].ToString();
            Countersign_Group1.ProcId = ViewState["InstanceID"].ToString();
            UploadAttachments1.ProcId = ViewState["InstanceID"].ToString();
            #region 审批意见框
            ApproveOpinionUC1.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC2.InstanceId = ViewState["InstanceID"].ToString();
            OpinionExecutiveDirector.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC3.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC4.InstanceId = ViewState["InstanceID"].ToString();
            #endregion

        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    private void BindSelectUnit(string FormID)
    {
        string methodName = "BindSelectUnit";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            string BidCommittee = bs.GetBidScaling(FormID).BidCommittee;
            foreach (string UserCode in BidCommittee.Split(','))
            {
                if (UserCode != null && !string.IsNullOrEmpty(UserCode))
                {
                    JC_BidScalingListInfo obj = bsl.GetBidScalingList(FormID, UserCode);
                    if (obj != null)
                    {
                        if (obj.SelectResult == "0")
                        {
                            lblFirstList.Text =  obj.UserName + "&nbsp;&nbsp;&nbsp;&nbsp;" + obj.CreatTime + "<br/>"+ lblFirstList.Text;
                        }
                        else if (obj.SelectResult == "1")
                        {
                            lblSecondList.Text =   obj.UserName + "&nbsp;&nbsp;&nbsp;&nbsp;" + obj.CreatTime + "<br/>"+lblSecondList.Text;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }
}