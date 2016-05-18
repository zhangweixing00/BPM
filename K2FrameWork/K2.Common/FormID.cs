using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace K2.Common
{
    public class FormID
    {
        public static readonly string ConnectionStringTransaction = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        /// <summary>
        /// 生成表单ID
        /// </summary>
        public static string Generate()
        {

            string formID = "";
            return formID;
        }

        /// <summary>
        /// 生成 serials NO
        /// </summary>

        public static string SerialsNO(string flowName)
        {

            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@nvchrFlowName", flowName);
            paras[1] = new SqlParameter("@Indate", DateTime.Now);
            paras[2] = new SqlParameter("@intClaimIDIndex", 0);
            paras[2].Direction = ParameterDirection.Output;

            SQLHelper.ExecuteNonQuery(ConnectionStringTransaction, CommandType.StoredProcedure, "SProc_Admin_GetFormIDIndex", paras);
            int serialsNOIndex = Convert.ToInt32(paras[2].Value.ToString());

            //flowID 在页面中硬编码发送过来！
            string serialsNO = flowName.ToUpper() + string.Format("{0:yyyyMMdd}", DateTime.Now) + string.Format("{0:d4}", serialsNOIndex);
            //string serialsNO = shortName.ToUpper() + string.Format("{0:d6}", serialsNOIndex);

            return serialsNO;

        }

        public static string GenerateBatchID(string batchName)
        {
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@nvchrFlowName", batchName);
            paras[1] = new SqlParameter("@Indate", DateTime.Now);
            paras[2] = new SqlParameter("@intClaimIDIndex", 0);
            paras[2].Direction = ParameterDirection.Output;

            SQLHelper.ExecuteNonQuery(ConnectionStringTransaction, CommandType.StoredProcedure, "SProc_Admin_GetFormIDIndex", paras);
            int serialsNOIndex = Convert.ToInt32(paras[2].Value.ToString());

            //flowID 在页面中硬编码发送过来！
            string BatchID = batchName.ToUpper() + string.Format("{0:yyyyMMdd}", DateTime.Now) + string.Format("{0:d3}", serialsNOIndex);
            //string serialsNO = shortName.ToUpper() + string.Format("{0:d6}", serialsNOIndex);

            return BatchID;

        }

        public static string GenerateBatchID(string batchName, string time)
        {
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@nvchrFlowName", batchName);
            paras[1] = new SqlParameter("@Indate", Convert.ToDateTime(time.Trim()));
            paras[2] = new SqlParameter("@intClaimIDIndex", 0);
            paras[2].Direction = ParameterDirection.Output;

            SQLHelper.ExecuteNonQuery(ConnectionStringTransaction, CommandType.StoredProcedure, "SProc_Admin_GetFormIDIndex", paras);
            int serialsNOIndex = Convert.ToInt32(paras[2].Value.ToString());

            //flowID 在页面中硬编码发送过来！
            string BatchID = batchName.ToUpper() + Convert.ToDateTime(time).ToString("yyyyMMdd") + string.Format("{0:d3}", serialsNOIndex);
            //string serialsNO = shortName.ToUpper() + string.Format("{0:d6}", serialsNOIndex);

            return BatchID;

        }

    }
}
