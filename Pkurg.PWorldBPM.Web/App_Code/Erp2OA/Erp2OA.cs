using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Sockets;
using System.Web;

/// <summary>
///DataAccess 的摘要说明
/// </summary>
public static class Erp2OA
{
    /// <summary>
    /// 获取两个视图
    /// dbo.View_OA_ERP_RGH_Workflow_Num
    /// dbo.View_OA_ERP_Workflow_Num
    /// </summary>
    /// <returns></returns>
    public static DataSet GetViews()
    {
        string userName = HttpContext.Current.User.Identity.Name.ToLower();
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Erp2OaConnectionString"].ConnectionString;
        string sql = string.Format("select * from View_OA_ERP_RGH_Workflow_Num where ATTRIBUTE4='{0}';select * from View_OA_ERP_Workflow_Num where ATTRIBUTE4='{0}' ;", userName);
        DataSet ds = new DataSet();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            //测试服务器是否开启
            //2014-12-29 yanghechun
            bool isConnection = TestConnection(connection.DataSource, 1433, 3000);
            if (!isConnection)
            {
                return null;
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = new SqlCommand(sql, connection);
            sqlDataAdapter.Fill(ds);
            return ds;
        }
    }

    /// <summary>
    /// 测试数据库是否开启
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="millisecondsTimeout"></param>
    /// <returns></returns>
    public static bool TestConnection(string host, int port, int millisecondsTimeout)
    {
        bool flag = true;

        Stopwatch wt = new Stopwatch();
        wt.Reset();
        wt.Start();

        TcpClient client = new TcpClient();
        try
        {
            //host是主机不包含端口,port就是端口了
            var ar = client.BeginConnect(host, port, null, null);
            ar.AsyncWaitHandle.WaitOne(millisecondsTimeout);            
            flag = client.Connected;
        }
        catch
        {
            flag = false;
        }
        finally
        {
            client.Close();
            wt.Stop();
        }
        return flag;
    }
}