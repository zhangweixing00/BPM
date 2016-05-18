using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_Instance
    {
        public static DataTable GetInterfaceList(string loginName, string procName)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@loginid",System.Data.SqlDbType.NVarChar,100)
            };
            parameters[0].Value = loginName;
            return DBHelper.ExecutedProcedure(string.Format("wf_app_{0}", procName), parameters);
        }

        public static DataTable GetInterfaceList_Archive(string userCode,int pageIndex, int pageSize, out int count)
        {
            count = 0;
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;

            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@UserCode",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize",System.Data.SqlDbType.Int),
            new SqlParameter("@Count",System.Data.SqlDbType.Int)
                 };
            parameters[0].Value = userCode;
            parameters[1].Value = pageIndex;
            parameters[2].Value = pageSize;
            parameters[3].Value = count;
            parameters[3].Direction = ParameterDirection.Output;
            DataTable dt = db.ExecutedProcedure("wf_app_GetArchiveList", parameters);

            count = Convert.ToInt32(parameters[3].Value);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public static void UpdateFormDataByProcID(string instanceID, string formData)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@instanceID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@formData",System.Data.SqlDbType.Xml)
            };
            parameters[0].Value = instanceID;
            parameters[1].Value = formData;
            DBHelper.ExecutedProcedure("wf_usp_UpdateFormDataByProcID", parameters);
        }

        public static DataTable GetInterfaceList_DoneList(string userCode, int pageIndex, int pageSize, out int count)
        {
            count = 0;
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;

            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@UserCode",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@PageIndex", System.Data.SqlDbType.Int),
            new SqlParameter("@PageSize",System.Data.SqlDbType.Int),
            new SqlParameter("@Count",System.Data.SqlDbType.Int)
                 };
            parameters[0].Value = userCode;
            parameters[1].Value = pageIndex;
            parameters[2].Value = pageSize;
            parameters[3].Value = count;
            parameters[3].Direction = ParameterDirection.Output;
            DataTable dt = db.ExecutedProcedure("wf_app_GetDoneListPaged", parameters);

            count = Convert.ToInt32(parameters[3].Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
    }
}
