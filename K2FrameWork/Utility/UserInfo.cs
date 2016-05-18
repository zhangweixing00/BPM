using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.Configuration;
using System.Configuration;

namespace Utility
{
    public class UserInfo
    {
        private static Hashtable htUsers;
        public static string ConnectionString = General.GetConstValue("ConnectionString");//ConfigurationManager.AppSettings["K2CommonConn"];

        public static string GetUserName(string adAccount)
        {
            if (htUsers == null)
            {
                htUsers = new Hashtable();
            }

            if (htUsers[adAccount] != null)
                return htUsers[adAccount].ToString();

            string name = LoadUserName(adAccount);

            if (string.IsNullOrEmpty(name))
                return adAccount;

            htUsers.Add(adAccount, name);
            return name;
        }

        public static bool IsAdmin(string adaccount)
        {
            string sql = string.Format("select count(*) from DelegationAdmin where AdAccount = N'{0}'", adaccount);
            int count = Convert.ToInt32(SQLHelper.ExecuteScalar(ConnectionString, sql));
            
            return count > 0;
        }

        private static string LoadUserName(string adAccount)
        {           
            string sql = string.Format("SELECT Name FROM UserInfo WHERE Account = '{0}'", adAccount);

            object name = SQLHelper.ExecuteScalar(ConnectionString, System.Data.CommandType.Text, sql);
           if (name != null)
               return name.ToString();

           return "";
        }
    }
}
