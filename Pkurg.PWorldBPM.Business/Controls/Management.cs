using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Common;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Controls
{
    /// <summary>
    /// 流程处理数据管理类
    /// </summary>
    public class Management
    {
        /// <summary>
        /// 根据流程实例ID获取记录
        /// </summary>
        /// <param name="WFInstanceId"></param>
        /// <returns></returns>
        public System.Data.DataTable GetFlowInstance(int WFInstanceId)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            return dataProvider.ExecuteDataTable(string.Format("select * from [dbo].[WF_WorkFlowInstance] where WFInstanceId='{0}'", WFInstanceId), CommandType.Text);
        }

        /// <summary>
        /// 流程跳转--更新数据库
        /// </summary>
        /// <param name="procInstID"></param>
        /// <param name="activityId"></param>
        /// <param name="activityName"></param>
        /// <returns></returns>
        public bool GoToActitvy(int procInstID, string activityId, string activityName)
        {
            string sql = string.Format("UPDATE WF_WorkFlowInstance SET WorkItemCode='{1}',WorkItemName='{2}',UpdateAtTime='{3}' WHERE WFInstanceId={0}",
                procInstID, activityId, activityName, DateTime.Now);

            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            int count = db.ExecuteNonQuery(sql, System.Data.CommandType.Text);
            return count > 0;
        }

        /// <summary>
        /// 终止流程-更新数据库
        /// </summary>
        /// <param name="procInstID"></param>
        /// <returns></returns>
        public bool StopActitvy(int procInstID)
        {
            string sql = string.Format("UPDATE WF_WorkFlowInstance SET WFStatus='{1}',FinishedTime='{2}',UpdateAtTime='{2}' WHERE WFInstanceId={0}",
                procInstID, "5", DateTime.Now);

            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            int count = db.ExecuteNonQuery(sql, System.Data.CommandType.Text);
            return count > 0;
        }       
    }
}
