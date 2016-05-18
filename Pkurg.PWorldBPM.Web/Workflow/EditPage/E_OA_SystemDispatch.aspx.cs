using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ.OA;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Entites.BIZ.OA;

public partial class Workflow_EditPage_E_OA_SystemDispatch : UPageBase
{
    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];


    #region 全局函数+初始化
    string strApprovers = string.Empty;
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

    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    /// <summary>
    /// 页面初始化
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitStartDeptment();

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(Request.QueryString["id"]);
                FormId = info.FormId;
                InitFormData();
                SetUserControlInstance();
            }
            else
            {
                string StartDeptId = ddlDepartName.SelectedItem.Value;
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(StartDeptId);
                Countersign1.CounterSignDeptId = StartDeptId;
                FormId = string.Empty;
            }
            InitCheckBoxList();
            InitLeader();
            InitForm();
        }
    }

    /// <summary>
    /// 初始化经办部门
    /// </summary>
    private void InitStartDeptment()
    {
        if (!IsPostBack)
        {
            Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(_BPMContext.CurrentUser.MainDeptId);
            BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
            Pkurg.PWorld.Entities.TList<Department> deptInfo2 = bfurd.GetDeptListByEmployeeCodeAndRoleName(_BPMContext.CurrentPWordUser.EmployeeCode, "部门成员");

            //初始化发起人所属“部门成员”的部门，若为空则初始化发起人所在部门
            foreach (Department DeptItem in deptInfo2)
            {
                ddlDepartName.Items.Add(new ListItem()
                {
                    Text = DeptItem.Remark,
                    Value = DeptItem.DepartCode
                });
            }
            if (deptInfo2.Count == 0)
            {
                ddlDepartName.Items.Add(new ListItem()
                {
                    Text = deptInfo.Remark,
                    Value = deptInfo.DepartCode
                });
            }
        }
    }

    /// <summary>
    /// 初始化总办会成员复选框和董事长勾选框
    /// </summary>
    private void InitCheckBoxList()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtLeaders = bfurd.GetSelectRoleUser(PKURGICode, "总办会成员");
        string strLeaders = GetRoleUsers(PKURGICode, "总办会成员");

        foreach (DataRow user in dtLeaders.Rows)
        {
            ListItem li = new ListItem();
            li.Value = "K2:Founder\\" + user["LoginName"].ToString();
            li.Text = user["EmployeeName"].ToString();
            if (!cblTopLeaders.Items.Contains(li))
            {
                cblTopLeaders.Items.Add(li);
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            XmlDocument xmldoc = new XmlDocument();
            SystemDispatchInfo formDataInfo = SystemDispatch.GetSystemDispatchInfo(FormId);
            if (!string.IsNullOrEmpty(formDataInfo.LeadersSelected))
            {
                xmldoc.LoadXml(formDataInfo.LeadersSelected);
            }

            XmlNode NodeLeaders = xmldoc.SelectSingleNode("//TopLeaders");
            if (NodeLeaders != null && NodeLeaders.Attributes["ID"].Value.Length > 0)
            {
                foreach (string UserGuid in NodeLeaders.Attributes["ID"].Value.Split(','))
                {
                    for (int i = 0; i < cblTopLeaders.Items.Count; i++)
                    {
                        if (cblTopLeaders.Items[i].Value == UserGuid)
                        {
                            cblTopLeaders.Items[i].Selected = true;
                        }
                    }
                }
            }
            XmlNode NodeChairman = xmldoc.SelectSingleNode("//Chairman");
            if (NodeChairman != null && NodeChairman.Attributes["ID"].Value != "noapprovers")
            {
                cbChairman.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < cblTopLeaders.Items.Count; i++)
            {
                cblTopLeaders.Items[i].Selected = true;
            }
        }
    }

    /// <summary>
    /// 加载发起部门及相关控件
    /// </summary>
    private void InitFormData()
    {
        try
        {
            SystemDispatchInfo formDataInfo = SystemDispatch.GetSystemDispatchInfo(FormId);

            ListItem item = ddlDepartName.Items.FindByValue(formDataInfo.StartDeptId);
            if (item != null)
            {
                ddlDepartName.SelectedIndex = ddlDepartName.Items.IndexOf(item);
            }
            ListItem departItem = ddlDepartName.Items.FindByValue(formDataInfo.StartDeptId);
            if (departItem == null)
            {
                ExceptionHander.GoToErrorPage();
                return;
            }
            departItem.Selected = true;
            Countersign1.CounterSignDeptId = formDataInfo.StartDeptId;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 初始化用户控件
    /// </summary>
    private void SetUserControlInstance()
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
        FlowRelated1.ProcId = workFlowInstance.InstanceId;
        Countersign1.ProcId = workFlowInstance.InstanceId;
        UploadAttachments1.ProcId = workFlowInstance.InstanceId;
        hfInstanceId.Value = workFlowInstance.InstanceId;
        OpinionDeptleader.InstanceId = workFlowInstance.InstanceId;
        OpinionRealateDept.InstanceId = workFlowInstance.InstanceId;
        OpinionTopLeaders.InstanceId = workFlowInstance.InstanceId;
        OpinionCEO.InstanceId = workFlowInstance.InstanceId;
        OpinionChairman.InstanceId = workFlowInstance.InstanceId;
    }

    private void InitLeader()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        string GroupCode = System.Configuration.ConfigurationManager.AppSettings["GroupCode"];
        string startDeptId = ddlDepartName.SelectedItem.Value;
        DataTable dtDeptleader = bfurd.GetSelectRoleUser(startDeptId, "部门负责人");
        DataTable dtCEO = bfurd.GetSelectRoleUser(GroupCode, "总裁");
        DataTable dtChairman = bfurd.GetSelectRoleUser(GroupCode, "董事长");

        if (dtDeptleader.Rows.Count != 0)
        {
            lbDeptleader.Text = "部门负责人(" + dtDeptleader.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtCEO.Rows.Count != 0)
        {
            lbCEO.Text = "CEO(" + dtCEO.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtChairman.Rows.Count != 0)
        {
            lbChairman.Text = "(" + dtChairman.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
    }

    private void InitForm()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            SystemDispatchInfo info = SystemDispatch.GetSystemDispatchInfo(FormId);
            cblSecurityLevel.SelectedIndex = Convert.ToInt32(info.SecurityLevel);
            cblUrgenLevel.SelectedIndex = Convert.ToInt32(info.UrgenLevel);
            UpdatedTextBox.Value = info.DateTime;
            tbDateTime.Text = info.DateTime;
            tbUserName.Text = info.UserName;
            tbMobile.Text = info.Mobile;
            tbTitle.Text = info.Title;
            tbContent.Text = info.Content;
            cblRedHeadDocument.SelectedIndex = int.Parse(info.RedHeadDocument);
            cblIsPublish.SelectedIndex = int.Parse(info.IsPublish);
            cblSecurityLevel.SelectedIndex = int.Parse(info.SecurityLevel);
            cblUrgenLevel.SelectedIndex = int.Parse(info.UrgenLevel);
            tbReportCode.Text = info.FormId;
        }
        else
        {
            tbUserName.Text = _BPMContext.CurrentPWordUser.EmployeeName;
            tbMobile.Text = _BPMContext.CurrentPWordUser.MobilePhone;
            UpdatedTextBox.Value = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }
    #endregion

    #region 按钮事件
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        if (_BPMContext.ProcInst != null)
        {
            new WF_WorkFlowInstance().UpdateNowStatusByFormID(FormId, "5");
            DisplayMessage.ExecuteJs("alert('操作成功'); window.close();");
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        int wfInstanceId = 0; //process instance id
        string id = ViewState["FormID"].ToString();

        SystemDispatchInfo dataInfo = SaveFormData();
        Countersign1.SaveData(true);//会签数据保存

        if (dataInfo != null)
        {
            UploadAttachments1.SaveAttachment(FormId);
            Countersign1.SaveAndSubmit();//会签数据保存

            NameValueCollection dataFields = SetWFParams();//工作流参数
            if (dataFields == null)
            {
                return;
            }

            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;
            WorkflowHelper.StartProcess(@"K2Workflow\OA_SystemDispatch", FormId, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance("1", DateTime.Now, wfInstanceId.ToString()))
                {
                    SaveWorkItem();
                    DisplayMessage.ExecuteJs("alert('提交成功');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
                    return;
                }
            }
        }

        Alert("提交失败");
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        SaveWFParams();
        NameValueCollection dataFields = SetWFParams();//工作流参数
        SystemDispatchInfo dataInfo = SaveFormData();

        if (dataInfo != null)
        {
            UploadAttachments1.SaveAttachment(FormId);

            if (SaveWorkFlowInstance("0", null, ""))
            {
                Alert("保存完成");
            }
        }
        else
        {
            Alert("保存失败");
        }
    }

    #endregion

    /// <summary>
    /// 保存表单
    /// </summary>
    /// <returns></returns>
    private SystemDispatchInfo SaveFormData()
    {
        bool isEdit = false;
        SystemDispatchInfo info = null;
        try
        {
            info = SystemDispatch.GetSystemDispatchInfo(FormId);
            if (info == null)
            {
                info = new SystemDispatchInfo();
                info.FormId = FormId;
                info.StartDeptId = ddlDepartName.SelectedItem.Value;
                info.DeptName = ddlDepartName.SelectedItem.Text;
                info.SecurityLevel = cblSecurityLevel.SelectedIndex.ToString();
                info.UrgenLevel = cblUrgenLevel.SelectedIndex.ToString();
                FormId = BPMHelp.GetSerialNumber("OA_ZD_");
                info.FormId = FormId;
                info.DateTime = UpdatedTextBox.Value;
                info.UserName = tbUserName.Text;
                info.Mobile = tbMobile.Text;
                info.Title = tbTitle.Text;
                info.IsPublish = cblIsPublish.SelectedIndex.ToString();
                info.RedHeadDocument = cblRedHeadDocument.SelectedIndex.ToString();
                info.Content = tbContent.Text;
                info.LeadersSelected = strApprovers;
            }
            else
            {
                isEdit = true;
                info.FormId = FormId;
                info.StartDeptId = ddlDepartName.SelectedItem.Value;
                info.DeptName = ddlDepartName.SelectedItem.Text;
                info.SecurityLevel = cblSecurityLevel.SelectedIndex.ToString();
                info.UrgenLevel = cblUrgenLevel.SelectedIndex.ToString();
                info.FormId = tbReportCode.Text;
                info.DateTime = UpdatedTextBox.Value;
                info.UserName = tbUserName.Text;
                info.Mobile = tbMobile.Text;
                info.Title = tbTitle.Text;
                info.IsPublish = cblIsPublish.SelectedIndex.ToString();
                info.RedHeadDocument = cblRedHeadDocument.SelectedIndex.ToString();
                info.Content = tbContent.Text;
                info.LeadersSelected = strApprovers;
            }
            if (!isEdit)
            {
                SystemDispatch.InsertSystemDispatchInfo(info);
            }
            else
            {
                SystemDispatch.UpdateSystemDispatch(info);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return info;
    }

    /// <summary>
    /// 保存审批人员参数
    /// </summary>
    private string SaveWFParams()
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmleLeaders = xmldoc.CreateElement("Leaders");
        xmldoc.AppendChild(xmleLeaders);

        List<string> ApproverList = new List<string>();//所有参与审批的用户列表，用来避免重复审批，已存在用户不再传入
        string GroupCode = System.Configuration.ConfigurationManager.AppSettings["GroupCode"];
        string startDeptId = ddlDepartName.SelectedItem.Value;
        string LeaderTemp = string.Empty;
        bool flag = true;//标记
        if (string.IsNullOrEmpty(GetRoleUsers(startDeptId, "部门负责人")))
        {
            flag = false;
            Alert(Page, "发起部门负责人尚未配置！");
        }
        List<string> countersigns = Countersign1.Result.Split(',').ToList();
        foreach (var item in countersigns)
        {
            if (!string.IsNullOrEmpty(item))
            {
                if (string.IsNullOrEmpty(GetRoleUsers(item, "部门负责人")))
                {
                    flag = false;
                    Department countetDept = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(item);
                    Alert(Page, countetDept.Remark + "部门负责人尚未配置！");
                }
            }
        }
        if (string.IsNullOrEmpty(GetRoleUsers(GroupCode, "总裁")))
        {
            flag = false;
            Alert(Page, "集团CEO尚未配置！");
        }

        if (this.cbChairman.Checked)
        {
            LeaderTemp = FilterDataField2(GetRoleUsers(GroupCode, "董事长"));
            XmlElement xmleChairman = xmldoc.CreateElement("Chairman");
            xmleLeaders.AppendChild(xmleChairman);
            xmleChairman.SetAttribute("ID", LeaderTemp);
        }
        else
        {
            LeaderTemp = string.Empty;
        }

        if (1 == 1)
        {
            LeaderTemp = FilterDataField2(GetRoleUsers(GroupCode, "总裁"));
            XmlElement xmleCEO = xmldoc.CreateElement("CEO");
            xmleLeaders.AppendChild(xmleCEO);
            xmleCEO.SetAttribute("ID", LeaderTemp);
        }

        if (cblTopLeaders.SelectedIndex != -1)
        {
            LeaderTemp = string.Empty;
            XmlElement xmlTopLeaders = xmldoc.CreateElement("TopLeaders");
            xmleLeaders.AppendChild(xmlTopLeaders);

            for (int i = 0; i < cblTopLeaders.Items.Count; i++)
            {
                if (cblTopLeaders.Items[i].Selected && !ApproverList.Contains(cblTopLeaders.Items[i].Value))
                {
                    ApproverList.Add(cblTopLeaders.Items[i].Value);
                    LeaderTemp += cblTopLeaders.Items[i].Value + ",";
                }
            }
            xmlTopLeaders.SetAttribute("ID", LeaderTemp);
        }
        else
        {
            LeaderTemp = string.Empty;
        }

        if (!string.IsNullOrEmpty(Countersign1.Result))
        {
            LeaderTemp = FilterDataField2(Countersign1.GetCounterSignUsers());
            XmlElement xmleCountersign = xmldoc.CreateElement("Countersign");
            xmleLeaders.AppendChild(xmleCountersign);
            xmleCountersign.SetAttribute("ID", LeaderTemp);
        }
        else
        {
            LeaderTemp = string.Empty;
        }

        if (1 == 1)
        {
            LeaderTemp = FilterDataField2(GetRoleUsers(startDeptId, "部门负责人"));
            XmlElement xmleDeptManager = xmldoc.CreateElement("DeptManager");
            xmleLeaders.AppendChild(xmleDeptManager);
            xmleDeptManager.SetAttribute("ID", LeaderTemp);
        }

        strApprovers = xmleLeaders.OuterXml;
        return strApprovers;
    }

    /// <summary>
    /// 设置流程参数
    /// </summary>
    /// <returns></returns>
    private NameValueCollection SetWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmleLeaders = xmldoc.CreateElement("Leaders");
        xmldoc.AppendChild(xmleLeaders);

        List<string> ApproverList = new List<string>();//所有参与审批的用户列表，用来避免重复审批，已存在用户不再传入
        string GroupCode = System.Configuration.ConfigurationManager.AppSettings["GroupCode"];
        string startDeptId = ddlDepartName.SelectedItem.Value;
        string LeaderTemp = string.Empty;
        bool flag = true;//标记
        if (string.IsNullOrEmpty(GetRoleUsers(startDeptId, "部门负责人")))
        {
            flag = false;
            Alert(Page, "发起部门负责人尚未配置！");
        }
        List<string> countersigns = Countersign1.Result.Split(',').ToList();
        foreach (var item in countersigns)
        {
            if (!string.IsNullOrEmpty(item))
            {
                if (string.IsNullOrEmpty(GetRoleUsers(item, "部门负责人")))
                {
                    flag = false;
                    Department countetDept = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(item);
                    Alert(Page, countetDept.Remark + "部门负责人尚未配置！");
                }
            }
        }
        if (string.IsNullOrEmpty(GetRoleUsers(GroupCode, "总裁")))
        {
            flag = false;
            Alert(Page, "集团CEO尚未配置！");
        }

        if (this.cbChairman.Checked)
        {
            LeaderTemp = FilterDataField2(GetRoleUsers(GroupCode, "董事长"));
            XmlElement xmleChairman = xmldoc.CreateElement("Chairman");
            xmleLeaders.AppendChild(xmleChairman);
            xmleChairman.SetAttribute("ID", LeaderTemp);
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("Chairman", LeaderTemp);
        if (1 == 1)
        {
            LeaderTemp = FilterDataField2(GetRoleUsers(GroupCode, "总裁"));
            XmlElement xmleCEO = xmldoc.CreateElement("CEO");
            xmleLeaders.AppendChild(xmleCEO);
            xmleCEO.SetAttribute("ID", LeaderTemp);
        }
        dataFields.Add("CEO", LeaderTemp);
        if (cblTopLeaders.SelectedIndex != -1)
        {
            LeaderTemp = string.Empty;
            XmlElement xmlTopLeaders = xmldoc.CreateElement("TopLeaders");
            xmleLeaders.AppendChild(xmlTopLeaders);

            for (int i = 0; i < cblTopLeaders.Items.Count; i++)
            {
                if (cblTopLeaders.Items[i].Selected && !ApproverList.Contains(cblTopLeaders.Items[i].Value))
                {
                    ApproverList.Add(cblTopLeaders.Items[i].Value);
                    LeaderTemp += cblTopLeaders.Items[i].Value + ",";
                }
            }
            xmlTopLeaders.SetAttribute("ID", LeaderTemp);
        }
        else
        {
            LeaderTemp = "noapprovers";
        }
        dataFields.Add("TopLeaders", LeaderTemp);
        if (!string.IsNullOrEmpty(Countersign1.Result))
        {
            LeaderTemp = FilterDataField2(Countersign1.GetCounterSignUsers());
            XmlElement xmleCountersign = xmldoc.CreateElement("Countersign");
            xmleLeaders.AppendChild(xmleCountersign);
            xmleCountersign.SetAttribute("ID", LeaderTemp);
        }
        else
        {
            LeaderTemp = string.Empty;
        }
        dataFields.Add("CounterSignUsers", LeaderTemp);
        if (1 == 1)
        {
            LeaderTemp = FilterDataField2(GetRoleUsers(startDeptId, "部门负责人"));
            XmlElement xmleDeptManager = xmldoc.CreateElement("DeptManager");
            xmleLeaders.AppendChild(xmleDeptManager);
            xmleDeptManager.SetAttribute("ID", LeaderTemp);
            dataFields.Add("DeptManager", LeaderTemp);
        }

        strApprovers = xmleLeaders.OuterXml;
        if (!flag)
        {
            dataFields = null;
        }
        return dataFields;
    }

    /// <summary>
    /// 更新流程参数
    /// </summary>
    /// <returns></returns>
    private void UpdateWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();

        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, "founder\\zybpmadmin");
    }

    private string FilterDataField(string dataField_old)
    {
        string dataField = dataField_old.Trim(',');
        if (string.IsNullOrEmpty(dataField))
        {
            dataField = "noapprovers";
        }
        return dataField;
    }
    private string FilterDataField(StringBuilder dataField_old)
    {
        return FilterDataField(dataField_old.ToString().Trim(','));
    }
    private string FilterDataField2(string dataField)
    {
        dataField = dataField.Trim(',');

        //单个审批人
        if (!dataField.Contains(','))
        {
            if (!string.IsNullOrEmpty(dataField) && strApprovers.Contains(dataField + ","))
            {
                dataField = "noapprovers";
            }

            if (string.IsNullOrEmpty(dataField))
            {
                dataField = "noapprovers";
            }
            else
            {
                if (dataField != "noapprovers")
                {
                    strApprovers = strApprovers + dataField + ",";
                }
            }
        }
        else//多个审批人
        {
            //多个审批人过滤完的字符串
            string nowApprovers = "";
            //多个审批人过滤前的集合
            List<string> nowApproverList = dataField.Split(',').ToList();
            foreach (var item in nowApproverList)
            {
                if (!strApprovers.Contains(item + ","))
                {
                    nowApprovers = nowApprovers + item + ",";

                    strApprovers = strApprovers + item + ",";
                }
            }
            dataField = nowApprovers == "" ? "noapprovers" : nowApprovers.Trim(',');
        }

        return dataField;
    }
    private string FilterDataField2(StringBuilder dataField)
    {
        return FilterDataField2(dataField.ToString().Trim(','));
    }

    /// <summary>
    /// 通过部门和角色获取部门列表
    /// </summary>
    private string GetRoleDepts(string item, string role)
    {
        StringBuilder dataInfos = new StringBuilder();
        BFCountersignRoleDepartment counterSignHelper = new BFCountersignRoleDepartment();
        DataTable dtDept = counterSignHelper.GetSelectCountersignDepartment(item, role);
        if (dtDept != null && dtDept.Rows.Count != 0)
        {
            foreach (DataRow rowItem in dtDept.Rows)
            {
                dataInfos.AppendFormat("{0},", rowItem["DepartCode"].ToString());
            }
        }
        return dataInfos.ToString().Trim(',');
    }

    /// <summary>
    /// 通过部门和角色获取用户列表
    /// </summary>
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

    private bool SaveWorkFlowInstance(string WfStatus, DateTime? SumitTime, string WfInstanceId)
    {
        bool result = false;
        WorkFlowInstance workFlowInstance = null;
        try
        {
            workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);
            bool isEdit = false;
            if (workFlowInstance == null)
            {
                workFlowInstance = new WorkFlowInstance();
                workFlowInstance.InstanceId = Guid.NewGuid().ToString();
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.AppId = "1001";
                workFlowInstance.CreateDeptCode = CurrentEmployee.DepartCode;
                workFlowInstance.CreateDeptName = CurrentEmployee.DepartName;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.FormTitle = tbTitle.Text;
            }
            else
            {
                isEdit = true;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.FormTitle = tbTitle.Text;
            }
            workFlowInstance.FormId = FormId;
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
            FlowRelated1.ProcId = workFlowInstance.InstanceId;
            Countersign1.ProcId = workFlowInstance.InstanceId;
            Countersign1.SaveData(true);//会签数据保存
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return result;
    }

    public void Alert(string msg)
    {
        DisplayMessage.ExecuteJs(string.Format("alert('{0}');", msg));
    }

    private void SaveWorkItem()
    {
        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(FormId);

        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
        {
             ApprovalID = Guid.NewGuid().ToString(),

           FormID = FormId,
            InstanceID = workFlowInstance.InstanceId,
            Opinion = "",
            ApproveAtTime = DateTime.Now,
            ApproveResult = "",//开始
            OpinionType = "",
            CurrentActiveName = "拟稿",
 ISSign = "0",

            DelegateUserName = "",
            DelegateUserCode = "",
            CreateAtTime = DateTime.Now,
            UpdateAtTime = DateTime.Now,
            FinishedTime = DateTime.Now,
            ApproveByUserCode = _BPMContext.CurrentPWordUser.EmployeeCode,
            ApproveByUserName = _BPMContext.CurrentPWordUser.EmployeeName
        };

        new BFApprovalRecord().AddApprovalRecord(appRecord);
    }

    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
    }

    void Alert(Page page, object message)
    {
        StringBuilder sb = new StringBuilder();
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message) + sb.ToString();
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }


}
