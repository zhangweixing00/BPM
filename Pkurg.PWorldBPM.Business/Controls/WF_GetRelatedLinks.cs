using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Pkurg.PWorld.Business.Common;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public partial class WF_GetRelatedLinks
    {
        public static string GetRelatedLinkByAppID(string appID)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@AppID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = appID;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetRelatedLinkByAppID", parameters);
            if (dataTable==null||dataTable.Rows.Count==0)
            {
                return "";
            }
            return dataTable.Rows[0]["AddressToLink"].ToString();
        }
        public static string GetRelatedLinks(string instanceID)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = instanceID;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetRelatedLinks", parameters);
            return dataTable.Rows[0]["AddressToLink"].ToString();
        }
        public static string GetRelatedClassName(string instanceID)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = instanceID;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetRelatedLinks", parameters);
            return dataTable.Rows[0]["ClassName"].ToString();
        }
        public static DataTable GetUserLists(string FormTitle, string CreateByUserName, string CreateATTime, string FinishedTime, int pageIndex, int pageSize,string ApproveStatusName,string FormID)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormTitle",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@CreateByUserName",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@CreateATTime",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@FinishedTime",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize", System.Data.SqlDbType.Int),
            new SqlParameter("@ApproveStatusName", System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar,200)
            };
            parameters[0].Value = FormTitle;
            parameters[1].Value = CreateByUserName; 
            parameters[2].Value = CreateATTime;
            parameters[3].Value = FinishedTime;
            parameters[4].Value = pageIndex;
            parameters[5].Value = pageSize;
            parameters[6].Value = ApproveStatusName;
            parameters[7].Value = FormID;

            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetUserLists", parameters);
            return dataTable;
        }

        public static DataTable GetAllUsersLists()
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            };
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetAllUsersLists", parameters);
            return dataTable;
        }

        public static DataTable UpdateProcessesStatus(string instanceID)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@instanceID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = instanceID;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_UpdateProcessesStatus", parameters);
            return dataTable;
        }

    }
}
