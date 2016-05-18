using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace OrgWebSite.Search.K2FlowCheck
{
    public partial class K2FlowCheck : System.Web.UI.Page
    {
        private string _displayCount = ConfigurationManager.AppSettings["K2EmployeeCheckCountry"];
        public string DisplayCount
        {
            get
            {
                return _displayCount;
            }
            set { _displayCount = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["param"]))
            {
                //弹出窗口时传递过来的条件
                param.Value = Request["param"].ToString();
            }
            if (!string.IsNullOrEmpty(Request["checkstyle"]))
            {
                //单选或多选
                checkstyle.Value = Request["checkstyle"].ToString();
                if (Convert.ToBoolean(checkstyle.Value))        //单选
                {
                    DisplayCount = ConfigurationManager.AppSettings["K2EmployeeCheckCountryOne"];
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ss", "SelectedUserDivDis();", true);
                    //firstDiv.Attributes["style"] = "width: 750px; height: 470px;";
                    //secDiv.Attributes["style"] = "overflow: hidden; width: 700px; height: 374px; margin-top: 0px;";
                    //btTrue.Attributes["style"] = "display:none;";
                    //thirdDiv.Attributes["style"] = "width: 250px; padding-right: 150px;";
                    noselectedtags.Attributes["style"] = "display:none;";
                }
                else//多选
                {
                    //firstDiv.Attributes["style"] = "width: 750px; height: 500px;";
                    //secDiv.Attributes["style"] = "overflow: hidden; width: 700px; height: 416px; margin-top: 0px;";
                    //btTrue.Attributes["style"] = "";
                    //thirdDiv.Attributes["style"] = "width: 300px; padding-right: 150px;";
                    noselectedtags.Attributes["style"] = "display:none;";
                }
            }

            if (!string.IsNullOrEmpty(Request["pos"]))
            {
                //单选或多选
                pos.Value = Request["pos"].ToString();
            }

        }
    }
}