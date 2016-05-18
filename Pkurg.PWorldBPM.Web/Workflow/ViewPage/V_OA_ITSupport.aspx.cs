using System;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3014")]
public partial class Workflow_ApprovePage_V_OA_ITSupport
    :  V_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            var info = BizContext.OA_ITSupport_Form.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbDepartName.Text = info.DeptName;
                tbCompany.Text = info.CompanyName;
                tbDate.Text = info.DateTime.ToString();
                tbEmail.Text = info.Email;
                tbPerson.Text = info.UserName;
                tbPhone.Text = info.Mobile;

                tbQuestion.Text = "--";
                if (info.QuestionId.HasValue && info.QuestionId.Value != -1)
                {
                    var qInfo = BizContext.V_ITSupport_Question.FirstOrDefault(x => x.Id == info.QuestionId.Value);
                    if (qInfo != null)
                    {
                        tbQuestion.Text = qInfo.QuestionKey;
                    }
                }
                tbType.Text = "--";
                if (info.STypeId.HasValue && info.STypeId.Value != -1)
                {
                    var qInfo = BizContext.V_ITSupport_Catalog.FirstOrDefault(x => x.Id == info.STypeId.Value);
                    if (qInfo != null)
                    {
                        tbType.Text = qInfo.Name;
                    }
                }

                tbContent.Text = info.ContentTxt.ToHtmlString();
                string statusContent = "";
                if (info.ProcessResult.HasValue)
                {
                    statusContent = string.Format("<span style='color:{1};'>({0})</span>",
                        info.ProcessResult.Value == 1 ? "已完成" : "已结单",info.ProcessResult.Value == 1 ? "green" : "red"
                        );
                }
                else
                {
                    statusContent = "<span style='color:blue;'>(处理中)</span>";
                }
                FormTitle = FormTitle + statusContent;
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
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }
    }
}
