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
    public partial class OriginatorConfirm : ProcessPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcControl = CDF1;
            IsCDFConfirm = true;
            WorkflowID = 3;
            CDF1.CurrentEmpolyeeID = EmployeeID;
            CDF1.IsSign = "false";
            if (!IsPostBack)
            {
                string sn = Request.QueryString["SN"];
                string procInstId = string.Empty;
                string viewflow = string.Empty;
                WorkflowHelper.GetProcessInfo(sn, ref procInstId, ref viewflow);
                Imgcontrol.Attributes["onclick"] = "window.open ('" + viewflow + "', 'newwindow', 'height=800, width=1600, top=0, left=0, toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no') ";
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

        protected void btnRepSigner_Click(object sender, ImageClickEventArgs e)
        {
            string _sn = Request.QueryString["SN"];
            if (!string.IsNullOrEmpty(_sn))
            {
                if (WorkflowHelper.ApproveProcess(_sn, "Next"))
                {
                    ProcessLog("Next", "校稿", "Next");
                    MessageBox.ShowAndPop(this.Page, "提交成功！", "/WorkSpace/MyWorklist.aspx");
                }
            }
        }
    }
}