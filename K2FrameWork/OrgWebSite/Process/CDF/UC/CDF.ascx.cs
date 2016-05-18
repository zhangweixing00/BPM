using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using System.ComponentModel;
using System.Data;
using Model;
using System.Xml;
using K2.Controls;
using BLL;
using OrgWebSite.Process.CDF.WFChart;
using WS.K2;
using K2.Common;
using System.Text;

namespace OrgWebSite.Process.CDF.UC
{
    public partial class CDF : ProcessControl
    {

        #region 初始化页面操作者
        private CustomFlowBLL boardFormOP = new CustomFlowBLL();
        #endregion

        [Description("EmpolyeeID")]
        public String CurrentEmpolyeeID { get; set; }

        [Description("选择的流程ID")]
        public string ProcessID
        {
            get
            {
                return hfProcessID.Value;
            }
            set
            {
                hfProcessID.Value = value;
            }
        }

        [Description("IsForm")]
        public bool IsForm
        {
            get
            {
                if (hfIsForm.Value.Equals("FlowChart"))
                    return false;
                else
                    return true;
            }
        }

        [Description("是否显示会签人")]
        public string IsSign
        {
            set
            {
                hfSign.Value = value;
            }
            get
            {
                return hfSign.Value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbl_formid.Attributes["style"] = "margin-left:0px;";
                lbl_date.Attributes["style"] = "margin-left:0px;";
            }
        }

        #region Base64
        public string ToBase64(string xml)
        {
            return StringHelper.EncodingForString(xml, System.Text.Encoding.UTF8);
        }
        #endregion

        #region LoadData
        public override void LoadData()
        {
            SetPage();
            if (TaskPage.IsStartPage)
            {
                EnabledModify = true;
                LoadEmployeeInfo(TaskPage.EmployeeID);    //加载发起人信息
                LoadDepartmentInfo(TaskPage.EmployeeID);  //加载部门信息
                LoadProcessInfo();                          //加载流程信息

                //LoadBusinessCategoryDoc();          //加载业务大类小类文档
                //this.Upload1.EmployeeCode = CurrentEmpolyeeCode;

                //if (BSCategory.SelectedItem != null)
                //    SetApproveRole(BSCategory.SelectedItem.Value);  //设置角色固定节点
            }
            else
            {
                if (!string.IsNullOrEmpty(TaskPage.FormID))
                {
                    if (TaskPage.IsReWorkPage || TaskPage.IsDraftPage)
                        EnabledModify = true;           //设置可写
                    if (TaskPage.IsApprove || TaskPage.IsDraftPage || TaskPage.IsReWorkPage || TaskPage.IsViewProcessPage || TaskPage.IsCDFConfirm)
                        LoadProcessData();
                    if (TaskPage.IsCDFConfirm)
                        SetPageReadOnly("liFlowDirect");
                    if (TaskPage.IsViewProcessPage)
                    {
                        //Upload1.IsView = true;
                    }

                    //if (BSCategory.SelectedItem != null)
                    //    SetApproveRole(BSCategory.SelectedItem.Value);  //设置角色固定节点
                }
            }
        }
        #endregion

        #region 保存业务数据
        public override bool SaveData(ProcessAction action)
        {
            switch (action)
            {
                case ProcessAction.SaveDraft:
                case ProcessAction.SubmitCF:
                    return InsertData(action);
                case ProcessAction.Submit:
                case ProcessAction.Save:
                case ProcessAction.SubmitDraft:
                case ProcessAction.Rework:
                case ProcessAction.Reject:
                case ProcessAction.Approve:
                case ProcessAction.Confirm:
                case ProcessAction.ApproveSave:
                    return UpdateData(action);
                default:
                    return false;
            }
        }
        #endregion

        #region 取得DataFiedls
        public override void GetDataFields()
        {
            
        }
        #endregion

