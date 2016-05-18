using System;
using System.Collections.Generic;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_ViewPage_V_ERP_ContractApproval : System.Web.UI.Page
{
    /// <summary>
    /// 加载表单
    /// </summary>
    private void InitFormData()
    {
        try
        {
            ContractApprovalInfo info = ContractApproval.GetModel(FormId);
            if (info != null)
            {
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(info.StartDeptId);
                ddlDepartName.Text = deptInfo.Remark;

                cblisoverCotract.Checked = info.IsOverContract.Value == 1;
                cbIsReportResource.Checked = info.IsReportToResource.Value == 1;
                cbIsReportFounder.Checked = info.IsReportToFounder.Value == 1;
            }
            ///加载业务数据
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string instId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(instId))
            {
                ViewState["InstanceID"] = Request.QueryString["id"];
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;
                InitFormData();
                InitApproveOpinion();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }
    }

    /// <summary>
    /// 标题
    /// </summary>
    public string FormTitle
    {
        get
        {
            return ViewState["FormTitle"].ToString();
        }
        set
        {
            ViewState["FormTitle"] = value;
        }
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

    /// <summary>
    /// 初始化意见框
    /// </summary>
    private void InitApproveOpinion()
    {
        List<UserControls_ApproveOpinionUC> options = GetOptions();
        foreach (var item in options)
        {
            item.InstanceId = ViewState["InstanceID"].ToString();
        }
    }

    private List<UserControls_ApproveOpinionUC> GetOptions()
    {
        List<UserControls_ApproveOpinionUC> options = new List<UserControls_ApproveOpinionUC>();
        options.Add(Option_1326);
        options.Add(Option_1327);
        options.Add(Option_1328);
        options.Add(Option_1329);
        options.Add(Option_1330);
        options.Add(Option_1331);
        options.Add(Option_1332);
        options.Add(Option_1333);
        options.Add(Option_1334);
        options.Add(Option_1335);
        options.Add(Option_1343);
        options.Add(Option_1336);
        options.Add(Option_1337);
        options.Add(Option_1338);
        options.Add(Option_1339);
        options.Add(Option_1340);
        options.Add(Option_1341);

        return options;
    }
}
