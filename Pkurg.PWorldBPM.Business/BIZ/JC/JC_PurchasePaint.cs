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
    public class PurchasePaint
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(PurchasePaintInfo model)
        {
            string sql = string.Format("INSERT INTO Biz.JC_PurchasePaint (FormID,DeptName,DeptCode,UserName,Mobile,DateTime," +
                        "Title,Content,IsApproval) VALUES" +"('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                        model.FormID,model.DeptName, model.DeptCode, model.UserName, model.Mobile,
                        model.DateTime, model.Title, model.Content, model.IsApproval);
            return DBHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(PurchasePaintInfo model)
        {
            string sql = string.Format("Update Biz.JC_PurchasePaint set DeptName = '{0}',DeptCode='{1}'," +
                        "UserName='{2}',Mobile='{3}',DateTime='{4}',Title='{5}',Content='{6}',IsApproval = '{7}' where FormID = '{8}'",
                        model.DeptName, model.DeptCode, model.UserName, model.Mobile, model.DateTime,
                        model.Title, model.Content, model.IsApproval, model.FormID);
            return DBHelper.ExecuteNonQuery(sql);
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
            string sql = string.Format("UPDATE Biz.JC_PurchasePaint SET  IsApproval = '{0}'"
                                        + " where FormID = '{1}'", strStatus, ID);
            return DBHelper.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        public bool Delete(string FormID)
        {
            string sql = string.Format("DELETE FROM Biz.JC_PurchasePaint WHERE FormID='{0}'", FormID);
            return DBHelper.ExecuteNonQuery(sql);
        }

        public PurchasePaintInfo Get(string FormID)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = FormID;
            DataTable dt = DBHelper.ExecutedProcedure("Biz.JC_PurchasePaint_Get", parameters);
            PurchasePaintInfo Info = new PurchasePaintInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                Info.FormID = Assign(dt.Rows[0]["FormID"]);
                Info.DeptName = Assign(dt.Rows[0]["DeptName"]);
                Info.DeptCode = Assign(dt.Rows[0]["DeptCode"]);
                Info.UserName = Assign(dt.Rows[0]["UserName"]);
                Info.Mobile = Assign(dt.Rows[0]["Mobile"]);
                Info.DateTime = Assign(dt.Rows[0]["DateTime"]);
                Info.Title = Assign(dt.Rows[0]["Title"]);
                Info.Content = Assign(dt.Rows[0]["Content"]);
                Info.IsApproval = Assign(dt.Rows[0]["IsApproval"]);
                return Info;
            }

            return null;
        }
        public PurchasePaintInfo GetInfoByWfId(string wfInstanceId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@WFInstanceId", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = wfInstanceId;
            DataTable dt = DBHelper.ExecutedProcedure("Biz.JC_PurchasePaint_GetBywfId", parameters);
            PurchasePaintInfo Info = new PurchasePaintInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                Info.FormID = Assign(dt.Rows[0]["FormID"]);
                Info.DeptName = Assign(dt.Rows[0]["DeptName"]);
                Info.DeptCode = Assign(dt.Rows[0]["DeptCode"]);
                Info.UserName = Assign(dt.Rows[0]["UserName"]);
                Info.Mobile = Assign(dt.Rows[0]["Mobile"]);
                Info.DateTime = Assign(dt.Rows[0]["DateTime"]);
                Info.Title = Assign(dt.Rows[0]["Title"]);
                Info.Content = Assign(dt.Rows[0]["Content"]);
                Info.IsApproval = Assign(dt.Rows[0]["IsApproval"]);
                return Info;
            }

            return null;
        }

        private string Assign(object o)
        {
            return o == null ? "" : o.ToString();
        }
    }
}
