using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Utility;
using DBUtility;
using IDAL;
using System.Data.SqlClient;

namespace SQLServerDAL
{
    public class WorkPlaceDAL : IWorkPlaceDAL
    {
        /// <summary>
        /// 取得所有的工作地点
        /// </summary>
        /// <returns></returns>
        public DataSet GetWorkPlace()
        {
            try
            {
                string sql = "SProc_Admin_GetWorkPlace";
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds;
                else
                    return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "WorkPlaceDAL.GetWorkPlace", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 添加或编辑工作地点信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="placeName"></param>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        public bool AddEditWorkPlace(Guid ID, string placeName, string placeCode)
        {
            try
            {
                string sql = "SProc_Admin_AddWorkPlace";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",ID),
                                          new SqlParameter("@PlaceName",placeName),
                                          new SqlParameter("@PlaceCode",placeCode),
                                          new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD())
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "WorkPlaceDAL.GetWorkPlace", DBManager.GetCurrentUserAD());
                return false;
            }
        }
    }
}
