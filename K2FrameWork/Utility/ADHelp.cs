using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.DirectoryServices;
using System.Text;

namespace Utility
{
    public class adUserInfo
    {
        /// <summary>
        /// 中文名称，如果有重名的，后面加（部门名称）
        /// </summary>
        public string cn { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string sn { get; set; }

        /// <summary>
        /// 名字，不包括姓
        /// </summary>
        public string givenName { get; set; }

        /// <summary>
        /// 大部门\中部门
        /// </summary>
        public string department { get; set; }

        /// <summary>
        /// 存放员工编号，如：BJ4523
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 姓名拼音，首字母大写，如：Jin XiaoGuang
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// 职级，如：2A
        /// </summary>
        public string employeeID { get; set; }

        /// <summary>
        /// 存放员工编号，如：BJ4523
        /// </summary>
        public string employeeNumber { get; set; }

        /// <summary>
        /// 如：BJ-BEIJING
        /// </summary>
        public string physicalDeliveryOfficeName { get; set; }

        /// <summary>
        /// AD帐号
        /// </summary>
        public string sAMAccountName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 如：+861062728073
        /// </summary>
        public string telephoneNumber { get; set; }

        /// <summary>
        /// 如：编辑、工程师等
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 汇报关系的员工号，如:BJ5678
        /// </summary>
        public string manager { get; set; }

        /// <summary>
        /// 如：jinxiaoguang@sohu-inc.com
        /// </summary>
        public string userPrincipalName { get; set; }

        /// <summary>
        /// 一般情况下等于 sAMAccountName
        /// </summary>
        public string mailNickname { get; set; }
    }

    public class ADHelp
    {
        /// 
        /// 域名
        /// 
        private static string DomainName = ConfigurationManager.AppSettings["DomainNameAD"].ToString();
        static string _adPath = ConfigurationManager.AppSettings["ADPath"].ToString();
        static string _adAccount = ConfigurationManager.AppSettings["ADAccadmin"].ToString();
        static string _adPassword = ConfigurationManager.AppSettings["ADPWD"].ToString();

        ///
        /// 扮演类实例
        ///
        private static IdentityImpersonation impersonate = new IdentityImpersonation(
           _adAccount,
            _adPassword,
            _adPath);

        /// 
        /// 用户登录验证结果
        ///
        public enum LoginResult
        {

            /// 
            /// 正常登录
            /// 

            LOGIN_USER_OK = 0,

            /// 
            /// 用户不存在
            /// 

            LOGIN_USER_DOESNT_EXIST,

            /// 
            /// 用户帐号被禁用
            /// 
            LOGIN_USER_ACCOUNT_INACTIVE,

            /// 
            /// 用户密码不正确
            /// 

            LOGIN_USER_PASSWORD_INCORRECT

        }



        /// 
        /// 用户属性定义标志
        /// 

        public enum ADS_USER_FLAG_ENUM
        {

            /// 

            /// 登录脚本标志。如果通过 ADSI LDAP 进行读或写操作时，该标志失效。如果通过 ADSI WINNT，该标志为只读。

            /// 

            ADS_UF_SCRIPT = 0X0001,

            /// 

            /// 用户帐号禁用标志

            /// 

            ADS_UF_ACCOUNTDISABLE = 0X0002,

            /// 

            /// 主文件夹标志

            /// 

            ADS_UF_HOMEDIR_REQUIRED = 0X0008,

            /// 

            /// 过期标志

            /// 

            ADS_UF_LOCKOUT = 0X0010,

            /// 

            /// 用户密码不是必须的

            /// 

            ADS_UF_PASSWD_NOTREQD = 0X0020,

            /// 

            /// 密码不能更改标志

            /// 

            ADS_UF_PASSWD_CANT_CHANGE = 0X0040,

            /// 

            /// 使用可逆的加密保存密码

            /// 

            ADS_UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0X0080,

            /// 

            /// 本地帐号标志

            /// 

            ADS_UF_TEMP_DUPLICATE_ACCOUNT = 0X0100,

            /// 

            /// 普通用户的默认帐号类型

            /// 

            ADS_UF_NORMAL_ACCOUNT = 0X0200,

            /// 

            /// 跨域的信任帐号标志

            /// 

            ADS_UF_INTERDOMAIN_TRUST_ACCOUNT = 0X0800,

            /// 

            /// 工作站信任帐号标志

            /// 

            ADS_UF_WORKSTATION_TRUST_ACCOUNT = 0x1000,

            /// 

            /// 服务器信任帐号标志

            /// 

            ADS_UF_SERVER_TRUST_ACCOUNT = 0X2000,

            /// 

            /// 密码永不过期标志

            /// 

            ADS_UF_DONT_EXPIRE_PASSWD = 0X10000,

            /// 

            /// MNS 帐号标志

            /// 

            ADS_UF_MNS_LOGON_ACCOUNT = 0X20000,

            /// 

            /// 交互式登录必须使用智能卡

            /// 

            ADS_UF_SMARTCARD_REQUIRED = 0X40000,

            /// 

            /// 当设置该标志时，服务帐号（用户或计算机帐号）将通过 Kerberos 委托信任

            /// 

            ADS_UF_TRUSTED_FOR_DELEGATION = 0X80000,

            /// 

            /// 当设置该标志时，即使服务帐号是通过 Kerberos 委托信任的，敏感帐号不能被委托

            /// 

            ADS_UF_NOT_DELEGATED = 0X100000,

            /// 

            /// 此帐号需要 DES 加密类型

            /// 

            ADS_UF_USE_DES_KEY_ONLY = 0X200000,

            /// 

            /// 不要进行 Kerberos 预身份验证

