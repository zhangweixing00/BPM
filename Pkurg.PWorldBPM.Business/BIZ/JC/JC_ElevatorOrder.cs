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
    public class JC_ElevatorOrder
    {
        private string className = "JC_ElevatorOrder";

        #region void

        /// <summary>
        /// 添加请示单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertElevatorOrder(JC_ElevatorOrderInfo model)
        {
            string methodName = "InsertElevatorOrder";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("JC_ElevatorOrderInfo={0}", model));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("INSERT INTO Biz.JC_ElevatorOrder (FormID,SecurityLevel,UrgenLevel," +
                        "ReportCode, StartDeptCode,DeptName ,UserName,Mobile,Date,OrderType,OrderID ,Url ,Note," +
                        "CreateByUserCode,CreateByUserName ,CreateAtTime ,ApproveStatus,ReportTitle, UpdateByUserCode, UpdateByUserName,MaxCost) VALUES" +
                        "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',{20})", model.FormID,
                        model.SecurityLevel, model.UrgenLevel, model.ReportCode, model.StartDeptCode, model.DeptName, model.UserName, model.Mobile,
                        model.Date, model.OrderType, model.OrderID, model.Url, model.Note, model.CreateByUserCode, model.CreateByUserName,
                        model.CreateAtTime, model.ApproveStatus, model.ReportTitle, model.UpdateByUserCode, model.UpdateByUserName, model.MaxCost);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新请示单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateElevatorOrder(JC_ElevatorOrderInfo model)
        {
            string methodName = "UpdateElevatorOrder";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("JC_ElevatorOrderInfo={0}", model));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("Update Biz.JC_ElevatorOrder set SecurityLevel='{0}',UrgenLevel = '{1}'," +
                        "ReportCode = '{2}',DeptName='{3}',UserName='{4}',Mobile='{5}',Date='{6}',OrderType='{7}',OrderID='{8}',Url='{9}'," +
                        "UpdateByUserCode ='{10}',UpdateByUserName = '{11}',SumitTime ='{12}',ApproveStatus ='{13}', ReportTitle='{14}', Note='{15}', StartDeptCode='{16}',MaxCost={17} where FormID = '{18}'",
                        model.SecurityLevel, model.UrgenLevel, model.ReportCode, model.DeptName, model.UserName, model.Mobile,
                        model.Date, model.OrderType, model.OrderID, model.Url, model.UpdateByUserCode, model.UpdateByUserName,
                        model.SumitTime, model.ApproveStatus, model.ReportTitle, model.Note, model.StartDeptCode, model.MaxCost, model.FormID);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="strStatus"></param>
        /// <param name="wfInstanceId"></param>
        /// <returns></returns>
        public bool UpdateStatus(string ID, string strStatus)
        {
            string methodName = "UpdateStatus";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN + ":" + string.Format("ID={0};Status={1}", ID, strStatus));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("UPDATE Biz.JC_ElevatorOrder SET SumitTime = '{0}', ApproveStatus = '{1}'"
                                        + " where FormID = '{2}'", DateTime.Now, strStatus, ID);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除请示单
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        public bool DeleteElevatorOrder(string FormID)
        {
            string methodName = "DeleteElevatorOrder";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("ID={0}", FormID));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("DELETE FROM Biz.JC_ElevatorOrder WHERE FormID='{0}'", FormID);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region return

        public JC_ElevatorOrderInfo GetElevatorOrder(string FormID)
        {
            string methodName = "GetElevatorOrder";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("ID={0}", FormID));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = FormID;
            DataTable dt = db.ExecutedProcedure("Biz.JC_GetElevatorOrder", parameters);
            JC_ElevatorOrderInfo jcInfo = new JC_ElevatorOrderInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                jcInfo.FormID = Assign(dt.Rows[0]["FormID"]);
                //TODO:此处有问题
                if (dt.Rows[0]["SecurityLevel"] != null)
                {
                    jcInfo.SecurityLevel = Convert.ToInt16(dt.Rows[0]["SecurityLevel"].ToString());
                }
                if (dt.Rows[0]["UrgenLevel"] != null)
                {
                    jcInfo.UrgenLevel = Convert.ToInt16(dt.Rows[0]["UrgenLevel"].ToString());
                }
                jcInfo.StartDeptCode = Assign(dt.Rows[0]["StartDeptCode"]);
                jcInfo.DeptName = Assign(dt.Rows[0]["DeptName"]);
                jcInfo.UserName = Assign(dt.Rows[0]["UserName"]);
                if (dt.Rows[0]["Date"] != null)
                {
                    jcInfo.Date = (DateTime)dt.Rows[0]["Date"];
                }
                jcInfo.Mobile = Assign(dt.Rows[0]["Mobile"]);
                jcInfo.ReportCode = Assign(dt.Rows[0]["ReportCode"]);
                jcInfo.ReportTitle = Assign(dt.Rows[0]["ReportTitle"]);
                jcInfo.Url = Assign(dt.Rows[0]["Url"]);
                if (dt.Rows[0]["MaxCost"] != null && dt.Rows[0]["MaxCost"] != System.DBNull.Value)
                {
                    jcInfo.MaxCost = Convert.ToDecimal(dt.Rows[0]["MaxCost"]);
                }
                jcInfo.Note = Assign(dt.Rows[0]["Note"]);
                jcInfo.OrderType = Assign(dt.Rows[0]["OrderType"]);
                jcInfo.OrderID = Assign(dt.Rows[0]["OrderID"]);
                jcInfo.CreateByUserName = Assign(dt.Rows[0]["CreateByUserName"]);
                jcInfo.CreateByUserCode = Assign(dt.Rows[0]["CreateByUserCode"]);
                jcInfo.CreateAtTime = (DateTime)dt.Rows[0]["CreateAtTime"];
                jcInfo.UpdateByUserCode = Assign(dt.Rows[0]["UpdateByUserCode"]);
                jcInfo.UpdateByUserName = Assign(dt.Rows[0]["UpdateByUserName"]);

                if (dt.Rows[0]["SumitTime"] != System.DBNull.Value)
                {
                    jcInfo.SumitTime = (DateTime)dt.Rows[0]["SumitTime"];
                }
                jcInfo.ApproveStatus = Assign(dt.Rows[0]["ApproveStatus"]);
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
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("CompanyCode = {0}; DeptName = {1}", companyCode, departName));
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
