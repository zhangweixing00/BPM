using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using System.Configuration;
using System.Data;
using BLL;
using System.ComponentModel;
using System.Reflection;

namespace OrgWebSite.Process.Controls
{
    public partial class UCProcessAction : WorkflowControl
    {
        #region control properties

        #region 暴露用户选择流程属性
        public string ProcessName
        {
            get
            {
                return hfProcessName.Value;
            }
            set
            {
                hfProcessName.Value = value;
            }
        }
        #endregion

        #region submit button visible
        public bool SubmitVisible
        {
            set
            {
                btnSubmit.Visible = value;
            }

        }

        #endregion

        #region btnSubmitHRBP button visible
        public bool SubmitHRBPVisible
        {
            set
            {
                btnSubmitHRBP.Visible = value;
            }

        }

        #endregion

        #region submit draft button visible
        public bool SubmitDraftVisible
        {
            set
            {
                btnSubmitDraft.Visible = value;
            }
        }
        #endregion


        #region submit hrbp draft button visible
        public bool SubmitHRBPDraftVisible
        {
            set
            {
                btnSubmitHRBPDraft.Visible = value;
            }
        }
        #endregion

        #region save button visible
        public bool SaveVisible
        {
            set
            {
                btnSave.Visible = value;
            }
        }
        #endregion

        #region close button visible
        public bool CloseVisible
        {
            set
            {
                btnClose.Visible = value;
            }
        }
        #endregion


        #region close hr button visible

        public bool CloseHR
        {
            set
            {
                btnCloseHR.Visible = value;
            }
        }
        #endregion

        public bool ReturnV
        {
            set
            {
                imagReturn.Visible = value;
            }
        }

        #region rework button visible
        public bool ReworkVisible
        {
            set
            {
                btnRework.Visible = value;
            }
        }
        #endregion

        #region Endorsement botton visibel

        public bool CDFEndorsementVisible
        {
            set
            {
                btnCDFEndorsement.Visible = value;
            }
        }

        #endregion

        #region CounterSign botton visibel

        public bool CDFCounterSignVisible
        {
            set
            {
                btnCDFCountersign.Visible = value;
            }
        }

        #endregion

        #region save draft button visible
        public bool SaveDraftVisible
        {
            set
            {
                btnSaveDraft.Visible = value;
            }
        }
        #endregion


        #region save draft HR button visible
        public bool SaveDraftHRVisible
        {
            set
            {
                btnSaveDraftHR.Visible = value;
            }
        }
        #endregion
        #region btnSubmitMore
        public bool SubmitMoreVisible
        {
            set
            {
                btnSubmitMore.Visible = value;
            }
        }
        #endregion

        #region approve button visible
        public bool ApproveVisible
        {
            set
            {
                btnApprove.Visible = value;
            }
        }
        #endregion

        #region approve save form
        public bool ApproveSaveVisible { set { btnApproveSave.Visible = value; } }
        #endregion
        #region
        public bool SubmitOnBoard
        {
            set
            {
                btnSubmitOnboard.Visible = value;
            }

        }
        #endregion

        #region reject button visible
        public bool RejectVisible
        {
            set
            {
                btnReject.Visible = value;
            }
        }
        #endregion

        #region reject button visible
        public bool RejectCFVisible
        {
            set
            {
                btnRejectCF.Visible = value;
            }
        }
        #endregion

        #region Confirm button visible
        public bool ConfirmVisible
        {
            set
            {


                btnConfirm.Visible = value;
            }
        }
        #endregion

        #region cancel button visible
        public bool CancelVisible
        {
            set
            {
                btnCancel.Visible = value;
            }
        }
        #endregion

        #region Previous Comments
        public string PreviousComments
        {
            get
            {
                if (ViewState["PreviousComments"] == null)
                {
                    ViewState["PreviousComments"] = "";
                }
                return ViewState["PreviousComments"].ToString();
            }
            set
            {
                ViewState["PreviousComments"] = value;
            }
        }
        #endregion

        #region comments panel visible
        public bool CommentsVisible
        {
            set
            {
                plComments.Visible = value;
            }
        }
        #endregion
        #region 审批意见
        public bool SPContentVisible
        {
            set
            {
                divComments.Visible = value;
            }
        }
        #endregion

