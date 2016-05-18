using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3018")]
public partial class Workflow_EditPage_E_HR_EmployeeRegular : E_WorkflowFormBase
{
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string HRDeptCode = System.Configuration.ConfigurationManager.AppSettings["HRDeptCode"];

    #region 重载函数
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbQualityScore1.Attributes.Add("onBlur", "Check(this,5);");
            tbQualityScore2.Attributes.Add("onBlur", "Check(this,5);");
            tbQualityScore3.Attributes.Add("onBlur", "Check(this,20);");
            tbQualityScore4.Attributes.Add("onBlur", "Check(this,7.5);");
            tbQualityScore5.Attributes.Add("onBlur", "Check(this,7.5);");
            tbQualityScore6.Attributes.Add("onBlur", "Check(this,7.5);");
            tbQualityScore7.Attributes.Add("onBlur", "Check(this,7.5);");
            tbQualityScore8.Attributes.Add("onBlur", "Check(this,10);");
            tbQualityScore9.Attributes.Add("onBlur", "Check(this,10);");
            tbQualityScore10.Attributes.Add("onBlur", "Check(this,15);");
            InitApproveList();
            StartDeptId = _BPMContext.CurrentUser.MainDeptId;
            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("HR_ER_");
                tbReportCode.Text = FormId;
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
            Pkurg.PWorldBPM.Business.BIZ.HR_EmployeeRegular info = BizContext.HR_EmployeeRegular.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                InitApproveList2(info.IsGroup);

