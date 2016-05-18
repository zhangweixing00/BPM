using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility;
using Utility;
using IDAL;
using System.Data.SqlClient;

namespace SQLServerDAL
{
    public class ProcessTypeDAL : IProcessTypeDAL
    {
        public DataSet GetProcessType()
        {
            try
            {
                string sql = "SProc_Admin_GetProcessType";

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "ProcessTypeDAL.GetProcessType", DBManager.GetCurrentUserAD());
                return null;
            }
        }
        public DataSet GetProcessTypeByID(string ID)
        {
            try
            {
                string sql = "SProc_Admin_GetProcessTypeByID";
                SqlParameter[] paras = { 
                                        new SqlParameter("@ID",ID)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "ProcessTypeDAL.GetProcessTypeByID", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        public DataSet GetPWorldCompanyData()
        {
            try
            {
                string sql = "select DepartCode,DepartName from dbo.T_Department where DeptType='0' order by OrderNo asc";
                
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringPWorld, CommandType.Text, sql);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "ProcessTypeDAL.GetPWorldCompanyData", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 取得K2流程名称
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataSet GetProcessNameByID(string  ID)
        {
            try
            {
                string sql = "SProc_Admin_GetProcessNameByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ProcessID",ID)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "ProcessTypeDAL.GetProcessNameByID", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}
