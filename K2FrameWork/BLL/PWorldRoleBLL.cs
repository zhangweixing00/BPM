using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using IDAL;

namespace BLL
{
   public class PWorldRoleBLL
    {

        //创建dal连接
        private static readonly IPWorldRoleDAL  dal = DALFactory.DataAccess.CreatePWorldRoleDAL();

        public IList<PWorldRoleInfo> GetPworldRole()
        {
            DataSet ds = dal.GetRole();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<PWorldRoleInfo> ptiList = new List<PWorldRoleInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PWorldRoleInfo info = new PWorldRoleInfo();
                    info.RoleId = dr["RoleId"].ToString();
                    info.RoleName = dr["RoleName"].ToString();
                    ptiList.Add(info);
                }
                return ptiList;
            }
            return null;
        }

     

       
    }
}
