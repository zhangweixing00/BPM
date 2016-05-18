using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.Configuration;

namespace Utility
{
    public class ADHelper
    {
        private static string ADPath = General.GetConstValue("ADPath"); //ConfigurationManager.AppSettings["ADPath"];
        private static string ADUser = General.GetConstValue("ADUser");//ConfigurationManager.AppSettings["ADUser"];
        private static string ADPass = General.GetConstValue("ADPass");//ConfigurationManager.AppSettings["ADPass"];
        private static string Domain = General.GetConstValue("Domain");//ConfigurationManager.AppSettings["Domain"];

        public static string GetMemberOfByAccount(string account)
        {
            if (account.IndexOf("\\") > 0)
            {
                account = account.Substring(account.IndexOf("\\") + 1, account.Length - account.IndexOf("\\") - 1);
            }

            DirectoryEntry entry = new DirectoryEntry(ADPath, ADUser, ADPass);
            DirectorySearcher mySearcher = new DirectorySearcher(entry);

            mySearcher.Filter = string.Format("(&(objectClass=user)(sAMAccountName={0})) ", account);
            mySearcher.PropertiesToLoad.Add("memberof");

            string retValue = "";
            entry = mySearcher.FindOne().GetDirectoryEntry();
            for (int i = 0; i < entry.Properties["memberof"].Count; i++)
            {
                string dn = (string)entry.Properties["memberof"][i];
                retValue += "'k2:" + Domain + "\\" + dn.Substring(3, dn.IndexOf(",") - 3) + "',";
            }

            if ((retValue != "") && (retValue.LastIndexOf(',') == (retValue.Length - 1)))
                return retValue.Remove(retValue.Length - 1);

            if (retValue == "")
                retValue = "''";

            return retValue;
        }

        public static string GetUserAccountWithoutDomain(string account)
        {
            if (account.IndexOf("\\") > 0)
            {
                return account.Substring(account.IndexOf("\\") + 1, account.Length - account.IndexOf("\\") - 1);  
            }

            return account;
        }
    }
}
