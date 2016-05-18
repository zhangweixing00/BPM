using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_ApprovalBox
    {
        public DataTable GetWorkItems(string procId,string activityName)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@procId",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@activityName",System.Data.SqlDbType.NVarChar,100),
                };
            parameters[0].Value = procId;
            parameters[1].Value = activityName;

            DataTable dt = DBHelper.ExecutedProcedure("wf_usp_GetWorkListByActivityName", parameters);

            return dt;
        }
    }
}
