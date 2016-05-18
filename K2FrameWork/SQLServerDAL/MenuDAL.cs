using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Utility;
using DBUtility;
using System.Data.SqlClient;
using IDAL;

namespace SQLServerDAL
{
    public class MenuDAL : IMenu
    {
        public DataSet GetMenuPermision(string user)
        {
            try
            {
                string sql = "SProc_GetMenuPermision";

                SqlParameter[] paras = { 
                                        new SqlParameter("@user",user)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "MenuDAL.GetMenuPermision", DBManager.GetCurrentUserAD());
                return null;
            }
        }
        public DataSet GetMenuPermisionByRoleID(string RoleID)
        {
            try
            {
                string sql = "SProc_GetMenuPermisionByRoleID";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleID",RoleID)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "MenuDAL.GetMenuPermisionByRoleID", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        public DataSet GetMenuInfo(string MenuName, string ParentMenuGuid, string MenuType)
        {
            try
            {
                string sql = "SProc_GetMenuInfo";

                SqlParameter[] paras = { 
                                        new SqlParameter("@MenuName",MenuName),
                                        new SqlParameter("@ParentMenuGuid",ParentMenuGuid),
                                        new SqlParameter("@MenuType",MenuType)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "MenuDAL.GetMenuInfo", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        public bool DeleteMenuInfo(string MenuGuids)
        {
            try
            {
                string sql = "SProc_DeleteMenuInfo";

                SqlParameter[] paras = { 
                                        new SqlParameter("@MenuGuids",MenuGuids)
                                   };

                SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "MenuDAL.UpdateMenuInfo", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        public bool CreateMenuInfo(string MenuGuid, string ParentMenuGuid, string MenuName, string MenuURL, string MenuType)
        {
            try
            {
                string sql = "SProc_AddMenuInfo";

                SqlParameter[] paras = { 
                                            new SqlParameter("@MenuGuid",MenuGuid)
                                           ,new SqlParameter("@ParentMenuGuid",ParentMenuGuid)
                                           ,new SqlParameter("@MenuName",MenuName)
                                           ,new SqlParameter("@MenuURL",MenuURL)
                                           ,new SqlParameter("@MenuType",MenuType)
                                           ,new SqlParameter("@CreatedBy", DBManager.GetCurrentUserAD())
                                        };

                SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "MenuDAL.CreateMenuInfo", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        public bool UpdateMenuInfo(string MenuGuid, string ParentMenuGuid, string MenuName, string MenuURL, string MenuType)
        {
            try
            {
                string sql = "SProc_UpdateMenuInfo";

                SqlParameter[] paras = { 
                                            new SqlParameter("@MenuGuid",MenuGuid)
                                           ,new SqlParameter("@ParentMenuGuid",ParentMenuGuid)
                                           ,new SqlParameter("@MenuName",MenuName)
                                           ,new SqlParameter("@MenuURL",MenuURL)
                                           ,new SqlParameter("@MenuType",MenuType)
                                        };

                SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "MenuDAL.UpdateMenuInfo", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        public bool UpdateMenuPermision(string MenuGuids, string RoleGuid)
        {
            try
            {
                string sql = "SProc_UpdateMenuPermision";

                SqlParameter[] paras = { 
                                            new SqlParameter("@MenuGuids",MenuGuids)
                                           ,new SqlParameter("@RoleGuid",RoleGuid)
                                           ,new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD())
                                        };

                SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "MenuDAL.UpdateMenuPermision", DBManager.GetCurrentUserAD());
                return false;
            }
        }
    }
}
