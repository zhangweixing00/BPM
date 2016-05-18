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
    public class CadresOrRemoval
    {
        #region void

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(CadresOrRemovalInfo model)
        {
            string sql = string.Format("INSERT INTO Biz.HR_CadresOrRemoval (FormID,chkCadresOrRemoval,CadresName,LocationCompanyDeptJob ,CadresCompanyDeptJob,CadresContent," +
                        "RemovalName, LocationCompanyDeptJobR,RemovalCompanyDeptjob,RemovalContent,LeadersSelected, IsApproval，IsGroup) VALUES" +
                        "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", model.FormID,
                        model.chkCadresOrRemoval, model.CadresName, model.LocationCompanyDeptJob, model.CadresCompanyDeptJob,model.CadresContent, model.RemovalName,
                        model.LocationCompanyDeptJobR, model.RemovalCompanyDeptjob,model.RemovalContent,model.LeadersSelected,model.IsApproval,model.IsGroup);
            return DBHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(CadresOrRemovalInfo model)
        {
            string sql = string.Format("Update Biz.HR_CadresOrRemoval set chkCadresOrRemoval='{0}',CadresName = '{1}',LocationCompanyDeptJob = '{2}',CadresCompanyDeptJob='{3}'," +
                        "CadresContent='{4}',RemovalName='{5}',LocationCompanyDeptJobR='{6}',RemovalCompanyDeptjob='{7}',RemovalContent='{8}',LeadersSelected='{9}',IsApproval = '{10}',IsGroup='{11}' where FormID = '{11}'",
                        model.chkCadresOrRemoval, model.CadresName, model.LocationCompanyDeptJob, model.CadresCompanyDeptJob, model.CadresContent, model.RemovalName, model.LocationCompanyDeptJobR,
                        model.RemovalCompanyDeptjob, model.RemovalContent, model.LeadersSelected, model.IsApproval, model.IsGroup,model.FormID);
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
            string sql = string.Format("UPDATE Biz.HR_CadresOrRemoval SET  IsApproval = '{0}'"
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
            string sql = string.Format("DELETE FROM Biz.HR_CadresOrRemoval WHERE FormID='{0}'", FormID);
            return DBHelper.ExecuteNonQuery(sql);
        }

        public CadresOrRemovalInfo Get(string FormID)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = FormID;
            DataTable dt = DBHelper.ExecutedProcedure("Biz.HR_CadresOrRemoval_Get", parameters);
            CadresOrRemovalInfo Info = new CadresOrRemovalInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                Info.FormID = Assign(dt.Rows[0]["FormID"]);
                Info.chkCadresOrRemoval = Assign(dt.Rows[0]["chkCadresOrRemoval"]);
                Info.CadresName = Assign(dt.Rows[0]["CadresName"]);
                Info.LocationCompanyDeptJob = Assign(dt.Rows[0]["LocationCompanyDeptJob"]);
                Info.CadresCompanyDeptJob = Assign(dt.Rows[0]["CadresCompanyDeptJob"]);
                Info.CadresContent = Assign(dt.Rows[0]["CadresContent"]);
                Info.RemovalName = Assign(dt.Rows[0]["RemovalName"]);
                Info.LocationCompanyDeptJobR = Assign(dt.Rows[0]["LocationCompanyDeptJobR"]);
                Info.RemovalCompanyDeptjob = Assign(dt.Rows[0]["RemovalCompanyDeptjob"]);
                Info.RemovalContent = Assign(dt.Rows[0]["RemovalContent"]);
                Info.LeadersSelected = Assign(dt.Rows[0]["LeadersSelected"]);
                Info.IsApproval = Assign(dt.Rows[0]["IsApproval"]);
                Info.IsGroup = Assign(dt.Rows[0]["IsGroup"]);
                return Info;
            }

            return null;
        }

        private string Assign(object o)
        {
            return o == null ? "" : o.ToString();
        }
        #endregion
    }
}
