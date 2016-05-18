using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Common;
using System.Data;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business
{
    public class DBHelper
    {

        public static DataTable ExecuteDataTable(string SQL, CommandType type)
        {
            DataProvider dataProvider = GeDataProvider();
            DataTable dt = null;
            try
            {
                dt = dataProvider.ExecuteDataTable(SQL, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataProvider.Close();
            }
            return dt;

        }
        public static DataTable ExecutedProcedure(string strProcedureName, SqlParameter[] parameters)
        {
            DataProvider dataProvider = GeDataProvider();
            DataTable dt = null;
            try
            {
                dt = dataProvider.ExecutedProcedure(strProcedureName, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataProvider.Close();
            }
            return dt;

        }
        private static DataProvider GeDataProvider()
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            return dataProvider;
        }

        public static bool ExecuteNonQuery(string strSQL)
        {
            DataProvider dataProvider = GeDataProvider();
            int Number = 0;
            try
            {
                Number = dataProvider.ExecuteNonQuery(strSQL, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataProvider.Close();
            }
            return Number > 0 ? true : false;
        }
    }
}
