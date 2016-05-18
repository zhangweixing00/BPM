using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using SourceCode.Workflow.Client;
using System.Collections.Specialized;
using Mang = SourceCode.Workflow.Management;
using SourceCode.Hosting.Client.BaseAPI;
using System.Data;
using SourceCode.Security.UserRoleManager.Management;

namespace WS.K2
{
    class K2Helper
    {
        #region  K2工作流参数设置信息

        private static readonly string K2ServerName = GetConfigValue<string>("K2ServerName", "Localhost");
        private static readonly string K2User = GetConfigValue<string>("K2User", "administrator");
        private static readonly string Password = GetConfigValue<string>("K2Password", "demopc@2011");
        private static readonly string K2Domain = GetConfigValue<string>("K2ServerDomain", "contoso");
        private static readonly string SecurityLabelName = GetConfigValue<string>("K2SecurityLabelName", "K2");

        private static T GetConfigValue<T>(string key, T initValue)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
            {
                return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            }
            return initValue;
        }

        #endregion

        #region Client Connection and Method

        #region private method
        private static ConnectionSetup GetConnectionSetup()
        {
            ConnectionSetup conSetup = new ConnectionSetup();
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.Host, K2ServerName);
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.UserID, K2User);
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.Password, Password);
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.Port, "5252");
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.Authenticate, "true");
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.WindowsDomain, K2Domain);
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.IsPrimaryLogin, "true");

            return conSetup;
        }

        private static void SetDataFields(WorklistItem listItem, CDataFields dataFields)
        {
            if (dataFields != null && dataFields.DataFieldLists != null && dataFields.DataFieldLists.Count > 0)
            {
                foreach (CDataField dataField in dataFields.DataFieldLists)
                {
                    string key = dataField.Key;
                    string value = dataField.Value;
                    if (dataField.Type == "DF")
                    {
                        listItem.ProcessInstance.DataFields[key].Value = value;
                    }
                    else if (dataField.Type == "XF")
                    {
                        listItem.ProcessInstance.XmlFields[key].Value = value;
                    }
                }
            }
        }

        private static void GetDataFields(WorklistItem listItem, CDataFields dataFields)
        {
            if (dataFields != null && dataFields.DataFieldLists != null && dataFields.DataFieldLists.Count > 0)
            {
                foreach (CDataField dataField in dataFields.DataFieldLists)
                {
                    string key = dataField.Key;
                    if (dataField.Type == "DF")
                    {
                        dataField.Value = listItem.ProcessInstance.DataFields[key].Value.ToString();
                    }
                    else if (dataField.Type == "XF")
                    {
                        dataField.Value = listItem.ProcessInstance.XmlFields[key].Value;
                    }
                }
            }
        }

        private static void SetDataFields(ProcessInstance procInst, CDataFields dataFields)
        {
            if (dataFields != null && dataFields.DataFieldLists != null && dataFields.DataFieldLists.Count > 0)
            {
                foreach (CDataField dataField in dataFields.DataFieldLists)
                {
                    string key = dataField.Key;
                    string value = dataField.Value;
                    if (dataField.Type == "DF")
                    {
                        procInst.DataFields[key].Value = value;
                    }
                    else if (dataField.Type == "XF")
                    {
                        procInst.XmlFields[key].Value = value;
                    }
                }
            }
        }

        private static void GetDataFields(ProcessInstance procInst, CDataFields dataFields)
        {
            if (dataFields != null && dataFields.DataFieldLists != null && dataFields.DataFieldLists.Count > 0)
            {
                foreach (CDataField dataField in dataFields.DataFieldLists)
                {
                    string key = dataField.Key;
                    if (dataField.Type == "DF")
                    {
                        dataField.Value = procInst.DataFields[key].Value.ToString();
                    }
                    else if (dataField.Type == "XF")
                    {
                        dataField.Value = procInst.XmlFields[key].Value;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 任务移交
        /// </summary>
        /// <param name="sn">任务SN</param>
        /// <param name="sourceUser">源用户</param>
        /// <param name="targetUser">目标用户</param>
        public static void RedirectWorkListItem(string sn, string sourceUser, string targetUser)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);
                    conn.ImpersonateUser(sourceUser);

                    SourceCode.Workflow.Client.WorklistItem listItem = conn.OpenWorklistItem(sn);
                    listItem.Redirect(targetUser);
                }
                catch (Exception ex)
                {
                    // TODO: throw?
                    throw ex;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    {
                        throw;
                    }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="sn">流程SN,必选</param>
        /// <param name="action">流程操作,为空则默认执行第0个操作</param>
        /// <param name="folio">流程编码,可选</param>
        /// <param name="dataFields">流程变量,必选</param>
        public static void ExecuteProcess(string sn, string action, string folio, string currentUser, CDataFields dataFields)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);
                    conn.RevertUser();
                    conn.ImpersonateUser(currentUser);

                    SourceCode.Workflow.Client.WorklistItem listItem = conn.OpenWorklistItem(sn);
                    SetDataFields(listItem, dataFields);
                    if (!string.IsNullOrEmpty(folio))
                    {
                        listItem.ProcessInstance.Folio = folio;
                    }
                    if (!string.IsNullOrEmpty(action))
                    {
                        listItem.Actions[action].Execute();
                    }
                    else
                    {
                        listItem.Actions[0].Execute();
                    }
                }
                catch
                {
                    // TODO: throw?
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    { }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="processName">流程名称：Project\Process1</param>
        /// <param name="folio">流程编码</param>
        /// <param name="nvcDataFields">流程变量</param>
        /// <returns>流程实例ID</returns>
        public static int StartProcess(string processName, string currentUser, string folio, CDataFields dataFields)
        {
            using (Connection conn = new Connection())
            {
                int procInstID = -1;
                try
                {
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);
                    conn.RevertUser();
                    conn.ImpersonateUser(currentUser);

                    ProcessInstance procInst = conn.CreateProcessInstance(processName);
                    SetDataFields(procInst, dataFields);
                    if (string.IsNullOrEmpty(folio))
                    {
                        folio = System.DateTime.Now.ToString();
                    }
                    procInst.Folio = folio;
                    conn.StartProcessInstance(procInst);
                    procInstID = procInst.ID;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    { }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
                return procInstID;
            }
        }

        /// <summary>
        /// 更新流程变量
        /// </summary>
        /// <param name="sn">流程SN</param>
        /// <param name="nvcDataFields">要更新的变量</param>
        public static void UpdateProcessDataFields(string sn, string currentUser, CDataFields dataFields)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);
                    conn.RevertUser();
                    conn.ImpersonateUser(currentUser);

                    WorklistItem listItem = conn.OpenWorklistItem(sn);
                    SetDataFields(listItem, dataFields);
                    listItem.ProcessInstance.Update();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    { }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 更新流程变量
        /// </summary>
        /// <param name="sn">流程实例ID</param>
        /// <param name="nvcDataFields">要更新的变量</param>
        public static void UpdateProcessDataFields(int procInstID, CDataFields dataFields)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);

                    ProcessInstance procInst = conn.OpenProcessInstance(procInstID);
                    SetDataFields(procInst, dataFields);
                    procInst.Update();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    { }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 更新流程变量
        /// </summary>
        /// <param name="sn">流程实例ID</param>
        /// <param name="nvcDataFields">要获取的变量的List</param>
        /// <returns>变量的List</returns>
        public static void GetProcessDataFields(int procInstID, CDataFields dataFields)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);

                    ProcessInstance procInst = conn.OpenProcessInstance(procInstID);
                    GetDataFields(procInst, dataFields);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    { }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 获取流程变量
        /// </summary>
        /// <param name="sn">流程SN</param>
        /// <param name="FieldNames">要获取的变量的List</param>
        /// <returns>变量的List</returns>
        public static void GetProcessDataFields(string sn, string currentUser, CDataFields dataFields)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);
                    conn.ImpersonateUser(currentUser);
                    WorklistItem listItem = conn.OpenWorklistItem(sn);
                    GetDataFields(listItem, dataFields);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    { }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="sn">任务SN</param>
        /// <param name="currentUser"></param>
        public static void ReleaseWorkListItem(string sn, string currentUser)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);
                    conn.ImpersonateUser(currentUser);

                    SourceCode.Workflow.Client.WorklistItem listItem = conn.OpenWorklistItem(sn);
                    listItem.Release();
                }
                catch
                {
                    // TODO: throw?
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    {
                        throw;
                    }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 委托代办
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="sourceUser"></param>
        /// <param name="targetUser"></param>
        public static string DelegateWorkListItem(string sn, string sourceUser, string targetUser)
        {
            using (Connection conn = new Connection())
            {
                string result = "";
                bool ContainsTargetUser = false;
                try
                {
                    string targetUserSL = SecurityLabelName + ":" + targetUser;
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);
                    conn.ImpersonateUser(sourceUser);

                    SourceCode.Workflow.Client.WorklistItem listItem = conn.OpenWorklistItem(sn);

                    //重置任务状态为Available，只有Available状态下的任务才有权限代理
                    listItem.Release();
                    //判断该任务是否已经代理给targetUser
                    foreach (Destination destuser in listItem.DelegatedUsers)
                    {
                        if (destuser.Name.Equals(targetUserSL, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ContainsTargetUser = true;
                            result = "任务不能重复代理给[" + targetUser + "]";
                            break;
                        }
                    }
                    if (!ContainsTargetUser)
                    {
                        //如果该任务是其它人代理给sourceUser的，则sourceUser不能再次代理给targetUser，即禁止传递代理
                        DataTable DelegationLog = K2DBHelper.GetDelegationsLog(listItem.SerialNumber).Tables[0];
                        foreach (DataRow dr in DelegationLog.Rows)
                        {
                            if (sourceUser.Equals(dr["ToUser"].ToString(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                ContainsTargetUser = true;
                                result = "该任务由[" + dr["FromUser"].ToString() + "]代理给[" + sourceUser + "]";
                                break;
                            }
                        }
                    }
                    if (!ContainsTargetUser)
                    {
                        Destination dest = new Destination();
                        dest.DestinationType = DestinationType.User;
                        for (int i = 0; i < listItem.Actions.Count; i++)
                        {
                            dest.AllowedActions.Add(listItem.Actions[i].Name);
                        }
                        dest.Name = targetUserSL;
                        //代理任务
                        listItem.Delegate(dest);

                        //Todo:给代理人邮件

                        //Todo：添加代理历史记录
                        int parentProcInstID = K2DBHelper.GetRootParentsID(listItem.ProcessInstance.ID);
                        K2DBHelper.AddDelegationsLog(listItem.ProcessInstance.ID, parentProcInstID, sn, sourceUser, targetUser, sourceUser);
                        result = "任务代理成功";
                    }
                    return result;
                }
                catch
                {
                    // TODO: throw?
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    {
                        throw;
                    }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 挂起
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="sourceUser"></param>
        /// <param name="second"></param>
        public static void SleepWorkListItem(string sn, string sourceUser, int second)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    ConnectionSetup conSetup = GetConnectionSetup();
                    conn.Open(conSetup);
                    conn.ImpersonateUser(sourceUser);

                    SourceCode.Workflow.Client.WorklistItem listItem = conn.OpenWorklistItem(sn);
                    listItem.Sleep(true, second);
                }
                catch
                {
                    // TODO: throw?
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.RevertUser();
                    }
                    catch
                    {
                        throw;
                    }

                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 供代理服务使用
        /// </summary>
        public static void DelegateWorkList()
        {
            try
            {
                //删除已经过期的代理规则
                K2DBHelper.DeleteExpiredDelegations();

                //获取有效的代理规则
                DataTable DelegationRules = K2DBHelper.GetActiveDelegations().Tables[0];

                if (DelegationRules != null && DelegationRules.Rows.Count > 0)
                {
                    using (Connection conn = new Connection())
                    {
                        try
                        {
                            ConnectionSetup conSetup = GetConnectionSetup();
                            conn.Open(conSetup);

                            foreach (DataRow DelegateRule in DelegationRules.Rows)
                            {
                                //获取被代理人的WorkList
                                conn.ImpersonateUser(DelegateRule["FromUserAD"].ToString());
                                Worklist delegateFromWorkList = conn.OpenWorklist();

                                if (delegateFromWorkList != null && delegateFromWorkList.Count > 0)
                                {
                                    foreach (WorklistItem listItem in delegateFromWorkList)
                                    {
                                        //选择状态为Available和Open状态的任务
                                        if (listItem.Status == WorklistStatus.Available || listItem.Status == WorklistStatus.Open)
                                        {
                                            //获取该任务的主流程实例ID
                                            int parentProcInstID = K2DBHelper.GetRootParentsID(listItem.ProcessInstance.ID);
                                            //获取该任务的主流程实例
                                            ProcessInstance inst = conn.OpenProcessInstance(parentProcInstID);
                                            //代理规则中设置的主流程的名称
                                            string delagetionType = DelegateRule["ProcessFullName"].ToString();
                                            //如果代理规则设置的流程名称为All或等主流程的名称
                                            if (delagetionType.Equals("ALL", StringComparison.InvariantCultureIgnoreCase) || delagetionType.Equals(inst.FullName, StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                bool ContainsTargetUser = false;
                                                string targetUserSL = SecurityLabelName + ":" + DelegateRule["ToUserAD"].ToString();

                                                //重置任务状态为Available，只有Available状态下的任务才有权限代理
                                                listItem.Release();
                                                //判断该任务是否已经代理给targetUser
                                                foreach (Destination destuser in listItem.DelegatedUsers)
                                                {
                                                    if (destuser.Name.Equals(targetUserSL, StringComparison.InvariantCultureIgnoreCase))
                                                    {
                                                        ContainsTargetUser = true;
                                                        break;
                                                    }
                                                }
                                                if (!ContainsTargetUser)
                                                {
                                                    //如果该任务是其它人代理给sourceUser的，则sourceUser不能再次代理给targetUser，即禁止传递代理
                                                    DataTable DelegationLog = K2DBHelper.GetDelegationsLog(listItem.SerialNumber).Tables[0];
                                                    foreach (DataRow dr in DelegationLog.Rows)
                                                    {
                                                        if (DelegateRule["FromUserAD"].ToString().Equals(dr["ToUser"].ToString(), StringComparison.InvariantCultureIgnoreCase))
                                                        {
                                                            ContainsTargetUser = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (!ContainsTargetUser)
                                                {
                                                    Destination dest = new Destination();
                                                    dest.DestinationType = DestinationType.User;
                                                    for (int i = 0; i < listItem.Actions.Count; i++)
                                                    {
                                                        dest.AllowedActions.Add(listItem.Actions[i].Name);
                                                    }
                                                    dest.Name = targetUserSL;
                                                    //任务代理
                                                    listItem.Delegate(dest);

                                                    //Todo:给代理人邮件

                                                    //Todo：添加代理历史记录
                                                    K2DBHelper.AddDelegationsLog(listItem.ProcessInstance.ID, parentProcInstID, listItem.SerialNumber, DelegateRule["FromUserAD"].ToString(), DelegateRule["ToUserAD"].ToString(), "Server");
                                                }
                                            }
                                        }
                                    }
                                }
                                conn.RevertUser();
                            }
                        }
                        catch
                        {
                            // TODO: throw?
                            throw;
                        }
                        finally
                        {
                            try
                            {
                                conn.RevertUser();
                            }
                            catch
                            {
                                throw;
                            }

                            if (conn != null)
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
            catch
            {
                // TODO: throw?
                throw;
            }
        }
        #endregion

        #region Server Connection and Method
        private static string GetServerConnectionSetup()
        {
            SCConnectionStringBuilder connectionString = new SCConnectionStringBuilder();

            connectionString.Authenticate = true;
            connectionString.Host = K2ServerName;
            connectionString.Integrated = true;
            connectionString.IsPrimaryLogin = true;
            connectionString.Port = 5555;
            connectionString.UserID = K2User;
            connectionString.Password = Password;
            connectionString.WindowsDomain = K2Domain;
            connectionString.SecurityLabelName = SecurityLabelName;

            return connectionString.ConnectionString;
        }

        /// <summary>
        /// GotoActivity
        /// </summary>
        /// <param name="procInstID"></param>
        /// <param name="activityName"></param>
        public static void GotoActivity(int procInstID, string activityName)
        {

            Mang.WorkflowManagementServer svr = new Mang.WorkflowManagementServer();
            try
            {
                svr.CreateConnection();
                svr.Connection.Open(GetServerConnectionSetup());
                svr.GotoActivity(procInstID, activityName);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (svr != null)
                {
                    svr.Connection.Close();
                }
            }
        }

        /// <summary>
        /// 撤消任务
        /// </summary>
        /// <param name="procInstID">流程实例ID，格式：1_2_3_4</param>
        /// <param name="activityName">实例结束结点</param>
        public static void CancelProcInstance(string procInstID)
        {
            Mang.WorkflowManagementServer svr = new Mang.WorkflowManagementServer();
            try
            {
                svr.CreateConnection();
                svr.Connection.Open(GetServerConnectionSetup());
                string[] procInstIDs = procInstID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string id in procInstIDs)
                {
                    svr.GotoActivity(Convert.ToInt32(id), "结束");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (svr != null)
                {
                    svr.Connection.Close();
                }
            }
        }

        /// <summary>
        ///  create a role 
        /// </summary>
        /// <param name="roleName">role name</param>
        /// <param name="roleDescription">roleDescription's description</param>
        /// <param name="users">user ad's</param>
        public static void CreateRole(string roleName, string roleDescription, string users)
        {
            if (string.IsNullOrEmpty(users))
            {
                return;
            }

            SourceCode.Security.UserRoleManager.Management.UserRoleManager roleManager = new UserRoleManager();

            try
            {
                roleManager.CreateConnection();
                roleManager.Connection.Open(GetServerConnectionSetup());
                if (roleManager.GetRole(roleName) == null)
                {
                    SourceCode.Security.UserRoleManager.Management.Role role = new SourceCode.Security.UserRoleManager.Management.Role();

                    // Set Role Name, Description and Properties
                    role.Name = roleName;
                    role.Description = roleDescription;
                    role.IsDynamic = true;

                    ////Add users to Include in Role
                    ////role.Include.Add(new UserItem("K2:DENALLIX\\Anthony"));
                    string[] user = users.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string u in user)
                    {
                        string us = SecurityLabelName + ":" + u;
                        if (role.Include[us] == null)
                        {
                            role.Include.Add(new UserItem(us));
                        }
                    }

                    roleManager.CreateRole(role);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (roleManager != null)
                {
                    roleManager.Connection.Close();
                    roleManager.Connection.Dispose();
                    roleManager.DeleteConnection();
                    roleManager.Connection = null;
                    roleManager = null;
                }
            }
        }

        /// <summary>
        ///  Add users to a role 
        /// </summary>
        /// <param name="roleName">role name</param>
        /// <param name="users">user ad's</param>
        public static void AddUsersToRole(string roleName, string users)
        {
            SourceCode.Security.UserRoleManager.Management.UserRoleManager roleManager = new UserRoleManager();

            try
            {
                roleManager.CreateConnection();
                roleManager.Connection.Open(GetServerConnectionSetup());

                SourceCode.Security.UserRoleManager.Management.Role role = roleManager.GetRole(roleName);

                ////Add users to Include in Role
                ////role.Include.Add(new UserItem("K2:DENALLIX\\Anthony"));
                if (role != null)
                {
                    string[] user = users.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string u in user)
                    {
                        string us = SecurityLabelName + ":" + u;
                        if (role.Include[us] == null)
                        {
                            role.Include.Add(new UserItem(us));
                        }
                    }

                    roleManager.UpdateRole(role);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (roleManager != null)
                {
                    roleManager.Connection.Close();
                    roleManager.Connection.Dispose();
                    roleManager.DeleteConnection();
                    roleManager.Connection = null;
                    roleManager = null;
                }
            }
        }

        /// <summary>
        ///  delete users of role 
        /// </summary>
        /// <param name="roleName">role name</param>
        /// <param name="users">user ad's</param>
        public static void DeleteUserFromRole(string roleName, string users)
        {
            UserRoleManager roleManager = new UserRoleManager();
            try
            {
                roleManager.CreateConnection();
                roleManager.Connection.Open(GetServerConnectionSetup());
                Role role = roleManager.GetRole(roleName);
                if (role != null)
                {
                    string[] user = users.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string u in user)
                    {
                        role.Include.Remove(role.Include[SecurityLabelName + ":" + u]);
                    }

                    roleManager.UpdateRole(role);
                    roleManager.Connection.Dispose();
                    roleManager.DeleteConnection();
                    roleManager.Connection = null;
                    roleManager = null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (roleManager != null)
                {
                    roleManager.Connection.Close();
                    roleManager.Connection.Dispose();
                    roleManager.DeleteConnection();
                    roleManager.Connection = null;
                    roleManager = null;
                }
            }
        }

        /// <summary>
        ///  delete role
        /// </summary>
        /// <param name="roleName">role name</param>
        public static void DeleteRole(string roleName)
        {
            UserRoleManager roleManager = new UserRoleManager();
            try
            {
                roleManager.CreateConnection();
                roleManager.Connection.Open(GetServerConnectionSetup());
                Role role = roleManager.GetRole(roleName);
                if (role != null)
                {
                    roleManager.DeleteRole(role.Guid, roleName);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (roleManager != null)
                {
                    roleManager.Connection.Close();
                    roleManager.Connection.Dispose();
                    roleManager.DeleteConnection();
                    roleManager.Connection = null;
                    roleManager = null;
                }
            }
        }

        /// <summary>
        ///  Get roles
        /// </summary>
        /// <returns>return roles</returns>
        public static string[] GetRoles()
        {
            UserRoleManager roleManager = new UserRoleManager();
            try
            {
                roleManager.CreateConnection();
                roleManager.Connection.Open(GetServerConnectionSetup());
                string[] roleNameList = roleManager.GetRoleNameList();
                return roleNameList;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (roleManager != null)
                {
                    roleManager.Connection.Close();
                    roleManager.Connection.Dispose();
                    roleManager.DeleteConnection();
                    roleManager.Connection = null;
                    roleManager = null;
                }
            }
        }

        /// <summary>
        ///  Get users of role by role name
        /// </summary>
        /// <param name="roleName">role name</param>
        /// <returns>return users</returns>
        public static string[] GetRoleUsers(string roleName)
        {
            UserRoleManager roleManager = new UserRoleManager();
            try
            {
                roleManager.CreateConnection();
                roleManager.Connection.Open(GetServerConnectionSetup());
                Role role = roleManager.GetRole(roleName);
                string[] names = new string[role.Include.Count];
                for (int i = 0; i < role.Include.Count; i++)
                {
                    names[i] = role.Include[i].Name;
                }

                return names;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (roleManager != null)
                {
                    roleManager.Connection.Close();
                    roleManager.Connection.Dispose();
                    roleManager.DeleteConnection();
                    roleManager.Connection = null;
                    roleManager = null;
                }
            }
        }
        #endregion
    }
}