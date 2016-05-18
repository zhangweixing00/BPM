using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.OA;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_ViewPage_V_OA_ContractAuditOfEToI : System.Web.UI.Page
{
    ContractAuditOfEToI Vitems = new ContractAuditOfEToI();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// <summary>
    /// 绑定表单数据
    /// </summary>
    private void BindFormData()
    {
        try
        {
            WorkFlowInstance Instance = wf_WorkFlowInstance.GetWorkFlowInstanceById(ViewState["InstanceID"].ToString());
            if (Instance == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            ContractAuditOfEToIInfo obj = Vitems.Get(Instance.FormId.ToString());
            if (obj == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            if (obj != null)
            {
                //保存数据
                tbReportCode.Text = obj.FormID;
                cblSecurityLevel.SelectedIndex = int.Parse(obj.SecurityLevel.ToString());
                cblUrgenLevel.SelectedIndex = int.Parse(obj.UrgenLevel.ToString());
                tbDepartName.Text = obj.DeptName;
                tbUserName.Text = obj.UserName;
                tbMobile.Text = obj.Mobile;
                tbDateTime.Text = obj.DateTime;
                //合同类型
                if (obj.ContractType1 == "00")
                {
                    ddlContractType1.Text = "";
                }
                else
                {
                    ddlContractType1.Text = obj.ContractTypeName1;
                }
                ddlContractType2.Text = obj.ContractTypeName2;
                ddlContractType3.Text = obj.ContractTypeName3;

                tbContractSum.Text = obj.ContractSum;
                cblIsSupplementProtocol.SelectedValue = obj.IsSupplementProtocol;
                tbSupplementProtocol.Text = obj.IsSupplementProtocolText;
                cblIsFormatContract.SelectedValue = obj.IsFormatContract;
                cblIsNormText.SelectedValue = obj.IsNormText;
                cblIsBidding.SelectedValue = obj.IsBidding;
                cblIsEstateProject.SelectedValue = obj.IsEstateProject;

                if (obj.EstateProjectName == "0")
                {
                    ddlEstateProjectName.Text = "";
                }
                else
                {
                    ddlEstateProjectName.Text = obj.EstateProjectNameText;
                }
                if (obj.EstateProjectNum == "0")
                {
                    ddlEstateProjectNum.Text = "";
                }
                else
                {
                    ddlEstateProjectNum.Text = obj.EstateProjectNumText;
                }
                //合同主体
                if (obj.ContractSubject == "00000")
                {
                    ddlContractSubject.Text = "";
                }
                else
                {
                    ddlContractSubject.Text = obj.ContractSubjectName;
                }
                tbContractSubject1.Text = obj.ContractSubjectName2;
                tbContractSubject2.Text = obj.ContractSubjectName3;
                tbContractSubject3.Text = obj.ContractSubjectName4;

                tbContractTitle.Text = obj.ContractTitle;
                tbContractContent.Text = obj.ContractContent.Replace(" ", "&nbsp;").Replace("\n", "<br/>");

                cbIsReport.Checked = obj.IsReport == "1" ? true : false;

                //if (!string.IsNullOrEmpty(obj.IsApproval))
                //{
                //    lbIsApproval.Text = string.Format("{1}({0})", obj.IsApproval == "1" ? "批准" : "拒绝", lbIsApproval.Text);
                //    if (obj.IsApproval == "1")
                //    {
                //        this.lbIsApproval.Style.Add("color", "green");
                //    }
                //    else
                //    {
                //        this.lbIsApproval.Style.Add("color", "red");
                //    }
                //}
            }
            FlowRelated1.ProcId = ViewState["InstanceID"].ToString();
            Countersign1.ProcId = ViewState["InstanceID"].ToString();
            UploadAttachments1.ProcId = ViewState["InstanceID"].ToString();
            #region 审批意见框
            OpinionDeptDiretor.InstanceId = ViewState["InstanceID"].ToString();
            OpinionDeptManager.InstanceId = ViewState["InstanceID"].ToString();
            OpinionCountersign.InstanceId = ViewState["InstanceID"].ToString();
            OpinionLawDept.InstanceId = ViewState["InstanceID"].ToString();
            OpinionAP.InstanceId = ViewState["InstanceID"].ToString();
            OpinionLawAP.InstanceId = ViewState["InstanceID"].ToString();
            OpinionVP.InstanceId = ViewState["InstanceID"].ToString();
            OpinionDirectors.InstanceId = ViewState["InstanceID"].ToString();
            OpinionCFO.InstanceId = ViewState["InstanceID"].ToString();
            OpinionEVP.InstanceId = ViewState["InstanceID"].ToString();
            OpinionPresident.InstanceId = ViewState["InstanceID"].ToString();
            OpinionChairman.InstanceId = ViewState["InstanceID"].ToString();
            OpinionStartToFinallyContract.InstanceId = ViewState["InstanceID"].ToString();
            OpinionLawAuditOpinion.InstanceId = ViewState["InstanceID"].ToString();
            OpinionSealAdministrator.InstanceId = ViewState["InstanceID"].ToString();
            OpinionFileManager.InstanceId = ViewState["InstanceID"].ToString();
            #endregion

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}