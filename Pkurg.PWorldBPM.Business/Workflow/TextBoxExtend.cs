using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pkurg.PWorldBPM.Business.Workflow
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:TextBoxExtend runat=server></{0}:TextBoxExtend>")]
    public class TextBoxExtend : System.Web.UI.WebControls.TextBox
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ContrlID
        {
            get
            {
                String s = (String)ViewState["ContrlID"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["ContrlID"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string FormContrlValueID
        {
            get
            {
                String s = (String)ViewState["FormContrlValueID"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["FormContrlValueID"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string IsInput
        {
            get
            {
                String s = (String)ViewState["IsInput"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["IsInput"] = value;
            }
        }
        private bool isRequire = false;
        [Bindable(true)]
        [Category("IsRequire")]
        [DefaultValue("")]
        [Localizable(true)]
        public bool IsRequire
        {
            get
            {
                return isRequire;
            }

            set
            {
                isRequire = value;
            }
        }

      //   protected override void Render(System.Web.UI.HtmlTextWriter writer)

      //     {

      //          Attributes["onblur"] =Page.ClientScript.GetPostBackEventReference(this,"this.value");

      //         base.Render (writer);

      //     }

  

      //public event EventHandler OnBlur;

      //public virtual void RaisePostBackEvent(string eventArgument)
      //{

      //    if (OnBlur != null)
      //    {

      //        OnBlur(this, null);

      //    }
      //}
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(ContrlID);
            output.Write(FormContrlValueID);
        }
    }
}
