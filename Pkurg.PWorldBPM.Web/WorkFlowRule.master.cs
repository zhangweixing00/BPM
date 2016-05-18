using System;

public partial class WorkFlowRule : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //2015-5-18
        IdentityUser identityUser = new IdentityUser();
        Pkurg.PWorldBPM.Common.Info.UserInfo userInfo = identityUser.GetEmployee();
        string code = userInfo.FounderLoginId;
        lblUserInfo.Text = code;
        //string localName = HttpContext.Current.User.Identity.Name.ToLower();
        //lblUserInfo.Text = localName;
    }
}