        //#region 取得XmlFiedls
        //public override void GetXmlFields()
        //{
        //    StringBuilder sb = new StringBuilder("<XmlDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
        //    foreach (ListItem li in cblSigners.Items)
        //    {
        //        if (li.Selected)
        //        {
        //            sb.AppendFormat("<CounterSign>{0}</CounterSign>", li.Value);
        //        }
        //    }
        //    sb.Append("</XmlDocument>");
        //    TaskPage.XmlFields["IPCDest"] = sb.ToString();
        //}
        //#endregion

        #region 更新DataFields
        public override void UpdateDataFields()
        {
            //TaskPage.DataFields["FormID"] = TaskPage.FormID;
            //TaskPage.DataFields["ApprovalType"] = "Main";
            //if (!TaskPage.IsReWorkPage)
            //TaskPage.DataFields["ApprovalXML"] = new CustomizeFlowChart().FlowChartJsonToXml_Approve(this.hfjqFlowChart.Value, TaskPage.ADAccount);
            //else
            //    TaskPage.DataFields["ApprovalXML"] = new CustomizeFlowChart().FlowChartJsonToXml_ReSubmit(this.hfjqFlowChart.Value, TaskPage.ADAccount);
        }
        #endregion

        #region 生成提交XML
        /// <summary>
        /// 创建提交XML
        /// </summary>
        /// <returns></returns>
        private string CreateRequestXml()
        {
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xmlDeclare = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);

            //跟节点
            XmlElement elementRoot = xDoc.CreateElement("Request");
            xDoc.AppendChild(elementRoot);

            //子节点
            XmlElement requestorNode = xDoc.CreateElement("Requestor");
            requestorNode.InnerText = TaskPage.EmployeeID;
            elementRoot.AppendChild(requestorNode);

            XmlElement requestorADNode = xDoc.CreateElement("RequestorAD");
            requestorADNode.InnerText = TaskPage.ADAccount;
            elementRoot.AppendChild(requestorADNode);

            XmlElement deptNode = xDoc.CreateElement("Department");
            deptNode.InnerText = ddlDepartment.SelectedValue;
            elementRoot.AppendChild(deptNode);

            XmlElement deptNodeName = xDoc.CreateElement("DepartmentName");
            deptNodeName.InnerText = ddlDepartment.SelectedItem.Text;
            elementRoot.AppendChild(deptNodeName);

            XmlElement wsNode = xDoc.CreateElement("WorkSpace");
            wsNode.InnerText = TaskPage.WorkSpace;
            elementRoot.AppendChild(wsNode);

            XmlElement requestEmail = xDoc.CreateElement("RequestorEmail");
            requestEmail.InnerText = TaskPage.Email;
            elementRoot.AppendChild(requestEmail);

            XmlElement requestTel = xDoc.CreateElement("RequestorTel");
            requestTel.InnerText = TaskPage.Tel;
            elementRoot.AppendChild(requestTel);

            XmlElement operatorNode = xDoc.CreateElement("Operator");
            operatorNode.InnerText = TaskPage.EmployeeID;
            elementRoot.AppendChild(operatorNode);

            XmlElement operatorADNode = xDoc.CreateElement("OperatorAD");
            operatorADNode.InnerText = TaskPage.ADAccount;
            elementRoot.AppendChild(operatorADNode);

            XmlElement operatorEmail = xDoc.CreateElement("OperatorEmail");
            operatorEmail.InnerText = TaskPage.Email;
            elementRoot.AppendChild(operatorEmail);

            XmlElement operatorTel = xDoc.CreateElement("OperatorTel");
            operatorTel.InnerText = TaskPage.Tel;
            elementRoot.AppendChild(operatorTel);

            return xDoc.InnerXml;
        }
        #endregion

