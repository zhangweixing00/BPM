using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.SmartObjects.Client;
using SourceCode.Hosting.Client.BaseAPI;
using System.Configuration;
using System.Data;
using System.Collections.Specialized;
using SourceCode.Workflow.Client;
using Utility;

namespace K2Utility
{
    public class K2Helper
    {
        #region k2 server info
        private static readonly string _K2ServerName = ConfigurationManager.AppSettings["K2ServerName"];
        private static readonly string _K2User = ConfigurationManager.AppSettings["K2User"];
        private static readonly string _Password = ConfigurationManager.AppSettings["K2Password"];
        private static readonly string _K2Domain = ConfigurationManager.AppSettings["Domain"];
        private static readonly string _SecurityLabelName = ConfigurationManager.AppSettings["SecurityLabelName"];
        private static readonly string _CurrentUser = System.Web.HttpContext.Current.User.Identity.Name;
        #endregion

        #region Smart Object
        private static SmartObjectClientServer GetSOClientConnection()
        {
            SmartObjectClientServer soClient = new SmartObjectClientServer();
            soClient.Connection = soClient.CreateConnection();

            if (!soClient.Connection.IsConnected)
            {

                //use connection string builder to create connection string.
                SCConnectionStringBuilder connectionStr = new SCConnectionStringBuilder();

                //set server info
                connectionStr.Host = _K2ServerName;

                connectionStr.Integrated = true;
                connectionStr.IsPrimaryLogin = true;
                connectionStr.Password = _Password;
                connectionStr.Port = 5555;
                connectionStr.UserID = _K2User;
                connectionStr.WindowsDomain = _K2Domain;
                connectionStr.SecurityLabelName = _SecurityLabelName;


                //open connection
                try
                {
                    soClient.Connection.Open(connectionStr.ConnectionString);
                }
                catch
                {
                    return null;
                }
            }

            return soClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="soname">Smart Object Name</param>
        /// <param name="method">Method</param>
        /// <param name="properties">Input Properties</param>
        /// <returns></returns>
        public static DataTable SOExecuteListDataTable(string soname, string method, NameValueCollection properties)
        {
            SmartObjectClientServer soClient = null;
            DataTable SOList = null;

            try
            {
                // get connection to smartobject Server
                soClient = GetSOClientConnection();

                // get smartobject
                SmartObject SO = soClient.GetSmartObject(soname);

                if (properties != null)
                {
                    for (int i = 0; i < properties.Count; i++)
                    {
                        string key = properties.GetKey(i).ToString();
                        if (properties[key] != null && !string.IsNullOrEmpty(properties[key]))
                        {
                            string value = properties[key].ToString();
                            //SO.ListMethods[method].InputProperties[key].Value = value;
                            SO.Properties[key].Value = value;
                        }
                    }
                }

                SO.MethodToExecute = method;

                SOList = soClient.ExecuteListDataTable(SO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close connection
                if (soClient != null)
                    soClient.Connection.Close();
            }
            return SOList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="soname">Smart Object Name</param>
        /// <param name="method">Method</param>
        /// <param name="properties">Input Properties</param>
        /// <returns></returns>
        public static void SOExecuteScalar(string soname, string method, NameValueCollection properties)
        {
            SmartObjectClientServer soClient = null;

            try
            {
                // get connection to smartobject Server
                soClient = GetSOClientConnection();

                // get smartobject
                SmartObject SO = soClient.GetSmartObject(soname);

                if (properties != null)
                {
                    for (int i = 0; i < properties.Count; i++)
                    {
                        string key = properties.GetKey(i).ToString();
                        if (properties[key] != null && !string.IsNullOrEmpty(properties[key]))
                        {
                            string value = properties[key].ToString();
                            //SO.ListMethods[method].InputProperties[key].Value = value;
                            SO.Properties[key].Value = value;
                        }
                    }
                }

                SO.MethodToExecute = method;

                soClient.ExecuteScalar(SO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close connection
                if (soClient != null)
                    soClient.Connection.Close();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="soname">Smart Object Name</param>
        /// <param name="method">Method</param>
        /// <param name="properties">Input Properties</param>
        /// <returns></returns>
        public static SmartObjectReader SOExecuteListReader(string soname, string method, NameValueCollection properties)
        {
            SmartObjectClientServer soClient = null;
            SmartObjectReader SOList = null;

            try
            {
                // get connection to smartobject Server
                soClient = GetSOClientConnection();

                // get smartobject
                SmartObject SO = soClient.GetSmartObject(soname);

                if (properties != null)
                {
                    for (int i = 0; i < properties.Count; i++)
                    {
                        string key = properties.GetKey(i).ToString();
                        if (properties[key] != null && !string.IsNullOrEmpty(properties[key]))
                        {
                            string value = properties[key].ToString();
                            //SO.ListMethods[method].InputProperties[key].Value = value;
                            SO.Properties[key].Value = value;
                        }
                    }
                }

                SO.MethodToExecute = method;

                SOList = soClient.ExecuteListReader(SO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close connection
                //if (soClient != null)
                //    soClient.Connection.Close();
            }
            return SOList;
        }

        #endregion

        #region  K2工作流参数设置信息
        private static ConnectionSetup GetConnectionSetup()
        {
            ConnectionSetup conSetup = new ConnectionSetup();
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.Host, GetConfigValue<string>("K2ServerName", "Localhost"));
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.UserID, GetConfigValue<string>("K2User", ""));
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.Password, GetConfigValue<string>("K2Password", ""));
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.Port, "5252");
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.Authenticate, "true");
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.WindowsDomain, GetConfigValue<string>("Domain", "Contoso"));
            conSetup.ConnectionParameters.Add(ConnectionSetup.ParamKeys.IsPrimaryLogin, "true");

            return conSetup;
        }
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
        public static void Redirect(string sn, string targetUser)
        {
            SourceCode.Workflow.Client.Connection con = new Connection();
            try
            {
                ConnectionSetup conSetup = GetConnectionSetup();
                con.Open(conSetup);
                con.ImpersonateUser(_CurrentUser);

                SourceCode.Workflow.Client.WorklistItem listItem = con.OpenWorklistItem(sn);
                listItem.Release();
                listItem.Redirect(targetUser);
                DBManager.AddWorkListLog(_CurrentUser, targetUser, listItem.SerialNumber, listItem.ProcessInstance.ID, listItem.ActivityInstanceDestination.ID, "Redirect", _CurrentUser);
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
                    con.RevertUser();
                }
                catch
                {
                    throw;
                }

                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public static void Release(string sn)
        {
            SourceCode.Workflow.Client.Connection con = new Connection();
            try
            {
                ConnectionSetup conSetup = GetConnectionSetup();
                con.Open(conSetup);
                con.ImpersonateUser(_CurrentUser);

                SourceCode.Workflow.Client.WorklistItem listItem = con.OpenWorklistItem(sn);
                listItem.Release();
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
                    con.RevertUser();
                }
                catch
                {
                    throw;
                }

                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public static void Delegate(string sn, string targetUser)
        {
            SourceCode.Workflow.Client.Connection con = new Connection();
            try
            {

                ConnectionSetup conSetup = GetConnectionSetup();
                con.Open(conSetup);
                con.ImpersonateUser(_CurrentUser);

                SourceCode.Workflow.Client.WorklistItem listItem = con.OpenWorklistItem(sn);
                listItem.Release();

                Destination dest = new Destination();
                dest.DestinationType = DestinationType.User;
                for (int i = 0; i < listItem.Actions.Count; i++)
                {
                    dest.AllowedActions.Add(listItem.Actions[i].Name);
                }
                dest.Name = targetUser;

                listItem.Delegate(dest);
                DBManager.AddWorkListLog(_CurrentUser, targetUser, listItem.SerialNumber, listItem.ProcessInstance.ID, listItem.ActivityInstanceDestination.ID, "Delegate", _CurrentUser);
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
                    con.RevertUser();
                }
                catch
                {
                    throw;
                }

                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public static void Sleep(string sn, int second)
        {
            SourceCode.Workflow.Client.Connection con = new Connection();
            try
            {
                ConnectionSetup conSetup = GetConnectionSetup();
                con.Open(conSetup);
                con.ImpersonateUser(_CurrentUser);

                SourceCode.Workflow.Client.WorklistItem listItem = con.OpenWorklistItem(sn);
                listItem.Sleep(true, second);
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
                    con.RevertUser();
                }
                catch
                {
                    throw;
                }

                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public static void Approve(string sn, string action, NameValueCollection dataFields)
        {
            SourceCode.Workflow.Client.Connection con = new Connection();
            try
            {
                ConnectionSetup conSetup = GetConnectionSetup();
                con.Open(conSetup);
                con.ImpersonateUser(_CurrentUser);

                SourceCode.Workflow.Client.WorklistItem listItem = con.OpenWorklistItem(sn);
                listItem.Release();
                if (dataFields != null)
                {
                    for (int i = 0; i < dataFields.Count; i++)
                    {
                        string key = dataFields.GetKey(i).ToString();
                        string value = dataFields[key].ToString();
                        listItem.ProcessInstance.DataFields[key].Value = value;
                    }
                }
                listItem.Actions[action].Execute();
            }
            catch(Exception ex)
            {
                // TODO: throw?
                throw ex;
            }
            finally
            {
                try
                {
                    con.RevertUser();
                }
                catch
                { }

                if (con != null)
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region Server Connection and Method
        #endregion

    }
}
