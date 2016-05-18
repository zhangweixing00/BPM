using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace K2Organization.WorkSpace
{
    public partial class MyWorkFlow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            //DataTable dt = K2.BDAdmin.DBManager.GetSendFlowByUserCode(string.Empty);
            //dlWorkFlow.DataSource = dt;
            //dlWorkFlow.DataBind();
        }
    }
}
