using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Model;
using System.Reflection;

namespace K2Utility
{
    #region delegate process action handler
    public delegate void ProcessActionHandler(ProcessAction action, EventArgs e);
    #endregion

    public class ProcessPage : BasePage
    {

        #region process action event
        public event ProcessActionHandler ProcessActionEvent;
        #endregion

        #region page properties

        #region workflow id
        private int _workflowID;
        /// <summary>
        /// workflow id
        /// </summary>
        public int WorkflowID
        {
            get { return _workflowID; }
            set { _workflowID = value; }
        }
        #endregion

        #region FlowName
        public string FlowName
        {
            get
            {
                //此处加入缓存
                //return Sohu.OA.Biz.DataBase.GetWorkFlow(WorkflowID);
                return "OA\\CustomFlow";
            }

        }
        #endregion

        #region process id
        /// <summary>
        /// process id
        /// </summary>
        public string ProcessID
        {
            get { return ViewState["ProcessID"] != null ? ViewState["ProcessID"].ToString() : string.Empty; }
            set { ViewState["ProcessID"] = value; }
        }
        #endregion

        #region ApproveXML
        /// <summary>
        /// 审批XML
        /// </summary>
        public string ApproveXML
        {
            get { return ViewState["ApproveXML"] != null ? ViewState["ApproveXML"].ToString() : string.Empty; }
            set { ViewState["ApproveXML"] = value; }
        }

        #endregion

        #region Form id
        /// <summary>
        /// claim id 修改成 form id
        /// </summary>
        public string FormID
        {
            get { return ViewState["FormID"] != null ? (String)ViewState["FormID"] : string.Empty; }
            set { ViewState["FormID"] = value; }
        }
        #endregion

        #region activity name
        /// <summary>
        /// activity name
        /// </summary>
        public string ActivityName
        {
            get { return ViewState["ActivityName"] != null ? (String)ViewState["ActivityName"] : string.Empty; }
            set { ViewState["ActivityName"] = value; }
        }
        #endregion

        #region process serial number
        /// <summary>
        /// process serial number
        /// </summary>
        public string ProcessSN
        {
            get { return ViewState["ProcessSN"] != null ? (String)ViewState["ProcessSN"] : string.Empty; }
            set { ViewState["ProcessSN"] = value; }
        }
        #endregion

        #region activityXml
        public string ActivityXml
        {
            get { return ViewState["ActivityXml"] != null ? (String)ViewState["ActivityXml"] : string.Empty; }
            set { ViewState["ActivityXml"] = value; }
        }
        #endregion

        #region is start process page
        private bool _isStartPage = false;
        /// <summary>
        /// is start process page
        /// </summary>
        public bool IsStartPage
        {
            get { return _isStartPage; }
            set { _isStartPage = value; }
        }
        #endregion

        #region is Rework process page
        private bool _isRework = false;
        /// <summary>
        /// is start process page
        /// </summary>
        public bool IsReWorkPage
        {
            get { return _isRework; }
            set { _isRework = value; }
        }
        #endregion

        #region is Draft process page
        private bool _isDraft = false;
        /// <summary>
        /// is start process page
        /// </summary>
        public bool IsDraftPage
        {
            get { return _isDraft; }
            set { _isDraft = value; }
        }
        #endregion

        #region is Edit process page
        private bool _isModifyPage = false;
        /// <summary>
        /// is Edit process page
        /// </summary>
        public bool IsModifyPage
        {
            get { return _isModifyPage; }
            set { _isModifyPage = value; }
        }
        #endregion

        #region is view process page

        private bool _isViewProcessPage = false;
        /// <summary>
        /// is view process page
        /// </summary>
        public bool IsViewProcessPage
        {
            get { return _isViewProcessPage; }
            set { _isViewProcessPage = value; }
        }

        #endregion

        #region 是否审批页面

        public bool IsApprove
        {
            get;
            set;
        }

        #endregion

        #region is HRBP Submit page
        private bool _isHRSubmitPage = false;
        /// <summary>
        /// is HR Submit  process page
        /// </summary>
        public bool IsHRSubmitPage
        {
            get { return _isHRSubmitPage; }
            set { _isHRSubmitPage = value; }
        }
        #endregion

        #region is OnBoard Staff Submit page
        private bool _isOnBoarSubmitPage = false;
        /// <summary>
        /// is HR Submit  process page
        /// </summary>
        public bool IsOnBoardSubmitPage
        {
            get { return _isOnBoarSubmitPage; }
            set { _isOnBoarSubmitPage = value; }
        }
        #endregion

        #region is ES process page
        private bool _isESConfirm = false;
        /// <summary>
        /// is es process page
        /// </summary>
        public bool IsESConfirm
        {
            get { return _isESConfirm; }
            set { _isESConfirm = value; }
        }
        #endregion

