using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.BIZ.Test
{
    public class TestProc
    {
        public static bool AddTestProc(TestProcInfo info)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Title",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Content",System.Data.SqlDbType.NVarChar,5000)
            };
            parameters[0].Value = info.FormId;
            parameters[1].Value = info.Title;
            parameters[2].Value = info.Content;

            dataProvider.ExecutedProcedure("Biz.Test_AddTestProc", parameters);
            return true;
        }
        public static TestProcInfo GetTestProcInfo(string formID)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100)
            };
            parameters[0].Value = formID;

            DataTable dt = dataProvider.ExecutedProcedure("Biz.Test_GetTestProc", parameters);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            return new TestProcInfo()
            {
                FormId = formID,
                Title = dt.Rows[0]["Title"].ToString(),
                Content = dt.Rows[0]["Content"].ToString()
            };
        }

    }
}