            /// 

            ADS_UF_DONT_REQUIRE_PREAUTH = 0X4000000,

            /// 

            /// 用户密码过期标志

            /// 

            ADS_UF_PASSWORD_EXPIRED = 0X800000,

            /// 

            /// 用户帐号可委托标志

            /// 

            ADS_UF_TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 0X1000000

        }


        public static DirectoryEntry GetContainer()
        {
            string strLdap = "LDAP://";
            string loginServerName = Environment.GetEnvironmentVariable("LOGONSERVER", EnvironmentVariableTarget.User);
            if (string.IsNullOrEmpty(loginServerName) == false)
            {
                loginServerName = loginServerName.Replace(@"\\", "");
                _adPath = strLdap + loginServerName + "/DC=" + DomainName + ",DC=COM";
            }
            DirectoryEntry de3 = new DirectoryEntry(_adPath, _adAccount, _adPassword, AuthenticationTypes.Secure);
            /***   String str = de3.Properties["defaultNamingContext"][0].ToString();
               DirectoryEntry entry = new DirectoryEntry("LDAP://" + str);
                return entry;
             * 
             * ***/
            return de3;
        }

        public static DirectoryEntry FindEntry(DirectoryEntry root, string sAMAccountName, Enums.EntryType entryType)
        {
            DirectoryEntry entry = null;
            DirectorySearcher searcher = new DirectorySearcher(root);
            searcher.Filter = "(&(objectClass=" + entryType.ToString().ToLower() + ")(sAMAccountName=" + sAMAccountName + "))";
            searcher.SearchScope = SearchScope.Subtree;

            try
            {
                SearchResult result = searcher.FindOne();
                if (result == null)
                    throw new Exception(string.Format("Can not find {0} : {1}", entryType.ToString().ToLower(), sAMAccountName));
                entry = result.GetDirectoryEntry();

            }
            catch (Exception ex)
            {
                return null;
            }
            return entry;
        }


        public static bool IsEntryExist(string sAMAccountName, Enums.EntryType entryType)
        {
            DirectorySearcher searcher = new DirectorySearcher(GetContainer());
            searcher.Filter = "(&(objectClass=" + entryType.ToString().ToLower() + ")(sAMAccountName=" + sAMAccountName + "))";//objectclass
            searcher.SearchScope = SearchScope.Subtree;
            SearchResult result = searcher.FindOne();
            searcher.Dispose();
            return result != null;
        }


        /// <summary>
        /// 读取用户
        /// </summary>
        /// <param name="schemaClassNameToSearch"></param>
        /// <returns></returns>
        public static DataTable GetUserList(Enums.EntryType schemaClassNameToSearch)
        {
            SearchResultCollection results = ExecuteAD(schemaClassNameToSearch);
            DataTable dt = GetUserList(results);
            results.Dispose();
            return dt;
        }


        /// <summary>
        /// 从AD中读取用户
        /// </summary>
        /// <param name="schemaClassNameToSearch"></param>
        /// <returns></returns>
        public static SearchResultCollection ExecuteAD(Enums.EntryType schemaClassNameToSearch)
        {
            DirectorySearcher searcher = new DirectorySearcher();
            /****
                  DirectoryEntry de3 = new DirectoryEntry(_adPath);
                  String str =de3.Properties ["defaultNamingContext"][0].ToString ();
            ***/
            DirectoryEntry de4 = GetContainer();
            searcher = new DirectorySearcher(de4);
            DirectorySearcher searcher2 = new DirectorySearcher(de4);
            // searcher.SearchRoot = new DirectoryEntry(_adPath, _adAccount, _adPassword);
            searcher.Filter = "(objectClass=" + schemaClassNameToSearch.ToString() + ")";
            searcher.SearchScope = SearchScope.Subtree;
            searcher.Sort = new SortOption("name", System.DirectoryServices.SortDirection.Ascending);
            searcher.PageSize = 512;

            //指对范围内的属性进行加载，以提高效率
            searcher.PropertiesToLoad.AddRange(new string[] { "name", "Path", "displayname", "samaccountname", "mail", "Comment", "memberOf" });
            SearchResultCollection results = searcher.FindAll();
            return results;
        }



        /// <summary>
        /// 读取用户
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static DataTable GetUserList(SearchResultCollection results)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Row", Type.GetType("System.String"));
            dt.Columns.Add("UserName", Type.GetType("System.String"));
            dt.Columns.Add("UserEmail", Type.GetType("System.String"));
            dt.Columns.Add("Depart", Type.GetType("System.String"));
            dt.Columns.Add("Group", Type.GetType("System.String"));
            dt.Columns.Add("isGroup", Type.GetType("System.String"));
            int row = 1;
            if (results.Count == 0)
                throw new Exception("域中没有任何用户");
            else
            {
                foreach (SearchResult result in results)
                {
                    string adPath = result.Path;
                    if (adPath.IndexOf("/") < 0)
                        continue;
                    //  DirectoryEntry entry = result.GetDirectoryEntry();

                    try
                    {


                        DataRow dr = dt.NewRow();

                        if (result.Properties["name"].Count > 0)
                            dr["username"] = result.Properties["name"][0].ToString();
                        //if (entry.Properties["samaccountname"].Count > 0)
                        //    dr["samaccountname"] = entry.Properties["sAMAccountName"][0].ToString();
                        //if (entry.Properties["displayname"].Count > 0)
                        //    dr["displayname"] = entry.Properties["displayname"][0].ToString();
                        //邮箱为空的时候不添加到列表中
                        if (result.Properties["mail"].Count > 0)
                        {
                            dr["UserEmail"] = result.Properties["mail"][0].ToString();
                        }
                        else
                        {
                            continue;
                        }
                        if (result.Properties["memberOf"].Count > 0)
                        {
                            dr["Group"] = GetOuByPath(result.Properties["memberOf"][0].ToString(), "cn");
                        }
                        //else
                        //{
                        //    continue;
                        //}

                        if (result.Properties["objectClass"].Count > 0 && result.Properties["objectClass"].Contains("group"))//邮件组
                        {
                            dr["isGroup"] = "true";
                        }
                        else
                        {
                            dr["isGroup"] = "false";
                        }
                        dr["Depart"] = GetOuByPath(result.Path, "ou");
                        // dr["Group"] = result.Properties["memberOf"][0].ToString();
                        dr["row"] = row.ToString(); //行
                        row++;
                        dt.Rows.Add(dr);
                    }
                    catch (Exception)
                    {

                        continue;
                    }

                }
            }
            return dt;
        }

