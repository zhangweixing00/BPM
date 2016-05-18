using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using System.Data;

namespace BLL
{
    public class PositionBLL
    {
        //创建dal连接
        private static readonly IPositionDAL dal = DALFactory.DataAccess.CreatePositionDAL();

        /// <summary>
        /// 取得职位信息
        /// </summary>
        /// <returns></returns>
        public IList<PositionInfo> GetPosition()
        {
            DataSet ds = dal.GetPosition();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<PositionInfo> pList = new List<PositionInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PositionInfo info = new PositionInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.PositionName = dr["PositionName"].ToString();
                    info.DeptCode = dr["DeptCode"].ToString();
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    pList.Add(info);
                }
                return pList;
            }
            return null;
        }
    }
}
