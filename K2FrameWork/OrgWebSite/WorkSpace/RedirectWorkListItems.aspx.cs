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
    public partial class RedirectWorkListItems : System.Web.UI.Page,ICallbackEventHandler
    {
        public string SNs
        {
            get 
            {
                if (!string.IsNullOrEmpty(Request.QueryString["sns"]))
                    return Request.QueryString["sns"];

                return "";
            }
        }

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
            //try
            //{
            //    string fromUser = Page.User.Identity.Name;
            //    string toUser = eventArgument;
            //    Connection k2conn = new Connection();
            //    k2conn.Open(General.GetConstValue("K2Server"));

            //    string[] arrSNs = SNs.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (string sn in arrSNs)
            //    {
            //        WorklistItem wli = k2conn.OpenWorklistItem(sn, "ASP", false);
            //        if (wli.Status == WorklistStatus.Open)
            //            wli.Release();
            //        wli.Redirect(toUser);
            //        DBManager.AddWorkListLog(fromUser, toUser, wli.SerialNumber, wli.ProcessInstance.ID, wli.ActivityInstanceDestination.ID, "Redirect", fromUser);
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
