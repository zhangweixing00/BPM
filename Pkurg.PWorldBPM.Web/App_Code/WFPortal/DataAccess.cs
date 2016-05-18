using System.Data;
using System.Data.SqlClient;

/// <summary>
///DataAccess 的摘要说明
/// </summary>
public static class DataAccess
{
    /// <summary>
    /// 判断用户是否是超级管理员
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static bool IsSuperAdmin(string code)
    {
        string superAdmin = System.Configuration.ConfigurationManager.AppSettings["SuperAdmin"].ToString();
        superAdmin = superAdmin + ";";
        return superAdmin.ToLower().IndexOf(code.ToLower() + ";") > -1;
    }

    /// <summary>
    /// 获取员工名称
    /// </summary>
    /// <param name="code">域账号</param>
    /// <returns></returns>
    public static string GetUserName(string code)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Center"].ConnectionString;

        string name = "";

        SqlConnection connection = new SqlConnection(connectionString);

        string sql = "select * from Org_Users where LoginID='" + code + "'";

        SqlCommand cmd = new SqlCommand(sql);
        cmd.Connection = connection;

        connection.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            name = reader["Name"].ToString();
        }

        connection.Close();

        return name;
    }

    /// <summary>
    /// 获取两个视图
    /// Workflow.WithoutPermissions:所有用户都可以发起的流程
    /// Workflow.LaunchPermissions：用户和流程关系     
    /// </summary>
    /// <returns></returns>
    public static DataSet GetViews()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Center"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionString);

        string sql = "select * from Workflow.WithoutPermissions ; select * from Workflow.LaunchPermissions ;";

        DataSet ds = new DataSet();

        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, connection);

        sqlDataAdapter.Fill(ds);

        connection.Close();

        return ds;
    }
}