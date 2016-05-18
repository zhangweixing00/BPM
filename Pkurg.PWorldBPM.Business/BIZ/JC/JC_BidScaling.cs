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
    public class JC_BidScaling
    {
        private string className = "JC_BidScaling";

        #region void

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertBidScaling(JC_BidScalingInfo model)
        {
            string methodName = "InsertBidScaling";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("JC_BidScalingInfo={0}", model));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("INSERT INTO Biz.JC_BidScaling (FormID,StartDeptCode,Title,DeptName ,UserName," +
                        "DateTime, Content,EntranceTime,IsAccreditByGroup,FirstLevel,FirstUnit ,SecondUnit ,ScalingResult,BidCommittee, IsApproval) VALUES" +
                        "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", model.FormID,
                        model.StartDeptCode, model.Title,model.DeptName, model.UserName, model.DateTime,model.Content,model.EntranceTime,
                        model.IsAccreditByGroup, model.FirstLevel, model.FirstUnit, model.SecondUnit, model.ScalingResult, model.BidCommittee, model.IsApproval);
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
        public bool UpdateBidScaling(JC_BidScalingInfo model)
        {
            string methodName = "UpdateBidScaling";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("JC_BidScalingInfo={0}", model));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("Update Biz.JC_BidScaling set StartDeptCode='{0}',Title = '{1}',DeptName = '{2}',UserName='{3}'," +
                        "DateTime='{4}',Content='{5}',EntranceTime='{6}',IsAccreditByGroup='{7}',FirstLevel='{8}',FirstUnit='{9}',SecondUnit='{10}'," +
                        "ScalingResult ='{11}',BidCommittee = '{12}',IsApproval = '{13}' where FormID = '{14}'",
                        model.StartDeptCode,model.Title,model.DeptName, model.UserName, model.DateTime,model.Content,model.EntranceTime,
                        model.IsAccreditByGroup,model.FirstLevel, model.FirstUnit, model.SecondUnit, model.ScalingResult, model.BidCommittee, model.IsApproval, model.FormID);
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
            string sql = string.Format("UPDATE Biz.JC_BidScaling SET  IsApproval = '{0}'"
                                        + " where FormID = '{1}'", strStatus, ID);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新BidCommittee
        /// </summary>
        public bool UpdateBidCommittee(string ID, string BidCommittee)
        {
            string methodName = "UpdateBidCommittee";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN + ":" + string.Format("ID={0};Status={1}", ID, BidCommittee));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("UPDATE Biz.JC_BidScaling SET  BidCommittee = '{0}'"
                                        + " where FormID = '{1}'", BidCommittee, ID);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新ScalingResult
        /// </summary>
        public bool UpdateScalingResult(string ID, string ScalingResult)
        {
            string methodName = "UpdateScalingResult";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN + ":" + string.Format("ID={0};Status={1}", ID, ScalingResult));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("UPDATE Biz.JC_BidScaling SET  ScalingResult = '{0}'"
                                        + " where FormID = '{1}'", ScalingResult, ID);
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
        public bool DeleteBidScaling(string FormID)
        {
            string methodName = "DeleteBidScaling";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("ID={0}", FormID));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("DELETE FROM Biz.JC_BidScaling WHERE FormID='{0}'", FormID);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region return

        public JC_BidScalingInfo GetBidScaling(string FormID)
        {
            string methodName = "GetBidScaling";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("ID={0}", FormID));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = FormID;
            DataTable dt = db.ExecutedProcedure("Biz.JC_GetBidScaling", parameters);
            JC_BidScalingInfo jcInfo = new JC_BidScalingInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                jcInfo.FormID = Assign(dt.Rows[0]["FormID"]);
                jcInfo.StartDeptCode = Assign(dt.Rows[0]["StartDeptCode"]);
                jcInfo.Title = Assign(dt.Rows[0]["Title"]);
                jcInfo.DeptName = Assign(dt.Rows[0]["DeptName"]);
                jcInfo.UserName = Assign(dt.Rows[0]["UserName"]);
                jcInfo.DateTime = Assign(dt.Rows[0]["DateTime"]);
                jcInfo.Content = Assign(dt.Rows[0]["Content"]);
                jcInfo.EntranceTime = Assign(dt.Rows[0]["EntranceTime"]);
                jcInfo.IsAccreditByGroup = Assign(dt.Rows[0]["IsAccreditByGroup"]);
                jcInfo.FirstLevel = Assign(dt.Rows[0]["FirstLevel"]);
                jcInfo.FirstUnit = Assign(dt.Rows[0]["FirstUnit"]);
                jcInfo.SecondUnit = Assign(dt.Rows[0]["SecondUnit"]);
                jcInfo.ScalingResult = Assign(dt.Rows[0]["ScalingResult"]);
                jcInfo.BidCommittee = Assign(dt.Rows[0]["BidCommittee"]);
                jcInfo.IsApproval = Assign(dt.Rows[0]["IsApproval"]);
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
        /// <summary>
        /// 通过formid得到JC_BidScaling的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static JC_BidScalingInfo GetJC_BidScalingInfoByWfId(string id)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("ID",SqlDbType.NVarChar,100)
                                      };
            parameters[0].Value = id;

            JC_FinalistApprovalInfo model = new JC_FinalistApprovalInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.JC_BidScalingInfoByWfId_Get", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private static JC_BidScalingInfo DataRowToModel(DataRow dataRow)
        {
            JC_BidScalingInfo info = new JC_BidScalingInfo();

            if (dataRow != null)
            {
                if (dataRow["FormID"] != null)
                {
                    info.FormID = dataRow["FormID"].ToString();
                }
                if (dataRow["StartDeptCode"] != null)
                {
                    info.StartDeptCode = dataRow["StartDeptCode"].ToString();
                }
                if (dataRow["Title"] != null)
                {
                    info.Title = dataRow["Title"].ToString();
                }
                if (dataRow["DeptName"] != null)
                {
                    info.DeptName = dataRow["DeptName"].ToString();
                }
                if (dataRow["UserName"] != null)
                {
                    info.UserName = dataRow["UserName"].ToString();
                }
                if (dataRow["DateTime"] != null)
                {
                    info.DateTime = dataRow["DateTime"].ToString();
                }
                if (dataRow["Content"] != null)
                {
                    info.Content = dataRow["Content"].ToString();
                }
                if (dataRow["EntranceTime"] != null)
                {
                    info.EntranceTime = dataRow["EntranceTime"].ToString();
                }
                if (dataRow["IsAccreditByGroup"] != null)
                {
                    info.IsAccreditByGroup = dataRow["IsAccreditByGroup"].ToString();
                }
                if (dataRow["FirstLevel"] != null)
                {
                    info.FirstLevel = dataRow["FirstLevel"].ToString();
                }
                if (dataRow["FirstUnit"] != null)
                {
                    info.FirstUnit = dataRow["FirstUnit"].ToString();
                }
                if (dataRow["SecondUnit"] != null)
                {
                    info.SecondUnit = dataRow["SecondUnit"].ToString();
                }
                if (dataRow["ScalingResult"] != null)
                {
                    info.ScalingResult = dataRow["ScalingResult"].ToString();
                }
                if (dataRow["BidCommittee"] != null)
                {
                    info.BidCommittee = dataRow["BidCommittee"].ToString();
                }
                if (dataRow["IsApproval"] != null)
                {
                    info.IsApproval = dataRow["IsApproval"].ToString();
                }
            }
            return info;
        }
        /// <summary>
        /// 通过formid更新IsApproval字段
        /// </summary>
        /// <param name="model"></param>
        public static JC_BidScalingInfo UpdateJC_BidScalingInfoInfoByModel(JC_BidScalingInfo model)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsApproval",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = model.FormID;
            parameters[1].Value = model.IsApproval;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_BidScalingInfoByID_Update", parameters);

            return model;
        }

        /// <summary>
        /// 根据流程ID获取JC_BidScalingInfo
        /// </summary>
        /// <param name="ProId"></param>
        /// <returns></returns>
        public static JC_BidScalingInfo GetJC_JC_BidScalingInfoByFormId(string ProId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
               new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = ProId;
            JC_BidScalingInfo model = new JC_BidScalingInfo();
            DataTable datatable = DBHelper.ExecutedProcedure("Biz.JC_BidScalingInfoByFormID_Get", parameters);
            if (datatable != null && datatable.Rows.Count > 0)
            {
                return DataRowToModel(datatable.Rows[0]);
            }
            else
            {
                return null;
            } 
        }

        public JC_BidScalingInfo GetBidScalingInfo(string formId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
               new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = formId;
            JC_BidScalingInfo model = new JC_BidScalingInfo();
            DataTable datatable = DBHelper.ExecutedProcedure("Biz.JC_GetBidScaling", parameters);
            if (datatable != null && datatable.Rows.Count > 0)
            {
                return DataRowToModel(datatable.Rows[0]);
            }
            else
            {
                return null;
            } 
        }
    }
}
