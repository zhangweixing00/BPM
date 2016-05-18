using System;
using System.Data.SqlClient;

/// <summary>
/// 移动端请求的日志
/// </summary>
public class MobileLog
{
    /// <summary>
    /// 写入日志
    /// </summary>
    /// <param name="action"></param>
    /// <param name="parameter"></param>
    /// <param name="url"></param>
    /// <param name="userCode"></param>
    public static void InsertLog(string action, string parameter, string url, string userCode)
    {
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("INSERT INTO [Mobile_Log]([Action],[Parameter],[Url],[UserCode],[CreateDate]) VALUES('{0}','{1}','{2}','{3}','{4}')",
                                      action, parameter, url, userCode, DateTime.Now);

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