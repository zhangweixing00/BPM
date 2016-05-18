using System;
using System.Collections.Generic;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3011")]
public partial class Workflow_ApprovePage_V_OA_CustomWorkflow 
    :  V_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            var info = BizContext.OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbTheme.Text = info.Title;
                tbContent.Text = info.ContentTxt.Replace(" ", "&nbsp;").ToHtmlString();
                tbDate.Text = info.DateTime.Value.ToShortDateString();
                tbPerson.Text = info.UserName;
                tbPhone.Text = info.Mobile;
                lbDepartName.Text = info.DeptName;
                if (!string.IsNullOrEmpty(info.SecurityLevel))
                {
                    lbFormTitle.Text = info.SecurityLevel;
                }
                else
                {
                    lbFormTitle.Text = info.Title;
                }
            }
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
            ///加载页面数据
            string instId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(instId))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;
                InitFormData();
                InitFlowData();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }
    }
    private void InitFlowData()
    {
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = CustomWorkflowDataProcess.GetWorkItemsData(_BPMContext.ProcID, FormId);
        if (itemInfos != null)
        {
            rptList.DataSource = itemInfos;
            rptList.DataBind();
        }
    }
}
