using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;
using DBUtility;
using Utility;

namespace SQLServerDAL
{
    public class PositionDAL:IPositionDAL
    {
        /// <summary>
        /// 取得职位信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetPosition()
        {
            try
            {
                string sql = "SProc_Admin_GetPosition";
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringPWorld, CommandType.StoredProcedure, sql);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "PositionDAL.GetPosition", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}
