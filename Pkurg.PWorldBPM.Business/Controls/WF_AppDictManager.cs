using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_AppDictManager
    {
        public static DataTable GetAppDictByAppName(string appName)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appName",System.Data.SqlDbType.VarChar,500),
            };
            parameters[0].Value = appName;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetAppDictByAppName", parameters);
            return dataTable;
        }

        public static DataTable GetAppDictToDataTable()
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            DataTable dataTable = DBHelper.ExecuteDataTable("select * from [WF_AppDict]", CommandType.Text);
            return dataTable;
        }

        public static DataTable GetAppDictByAppID(string appId)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100),
            };
            parameters[0].Value = appId;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetAppDictByAppId", parameters);
            return dataTable;
        }

        public static void EditAppDict(string appId, string appName, string workFlowName, string formName, bool isOpen)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100),
            new SqlParameter("@appName",System.Data.SqlDbType.VarChar,500),
            new SqlParameter("@workFlowName",System.Data.SqlDbType.VarChar,500),
            new SqlParameter("@formName",System.Data.SqlDbType.VarChar,1000),
            new SqlParameter("@IsOpen",System.Data.SqlDbType.Int)
            };
            parameters[0].Value = appId;
            parameters[1].Value = appName;
            parameters[2].Value = workFlowName;
            parameters[3].Value = formName;
            parameters[4].Value = isOpen?1:0;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_EditAppDict", parameters);
        }

        public static DataTable GetAppListByAppIdOrAppNameOrWorkFlowNameOrFormName(string appId, string appName, string workFlowName, string formName)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100),
            new SqlParameter("@appName",System.Data.SqlDbType.VarChar,500),
            new SqlParameter("@workFlowName",System.Data.SqlDbType.VarChar,500),
            new SqlParameter("@formName",System.Data.SqlDbType.VarChar,1000),
            };
            parameters[0].Value = appId;
            parameters[1].Value = appName;
            parameters[2].Value = workFlowName;
            parameters[3].Value = formName;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetAppListByAppIdOrAppNameOrWorkFlowNameOrFormName", parameters);
            return dataTable;
        }
    }
}
