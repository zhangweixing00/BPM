using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using Utility;
using IDAL;

namespace SQLServerDAL
{
    public class MyJoinedDAL : IMyJoined
    {
        public DataSet GetMyJoined(string actionerName, string folio, string startTime, string endTime, string pagenum, string pagesize, string procFullName, string submitor, string paraEmpression)
        {
            try
            {
                string sql = "P_K2_GetMyDoc";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ActionerName", actionerName),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@StartTime", startTime),
                                        new SqlParameter("@EndTime", endTime),
                                        new SqlParameter("@PageNum", pagenum),
                                        new SqlParameter("@PageSize", pagesize),
                                        new SqlParameter("@ProcFullName", procFullName),
                                        new SqlParameter("@Submitor", submitor),
                                        new SqlParameter("@ParaEmpression", paraEmpression)
                                   };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "MyJoinedDAL.GetMyJoined", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}
