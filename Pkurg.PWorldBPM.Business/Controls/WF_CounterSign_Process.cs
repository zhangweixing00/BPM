using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_CounterSign_Process
    {
        public static void SyncCounterSignData(string instanceID, List<WF_CountersignParameter> items)
        {
            DeleteCountersign(instanceID);

            for (int i = 0; i < items.Count; i++)
            {
                InsertCountersign(instanceID, items[i]);
            }
        }


        public static void InsertCountersign(string instanceID, WF_CountersignParameter item)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,50),
                new SqlParameter("@CountersignDeptCode",System.Data.SqlDbType.NVarChar,50),
                new SqlParameter("@CreateByUserCode",System.Data.SqlDbType.NVarChar,50),
                new SqlParameter("@CreateByUserName",System.Data.SqlDbType.NVarChar,50),
                new SqlParameter("@CountersignDeptName",System.Data.SqlDbType.NVarChar,50),
                new SqlParameter("@seCode",System.Data.SqlDbType.NVarChar,50)
            };
            parameters[0].Value = instanceID;
            parameters[1].Value = item.CountersignDeptCode;
            parameters[2].Value = item.CreateByUserCode;
            parameters[3].Value = item.CreateByUserName;
            parameters[4].Value = item.CountersignDeptName;
            parameters[5].Value = item.SeCode;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_InsertCountersign", parameters);
        }

        public static void DeleteCountersign(string InstanceID)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,50)
            };
            parameters[0].Value = InstanceID;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_DeleteCountersign", parameters);
        }
    }
}