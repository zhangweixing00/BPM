using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using DBUtility;
using System.Data;
using Utility;
using IDAL;

namespace SQLServerDAL
{
    public class FormTemplateControlDAL : IFormTemplateControlDAL
    {
        public bool CreateFormTemplateControl(FormTemplateControlInfo info)
        {
            try
            {
                string sql = "P_K2_AddFormTemplateControl";

                SqlParameter[] paras = { 
                                            new SqlParameter("@FormTemplateID",info.FormTemplateID)
                                           ,new SqlParameter("@ControlID",info.ControlID)
                                           ,new SqlParameter("@CreatedBy", DBManager.GetCurrentUserAD())
                                        };

                SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormTemplateControlDAL.CreateFormTemplateControl", DBManager.GetCurrentUserAD());
                return false;
            }
        }
    }
}
