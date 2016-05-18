using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.BIZ.ERP
{
    public class ERP_PaymentApplication_Grade
    {
        public static int GetERP_PaymentApplication_GradeInfo(string type, string startDeptName, string isInPlan, string amounts)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@Type",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@startDeptName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@isInPlan",System.Data.SqlDbType.Int),
            new SqlParameter("@amounts",System.Data.SqlDbType.Decimal)
            };
            parameters[0].Value = type;
            parameters[1].Value = startDeptName;
            parameters[2].Value = isInPlan;
            parameters[3].Value = amounts;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_PaymentApplication_Grade_Get", parameters);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return int.Parse(dataTable.Rows[0][0].ToString());
            }
            return -1;
        }
    }
}
