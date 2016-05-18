using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;

namespace K2Utility
{
    public class ViewPage : ProcessPage
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                //register print css
                HtmlLink link = new HtmlLink();
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("rel", "stylesheet");
                link.Attributes.Add("media", "print");
                link.Attributes.Add("href", "../css/print.css");
                Header.Controls.Add(link);

                //register view process script
                ClientScript.RegisterClientScriptInclude(this.GetType(), "", "../../JavaScript/ViewProcess.js");
            }
            base.OnLoad(e);
        }
    }
}
