using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using System.Data;

namespace BLL
{
    public class DepartmentBLL
    {
        //创建dal连接
        private static readonly IDepartment dal = DALFactory.DataAccess.CreateDepartmentDAL();

        /// <summary>
        /// 取得指定的部门信息
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public IList<DepartmentInfo> GetSubDepartments(string orgCode, string deptCode)
        {
            return dal.GetSubDepartments(orgCode, deptCode);
        
        }

        /// <summary>
        /// 取得部门类别
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptType()
        {
            DataSet ds = dal.GetDeptType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// 取得部门
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable GetSortDepartment(string orgID)
        {
            DataSet ds = dal.GetSortDepartment(orgID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// 获取整个组织结构
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrgStruct()
        {
            OrganizationBLL bll = new OrganizationBLL();
            DataTable dt = bll.GetOrgOfDeptType();
            DataTable dt2 = bll.GetOrgOfDeptType();
            
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["DeptCode"] = dr["DeptCode"].ToString();
                    DataTable tp = GetSortDepartment(dr["DeptCode"].ToString());
                    DataRow[] drs = tp.Select("ParentCode = 'R0'");
                    if (drs != null && drs.Length > 0)
                    {
                        foreach (DataRow d in drs)
                        {
                            d["ParentCode"] = dr["DeptCode"];
                        }
                    }
                    dt2.Merge(tp);
                    dt2.AcceptChanges();
                }
            }
            return dt2;
        }

        /// <summary>
        /// 取得单一部门信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DepartmentInfo GetDepartmentInfo(string deptCode)
        {
            DataSet ds = dal.GetDepartmentInfo(deptCode);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DepartmentInfo info = new DepartmentInfo();
                DataTable dt = ds.Tables[0];
                info.Abbreviation = dt.Rows[0]["Abbreviation"].ToString();
                info.Code = dt.Rows[0]["Code"].ToString();
                info.DeptName = dt.Rows[0]["Department"].ToString();
                info.DeptCode = dt.Rows[0]["DeptCode"].ToString();
                info.Description = dt.Rows[0]["Description"].ToString();
                info.State = Convert.ToInt32(dt.Rows[0]["State"].ToString());
                info.Levels = Convert.ToInt32(dt.Rows[0]["Levels"].ToString());
                info.ParentCode = dt.Rows[0]["ParentCode"].ToString();
                info.ParentDepartment = dt.Rows[0]["ParentDepartment"].ToString();
                info.DeptTypeCode = dt.Rows[0]["DeptTypeCode"].ToString();
                info.DeptTypeCode = dt.Rows[0]["DeptType"].ToString();
                int orderno = 0;
                int.TryParse(dt.Rows[0]["OrderNO"].ToString(), out orderno);
                info.OrderNo = orderno;
                return info;
            }
            return null;
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="dept"></param>
        /// <param name="shouldOrderNO"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool CreateDepartment(DepartmentInfo dept, string shouldOrderNO, string action)
        {
            return dal.CreateDepartment(dept, shouldOrderNO, action);
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="dept"></param>
        /// <param name="shouldOrderNO"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool UpdateDepartment(DepartmentInfo dept, string shouldOrderNO, string action)
        {
            return dal.UpdateDepartment(dept, shouldOrderNO, action);
        }

        /// <summary>
        /// 取得用户所在部门
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public IList<DepartmentInfo> GetDepartmentByUserCode(string usercode)
        {
            DataSet ds = dal.GetDepartmentByUserCode(usercode);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<DepartmentInfo> deptList = new List<DepartmentInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DepartmentInfo info = new DepartmentInfo();
                    info.DeptName = dr["DeptName"].ToString();
                    info.DeptCode = dr["DeptCode"].ToString();
                    try
                    {
                        info.IsMainDept = Boolean.Parse(dr["IsMainDept"].ToString());
                    }
                    catch
                    {
                        info.IsMainDept = false;
                    }
                    deptList.Add(info);
                }
                return deptList;
            }
            return null;
        }

        /// <summary>
        /// 删除部门用户
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="userCodes"></param>
        /// <returns></returns>
        public bool DeleteDeptUser(string deptCode, string userCodes)
        {
            return dal.DeleteDeptUser(deptCode, userCodes);
        }

        /// <summary>
        /// 取得部门信息
        /// </summary>
        /// <returns></returns>
        public IList<DepartmentInfo> GetDepartmentInfo()
        {
            DataSet ds = dal.GetDepartmentInfo();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<DepartmentInfo> deptList = new List<DepartmentInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DepartmentInfo info = new DepartmentInfo();

                    info.Abbreviation = dr["Abbreviation"].ToString();
                    info.Code = dr["Code"].ToString();
                    info.DeptName = dr["Department"].ToString();
                    info.DeptCode = dr["DeptCode"].ToString();
                    info.Description = dr["Description"].ToString();
                    info.State = Convert.ToInt32(dr["State"].ToString());
                    info.Levels = Convert.ToInt32(dr["Levels"].ToString());
                    info.ParentCode = dr["ParentCode"].ToString();
                    //info.ParentDepartment = dr["ParentDepartment"].ToString();
                    info.DeptTypeCode = dr["DeptTypeCode"].ToString();
                    //info.DeptTypeCode = dr["DeptType"].ToString();
                    int orderno = 0;
                    int.TryParse(dr["OrderNO"].ToString(), out orderno);
                    info.OrderNo = orderno;
                    deptList.Add(info);
                }
                return deptList;
            }
            return null;
        }
    }
}
