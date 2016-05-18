using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using Utility;
using System.Data.SqlClient;
using DBUtility;
using System.Data;

namespace SQLServerDAL
{
    public class FormDesignDAL : IFormDesignDAL
    {
        /// <summary>
        /// 添加Control
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool CreateControl(ControlInfo info)
        {
            try
            {
                string sql = "P_K2_AddControl";

                SqlParameter[] paras = { 
                                            new SqlParameter("@Name",info.Name)
                                           ,new SqlParameter("@Type",info.Type)
                                           ,new SqlParameter("@Class",info.Class)
                                           ,new SqlParameter("@Json",info.Json)
                                           ,new SqlParameter("@Html",info.Html)
                                           ,new SqlParameter("@Desc",info.Description)
                                           ,new SqlParameter("@CreatedBy", DBManager.GetCurrentUserAD())
                                        };

                SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.CreateControl", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得所有控件
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCountrol()
        {
            try
            {
                string sql = "P_K2_GetALLControls";
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.GetAllCountrol", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 删除控件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DelControlById(Guid Id)
        {
            try
            {
                string sql = "P_K2_DelControlByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",Id)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.DelControlById", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 更新控件
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateControl(ControlInfo info)
        {
            try
            {
                string sql = "P_K2_UpdateControl";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",info.ID),
                                          new SqlParameter("@Name",info.Name),
                                          new SqlParameter("@Type",info.Type),
                                          new SqlParameter("@Class",info.Class),
                                          new SqlParameter("@Json",info.Json),
                                          new SqlParameter("@Html",info.Html),
                                          new SqlParameter("@Description",info.Description)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.UpdateControl", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得某个控件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetControlById(Guid Id)
        {
            try
            {
                string sql = "P_K2_GetControlByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",Id)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.GetControlById", DBManager.GetCurrentUserAD());
                return null;
            }
        }


        /// <summary>
        /// 取得所有表单模板库
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLFormTemplate()
        {
            try
            {
                string sql = "P_k2_GetFLDataSource";
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure,sql);
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.GetALLFormTemplate", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 更新表单模板
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateFormTemplate(FormTemplateInfo info)
        {
            try
            {
                string sql = "P_K2_UpdateFormTemplate";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",info.ID),
                                          new SqlParameter("@Name",info.Name),
                                          new SqlParameter("@Version",info.Version),
                                          new SqlParameter("@Description",info.Description)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.UpdateFormTemplate", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        public bool UpdateFormTemplateHtml(FormTemplateInfo info)
        {
            try
            {
                string sql = "P_K2_UpdateFormTemplateHtml";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",info.ID),
                                          new SqlParameter("@Html",info.Html)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.UpdateFormTemplateHtml", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 创建表单模板库
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool CreateFormTemplate(FormTemplateInfo info)
        {
            try
            {
                string sql = "P_K2_AddFormTemplate";
                SqlParameter[] parms ={
                                          new SqlParameter("@Name",info.Name),
                                          new SqlParameter("@Version",info.Version),
                                          new SqlParameter("@Html",info.Html),
                                          new SqlParameter("@Description",info.Description),
                                          new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD())
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.CreateFormTemplate", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得某个表单模板库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetFormTemplateById(Guid Id)
        {
            try
            {
                string sql = "P_K2_GetFormTemplateByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",Id)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.GetFormTemplateById", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 删除表单模板库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DelFormTemplateById(Guid Id)
        {
            try
            {
                string sql = "P_K2_DeleteFormTemplateByID";
                SqlParameter[] parms ={
                                      new SqlParameter("@ID",Id)
                                 };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormDesignDAL.DelFormTemplateById", DBManager.GetCurrentUserAD());
                return false;
            }
        }
    }
}
