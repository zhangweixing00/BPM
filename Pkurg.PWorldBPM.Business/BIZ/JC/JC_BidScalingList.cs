using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Common.Log;
using Pkurg.BPM.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
    public class JC_BidScalingList
    {
        private string className = "JC_BidScalingList";

        #region void

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertBidScalingList(JC_BidScalingListInfo model)
        {
            string methodName = "InsertBidScalingList";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("JC_BidScalingListInfo={0}", model));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("INSERT INTO Biz.JC_BidScalingList (FormID,UserCode ,UserName,CreatTime, SelectResult) VALUES" +
                        "('{0}','{1}','{2}','{3}','{4}')", model.FormID,model.UserCode, model.UserName, model.CreatTime,model.SelectResult);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateBidScalingList(JC_BidScalingListInfo model)
        {
            string methodName = "UpdateBidScalingList";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("JC_BidScalingListInfo={0}", model));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("Update Biz.JC_BidScalingList set UserCode = '{0}',UserName='{1}',CreatTime='{2}',SelectResult='{3}' where FormID = '{18}'",
                        model.UserCode, model.UserName, model.CreatTime,model.SelectResult, model.FormID);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        public bool DeleteBidScalingList(string FormID)
        {
            string methodName = "DeleteBidScalingList";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("ID={0}", FormID));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("DELETE FROM Biz.JC_BidScalingList WHERE FormID='{0}'", FormID);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region return

        public JC_BidScalingListInfo GetBidScalingList(string FormID ,string UserCode)
        {
            string methodName = "GetBidScalingList";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("ID={0}", FormID));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar, 100)
                ,new SqlParameter("@UserCode", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = FormID;
            parameters[1].Value = UserCode;
            DataTable dt = db.ExecutedProcedure("Biz.JC_GetBidScalingList", parameters);
            JC_BidScalingListInfo jcInfo = new JC_BidScalingListInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                jcInfo.FormID = Assign(dt.Rows[0]["FormID"]);
                jcInfo.UserCode = Assign(dt.Rows[0]["UserCode"]);
                jcInfo.UserName = Assign(dt.Rows[0]["UserName"]);
                jcInfo.CreatTime = Assign(dt.Rows[0]["CreatTime"]);
                jcInfo.SelectResult = Assign(dt.Rows[0]["SelectResult"]);
                return jcInfo;
            }

            return null;
        }

        private string Assign(object o)
        {
            return o == null ? "" : o.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dept"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string GetDeptCode(string companyCode, string departName)
        {
            string methodName = "GetRoleUser";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("CompanyCode = {0}; UserCode = {1}", companyCode, departName));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ESSPConnectionString"].ConnectionString;
            string sql = string.Format("select DepartCode from T_Department where ParentDepartCode = '{0}' and DepartName = '{1}'",
                                        companyCode, departName);
            DataTable dt = db.ExecuteDataTable(sql, CommandType.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                return (string)dt.Rows[0]["DepartCode"];
            }
            return null;
        }
        #endregion
    }
}
