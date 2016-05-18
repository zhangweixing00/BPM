using System;
using System.DirectoryServices;
using System.Web;

public partial class Switch : UPageBase
{
    SwitchUser bll = new SwitchUser();

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        bool flag = CheckLogin(txtUserCode.Text.Trim(), txtPwd.Text.Trim());
        if (!flag)
        {
            lblMsg.Text = "登录失败，请重新输入！";
            txtPwd.Focus();
        }
        else
        {
            //如果切换的账号和当前一致，不处理
            if (fromUserCode.ToLower() == txtUserCode.Text.Trim().ToLower())
            {
                Pkurg.PWorldBPM.Common.Services.OrgService services = new Pkurg.PWorldBPM.Common.Services.OrgService();
                Pkurg.PWorldBPM.Common.Info.UserInfo model = services.GetUserInfo(fromUserCode);
                if (model != null)
                {
                    System.Web.HttpContext.Current.Session["BPM_User"] = model;
                    //Delete
                    new SwitchUser().Delete(fromUserCode);
                }
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                string toUserCode = txtUserCode.Text.Trim().ToLower();
                Pkurg.PWorldBPM.Common.Services.OrgService services = new Pkurg.PWorldBPM.Common.Services.OrgService();
                Pkurg.PWorldBPM.Common.Info.UserInfo model = services.GetUserInfo(toUserCode);
                if (model != null)
                {
                    _BPMContext.LoginId = toUserCode;
                    //Insert
                    bll.Insert(fromUserCode, toUserCode);
                    //JsHelper.AlertAndRedirect(Page, "切换用户成功！如果要注销，请点击“退出”按钮。", "Default.aspx");
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lblMsg.Text = "域账号不存在！";
                }
            }

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    bool CheckLogin(string userName, string passWord)
    {
        try
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["LDAP"];
            if (string.IsNullOrEmpty(path))
            {
                path = "LDAP://172.25.1.3/DC=jtcorp,DC=founder,DC=com";
            }

            DirectoryEntry entry = new DirectoryEntry(path, userName, passWord);
            return entry.NativeGuid != null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected string fromUserCode
    {
        get
        {
            return HttpContext.Current.User.Identity.Name.ToLower().Replace("founder\\", "");
        }
    }

}