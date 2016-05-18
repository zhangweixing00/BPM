using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.Security;
using Model;
using System.Configuration;

namespace K2Utility
{
    public class BasePage : Page
    {
        /// <summary>
        /// Windows Domain Name
        /// </summary>
        static readonly string DomainName = ConfigurationManager.AppSettings["DomainName"];
        string NoFilter = ConfigurationManager.AppSettings["NoFilter"];

        public readonly string cookieEmployeeKey = "OA.Employee";
        public readonly string cookieEmployeeName = "OA.Employee.Name";
        public readonly string cookieEmployeeCode = "OA.Employee.Code";
        public readonly string cookieEmployeeEmail = "OA.Employee.Email";
        public readonly string cookieEmployeePost = "OA.Employee.Post";


        protected override void OnInit(EventArgs e)
        {
            CheckUserRoleForm();

            base.OnInit(e);
        }
        protected string CurrentUser
        {
            get { return Page.User.Identity.Name; }
        }
        protected string CurrentUserAdaccoutWithK2Lable
        {
            get { return "K2:" + CurrentUser; }
        }

        #region employee
        /// <summary>
        /// 员工号
        /// </summary>
        public string EmployeeCode
        {
            get
            {
                HttpCookie cookie = GetCookie();
                return cookie[cookieEmployeeCode];
                //return Employee.ID.ToString();
            }
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeID
        {
            get
            {
                return Employee.ID.ToString();
            }
        }

        /// <summary>
        /// POST
        /// </summary>
        public string Post
        {
            get
            {
                return Employee.CostCenter;
            }
        }

        /// <summary>
        /// employee name
        /// </summary>
        public string CHName
        {
            get
            {
                return Employee.CHName;
            }
        }

        /// <summary>
        /// 员工AD 
        /// </summary>
        public string ADAccount
        {
            get
            {
                //FBA to AD
                return Page.User.Identity.Name;
                //return "honycapital\\xugh";
            }
        }

        /// <summary>
        /// 工作地点
        /// </summary>
        public string WorkSpace
        {
            get
            {
                return Employee.WorkPlace;
            }
        }

        /// <summary>
        /// 邮件
        /// </summary>
        public string Email
        {
            get
            {
                return Employee.Email;
            }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            get
            {
                return Employee.BlackBerry;
            }
        }

        public HttpCookie GetCookie()
        {
            HttpCookie cookie = CookieHelper.GetCookie(cookieEmployeeKey);
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                cookie = SetCookie();
            }
            return cookie;
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


        public UserProfileInfo Employee
        {
            get
            {
                UserProfileInfo employeeEntity = Biz_Employee.GetEmployee(ADAccount);
                return employeeEntity;
            }
        }

        private HttpCookie SetCookie()
        {
            UserProfileInfo employee = Biz_Employee.GetEmployee(ADAccount);
            return SetCookie(employee);
        }

        public HttpCookie SetCookie(UserProfileInfo employee)
        {
            CookieEntity cokc = new CookieEntity();
            cokc.mainCookieName = cookieEmployeeKey;
            cokc.mainCookieValue = Server.UrlEncode(employee.CHName);

            Dictionary<string, string> childCooks = new Dictionary<string, string>();
            childCooks.Add(cookieEmployeeName, Server.UrlEncode(employee.CHName));
            childCooks.Add(cookieEmployeeEmail, employee.Email);
            childCooks.Add(cookieEmployeeCode, employee.ID.ToString());
            //childCooks.Add(cookieEmployeePost, Server.UrlEncode(employee.Post));
            cokc.childCookies = childCooks;

            return CookieHelper.SetCookie(cokc);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie = new HttpCookie(cookieEmployeeKey);
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.SetCookie(cookie);
        }
        #endregion

        /// <summary>
        /// 检查角色,是否有查看当前表单的权限
        /// </summary>
        public void CheckUserRoleForm()
        {
            //if (IsNeedFilter())
            //{
            //    //用户能看到一些菜单
            //    List<T_Application> list = Biz_RoleForm.biz_RoleForm.GetV_Source_MenuPermission(EmployeeCode);
            //    //是否有权限查看表单
            //    var path = GetRequestPath(2).ToLower();

            //    //是否需要验证权限
            //    bool isExistPath = Biz_RoleForm.biz_RoleForm.IsExistPath(path);
            //    //有没有权限看到当前页面
            //    var menu = list.Where(c => c.MenuURL.ToLower().Contains(path));
            //    if (menu.Count() < 1 && isExistPath)
            //    {
            //        Context.Response.Redirect("/error.aspx?error=" + Server.UrlEncode("没有权限查看此页面！"));
            //    }

            //}
        }

        /// <summary>
        /// 执行弹出消息
        /// </summary>
        /// <param name="msg"></param>
        public void ExecAlertScritp(string msg)
        {
            Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                "Msg", "<script language='javascript'>alert('" + msg + "')</script>"
                );
        }
    }
}
