using System;
using System.Web;

public partial class Mapping : UPageBase
{
    Pkurg.PWorldBPM.Business.Sys.MappingUser ClassMappingUser = new Pkurg.PWorldBPM.Business.Sys.MappingUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DoMapping();
        }
    }

    void DoMapping()
    {
        string currentUserCode = HttpContext.Current.User.Identity.Name.ToLower().Replace("founder\\", "");
        Pkurg.PWorldBPM.Business.Sys.SYS_MappingUser mapping = ClassMappingUser.UpdateState(currentUserCode, 1);
        if (mapping != null)
        {
            string toUserCode = mapping.ToUserCode;
            Pkurg.PWorldBPM.Common.Services.OrgService services = new Pkurg.PWorldBPM.Common.Services.OrgService();
            Pkurg.PWorldBPM.Common.Info.UserInfo model = services.GetUserInfo(toUserCode);
            if (model != null)
            {
                _BPMContext.LoginId = toUserCode;
            }
        }
        Response.Redirect("~/Default.aspx");
    }
}
