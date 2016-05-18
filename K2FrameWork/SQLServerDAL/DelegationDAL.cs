using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using DBUtility;
using Utility;
using System.Data;
using IDAL;

namespace SQLServerDAL
{
    public class DelegationDAL : IDelegation
    {
        public bool CreateDelegation(DelegationInfo info)
        {
            try
            {
                string sql = "SProc_AddDelegation";
                SqlParameter[] paras = {          
                                               new SqlParameter("@ProcName",info.ProcName)
                                               , new SqlParameter("@ActivityName",info.ActivityName)
                                               , new SqlParameter("@Conditions",info.Conditions)
                                               , new SqlParameter("@FromUser",info.FromUser)
                                               , new SqlParameter("@ToUser",info.ToUser)
                                               , new SqlParameter("@StartDate",info.StartDate)
                                               , new SqlParameter("@EndDate",info.EndDate)
                                               , new SqlParameter("@CreateDate",info.CreateDate)
                                               , new SqlParameter("@CreatedByUser",DBManager.GetCurrentUserAD())
                                               , new SqlParameter("@Remark",info.Remark)
                                               , new SqlParameter("@State",info.State)
                                       };

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DelegationDAL.CreateDelegation", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        public DataSet GetDelegation(string ProcName, string FromUser,string ToUser, string StartDate, string EndDate, string State)
        {
            try
            {
                string sql = "SProc_GetDelegation";
                SqlParameter[] paras = {          
                                               new SqlParameter("@ProcName",ProcName)
                                               , new SqlParameter("@FromUser",FromUser)
                                               , new SqlParameter("@ToUser",ToUser)
                                               , new SqlParameter("@StartDate",StartDate)
                                               , new SqlParameter("@EndDate",EndDate)
                                               , new SqlParameter("@State",State)
                                       };

                DataSet ds=SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DelegationDAL.CreateDelegation", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}
