using System;
using System.Drawing;
using System.Linq;
using System.Web;

public partial class BPM : System.Web.UI.MasterPage
{
    Pkurg.PWorldBPM.Business.Sys.MappingUser ClassMappingUser = new Pkurg.PWorldBPM.Business.Sys.MappingUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLoginUser)
            {
                //链接为模拟用户页面
                lbtnLogin.Visible = true;
            }

            string localName = HttpContext.Current.User.Identity.Name.ToLower();
            lblUserInfo.Text = localName;

            string to = new SwitchUser().IsExist(fromUserCode);
            if (!string.IsNullOrEmpty(to))
            {
                lblUserInfo.Text = "founder\\" + to;
                lblUserInfo.ForeColor = Color.Red;
                lbtnLogout.Visible = true;
            }

            to = new LoginUser().IsExist(fromUserCode);
            if (!string.IsNullOrEmpty(to))
            {
                lblUserInfo.Text = localName + "模拟了" + to;
                lblUserInfo.ForeColor = Color.Red;
            }
            //映射用户
            if (IsMappingUser)
            {
                Pkurg.PWorldBPM.Business.Sys.SYS_MappingUser model = ClassMappingUser.GetState(localName);
                if (model == null)
                {
                    lbtnMapping.Visible = false;
                    lbtnCancelMapping.Visible = false;
                }
                else if (model.State == 0)
                {
                    lbtnMapping.Visible = true;
                    lbtnCancelMapping.Visible = false;
                }
                else if (model.State == 1)
                {
                    lblUserInfo.Text = localName + "映射了" + model.ToUserCode;
                    lblUserInfo.ForeColor = Color.Red;
                    lbtnMapping.Visible = false;
                    lbtnCancelMapping.Visible = true;
                }
            }
        }
    }

    //切换
    protected void lbtnSwitch_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Switch.aspx");
    }

    //模拟
    protected void lbtnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }

    //退出切换用户
    protected void lbtnLogout_Click(object sender, EventArgs e)
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

    //映射
    protected void lbtnMapping_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Mapping.aspx");
    }

    //取消映射
    protected void lbtnCancelMapping_Click(object sender, EventArgs e)
    {
        Pkurg.PWorldBPM.Common.Services.OrgService services = new Pkurg.PWorldBPM.Common.Services.OrgService();
        Pkurg.PWorldBPM.Common.Info.UserInfo model = services.GetUserInfo(fromUserCode);
        if (model != null)
        {
            System.Web.HttpContext.Current.Session["BPM_User"] = model;

            string CurrentUserCode = HttpContext.Current.User.Identity.Name.ToLower().Replace("founder\\", "");
            ClassMappingUser.UpdateState(CurrentUserCode, 0);
            lbtnMapping.Visible = true;
            lbtnCancelMapping.Visible = false;
        }

        Response.Redirect("~/Default.aspx");
    }

    #region 属性

    /// <summary>
    /// 是否有映射用户的权限
    /// </summary>
    protected bool IsMappingUser
    {
        get
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["MappingUsers"]))
            {
                string loginUsers = System.Configuration.ConfigurationManager.AppSettings["MappingUsers"];
                string[] items = loginUsers.Split(',');
                flag = items.Any(p => p == fromUserCode);
            }
            return flag;
        }
    }

    /// <summary>
    /// 是否有模拟登录的权限
    /// </summary>
    protected bool IsLoginUser
    {
        get
        {
            //测试环境，每个人都可以模拟
            //正式环境，只有LoginUsers才可以模拟
            if (IsDebug)
            {
                return true;
            }

            bool flag = false;
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["LoginUsers"]))
            {
                string loginUsers = System.Configuration.ConfigurationManager.AppSettings["LoginUsers"];
                string[] items = loginUsers.Split(',');
                flag = items.Any(p => p == fromUserCode);
            }
            return flag;
        }
    }

    string fromUserCode
    {
        get
        {
            return HttpContext.Current.User.Identity.Name.ToLower().Replace("founder\\", "");
        }
    }

    bool IsDebug
    {
        get
        {

            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["IsDebug"]))
            {
                return System.Configuration.ConfigurationManager.AppSettings["IsDebug"].ToString() == "1";
            }
            else
            {
                return false;
            }
        }
    }

    #endregion
}