                tbReportCode.Text = info.FormID;
                tbUserName.Text = info.UserName;
                tbDeptName.Text = info.DeptName;
                tbEntryTime.Value = info.EntryTime;
                tbProbationPeriod.Text = info.ProbationPeriod;
                tbProbationPeriodStart.Value = info.ProbationPeriodStart;
                tbProbationPeriodEnd.Value = info.ProbationPeriodEnd;
                tbPostLevel.Text = info.PostLevel;
                tbPost.Text = info.Post;
                tbAchievement.Text = info.Achievement;
                tbSign.Text = info.Sign;
                tbSignDate.Text = info.SignDate;
                tbQualityScore1.Text = info.QualityScore1;
                tbQualityScore2.Text = info.QualityScore2;
                tbQualityScore3.Text = info.QualityScore3;
                tbQualityScore4.Text = info.QualityScore4;
                tbQualityScore5.Text = info.QualityScore5;
                tbQualityScore6.Text = info.QualityScore6;
                tbQualityScore7.Text = info.QualityScore7;
                tbQualityScore8.Text = info.QualityScore8;
                tbQualityScore9.Text = info.QualityScore9;
                tbQualityScore10.Text = info.QualityScore10;
                ddlWorkCompletion.SelectedValue = info.WorkCompletion;
                tbAdvantage.Text = info.Advantage;
                tbDisadvantage.Text = info.Disadvantage;
                tbSuggest.Text = info.Suggest;
                tbQualityScore.Text = info.QualityScore;
                tbAchievementScore.Text = info.AchievementScore;
                tbTatolScore.Text = info.TatolScore;
                ddlIsAgreeRegular.SelectedValue = info.IsAgreeRegular;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void SaveFormData()
    {
        var info = BizContext.HR_EmployeeRegular.FirstOrDefault(x => x.FormID == FormId);

        if (info == null)
        {
            info = new HR_EmployeeRegular()
            {
               FormID = FormId,
                UserName = tbUserName.Text,
                LoginID = tbLoginID.Value,
                DeptName = tbDeptName.Text,
                DeptCode = tbDeptCode.Value,
                EntryTime = tbEntryTime.Value,
                ProbationPeriod = tbProbationPeriod.Text,
                ProbationPeriodStart = tbProbationPeriodStart.Value,
                ProbationPeriodEnd = tbProbationPeriodEnd.Value,
                PostLevel = tbPostLevel.Text,
                QualityScore1 = tbQualityScore1.Text,
                QualityScore2 = tbQualityScore2.Text,
                QualityScore3 = tbQualityScore3.Text,
                QualityScore4 = tbQualityScore4.Text,
                QualityScore5 = tbQualityScore5.Text,
                QualityScore6 = tbQualityScore6.Text,
                QualityScore7 = tbQualityScore7.Text,
                QualityScore8 = tbQualityScore8.Text,
                QualityScore9 = tbQualityScore9.Text,
                QualityScore10 = tbQualityScore10.Text,
                QualityScore = tbQualityScore.Text,
                AchievementScore = tbAchievementScore.Text,
                TatolScore = tbTatolScore.Text,
                IsGroup = hfIsGroup.Value
            };
            BizContext.HR_EmployeeRegular.InsertOnSubmit(info);
        }
        else
        {
            info.UserName = tbUserName.Text;
            info.LoginID = tbLoginID.Value;
            info.DeptName = tbDeptName.Text;
            info.DeptCode = tbDeptCode.Value;
            info.EntryTime = tbEntryTime.Value;
            info.ProbationPeriod = tbProbationPeriod.Text;
            info.ProbationPeriodStart = tbProbationPeriodStart.Value;
            info.ProbationPeriodEnd = tbProbationPeriodEnd.Value;
            info.PostLevel = tbPostLevel.Text;
            info.QualityScore1 = tbQualityScore1.Text;
            info.QualityScore2 = tbQualityScore2.Text;
            info.QualityScore3 = tbQualityScore3.Text;
            info.QualityScore4 = tbQualityScore4.Text;
            info.QualityScore5 = tbQualityScore5.Text;
            info.QualityScore6 = tbQualityScore6.Text;
            info.QualityScore7 = tbQualityScore7.Text;
            info.QualityScore8 = tbQualityScore8.Text;
            info.QualityScore9 = tbQualityScore9.Text;
            info.QualityScore10 = tbQualityScore10.Text;
            info.QualityScore = tbQualityScore.Text;
            info.AchievementScore = tbAchievementScore.Text;
            info.TatolScore = tbTatolScore.Text;
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
        dataFields.Add("Employee", tbLoginID.Value);
        dataFields.Add("HRDeptManager2", "noapprovers");
        dataFields.Add("RDeptManager", "noapprovers");
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
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = tbDeptCode.Value, RoleName = "部门负责人", Name = "EDeptManager" });
        if (tbDeptName.Text.Contains("人力资源部"))
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "HRDeptManager" });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = HRDeptCode, RoleName = "部门负责人", Name = "HRDeptManager" });
        }
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = tbDeptCode.Value, RoleName = "主管助理总裁,主管副总裁", Name = "Director" });
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
        FormTitle = tbUserName.Text + "-转正审批(" + DateTime.Now.ToShortDateString() + ")";
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
        FormTitle = tbUserName.Text + "-转正审批(" + DateTime.Now.ToShortDateString() + ")";
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
        else if (IsGroup == "group")
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
    /// 初始化领导名称
    /// </summary>
    private void InitLeader()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(tbDeptCode.Value, "部门负责人");
        DataTable dtHRDeptManager = bfurd.GetSelectRoleUser(HRDeptCode, "部门负责人");
        DataTable dtDirector = bfurd.GetSelectRoleUser(tbDeptCode.Value, "主管副总裁");
        DataTable dtPresident = bfurd.GetSelectRoleUser(PKURGICode, "董事长");

        if (dtDeptManager.Rows.Count != 0)
        {
            lbDeptManager2.Text = "(" + dtDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        else
        {
            lbDeptManager2.Text = "";
        }
        if (dtHRDeptManager.Rows.Count != 0)
        {
            lbHRDeptManager2.Text = "(" + dtHRDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtDirector.Rows.Count != 0)
        {
            lbDirector2.Text = "(" + dtDirector.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        else
        {
            lbDirector2.Text = "";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbPresident2.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        InitLeader();
    }

    private string SumQualityScore()
    {
        float QualityScore = 0;
        QualityScore = QualityScore + float.Parse(tbQualityScore1.Text);
        QualityScore = QualityScore + float.Parse(tbQualityScore2.Text);
        QualityScore = QualityScore + float.Parse(tbQualityScore3.Text);
        QualityScore = QualityScore + float.Parse(tbQualityScore4.Text);
        QualityScore = QualityScore + float.Parse(tbQualityScore5.Text);
        QualityScore = QualityScore + float.Parse(tbQualityScore6.Text);
        QualityScore = QualityScore + float.Parse(tbQualityScore7.Text);
        QualityScore = QualityScore + float.Parse(tbQualityScore8.Text);
        QualityScore = QualityScore + float.Parse(tbQualityScore9.Text);
        QualityScore = QualityScore + float.Parse(tbQualityScore10.Text);
        return QualityScore.ToString();
    }

    #endregion

}
