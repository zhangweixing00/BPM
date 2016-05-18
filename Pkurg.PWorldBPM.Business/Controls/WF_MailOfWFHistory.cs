using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business.Controls
{
   public class WF_MailOfWFHistory
    {
        public static DataTable GetMailOfWFHistoryInfo(WF_GetMailOfWFHistoryInfo info)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100)
            };
            parameters[0].Value = info.InstanceID;
            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.usp_GetMailInfoByInstID", parameters);
            return dataTable;
        }
        public static DataTable UpdateMailInfoOfWFHistoryInfo(WF_GetMailOfWFHistoryInfo info)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@SN",System.Data.SqlDbType.NVarChar,20),
            new SqlParameter("@FormTitle",System.Data.SqlDbType.NVarChar,300),
            new SqlParameter("@ApproveLeader",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@Status",System.Data.SqlDbType.NVarChar,10),
            new SqlParameter("@ApproveLeaderEmail",System.Data.SqlDbType.NVarChar,300)
            };
            parameters[0].Value = info.InstanceID;
            parameters[1].Value = info.SN;
            parameters[2].Value = info.FormTitle;
            parameters[3].Value = info.ApproveLeader;
            parameters[4].Value = info.Status;
            parameters[5].Value = info.ApproveLeaderEmail;

            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.usp_UpdateMailInfoOfWFHistory", parameters);
            return dataTable;
        }
        public static DataTable InsertMailInfoOfWFHistoryInfo(WF_GetMailOfWFHistoryInfo info)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@SN",System.Data.SqlDbType.NVarChar,20),
            new SqlParameter("@FormTitle",System.Data.SqlDbType.NVarChar,300),
            new SqlParameter("@ApproveLeader",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@Status",System.Data.SqlDbType.NVarChar,10),
            new SqlParameter("@ApproveLeaderEmail",System.Data.SqlDbType.NVarChar,300)
            };
            parameters[0].Value = info.InstanceID;
            parameters[1].Value = info.SN;
            parameters[2].Value = info.FormTitle;
            parameters[3].Value = info.ApproveLeader;
            parameters[4].Value = info.Status;
            parameters[5].Value = info.ApproveLeaderEmail;

            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.usp_InsertMailInfoOfWFHistory", parameters);
            return dataTable;
        }
        public static DataTable DeleteMailOfWFHistoryInfo(WF_GetMailOfWFHistoryInfo info)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100)
            };
            parameters[0].Value = info.InstanceID;
            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.usp_DeleteMailInfoOfWFHistory", parameters);
            return dataTable;
        }
    }
}
