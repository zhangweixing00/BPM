using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Pkurg.PWorld.Business.Common;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class Sys_ProcessManage
    {
        //修改为分页，每次返回的数据少，提高性能
        public static DataTable GetInstanceListByParamsPaged(string FormTitle, string CreateByUserName, string CreateATTime, string FinishedTime, int pageIndex, int pageSize, string ApproveStatusName, string FormID, string companyCode, int userRight,out int count)
        {
            count = 0;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormTitle",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@CreateByUserName",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@CreateATTime",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@FinishedTime",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize", System.Data.SqlDbType.Int),
            new SqlParameter("@ApproveStatusName", System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@companyCode", System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@userRight", System.Data.SqlDbType.Int),
            new SqlParameter("@count", System.Data.SqlDbType.Int)
            };
            parameters[0].Value = FormTitle;
            parameters[1].Value = CreateByUserName;
            parameters[2].Value = CreateATTime;
            parameters[3].Value = FinishedTime;
            parameters[4].Value = pageIndex;
            parameters[5].Value = pageSize;
            parameters[6].Value = ApproveStatusName;
            parameters[7].Value = FormID;
            parameters[8].Value = companyCode;
            parameters[9].Value = userRight;
            parameters[10].Value = userRight;
            parameters[10].Direction = ParameterDirection.Output;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_sys_GetInstanceListByParams_Paged", parameters);
            count = Convert.ToInt16(parameters[10].Value);
            return dataTable;
        }

        public static DataTable GetInstanceListByParams(string FormTitle, string CreateByUserName, string CreateATTime, string FinishedTime, int pageIndex, int pageSize, string ApproveStatusName, string FormID, string companyCode, int userRight)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormTitle",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@CreateByUserName",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@CreateATTime",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@FinishedTime",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize", System.Data.SqlDbType.Int),
            new SqlParameter("@ApproveStatusName", System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@companyCode", System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@userRight", System.Data.SqlDbType.Int),
            };
            parameters[0].Value = FormTitle;
            parameters[1].Value = CreateByUserName;
            parameters[2].Value = CreateATTime;
            parameters[3].Value = FinishedTime;
            parameters[4].Value = pageIndex;
            parameters[5].Value = pageSize;
            parameters[6].Value = ApproveStatusName;
            parameters[7].Value = FormID;
            parameters[8].Value = companyCode;
            parameters[9].Value = userRight;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_sys_GetInstanceListByParams", parameters);
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
