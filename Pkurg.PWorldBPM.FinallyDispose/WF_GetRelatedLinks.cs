using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Pkurg.PWorldBPM.FinallyDispose;

namespace Pkurg.PWorldBPM.FinallyDispose
{
    public class WF_GetRelatedLinks
    {
        public static ConnectionType connectionType = ConnectionType.测试;
        public static string GetRelatedClassName(string instanceID)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.SetConnectionType(connectionType);
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = instanceID;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetRelatedLinks", parameters);
            return dataTable.Rows[0]["ClassName"].ToString();
        }
        public static string GetRelatedLinks(string instanceID)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.SetConnectionType(connectionType);
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@InstanceID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = instanceID;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetRelatedLinks", parameters);
            return dataTable.Rows[0]["AddressToLink"].ToString();
        }
    }
}
