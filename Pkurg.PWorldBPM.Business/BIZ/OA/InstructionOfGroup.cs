using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Common.Log;
using Pkurg.BPM.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.BIZ
{
    public class InstructionOfGroup
    {

        #region void

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(InstructionOfGroupInfo model)
        {
            string sql = string.Format("INSERT INTO Biz.OA_InstructionOfGroup (FormID,SecurityLevel,UrgenLevel,DeptName ,DeptCode,UserName," +
                        "Mobile, DateTime,Title,Content,LeadersSelected,IsReport,RelatedFormID, IsApproval) VALUES" +
                        "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", model.FormID,
                        model.SecurityLevel, model.UrgenLevel, model.DeptName, model.DeptCode,model.UserName, model.Mobile,
                        model.DateTime, model.Title,model.Content,model.LeadersSelected,model.IsReport,model.RelatedFormID,model.IsApproval);
            return DBHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(InstructionOfGroupInfo model)
        {
            string sql = string.Format("Update Biz.OA_InstructionOfGroup set SecurityLevel='{0}',UrgenLevel = '{1}',DeptName = '{2}',DeptCode='{3}'," +
                        "UserName='{4}',Mobile='{5}',DateTime='{6}',Title='{7}',Content='{8}',LeadersSelected='{9}',IsReport='{10}',IsApproval = '{11}',"+
                        "RelatedFormID='{12}' where FormID = '{13}'",
                        model.SecurityLevel, model.UrgenLevel, model.DeptName, model.DeptCode, model.UserName, model.Mobile, model.DateTime,
                        model.Title, model.Content,model.LeadersSelected, model.IsReport, model.IsApproval,model.RelatedFormID, model.FormID);
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
            string sql = string.Format("UPDATE Biz.OA_InstructionOfGroup SET  IsApproval = '{0}'"
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
            string sql = string.Format("DELETE FROM Biz.OA_InstructionOfGroup WHERE FormID='{0}'", FormID);
            return DBHelper.ExecuteNonQuery(sql);
        }

        public InstructionOfGroupInfo Get(string FormID)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = FormID;
            DataTable dt = DBHelper.ExecutedProcedure("Biz.OA_InstructionOfGroup_Get", parameters);
            InstructionOfGroupInfo Info = new InstructionOfGroupInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                Info.FormID = Assign(dt.Rows[0]["FormID"]);
                Info.SecurityLevel = Assign(dt.Rows[0]["SecurityLevel"]);
                Info.UrgenLevel = Assign(dt.Rows[0]["UrgenLevel"]);
                Info.DeptName = Assign(dt.Rows[0]["DeptName"]);
                Info.DeptCode = Assign(dt.Rows[0]["DeptCode"]);
                Info.UserName = Assign(dt.Rows[0]["UserName"]);
                Info.Mobile = Assign(dt.Rows[0]["Mobile"]);
                Info.DateTime = Assign(dt.Rows[0]["DateTime"]);
                Info.Title = Assign(dt.Rows[0]["Title"]);
                Info.Content = Assign(dt.Rows[0]["Content"]);
                Info.LeadersSelected = Assign(dt.Rows[0]["LeadersSelected"]);
                Info.IsReport = Assign(dt.Rows[0]["IsReport"]);
                Info.IsApproval = Assign(dt.Rows[0]["IsApproval"]);
                return Info;
            }

            return null;
        }

        private string Assign(object o)
        {
            return o == null ? "" : o.ToString();
        }
        #endregion
        /// <summary>
        /// app3003
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public InstructionOfGroupInfo GetInstructionOfGroupInfo(string formId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = formId;
            DataTable dt = DBHelper.ExecutedProcedure("Biz.OA_InstructionOfGroup_Get", parameters);
            InstructionOfGroupInfo Info = new InstructionOfGroupInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                Info.FormID = Assign(dt.Rows[0]["FormID"]);
                Info.SecurityLevel = Assign(dt.Rows[0]["SecurityLevel"]);
                Info.UrgenLevel = Assign(dt.Rows[0]["UrgenLevel"]);
                Info.DeptName = Assign(dt.Rows[0]["DeptName"]);
                Info.DeptCode = Assign(dt.Rows[0]["DeptCode"]);
                Info.UserName = Assign(dt.Rows[0]["UserName"]);
                Info.Mobile = Assign(dt.Rows[0]["Mobile"]);
                Info.DateTime = Assign(dt.Rows[0]["DateTime"]);
                Info.Title = Assign(dt.Rows[0]["Title"]);
                Info.Content = Assign(dt.Rows[0]["Content"]);
                Info.LeadersSelected = Assign(dt.Rows[0]["LeadersSelected"]);
                Info.IsReport = Assign(dt.Rows[0]["IsReport"]);
                Info.IsApproval = Assign(dt.Rows[0]["IsApproval"]);
                return Info;
            }

            return null;
        }
    }
}
