using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace K2.BDAdmin.Web.WorkSpace
{
    public partial class MyWorklist : System.Web.UI.Page, ICallbackEventHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region ICallbackEventHandler Members

        public string GetCallbackResult()
        {
            return "";
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            //MyWorklist1.ReLoadData();
            //string batchApprovalAction = General.GetConstValue("BatchApprovalAction");
            //try
            //{
            //    string[] arrSNs = eventArgument.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //    SourceCode.Workflow.Client.Connection k2conn = new SourceCode.Workflow.Client.Connection();
            //    k2conn.Open(General.GetConstValue("K2Server"));
            //    foreach (string sn in arrSNs)
            //    {
            //        SourceCode.Workflow.Client.WorklistItem wli = k2conn.OpenWorklistItem(sn);
            //        foreach (SourceCode.Workflow.Client.Action act in wli.Actions)
            //        {
            //            if (batchApprovalAction.ToUpper().Contains(act.Name.ToUpper() + ";"))
            //            {
            //                act.Execute();
            //            }
            //        }
            //    }

            //    k2conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    DBManager.RecoreErrorProfile(ex, "K2.BDAdmin.Web.WorkPlace.RaiseCallbackEvent", "K2.BDAdmin");
            //}            
        }

        #endregion
    }
}
