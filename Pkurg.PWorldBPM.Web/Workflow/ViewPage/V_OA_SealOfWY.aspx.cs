using System;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3025")]
public partial class Workflow_ViewPage_V_OA_SealOfWY: V_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.OA_SealOfWY info = BizContext.OA_SealOfWY.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbReportCode.Text = info.FormID;
                cblSecurityLevel.SelectedValue = info.SecurityLevel;
                cblUrgenLevel.SelectedValue = info.UrgenLevel;
                tbUserName.Text = info.UserName;
                tbDeptName.Text = info.DeptName;
                tbDateTime.Text = info.DateTime;
                tbTitle.Text = info.Title;
                cblRemark.SelectedValue = info.Remark;
                tbContent.Text = info.Content.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                InitCheckBoxList(info.LeadersSelected);
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

    private void InitCheckBoxList(string cblCheck)
    {
        if (!string.IsNullOrEmpty(cblCheck))
        {
            string[] array = cblCheck.Split(',');
            cbAP.Checked = array[0].Substring(0, 1) == "0" ? false : true;
            cbVP.Checked = array[1].Substring(0, 1) == "0" ? false : true;
            cbPresident.Checked = array[2].Substring(0, 1) == "0" ? false : true;
        }
    }
}
