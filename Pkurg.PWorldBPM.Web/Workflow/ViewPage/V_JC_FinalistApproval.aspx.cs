using System;
using System.Collections.Generic;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_ViewPage_V_JC_FinalistApproval : System.Web.UI.Page
{
    /// <summary>
    /// 加载表单
    /// </summary>
    private void InitFormData()
    {
        try
        {
            JC_FinalistApprovalInfo info = JC_FinalistApproval.GetJC_FinalistApprovalInfoByFormID(FormId);
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (info != null)
                {
                    //加载业务数据
                    tbProjectName.Text = info.ProjectName;
                    ddlDepartName.Text = info.ReportDept;
                    lbDeptCode.Text = info.StartDeptId;
                    tbDateTime.Text = info.ReportDate ?? "";
                    cblIsImpowerProject.SelectedIndex = int.Parse(info.IsAccreditByGroup);
                    tbCheckStatus.Text = info.CheckStatus.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                    if (!string.IsNullOrEmpty(info.IsApproval))
                    {
                        lbIsApproval.Text = string.Format("{1}({0})", info.IsApproval == "1" ? "批准" : "拒绝", lbIsApproval.Text);
                        if (info.IsApproval == "1")
                        {
                            this.lbIsApproval.Style.Add("color", "green");
                        }
                        else
                        {
                            this.lbIsApproval.Style.Add("color", "red");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    //是否正确
    public string className = "Workflow_ViewPage_V_ERP_Instruction";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string instId = Request.QueryString["ID"];
            if (!string.IsNullOrEmpty(instId))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                ViewState["InstanceID"] = Request.QueryString["ID"];
                FormId = info.FormId;
                InitFormData();
                InitApproveOpinion();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }

            //集团隐藏
            if (lbDeptCode.Text.Contains("S363"))
            {
                cblIsImpowerProject.Visible = false;
                lbIsImpowerProject.Visible = false;
                tbCompany.Visible = false;
                tbCompanyCommittee.Visible = false;
                tbURL.Visible = false;
                tbGroup.Visible = true;
                tbGroupCommittee.Visible = true;
            }
            else
            {
                cblIsImpowerProject.Visible = true;
                lbIsImpowerProject.Visible = true;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                tbCompany.Visible = false;
                tbCompanyCommittee.Visible = false;
                tbURL.Visible = false;
                tbGroup.Visible = true;
                tbGroupCommittee.Visible = cblIsImpowerProject.SelectedValue == "1" ? false : true;
            }
            else
            {
                tbCompanyCommittee.Visible = cblIsImpowerProject.SelectedValue == "1" ? true : false;
            }
        }
    }
    /// <summary>
    /// 初始化意见框
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    private void InitApproveOpinion()
    {
        List<UserControls_ApproveOpinionUC> options = GetOptions();
        foreach (var item in options)
        {
            item.InstanceId = ViewState["InstanceID"].ToString();
        }
    }
    /// <summary>
    /// 得到意见列表
    /// </summary>
    /// <returns></returns>
    private List<UserControls_ApproveOpinionUC> GetOptions()
    {
        List<UserControls_ApproveOpinionUC> options = new List<UserControls_ApproveOpinionUC>();
        //将各个领导意见添加到list中
        options.Add(OpinionPurchasadministrationDeptLeader);
        options.Add(OpinionLegalDeptLeader);
        options.Add(OpinionGroupPurchasadministrationDeptLeader);
        options.Add(OpinionGroupLegalDeptLeader);
        options.Add(OpinionExecutiveDirector);
        options.Add(OpinionTenderCommitteeLeader);
        options.Add(OpinionTenderCommitteeChairman);
        options.Add(OpinionGroupTenderCommitteeLeader);
        options.Add(OpinionGroupTenderCommitteeChairman);
        return options;
    }
    /// <summary>
    /// FormId
    /// </summary>
    public string FormId
    {
        get
        {
            return ViewState["FormID"].ToString();
        }
        set
        {
            ViewState["FormID"] = value;
        }
    }
}