using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using Model;
using System.Reflection;
namespace OrgWebSite.Process.CDF
{
    public partial class XReSubmit : ProcessPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcControl = CDF1;
            IsReWorkPage = true;
            WorkflowID = 3;
            CDF1.IsSign = "true";
            CDF1.CurrentEmpolyeeID = EmployeeID;
            if (!IsPostBack)
            {
                string sn = Request.QueryString["SN"];
                string procInstId = string.Empty;
                string viewflow = string.Empty;
                WorkflowHelper.GetProcessInfo(sn, ref procInstId, ref viewflow);
                Imgcontrol.Attributes["onclick"] = "window.open ('" + viewflow + "', 'newwindow', 'height=800, width=1600, top=0, left=0, toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no') ";
            }
        }

        protected void btnApprove_Click(object sender, ImageClickEventArgs e)
        {
            string _sn = Request.QueryString["SN"];
            if (!string.IsNullOrEmpty(_sn))
            {
                if (WorkflowHelper.ApproveProcess(_sn, "ReSubmit"))
                {
                    ProcessLog("ReSubmit", "发起人", "ReSubmit");
                    MessageBox.ShowAndPop(this.Page, "提交成功！", "/WorkSpace/MyWorklist.aspx");
                }
            }
        }

        protected void btnReject_Click(object sender, ImageClickEventArgs e)
        {
            string _sn = Request.QueryString["SN"];
            if (!string.IsNullOrEmpty(_sn))
            {
                if (WorkflowHelper.ApproveProcess(_sn, "Cancel"))
                {
                    ProcessLog("Cancel", "发起人", "Cancel");
                    MessageBox.ShowAndPop(this.Page, "提交成功！", "/WorkSpace/MyWorklist.aspx");
                }
            }
        }

        /// <summary>
        /// process log 
        /// </summary>
        /// <param name="action">process action</param>
        /// <param name="action">activityName</param>
        /// <returns>success or not</returns>
        public bool ProcessLog(string action, string activityName, string opration)
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
            processlog.Comments = string.Empty;
            processlog.Operation = opration;

            Assembly ass = Assembly.Load("BLL");
            Type type = ass.GetType("BLL.ProcessLogBLL");
            MethodInfo info = type.GetMethod("InsertProcessLog");
            Object obj = ass.CreateInstance("BLL.ProcessLogBLL");
            info.Invoke(obj, new ProcessLogInfo[] { processlog });
            return true;
        }
    }
}