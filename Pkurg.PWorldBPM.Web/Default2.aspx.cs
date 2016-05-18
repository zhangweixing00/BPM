using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var currentStep = DBContext.GetBizContext().OA_ITSupport_Step.FirstOrDefault(x => x.Id == "0fdd87a2-4ce5-4413-87a6-cdb567bb4e57");

    }
}