        #region CustomFlow Submit Button
        public bool SubmitCFVisible
        {
            set
            {
                btnSubmitCF.Visible = true;
            }
        }
        public bool btnViewFlowVisible
        {
            set
            {
                btnViewFlow.Visible = true;
            }
        }

        public bool btnReViewFlowVisible
        {
            set
            {
                btnReViewFlow.Visible = value;
            }
        }

        public bool btnSubmitDraftCFVisible
        {
            set
            {
                btnSubmitDraftCF.Visible = true;
            }
        }
        public bool btnReworkCFCFVisible
        {
            set
            {
                btnReworkCF.Visible = true;
            }
        }

        public bool StartCounterVisible
        {
            set
            {
                btnStartCounter.Visible = value;
            }
            get
            {
                return btnStartCounter.Visible;
            }
        }
        #endregion

        #region abandon
        public bool Abandon
        {
            set
            {
                btnCDFAbandon.Visible = value;
            }
        }
        #endregion

        #region save to draft
        public bool SaveToDraft
        {
            set
            {
                btnCDFSaveToDraft.Visible = value;
            }
        }
        #endregion

        //王凤龙添加离职按钮
        #region SaveHrbp hr button visible
        //保存可查看
        public bool LeSaveHrbpVisible
        {
            set
            {
                btSaveHRBP.Visible = value;
            }
        }
        //编辑页面的保存功能
        public bool LeaveSaveSubmit
        {
            set
            {
                btnleaveSave.Visible = value;
            }
        }
        //提交发送邮件
        public bool LeSubmitHRBPMoreVisible
        {
            set
            {
                btnLeaveSubmitHRBP.Visible = value;
            }
        }

        //王红福离职假按钮 
        public bool LeaveFormConfirm
        {
            set
            {
                btLeaveFormConfrim.Visible = value;
            }
        }
        //王凤龙离职部门领导确认

        public bool LeaveDepartLeaderConfirm
        {
            set
            {
                btDepartBossConfrim.Visible = value;
            }
        }
        //保存草稿
        public bool LeSaveDraftMoreVisible
        {
            set
            {
                bnLeaveSaveDraft.Visible = value;
            }
        }
        //关闭
        public bool LeCloseMoreVisible
        {
            set
            {
                bnLeaveCloseHR.Visible = value;
            }
        }
        #endregion

        #endregion

        protected string CurrentUserAdaccoutWithK2Lable
        {
            get { return "K2:" + ConfigurationManager.AppSettings["DomainName"] + "\\" + Page.User.Identity.Name.Split('@')[0]; }
        }

        protected string GetAdAccoutByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return "";
            return ConfigurationManager.AppSettings["DomainName"] + "\\" + email.Split('@')[0];
        }

        #region 注册脚本
        protected override void OnPreRender(EventArgs e)
        {
            //if (SuppotJS())
            //{
            //    Page.ClientScript.RegisterClientScriptResource(this.GetType(), "TaskAmount.javascript.TaskRefresh.js");
            //}

            base.OnPreRender(e);
        }
        #endregion

        #region 检测js是否可用
        //侦测脚本是否可用的函数
        private bool SuppotJS()
        {
            if (!DesignMode)
            {
                if (Page.Request.Browser.EcmaScriptVersion.Major > 0 && Page.Request.Browser.W3CDomVersion.Major > 0)
                    return true;
                return false;
            }
            return false;
        }
        #endregion
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            string ProcessSN = "";

