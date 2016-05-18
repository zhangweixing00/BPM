using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Services.DAL;

namespace Pkurg.PWorldBPM.Services.BLL
{
    public class PWorldUserInfoBLL
    {
        #region Return

        /// <summary>
        /// 获取需要归档的数据列表
        /// </summary>
        /// <returns></returns>
        public V_Pworld_UserInfo_All GetUserInfo(string userCode)
        {
            using (Linq2BPMDataContext db = new Linq2BPMDataContext())
            {
                return db.V_Pworld_UserInfo_All.Where(m => m.EmployeeCode.Trim().ToLower() == userCode.Trim().ToLower()).FirstOrDefault();
            }
        }

        #endregion Return
    }
}