        public static string GetOuByPath(string path, string part)
        {
            string[] dns = path.Split(',');
            //string part1 = part.ToLower();
            //string part2 = part.ToUpper();
            //var data = dns.Where(c => c.StartsWith(part.ToUpper()) || c.StartsWith(part.ToLower()));

            string ou = string.Empty;
            //foreach (var item in data.ToArray())
            //{
            //    ou += item.Split('=')[1] + ";";
            //}
            return ou;
        }

        /// <summary>
        /// 得到组织列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOUList()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));

            DirectorySearcher searcher = new DirectorySearcher();
            searcher.SearchRoot = new DirectoryEntry(_adPath, _adAccount, _adPassword);
            searcher.Filter = "(objectClass=organizationalUnit)";
            searcher.SearchScope = SearchScope.Subtree;
            searcher.Sort = new SortOption("name", System.DirectoryServices.SortDirection.Ascending);
            searcher.PageSize = 512;

            SearchResultCollection results = searcher.FindAll();


            foreach (SearchResult result in results)
            {
                string adPath = result.Path;
                if (adPath.IndexOf("/") < 0)
                    continue;
                DirectoryEntry entry = result.GetDirectoryEntry();
                if (entry != null)
                {
                    DataRow dr = dt.NewRow();
                    if (entry.Properties["name"].Count > 0)
                        dr["name"] = entry.Properties["name"][0].ToString();

                    dt.Rows.Add(dr);
                }
            }

            return dt;

        }

