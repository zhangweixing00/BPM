using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace K2Utility
{
    public sealed class CookieHelper
    {
        public static HttpCookie SetCookie(CookieEntity cookCls)
        {
            HttpCookie cookie = new HttpCookie(cookCls.mainCookieName);
            cookie.Value = cookCls.mainCookieValue;
            if (string.IsNullOrEmpty(cookCls.domainName))
            {
                cookie.Domain = cookCls.domainName;
            }
            if (cookCls.ExpireDate != null)
            {
                cookie.Expires = (DateTime)cookCls.ExpireDate;
            }
            foreach (KeyValuePair<string,string> entry in cookCls.childCookies)
            {
                cookie[entry.Key] = cookCls.childCookies[entry.Key];
            }
            HttpContext.Current.Response.AppendCookie(cookie);
            return cookie;
        }

        public static HttpCookie GetCookie(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName];
        }
    }

    public class CookieEntity
    {
        public string mainCookieName { get; set; }
        public string mainCookieValue { get; set; }
        public string domainName { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Dictionary<string, string> childCookies { get; set; }
    }
}
