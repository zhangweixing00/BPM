using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class EmailTask
    {
        public static string connectionString = "";
        public static List<Pkurg.PWorldBPM.Business.Controls.EmailTaskInfo> GetAllEmailInfo()
        {

            List<Pkurg.PWorldBPM.Business.Controls.EmailTaskInfo> infos = new List<EmailTaskInfo>();
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = connectionString;// System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            };
            DataTable dt = dataProvider.ExecutedProcedure("wf_Email_GetToDoList", parameters);
            foreach (DataRow item in dt.Rows)
            {
                if (item["SumitTime"] == null || string.IsNullOrEmpty(item["SumitTime"].ToString()))
                {
                    continue;
                }
                infos.Add(new EmailTaskInfo()
                {
                    AppName = item["AppName"].ToString(),
                    CreateByUserName = item["CreateByUserName"].ToString(),
                    InstanceID = item["InstanceID"].ToString(),
                    CreateDeptName = item["CreateDeptName"].ToString(),
                    Sn = item["Sn"].ToString(),
                    Email = item["Email"].ToString(),
                    SumitTime = DateTime.Parse(item["SumitTime"].ToString())
                });
            }
            return infos;
        }

        /// <summary>
        /// 根据流程Id调用wf_Email_GetFinishInstInfo
        /// </summary>
        /// <param name="wfId"></param>
        /// <returns></returns>
        public static FinallyEmailTaskInfo GetFinallyEmailTaskInfo(string wfId)
        {
            FinallyEmailTaskInfo finallyEmailTaskInfo = new FinallyEmailTaskInfo();
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = connectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@wfInstId",System.Data.SqlDbType.NVarChar,100)
            };
            parameters[0].Value = wfId;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_Email_GetFinishInstInfo", parameters);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                finallyEmailTaskInfo.InstanceID = dataTable.Rows[0]["InstanceID"].ToString();
                finallyEmailTaskInfo.CreateDeptName = dataTable.Rows[0]["CreateDeptName"].ToString();
                finallyEmailTaskInfo.CreateByUserName = dataTable.Rows[0]["CreateByUserName"].ToString();
                finallyEmailTaskInfo.AppName = dataTable.Rows[0]["AppName"].ToString();

                if (dataTable.Rows[0]["SumitTime"] != null && dataTable.Rows[0]["SumitTime"].ToString() != "")
                {
                    finallyEmailTaskInfo.SumitTime = DateTime.Parse(dataTable.Rows[0]["SumitTime"].ToString());
                }
                else
                {
                    finallyEmailTaskInfo.SumitTime = DateTime.MinValue;
                }

                finallyEmailTaskInfo.Email = dataTable.Rows[0]["Email"].ToString();

                return finallyEmailTaskInfo;
            }

            else
            {
                return null;
            }

        }
    }
}
