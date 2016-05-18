using System;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3023")]
public partial class Workflow_ApprovePage_V_OA_InstructionOfWY 
    : A_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            ///加载业务数据
            var info = BizContext.OA_InstructionOfWY.FirstOrDefault(x => x.FormID == FormId);
            cblSecurityLevel.SelectedIndex = int.Parse(info.SecurityLevel.ToString());
            cblUrgenLevel.SelectedIndex = int.Parse(info.UrgenLevel.ToString());

            tbReportCode.Text = info.FormID;
            StartDeptId = info.DeptCode;
            tbDepartName.Text = info.DeptName;
            tbUserName.Text = info.UserName;
            tbMobile.Text = info.Mobile;
            tbDateTime.Text = info.DateTime;
            cblIsReportToWY.SelectedValue = info.IsReportToWY;
            cblIsReportToGroup.SelectedValue = info.IsReportToGroup;
            tbTitle.Text = info.Title;
            tbContent.Text = info.Content;

            InitCheckBoxList(info.LeadersSelected);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitCheckBoxList(string cblCheck)
    {
        if (!string.IsNullOrEmpty(cblCheck))
        {
            string[] array = cblCheck.Split(',');
            cbAP.Checked = array[0].Substring(0, 1) == "0" ? false : true;
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

        //判断分公司和物业集团显示
        if (StartDeptId.Contains("S366-S976"))
        {
            Company.Visible = false;
            Group.Visible = true;
            Group1.Visible = false;
            IsReportToWY.Visible = false;
        }
        else if (!StartDeptId.Contains("S366-S976") && cblIsReportToWY.SelectedItem.Value == "1")
        {
            Company.Visible = true;
            Company1.Visible = true;
            Group.Visible = true;
            IsReportToGroup.Visible = false;
        }
        else if (!StartDeptId.Contains("S366-S976") && cblIsReportToWY.SelectedItem.Value == "0")
        {
            Company.Visible = true;
            Company1.Visible = false;
            Group.Visible = false;
            IsReportToGroup.Visible = false;
        }
    }
}
