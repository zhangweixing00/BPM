using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_EditPage_E_HR_CadresOrRemoval : UPageBase
{
    public string className = "Workflow_EditPage_E_HR_CadresOrRemoval";

    CadresOrRemoval Eitems = new CadresOrRemoval();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    string GroupCode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string HRDeptCode = System.Configuration.ConfigurationManager.AppSettings["HRDeptCode"];

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

    protected override void OnPreRender(EventArgs e)
    {
        //防止二次提交
        ScriptManager.RegisterOnSubmitStatement(Page, typeof(Page), "Go_disabled", "disabledButton('lbSave');disabledButton('lbSubmit');disabledButton('lbClose');");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Clear_disabled", "enableButton('lbSave');enableButton('lbSubmit');enableButton('lbClose');", true);

        if (!IsPostBack)
        {
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitApproveList();
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(Request.QueryString["id"]);
                if (info != null)
                {
                    ViewState["FormID"] = info.FormId;
                    BindFormData();
                    SetUserControlInstance();
                }
            }
            else
            {
                FormId = BPMHelp.GetSerialNumber("HR_CR_");
                tbReportCode.Text = FormId;
            }
            InitLeader();
            if (chkCadresOrRemoval.SelectedIndex != -1)
            {
                tbCadre.Visible = chkCadresOrRemoval.SelectedIndex != 1 ? true : false;
                tbRemoval.Visible = chkCadresOrRemoval.SelectedIndex != 0 ? true : false;
            }
        }
    }

    private void InitLeader()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtHRDeptManager = bfurd.GetSelectRoleUser(HRDeptCode, "部门负责人");
        DataTable dtChairman = bfurd.GetSelectRoleUser(GroupCode, "董事长");
        DataTable dtDeptManagers = bfurd.GetSelectRoleUser(GroupCode, "部门总成员");
        DataTable dtDirectors = bfurd.GetSelectRoleUser(GroupCode, "总办会成员");

        if (dtHRDeptManager.Rows.Count != 0)
        {
            lbHRDeptManager.Text = "(" + dtHRDeptManager.Rows[0]["EmployeeName"].ToString() + ")审核";
        }
        if (dtChairman.Rows.Count != 0)
        {
            lbChairman.Text = "(" + dtChairman.Rows[0]["EmployeeName"].ToString() + ")审批";
        }

        if (string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            foreach (DataRow user in dtDeptManagers.Rows)
            {
                ListItem li = new ListItem();
                li.Value = "K2:Founder\\" + user["LoginName"].ToString();
                li.Text = user["EmployeeName"].ToString();
                if (!cblDeptManager.Items.Contains(li))
                {
                    cblDeptManager.Items.Add(li);
                }
            }

            foreach (DataRow user in dtDirectors.Rows)
            {
                ListItem li = new ListItem();
                li.Value = "K2:Founder\\" + user["LoginName"].ToString();
                li.Text = user["EmployeeName"].ToString();
                if (!cblDirector1.Items.Contains(li))
                {
                    cblDirector1.Items.Add(li);
                }
                if (!cblDirector2.Items.Contains(li))
                {
                    cblDirector2.Items.Add(li);
                }
                if (!cblDirector3.Items.Contains(li))
                {
                    cblDirector3.Items.Add(li);
                }
                if (!cblDirector4.Items.Contains(li))
                {
                    cblDirector4.Items.Add(li);
                }
            }
        }
        else
        {
            XmlDocument xmldoc = new XmlDocument();
            CadresOrRemovalInfo formDataInfo = Eitems.Get(FormId);
            if (!string.IsNullOrEmpty(formDataInfo.LeadersSelected))
            {
                xmldoc.LoadXml(formDataInfo.LeadersSelected);
            }

            InitCheckboxlist(xmldoc, "Director1", cblDirector1);
            InitCheckboxlist(xmldoc, "Director2", cblDirector2);
            InitCheckboxlist(xmldoc, "Director3", cblDirector3);
            InitCheckboxlist(xmldoc, "Director4", cblDirector4);
            InitCheckboxlist(xmldoc, "DeptManager", cblDeptManager);
        }
    }

    private void InitCheckboxlist(XmlDocument xmldoc,string NodTemp,CheckBoxList cbl)
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

    private void BindFormData()
    {
        try
        {
            CadresOrRemovalInfo obj = Eitems.Get(ViewState["FormID"].ToString());
            InitApproveList2(obj.IsGroup);
            tbReportCode.Text = obj.FormID;
            chkCadresOrRemoval.SelectedValue = obj.chkCadresOrRemoval != null ? obj.chkCadresOrRemoval.ToString() : "-1";
            tbCadresName.Text = obj.CadresName;
            tbLocationCompanyDeptJob.Text = obj.LocationCompanyDeptJob;
            tbCadresCompanyDeptJob.Text = obj.CadresCompanyDeptJob;
            tbCadresContent.Text = obj.CadresContent;
            tbRemovalName.Text = obj.RemovalName;
            tbLocationCompanyDeptJobR.Text = obj.LocationCompanyDeptJobR;
            tbRemovalCompanyDeptjob.Text = obj.RemovalCompanyDeptjob;
            tbRemovalContent.Text = obj.RemovalContent;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetUserControlInstance()
    {
        string instId = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(instId))
        {
            UploadAttachments1.ProcId = instId;
            hfInstanceId.Value = instId;
        }
    }

    private CadresOrRemovalInfo SaveData(string ID, string wfStatus)
    {
        SaveWFParams();
        CadresOrRemovalInfo obj = null;
        try
        {
            UploadAttachments1.SaveAttachment(ViewState["FormID"].ToString());
            obj = Eitems.Get(ID);
            bool isEdit = false;
            if (obj == null)
            {
                obj = new CadresOrRemovalInfo();
                obj.FormID = ViewState["FormID"].ToString();
                obj.IsGroup = hfIsGroup.Value;
            }
            else
            {
                isEdit = true;
                obj.FormID = ViewState["FormID"].ToString();
            }
            if (chkCadresOrRemoval.SelectedIndex != -1)
            {
                obj.chkCadresOrRemoval = chkCadresOrRemoval.SelectedValue.ToString();
            }
            obj.CadresName = tbCadresName.Text;
            obj.CadresCompanyDeptJob = tbCadresCompanyDeptJob.Text;
            obj.LocationCompanyDeptJob = tbLocationCompanyDeptJob.Text;
            obj.CadresContent = tbCadresContent.Text;
            obj.RemovalName = tbRemovalName.Text;
            obj.RemovalCompanyDeptjob = tbRemovalCompanyDeptjob.Text;
            obj.LocationCompanyDeptJobR = tbLocationCompanyDeptJobR.Text;
            obj.RemovalContent = tbRemovalContent.Text;
            //obj.ApproveStatus = wfStatus;
            obj.LeadersSelected = lblApprovers.Text;

            if (!isEdit)
            {
                Eitems.Insert(obj);
            }
            else
            {
                Eitems.Update(obj);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return obj;
    }

    private bool SaveWorkFlowInstance(CadresOrRemovalInfo obj, string WfStatus, DateTime? SumitTime, string WfInstanceId)
    {
        bool result = false;
        WorkFlowInstance workFlowInstance = null;
        try
        {
            workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(obj.FormID);
            bool isEdit = false;
            if (workFlowInstance == null)
            {
                workFlowInstance = new WorkFlowInstance();
                workFlowInstance.InstanceId = Guid.NewGuid().ToString();
                workFlowInstance.CreateDeptCode = CurrentEmployee.DepartCode;
                workFlowInstance.CreateDeptName = CurrentEmployee.DepartName;
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.AppId = "3013";
            }
            else
            {
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                isEdit = true;
            }
            workFlowInstance.FormId = obj.FormID;
            if (chkCadresOrRemoval.SelectedIndex == 0)
            {
                workFlowInstance.FormTitle = tbCadresName.Text + "的干部任职审批表";
            }
            else if (chkCadresOrRemoval.SelectedIndex == 1)
            {
                workFlowInstance.FormTitle = tbRemovalName.Text + "的干部免职审批表";
            }
            else
            {
                workFlowInstance.FormTitle = tbCadresName.Text + "的干部任职和"+tbRemovalName.Text + "的干部免职审批表";
            }
            workFlowInstance.WfStatus = WfStatus;
            if (SumitTime != null)
            {
                workFlowInstance.SumitTime = SumitTime;
            }

            if (WfInstanceId != "")
            {
                workFlowInstance.WfInstanceId = WfInstanceId;
            }

            if (!isEdit)
            {
                result = wf_WorkFlowInstance.AddWorkFlowInstance(workFlowInstance);
            }
            else
            {
                result = wf_WorkFlowInstance.UpdateWorkFlowInstance(workFlowInstance);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return result;
    }

    /// <summary>
    /// 保存审批人员参数
    /// </summary>
    private string SaveWFParams()
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmleLeaders = xmldoc.CreateElement("Leaders");
        xmldoc.AppendChild(xmleLeaders);
        List<string> ApproverList = new List<string>();
        string LeaderTemp = string.Empty;
        bool flag = true;//标记

        NewNode(xmldoc, xmleLeaders, "Chairman", GroupCode, "董事长", true);
        NewNode(xmldoc, xmleLeaders, "HRDeptManager", HRDeptCode, "部门负责人", true);
        NewNodecbl(xmldoc, xmleLeaders, "DeptManager", cblDeptManager, true);
        NewNodecbl(xmldoc, xmleLeaders, "Director1", cblDirector1, true);
        NewNodecbl(xmldoc, xmleLeaders, "Director2", cblDirector2, true);
        NewNodecbl(xmldoc, xmleLeaders, "Director3", cblDirector3, true);
        NewNodecbl(xmldoc, xmleLeaders, "Director4", cblDirector4, true);

        if (!flag)
        {
            lblApprovers.Text = null;
        }
        else
        {
            lblApprovers.Text = xmleLeaders.OuterXml;
        }
        return lblApprovers.Text;
    }

    private void NewNode(XmlDocument xmldoc, XmlElement xmleLeaders, string NodeTemp,string DeptCodeTemp,string RoleNameTemp,bool IsCheck)
    {
        string UsersID = "";
        string UsersName = "";
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtHRDeptManager = bfurd.GetSelectRoleUser(DeptCodeTemp, RoleNameTemp);

        foreach (DataRow user in dtHRDeptManager.Rows)
        {
            UsersID = UsersID+(string.IsNullOrEmpty(UsersID)?"":",")+"K2:Founder\\" + user["LoginName"].ToString();
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

    /// <summary>
    /// 设置流程参数
    /// </summary>
    /// <returns></returns>
    private NameValueCollection SetWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(lblApprovers.Text);

        DataFields_Add(dataFields,"Chairman", xmldoc.SelectSingleNode("//Chairman"));
        DataFields_Add(dataFields, "HRDeptManager", xmldoc.SelectSingleNode("//HRDeptManager"));
        DataFields_Add(dataFields, "DeptManager", xmldoc.SelectSingleNode("//DeptManager"));
        DataFields_Add(dataFields, "Director1", xmldoc.SelectSingleNode("//Director1"));
        DataFields_Add(dataFields, "Director2", xmldoc.SelectSingleNode("//Director2"));
        DataFields_Add(dataFields, "Director3", xmldoc.SelectSingleNode("//Director3"));
        DataFields_Add(dataFields, "Director4", xmldoc.SelectSingleNode("//Director4"));
        dataFields.Add("IsPass", "1");
        return dataFields;
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

    protected void Save_Click(object sender, EventArgs e)
    {
        string id = ViewState["FormID"].ToString();
        CadresOrRemovalInfo obj = SaveData(id, "00");
        if (obj != null)
        {
            if (SaveWorkFlowInstance(obj, "0", null, ""))
            {
                Alert(Page, "保存成功！");
            }
        }
        else
        {
            Alert(Page, "保存失败");
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        string SaveVerification = SaveWFParams();
        if (string.IsNullOrEmpty(SaveVerification))
        {
            return;
        }
        NameValueCollection dataFields = SetWFParams();
        
        int wfInstanceId = 0; //process instance id
        string id = ViewState["FormID"].ToString();

        CadresOrRemovalInfo obj = SaveData(id, "02");
        if (obj != null)
        {
            //开启流程
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            WorkflowHelper.StartProcess(@"K2Workflow\HR_CadresOrRemoval", id, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance(obj, "1", DateTime.Now, wfInstanceId.ToString()))
                {

                    if (Eitems.UpdateStatus(id, "02"))
                    {
                        string Opinion = "";
                        string ApproveResult = "同意";
                        string OpinionType = "";
                        string IsSign = "0";
                        string DelegateUserName = "";
                        string DelegateUserCode = "";
                        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(obj.FormID);

                        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
                        {
                             ApprovalID = Guid.NewGuid().ToString(),
                
                            FormID = id,
                            InstanceID = workFlowInstance.InstanceId,
                            Opinion = Opinion,
                            ApproveAtTime = DateTime.Now,
                            ApproveByUserCode = CurrentEmployee.EmployeeCode,
                            ApproveByUserName = CurrentEmployee.EmployeeName,
                            ApproveResult = ApproveResult,
                            OpinionType = OpinionType,
                            CurrentActiveName = "拟稿",
                            ISSign = IsSign,
                
                            DelegateUserName = DelegateUserName,
                            DelegateUserCode = DelegateUserCode,
                            CreateAtTime = DateTime.Now,
                            CreateByUserCode = CurrentEmployee.EmployeeCode,
                            CreateByUserName = CurrentEmployee.EmployeeName,
                            UpdateAtTime = DateTime.Now,
                            UpdateByUserCode = CurrentEmployee.EmployeeCode,
                            UpdateByUserName = CurrentEmployee.EmployeeName,
                            FinishedTime = DateTime.Now
                        };
                        BFApprovalRecord bfApproval = new BFApprovalRecord();
                        bfApproval.AddApprovalRecord(appRecord);
                    }
                }
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
    }

    /// <summary>
    /// 终止
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        if (_BPMContext.ProcInst != null)
        {
            new WF_WorkFlowInstance().UpdateNowStatusByFormID(FormId, "5");
            DisplayMessage.ExecuteJs("alert('操作成功'); window.close();");
        }
        else
        {
            DisplayMessage.ExecuteJs("window.close();");
        }
    }

    void Alert(Page page, object message)
    {
        StringBuilder sb = new StringBuilder();
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message) + sb.ToString();
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    /// <summary>
    /// 执行JS
    /// </summary>
    /// <param name="page"></param>
    /// <param name="js"></param>
    public static void RunJs(Page page, string js)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    private static string GetRoleUsers(string dept, string roleName)
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        StringBuilder dataInfos = new StringBuilder();
        DataTable dtDept = bfurd.GetSelectRoleUser(dept, roleName);
        if (dtDept != null && dtDept.Rows.Count != 0)
        {
            foreach (DataRow rowItem in dtDept.Rows)
            {
                dataInfos.AppendFormat("K2:Founder\\{0},", rowItem["LoginName"].ToString());
            }
        }
        return dataInfos.ToString().Trim(',');
    }

    protected void chkCadresOrRemoval_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkCadresOrRemoval.SelectedIndex != -1)
        {
            tbCadre.Visible = chkCadresOrRemoval.SelectedIndex != 1 ? true : false;
            tbRemoval.Visible = chkCadresOrRemoval.SelectedIndex != 0 ? true : false;
        }
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
            lbDirector.Text = "分管领导意见：";
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
                lbDirector.Text = "分管领导意见：";
                lbPresident.Text = "CEO意见：";
            }
        }
    }
}
