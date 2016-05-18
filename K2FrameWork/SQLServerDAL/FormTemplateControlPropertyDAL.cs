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
    public class FormTemplateControlPropertyDAL : IFormTemplateControlPropertyDAL
    {
        public bool CreateFormTemplateControlProperty(FormTemplateControlPropertyInfo info)
        {
            try
            {
                string sql = "P_K2_AddFormTemplateControlProperty";

                SqlParameter[] paras = { 
                                            new SqlParameter("@ControlID",info.ControlID)
                                           ,new SqlParameter("@Name", info.Name)
                                           ,new SqlParameter("@Value",info.Value)
                                           ,new SqlParameter("@Type",info.Type)
                                           ,new SqlParameter("@DefaultValue",info.DefaultValue)
                                           ,new SqlParameter("@Group",info.Group)
                                        };

                SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "FormTemplateControlPropertyDAL.CreateFormTemplateControlProperty", DBManager.GetCurrentUserAD());
                return false;
            }
        }
    }
}
