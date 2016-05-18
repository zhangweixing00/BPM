using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using System.Data;

namespace BLL
{
    public class OrganizationBLL
    {
        //创建dal连接
        private static readonly IOrganizationDAL dal = DALFactory.DataAccess.CreateOrganizationDAL();

        /// <summary>
        /// 取得所有组织信息
        /// </summary>
        /// <returns></returns>
        public IList<OrganizationInfo> GetOrgList()
        {
            DataSet ds = dal.GetOrgList();
            List<OrganizationInfo> orgList = new List<OrganizationInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    OrganizationInfo info = new OrganizationInfo();
                    info.ID = new Guid(dr["ID"].ToString());
                    info.OrgName = dr["OrgName"].ToString();
                    info.OrgCode = dr["OrgCode"].ToString();
                    info.OrgDescription = dr["OrgDescription"].ToString();
                    info.State = int.Parse(dr["State"].ToString());
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    orgList.Add(info);
                }
            }
            return orgList;
        }

        /// <summary>
        /// 取得部门数据类型的组织信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrgOfDeptType()
        {
            return dal.GetOrgOfDeptType();
        }

        /// <summary>
        /// 取得某个部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrganizationInfo GetOrgByID(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Guid orgId = new Guid(id);
                if (orgId != null)
                {
                    DataSet ds = dal.GetOrgByID(orgId);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            OrganizationInfo info = new OrganizationInfo();
                            info.ID = orgId;
                            info.OrgName = dr["OrgName"].ToString();
                            info.OrgCode = dr["OrgCode"].ToString();
                            info.OrgDescription = dr["OrgDescription"].ToString();
                            info.State = int.Parse(dr["State"].ToString());
                            info.OrderNo = int.Parse(dr["OrderNo"].ToString());
                            info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                            info.CreatedBy = dr["CreatedBy"].ToString();
                            return info;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 创建组织
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool CreateOrgInfo(string orgName, string orgCode, string orgDesc, string orderno)
        {
            OrganizationInfo info = new OrganizationInfo();
            info.OrgName = orgName;
            info.OrgCode = orgCode;
            info.OrgDescription = orgDesc;
            int _orderNo = 0;
            int.TryParse(orderno, out _orderNo);
            info.OrderNo = _orderNo;
            return dal.CreateOrgInfo(info);
        }

        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateOrgInfo(Guid id, string orgName, string orgCode, string orgDesc, string orderno)
        {
            OrganizationInfo info = new OrganizationInfo();
            info.OrgName = orgName;
            info.OrgCode = orgCode;
            info.OrgDescription = orgDesc;
            int _orderNo = 0;
            int.TryParse(orderno, out _orderNo);
            info.OrderNo = _orderNo;
            info.ID = id;
            return dal.UpdateOrgInfo(info);
        }

        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteOrgInfo(Guid id)
        {
            return dal.DeleteOrgInfo(id);
        }

        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteOrgInfoByID(string Id)
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
            return dal.DeleteOrgInfoByID(ID);
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public bool DeleteDepartmentByCode(string deptCode)
        {
            return dal.DeleteDepartmentByCode(deptCode);
        }
    }
}
