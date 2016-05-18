using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3023")]
public partial class Workflow_EditPage_E_OA_InstructionOfWY
    : E_WorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            //初始化起始部门 
            InitStartDeptment();

            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("OA_WYQS_");
                FormTitle = SysContext.WF_AppDict.FirstOrDefault(x => x.AppId == AppID).AppName;
                StartDeptId = ddlDepartName.SelectedItem.Value;
                tbReportCode.Text = FormId;
                UpdatedTextBox.Value = DateTime.Now.ToShortDateString();
                tbUserName.Text = CurrentEmployee.EmployeeName;
                tbMobile.Text = CurrentEmployee.MobilePhone;

                cbAP.Checked = true;
                cbVP.Checked = true;
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;

                InitFormData();
            }
        }

        if (StartDeptId.Contains("S366-S976"))
        {
            Company.Visible = false;
            Group.Visible = true;
            Group1.Visible = false;
            IsReportToWY.Visible = false;
            //cblIsReportToWY.Visible = false;

        }
        else if (!StartDeptId.Contains("S366-S976"))
        {
            Company.Visible = true;
            Company1.Visible = true;
            Group.Visible = true;
            IsReportToGroup.Visible = false;
        }
    }

    /// <summary>
    /// 初始化起始部门
    /// </summary>
    private void InitStartDeptment()
    {
        //得到起始部门的数据源
        ddlDepartName.DataSource = GetStartDeptmentDataSource();
        ddlDepartName.DataTextField = "Remark";
        ddlDepartName.DataValueField = "DepartCode";
        ddlDepartName.DataBind();
    }

    /// <summary>
    /// 初始化表单数据
    /// </summary>
    protected override void InitFormData()
    {
        try
        {
            ///加载业务数据
            Pkurg.PWorldBPM.Business.BIZ.OA_InstructionOfWY info = BizContext.OA_InstructionOfWY.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                cblSecurityLevel.SelectedIndex = int.Parse(info.SecurityLevel.ToString());
                cblUrgenLevel.SelectedIndex = int.Parse(info.UrgenLevel.ToString());

                tbReportCode.Text = info.FormID;
                StartDeptId = info.DeptCode;
                ddlDepartName.SelectedItem.Value = info.DeptName;
                tbUserName.Text = info.UserName;
                tbMobile.Text = info.Mobile;
                tbDateTime.Text = info.DateTime;
                cblIsReportToWY.SelectedValue = info.IsReportToWY;
                cblIsReportToGroup.SelectedValue = info.IsReportToGroup;
                tbTitle.Text = info.Title;
                tbContent.Text = info.Content;

                InitCheckBoxList(info.LeadersSelected);
            }
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
            cbVP.Checked = array[1].Substring(0, 1) == "0" ? false : true;
        }
    }
    /// <summary>
    /// 保存数据
    /// </summary>
    protected override void SaveFormData()
    {
        var info = BizContext.OA_InstructionOfWY.FirstOrDefault(x => x.FormID == FormId);
        if (info == null)
        {
            info = new OA_InstructionOfWY()
            {
               FormID = FormId,
                DeptCode = ddlDepartName.SelectedItem.Value.ToString(),
                DeptName = ddlDepartName.SelectedItem.Text,
                UserName = tbMobile.Text,
                DateTime = tbDateTime.Text,
                Mobile = tbMobile.Text,
                IsReportToWY = cblIsReportToWY.SelectedIndex.ToString(),
                IsReportToGroup = cblIsReportToGroup.SelectedIndex.ToString(),
                Title = tbTitle.Text,
                Content=tbContent.Text,
                SecurityLevel = cblSecurityLevel.SelectedIndex.ToString(),
                UrgenLevel = cblUrgenLevel.SelectedIndex.ToString(),
                RelatedFormID = string.Empty,

                LeadersSelected = SaveLeadersSelected(),
            };
            BizContext.OA_InstructionOfWY.InsertOnSubmit(info);
        }
        BizContext.SubmitChanges();
    }

    protected override string GetFormTitle()
    {
        return tbTitle.Text;
    }
    /// <summary>
    /// 保存勾选框数据
    /// </summary>
    /// <returns></returns>
    private string SaveLeadersSelected()
    {
        string strLeadersSelected = "";
        strLeadersSelected = (cbAP.Checked ? "1" : "0") + ":AP";
        strLeadersSelected += "," + (cbVP.Checked ? "1" : "0") + ":VP";
        return strLeadersSelected;
    }
    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "1");
        if (StartDeptId.Contains("S366-S976"))
        {
            dataFields.Add("IsReportToGroup", cblIsReportToGroup.SelectedItem.Value);
            dataFields.Add("IsReportToWY", "0");
        }
        else
        {
            dataFields.Add("IsReportToGroup", "0");
            dataFields.Add("IsReportToWY", cblIsReportToWY.SelectedItem.Value);
        }
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        //string startDeptId = _BPMContext.CurrentUser.MainDeptId;
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        string WYGroupCode = System.Configuration.ConfigurationManager.AppSettings["WYGroupCode"];
        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///自动生成
        if (!StartDeptId.Contains("S366-S976"))
        {
            /////分公司
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "CityDeptManager" });
            dfInfos.Add(new K2_DataFieldInfo() { RoleName = "部门负责人", Name = "CounterSignUsers", DeptCode = Countersign1.Result, IsRepeatIgnore = true, IsHaveToExsit = true });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "主管副总经理", Name = "ViceLeader" });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "总裁", Name = "CityPresident" });
            if (cblIsReportToWY.SelectedItem.Value == "0")
            {
                //物业集团
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "DeptManager" });
                dfInfos.Add(new K2_DataFieldInfo() { RoleName = "部门负责人", Name = "CounterSignUsers_Group", Result = "noapprovers", IsRepeatIgnore = true });
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "主管助理总裁", Name = "AssiPresident" });
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "主管副总裁", Name = "VicePresident" });
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "总裁", Name = "President" });
            }
            else
            {
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "DeptManager" });
                dfInfos.Add(new K2_DataFieldInfo() { RoleName = "部门负责人", Name = "CounterSignUsers_Group", DeptCode = Countersign_Group1.Result, IsRepeatIgnore = true, IsHaveToExsit = true });
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign_Group1.Result, RoleName = "主管助理总裁", Name = "AssiPresident" });
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign_Group1.Result, RoleName = "主管副总裁", Name = "VicePresident" });
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = WYGroupCode, RoleName = "总裁", Name = "President" });
            }
            
        }
        else
        {
            /////分公司
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "CityDeptManager" });
            dfInfos.Add(new K2_DataFieldInfo() { RoleName = "部门负责人", Name = "CounterSignUsers", Result = "noapprovers", IsRepeatIgnore = true});
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "主管副总经理", Name = "ViceLeader" });
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "总裁", Name = "CityPresident" });

            //物业集团
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "DeptManager" });
            dfInfos.Add(new K2_DataFieldInfo() { RoleName = "部门负责人", Name = "CounterSignUsers_Group", DeptCode = Countersign_Group1.Result, IsRepeatIgnore = true, IsHaveToExsit = true });
            if (cbAP.Checked)
            {
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId + "," + Countersign1.Result, RoleName = "主管助理总裁", Name = "AssiPresident" });
            }
            else
            {
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "AssiPresident" });
            }
            if (cbVP.Checked)
            {
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId + "," + Countersign1.Result, RoleName = "主管副总裁", Name = "VicePresident" });
            }
            else
            {
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "VicePresident" });
            }
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = WYGroupCode, RoleName = "总裁", Name = "President" });
        }
        return dfInfos;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        Save();
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        Submit();
    }

    /// <summary>
    /// 终止
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        DelWorkflow();
    }

    /// <summary>
    /// 发起部门更改方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
    }
}
