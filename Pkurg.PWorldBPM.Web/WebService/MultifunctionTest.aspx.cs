using System;
using System.Collections.Generic;
using Pkurg.PWorldBPM.Business.Controls;

public partial class WebService_MultifunctionTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        List<WF_CountersignParameter> item = new List<WF_CountersignParameter>();
        WF_CounterSign.SyncDataToDB("1", item);
    }
}