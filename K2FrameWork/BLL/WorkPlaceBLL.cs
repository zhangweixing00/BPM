using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;
using Model;

namespace BLL
{
    public class WorkPlaceBLL
    {
        //创建dal连接
        private static readonly IWorkPlaceDAL dal = DALFactory.DataAccess.CreateWorkPlaceDAL();

        /// <summary>
        /// 取得所有的工作地点
        /// </summary>
        /// <returns></returns>
        public List<WorkPlaceInfo> GetWorkPlace()
        {
            DataSet ds = dal.GetWorkPlace();
            if (ds != null)
            {
                List<WorkPlaceInfo> wpList = new List<WorkPlaceInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WorkPlaceInfo info = new WorkPlaceInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.PlaceName = dr["PlaceName"].ToString();
                    info.PlaceCode = dr["PlaceCode"].ToString();
                    info.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    wpList.Add(info);
                }
                return wpList;
            }
            return null;
        }

        /// <summary>
        /// 通过ID取得工作地点信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public WorkPlaceInfo GetWorkPlaceByID(string Id)
        {
            List<WorkPlaceInfo> wpList = GetWorkPlace();
            if (wpList != null)
            {
                return wpList.Find(delegate(WorkPlaceInfo info)
                {
                    if (info.ID.ToString().Equals(Id, StringComparison.OrdinalIgnoreCase))
                        return true;
                    else
                        return false;
                });
            }
            return null;
        }

        /// <summary>
        /// 通过Code取得工作地点信息
        /// </summary>
        /// <param name="workPlace"></param>
        /// <returns></returns>
        public WorkPlaceInfo GetWorkPlaceByCode(string workPlace)
        {
            List<WorkPlaceInfo> wpList = GetWorkPlace();
            if (wpList != null)
            {
                return wpList.Find(delegate(WorkPlaceInfo info)
                {
                    if (info.PlaceCode.Equals(workPlace, StringComparison.OrdinalIgnoreCase))
                        return true;
                    else
                        return false;
                });
            }
            return null;
        }

        /// <summary>
        /// 判断是否已经存在工作地点编码
        /// </summary>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        public bool IsExists(string placeCode)
        {
            List<WorkPlaceInfo> wpList = GetWorkPlace();
            if (wpList != null)
            {
                return wpList.Exists(delegate(WorkPlaceInfo info)
                {
                    if (info.PlaceCode.Equals(placeCode, StringComparison.OrdinalIgnoreCase))
                        return true;
                    else
                        return false;
                });
            }
            return false;
        }

        /// <summary>
        /// 添加或编辑工作地点信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="placeName"></param>
        /// <param name="placeCode"></param>
        /// <returns></returns>
        public bool AddEditWorkPlace(string id, string placeName, string placeCode)
        {
            Guid ID = Guid.Empty;
            try
            {
                ID = new Guid(id);
            }
            catch
            {
                return false;
            }
            return dal.AddEditWorkPlace(ID, placeName, placeCode);
        }
    }
}
