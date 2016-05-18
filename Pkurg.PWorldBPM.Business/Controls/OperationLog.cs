using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business.Controls
{
    /// <summary>
    /// 记录操作日志
    /// </summary>
    public class OperationLog
    {
        public static bool Log(string uName, string des, int opType)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@UserName",System.Data.SqlDbType.NVarChar,100),
                    new SqlParameter("@OpDes",System.Data.SqlDbType.Text),
                    new SqlParameter("@OpType",System.Data.SqlDbType.Int)
                };
                parameters[0].Value = uName;
                parameters[1].Value = des;
                parameters[2].Value = opType;
                DBHelper.ExecutedProcedure("wf_sys_Log_Operation_Create", parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
