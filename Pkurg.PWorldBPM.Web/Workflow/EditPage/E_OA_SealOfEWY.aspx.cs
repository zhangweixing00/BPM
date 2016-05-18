using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3026")]
public partial class Workflow_EditPage_E_OA_SealOfEWY : E_WorkflowFormBase
{
    #region 重载函数
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitStartDeptment();
            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("OA_EWYYZ_");
                StartDeptId = ddlDeptName.SelectedItem.Value;
                tbReportCode.Text = FormId;
                tbUserName.Text = CurrentEmployee.EmployeeName;
                UpdatedTextBox.Value = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;
                InitFormData();
            }
            InitLeader();
        }
    }

    protected override void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.OA_SealOfEWY info = BizContext.OA_SealOfEWY.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbReportCode.Text = info.FormID;
                cblSecurityLevel.SelectedValue = info.SecurityLevel;
                cblUrgenLevel.SelectedValue = info.UrgenLevel;
                tbUserName.Text = info.UserName;
                ddlDeptName.SelectedValue = info.DeptName;
                UpdatedTextBox.Value = info.DateTime;
                tbTitle.Text = info.Title;
                cblRemark.SelectedValue = info.Remark;
                tbContent.Text = info.Content;
                InitCheckBoxList(info.LeadersSelected);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void SaveFormData()
    {
        var info = BizContext.OA_SealOfEWY.FirstOrDefault(x => x.FormID == FormId);
        if (info == null)
        {
            info = new OA_SealOfEWY()
            {
               FormID = FormId,
                SecurityLevel = cblSecurityLevel.SelectedValue,
                UrgenLevel = cblUrgenLevel.SelectedValue,
                UserName = tbUserName.Text,
                DeptName = ddlDeptName.SelectedItem.Text,
                DeptCode = ddlDeptName.SelectedItem.Value.ToString(),
                DateTime = UpdatedTextBox.Value,
                Title = tbTitle.Text,
                Remark = cblRemark.SelectedValue,
                Content = tbContent.Text,
                LeadersSelected = SaveLeadersSelected(),
            };
            BizContext.OA_SealOfEWY.InsertOnSubmit(info);
        }
        else
        {
            info.SecurityLevel = cblSecurityLevel.SelectedValue;
            info.UrgenLevel = cblUrgenLevel.SelectedValue;
            info.UserName = tbUserName.Text;
            info.DeptName = ddlDeptName.SelectedItem.Text;
            info.DeptCode = ddlDeptName.SelectedItem.Value.ToString();
            info.DateTime = UpdatedTextBox.Value;
            info.Title = tbTitle.Text;
            info.Remark = cblRemark.SelectedValue;
            info.Content = tbContent.Text;
            info.LeadersSelected = SaveLeadersSelected();
        }
        BizContext.SubmitChanges();
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "1");
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        string GeneralDeptCode = BPMHelp.GetDeptIDByOtherIDAndName(StartDeptId, "综合管理部");

        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        if (true)
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "DeptManager" });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "DeptManager" });
        }
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign1.Result, Name = "CounterSignUsers", RoleName = "部门负责人", });
        if (cbVP.Checked)
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId + "," + Countersign1.Result, RoleName = "主管副总经理", Name = "VicePresident" });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "VicePresident" });
        }
        if (cbPresident.Checked)
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "总经理", Name = "President" });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "President" });
        }

        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = GeneralDeptCode, RoleName = "部门负责人", Name = "SealDeptManager" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "公章管理员", Name = "SealManager" });

        return dfInfos;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        FormTitle = tbTitle.Text + "(" + UpdatedTextBox.Value + ")";
        Save();
        Alert("保存完成");
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        FormTitle = tbTitle.Text + "(" + UpdatedTextBox.Value + ")";
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
    #endregion

    #region 自定义函数
    /// <summary>
    /// 初始化起始部门
    /// </summary>
    private void InitStartDeptment()
    {
        //得到起始部门的数据源
        ddlDeptName.DataSource = GetStartDeptmentDataSource();
        ddlDeptName.DataTextField = "Remark";
        ddlDeptName.DataValueField = "DepartCode";
        ddlDeptName.DataBind();
    }

    /// <summary>
    /// 发起部门更改方法
    /// </summary>
    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartDeptId = ddlDeptName.SelectedItem.Value;
        InitLeader();
    }

    /// <summary>
    /// 初始化领导名称
    /// </summary>
    private void InitLeader()
    {
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        string GeneralDeptCode = BPMHelp.GetDeptIDByOtherIDAndName(StartDeptId, "综合管理部");
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(StartDeptId, "部门负责人");
        DataTable dtPresident = bfurd.GetSelectRoleUser(CompanyCode, "总经理");
        DataTable dtZHDeptManager = bfurd.GetSelectRoleUser(GeneralDeptCode, "部门负责人");

        if (dtDeptManager.Rows.Count != 0)
        {
            lbDeptManager.Text = "(" + dtDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        else
        {
            lbDeptManager.Text = "";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbPresident.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtZHDeptManager.Rows.Count != 0)
        {
            lbZHDeptManager.Text = "(" + dtZHDeptManager.Rows[0]["EmployeeName"].ToString() + ")审核";
        }
    }

    private void InitCheckBoxList(string cblCheck)
    {
        if (!string.IsNullOrEmpty(cblCheck))
        {
            string[] array = cblCheck.Split(',');
            cbVP.Checked = array[1].Substring(0, 1) == "0" ? false : true;
            cbPresident.Checked = array[2].Substring(0, 1) == "0" ? false : true;
        }
    }

    private string SaveLeadersSelected()
    {
        string strLeadersSelected = "";
        strLeadersSelected += "," + (cbVP.Checked ? "1" : "0") + ":VP";
        strLeadersSelected += "," + (cbPresident.Checked ? "1" : "0") + ":President";
        return strLeadersSelected;
    }
    #endregion

}
