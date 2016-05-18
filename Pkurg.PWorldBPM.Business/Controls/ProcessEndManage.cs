using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class ProcessEndManage
    {
        public static DataTable GetAllEndInfos()
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            };
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetAllEndInfo", parameters);
            return dataTable;
        }
        public static DataTable GetAppDictByAppID(string appId)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100)
            };
            parameters[0].Value = appId;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetAppDictByAppId", parameters);
            return dataTable;
        }
        public static DataTable GetSomeInfoByOthers(string AppName)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@AppName",System.Data.SqlDbType.NVarChar,500)
            };
            parameters[0].Value = AppName;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetSomeInfoByOthers", parameters);
            return dataTable;
        }
        public static void EditAllEndInfos(ProcessEndManageInfo info)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@AppCode",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@AddressToLink",System.Data.SqlDbType.VarChar,1000),
            new SqlParameter("@CreateTime",System.Data.SqlDbType.DateTime),
            new SqlParameter("@ClassName",System.Data.SqlDbType.VarChar,1000)
            };
            parameters[0].Value = info.AppCode;
            parameters[1].Value = info.AddressToLink;
            parameters[2].Value = info.CreateTime;
            parameters[3].Value = info.ClassName;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_EditAllEndInfos", parameters);
        }
        public static DataTable GetAppEndByAppID(string appId)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100)
            };
            parameters[0].Value = appId;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetAppEndByAppId", parameters);
            return dataTable;
        }
        public static DataTable EditEndInfos(string AppCode, string AddressToLink, string ClassName)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@AppCode",System.Data.SqlDbType.VarChar,50),
            new SqlParameter("@AddressToLink",System.Data.SqlDbType.VarChar,1000),
            new SqlParameter("@ClassName",System.Data.SqlDbType.VarChar,1000)
            };
            parameters[0].Value = AppCode;
            parameters[1].Value = AddressToLink;
            parameters[2].Value = ClassName;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_EditEndInfos", parameters);
            return dataTable;
        }
        public static DataTable InsertEndInfos(string AppCode)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@AppCode",System.Data.SqlDbType.VarChar,50)
            };
            parameters[0].Value = AppCode;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_InsertEndInfos", parameters);
            return dataTable;
        }
        public static DataTable InsertEndInfo(string AppCode, string AddressToLink, string ClassName)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@AppCode",System.Data.SqlDbType.VarChar,50),
            new SqlParameter("@AddressToLink",System.Data.SqlDbType.VarChar,1000),
            new SqlParameter("@ClassName",System.Data.SqlDbType.VarChar,1000)
            };
            parameters[0].Value = AppCode;
            parameters[1].Value = AddressToLink;
            parameters[2].Value = ClassName;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_InsertEndInfo", parameters);
            return dataTable;
        }

        public static DataTable AlterEndInfos(string AppId)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@AppCode",System.Data.SqlDbType.VarChar,50)
            };
            parameters[0].Value = AppId;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_InsertEndInfos", parameters);
            return dataTable;
        }
     }
}
