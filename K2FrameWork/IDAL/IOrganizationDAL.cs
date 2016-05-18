using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace IDAL
{
    public interface IOrganizationDAL
    {
        DataSet GetOrgList();

        /// <summary>
        /// 取得某个组织信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataSet GetOrgByID(Guid id);

        /// <summary>
        /// 创建组织
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool CreateOrgInfo(OrganizationInfo info);

        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool UpdateOrgInfo(OrganizationInfo info);

        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteOrgInfo(Guid id);

        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteOrgInfoByID(Guid Id);

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        bool DeleteDepartmentByCode(string deptCode);

        DataTable GetOrgOfDeptType();
    }
}