        #region insert data 添加信息
        /// <summary>
        /// 插入表单数据到DB
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool InsertData(ProcessAction action)
        {
            bool result = false;
            try
            {
                this.FormId.Value = this.lbl_formid.Text = TaskPage.FormID = FormID.SerialsNO("CDF");
                TaskPage.ProcessID = ddlProcessType.SelectedValue;
                this.State.Value = "1";
                CustomFlow cf = CreateCustomFlowObject();
                //string approveNodeXml = K2.Common.K2Rule.GetApproveNodeXML(ddlProcessType.SelectedValue, "Reimbursement", CreateRequestXml()); //提交申请
                //string approveXml = K2.Common.K2Rule.GetApproveXML(approveNodeXml, CreateRequestXml());   //取得最终的xml

                cf.jqFlowChart = "";

                if (action == ProcessAction.SaveDraft || action == ProcessAction.Save)
                {
                    //cf.jqFlowChart = cfc.FlowChartJsonToXml_Approve(this.hfjqFlowChart.Value, hfEmployeeCode.Value);
                    cf.ProcessState = K2Utility.ProcessStatus.Draft.ToString();
                }
                else if (action == ProcessAction.SubmitCF || action == ProcessAction.SubmitDraft)
                    cf.ProcessState = K2Utility.ProcessStatus.Running.ToString();

                CustomFlowBLL bll = new CustomFlowBLL();
                if (bll.AddCustomFlow(cf))
                {
                    if (action == ProcessAction.SubmitCF)
                        bll.UpdateAttachStatusByAttachAttachCodes(cf.AttachIds);
                    result = true;
                }
                else
                    result = false;
                return result;
            }
            catch (Exception ex)
            {
                LogUtil.Log.Error(ex.Message);
                return false;       //异常和错误信息写入日志表中
            }
        }

        #endregion

        #region 创建业务对象
        private CustomFlow CreateCustomFlowObject()
        {
            CustomFlow cf = new CustomFlow();
            cf.Applicant = TaskPage.ADAccount;
            cf.AppReason = Server.HtmlEncode(AppReason.Text);
            cf.AppExplain = "";
            cf.CreatedBy = TaskPage.ADAccount;
            cf.FormId = FormId.Value;
            cf.Priority = Priority.SelectedValue;
            cf.ProcessId = ddlProcessType.SelectedValue;
            cf.State = int.Parse(this.State.Value);
            cf.Urgent = Urgent.SelectedValue;
            cf.AttachIds = string.Empty;
            cf.SubmitId = TaskPage.EmployeeID;
            cf.Operator = TaskPage.ADAccount;
            //通知方式
            cf.IsEmail = cbIsEmail.Checked;
            cf.IsSMS = cbIsSMS.Checked;
            cf.DeptCode = ddlDepartment.SelectedValue;
            return cf;
        }
        #endregion

