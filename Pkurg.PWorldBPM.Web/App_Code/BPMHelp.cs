using System;
using System.Data;
using System.Data.SqlClient;
using Pkurg.PWorldTemp.WorkflowCommon;

/// <summary>
///BPMHelp 的摘要说明
/// </summary>
public class BPMHelp
{
    public BPMHelp()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 获取流程序列号
    /// 如QSD201409240010
    /// </summary>
    /// <param name="workFlowCode">流程编码、流程模板名称</param>
    /// <returns></returns>
    public static string GetSerialNumber(string workFlowCode)
    {
        string date1 = DateTime.Now.ToString("yyyy-MM-dd");
        string date2 = DateTime.Now.ToString("yyyyMMdd");

        string number = "";

        int codeValue = 0;

        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;

        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@Date",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@Code",System.Data.SqlDbType.NVarChar,50)
                };
        parameters[0].Value = date1;
        parameters[1].Value = workFlowCode;

        DataTable dt = db.ExecutedProcedure("GetSerialNumber", parameters);

        if (dt != null && dt.Rows.Count > 0)
        {
            codeValue = Convert.ToInt16(dt.Rows[0][0]);
            string no = String.Format("{0:D4}", codeValue);
            //QSD201409240010
            number = workFlowCode + date2 + no;
        }
        return number;
    }

    /// <summary>
    /// 获取归档列表（分页）
    /// </summary>
    /// <param name="userCode"></param>
    /// <param name="source"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="proName"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static DataTable GetArchiveList(string userCode, string source, int pageIndex, int pageSize, string proName, string startTime, string endTime, out int count)
    {
        count = 0;
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;

        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@UserCode",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@Source", System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize",System.Data.SqlDbType.Int),
            new SqlParameter("@ProName",System.Data.SqlDbType.NVarChar,300),
            new SqlParameter("@StartTime1",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@EndTime1",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@Count",System.Data.SqlDbType.Int)
                 };
        parameters[0].Value = userCode;
        parameters[1].Value = source;
        parameters[2].Value = pageIndex;
        parameters[3].Value = pageSize;
        parameters[4].Value = proName;
        parameters[5].Value = startTime;
        parameters[6].Value = endTime;
        parameters[7].Value = count;
        parameters[7].Direction = ParameterDirection.Output;
        DataTable dt = db.ExecutedProcedure("wf_usp_GetArchiveListProc", parameters);

        count = Convert.ToInt32(parameters[7].Value);

        if (dt != null && dt.Rows.Count > 0)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }

    //BPM 归档，
    //把存储过程分开
    public static DataTable GetArchiveList_bpm(string userCode, string source, int pageIndex, int pageSize, string proName, string startTime, string endTime, out int count)
    {
        count = 0;
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;

        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@UserCode",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@Source", System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize",System.Data.SqlDbType.Int),
            new SqlParameter("@ProName",System.Data.SqlDbType.NVarChar,300),
            new SqlParameter("@StartTime1",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@EndTime1",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@Count",System.Data.SqlDbType.Int)
                 };
        parameters[0].Value = userCode;
        parameters[1].Value = source;
        parameters[2].Value = pageIndex;
        parameters[3].Value = pageSize;
        parameters[4].Value = proName;
        parameters[5].Value = startTime;
        parameters[6].Value = endTime;
        parameters[7].Value = count;
        parameters[7].Direction = ParameterDirection.Output;
        DataTable dt = db.ExecutedProcedure("wf_usp_GetArchiveListProc_bpm", parameters);

        count = Convert.ToInt32(parameters[7].Value);

        if (dt != null && dt.Rows.Count > 0)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }

    //注释 by yanghechun 2015-5-25

    ///// <summary>
    ///// 获得归档数目
    ///// </summary>
    ///// <param name="userCode"></param>
    ///// <param name="source"></param>
    ///// <param name="proName"></param>
    ///// <param name="startTime"></param>
    ///// <param name="endTime"></param>
    ///// <returns></returns>
    //public static int GetArchiveCount(string userCode, string source, string proName, string startTime, string endTime)
    //{
    //    int count = 0;
    //    if (proName == null)
    //    {
    //        proName = "";
    //    }
    //    if (startTime == null)
    //    {
    //        startTime = "";
    //    }
    //    if (endTime == null)
    //    {
    //        endTime = "";
    //    }
    //    DataProvider db = new DataProvider();
    //    db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
    //    SqlParameter[] parameters = new SqlParameter[]{
    //        new SqlParameter("@UserCode",System.Data.SqlDbType.NVarChar,50),
    //        new SqlParameter("@Source", System.Data.SqlDbType.VarChar,3),
    //        new SqlParameter("@ProName",System.Data.SqlDbType.NVarChar,300),
    //        new SqlParameter("@StartTime1",System.Data.SqlDbType.NVarChar,50),
    //        new SqlParameter("@EndTime1",System.Data.SqlDbType.NVarChar,50),
    //     };
    //    parameters[0].Value = userCode;
    //    parameters[1].Value = source;
    //    parameters[2].Value = proName;
    //    parameters[3].Value = startTime;
    //    parameters[4].Value = endTime;
    //    //string sqlString = "select count(*) as count from V_ArchiveProc_OA_BPM " + 
    //    //        "where userCode = @UserCode and Source = @Source" +
    //    //        "and (@ProName = '' or ProcName like '%'+@ProName+'%')" + 
    //    //        "and (@StartTime = '' or StartTime >= @StartTime) and (EndTime <= @EndTime or @EndTime = '')";
    //    //count = int.Parse(db.ExecuteScalar(sqlString, CommandType.Text, parameters));
    //    //count = 10;
    //    DataTable dt = db.ExecutedProcedure("wf_usp_GetArchiveProcCount", parameters);
    //    if (dt.Rows.Count > 0)
    //    {
    //        count = (int)dt.Rows[0]["count"];
    //    }
    //    return count;
    //}

    /// <summary>
    /// 获取员工列表
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static DataTable GetEmployeesList(string employeeName, string loginName, int pageIndex, int pageSize)
    {
        if (employeeName == null)
        {
            employeeName = "";
        }
        if (loginName == null)
        {
            loginName = "";
        }
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@EmployeeName", System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@LoginName", System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize", System.Data.SqlDbType.Int)
        };
        parameters[0].Value = employeeName;
        parameters[1].Value = loginName;
        parameters[2].Value = pageIndex;
        parameters[3].Value = pageSize;

        DataTable dt = db.ExecutedProcedure("wf_GetUserList", parameters);

        if (dt != null && dt.Rows.Count > 0)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 获取满足条件的用户数量
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public static int GetEmployeesCount(string employeeName, string loginName)
    {
        int count = 0;
        if (employeeName == null)
        {
            employeeName = "";
        }
        if (loginName == null)
        {
            loginName = "";
        }
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@EmployeeName", System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@LoginName", System.Data.SqlDbType.NVarChar,50)
        };
        parameters[0].Value = employeeName;
        parameters[1].Value = loginName;
        DataTable dt = db.ExecutedProcedure("wf_GetUserCount", parameters);

        if (dt != null && dt.Rows.Count > 0)
        {
            count = (int)dt.Rows[0]["count"];
        }
        return count;
    }

    /// <summary>
    /// 插入授权表
    /// </summary>
    /// <param name="AuthorizedByUserCode"></param>
    /// <param name="AuthorizedByName"></param>
    /// <param name="ProId"></param>
    /// <param name="ProcName"></param>
    /// <param name="AuthorizedUserCode"></param>
    /// <param name="AuthorizedUserName"></param>
    /// <returns></returns>
    public static bool InsertAuthorization(string AuthorizedByUserCode, string AuthorizedByUserName, string ProId, string ProcName, string AuthorizedUserCode, string AuthorizedUserName)
    {
        bool flag = false;
        DateTime date = DateTime.Now;
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        string sql = "INSERT INTO [dbo].[WF_Authorization](AuthorizedUserCode,AuthorizedUserName," +
                    "ProcId,ProcName,AuthorizedByUserCode,AuthorizedByUserName,AuthorizedOn)" +
                    " VALUES ('" + AuthorizedUserCode + "','" + AuthorizedUserName + "','" + ProId + "','" + ProcName
                    + "','" + AuthorizedByUserCode + "','" + AuthorizedByUserName + "','" + date + "')";
        if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// 判断是否存在授权信息
    /// </summary>
    /// <param name="ProId"></param>
    /// <param name="AuthorizedByUserCode"></param>
    /// <param name="AuthorizedUserCode"></param>
    /// <returns></returns>
    public static bool ExistAuthorization(string ProId, string AuthorizedByUserCode, string AuthorizedUserCode)
    {
        bool flag = false;
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        string sql = string.Format("select 1 from WF_Authorization where ProcId ='{0}' and  AuthorizedByUserCode='{1}' and AuthorizedUserCode='{2}'",
                      ProId, AuthorizedByUserCode, AuthorizedUserCode);
        if (db.ExecuteDataTable(sql, CommandType.Text).Rows.Count > 0)
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// 获取OA授权列表
    /// </summary>
    /// <param name="type"></param>
    /// <param name="userCode"></param>
    /// <param name="procName"></param>
    /// <param name="startTime1"></param>
    /// <param name="startTime2"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    public static DataTable GetOAAuthorizationList(int type, string userCode, string procName, string startTime1, string startTime2, int pageIndex, int pageSize, out int totalCount)
    {
        totalCount = 0;
        if (procName == null)
        {
            procName = "";
        }
        if (startTime1 == null)
        {
            startTime1 = "";
        }
        if (startTime2 == null)
        {
            startTime2 = "";
        }
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Center"].ConnectionString;
        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@Type", System.Data.SqlDbType.Int),
            new SqlParameter("@UserCode", System.Data.SqlDbType.NVarChar, 50),
            new SqlParameter("@ProcName", System.Data.SqlDbType.NVarChar, 300),
            new SqlParameter("@StartTime1", System.Data.SqlDbType.NVarChar, 50),
            new SqlParameter("@StartTime2", System.Data.SqlDbType.NVarChar, 50),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize", System.Data.SqlDbType.Int),
            new SqlParameter("@Count", System.Data.SqlDbType.Int)
        };
        parameters[0].Value = type;
        parameters[1].Value = userCode;
        parameters[2].Value = procName;
        parameters[3].Value = startTime1;
        parameters[4].Value = startTime2;
        parameters[5].Value = pageIndex;
        parameters[6].Value = pageSize;
        parameters[7].Value = totalCount;
        parameters[7].Direction = ParameterDirection.Output;
        DataTable dt = db.ExecutedProcedure("BPM_GetAuthorizationList", parameters);
        totalCount = Convert.ToInt32(parameters[7].Value);
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt;
        }
        return null;
    }

    /// <summary>
    /// 获取授权列表
    /// </summary>
    /// <param name="type"></param>
    /// <param name="userCode"></param>
    /// <param name="procName"></param>
    /// <param name="startTime1"></param>
    /// <param name="startTime2"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static DataTable GetAuthorList(int type, string userCode, string procName, string startTime1, string startTime2, int pageIndex, int pageSize)
    {
        if (procName == null)
        {
            procName = "";
        }
        if (startTime1 == null)
        {
            startTime1 = "";
        }
        if (startTime2 == null)
        {
            startTime2 = "";
        }
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@Type", System.Data.SqlDbType.Int),
            new SqlParameter("@UserCode", System.Data.SqlDbType.NVarChar, 50),
            new SqlParameter("@ProcName", System.Data.SqlDbType.NVarChar, 300),
            new SqlParameter("@StartTime1", System.Data.SqlDbType.NVarChar, 50),
            new SqlParameter("@StartTime2", System.Data.SqlDbType.NVarChar, 50),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize", System.Data.SqlDbType.Int)
        };
        parameters[0].Value = type;
        parameters[1].Value = userCode;
        parameters[2].Value = procName;
        parameters[3].Value = startTime1;
        parameters[4].Value = startTime2;
        parameters[5].Value = pageIndex;
        parameters[6].Value = pageSize;
        DataTable dt = db.ExecutedProcedure("wf_GetAuthorList", parameters);
        if (dt != null && dt.Rows.Count > 0)
        {
            return dt;
        }
        return null;
    }

    /// <summary>
    /// 获取授权数目
    /// </summary>
    /// <param name="type"></param>
    /// <param name="userCode"></param>
    /// <param name="procName"></param>
    /// <param name="startTime1"></param>
    /// <param name="startTime2"></param>
    /// <returns></returns>
    public static int GetAuthorCount(int type, string userCode, string procName, string startTime1, string startTime2)
    {
        int count = 0;
        if (procName == null)
        {
            procName = "";
        }
        if (startTime1 == null)
        {
            startTime1 = "";
        }
        if (startTime2 == null)
        {
            startTime2 = "";
        }
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@Type", System.Data.SqlDbType.Int),
            new SqlParameter("@UserCode", System.Data.SqlDbType.NVarChar, 50),
            new SqlParameter("@ProcName", System.Data.SqlDbType.NVarChar, 300),
            new SqlParameter("@StartTime1", System.Data.SqlDbType.NVarChar, 50),
            new SqlParameter("@StartTime2", System.Data.SqlDbType.NVarChar, 50)
        };
        parameters[0].Value = type;
        parameters[1].Value = userCode;
        parameters[2].Value = procName;
        parameters[3].Value = startTime1;
        parameters[4].Value = startTime2;
        DataTable dt = db.ExecutedProcedure("wf_GetAuthorCount", parameters);
        if (dt != null && dt.Rows.Count > 0)
        {
            count = (int)dt.Rows[0]["count"];
        }
        return count;
    }

    public static bool DeleteAuthorizations(string AuthorizationID)
    {
        bool flag = false;
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        string sql = string.Format("delete from WF_Authorization where AuthorizationID = '{0}'", AuthorizationID);
        if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
        {
            flag = true;
        }

        return flag;
    }

    /// <summary>
    /// 删除OA授权
    /// 2015-4-21 yanghechun
    /// </summary>
    /// <param name="AuthorizationID"></param>
    /// <returns></returns>
    public static bool DeleteOAAuthorizations(string AuthorizationID)
    {
        bool flag = false;
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Center"].ConnectionString;
        string sql = string.Format("delete from WF_FlowAccredit where AccreditID = '{0}'", AuthorizationID);
        if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
        {
            flag = true;
        }
        return flag;
    }

    public static string GetUrl(string procId, out string instanceId)
    {
        string url = "";
        instanceId = "";
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@ProcId", System.Data.SqlDbType.NVarChar,200)
        };
        parameters[0].Value = procId;
        DataTable dt = db.ExecutedProcedure("GetUrl", parameters);
        if (dt != null && dt.Rows.Count > 0)
        {
            url = dt.Rows[0]["FormName"].ToString();
            instanceId = dt.Rows[0]["InstanceID"].ToString();
        }
        return url;
    }

    //实例管理查看具体流程，获取DateField
    public static DataTable Get_K2_ProcInstDataAudit(int ProcInstID, out int count)
    {
        count = 0;
        DataProvider db = new DataProvider();
        db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;

        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@ProcInstID",System.Data.SqlDbType.Int),
            new SqlParameter("@Count",System.Data.SqlDbType.Int)
                 };
        parameters[0].Value = ProcInstID;
        parameters[1].Value = count;
        parameters[1].Direction = ParameterDirection.Output;
        DataTable dt = db.ExecutedProcedure("K2_ProcInstDataAudit_Get", parameters);

        count = Convert.ToInt32(parameters[1].Value);

        if (dt != null && dt.Rows.Count > 0)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 获取相关部门ID，通过部门名和另一个部门ID，递归
    /// 2015-11-19 
    /// </summary>
    /// <param name="OtherDeptID" name="DeptName"></param>
    /// <returns></returns>
    public static string GetDeptIDByOtherIDAndName(string OtherDeptID, string DeptName)
    {
        Pkurg.PWorld.Business.Permission.BFPmsUserRoleDepartment bfurd = new Pkurg.PWorld.Business.Permission.BFPmsUserRoleDepartment();
        string DeptCode = bfurd.GetDeptByCurrentDeptCodeAndOtherDeptName(OtherDeptID, DeptName).DepartCode;
        if (string.IsNullOrEmpty(DeptCode))
        {
            DeptCode = bfurd.GetDeptByCurrentDeptCodeAndOtherDeptName(OtherDeptID.Substring(0, OtherDeptID.Length - 5), DeptName).DepartCode;
        }
        if (string.IsNullOrEmpty(DeptCode))
        {
            DeptCode = bfurd.GetDeptByCurrentDeptCodeAndOtherDeptName(OtherDeptID.Substring(0, OtherDeptID.Length - 5), DeptName).DepartCode;
        }

        return DeptCode;
    }

    /// <summary>
    /// 获取公司编码，通过部门ID
    /// 2015-11-19 
    /// </summary>
    /// <param name="DeptID"></param>
    /// <returns></returns>
    public static string GetCompanyCodeByDeptID(string DeptID)
    {
        string CompanyCode = null;
        if (!string.IsNullOrEmpty(DeptID))
        {
            string[] array = DeptID.Split('-');
            if (array != null && array.Length > 2)
            {
                CompanyCode = array[0] + "-" + array[1];
            }
            else
            {
                CompanyCode = DeptID;
            }
        }
        return CompanyCode;
    }

    /// <summary>
    /// 写入实例管理日志
    /// </summary>
    /// <param name="action"></param>
    /// <param name="form_Id"></param>
    /// <param name="parameter"></param>
    /// <param name="userCode"></param>
    /// <param name="domainUserCode"></param>
    public static void InsertInstanceLog(string action, string form_Id, string parameter, string userCode, string domainUserCode)
    {
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("INSERT INTO [Instance_Log]([Action],[Form_Id],[Parameter],[UserCode],[DomainUserCode],[CreateDate]) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",
                                      action, form_Id, parameter, userCode, domainUserCode, DateTime.Now);

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        catch
        {

        }
    }
}