        /// <summary>
        /// 根据用户得到组
        /// </summary>
        /// <param name="de"></param>
        /// <returns></returns>
        public static string GetGroups(DirectoryEntry de)
        {
            DirectorySearcher search = new DirectorySearcher(de);
            // search.Filter = "(name=Test3333)";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();
            try
            {
                SearchResult result = search.FindOne();

                int propertyCount = result.Properties["memberOf"].Count;

                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();

        }

        /// <summary>
        /// 创建AD账号
        /// </summary>
        /// <param name="ouName">如：BJ-OFFICE\FOCUS\USERS</param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static bool CreateNewUser(string ouName, adUserInfo userInfo, out string returnInfo)
        {
            returnInfo = "";
            try
            {
                DirectoryEntry entry = GetContainer();
                DirectoryEntry subEntry = Get_Create_OU(entry, ouName);

                DirectoryEntry deUser = subEntry.Children.Add("CN=" + userInfo.cn, "user");
                SetUserInfo(userInfo, deUser);
                deUser.CommitChanges();

                deUser.Invoke("SetPassword", new object[] { userInfo.password });
                ADHelp.EnableUser(deUser);

                deUser.Close();
                return true;
            }
            catch (Exception ex)
            {
                returnInfo = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 更新AD账号
        /// </summary>
        /// <param name="ouName">如：BJ-OFFICE\FOCUS\USERS</param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static bool UpdateAdUser(string ouName, adUserInfo userInfo, out string returnInfo)
        {
            returnInfo = "";
            try
            {
                DirectoryEntry deUser = GetDirectoryEntry(userInfo.sAMAccountName);
                if (deUser == null)
                {
                    returnInfo = "更新用户时，没找到用户:" + userInfo.sAMAccountName;
                    return false;
                }
                //deUser.Properties["name"].Value = userInfo.cn;
                deUser.Properties["sn"].Value = userInfo.sn ?? "";
                deUser.Properties["givenName"].Value = userInfo.givenName ?? "";
                deUser.Properties["department"].Value = userInfo.department;
                deUser.Properties["description"].Value = userInfo.description ?? "";
                //deUser.Properties["displayName"].Value = userInfo.displayName ?? "";
                deUser.Properties["employeeID"].Value = userInfo.employeeID ?? "";
                deUser.Properties["employeeNumber"].Value = userInfo.employeeNumber ?? "";
                deUser.Properties["physicalDeliveryOfficeName"].Value = userInfo.physicalDeliveryOfficeName ?? "";
                deUser.Properties["sAMAccountName"].Value = userInfo.sAMAccountName;
                deUser.Properties["telephoneNumber"].Value = userInfo.telephoneNumber ?? "";
                deUser.Properties["title"].Value = userInfo.title ?? "";

                DirectoryEntry userManagerEntry = GetUserEntryByEmployeeNumber(userInfo.manager);
                if (userManagerEntry != null)
                {
                    deUser.Properties["manager"].Value = userManagerEntry.Properties["distinguishedName"].Value;
                }

                deUser.Properties["userPrincipalName"].Value = userInfo.userPrincipalName ?? "";
                deUser.CommitChanges();

                deUser.Close();
                return true;
            }
            catch (Exception ex)
            {
                returnInfo = ex.Message;
                return false;
            }
        }

        private static void SetUserInfo(adUserInfo userInfo, DirectoryEntry deUser)
        {
            deUser.Properties["name"].Value = userInfo.cn;
            deUser.Properties["sn"].Value = userInfo.sn ?? "";
            deUser.Properties["givenName"].Value = userInfo.givenName ?? "";
            deUser.Properties["department"].Value = userInfo.department;
            deUser.Properties["description"].Value = userInfo.description ?? "";
            deUser.Properties["displayName"].Value = userInfo.displayName ?? "";
            deUser.Properties["employeeID"].Value = userInfo.employeeID ?? "";
            deUser.Properties["employeeNumber"].Value = userInfo.employeeNumber ?? "";
            deUser.Properties["physicalDeliveryOfficeName"].Value = userInfo.physicalDeliveryOfficeName ?? "";
            deUser.Properties["sAMAccountName"].Value = userInfo.sAMAccountName;
            deUser.Properties["telephoneNumber"].Value = userInfo.telephoneNumber ?? "";
            deUser.Properties["title"].Value = userInfo.title ?? "";

            DirectoryEntry userManagerEntry = GetUserEntryByEmployeeNumber(userInfo.manager);
            if (userManagerEntry != null)
            {
                deUser.Properties["manager"].Value = userManagerEntry.Properties["distinguishedName"].Value;
            }

            deUser.Properties["userPrincipalName"].Value = userInfo.userPrincipalName ?? "";
        }
        /// <summary>
        /// 创建AD账号
        /// </summary>
        /// <param name="ouName">例如：BJ-OFFICE\FOCUS\USERS</param>
        /// <param name="cnName"></param>
        /// <param name="sAMAccountName">帐号（不包括@）</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool CreateNewUser(string ouName, string cnName, string sAMAccountName, string password)
        {
            try
            {
                DirectoryEntry entry = GetContainer();
                DirectoryEntry subEntry = Get_Create_OU(entry, ouName);// entry.Children.Find(ldapDN);
                DirectoryEntry deUser = subEntry.Children.Add("CN=" + cnName, "user");
                deUser.Properties["sAMAccountName"].Value = sAMAccountName;
                deUser.CommitChanges();

                deUser.Invoke("SetPassword", new object[] { password });
                ADHelp.EnableUser(deUser);

                deUser.Close();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ouName"></param>
        /// <returns></returns>
        public static DirectoryEntry CreateOU(string ouName)
        {
            //if (GetDirectoryEntryOfOU(ouName) == null)
            {
                DirectoryEntry entry = GetContainer();
                DirectoryEntry OU = entry.Children.Add("OU=" + ouName, "organizationalUnit");
                OU.CommitChanges();
                return OU;
            }
        }

        public static DirectoryEntry CreateOU(DirectoryEntry container, string ouName)
        {
            //if (GetDirectoryEntryOfOU(ouName) == null)
            {
                DirectoryEntry entry = container;
                DirectoryEntry OU = entry.Children.Add("OU=" + ouName, "organizationalUnit");
                OU.CommitChanges();
                return OU;
            }
        }

        /// <summary>
        /// 创建AD账号(在Users下面)
        /// </summary>
        /// <param name="cnName"></param>
        /// <param name="sAMAccountName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CreateNewUser(string cnName, string sAMAccountName, string password)
        {
            return CreateNewUser("CN=Users", cnName, sAMAccountName, password);
        }

        /// <summary>
        /// AD中是否存在该登录名的用户
        /// </summary>
        /// <param name="sAMAccountName">登录名</param>
        /// <returns></returns>
        public static bool IsAccountNameExists(string sAMAccountName)
        {
            DirectoryEntry de = GetContainer();

            DirectorySearcher deSearch = new DirectorySearcher(de);

            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + sAMAccountName + "))";       // LDAP 查询串

            SearchResult results = deSearch.FindOne();

            if (results == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// AD中是否存在该中文名称的用户
        /// </summary>
        /// <param name="cnName"></param>
        /// <returns></returns>
        public static bool IsUserCnNameExists(string cnName)
        {
            DirectoryEntry de = GetContainer();

            DirectorySearcher deSearch = new DirectorySearcher(de);

            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(cn=" + cnName + "))";       // LDAP 查询串

            SearchResultCollection results = deSearch.FindAll();

            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 该用户是否有邮箱，禁用的邮箱也返回false
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <returns></returns>
        public static bool IsExistMailBox(string sAMAccountName)
        {
            if (string.IsNullOrEmpty(sAMAccountName)) return false;
            try
            {
                DirectoryEntry entry = GetContainer();
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + sAMAccountName + "))";
                SearchResult SearchResult = search.FindOne();
                if (SearchResult == null)
                {
                    if (entry != null)
                    {
                        entry.Close();
                        return false;
                    }
                }
                else
                {
                    if (SearchResult.Properties["mail"].Count > 0 && SearchResult.Properties["homeMDB"].Count > 0) //是否已经有邮箱(禁用的没考虑) 
                    {
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
            return false;
        }

        /// 判断用户帐号是否激活

        /// 用户帐号属性控制器

        /// 如果用户帐号已经激活，返回 true；否则返回 false

        public static bool IsAccountActive(int userAccountControl)
        {

            int userAccountControl_Disabled = Convert.ToInt32(ADS_USER_FLAG_ENUM.ADS_UF_ACCOUNTDISABLE);

            int flagExists = userAccountControl & userAccountControl_Disabled;



            if (flagExists > 0)

                return false;

            else

                return true;

        }



        /// 判断用户与密码是否足够以满足身份验证进而登录

        /// 用户公共名称

        /// 密码

        /// 如能可正常登录，则返回 true；否则返回 false

        public static LoginResult Login(string sAMAccountName, string password)
        {

            DirectoryEntry de = GetDirectoryEntry(sAMAccountName);



            if (de != null)
            {

                // 必须在判断用户密码正确前，对帐号激活属性进行判断；否则将出现异常。

                int userAccountControl = Convert.ToInt32(de.Properties["userAccountControl"][0]);

                de.Close();



                if (!IsAccountActive(userAccountControl))

                    return LoginResult.LOGIN_USER_ACCOUNT_INACTIVE;



                if (GetDirectoryEntry(sAMAccountName, password) != null)

                    return LoginResult.LOGIN_USER_OK;

                else

                    return LoginResult.LOGIN_USER_PASSWORD_INCORRECT;

            }

            else
            {

                return LoginResult.LOGIN_USER_DOESNT_EXIST;

            }

        }



        /// 判断用户帐号与密码是否足够以满足身份验证进而登录

        /// 用户帐号

        /// 密码

        /// 如能可正常登录，则返回 true；否则返回 false

        public static LoginResult LoginByAccount(string sAMAccountName, string password)
        {

            DirectoryEntry de = GetDirectoryEntryByAccount(sAMAccountName);



            if (de != null)
            {

                // 必须在判断用户密码正确前，对帐号激活属性进行判断；否则将出现异常。

                int userAccountControl = Convert.ToInt32(de.Properties["userAccountControl"][0]);

                de.Close();



                if (!IsAccountActive(userAccountControl))

                    return LoginResult.LOGIN_USER_ACCOUNT_INACTIVE;



                if (GetDirectoryEntryByAccount(sAMAccountName, password) != null)

                    return LoginResult.LOGIN_USER_OK;

                else

                    return LoginResult.LOGIN_USER_PASSWORD_INCORRECT;

            }

            else
            {

                return LoginResult.LOGIN_USER_DOESNT_EXIST;

            }

        }



        /// 设置用户密码，管理员可以通过它来修改指定用户的密码。
        /// 用户公共名称
        /// 用户新密码
        public static void SetPassword(string sAMAccountName, string newPassword)
        {

            DirectoryEntry de = GetDirectoryEntry(sAMAccountName);



            // 模拟超级管理员，以达到有权限修改用户密码

            //  impersonate.BeginImpersonate();

            de.Invoke("SetPassword", new object[] { newPassword });

            // impersonate.StopImpersonate();



            de.Close();

        }



        /// 设置帐号密码，管理员可以通过它来修改指定帐号的密码。
        /// 用户帐号
        /// 用户新密码
        public static void SetPasswordByAccount(string sAMAccountName, string newPassword)
        {
            DirectoryEntry de = GetDirectoryEntryByAccount(sAMAccountName);

            // 模拟超级管理员，以达到有权限修改用户密码
            IdentityImpersonation impersonate = new IdentityImpersonation(_adAccount, _adPassword, DomainName);

            impersonate.BeginImpersonate();

            de.Invoke("SetPassword", new object[] { newPassword });

            impersonate.StopImpersonate();
            de.Close();
        }


        /// 修改用户密码
        /// 用户公共名称
        /// 旧密码
        /// 新密码
        public static void ChangeUserPassword(string sAMAccountName, string oldPassword, string newPassword)
        {

            // to-do: 需要解决密码策略问题

            DirectoryEntry oUser = GetDirectoryEntry(sAMAccountName);

            oUser.Invoke("ChangePassword", new Object[] { oldPassword, newPassword });

            oUser.Close();

        }



        /// 启用指定公共名称的用户
        /// 用户公共名称
        public static void EnableUser(string sAMAccountName)
        {
            EnableUser(GetDirectoryEntry(sAMAccountName));
        }



        /// 启用指定 的用户
        public static void EnableUser(DirectoryEntry de)
        {

            //impersonate.BeginImpersonate();

            de.Properties["userAccountControl"][0] = ADHelp.ADS_USER_FLAG_ENUM.ADS_UF_NORMAL_ACCOUNT;// | ADHelper.ADS_USER_FLAG_ENUM.ADS_UF_DONT_EXPIRE_PASSWD;

            de.CommitChanges();

            // impersonate.StopImpersonate();

            de.Close();

        }



        /// 禁用指定公共名称的用户
        /// 用户公共名称
        public static void DisableUser(string sAMAccountName)
        {
            DisableUser(GetDirectoryEntry(sAMAccountName));

        }


        /// 禁用指定 的用户
        public static void DisableUser(DirectoryEntry de)
        {
            if (de == null) return;
            //impersonate.BeginImpersonate();

            de.Properties["userAccountControl"][0] = ADHelp.ADS_USER_FLAG_ENUM.ADS_UF_NORMAL_ACCOUNT | ADHelp.ADS_USER_FLAG_ENUM.ADS_UF_DONT_EXPIRE_PASSWD | ADHelp.ADS_USER_FLAG_ENUM.ADS_UF_ACCOUNTDISABLE;

            de.CommitChanges();

            //impersonate.StopImpersonate();

            de.Close();

        }



        ///// <summary>
        ///// 将指定的用户添加到指定的组中。默认为 Users 下的组和用户。
        ///// </summary>
        ///// <param name="sAMAccountName">用户帐号</param>
        ///// <param name="ouName">可能为多级，如"BJ-OFFICE\FOCUS\USRES"</param>
        ///// <param name="isCreate_NotExistOU">是否创建不存在的OU</param>
        //public static bool AddUserToOU(string sAMAccountName, string ouName,bool isCreate_NotExistOU)
        //{
        //    if(string.IsNullOrEmpty(ouName)) return false;

        //    DirectoryEntry entry = Get_Create_OU(GetContainer(), ouName);

        //    DirectoryEntry oUser = GetDirectoryEntry(sAMAccountName);

        //    //impersonate.BeginImpersonate();

        //    entry.Children.Add(sAMAccountName, "user");

        //    entry.CommitChanges();

        //    //impersonate.StopImpersonate();

        //    entry.Close();

        //    oUser.Close();
        //    return true;
        //}

        public static DirectoryEntry Get_Create_OU(DirectoryEntry entry, string ouName)
        {
            string[] arrOU = ouName.Split('\\');
            foreach (string strOu in arrOU)
            {
                DirectoryEntry entryFind = GetDirectoryEntryOfOU(entry, strOu);
                if (entryFind == null)
                {
                    entry = CreateOU(entry, strOu);
                }
                else
                {
                    entry = entryFind;
                }
            }
            return entry;
        }

        public static void RemoveUserFromOU(string sAMAccountName, string ouName)
        {
            DirectoryEntry ou = GetDirectoryEntryOfGroup(ouName);

            DirectoryEntry oUser = GetDirectoryEntry(sAMAccountName);

            impersonate.BeginImpersonate();

            ou.Properties["member"].Remove(oUser.Properties["distinguishedName"].Value);

            ou.CommitChanges();

            impersonate.StopImpersonate();

            ou.Close();

            oUser.Close();
        }


        /// <summary>
        /// 把一个用户添加到多个邮件组中
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <param name="mailGroups">多个邮件组用";"分隔</param>
        public static string AddUserToGroups(string sAMAccountName, string mailGroups)
        {
            string errorInfo = "";
            if (!string.IsNullOrEmpty(mailGroups))
            {
                string[] arrMG = mailGroups.Split(';');
                foreach (string mg in arrMG)
                {
                    if (string.IsNullOrEmpty(mg) == false)
                    {
                        try
                        {
                            ADHelp.AddUserToGroup(sAMAccountName, mg);
                        }
                        catch (Exception ex)
                        {
                            errorInfo += "帐号 '" + sAMAccountName + "' 加入邮件组 '" + mg + "' 出错：" + ex.Message + "\r\n";
                            continue;
                        }
                    }
                }
            }
            return errorInfo;
        }

        /// <summary>
        /// 将指定的用户添加到指定的组中。默认为 Users 下的组和用户。
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <param name="groupName"></param>
        public static void AddUserToGroup(string sAMAccountName, string groupName)
        {
            if (string.IsNullOrEmpty(sAMAccountName) || string.IsNullOrEmpty(groupName))
            {
                return;
            }
            DirectoryEntry oGroup = GetDirectoryEntryOfGroup(groupName);
            DirectoryEntry oUser = GetDirectoryEntry(sAMAccountName);

            if (oGroup == null || oUser == null) return;

            if (oGroup.Properties["member"].Contains(oUser.Properties["distinguishedName"].Value) == false)
            {
                oGroup.Properties["member"].Add(oUser.Properties["distinguishedName"].Value);
                oGroup.CommitChanges();
            }

            oGroup.Close();
            oUser.Close();
        }

        /// <summary>
        /// 移动用户
        /// </summary>
        /// <param name="NewUser"></param>
        /// <param name="returnErrInfo"></param>
        /// <returns></returns>
        public static bool MoveUser(DirectoryEntry NewUser, string ouName, out string returnErrInfo)
        {
            returnErrInfo = "";
            try
            {
                DirectoryEntry ouDe = Get_Create_OU(GetContainer(), ouName);
                if (ouDe != null)
                {
                    NewUser.MoveTo(ouDe);
                }
                else
                {
                    returnErrInfo = "不存在OU:" + ouName;
                    return false;
                }
            }
            catch (Exception ex)
            {
                returnErrInfo = ex.Message;
                return false;
            }
            return true;
        }


        /// 将用户从指定组中移除。默认为 Users 下的组和用户。
        /// 用户公共名称
        /// 组名
        public static void RemoveUserFromGroup(string sAMAccountName, string groupName)
        {

            DirectoryEntry oGroup = GetDirectoryEntryOfGroup(groupName);

            DirectoryEntry oUser = GetDirectoryEntry(sAMAccountName);



            impersonate.BeginImpersonate();

            oGroup.Properties["member"].Remove(oUser.Properties["distinguishedName"].Value);

            oGroup.CommitChanges();

            impersonate.StopImpersonate();


            oGroup.Close();

            oUser.Close();

        }

        /// <summary>
        /// 将用户从指定组中移除。默认为 Users 下的组和用户。
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <param name="groupNames">多个组名用";"分割</param>
        public static void RemoveUserFromGroups(string sAMAccountName, string groupNames)
        {
            if (string.IsNullOrEmpty(sAMAccountName) || string.IsNullOrEmpty(groupNames)) return;

            DirectoryEntry oUser = GetDirectoryEntry(sAMAccountName);
            if (oUser == null) return;


            string[] arrGroupNames = groupNames.Split(';');
            foreach (string groupName in arrGroupNames)
            {
                if (string.IsNullOrEmpty(groupName)) continue;

                DirectoryEntry oGroup = GetDirectoryEntryOfGroup(groupName);
                if (oGroup == null) continue;

                if (oGroup.Properties["member"].Contains(oUser.Properties["distinguishedName"].Value))
                {
                    oGroup.Properties["member"].Remove(oUser.Properties["distinguishedName"].Value);
                    oGroup.CommitChanges();
                }
                oGroup.Close();
            }
            oUser.Close();
        }
        /// <summary>
        /// 把该帐号移出其所在的所有邮件组
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <param name="returnErrInfo"></param>
        public static bool RemoveUserFromGroups(string sAMAccountName, out string returnErrInfo)
        {
            returnErrInfo = "";
            if (string.IsNullOrEmpty(sAMAccountName))
            {
                returnErrInfo = "帐号不能为空！";
                return false;

            }

            DirectoryEntry oUser = GetDirectoryEntry(sAMAccountName);
            if (oUser == null)
            {
                returnErrInfo = "未找到该用户：" + sAMAccountName;
                return false;
            }

            string groupNames = string.Empty;
            if (oUser.Properties["memberOf"].Count > 0)
            {
                for (int i = 0; i < oUser.Properties["memberOf"].Count; i++)
                {
                    if (oUser.Properties["memberOf"][i].ToString().Contains(" {") == false)//特殊组不处理 ,主要指 ServiceAccount\ErpCRM 下面的组
                    {
                        groupNames += GetOuByPath(oUser.Properties["memberOf"][i].ToString(), "cn");
                    }
                }
                //groupNames = groupNames.Replace("Users;", "");
                //groupNames = GetOuByPath(oUser.Properties["memberOf"][0].ToString(), "cn");
            }
            else
            {
                return true;
            }

            if (string.IsNullOrEmpty(groupNames))
            {
                returnErrInfo = ""; // "查找的邮件组为空！";
                return true;
            }


            string[] arrGroupNames = groupNames.Split(';');
            foreach (string groupName in arrGroupNames)
            {
                if (string.IsNullOrEmpty(groupName)) continue;
                try
                {
                    DirectoryEntry oGroup = GetDirectoryEntryOfGroup(groupName);
                    if (oGroup == null) continue;

                    if (oGroup.Properties["member"].Contains(oUser.Properties["distinguishedName"].Value))
                    {
                        oGroup.Properties["member"].Remove(oUser.Properties["distinguishedName"].Value);
                        oGroup.CommitChanges();
                    }
                    oGroup.Close();
                }
                catch (Exception ex)
                {
                    returnErrInfo += "\r\n移出组：" + groupName + " 出错：" + ex.Message;
                }
            }
            oUser.Close();

            if (string.IsNullOrEmpty(returnErrInfo) == false)
            {
                return false;
            }
            return true;
        }

        #region GetDirectoryObject

        /// 
        /// 获得DirectoryEntry对象实例,以管理员登陆AD
        /// 




        /// 根据指定用户名和密码获得相应DirectoryEntry实体
        private static DirectoryEntry GetDirectoryObject(string userName, string password)
        {

            DirectoryEntry entry = new DirectoryEntry(_adPath, userName, password, AuthenticationTypes.None);

            return entry;

        }



        /// 
        /// i.e. /CN=Users,DC=creditsights, DC=cyberelves, DC=Com
        /// 
        private static DirectoryEntry GetDirectoryObject(string domainReference)
        {

            DirectoryEntry entry = new DirectoryEntry(_adPath + domainReference, _adAccount, _adPassword, AuthenticationTypes.Secure);

            return entry;

        }



        /// 
        /// 获得以UserName,Password创建的DirectoryEntry
        /// 
        private static DirectoryEntry GetDirectoryObject(string domainReference, string userName, string password)
        {

            DirectoryEntry entry = new DirectoryEntry(_adPath + domainReference, userName, password, AuthenticationTypes.Secure);

            return entry;

        }



        #endregion


        #region GetDirectoryEntry
        /// <summary>
        /// 根据用户公共名称取得用户的对象.用户公共名称,如果找到该用户，则返回用户的 对象；否则返回 null
        /// </summary>
        /// <param name="cnName">cn</param>
        /// <returns></returns>
        public static DirectoryEntry GetDirectoryEntry(string sAMAccountName)
        {
            DirectoryEntry de = GetContainer();

            DirectorySearcher deSearch = new DirectorySearcher(de);

            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + sAMAccountName + "))";

            deSearch.SearchScope = SearchScope.Subtree;

            try
            {
                SearchResult result = deSearch.FindOne();
                if (result == null) return null;
                de = GetDirectoryEntryByPath(result.Path);
                return de;
            }
            catch
            {
                return null;
            }
        }


        public static DirectoryEntry GetDirectoryEntryByPath(string path)
        {
            return new DirectoryEntry(path, _adAccount, _adPassword, AuthenticationTypes.Secure);
        }

        /// <summary>
        /// 根据用户公共名称和密码取得用户的 对象
        /// </summary>
        /// <param name="cnName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static DirectoryEntry GetDirectoryEntry(string cnName, string password)
        {

            DirectoryEntry de = GetDirectoryObject(cnName, password);

            DirectorySearcher deSearch = new DirectorySearcher(de);

            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(cn=" + cnName + "))";

            deSearch.SearchScope = SearchScope.Subtree;

            try
            {
                SearchResult result = deSearch.FindOne();

                de = new DirectoryEntry(result.Path);

                return de;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 根据用户帐号称取得用户的 对象
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <returns></returns>
        public static DirectoryEntry GetUserEntryByEmployeeNumber(string employeeNumber)
        {
            DirectoryEntry de = GetContainer();
            DirectorySearcher deSearch = new DirectorySearcher(de);
            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(employeeNumber=" + employeeNumber + "))";
            deSearch.SearchScope = SearchScope.Subtree;
            try
            {
                SearchResult result = deSearch.FindOne();
                de = new DirectoryEntry(result.Path);
                return de;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 根据用户帐号称取得用户的 对象
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <returns></returns>
        public static DirectoryEntry GetDirectoryEntryByAccount(string sAMAccountName)
        {

            DirectoryEntry de = GetContainer();

            DirectorySearcher deSearch = new DirectorySearcher(de);

            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + sAMAccountName + "))";

            deSearch.SearchScope = SearchScope.Subtree;

            try
            {

                SearchResult result = deSearch.FindOne();

                de = new DirectoryEntry(result.Path);

                return de;

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据用户帐号和密码取得用户的 对象
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static DirectoryEntry GetDirectoryEntryByAccount(string sAMAccountName, string password)
        {
            DirectoryEntry de = GetDirectoryEntryByAccount(sAMAccountName);
            if (de != null)
            {
                string cnName = de.Properties["cn"][0].ToString();
                if (GetDirectoryEntry(cnName, password) != null)
                    return GetDirectoryEntry(cnName, password);
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有的邮件组
        /// </summary>
        /// <returns></returns>
        public static SearchResultCollection GetEmailGroups()
        {
            try
            {
                DirectoryEntry de = GetContainer();

                DirectorySearcher deSearch = new DirectorySearcher(de);

                deSearch.Filter = "(&(objectClass=group)(objectCategory=group)(mail=*))"; //(&(objectClass=group)(groupType=8))

                deSearch.SearchScope = SearchScope.Subtree;

                SearchResultCollection result = deSearch.FindAll();

                return result;

            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 通过显示名称或者mail地址来模糊查询
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        public static SearchResultCollection SearchEmailsByNameOrMail(string strSearch)
        {
            if (string.IsNullOrEmpty(strSearch.Trim())) return null;
            try
            {
                DirectoryEntry de = GetContainer();

                DirectorySearcher deSearch = new DirectorySearcher(de);
                //1.2.840.113556.1.4.803 and ,1.2.840.113556.1.4.804 or
                //userAccountControl:1.2.840.113556.1.4.803:=2 所有被禁用的账户
                deSearch.Filter = "(&(|(objectCategory=person)(objectCategory=group))(!userAccountControl:1.2.840.113556.1.4.803:=2)(|(name=*" + strSearch + "*)(mail=*" + strSearch + "*)(displayName=*" + strSearch + "*)))";
                deSearch.SearchScope = SearchScope.Subtree;

                SearchResultCollection result = deSearch.FindAll();
                return result;
            }
            catch
            {
                return null;
            }
        }
        ///// <summary>
        ///// 获取所有的邮件组和邮箱
        ///// </summary>
        ///// <returns></returns>
        //public static SearchResultCollection GetEmailsAndGroups()
        //{
        //    try
        //    {
        //        DirectoryEntry de = GetContainer();

        //        DirectorySearcher deSearch = new DirectorySearcher(de);

        //        //deSearch.Filter = "(|(&(objectCategory=person)(userAccountControl=512))((&(objectClass=group)(groupType=8))))";
        //       // deSearch.Filter = "(&(objectCategory=group)(objectClass=group))";
        //        deSearch.Filter = "(|(&(objectCategory=person)(userAccountControl=512))((&(objectClass=group)(objectCategory=group)(mail=*))))";
        //        deSearch.SearchScope = SearchScope.Subtree;

        //        SearchResultCollection result = deSearch.FindAll();

        //        return result;

        //    }
        //    catch
        //    {
        //        return null;

        //    }
        //}

        /// <summary>
        /// 根据组名取得用户组的 对象
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static DirectoryEntry GetDirectoryEntryOfGroup(string groupName)
        {
            try
            {
                DirectoryEntry de = GetContainer();
                DirectorySearcher deSearch = new DirectorySearcher(de);
                deSearch.Filter = "(&(objectClass=group)(cn=" + groupName + "))";
                deSearch.SearchScope = SearchScope.Subtree;
                SearchResult result = deSearch.FindOne();
                de = new DirectoryEntry(result.Path);
                return de;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据组织单位名称取得组织单位的 对象
        /// </summary>
        /// <param name="ouName"></param>
        /// <returns></returns>
        public static DirectoryEntry GetDirectoryEntryOfOU(string ouName)
        {
            try
            {
                DirectoryEntry de = GetContainer();
                DirectorySearcher deSearch = new DirectorySearcher(de);
                deSearch.Filter = "(&(objectClass=organizationalUnit)(OU=" + ouName + "))";

                SearchResultCollection srcol = deSearch.FindAll();
                if (srcol != null && srcol.Count > 0)
                    return srcol[0].GetDirectoryEntry();
                else
                    return null;
                //deSearch.SearchScope = SearchScope.Subtree;
                //SearchResult result = deSearch.FindOne();
                //de = new DirectoryEntry(result.Path);
                //return de;
            }
            catch
            {
                return null;

            }
        }

        /// <summary>
        /// 根据组织单位名称取得组织单位的 对象
        /// </summary>
        /// <param name="ouName"></param>
        /// <returns></returns>
        public static DirectoryEntry GetDirectoryEntryOfOU(DirectoryEntry containerEntry, string ouName)
        {
            try
            {
                DirectoryEntry de = containerEntry;
                DirectorySearcher deSearch = new DirectorySearcher(de);
                deSearch.Filter = "(&(objectClass=organizationalUnit)(OU=" + ouName + "))";

                SearchResultCollection srcol = deSearch.FindAll();
                if (srcol != null && srcol.Count > 0)
                    return srcol[0].GetDirectoryEntry();
                else
                    return null;
                //deSearch.SearchScope = SearchScope.Subtree;
                //SearchResult result = deSearch.FindOne();
                //de = new DirectoryEntry(result.Path);
                //return de;
            }
            catch
            {
                return null;

            }
        }

        #endregion

    }
}