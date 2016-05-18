using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class UserControls_Left : System.Web.UI.UserControl
{
    protected string navigationContent = string.Empty;
    SiteMapDataSource SiteMapDataSource1 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.MasterPageFile == "/BPM.master")
        {
            SiteMapDataSource1 = (SiteMapDataSource)this.Page.Master.FindControl("SiteMapDataSource1");
        }
        else
        {
            SiteMapDataSource1 = (SiteMapDataSource)this.Page.FindControl("SiteMapDataSource1");
        }


        if (!IsPostBack)
        {
            BuildNavigation();
        }
    }

    private void BuildNavigation()
    {
        int index = 0;
        try
        {
            StringBuilder strContent = new StringBuilder();
            SiteMapNodeCollection nodes = this.SiteMapDataSource1.Provider.RootNode.ChildNodes;
            foreach (SiteMapNode subNode in nodes)
            {
                //控制菜单权限组
                if (subNode.Title == "系统管理" && !IsLoginUser)
                {
                    continue;
                }

                if (subNode.Title == "流程制度" && !IsCagegoryAdmin)
                {
                    continue;
                }

                if (subNode.Title == "工作流管理" && !IsInstanceAdmin)
                {
                    continue;
                }

                index++;

                strContent.AppendFormat(@"<dl tag={0}><dt tag={0}>{1}</dt>", index, subNode.Title);
                strContent.AppendFormat(@"<dd>");

                int j = 0;
                foreach (SiteMapNode node in subNode.ChildNodes)
                {
                    j++;

                    bool isCurrent = (this.SiteMapDataSource1.Provider.CurrentNode != null) && (node.Key == this.SiteMapDataSource1.Provider.CurrentNode.Key);

                    strContent.Append(@"<div class=""item"">");

                    if (isCurrent)
                    {
                        strContent.Append(@"<span class='curr' style='float:left;'>");
                    }
                    else
                    {
                        strContent.Append(@"<span style='float:left;'>");
                    }
                    strContent.Append(@"<a href=");
                    strContent.Append(node.Url);
                    string blank = " Target=\"_blank\"";
                    if (node.Title.ToLower() == "pworld" || node.Title.ToLower() == "流程制度门户")
                    {
                        strContent.Append(blank);
                    }
                    strContent.AppendFormat(@" tag={0}>", index * 100 + j);

                    strContent.Append(node.Title);

                    strContent.Append(@"</a>");
                    strContent.Append(@"</span>");

                    strContent.Append(@"</div>");
                }
                strContent.AppendFormat(@"</dd>");
                strContent.AppendFormat(@"</dl>");
            }
            litNav.Text = strContent.ToString();

        }
        catch
        {
            return;
        }

    }

    protected bool IsDebug
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

    /// <summary>
    /// 是否有模拟登录的权限(是否管理员)
    /// yanghechun
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
                string fromUserCode = HttpContext.Current.User.Identity.Name.ToLower().Replace("founder\\", "");
                //flag = System.Configuration.ConfigurationManager.AppSettings["LoginUsers"].ToString().ToLower().Contains(fromUserCode);
                string loginUsers = System.Configuration.ConfigurationManager.AppSettings["LoginUsers"];
                string[] items = loginUsers.Split(',');
                flag = items.Any(p => p == fromUserCode);
            }
            return flag;
        }
    }

    /// <summary>
    /// 是否流程制度分类管理员
    /// left.ascx也有
    /// </summary>
    public bool IsCagegoryAdmin
    {
        get
        {
            bool flag = false;
            //全局管理员
            Pkurg.PWorldBPM.WorkFlowRule.Setting db = new Pkurg.PWorldBPM.WorkFlowRule.Setting();
            string admin = db.GetValueByName("Rule_Admin");

            IdentityUser identityUser = new IdentityUser();
            Pkurg.PWorldBPM.Common.Info.UserInfo userInfo = identityUser.GetEmployee();
            string userCode = userInfo.LoginId;

            flag = admin.Split(',').Contains(userCode);

            if (!flag)
            {
                //流程分类和管理员
                Pkurg.PWorldBPM.WorkFlowRule.Rule rule = new Pkurg.PWorldBPM.WorkFlowRule.Rule();
                flag = rule.CheckIsCagegoryAdmin(userCode);
            }
            return flag;
        }
    }


    /// <summary>
    /// 流程实例管理员：超级管理员+城市公司流程管理员
    /// </summary>
    public bool IsInstanceAdmin
    {
        get
        {
            bool isSuperAdmin = IdentityUser.CheckPermission("SuperAdmins");
            bool isOAAdmin = IdentityUser.CheckPermission("OAAdmin");
            bool isERPAdmin = IdentityUser.CheckPermission("ERPAdmin");
            if (isSuperAdmin || isOAAdmin || isERPAdmin)
            {
                return true;
            }
            Pkurg.PWorldBPM.Common.Info.UserInfo userInfo = new IdentityUser().GetEmployee();
            string userCode = userInfo.LoginId;
            Pkurg.PWorld.Entities.TList<Pkurg.PWorld.Entities.Department> deptInfos = new Pkurg.PWorld.Business.Permission.BFPmsUserRoleDepartment().
    GetDeptListByEmployeeCodeAndRoleName(userInfo.PWordUser.EmployeeCode, "实例管理员");
            if (deptInfos.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}