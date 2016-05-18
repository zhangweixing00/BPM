using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using BLL;
using K2Utility;

namespace OrgWebSite.Admin
{
    /// <summary>
    /// Summary description for Validate
    /// </summary>
    public class Validate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["Op"]))
            {
                StringBuilder propertiesList = new StringBuilder();
                switch (HttpContext.Current.Request.QueryString["Op"])
                {
                    case "Validate":
                        propertiesList = Valiedate();
                        break;
                }
                HttpContext.Current.Response.Write(propertiesList.ToString());
            }
        }

        private StringBuilder Valiedate()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["FilterCount"]) && !string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["FilterCond"]))
            {
                string str = HttpContext.Current.Request.QueryString["FilterCond"];
                int count = int.Parse(HttpContext.Current.Request.QueryString["FilterCount"]);
                StringBuilder ret = new StringBuilder();
                str = str.Replace("AND", " AND ").Replace("OR", " OR ").Replace("NOT", " AND ").Replace("(", " ( ").Replace(")", " ) ");    //格式化

                string[] strArray = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in strArray)
                {
                    if (StringHelper.IsInt(s))
                    {
                        ret.Append(s + " = " + s);
                    }
                    else
                    {
                        ret.Append(" " + s + " ");
                    }
                }

                string sQLValue = "select * from FilterConditionParse where ";
                StringBuilder builder = new StringBuilder();
                //for (int i = count; i > 0; i--)
                //{
                //    if (str.ToString().Contains(i.ToString()))
                //    {
                //        str = str.Replace(i.ToString(), "[" + i.ToString() + "] = [" + i.ToString() + "]");
                //    }
                //}
                sQLValue = sQLValue + ret.ToString();
                ProcessRuleBLL bll = new ProcessRuleBLL();
                string str4 = bll.ValidateExpression(sQLValue);
                if (str4.Length == 0)
                {
                    str4 = "true";
                }
                builder.Append(str4);
                return builder;
            }
            return null;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}