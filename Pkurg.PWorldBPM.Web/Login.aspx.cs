using System;
using System.Linq;
using System.Web;

public partial class Login : UPageBase
{
    LoginUser bll = new LoginUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsLoginUser)
            {
                lblMsg.Text = "对不起，您没有模拟用户的权限！";
                btnConfirm.Enabled = false;
            }
            else
            {
                lblMsg.Text = "";
                btnConfirm.Enabled = true;
            }
            string to = bll.IsExist(fromUserCode);
            if (!string.IsNullOrEmpty(to))
            {
                txtUserCode.Text = to;
            }
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string toUserCode = txtUserCode.Text.Trim().ToLower();

        Pkurg.PWorldBPM.Common.Services.OrgService services = new Pkurg.PWorldBPM.Common.Services.OrgService();
        Pkurg.PWorldBPM.Common.Info.UserInfo model = services.GetUserInfo(toUserCode);
        if (model != null)
        {
            _BPMContext.LoginId = toUserCode;
            //Insert
            bll.Insert(fromUserCode, toUserCode);
            lblMsg.Text = "";
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            lblMsg.Text = "登录名不存在！";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtUserCode.Text.Trim()))
        {
            //return;
        }

        Pkurg.PWorldBPM.Common.Services.OrgService services = new Pkurg.PWorldBPM.Common.Services.OrgService();
        Pkurg.PWorldBPM.Common.Info.UserInfo model = services.GetUserInfo(fromUserCode);
        if (model != null)
        {
            System.Web.HttpContext.Current.Session["BPM_User"] = model;
            //Delete
            bll.Delete(fromUserCode);
        }
        Response.Redirect("~/Login.aspx");
    }

    protected string fromUserCode
    {
        get
        {
            return HttpContext.Current.User.Identity.Name.ToLower().Replace("founder\\", "");
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
                //flag = System.Configuration.ConfigurationManager.AppSettings["LoginUsers"].ToString().ToLower().Contains(fromUserCode);
                string loginUsers = System.Configuration.ConfigurationManager.AppSettings["LoginUsers"];
                string[] items = loginUsers.Split(',');
                flag = items.Any(p => p == fromUserCode);
            }
            return flag;
        }
    }
}