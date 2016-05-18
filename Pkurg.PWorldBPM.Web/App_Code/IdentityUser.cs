using System;
using System.Linq;
using System.Web;
using Pkurg.PWorldBPM.Common.Info;

/// <summary>
///IdentityUser 的摘要说明
/// </summary>
public class IdentityUser
{
    /// <summary>
    /// 根据登录名获取用户实体
    /// 有缓存
    /// by yanghechun
    /// </summary>
    /// <returns></returns>
    public UserInfo GetEmployee()
    {
        string local = "";
        //移动端没有用户认证,匿名认证
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            //app.ashx,移动端查看的view页面请求都会有这两个参数。
            //PC端获取不到当前用户，因为没有userencode
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["userencode"]) && HttpContext.Current.Request["ref"].ToLower() == "mobile")
            {
                local = DESEncrypt.Decrypt(HttpContext.Current.Request["userencode"]);
            }
            else
            {
                local = "zybpm";
            }
        }
        else
        {
            local = HttpContext.Current.User.Identity.Name.ToLower().Replace("founder\\", "");
        }

        string loginName = local;

        //处理方正世纪的域账号
        if (local.ToLower().StartsWith("hold") && local.ToLower() == "hold\\wangwh")
        {
            loginName = "wangweihong";
        }
        else
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //切换用户
                string to = new SwitchUser().IsExist(local);
                if (!string.IsNullOrEmpty(to))
                {
                    loginName = to;
                }
                //模拟用户
                to = new LoginUser().IsExist(local);
                if (!string.IsNullOrEmpty(to))
                {
                    loginName = to;
                }
                //映射用户
                if (IsMappingUser(local))
                {                   
                    Pkurg.PWorldBPM.Business.Sys.MappingUser ClassMappingUser = new Pkurg.PWorldBPM.Business.Sys.MappingUser();
                    to = ClassMappingUser.GetToUserCode(local, 1);
                    if (!string.IsNullOrEmpty(to))
                    {
                        loginName = to;
                    }
                }               
            }
        }

        //最终用户
        string key = "cache_user_" + loginName;
        if (Pkurg.PWorldBPM.Common.Cache.DataCache.GetCache(key) == null)
        {
            Pkurg.PWorldBPM.Common.Services.OrgService services = new Pkurg.PWorldBPM.Common.Services.OrgService();
            UserInfo model = services.GetUserInfo(loginName);
            if (model != null)
            {
                Pkurg.PWorldBPM.Common.Cache.DataCache.SetCache(key, model, DateTime.Now.AddDays(7), TimeSpan.Zero);
            }
            else
            {
                //被模拟的用户不存在，重新切换到当前用户
                bool flag1 = new SwitchUser().DeleteByTo(loginName);
                bool flag2 = new LoginUser().DeleteByTo(loginName);
                if (flag1 & flag2)
                {
                    HttpContext.Current.Response.Redirect("~/Default.aspx");
                }
                else
                {
                    throw new Exception("未找到用户信息");
                }
            }
        }
        return (UserInfo)Pkurg.PWorldBPM.Common.Cache.DataCache.GetCache(key);
    }

    /// <summary>
    /// 验证当前用户权限
    /// star
    /// </summary>
    /// <param name="permissionName"></param>
    /// <returns></returns>
    public static bool CheckPermission(string permissionName)
    {
        UserInfo info = new IdentityUser().GetEmployee();
        Pkurg.PWorldBPM.WorkFlowRule.Setting db = new Pkurg.PWorldBPM.WorkFlowRule.Setting();
        string admin = db.GetValueByName(permissionName);
        bool flag = admin.ToLower().Split(',').Contains(info.LoginId.ToLower());

        return flag;
    }

    /// <summary>
    /// 是否有映射用户的权限
    /// </summary>
    protected bool IsMappingUser(string local)
    {
        bool flag = false;
        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["MappingUsers"]))
        {
            string loginUsers = System.Configuration.ConfigurationManager.AppSettings["MappingUsers"];
            string[] items = loginUsers.Split(',');
            flag = items.Any(p => p == local);
        }
        return flag;
    }
}