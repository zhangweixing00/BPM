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
    public class PWorldRoleDAL : IPWorldRoleDAL
    {
        public DataSet GetRole()
        {
            try
            {
                string sql = "select RoleId,RoleName from PWorld.dbo.T_PmsRole where RoleType=0 order by OrderNo ";
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "PWorldRoleDAL.GetRole", DBManager.GetCurrentUserAD());
                return null;
            }
        }
       

       
    }
}
