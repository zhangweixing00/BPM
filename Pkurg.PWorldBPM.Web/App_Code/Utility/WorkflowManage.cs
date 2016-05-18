using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using SourceCode.Workflow.Client;
using SourceCode.Workflow.Management;

/// <summary>
///WorkflowManage 的摘要说明
/// </summary>
public class WorkflowManage
{
    #region k2 server info
    /// <summary>
    /// k2 server name
    /// </summary>
    static readonly string K2ServerName = ConfigurationManager.AppSettings["K2ServerName"];
    /// <summary>
    /// k2 server port
    /// </summary>
    static readonly string K2ServerPort = ConfigurationManager.AppSettings["K2ServerPort"];
    /// <summary>
    /// k2 host port
    /// </summary>
    static readonly string K2HostPort = ConfigurationManager.AppSettings["K2HostPort"];
    /// <summary>
    /// Windows Domain Name
    /// </summary>
    static readonly string DomainName = ConfigurationManager.AppSettings["DomainName"];
    /// <summary>
    /// k2 service user account
    /// </summary>
    static readonly string KSUserName = ConfigurationManager.AppSettings["K2ServiceUser"];
    /// <summary>
    /// k2 service user password
    /// </summary>
    static readonly string KSUserPwd = ConfigurationManager.AppSettings["K2ServicePwd"];

    //FBA to AD
    private static string _currentUser;
    public static string CurrentUser
    {
        get
        {
            //if (System.Web.HttpContext.Current != null)
            //{
            //    //_currentUser = System.Web.HttpContext.Current.User.Identity.Name;

            //    //if (_currentUser.ToLower().Contains("@sohu-inc.com"))
            //    //{
            //    //_currentUser = ConfigurationManager.AppSettings["DomainName"] + "\\" + _currentUser.Split('@')[0];
            //    //}
            //}
            //else
            //{
            //    _currentUser = System.Environment.UserName;
            //}
            return _currentUser;
        }
        set { _currentUser = value; }
    }
    #endregion

    #region k2 connection string
    /// <summary>
    /// get k2 connection string.(Integrated=True)
    /// </summary>
    /// <returns>k2 connection string</returns>
    public static string GetConnString()
    {
        //FBA to AD
        //return "Integrated=False;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2HostPort + ";SecurityLabelName=K2SQL;UserID=" + userName + ";Password=" + password + ";";
        return "Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2HostPort + ";WindowsDomain=" + DomainName + ";SecurityLabelName=K2;UserID=" + KSUserName + ";Password=" + KSUserPwd + ";";
    }

    public static string GetConnStringDel()
    {
        //FBA to AD
        //return "Integrated=False;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2HostPort + ";SecurityLabelName=K2SQL;UserID=" + userName + ";Password=" + password + ";";
        return "Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2ServerPort + ";WindowsDomain=" + DomainName + ";SecurityLabelName=K2;UserID=" + KSUserName + ";Password=" + KSUserPwd + ";";
    }
    #endregion

    //private static string adminUserLoginName = "founder\\zybpmadmin";

    public static void Abort()
    {
        //SourceCode.Workflow.Management.Processes.
    }

    public static bool GoToActitvy(int wfId, string activityName)
    {
        WorkflowManagementServer svr = new WorkflowManagementServer(K2ServerName, uint.Parse(K2ServerPort));
        svr.Open();
        return svr.GotoActivity(wfId, activityName);
    }


    /// <summary>
    /// 结束流程
    /// </summary>
    /// <param name="wfId"></param>
    /// <returns></returns>
    public static bool StopWorkflow(int wfId)
    {
        WorkflowManagementServer svr = new WorkflowManagementServer(K2ServerName, uint.Parse(K2ServerPort));

        try
        {
            svr.Open();
            bool flag = svr.StopProcessInstances(wfId);
            return flag;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            svr.Connection.Close();
        }
    }

    public static Dictionary<string, int> GetProcActivites(int procID)
    {
        Dictionary<string, int> list = new Dictionary<string, int>();
        WorkflowManagementServer svr = new WorkflowManagementServer(K2ServerName, uint.Parse(K2ServerPort));

        try
        {
            svr.Open();
            Activities activities = svr.GetProcInstActivities(procID);

            foreach (Activity activity in activities)
            {
                list.Add(activity.Name, activity.ID );
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            svr.Connection.Close();
        }

        return list;
    }

    public static NameValueCollection GetDataFields(int wfId)
    {
        NameValueCollection dataFields = new NameValueCollection();

        WorkflowManagementServer svr = new WorkflowManagementServer(K2ServerName, uint.Parse(K2ServerPort));

        try
        {
            svr.Open();
ProcessInstances ss=svr.GetProcessInstances(wfId);
           // ss[0].Process.DataFields

            //foreach (ProcessDataField item in dfs)
            //{
            //    dataFields.Add(item.Name, item.MetaData);
            //}
        }
        catch (Exception ex)
        {
        }
        finally
        {
            svr.Connection.Close();
        }
        return dataFields;
    }
    
    public static bool ModifyDataField(string sn, NameValueCollection dataFields)
    {
        Connection k2Conn = new Connection();

        try
        {
            k2Conn.Open(K2ServerName, WorkflowHelper.GetConnString());

            SourceCode.Workflow.Client.ProcessInstance inst = k2Conn.OpenProcessInstance(Convert.ToInt32(sn.Split('_')[0]));
            if (inst != null)
            {
                foreach (string s in dataFields)
                {
                    inst.DataFields[s].Value = dataFields[s];
                }

                inst.Update();
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            k2Conn.Close();
        }
    }

    /// <summary>
    /// 获取下一个审批人(处理人/处理步骤/接收时间)
    /// </summary>
    /// <param name="formId"></param>
    /// <returns></returns>
    public static SourceCode.Workflow.Management.WorklistItems GetNextApprover(string formId)
    {
        WorkflowManagementServer wms = new WorkflowManagementServer();
        try
        {
            wms.Open(WorkflowHelper.GetConnString4Management());
            SourceCode.Workflow.Management.Criteria.WorklistCriteriaFilter wcf = new SourceCode.Workflow.Management.Criteria.WorklistCriteriaFilter();
            wcf.AddRegularFilter(WorklistFields.Folio, SourceCode.Workflow.Management.Criteria.Comparison.Like, formId);

            WorklistItems wlis = wms.GetWorklistItems(wcf);
            wms.Connection.Close();
            return wlis;
        }
        catch
        {
            wms.Connection.Close();
            return null;
        }
        finally
        {

        }
    }
}