        #region Update Data 更新信息
        /// <summary>
        /// 更新表单数据
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected bool UpdateData(ProcessAction action)
        {
            try
            {
                //Biz_CustomFlow bcf = new Biz_CustomFlow();
                CustomFlowBLL bll = new CustomFlowBLL();
                CustomFlow cf = CreateCustomFlowObject();
                if (action == ProcessAction.SaveDraft || action == ProcessAction.Save)
                {
                    CustomizeFlowChart cfc = new CustomizeFlowChart();
                    cf.jqFlowChart = cfc.FlowChartJsonToXml_Approve(this.hfjqFlowChart.Value, TaskPage.ADAccount);
                    cf.ProcessState = K2Utility.ProcessStatus.Draft.ToString();
                    bll.UpdateCustomFlowByFormId(cf);
                }
                if (action == ProcessAction.Approve || action == ProcessAction.Rework || action == ProcessAction.ApproveSave || action == ProcessAction.SubmitDraft)
                {
                    cf.ProcessState = K2Utility.ProcessStatus.Running.ToString();
                    if (action == ProcessAction.Rework)
                    {
                        CustomizeFlowChart cfc = new CustomizeFlowChart();
                        cf.jqFlowChart = cfc.FlowChartJsonToXml_ReSubmit(this.hfjqFlowChart.Value, TaskPage.ADAccount);
                        bll.UpdateCustomFlowByFormId(cf);
                    }
                    bll.UpdateCustomFlowStatusByAttachIds(cf.AttachIds, TaskPage.FormID);

                    //如果是审批，则还需要修改附件表的状态
                    if (action == ProcessAction.Approve || action == ProcessAction.Rework || action == ProcessAction.SubmitDraft)
                    {
                        bll.UpdateAttachStatusByAttachAttachCodes(cf.AttachIds);
                    }

                    if (action == ProcessAction.Approve && IsEnd)
                    {
                        bll.UpdateCostomFlowStatusByFormId(TaskPage.FormID, K2Utility.ProcessStatus.Finished.ToString());
                    }
                }
                if (action == ProcessAction.Reject)
                {
                    cf.ProcessState = K2Utility.ProcessStatus.Rejected.ToString();
                    //bcf.UpdateCustomFlowStatusByAttachIds(cf.AttachIds, TaskPage.FormID);
                    bll.UpdateCostomFlowStatusByFormId(TaskPage.FormID, cf.ProcessState);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private bool IsEnd
        {
            //get
            //{
            //    return false;
            //}
            get
            {
                bool returnVale = false;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(new CustomizeFlowChart().FlowChartJsonToXml_Approve(this.hfjqFlowChart.Value, TaskPage.ADAccount));
                bool mainNext = false;
                XmlNode xnCur = xmlDoc.SelectSingleNode("Approvals/Approval[@state='current']");
                if (xnCur != null)
                {
                    string meetState = xnCur.Attributes["meetState"].Value;
                    XmlElement xeCur = (XmlElement)xnCur;
                    if (meetState.ToUpper() == "N")
                    {
                        mainNext = true;
                    }
                }
                else
                {
                    mainNext = true;
                }

                if (mainNext)
                {
                    XmlNode xnWait = xmlDoc.SelectSingleNode("Approvals/Approval[@state='wait']");
                    if (xnWait == null)
                    {
                        returnVale = true;
                    }
                }

                return returnVale;
            }
        }
        #endregion

        #region 取得当前登陆人信息
        /// <summary>
        /// LoadEmployeeInfo 根据员工ID获取员工信息
        /// </summary>
        private void LoadEmployeeInfo(string Code)
        {
            UserProfileBLL bll = new UserProfileBLL();
            UserProfileInfo info = bll.GetUserProfile(Code);
            SubmitName.Value = info.CHName;
            SubmitID.Value = info.ID.ToString();
            ApplicantName.Value = SubmitName.Value;

            lbl_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion

        #region 加载部门信息
        private void LoadDepartmentInfo(string code)
        {
            UserProfileBLL bll = new UserProfileBLL();
            List<UserDepartmentInfo> infoList = bll.GetDepartmentByUserID(code);
            ddlDepartment.Items.Clear();
            if (infoList != null)
            {
                foreach (UserDepartmentInfo info in infoList)
                {
                    ListItem li = new ListItem();
                    li.Value = info.DeptCode.ToString();
                    li.Text = info.DeptName + "(" + info.OrgName + ")";
                    if (info.IsMain)
                        li.Selected = true;
                    ddlDepartment.Items.Add(li);
                }
            }
        }
        #endregion

        #region 加载流程信息
        private void LoadProcessInfo()
        {
            ProcessTypeBLL bll = new ProcessTypeBLL();
            IList<ProcessTypeInfo> ptiList = bll.GetProcessType();
            ddlProcessType.Items.Clear();
            if (ptiList != null)
            {
                foreach (ProcessTypeInfo info in ptiList)
                {
                    ListItem li = new ListItem();
                    li.Value = info.ID.ToString();
                    li.Text = info.ProcessType;
                    ddlProcessType.Items.Add(li);
                }
                ProcessID = ddlProcessType.SelectedValue;
            }
        }
        #endregion

        #region load process data
        /// <summary>
        /// LoadProcessData 加载信息
        /// </summary>
        protected void LoadProcessData()
        {
            //加载业务信息
            LoadCustomFlowInfo(TaskPage.FormID);
        }

        #endregion

        #region Load 业务表信息

        private void LoadCustomFlowInfo(string FormId)
        {
            //DataTable dt = boardFormOP.GetCustomFlowByFormId(FormId);
            CustomFlow cf = boardFormOP.GetCustomFlowByFormId(FormId);
            if (cf != null)
            {
                //基本信息
                this.lbl_formid.Text = FormId;
                this.lbl_date.Text = cf.SubmitDate.Value.ToString("yyyy-MM-dd");

                //申请信息
                ApplicantName.Value = cf.EmployeeName;
                //EmployeeCode.Text = cf.SubmitId;
                //Tel.Value = cf.Tel;
                //Email.Value = cf.Email;

                State.Value = cf.State.Value.ToString();
                this.FormId.Value = cf.FormId;
                CreatedBy.Value = cf.CreatedBy;
                AppReason.Text = Server.HtmlDecode(cf.AppReason);

                LoadProcessInfo();  //加载流程信息
                LoadDepartmentInfo(cf.SubmitId);    //加载部门信息
                foreach (ListItem li in ddlProcessType.Items)
                {
                    if (li.Value.Equals(cf.ProcessId, StringComparison.OrdinalIgnoreCase))
                    {
                        lbProcessType.Value = li.Text;
                        li.Selected = true;
                        break;
                    }
                }
                foreach (ListItem li in ddlDepartment.Items)
                {
                    if (li.Value.Equals(cf.DeptCode, StringComparison.OrdinalIgnoreCase))
                    {
                        lbDepartment.Value = li.Text;
                        li.Selected = true;
                        break;
                    }
                }

                /*暂时屏蔽---------------*/
                //CustomizeFlowChart cfc = new CustomizeFlowChart();
                //if (TaskPage.IsViewProcessPage)
                //{
                //    TaskPage.ApproveXML = boardFormOP.GetApproveXMLByProcInsID(Request.QueryString["ProcInstID"]);     //取得InstanceID
                //}

                //if (TaskPage.IsDraftPage)
                //    hfjqFlowChart.Value = cfc.FlowChartXmlToJson(cf.ApproveXML, false);
                //else if (TaskPage.IsReWorkPage)
                //    hfjqFlowChart.Value = cfc.FlowChartXmlToJson_ReSubmit(cf.ApproveXML, false);
                //else
                //    hfjqFlowChart.Value = cfc.FlowChartXmlToJson(TaskPage.ApproveXML, false);
            }
        }

        #endregion

        #region public void SetPageReadOnly(string divId)
        /// <summary>
        /// 设置页面Div为只读
        /// </summary>
        /// <param name="divId">要设为只读的DivId</param>
        public void SetPageReadOnly(string divId)
        {
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "debugger;$(document).ready(function(){document.getElementById('" + divId + "').style='display:none;';});", true);
            //liFlowDirect.Attributes["style"] = "display:none;";     //隐藏流程指引
        }
        #endregion

        #region 设置页面
        private void SetPage()
        {
            if (TaskPage.IsStartPage || TaskPage.IsReWorkPage || TaskPage.IsDraftPage)
            {
                this.Priority.Attributes["style"] = "";
                this.Urgent.Attributes["style"] = "";
                this.ddlDepartment.Attributes["style"] = "";
                this.ddlProcessType.Attributes["style"] = "";
                this.lbDepartment.Attributes["style"] = "display:none;";
                this.lbProcessType.Attributes["style"] = "display:none;";
                this.lbPriority.Attributes["style"] = "display:none;";
                this.lbUrgent.Attributes["style"] = "display:none;";
                trGvFiles.Attributes["style"] = "";
                //ibtnApplicant.Attributes["style"] = "";
                flowDoc.Attributes["style"] = "";
                cbIsEmail.Enabled = true;
                cbIsSMS.Enabled = true;
            }
            else if (TaskPage.IsApprove || TaskPage.IsCDFConfirm)
            {
                this.Priority.Attributes["style"] = "display:none;";
                this.Urgent.Attributes["style"] = "display:none;";
                this.ddlDepartment.Attributes["style"] = "display : none;";
                this.ddlProcessType.Attributes["style"] = "display:none;";
                this.lbDepartment.Attributes["style"] = "";
                this.lbProcessType.Attributes["style"] = "";
                this.lbPriority.Attributes["style"] = "";
                this.lbUrgent.Attributes["style"] = "";
                //trGvFiles.Attributes["style"] = "display:none;";
                //ibtnApplicant.Attributes["style"] = "display:none;";
                flowDoc.Attributes["style"] = "display:none;";
                cbIsEmail.Enabled = false;
                cbIsSMS.Enabled = false;

                //修改class
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$(document).ready(function(){$('.conter_right_list_input_bg').removeClass('conter_right_list_input_bg').addClass('conter_right_list_input_bg_border'); $('#reason_read').css('display','none');});", true);
            }
            else if (TaskPage.IsViewProcessPage)
            {
                this.Priority.Attributes["style"] = "display:none;";
                this.Urgent.Attributes["style"] = "display:none;";
                this.ddlDepartment.Attributes["style"] = "display:none;";
                this.ddlProcessType.Attributes["style"] = "display:none;";
                this.lbDepartment.Attributes["style"] = "";
                this.lbProcessType.Attributes["style"] = "";
                this.lbPriority.Attributes["style"] = "";
                this.lbUrgent.Attributes["style"] = "";
                trGvFiles.Attributes["style"] = "display:none;";
                //ibtnApplicant.Attributes["style"] = "display:none;";
                flowDoc.Attributes["style"] = "display:none;";
                cbIsEmail.Enabled = false;
                cbIsSMS.Enabled = false;

                //修改class
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$(document).ready(function(){$('.conter_right_list_input_bg').removeClass('conter_right_list_input_bg').addClass('conter_right_list_input_bg_border'); $('#reason_read').css('display','none');});", true);
            }
            //if (TaskPage.IsCDFConfirm)
            //    Upload1.IsConfirm = true;

            //保存SN
            if (!string.IsNullOrEmpty(Request.QueryString["SN"]))
                hfProcessSN.Value = Request.QueryString["SN"];

            if (hfSign.Value.Equals("true"))
            {
                ulsign.Attributes["style"] = "width: 600px;";
            }
            else
            {
                ulsign.Attributes["style"] = "display:none;";
            }
        }
        #endregion

        protected void ddlProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessID = ddlProcessType.SelectedValue;
        }

        public override string GetApproveXml()
        {
            WS.K2.K2WS ws = new K2WS();
            string approveXml = ws.GetStartProcessXML(Page.User.Identity.Name, Page.User.Identity.Name, "WF.K2\\SCF", "P_PekFix2", "<Keys><Key OrderNo=\"10\">部门成员</Key></Keys>", "<Data><Field OrderNo=\"10\" Code=\"CarFee\" Value=\"200\" /></Data>", "PR");
            return approveXml;
        }

        public override void GetXmlFields()
        {
            StringBuilder sb = new StringBuilder("<XmlDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            foreach (ListItem li in cblSigners.Items)
            {
                if (li.Selected)
                {
                    sb.AppendFormat("<CounterSign>{0}</CounterSign>", li.Value);
                }
            }
            sb.Append("</XmlDocument>");
            TaskPage.XmlFields["IPCDest"] = sb.ToString();
        }
    }
}