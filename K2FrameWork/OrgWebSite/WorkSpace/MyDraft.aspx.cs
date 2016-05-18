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
using Sohu.OA.Web;
using Utility;

namespace K2.BDAdmin.Web.WorkSpace
{
    public partial class MyDraft : BasePage, ICallbackEventHandler
    {
        protected override void OnInit(EventArgs e)
        {
            //base.OnInit(e);
            //MyDraft1.EmployeeCode = this.EmployeeCode;
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
            MyDraft1.DeleteDraft(eventArgument.Split(';')[0], eventArgument.Split(';')[1]);
        }

        #endregion
    }
}