        #region is ITApprove process page
        private bool _isITConfirm = false;
        /// <summary>
        /// is it confirm process page
        /// </summary>
        public bool IsITConfirm
        {
            get { return _isITConfirm; }
            set { _isITConfirm = value; }
        }
        #endregion

        #region is HR C&B process page
        private bool _isHRCB = false;
        /// <summary>
        /// is it confirm process page
        /// </summary>
        public bool IsHRCB
        {
            get { return _isHRCB; }
            set { _isHRCB = value; }
        }
        #endregion

        #region  ExtensionAssign
        private bool _isExtensionAssign = false;
        /// <summary>
        /// is it confirm process page
        /// </summary>
        public bool IsExtensionAssign
        {
            get { return _isExtensionAssign; }
            set { _isExtensionAssign = value; }
        }
        #endregion

        #region  PhoneConfirm
        private bool _isPhoneConfirm = false;
        /// <summary>
        /// is it confirm process page
        /// </summary>
        public bool IsPhoneConfirm
        {
            get { return _isPhoneConfirm; }
            set { _isPhoneConfirm = value; }
        }
        #endregion

        #region  CashierForm
        private bool _isCashierForm = false;
        /// <summary>
        /// is it confirm process page
        /// </summary>
        public bool IsCashierForm
        {
            get { return _isCashierForm; }
            set { _isCashierForm = value; }
        }

        #endregion



        #region is CDF Confirm process page
        private bool _isCDFConfirm = false;
        public bool IsCDFConfirm
        {
            get { return _isCDFConfirm; }
            set { _isCDFConfirm = value; }
        }
        #endregion

        #region approval comments
        /// <summary>
        /// approval comments 
        /// </summary>
        public string Comments
        {
            get { return ViewState["Comments"] != null ? (String)ViewState["Comments"] : string.Empty; }
            set { ViewState["Comments"] = value; }
        }

        #endregion


        #region process main user control
        private ProcessControl _procControl;
        /// <summary>
        /// process main user control
        /// </summary>
        public ProcessControl ProcControl
        {
            get { return _procControl; }
            set { _procControl = value; }
        }
        #endregion
        //public string RejectType
        //{
        //    get { return ViewState["RejectType"] != null ? (String)ViewState["RejectType"] : "0"; }
        //    set { ViewState["RejectType"] = value; }
        //}


        #region 离职页面定义
        //王凤龙添加
        #region is Edit leaveFrom page
        private bool _isHrcbPage = false;
        /// <summary>
        /// is Edit process page
        /// </summary>
        public bool IsHrcbPage
        {
            get { return _isHrcbPage; }
            set { _isHrcbPage = value; }
        }
        #endregion
        #endregion


        #region process data fields
        private NameValueCollection _dataFields;
        /// <summary>
        /// process data fields
        /// </summary>
        public NameValueCollection DataFields
        {
            get
            {
                if (_dataFields == null)
                    _dataFields = new NameValueCollection();
                return _dataFields;
            }
        }
        #endregion

        #region process xml fields
        private NameValueCollection _xmlFields;
        /// <summary>
        /// process xml fields
        /// </summary>
        public NameValueCollection XmlFields
        {
            get
            {
                if (_xmlFields == null)
                    _xmlFields = new NameValueCollection();
                return _xmlFields;
            }
        }
        #endregion



        #endregion

        #region base methods

        #region on load
        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                #region approval page

                if (!string.IsNullOrEmpty(Request.QueryString["SN"]))
                {
                    ProcessSN = Request.QueryString["SN"];
                    string formID = string.Empty;
                    string activityName = string.Empty;
                    string processID = string.Empty;
                    string approveXML = string.Empty;       //取得审批XML
                    //FBA to AD
                    //WorkflowHelper.GetProcessInfo(MUID, Password, ProcessSN, ref processID, ref activityName);
                    //任务占用提示 edit by lee
                    try
                    {
                        //WorkflowHelper.GetProcessInfo(ProcessSN, ref formID, ref processID, ref activityName, ref approveXML);//,StaffNo);
                        WorkflowHelper.GetSCFProcessInfo(ProcessSN, ref formID, ref processID, ref activityName, ref approveXML);
                        FormID = formID;
                        //ProcessID = processID;
                        string FName = FormID.Substring(0, 3);
                        ActivityName = activityName;
                        ActivityXml = approveXML;
                    }
                    catch (Exception ex)
                    {
                        //is not allowed to open the worklist item with
                        if (ex.Message.Contains("26030"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "try{ymPrompt=top.ymPrompt}catch(e){} ymPrompt.alert({ message: '此任务已被他人处理或占用，您已无法处理！', title: '任务提示', handler:function() {window.location.href='/WorkSpace/MyWorklist.aspx ';}});", true);
                            return;
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "try{ymPrompt=top.ymPrompt}catch(e){} ymPrompt.alert({ message: '此任务已被他人处理或被占用！', title: '任务提示', handler:function() {window.location.href='/WorkSpace/MyWorklist.aspx ';}});", true);
                            return;
                        }
                    }

