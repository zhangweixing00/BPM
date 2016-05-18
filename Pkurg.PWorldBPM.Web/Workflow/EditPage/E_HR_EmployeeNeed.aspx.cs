using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3016")]
public partial class Workflow_EditPage_E_HR_EmployeeNeed : E_WorkflowFormBase
{
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string HRDeptCode = System.Configuration.ConfigurationManager.AppSettings["HRDeptCode"];

    #region 重载函数
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitStartDeptment();
            InitApproveList();

            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("HR_EN_");
                StartDeptId = ddlDeptName.SelectedItem.Value;
                tbReportCode.Text = FormId;
                UpdatedTextBox.Value = DateTime.Now.ToShortDateString();
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
            Pkurg.PWorldBPM.Business.BIZ.HR_EmployeeNeed info = BizContext.HR_EmployeeNeed.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                InitApproveList2(info.IsGroup);
                ListItem selectedItem = ddlDeptName.Items.FindByValue(info.DeptCode);
                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                }
                ddlReason.SelectedValue = info.Reason;
                ddlAge.SelectedValue = info.Age;
                ddlEducation.SelectedValue = info.Education;
                ddlReason.SelectedValue = info.Reason;
                ddlSex.SelectedValue = info.Sex;
                ddlSpecialty.SelectedValue = info.Specialty;
                ddlTitle.SelectedValue = info.Title;
                ddlWorkingLifetime.SelectedValue = info.WorkingLifetime;

                tbReportCode.Text = info.FormID;
                UpdatedTextBox.Value = info.DateTime;
                tbPosition.Text = info.Position;
                tbNumber.Text = info.Number;
                tbMajorDuty.Text = info.MajorDuty;
                tbProfessionalAbility.Text = info.ProfessionalAbility;
                tbWorkTime.Value = info.WorkTime;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void SaveFormData()
    {
        var info = BizContext.HR_EmployeeNeed.FirstOrDefault(x => x.FormID == FormId);

        if (info == null)
        {
            info = new HR_EmployeeNeed()
            {
               FormID = FormId,
                DateTime = UpdatedTextBox.Value,
                DeptName = ddlDeptName.SelectedItem.Text,
                DeptCode = ddlDeptName.SelectedItem.Value.ToString(),
                Position = tbPosition.Text,
                Number = tbNumber.Text,
                Reason = ddlReason.SelectedItem.Text,
                MajorDuty = tbMajorDuty.Text,
                Sex = ddlSex.SelectedItem.Text,
                Age = ddlAge.SelectedItem.Text,
                Education = ddlEducation.SelectedItem.Text,
                Specialty = ddlSpecialty.SelectedItem.Text,
                Title = ddlTitle.SelectedItem.Text,
                WorkingLifetime = ddlWorkingLifetime.SelectedItem.Text,
                WorkTime = tbWorkTime.Value,
                ProfessionalAbility = tbProfessionalAbility.Text,
                IsGroup = hfIsGroup.Value
            };
            BizContext.HR_EmployeeNeed.InsertOnSubmit(info);
        }
        else
        {
            info.DateTime = UpdatedTextBox.Value;
            info.DeptName = ddlDeptName.SelectedItem.Text;
            info.DeptCode = ddlDeptName.SelectedItem.Value.ToString();
            info.Position = tbPosition.Text;
            info.Number = tbNumber.Text;
            info.Reason = ddlReason.SelectedItem.Text;
            info.MajorDuty = tbMajorDuty.Text;
            info.Sex = ddlSex.SelectedItem.Text;
            info.Age = ddlAge.SelectedItem.Text;
            info.Education = ddlEducation.SelectedItem.Text;
            info.Specialty = ddlSpecialty.SelectedItem.Text;
            info.Title = ddlTitle.SelectedItem.Text;
            info.WorkingLifetime = ddlWorkingLifetime.SelectedItem.Text;
            info.WorkTime = tbWorkTime.Value;
            info.ProfessionalAbility = tbProfessionalAbility.Text;
        }
        BizContext.SubmitChanges();
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        //所有DataField：ActJumped,Director,EDeptManager,Employee,HRDeptManager,HRDeptManager2,IsPass,President,RDeptManager
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("Employee", "noapprovers");
        dataFields.Add("RDeptManager", "noapprovers");
        dataFields.Add("HRDeptManager2", "noapprovers");
        dataFields.Add("IsPass", "1");
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        //string startDeptId = _BPMContext.CurrentUser.MainDeptId;

        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///已存在dataField：ActJumped,Director,EDeptManager,Employee,HRDeptManager,HRDeptManager2,IsPass,President,RDeptManager
        ///自动生成
        //DataTable dtDeptManager = new BFPmsUserRoleDepartment().GetSelectRoleUser(StartDeptId, "部门负责人");
        //if (dtDeptManager.Rows.Count != 0)
        //{
        //    if (dtDeptManager.Rows[0]["EmployeeName"].ToString() == CurrentEmployee.EmployeeName)
        //    {
        //        dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "EDeptManager" });
        //    }
        //    else
        //    {
        //        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "EDeptManager" });
        //    }
        //}
        //else
        //{
        //    dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "EDeptManager" });
        //}
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "EDeptManager" });
        if (ddlDeptName.SelectedItem.Text.Contains("人力资源部"))
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "HRDeptManager" });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = HRDeptCode, RoleName = "部门负责人", Name = "HRDeptManager" });
        }
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "主管助理总裁,主管副总裁", Name = "Director" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = PKURGICode, RoleName = "董事长", Name = "President" });

        return dfInfos;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        FormTitle = ddlDeptName.SelectedItem.Text + "-人员需求申请(" + UpdatedTextBox.Value + ")";
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
        FormTitle = ddlDeptName.SelectedItem.Text + "-人员需求申请(" + UpdatedTextBox.Value + ")";
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
    /// 初始化审批栏
    /// </summary>
    private void InitApproveList()
    {
        string IsGroup = Request.QueryString["ref"];
        if (string.IsNullOrEmpty(IsGroup))
        {
            lbDirector.Text = "相关董事意见：";
            lbPresident.Text = "董事长意见：";
            hfIsGroup.Value = "0";
            lbTitle.Text = "Peking University Resources Group Investment Company Limited";
        }
        else
        {
            lbDirector.Text = "用人部门分管领导意见：";
            lbPresident.Text = "CEO意见：";
            hfIsGroup.Value = "1";
        }
    }

    /// <summary>
    /// 初始化审批栏2
    /// </summary>
    private void InitApproveList2(string IsGroup)
    {
        lbDirector.Text = "相关董事意见：";
        lbPresident.Text = "董事长意见：";
        if (!string.IsNullOrEmpty(IsGroup))
        {
            if (IsGroup == "1")
            {
                lbDirector.Text = "用人部门分管领导意见：";
                lbPresident.Text = "CEO意见：";
            }
        }
    }

    /// <summary>
    /// 发起部门更改方法
    /// </summary>
    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartDeptId = ddlDeptName.SelectedItem.Value;
    }

    /// <summary>
    /// 初始化领导名称
    /// </summary>
    private void InitLeader()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(StartDeptId, "部门负责人");
        DataTable dtHRDeptManager = bfurd.GetSelectRoleUser(HRDeptCode, "部门负责人");
        DataTable dtDirector = bfurd.GetSelectRoleUser(StartDeptId, "主管副总裁");
        DataTable dtPresident = bfurd.GetSelectRoleUser(PKURGICode, "董事长");

        if (dtDeptManager.Rows.Count != 0)
        {
            lbDeptManager2.Text = "(" + dtDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtHRDeptManager.Rows.Count != 0)
        {
            lbHRDeptManager2.Text = "(" + dtHRDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtDirector.Rows.Count != 0)
        {
            lbDirector2.Text = "(" + dtDirector.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbPresident2.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
    }
    #endregion
}
