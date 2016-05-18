using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Pkurg.PWorldBPM.FinallyDisposeServices;

namespace Pkurg.PWorldBPM.FinallyDispose
{
    public class WorkflowInstDispose
    {
        public static ConnectionType connectionType = ConnectionType.测试;

        public static void Dispose(int k2_workflowId, bool isStartNewWorkflow)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = "Data Source=172.25.20.47;Initial Catalog=BPM;Persist Security Info=True;User ID=sa;Password=Founder@2014; Max Pool Size=150;Connect Timeout=500";

            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@workflowId",System.Data.SqlDbType.NVarChar,20)
                };
            parameters[0].Value = k2_workflowId.ToString();
            DataTable dt = dataProvider.ExecutedProcedure("wf_usp_GetWorkFlowInstanceListAndUpdate", parameters);

            if (dt != null || dt.Rows.Count == 0)
            {
                return;
            }

            string instId = dt.Rows[0]["InstanceID"].ToString();
            string formId = dt.Rows[0]["FormID"].ToString();

            if (isStartNewWorkflow)
            {
                //调用保存函数
            }
        }

        public static bool Dispose(int k2_workflowId, string startUser, string spName)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.SetConnectionType(connectionType);
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@workflowId",System.Data.SqlDbType.NVarChar,20)
                };
            parameters[0].Value = k2_workflowId.ToString();
            DataTable dt = dataProvider.ExecutedProcedure("wf_usp_GetWorkFlowInstanceListAndUpdate", parameters);

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            try
            {
                string instId = dt.Rows[0]["InstanceID"].ToString();
                string formId = dt.Rows[0]["FormID"].ToString();

                if (!string.IsNullOrEmpty(startUser))
                {
                    //调用保存函数
                    startUser = startUser.ToLower().Replace("k2:founder\\", "").Replace("founder\\", "");
                    new CreateNewFormService().CreateNewFormByInstanceIDAndLoginNameWithStoredProcedure(instId, startUser, spName);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("WorkflowInstDispose--Ex:{0}", ex.Message);
                return false;
            }

        }

        public static void Dispose(int k2_workflowId, Dictionary<string, string> dataFields)
        {
            try
            {
                SerializableDictionary<string, string> new_dataFields = new SerializableDictionary<string, string>();
                foreach (KeyValuePair<string, string> item in dataFields)
                {
                    new_dataFields.Add(item.Key, item.Value);
                }
                WF_GetRelatedLinks.connectionType = connectionType;
                string url = WF_GetRelatedLinks.GetRelatedLinks(k2_workflowId.ToString());
                DynamicWebService service = new DynamicWebService(url);
                //InstanceService service = new InstanceService(url);
                service.DoServiceEvent(k2_workflowId, new_dataFields);


            }
            catch
            {

            }

        }
    }
}