                    //if (ActivityPageName(FName) != GetPageName())
                    //    throw new Exception("00001 You have no permission to open this process.");
                }
                #endregion

                #region HR C&B
                if (!string.IsNullOrEmpty(Request.QueryString["FormID"]))
                {
                    FormID = Request.QueryString["FormID"];
                }
                #endregion


                #region draft page
                if (!string.IsNullOrEmpty(Request.QueryString["FormID"]) && IsDraftPage)
                    FormID = Request.QueryString["FormID"];
                #endregion
            }
            base.OnLoad(e);
        }
        #endregion

        #endregion

        #region
        /// <summary>
        /// 初始化ProcessLog操作者
        /// </summary>
        //private Biz.Biz_ProcessLog processlogOP = new Biz.Biz_ProcessLog();
        #endregion

        #region page methods

        #region save data
        public bool SaveData(ProcessAction action)
        {
            bool result = false;

            #region save form data
            if (ProcControl.EnabledModify || action == ProcessAction.Confirm || ProcControl.IsEnableEditPage)
            {
                if (action == ProcessAction.Reject || action == ProcessAction.Cancel)
                    result = true;
                else if (ProcControl.SaveData(action))
                {
                    if (action == ProcessAction.Rework)
                    {
                        IsReWorkPage = true;
                    }
                    if (action == ProcessAction.Rework)
                    {
                        ProcControl.UpdateDataFields();
                        //FBA to AD
                        //result = WorkflowHelper.UpdateDataFields(MUID, Password, ProcessSN, DataFields);
                        result = WorkflowHelper.UpdateDataFields(ProcessSN, DataFields);
                    }
                    else
                        result = true;
                }
                else
                    result = false;
            }
            else if (action == ProcessAction.Approve || action == ProcessAction.Reject || action == ProcessAction.ApproveSave || IsHRSubmitPage)
            {
                //姚骁然修改（因为当审批人选择拒绝时，所作修改均无效）
                if (WorkflowID != 3)
                {
                    ProcControl.SaveData(action);
                    if (IsHRSubmitPage)
                    {
                        ActivityName = "HR C&B";//Test by lee
                    }
                }
                result = true;
            }
            else if (IsOnBoardSubmitPage)
            {
                result = true;
            }
            else
            {
                ProcControl.UpdateDataFields();
                //FBA to AD
                //WorkflowHelper.UpdateDataFields(MUID, Password, ProcessSN, DataFields);
                WorkflowHelper.UpdateDataFields(ProcessSN, DataFields);
                result = true;
            }
            #endregion

            if (action == ProcessAction.SubmitHRBP || action == ProcessAction.SaveHRDraft || action == ProcessAction.SubmitHRBPDraft || action == ProcessAction.Reject || action == ProcessAction.Submit || action == ProcessAction.SubmitDraft || action == ProcessAction.Cancel || action == ProcessAction.Rework || action == ProcessAction.SaveDraft || action == ProcessAction.SubmitHRCB)
                UpdateProcessStatus(GetProcessStatus(action).ToString());
            //HR BP在员工录入时虽然没有启动流程，但是此时需要计入操作历史记录中
            if (action == ProcessAction.SubmitHRBP || action == ProcessAction.SubmitHRBPDraft)
                if (WorkflowID == 2 && IsStartPage == true)
                {
                    ProcessLog(ProcessAction.SubmitHRCB.ToString(), "HRBP");
                    ProcessLog(ProcessAction.SubmitHRBP.ToString(), "HRBP");
                }
                else
                    ProcessLog(ProcessAction.SubmitHRBP.ToString(), "HRBP");
            if (action == ProcessAction.SubmitHRCB)
                ProcessLog(ProcessAction.SubmitHRCB.ToString(), "HRBP");

            //判断是否是自定义流程
            if (WorkflowID == 3)
            {
                //自定义流程提交处理
                if (action == ProcessAction.SubmitCF)   //提交
                {
                    ProcControl.SaveData(action);
                    //UpdateProcessStatus(GetProcessStatus(action).ToString(), FormID, "CustomFlow");
                    ProcessLog(ProcessAction.SubmitCF.ToString(), "提交");
                }
                if (action == ProcessAction.Rework)     //重提交
                {
                    ProcControl.SaveData(action);
                    ProcessLog(ProcessAction.SubmitCF.ToString(), "重提交");
                }
                if ((action == ProcessAction.SaveDraft || action == ProcessAction.Save) && IsHRSubmitPage != true)  //保存
                {
                    ProcControl.SaveData(action);
                }
                if (action == ProcessAction.Approve)     //审批
                {
                    ProcControl.SaveData(action);
                    ProcControl.UpdateDataFields();
                    WorkflowHelper.UpdateDataFields(ProcessSN, DataFields);
                }
                if (action == ProcessAction.ApproveSave)    //审批保存
                {
                    ProcControl.SaveData(action);
                    //还要保存审批记录
                }
                if (action == ProcessAction.Reject)      //拒绝
                {

                }
                if (action == ProcessAction.SubmitDraft)     //从草稿箱提交
                {
                    ProcControl.SaveData(action);
                    ProcessLog(action.ToString(), "提交");
                }
                if (action == ProcessAction.StartCounter)
                {
                    ProcControl.SaveData(action);
                }
            }
            return result;
        }
        #endregion

        #region update process status
        /// <summary>
        /// update process status
        /// </summary>
        /// <param name="status">process status</param>
        /// <returns>update process status successfully or faild.</returns>
        public bool UpdateProcessStatus(string status)
        {
            //return Sohu.OA.Biz.DataBase.UpdateProcessStatus(status, FormID);
            return true;
        }

        public bool UpdateProcessStatus(string status, string formID)
        {
            //return Sohu.OA.Biz.DataBase.UpdateProcessStatus(status, formID);
            return true;
        }

        /// <summary>
        /// 更新状态（自定义流程）
        /// </summary>
        /// <param name="status"></param>
        /// <param name="formID"></param>
        /// <param name="tName">更新的表名</param>
        /// <returns></returns>
        public bool UpdateProcessStatus(string status, string formID, string tName)
        {
            //return Sohu.OA.Biz.DataBase.UpdateProcessStatus(status, formID, tName);
            return true;
        }

        #endregion

        #region get process status
        /// <summary>
        /// get process status
        /// </summary>
        /// <param name="action">process action</param>
        /// <returns>process action</returns>
        public ProcessStatus GetProcessStatus(ProcessAction action)
        {
            switch (action)
            {
                default:
                    return ProcessStatus.Running;

                case ProcessAction.SubmitHRBP:
                case ProcessAction.SubmitHRBPDraft:
                    if (WorkflowID == 2)
                    { return ProcessStatus.Running; }
                    else
                        return ProcessStatus.HRBPSubmit;

                case ProcessAction.SaveDraft:
                case ProcessAction.SaveHRDraft:
                    return ProcessStatus.Draft;
                case ProcessAction.SubmitHRCB:
                    return ProcessStatus.SubmitHRCB;

                case ProcessAction.Reject:
                    return ProcessStatus.Rejected;

                case ProcessAction.Cancel:
                    return ProcessStatus.Cancelled;
            }
        }
        #endregion

        #region get china status
        public static string GetChProcessStatus(string status)
        {
            switch (status)
            {
                default:
                    return "处理中";
                case "HRBPSubmit":
                    return "已提交";
                case "Finished":
                    return "已完成";
                case "Cancelled":
                    return "已取消";
                case "Rejected":
                    return "拒绝";
                case "Draft":
                    return "草稿";
                //王凤龙添加
                case "DraftHR":
                    return "草稿";
                case "SubmitHRCB":
                    return "已提交";
                //采购添加
                case "Waiting":
                    return "待收货";
                case "Receiving":
                    return "收货中";
                case "Received":
                    return "收货完成";
                //SA
                case "SARejected":
                    return "配置驳回";
                case "SACancelled":
                    return "审批驳回";
                case "SAFinished":
                    return "PR已同步";
                case "COFinished":
                    return "合同申请已同步";
                case "PRApproved":
                    return "PR已审核";
                case "COApproved":
                    return "合同已审核";
                case "Approved":
                    return "已审核";
                case "RCRejected":
                    return "收货驳回";
                case "CORejected":
                    return "合同驳回";
                case "COCanceled":
                    return "合同取消";
            }
        }
        #endregion

        #region 得到PO单列表的状态
        public static string GetprocessState(string state)
        {
            switch (state)
            {
                default:
                    return "";
                case "Draft":
                    return "草稿";
                case "Submit":
                    return "提交";
                case "Running":
                    return "处理中";
                //王凤龙添加
                case "Finished":
                    return "完成";
                case "Unvalid":
                    return "未生效";
            }
        }
        #endregion

        #region
        public static string GetActionString(string actionName)
        {
            switch (actionName)
            {
                default:
                    return "确认";
                case "Submit":
                    return "提交";
                case "SubmitHRBP":
                    return "HR BP提交";
                case "SubmitHRCB":
                    return "HR BP提交";
                case "HR C&B":
                    return "HR C&B提交";
                case "Approve":
                    return "审批";
                case "Reject":
                    return "拒绝";
                case "SubmitCF":
                    return "提交";
                case "StartCounter":
                    return "发起会签";
                case "Cancel":
                    return "取消";
                case "SubmitOriginator":
                    return "沟通发起人";
                case "GotoEND":
                    return "无异议";
                case "Accepted":
                    return "接受会签意见";
                case "NotAccepted":
                    return "不接受会签意见";
                case "Next":
                    return "校稿";
            }


        }
        #endregion

        #region image

        public static string GetStateDes(string status, int i)
        {
            string comment = "";
            if (i == 1)
            {
                if (status == "未分配")
                {
                    comment = "不分配";
                }
                else if (status == "未处理")
                {
                    comment = "未处理";
                }
                else if (status == "已完成")
                {
                    comment = "已完成";
                }
            }
            else if (i == 0)
            {
                if (status == "未分配")
                {
                    comment = "不分配";
                }
                else if (status == "未处理")
                {
                    comment = "不分配";
                }
                else if (status == "已完成")
                {
                    comment = "不分配";
                }

            }
            return comment;

        }

        public static string GetPhoneStateDes(int em, int i)
        {
            string comment = "";
            if (i == 1)
            {
                if (em == -1)
                {
                    comment = "不分配";
                }
                else if (em == 0)
                {
                    comment = "未处理";
                }
                else if (em == 1)
                {
                    comment = "已完成";
                }
            }
            else if (i == 0)
            {
                if (em == -1)
                {
                    comment = "不分配";
                }
                else if (em == 0)
                {
                    comment = "不分配";
                }
                else if (em == 1)
                {
                    comment = "不分配";
                }

            }
            return comment;

        }

        public static string GetImageUserUrl(int i)
        {

            switch (i)
            {
                default:
                    return "/pic/gridview/AccountRed.png";
                case 2:
                    return "";
                case 0:
                    return "/pic/gridview/AccountYellow.png";
                case 1:
                    return "/pic/gridview/AccountBlue.png";

            }
        }
        public static string GetImageDes(int i)
        {

            switch (i)
            {
                default:
                    return "未填写";
                case 2:
                    return "";
                case 0:
                    return "未填写";
                case 1:
                    return "已完成";

            }
        }

        public static string GetImageITUrl(string status, int i)
        {

            string url = "";
            if (i == 1)
            {
                if (status == "未分配")
                {
                    url = "/pic/gridview/ITRed.png";
                }
                else if (status == "未处理")
                {
                    url = "/pic/gridview/ITYellow.png";
                }
                else if (status == "已完成")
                {
                    url = "/pic/gridview/ITBlue.png";
                }
            }
            else if (i == 0)
            {
                if (status == "未分配")
                {
                    url = "/pic/gridview/ITRed.png";
                }
                else if (status == "未处理")
                {
                    url = "/pic/gridview/ITRed.png";
                }
                else if (status == "已完成")
                {
                    url = "/pic/gridview/ITRed.png";
                }

            }

            return url;
        }
        public static string GetImagePhoneUrl(int em, int i)
        {
            string url = "";
            if (i == 1)
            {
                if (em == -1)
                {
                    url = "/pic/gridview/PhoneRed.png";
                }
                else if (em == 0)
                {
                    url = "/pic/gridview/PhoneYellow.png";
                }
                else if (em == 1)
                {
                    url = "/pic/gridview/PhoneBlue.png";
                }
            }
            else if (i == 0)
            {
                if (em == -1)
                {
                    url = "/pic/gridview/PhoneRed.png";
                }
                else if (em == 0)
                {
                    url = "/pic/gridview/PhoneRed.png";
                }
                else if (em == 1)
                {
                    url = "/pic/gridview/PhoneRed.png";
                }

            }

            return url;
        }
        public static string GetImageEmailUrl(string status, int i)
        {
            string url = "";
            if (i == 1)
            {
                if (status == "未分配")
                {
                    url = "/pic/gridview/EmailRed.png";
                }
                else if (status == "未处理")
                {
                    url = "/pic/gridview/EmailYellow.png";
                }
                else if (status == "已完成")
                {
                    url = "/pic/gridview/EmailBlue.png";
                }
            }
            else if (i == 0)
            {
                if (status == "未分配")
                {
                    url = "/pic/gridview/EmailRed.png";
                }
                else if (status == "未处理")
                {
                    url = "/pic/gridview/EmailRed.png";
                }
                else if (status == "已完成")
                {
                    url = "/pic/gridview/EmailRed.png";
                }

            }

            return url;
        }
        public static string GetImageESUrl(string status, int i)
        {
            string url = "";
            if (i == 1)
            {
                if (status == "未分配")
                {
                    url = "/pic/gridview/ESRed.png";
                }
                else if (status == "未处理")
                {
                    url = "/pic/gridview/ESYellow.png";
                }
                else if (status == "已完成")
                {
                    url = "/pic/gridview/ESBlue.png";
                }
            }
            else if (i == 0)
            {
                if (status == "未分配")
                {
                    url = "/pic/gridview/ESRed.png";
                }
                else if (status == "未处理")
                {
                    url = "/pic/gridview/ESRed.png";
                }
                else if (status == "已完成")
                {
                    url = "/pic/gridview/ESRed.png";
                }

            }

            return url;
        }
        #endregion

        #region process methods

        #region start process
        /// <summary>
        /// start process
        /// </summary>
        /// <param name="action">process action</param>
        /// <returns></returns>
        public bool StartProcess(ProcessAction action)
        {
            if (SaveData(action))
            {
                ProcControl.GetDataFields();
                string fullName = "SohuBPMFlow\\OAF";//SqlHelper.ExecuteScalar(Database.MerckHRWorkFlow, "GetWorkflowFullName", new SqlParameter[] { new SqlParameter("@intWorkflowID", WorkflowID) }).ToString();

                int ProcInstID = -1;//added by lee

                //if (WorkflowHelper.StartProcess(MUID, Password, fullName, ClaimID, DataFields))//FBA to AD
                //if (WorkflowHelper.StartProcess(fullName, ClaimID, DataFields))
                if (WorkflowHelper.StartProcess(fullName, FormID, DataFields, ref ProcInstID))
                {
                    //流程启动成功，得到K2返回的 ProcessInstance ID，通过FormID来更改这个表单的ProcessID
                    UpdateProcInstID(ProcInstID, FormID);//edit by lee 2011-5-31
                    return ProcessLog(ProcessAction.Submit.ToString());
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool StartProcess(ProcessAction action, string flowName)
        {
            if (SaveData(action))
            {
                ProcControl.GetDataFields();
                string fullName = "";
                if (WorkflowID == 3)
                {
                    fullName = flowName;
                }
                else
                {
                    fullName = "SohuBPMFlow\\TAF";
                }//SqlHelper.ExecuteScalar(Database.MerckHRWorkFlow, "GetWorkflowFullName", new SqlParameter[] { new SqlParameter("@intWorkflowID", WorkflowID) }).ToString();

                int ProcInstID = -1;//added by lee


                if (WorkflowHelper.StartProcess(fullName, FormID, DataFields, ref ProcInstID))
                {
                    //流程启动成功，得到K2返回的 ProcessInstance ID，通过FormID来更改这个表单的ProcessID
                    if (WorkflowID != 3)
                    {
                        return UpdateProcInstID(ProcInstID, FormID, "TAF");//edit by lee 2011-5-31
                    }
                    else
                        return true;//edit by lee
                    // return ProcessLog(ProcessAction.Submit.ToString());
                }
                else
                    return false;

            }
            else
                return false;
        }


        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="action"></param>
        /// <param name="flowName"></param>
        /// <returns></returns>
        public bool StartProcess(ProcessAction action, string flowName, string requestor, ref int ProcInstID)
        {
            if (SaveData(action))
            {
                ProcControl.GetDataFields();
                string approveXml = ProcControl.GetApproveXml();
                string fullName = flowName;
                
                ProcInstID = -1;//added by lee

                //取得审批链Xml
                WS.K2.K2WS ws = new WS.K2.K2WS();
                string pid = ws.StartProcess(fullName, requestor, FormID, approveXml, string.Empty, string.Empty);
                if (StringHelper.isNumeric(pid))
                {
                    ProcInstID = Int32.Parse(pid);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// 发起流程(POC)
        /// </summary>
        /// <param name="action"></param>
        /// <param name="flowName"></param>
        /// <returns></returns>
        public bool StartProcess(ProcessAction action, string flowName, ref int ProcInstID)
        {
            if (SaveData(action))
            {
                string fullName = flowName;

                ProcInstID = -1;//added by lee

                ProcControl.GetDataFields();    //取得datafields
                ProcControl.GetXmlFields();     //取得xmlfields

                if (WorkflowHelper.StartProcess(fullName, FormID, DataFields, XmlFields, ref ProcInstID))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        #endregion

        #region 批量发起审批
        /// <summary>
        /// 通过BatchID找到所包含的所有表单的ID然后通过ID取出相关流程数据，启动流程实例
        /// </summary>
        /// <param name="action"></param>
        /// <param name="batchID"></param>
        /// <returns></returns>
        public bool BatchStartProcess(ProcessAction action)
        {
            bool result = false;




            return result;
        }


        #endregion

        #region approve process
        /// <summary>
        /// approve process
        /// </summary>
        /// <param name="action">approval action</param>
        /// <returns></returns>
        public bool ApproveProcess(ProcessAction action)
        {
            if (SaveData(action))
            {
                if (WorkflowHelper.ApproveProcess(ProcessSN, action.ToString()))
                {
                    ProcessLog(action.ToString());
                    #region refresh my work
                    //int count = 0;
                    //if (Request.Cookies["MyWork"] != null)
                    //{
                    //    HttpCookie cookie = Request.Cookies["MyWork"];
                    //    Response.Cookies.Remove("MyWork");
                    //    count = Convert.ToInt32(cookie.Values["Count"]) - 1;
                    //    //count = Convert.ToInt32(SqlHelper.ExecuteScalar(Database.K2Server, "GetMyWorkCount", new SqlParameter[] { new SqlParameter("@nvchrMUID", Page.User.Identity.Name) }));
                    //    cookie.Values["Count"] = count.ToString();
                    //    Response.Cookies.Add(cookie);
                    //}
                    //ClientScript.RegisterStartupScript(this.GetType(), "", "if(window.opener!=null)\nwindow.opener.ReGetMyWork();", true);
                    #endregion

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 通过接口进行审批
        /// </summary>
        /// <param name="action"></param>
        /// <param name="currentUser"></param>
        /// <param name="activityXML"></param>
        /// <returns></returns>
        public bool ApproveProcess(ProcessAction action, string activityXML, string currentUser)
        {
            if (SaveData(action))
            {
                if (WorkflowHelper.ApproveProcess(ProcessSN, action.ToString(), CurrentUser, activityXML))
                {
                    ProcessLog(action.ToString());
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        #endregion

        #region approve process
        /// <summary>
        /// approve process
        /// </summary>
        /// <param name="action">approval action</param>
        /// <returns></returns>
        public bool ApproveProcess(ProcessAction action, string activityName)
        {
            if (action == ProcessAction.StartCounter)
            {
                action = ProcessAction.Approve;
                if (SaveData(action))
                {
                    if (WorkflowHelper.ApproveProcess(ProcessSN, action.ToString()))
                    {
                        ProcessLog(ProcessAction.StartCounter.ToString(), activityName);
                        #region refresh my work
                        //int count = 0;
                        //if (Request.Cookies["MyWork"] != null)
                        //{
                        //    HttpCookie cookie = Request.Cookies["MyWork"];
                        //    Response.Cookies.Remove("MyWork");
                        //    count = Convert.ToInt32(cookie.Values["Count"]) - 1;
                        //    //count = Convert.ToInt32(SqlHelper.ExecuteScalar(Database.K2Server, "GetMyWorkCount", new SqlParameter[] { new SqlParameter("@nvchrMUID", Page.User.Identity.Name) }));
                        //    cookie.Values["Count"] = count.ToString();
                        //    Response.Cookies.Add(cookie);
                        //}
                        //ClientScript.RegisterStartupScript(this.GetType(), "", "if(window.opener!=null)\nwindow.opener.ReGetMyWork();", true);
                        #endregion

                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            return false;
        }
        #endregion

        public bool ApproveProcess(ProcessAction action, bool result, string activityName)
        {
            if (result)
            {
                if (WorkflowHelper.ApproveProcess(ProcessSN, action.ToString()))
                {
                    ProcessLog(action.ToString(), activityName);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        #region

        #endregion

        #endregion

        #region process log
        /// <summary>
        /// process log
        /// </summary>
        /// <param name="action">process action</param>
        /// <returns>success or not</returns>
        public bool ProcessLog(string action)
        {
            Assembly ass = Assembly.Load("BLL");
            Type type = ass.GetType("BLL.ProcessLogBLL");
            MethodInfo info = type.GetMethod("InsertProcessLog");
            Object obj = ass.CreateInstance("BLL.ProcessLogBLL");
            
            if (WorkflowID == 3)
            {
                if (this.ActivityName.Equals("申请人重新提交", StringComparison.OrdinalIgnoreCase) && action.Equals("Cancel"))
                {
                    //Entity.T_ProcessLog processlog1 = new Entity.T_ProcessLog() { ActivityName = "取消任务", Operation = action, ProcessLogId = Guid.NewGuid(), FormId = FormID, Operatetime = DateTime.Now, ApproverName = EmployeeName, ApproverCode = this.EmployeeCode, ApprovePosition = this.Post, Comments = this.Comments };
                    ProcessLogInfo processlog1 = new ProcessLogInfo();
                    processlog1.ActivityName = "取消任务";
                    processlog1.Operation = action;
                    processlog1.ProcessLogId = Guid.NewGuid();
                    processlog1.FormId = FormID;
                    processlog1.Operatetime = DateTime.Now;
                    processlog1.ApproverName = Employee.CHName;
                    processlog1.ApproverID = Employee.ID;
                    processlog1.ApprovePosition = this.Post;
                    processlog1.Comments = this.Comments;
                    
                    //return processlogOP.InsertIntoProcessLog(processlog1);//插入审批记录
                    info.Invoke(obj, new ProcessLogInfo[] { processlog1 });
                    return true;
                }
            }
            //Entity.T_ProcessLog processlog = new Entity.T_ProcessLog() { ActivityName = this.ActivityName, Operation = action, ProcessLogId = Guid.NewGuid(), FormId = FormID, Operatetime = DateTime.Now, ApproverName = EmployeeName, ApproverCode = this.EmployeeCode, ApprovePosition = this.Post, Comments = this.Comments };
            ProcessLogInfo processlog = new ProcessLogInfo();
            processlog.ActivityName = this.ActivityName;
            processlog.Operation = action;
            processlog.ProcessLogId = Guid.NewGuid();
            processlog.FormId = FormID;
            processlog.Operatetime = DateTime.Now;
            processlog.ApproverName = Employee.CHName;
            processlog.ApproverID = Employee.ID;
            processlog.ApprovePosition = this.Post;
            processlog.Comments = this.Comments;
            //return processlogOP.InsertIntoProcessLog(processlog);//插入审批记录
            //return true;
            info.Invoke(obj, new ProcessLogInfo[] { processlog });
            return true;
        }


        /// <summary>
        /// process log 
        /// </summary>
        /// <param name="action">process action</param>
        /// <param name="action">activityName</param>
        /// <returns>success or not</returns>
        public bool ProcessLog(string action, string activityName)
        {
            ProcessLogInfo processlog = new ProcessLogInfo();
            processlog.ActivityName = activityName;
            processlog.Operation = action;
            processlog.ProcessLogId = Guid.NewGuid();
            processlog.FormId = FormID;
            processlog.Operatetime = DateTime.Now;
            processlog.ApproverName = Employee.CHName;
            processlog.ApproverID = Employee.ID;
            processlog.ApprovePosition = this.Post;
            processlog.Comments = this.Comments;

            Assembly ass = Assembly.Load("BLL");
            Type type = ass.GetType("BLL.ProcessLogBLL");
            MethodInfo info = type.GetMethod("InsertProcessLog");
            Object obj = ass.CreateInstance("BLL.ProcessLogBLL");
            info.Invoke(obj, new ProcessLogInfo[] { processlog });
            return true;
        }

        public bool ProcessLog(string action, string activityName, string userName, string empCode, string userPosition)
        {
            //Entity.T_ProcessLog processlog = new Entity.T_ProcessLog() { ActivityName = activityName, Operation = action, ProcessLogId = Guid.NewGuid(), FormId = FormID, Operatetime = DateTime.Now, ApproverName = userName, ApproverCode = empCode, ApprovePosition = userPosition };
            //return processlogOP.InsertIntoProcessLog(processlog);//插入审批记录
            return true;
        }

        public bool ProcessLog(string action, string activityName, string userName, string empCode, string userPosition, string formID)
        {
            //Entity.T_ProcessLog processlog = new Entity.T_ProcessLog() { ActivityName = activityName, Operation = action, ProcessLogId = Guid.NewGuid(), FormId = formID, Operatetime = DateTime.Now, ApproverName = userName, ApproverCode = empCode, ApprovePosition = userPosition };
            //return processlogOP.InsertIntoProcessLog(processlog);//插入审批记录
            return true;
        }

        #region Update ProcInstID to DB

        /// <summary>
        /// Update ProcInstID to DB(ProcessFlow)
        /// </summary>
        /// <param name="ProcInstID"></param>
        /// <param name="ClaimID"></param>
        /// <returns></returns>
        public bool UpdateProcInstID(int ProcInstID, string FormID)
        {
            //return Sohu.OA.Biz.DataBase.UpdateProcInstID(ProcInstID, FormID, FlowName);
            return true;
        }

        public bool UpdateProcInstID(int ProcInstID, string FormID, string flowName)
        {
            //return Sohu.OA.Biz.DataBase.UpdateProcInstID(ProcInstID, FormID, flowName);
            return true;
        }
        #endregion

        #region do process action
        public void DoProcessAction(ProcessAction action, EventArgs e)
        {
            if (ProcessActionEvent != null)
                ProcessActionEvent(action, e);
        }
        #endregion

        #region get page name
        private string GetPageName()
        {
            string url = Request.Url.ToString();
            int startIndex = url.LastIndexOf("/") + 1;
            return url.Substring(startIndex, url.LastIndexOf(".aspx") - startIndex).ToLower().Trim();
        }
        private string ActivityPageName(string FName)
        {
            //return Sohu.OA.Biz.DataBase.ActivityPageName(FName, ActivityName);
            return string.Empty;
        }

        #endregion
        #endregion
        #endregion
    }
}