            //SetTaskAmount();    //设置任务数量

            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["SN"]))
                {
                    ProcessSN = Request.QueryString["SN"];
                }
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", ";SetTaskAmount();", true);
            }

            if (TaskPage != null)
            {
                if (TaskPage.IsDraftPage || TaskPage.IsStartPage || TaskPage.IsModifyPage || TaskPage.IsViewProcessPage)
                {
                    divComments.Attributes.Remove("style");
                    divComments.Attributes.Add("style", "display:none;");
                    //span1.Attributes.Remove("style");
                }
            }
            else
            {
                divComments.Attributes.Remove("style");
                divComments.Attributes.Add("style", "display:none;");
            }
        }
        #endregion

        #region button onclicks

        #region submit
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (TaskPage.StartProcess(ProcessAction.Submit))
                MessageBox.ShowAndClose(this.Page, "提交申请成功！");
            else
                MessageBox.Show(this.Page, "提交申请失败！");
        }
        #endregion
        //add by lee at 2011-6-8
        #region submit HRBP
        protected void btnSubmitHRBP_Click(object sender, EventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            if (TaskPage.SaveData(ProcessAction.SubmitHRBP))
                MessageBox.ShowAndPop(this.Page, "提交员工信息成功！", "/Process/OAF/HRBP/OnBoardManage.aspx");
            //MessageBox.ShowAndClose(this.Page, "Submited process successfully!<br/>提交申请成功！");
            else
                MessageBox.Show(this.Page, "提交员工信息失败！");
        }
        #endregion


        #region btnSubmitMore_Click
        protected void btnSubmitMore_Click(object sender, EventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            if (TaskPage.SaveData(ProcessAction.SubmitHRBP))
                MessageBox.ShowAndPop(this.Page, "提交员工信息成功！", "/Process/OAF/HRBPSubmit.aspx");
            //MessageBox.ShowAndClose(this.Page, "Submited process successfully!<br/>提交申请成功！");
            else
                MessageBox.Show(this.Page, "提交员工信息失败！");
        }
        #endregion

        #region submit draft

        protected void btnSubmitHRBPDraft_Click(object sender, EventArgs e)
        {
            //    string status = K2.BDAdmin.ProcessStatus.Draft.ToString().ToLower();//SqlHelper.ExecuteScalar(Database.MerckHRWorkFlow, "GetProcessInfoByProcessID", paras).ToString();
            //    if (status.ToLower() == K2.BDAdmin.ProcessStatus.Draft.ToString().ToLower())
            //    {
            //        if (TaskPage.IsDraftPage)
            //        {
            //            if (TaskPage.SaveData(ProcessAction.SubmitHRBPDraft))
            //                MessageBox.ShowAndPop(this.Page, "提交员工信息成功！", "/Process/OAF/HRBP/OnBoardManage.aspx");
            //            else
            //                MessageBox.Show(this.Page, "提交员工信息失败！");
            //        }
            //        else
            //        {
            //            if (TaskPage.SaveData(ProcessAction.SubmitHRBPDraft))
            //                MessageBox.ShowAndPop(this.Page, "提交员工信息成功！", "/Process/OAF/HRBP/OnBoardManage.aspx");
            //            else
            //                MessageBox.Show(this.Page, "提交员工信息失败！");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show(this.Page, "此数据表单已被提交！");
            //    }

        }
        protected void btnSubmitDraft_Click(object sender, EventArgs e)
        {
            ////SqlParameter[] paras = new SqlParameter[3];
            ////paras[0] = new SqlParameter("@inbProcessID", TaskPage.ProcessID);
            ////paras[1] = new SqlParameter("@nvchrFlowName", TaskPage.FlowName);
            ////paras[2] = new SqlParameter("@nvchrFieldName", "Status");
            //string status = K2.BDAdmin.ProcessStatus.Draft.ToString().ToLower();//SqlHelper.ExecuteScalar(Database.MerckHRWorkFlow, "GetProcessInfoByProcessID", paras).ToString();
            //if (status.ToLower() == K2.BDAdmin.ProcessStatus.Draft.ToString().ToLower())
            //{
            //    if (TaskPage.WorkflowID == 3)  //自定义流程
            //    {
            //        if (TaskPage.SaveData(ProcessAction.Rework))
            //        {
            //            if (TaskPage.StartProcess(ProcessAction.SubmitDraft, "Sohu.OA.Workflow\\CustomWF"))
            //                MessageBox.ShowAndPop(this.Page, "提交成功，该任务已进入到您的‘我的申请’中！", "/WorkSpace/MyDraft.aspx");
            //            else
            //                MessageBox.Show(this.Page, "提交申请失败！");
            //        }
            //        else
            //        {
            //            MessageBox.Show(this.Page, "提交申请失败！");
            //        }
            //    }
            //    else
            //    {
            //        if (TaskPage.StartProcess(ProcessAction.SubmitDraft))
            //            MessageBox.ShowAndClose(this.Page, "提交申请成功！", "../../MyDraft.aspx");
            //        else
            //            MessageBox.Show(this.Page, "提交申请失败！");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show(this.Page, "此申请已被提交！");
            //}
        }
        #endregion

        #region save draft
        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            if (TaskPage.SaveData(ProcessAction.SaveDraft))
            {
                if (TaskPage.WorkflowID == 3)
                {
                    MessageBox.ShowAndPop(this.Page, "保存成功，该任务已进入到您的‘草稿箱’中！", "/WorkSpace/MyDraft.aspx");
                }
                else
                {
                    MessageBox.ShowAndReload(this.Page, "保存申请成功！");
                }
            }
            //MessageBox.Show(this.Page, "Saved process successfully!<br/>保存申请成功！", "../OAF/HRBPSubmit.aspx?FormID=" + TaskPage.FormID + "");
            else
                MessageBox.Show(this.Page, "保存申请失败！");
        }
        #endregion

        #region save HR BP
        protected void btnSaveDraftHR_Click(object sender, EventArgs e)
        {
            if (TaskPage.SaveData(ProcessAction.SaveHRDraft))
                MessageBox.Show(this.Page, "保存申请成功！", "/Process/OAF/HRBP/OnBoardManage.aspx");
            else
                MessageBox.Show(this.Page, "保存申请失败！");
        }
        #endregion

        #region rework
        protected void btnRework_Click(object sender, EventArgs e)
        {
            if (TaskPage.ApproveProcess(ProcessAction.Rework))
            {
                if (TaskPage.WorkflowID == 3)
                {
                    MessageBox.ShowAndPop(this.Page, "提交申请成功！", "/WorkSpace/MyWorklist.aspx");
                }
                else
                {
                    MessageBox.ShowAndClose(this.Page, "提交申请成功！");
                }
            }
            else
                MessageBox.Show(this.Page, "提交申请失败！");
        }
        #endregion

        #region save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (TaskPage.SaveData(ProcessAction.Save))
            {
                if (TaskPage.IsModifyPage)
                {
                    MessageBox.Show(this.Page, "保存员工信息成功!", System.Web.HttpContext.Current.Request.Path + "?FormID=" + TaskPage.FormID);
                }
                else
                    MessageBox.ShowAndClose(this.Page, "保存申请成功！");
            }
            else
                MessageBox.Show(this.Page, "保存申请失败！");
        }
        #endregion

        #region approve
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            TaskPage.DoProcessAction(ProcessAction.Approve, e);
            if (TaskPage.ApproveProcess(ProcessAction.Approve,"",""))
            {
                if (TaskPage.WorkflowID == 3)
                {
                    MessageBox.ShowAndPop(this.Page, "审批成功！该任务已进入到您的‘已处理的任务’中！", "/WorkSpace/MyWorklist.aspx");
                }
                else
                {
                    MessageBox.ShowAndClose(this.Page, "审批申请成功！");
                }
            }

            else
                MessageBox.ShowAndPop(this.Page, "此任务已不存在或您已无权处理此任务！", "/WorkSpace/MyWorklist.aspx");
        }
        #endregion

        #region 入职人员提交表单信息
        protected void btnSubmitOnboard_Click(object sender, EventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            TaskPage.DoProcessAction(ProcessAction.Submit, e);
            if (TaskPage.ApproveProcess(ProcessAction.Submit))
            {
                MessageBox.ShowAndClose(this.Page, "提交表单成功！");

            }

            else
                MessageBox.Show(this.Page, "提交表单失败！");
        }
        #endregion



        #region reject
        protected void btnReject_Click(object sender, EventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            TaskPage.DoProcessAction(ProcessAction.Reject, e);
            if (TaskPage.ApproveProcess(ProcessAction.Reject))
            {
                if (TaskPage.WorkflowID == 3)
                {
                    MessageBox.ShowAndPop(this.Page, "拒绝成功！该任务已进入到您的‘已处理的任务’中！", "/WorkSpace/MyWorklist.aspx");
                }
                else
                {
                    MessageBox.ShowAndClose(this.Page, "拒绝申请成功！");
                }
            }
            else
                MessageBox.ShowAndPop(this.Page, "此任务已不存在或您已无权处理此任务！", "/WorkSpace/MyWorklist.aspx");
        }
        #endregion

        #region Confirm
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            if (TaskPage.ApproveProcess(ProcessAction.Confirm))
                MessageBox.ShowAndPop(this.Page, "确认成功！", "/WorkSpace/MyWorklist.aspx");
            // MessageBox.ShowAndClose(this.Page, "Confirm process successfully!<br/>确认成功！");
            else
                // MessageBox.Show(this.Page, "确认失败！");
                MessageBox.Show(this.Page, "此任务已被他人处理！");
        }
        #endregion

        #region cancel
        public void btnCancel_Click(object sender, EventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            if (TaskPage.ApproveProcess(ProcessAction.Cancel))
                MessageBox.ShowAndPop(this.Page, "取消成功，该任务已进入到您的‘已处理的任务’中！", "/WorkSpace/MyWorklist.aspx");
            else
                MessageBox.Show(this.Page, "取消申请失败！");
        }
        #endregion
        #region close hr
        protected void btnCloseHR_Click(object sender, EventArgs e)
        {
            if (TaskPage.IsModifyPage)
            {
                MessageBox.ReturnPage(this.Page, "/Process/OAF/HRBP/OnBoardManage.aspx");
            }
            else if (TaskPage.IsHRSubmitPage)
            {
                MessageBox.ReturnPage(this.Page, "/Process/OAF/HRBP/OnBoardManage.aspx");
            }
            else if (TaskPage.IsStartPage)
            {
                MessageBox.ReturnPage(this.Page, "/Process/OAF/HRBP/OnBoardManage.aspx");
            }
            else
            {
                MessageBox.ReturnPage(this.Page, "/WorkSpace/MyWorklist.aspx");
            }

        }
        #endregion
        #endregion

        #region 取得任务数量
        private void SetTaskAmount()
        {
            DataSet ds;
            WorlListBLL bll = new WorlListBLL();

            //取得任务数量
            ds = bll.GetMyWorklist(CurrentUserAdaccoutWithK2Lable, "1", "12", "", "", "''", "", "", "", "", "");
            if (ds != null && ds.Tables.Count >= 3)
            {
                hfMyWorklist.Value = ds.Tables[2].Rows[0]["TotalNum"].ToString();
            }
            else
            {
                hfMyWorklist.Value = "0";
            }
        }
        #endregion

        #region GetProcessID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromID"></param>
        /// <param name="WorkFlowName"></param>
        /// <param name="processid"></param>
        /// <returns></returns>
        protected int GetProcessID(string FromID, string WorkFlowName, ref int processid)
        {

            return 0;


        }
        #endregion

        protected void btnSubmitCF_Click(object sender, EventArgs e)
        {
            //反射执行取得流程名称
            MethodInfo mi = Page.GetType().GetMethod("SetValue");
            mi.Invoke(Page, null);
            int ProcInstID = -1;

            if (TaskPage.StartProcess(ProcessAction.SubmitCF, "WF.K2\\SCF", Page.User.Identity.Name, ref ProcInstID))
                MessageBox.ShowAndPop(this.Page, "提交成功，该任务已进入到您的‘我的申请’中！", "/WorkSpace/MyStarted.aspx");
            else
                MessageBox.Show(this.Page, "提交申请失败！");
        }

        /// <summary>
        /// 审批时保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApproveSave_Click(object sender, EventArgs e)
        {
            if (TaskPage.SaveData(ProcessAction.ApproveSave))
                MessageBox.ShowAndClose(this.Page, "保存成功！");
            else
                MessageBox.ShowAndPop(this.Page, "此任务已不存在或您已无权处理此任务！", "/WorkSpace/MyWorklist.aspx");
        }

        protected void btnStartCounter_Click(object sender, ImageClickEventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            TaskPage.DoProcessAction(ProcessAction.Approve, e);
            if (TaskPage.ApproveProcess(ProcessAction.StartCounter, "会签"))
            {
                if (TaskPage.WorkflowID == 3)
                {
                    MessageBox.ShowAndPop(this.Page, "发起会签成功！", "/WorkSpace/MyWorklist.aspx");
                }
                else
                {
                    MessageBox.ShowAndClose(this.Page, "审批申请成功！");
                }
            }

            else
                MessageBox.Show(this.Page, "发起会签失败！");
        }

        #region 保存可查看，wfl添加 begin
        protected void btSaveHRBP_Click(object sender, ImageClickEventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            if (TaskPage.SaveData(ProcessAction.SubmitHRCB))
                MessageBox.ShowAndPop(this.Page, "提交离职员工信息成功！", "/Process/TAF/HRBP/LeftEmployeeMgr.aspx");
            //MessageBox.ShowAndClose(this.Page, "Submited process successfully!<br/>提交申请成功！");
            else
                MessageBox.Show(this.Page, "提交离职员工信息失败！");
        }

        //提交
        protected void btnLeaveSubmitHRBP_Click(object sender, ImageClickEventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            if (TaskPage.StartProcess(ProcessAction.SubmitHRBP, "TAF"))
                MessageBox.ShowAndPop(this.Page, "发起离职流程成功！", "/Process/TAF/HRBP/LeftEmployeeMgr.aspx");
            //MessageBox.ShowAndClose(this.Page, "Submited process successfully!<br/>提交申请成功！");
            else
                MessageBox.Show(this.Page, "发起离职流程失败！");
        }

        //保存草稿
        protected void bnLeaveSaveDraft_Click(object sender, ImageClickEventArgs e)
        {
            if (TaskPage.SaveData(ProcessAction.SaveHRDraft))
                MessageBox.Show(this.Page, "保存申请成功！", "/Process/TAF/HRBP/LeftEmployeeMgr.aspx");
            else
                MessageBox.Show(this.Page, "保存申请失败！");
        }

        //关闭
        protected void bnLeaveCloseHR_Click(object sender, ImageClickEventArgs e)
        {
            if (TaskPage.IsModifyPage)
            {
                MessageBox.ReturnPage(this.Page, "/Process/TAF/HRBP/LeftEmployeeMgr.aspx");
            }
            else if (TaskPage.IsStartPage)
            {
                MessageBox.ReturnPage(this.Page, "/Process/TAF/HRBP/LeftEmployeeMgr.aspx");
            }
            else
            {
                MessageBox.ReturnPage(this.Page, "/WorkSpace/MyWorklist.aspx");
            }
        }

        //只保存数据
        protected void btnleaveSave_Click(object sender, ImageClickEventArgs e)
        {
            TaskPage.Comments = txtComments.Text;
            if (TaskPage.SaveData(ProcessAction.DraftHR))
                MessageBox.ShowAndPop(this.Page, "保存离职员工信息成功！", "/Process/TAF/HRBP/LeftEmployeeMgr.aspx");
            //MessageBox.ShowAndClose(this.Page, "Submited process successfully!<br/>提交申请成功！");
            else
                MessageBox.Show(this.Page, "保存离职员工信息失败！");
        }

        #endregion 保存可查看，wfl添加 end

        #region 自定义发起（返回按钮）
        public bool CDFStartBackVisible
        {
            set
            {
                btnCDFStartBack.Visible = value;
            }
        }

        protected void btnCDFStartBack_Click(object sender, ImageClickEventArgs e)
        {
            MessageBox.ReturnPage(this.Page, "../../Index.aspx");
        }
        #endregion


        #region 自定义审批（返回按钮）
        public bool CDFApproveBackVisible
        {
            set
            {
                btnCDFApproveBack.Visible = value;
            }
        }
        protected void btnCDFApproveBack_Click(object sender, ImageClickEventArgs e)
        {
            MessageBox.ReturnPage(this.Page, "../../WorkSpace/MyWorklist.aspx");
        }
        #endregion

        #region 自定义审批（返回按钮）
        public bool CDFDraftBackVisible
        {
            set
            {
                btnCDFDraftBack.Visible = value;
            }
        }
        protected void btnCDFDraftBack_Click(object sender, ImageClickEventArgs e)
        {
            MessageBox.ReturnPage(this.Page, "../../WorkSpace/MyDraft.aspx");
        }
        #endregion

        #region 自定义查看（返回按钮）
        public bool CDFViewBackVisible
        {
            set
            {
                btnCDFViewBack.Visible = value;
            }
        }
        protected void btnCDFViewBack_Click(object sender, ImageClickEventArgs e)
        {
            string refAddress = Request.ServerVariables["HTTP_REFERER"];
            MessageBox.ReturnPage(this.Page, "../../WorkSpace/MyStarted.aspx");
        }
        #endregion
    }
}