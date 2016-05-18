using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business.Controls
{
   public class WF_MailOfWF
    {
       public static string connectionString = "";
       public static void AddMailOfWFInfo(WF_GetMailOfWFInfo info)
       {
           DataTable dt = GetMailOfWFInfoBySN(info.SN,info.ApproveLeaderEmail);
           if (dt == null || dt.Rows.Count == 0)
           {
               InsertMailInfoOfWFInfo(info);
           }
           else
           {
               UpdateMailInfoOfWFInfo(info.SN, info.Status);
           }
       }
        //public static DataTable GetMailOfWFInfo(WF_GetMailOfWFInfo info)
        //{
        //    DataProvider dataProvider = new DataProvider();
        //    dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        //    SqlParameter[] parameters = new SqlParameter[]{
        //    new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100)
        //    };
        //    parameters[0].Value = info.InstanceID;
        //    DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetMailInfoByInstID", parameters);
        //    return dataTable;
        //}
        public static DataTable GetMailOfWFInfoBySN(string SN,string email)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = connectionString;// System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@SN",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@email",System.Data.SqlDbType.NVarChar,1000)
            };
            parameters[0].Value = SN;
            parameters[1].Value = email;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetMailInfoBySN", parameters);
            return dataTable;
        }
        public static DataTable UpdateMailInfoOfWFInfo(string SN,string Status)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = connectionString;// System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@SN",System.Data.SqlDbType.NVarChar,20),
            new SqlParameter("@Status",System.Data.SqlDbType.NVarChar,10)
            };
            parameters[0].Value =SN;
            parameters[1].Value =Status;

            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_UpdateMailInfoOfWF", parameters);
            return dataTable;
        }
        public static DataTable InsertMailInfoOfWFInfo(WF_GetMailOfWFInfo info)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = connectionString;// System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
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

            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_InsertMailInfoOfWF", parameters);
            return dataTable;
        }
        //public static DataTable DeleteMailOfWFInfo(WF_GetMailOfWFInfo info)
        //{
        //    DataProvider dataProvider = new DataProvider();
        //    dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
        //    SqlParameter[] parameters = new SqlParameter[]{
        //    new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100)
        //    };
        //    parameters[0].Value = info.InstanceID;
        //    DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_DeleteMailInfoOfWF", parameters);
        //    return dataTable;
        //}
    }
}
