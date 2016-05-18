using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3021")]
public partial class Workflow_EditPage_E_HR_SalaryAdjust : E_WorkflowFormBase
{
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string HRDeptCode = System.Configuration.ConfigurationManager.AppSettings["HRDeptCode"];

    #region 重载函数
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitApproveList();

            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("HR_SA_");
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
            StartDeptId = _BPMContext.CurrentUser.MainDeptId;
        }
    }

    protected override void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.HR_SalaryAdjust info = BizContext.HR_SalaryAdjust.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                InitApproveList2(info.IsGroup);

                tbReportCode.Text = info.FormID;
                tbAnnualSalary.Text = info.AnnualSalary;
                tbDeptName.Text = info.DeptName;
                tbEffectiveDate.Value = info.EffectiveDate;
                tbPost.Text = info.Post;
                tbRatio.Text = info.Ratio;
                tbReason.Text = info.Reason;
                tbSalary.Text = info.Salary;
                tbToAnnualSalary.Text = info.ToAnnualSalary;
                tbToDeptName.Text = info.ToDeptName;
                tbToPost.Text = info.ToPost;
                tbToRatio.Text = info.ToRatio;
                tbToSalary.Text = info.ToSalary;
                tbUserName.Text = info.UserName;
                tbWorkPlace.Text = info.WorkPlace;
                hfApprovers.Value = info.LeadersSelected;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void SaveFormData()
    {
        var info = BizContext.HR_SalaryAdjust.FirstOrDefault(x => x.FormID == FormId);

        if (info == null)
        {
            info = new HR_SalaryAdjust()
            {
               FormID = FormId,
                AnnualSalary = tbAnnualSalary.Text,
                DeptName = tbDeptName.Text,
                EffectiveDate = tbEffectiveDate.Value,
                Post = tbPost.Text,
                Ratio = tbRatio.Text,
                Reason = tbReason.Text,
                Salary = tbSalary.Text,
                ToAnnualSalary = tbToAnnualSalary.Text,
                ToDeptName = tbToDeptName.Text,
                ToPost = tbToPost.Text,
                ToRatio = tbToRatio.Text,
                ToSalary = tbToSalary.Text,
                UserName = tbUserName.Text,
                WorkPlace = tbWorkPlace.Text,
                LeadersSelected = SaveApproveList(),
                IsGroup = hfIsGroup.Value
            };
            BizContext.HR_SalaryAdjust.InsertOnSubmit(info);
        }
        else
        {
            info.AnnualSalary = tbAnnualSalary.Text;
            info.DeptName = tbDeptName.Text;
            info.EffectiveDate = tbEffectiveDate.Value;
            info.Post = tbPost.Text;
            info.Ratio = tbRatio.Text;
            info.Reason = tbReason.Text;
            info.Salary = tbSalary.Text;
            info.ToAnnualSalary = tbToAnnualSalary.Text;
            info.ToDeptName = tbToDeptName.Text;
            info.ToPost = tbToPost.Text;
            info.ToRatio = tbToRatio.Text;
            info.ToSalary = tbToSalary.Text;
            info.UserName = tbUserName.Text;
            info.WorkPlace = tbWorkPlace.Text;
            info.LeadersSelected = SaveApproveList();
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
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(hfApprovers.Value);
        dataFields.Add("Employee", "noapprovers");
        dataFields.Add("HRDeptManager", "noapprovers");
        dataFields.Add("RDeptManager", "noapprovers");
        DataFields_Add(dataFields, "EDeptManager", xmldoc.SelectSingleNode("//EDeptManager"));
        DataFields_Add(dataFields, "Director", xmldoc.SelectSingleNode("//Director"));
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
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = HRDeptCode, RoleName = "部门负责人", Name = "HRDeptManager2" });
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
        FormTitle = tbUserName.Text + "-薪酬调整审批";
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
        FormTitle = tbUserName.Text + "-薪酬调整审批";
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
        else if(IsGroup=="group")
        {
            lbDirector.Text = "分管领导意见：";
            lbPresident.Text = "CEO意见：";
            hfIsGroup.Value = "1";
        }
        else if (IsGroup == "EI")
        {
            lbDirector.Text = "相关董事意见：";
            lbPresident.Text = "董事长意见：";
            hfIsGroup.Value = "2";
            lbTitle.Text = "Peking University Resources Group Investment Company Limited";
            trRDeptManager.Visible = false;
        }
        else if (IsGroup == "EG")
        {
            lbDirector.Text = "分管领导意见：";
            lbPresident.Text = "CEO意见：";
            hfIsGroup.Value = "3";
            trRDeptManager.Visible = false;
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
            else if (IsGroup == "2")
            {
                trRDeptManager.Visible = false;
            }
            else if (IsGroup == "3")
            {
                lbDirector.Text = "用人部门分管领导意见：";
                lbPresident.Text = "CEO意见：";
                trRDeptManager.Visible = false;
            }
        }
    }

    /// <summary>
    /// 初始化领导名称
    /// </summary>
    private void InitLeader()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtHRDeptManager = bfurd.GetSelectRoleUser(HRDeptCode, "部门负责人");
        DataTable dtPresident = bfurd.GetSelectRoleUser(PKURGICode, "董事长");
        DataTable dtDeptManagers = bfurd.GetSelectRoleUser(PKURGICode, "部门总成员");
        DataTable dtDirectors = bfurd.GetSelectRoleUser(PKURGICode, "总办会成员");

        if (dtHRDeptManager.Rows.Count != 0)
        {
            lbHRDeptManager2.Text = "(" + dtHRDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbPresident2.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }

        foreach (DataRow user in dtDeptManagers.Rows)
        {
            ListItem li = new ListItem();
            li.Value = "K2:Founder\\" + user["LoginName"].ToString();
            li.Text = user["EmployeeName"].ToString();
            if (!cblRDeptManager.Items.Contains(li))
            {
                cblRDeptManager.Items.Add(li);
            }
        }
             
        foreach (DataRow user in dtDirectors.Rows)
        {
            ListItem li = new ListItem();
            li.Value = "K2:Founder\\" + user["LoginName"].ToString();
            li.Text = user["EmployeeName"].ToString();
            if (!cblDirector.Items.Contains(li))
            {
                cblDirector.Items.Add(li);
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(hfApprovers.Value);
            InitCheckboxlist(xmldoc, "/Leaders/Director", cblDirector);
            InitCheckboxlist(xmldoc, "/Leaders/EDeptManager", cblRDeptManager);
        }
    }

    /// <summary>
    /// 初始化Checkboxlist
    /// </summary>
    private void InitCheckboxlist(XmlDocument xmldoc, string NodTemp, CheckBoxList cbl)
    {
        XmlNode XmlNode = xmldoc.SelectSingleNode(NodTemp);
        if (XmlNode != null && XmlNode.Attributes["ID"].Value.Length > 0)
        {
            foreach (string UserGuid in XmlNode.Attributes["ID"].Value.Split(','))
            {
                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    if (cbl.Items[i].Value == UserGuid)
                    {
                        cbl.Items[i].Selected = true;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 保存审批人员参数
    /// </summary>
    private string SaveApproveList()
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmleLeaders = xmldoc.CreateElement("Leaders");
        xmldoc.AppendChild(xmleLeaders);
        List<string> ApproverList = new List<string>();
        string LeaderTemp = string.Empty;

        NewNodecbl(xmldoc, xmleLeaders, "EDeptManager", cblRDeptManager, true);
        NewNodecbl(xmldoc, xmleLeaders, "Director", cblDirector, true);
        NewNode(xmldoc, xmleLeaders, "HRDeptManager", HRDeptCode, "部门负责人", true);
        NewNode(xmldoc, xmleLeaders, "Chairman", PKURGICode, "董事长", true);

        hfApprovers.Value = xmleLeaders.OuterXml;
        return hfApprovers.Value;
    }

    private void NewNode(XmlDocument xmldoc, XmlElement xmleLeaders, string NodeTemp, string DeptCodeTemp, string RoleNameTemp, bool IsCheck)
    {
        string UsersID = "";
        string UsersName = "";
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtHRDeptManager = bfurd.GetSelectRoleUser(DeptCodeTemp, RoleNameTemp);

        foreach (DataRow user in dtHRDeptManager.Rows)
        {
            UsersID = UsersID + (string.IsNullOrEmpty(UsersID) ? "" : ",") + "K2:Founder\\" + user["LoginName"].ToString();
            UsersName = UsersName + (string.IsNullOrEmpty(UsersName) ? "" : ",") + user["EmployeeName"].ToString();
        }

        XmlElement xmleTemp = xmldoc.CreateElement(NodeTemp);
        xmleLeaders.AppendChild(xmleTemp);
        xmleTemp.SetAttribute("ID", UsersID);
        xmleTemp.SetAttribute("Name", UsersName);
    }

    private void NewNodecbl(XmlDocument xmldoc, XmlElement xmleLeaders, string NodeTemp, CheckBoxList cblTemp, bool IsCheck)
    {
        string UsersID = "";
        string UsersName = "";
        if (cblTemp.SelectedIndex != -1)
        {
            for (int i = 0; i < cblTemp.Items.Count; i++)
            {
                if (cblTemp.Items[i].Selected)
                {
                    UsersID += (string.IsNullOrEmpty(UsersID) ? "" : ",") + cblTemp.Items[i].Value;
                    UsersName += (string.IsNullOrEmpty(UsersName) ? "" : ",") + cblTemp.Items[i].Text;
                }
            }
        }

        XmlElement xmleTemp = xmldoc.CreateElement(NodeTemp);
        xmleLeaders.AppendChild(xmleTemp);
        xmleTemp.SetAttribute("ID", UsersID);
        xmleTemp.SetAttribute("Name", UsersName);
    }

    private NameValueCollection DataFields_Add(NameValueCollection dataFields, string DataFieldName, XmlNode NodeTemp)
    {
        string LeaderTemp;
        if (NodeTemp != null)
        {
            LeaderTemp = string.IsNullOrEmpty(NodeTemp.Attributes["ID"].Value) ? "noapprovers" : NodeTemp.Attributes["ID"].Value;
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add(DataFieldName, LeaderTemp);
        return dataFields;
    }
    #endregion
}
