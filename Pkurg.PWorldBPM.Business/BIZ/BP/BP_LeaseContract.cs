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
    public class BP_LeaseContract
    {
        private string className = "BP_LeaseContract";

        #region void

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertLeaseContract(BP_LeaseContractInfo model)
        {
            string methodName = "InsertLeaseContract";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("BP_LeaseContractInfo={0}", model));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("INSERT INTO Biz.BP_LeaseContract (FormID,SecurityLevel,UrgenLevel," +
                        "ReportCode, StartDeptCode,DeptName,UserName,Mobile,Date,ReportTitle,BizType,BizID,ApproveFlag,Reason,Url ," +
                        "DecorationContract,ServiceContract,CompensationContract,ModificationContract,SupplementContract,LesseeContract,Remark," +
                        "CreateByUserCode,CreateByUserName,CreateAtTime,UpdateByUserCode,UpdateByUserName,ApproveStatus) VALUES" +
                        "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}')", 
                        model.FormID,model.SecurityLevel, model.UrgenLevel, model.ReportCode, model.StartDeptCode, model.DeptName, model.UserName, model.Mobile,
                        model.Date, model.ReportTitle, model.BizType, model.BizID,model.ApproveFlag, model.Reason,model.Url, model.DecorationContract,model.ModificationContract,
                        model.ServiceContract,model.CompensationContract,model.SupplementContract,model.LesseeContract,model.Remark,
                         model.CreateByUserCode, model.CreateByUserName,model.CreateAtTime, model.UpdateByUserCode, model.UpdateByUserName, model.ApproveStatus);
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
        public bool UpdateLeaseContract(BP_LeaseContractInfo model)
        {
            string methodName = "UpdateLeaseContract";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("BP_LeaseContractInfo={0}", model));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("Update Biz.BP_LeaseContract set SecurityLevel='{0}',UrgenLevel = '{1}',ReportCode = '{2}', StartDeptCode='{3}'," +
                        "DeptName='{4}',UserName='{5}',Mobile='{6}',Date='{7}', ReportTitle='{8}',BizType='{9}',BizID='{10}',Reason='{11}',Url='{12}'," +
                        "DecorationContract='{13}',ServiceContract='{14}',CompensationContract='{15}',ModificationContract='{16}',SupplementContract='{17}',LesseeContract='{18}'," +
                        "Remark='{19}',UpdateByUserCode ='{20}',UpdateByUserName = '{21}',SumitTime ='{22}',ApproveStatus ='{23}',ApproveFlag={24} where FormID = '{25}'",
                        model.SecurityLevel, model.UrgenLevel, model.ReportCode,model.StartDeptCode, model.DeptName, model.UserName, 
                        model.Mobile, model.Date, model.ReportTitle ,model.BizType, model.BizID, model.Reason,model.Url, 
                        model.DecorationContract,model.ServiceContract,model.CompensationContract,model.ModificationContract, model.SupplementContract, model.LesseeContract, 
                        model.Remark,model.UpdateByUserCode, model.UpdateByUserName,model.SumitTime, model.ApproveStatus, model.ApproveFlag, model.FormID);
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
            string sql = string.Format("UPDATE Biz.BP_LeaseContract SET SumitTime = '{0}', ApproveStatus = '{1}'"
                                        + " where FormID = '{2}'", DateTime.Now, strStatus, ID);
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
        public bool DeleteLeaseContract(string FormID)
        {
            string methodName = "DeleteLeaseContract";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("ID={0}", FormID));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            string sql = string.Format("DELETE FROM Biz.BP_LeaseContract WHERE FormID='{0}'", FormID);
            if (db.ExecuteNonQuery(sql, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region return

        public BP_LeaseContractInfo GetLeaseContract(string FormID)
        {
            string methodName = "GetLeaseContract";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("ID={0}", FormID));
            DataProvider db = new DataProvider();
            db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = FormID;
            DataTable dt = db.ExecutedProcedure("Biz.BP_GetLeaseContract", parameters);
            BP_LeaseContractInfo jcInfo = new BP_LeaseContractInfo();
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
                
                if (dt.Rows[0]["BizType"] != null)
                {
                    jcInfo.BizType = Convert.ToInt32(dt.Rows[0]["BizType"].ToString());
                }
                if (dt.Rows[0]["BizID"] != null)
                {
                    jcInfo.BizID = Convert.ToInt32(dt.Rows[0]["BizID"].ToString());
                }
                if (dt.Rows[0]["ApproveFlag"] != null)
                {
                    jcInfo.ApproveFlag = Convert.ToInt16(dt.Rows[0]["ApproveFlag"].ToString());
                }
                jcInfo.Reason = Assign(dt.Rows[0]["Reason"]);
                jcInfo.Url = Assign(dt.Rows[0]["Url"]);
                if (dt.Rows[0]["DecorationContract"] != null)
                {
                    jcInfo.DecorationContract = Convert.ToInt16(dt.Rows[0]["DecorationContract"].ToString());
                }
                if (dt.Rows[0]["ServiceContract"] != null)
                {
                    jcInfo.ServiceContract = Convert.ToInt16(dt.Rows[0]["ServiceContract"].ToString());
                }
                if (dt.Rows[0]["CompensationContract"] != null)
                {
                    jcInfo.CompensationContract = Convert.ToInt16(dt.Rows[0]["CompensationContract"].ToString());
                }
                if (dt.Rows[0]["ModificationContract"] != null)
                {
                    jcInfo.ModificationContract = Convert.ToInt16(dt.Rows[0]["ModificationContract"].ToString());
                }
                if (dt.Rows[0]["SupplementContract"] != null)
                {
                    jcInfo.SupplementContract = Convert.ToInt16(dt.Rows[0]["SupplementContract"].ToString());
                }
                if (dt.Rows[0]["LesseeContract"] != null)
                {
                    jcInfo.LesseeContract = Convert.ToInt16(dt.Rows[0]["LesseeContract"].ToString());
                }
                jcInfo.Remark = Assign(dt.Rows[0]["Remark"]);
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
