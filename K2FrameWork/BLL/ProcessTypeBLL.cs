using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using IDAL;

namespace BLL
{
    public class ProcessTypeBLL
    {
        //创建dal连接
        private static readonly IProcessTypeDAL dal = DALFactory.DataAccess.CreateProcessTypeDAL();

        public IList<ProcessTypeInfo> GetProcessType()
        {
            DataSet ds = dal.GetProcessType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<ProcessTypeInfo> ptiList = new List<ProcessTypeInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProcessTypeInfo info = new ProcessTypeInfo();
                    info.ID = dr["ID"].ToString();
                    info.ProcessType = dr["ProcessType"].ToString();
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    ptiList.Add(info);
                }
                return ptiList;
            }
            return null;
        }
        public IList<PWorldDeptInfo> GetPWorldCompanyList()
        {
            DataSet ds = dal.GetPWorldCompanyData();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<PWorldDeptInfo> ptiList = new List<PWorldDeptInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PWorldDeptInfo info = new PWorldDeptInfo();
                    info.DepartCode = dr["DepartCode"].ToString();
                    info.DepartName = dr["DepartName"].ToString();
                   
                    ptiList.Add(info);
                }
                return ptiList;
            }
            return null;
        }
        public ProcessTypeInfo GetProcessTypeByID(string ID)
        {
            DataSet ds = dal.GetProcessTypeByID(ID);
            ProcessTypeInfo info = new ProcessTypeInfo();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    info.ID = dr["ID"].ToString();
                    info.ProcessType = dr["ProcessType"].ToString();
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    info.CreatedBy = dr["CreatedBy"].ToString();
                }
                return info;
            }
            return null;
        }

        /// <summary>
        /// 取得K2流程名称
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetProcessNameByID(string Id)
        {
            //Guid ID = Guid.Empty;
            //try
            //{
            //    ID = new Guid(Id);
            //}
            //catch
            //{
            //    return string.Empty;
            //}
            DataSet ds = dal.GetProcessNameByID(Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    return dr["ProcessFullName"].ToString();
                }
            }
            return string.Empty;
        }
    }
}
