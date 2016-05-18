using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Utility;
using DBUtility;
using IDAL;

namespace SQLServerDAL
{
    public class MyApplicationDAL : IMyApplication
    {
        public DataSet GetMyApplication(string actionerName, string folio, string startTime,string endTime,string pagenum, string pagesize, string procFullName,  string paraEmpression)
        {
            try
            {
                string sql = "SProc_GetMyApplication";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserName", actionerName),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@StartTime", startTime),
                                        new SqlParameter("@EndTime", endTime),
                                        new SqlParameter("@PageNum", pagenum),
                                        new SqlParameter("@PageSize", pagesize),
                                        new SqlParameter("@ProcFullName", procFullName),
                                        new SqlParameter("@ParaEmpression", paraEmpression)
                                   };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "MyApplicationDAL.GetMyApplication", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}
