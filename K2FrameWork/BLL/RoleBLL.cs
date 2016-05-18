using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;
using Model;

namespace BLL
{
    public class RoleBLL
    {
        //创建dal连接
        private static readonly IRoleDAL dal = DALFactory.DataAccess.CreateRoleDAL();


        /// <summary>
        /// 取得流程下角色信息
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public IList<RoleInfo> GetRoles(string processCode)
        {
            DataSet ds = dal.GetRoles(processCode);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<RoleInfo> rList = new List<RoleInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RoleInfo info = new RoleInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.RoleName = dr["RoleName"].ToString();

                    try
                    {
                        info.ProcessCode = new Guid(dr["ProcessCode"].ToString());
                    }
                    catch
                    {
                        info.ProcessCode = Guid.Empty;
                    }

                    info.ProcessType = dr["processtype"].ToString();
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.Desciption = dr["Desciption"].ToString();

                    rList.Add(info);
                }
                return rList;
            }
            return null;
        }

        public DataSet GetRoleByMenuID(string MenuGUID)
        {
            return dal.GetRoleByMenuID(MenuGUID);
        }

        /// <summary>
        /// 取得某个角色
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public RoleInfo GetRoleByRoleCode(string roleCode)
        {
            DataTable dt = dal.GetRoleInfoByRoleCode(roleCode);
            if (dt != null &&  dt.Rows.Count > 0)
            {
                RoleInfo info = new RoleInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    info.ID = new Guid(dr["ID"].ToString());
                    info.RoleName = dr["RoleName"].ToString();

                    try
                    {
                        info.ProcessCode = new Guid(dr["ProcessCode"].ToString());
                    }
                    catch
                    {
                        info.ProcessCode = Guid.Empty;
                    }
                    info.OrgID = new Guid(dr["OrgID"].ToString());
                    info.ProcessType = dr["ProcessType"].ToString();
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.Desciption = dr["Desciption"].ToString();
                    return info;
                }
            }
            return null;
        }

        public bool UpdateRole(string roleCode, string roleName, string processCode, string orgID,string description)
        {
            Guid OrgID = Guid.Empty;
            try
            {
                OrgID = new Guid(orgID);
            }
            catch
            {
                return false;
            }
            return dal.UpdateRole(roleCode, roleName, processCode, OrgID, description);
        }

        public bool AddNewRole(string roleName, string processCode, string orgID, string description)
        {
            Guid OrgID = Guid.Empty;
            try
            {
                OrgID = new Guid(orgID);
            }
            catch
            {
                return false;
            }
            return dal.AddNewRole(roleName, processCode, OrgID, description);
        }

        public DataSet GetRoleUserByCode(string roleCode)
        {
            return dal.GetRoleUserByCode(roleCode);
        }


        /// <summary>
        /// 取得角色信息
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataTable GetRoleInfoByRoleCode(string roleCode)
        {
            return dal.GetRoleInfoByRoleCode(roleCode);
        }

        /// <summary>
        /// 删除角色用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteUserFromRoleUser(string ids)
        {
            return dal.DeleteUserFromRoleUser(ids);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleCodes"></param>
        /// <returns></returns>
        public bool DeleteRoles(string roleCodes)
        {
            return dal.DeleteRoles(roleCodes);
        }

        /// <summary>
        /// 添加角色用户
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="ad"></param>
        /// <returns></returns>
        public bool AddUserToRoleUser(string roleCode, string ad)
        {
            return dal.AddUserToRoleUser(roleCode, ad);
        }

        /// <summary>
        /// 取得角色用户信息
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataSet GetRoleUser(string roleCode, string userCode)
        {
            try
            {
                return dal.GetRoleUser(new Guid(roleCode), new Guid(userCode));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 更新用户角色表
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userCode"></param>
        /// <param name="deptName"></param>
        /// <param name="deptCode"></param>
        /// <param name="dutyRegion"></param>
        /// <param name="mainRoleName"></param>
        /// <param name="mainRoleCode"></param>
        /// <param name="expand1"></param>
        /// <param name="expand2"></param>
        /// <param name="expand3"></param>
        /// <param name="expand4"></param>
        /// <returns></returns>
        public bool UpdateRoleUserByID(string Id, string userCode, string deptName, string deptCode, string dutyRegion, string mainRoleName, string mainRoleCode, string expand1, string expand2, string expand3, string expand4)
        {
            Guid ID = Guid.Empty;
            try
            {
                ID = new Guid(Id);
            }
            catch
            {
                return false;
            }
            return dal.UpdateRoleUserByID(ID, userCode, deptName, deptCode, dutyRegion, mainRoleName, mainRoleCode, expand1, expand2, expand3, expand4);
        }

        /// <summary>
        /// 通过组织ID获取角色信息
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="parm"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable GetRolesByOrgParams(string orgID, string parm, string rows)
        {
            Guid OrgID = Guid.Empty;
            try
            {
                OrgID = new Guid(orgID);
            }
            catch
            {
                return null;
            }
            return dal.GetRolesByOrgParams(OrgID, parm, rows);
        }
    }
}
