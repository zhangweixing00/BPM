using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_Process
    {
        /// <summary>
        /// 重新发送当前流程的邮件
        /// </summary>
        /// <param name="formID"></param>
        /// <returns></returns>
        public static List<string> ReSendEmailAndGetToDoUsers(string id)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@ID",System.Data.SqlDbType.NVarChar,100)
            };
            parameters[0].Value = id;

            DataTable dt = DBHelper.ExecutedProcedure("wf_usp_ReSendEmail", parameters);
            List<string> users = new List<string>();
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["PartId"] == null)
                    {
                        continue;
                    }
                    users.Add(item["PartId"].ToString());
                }
            }
            return users;
        }
    }
}
