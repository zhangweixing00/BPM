using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Manage;
using Pkurg.PWorld.Entities;
using Pkurg.PWorld.Business.Permission;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_CounterSign
    {
        public static List<Pkurg.PWorld.Entities.Department> GetCountSignDeptInfosByCreater(string deptId)
        {
            List<Pkurg.PWorld.Entities.Department> infos = new List<Department>();

            DataTable dt = GetCounterSignDepts(deptId);
            foreach (DataRow item in dt.Rows)
            {
                infos.Add(new Department()
                {
                    DepartCode = item["DepartCode"].ToString(),
                    DepartName = item["DepartName"].ToString()
                });
            }
            return infos;
        }

        private static DataTable GetCounterSignDepts(string deptId)
        {
            BFCountersignRoleDepartment counterSignHelper = new BFCountersignRoleDepartment();
            DataTable dt = counterSignHelper.GetSelectCountersignDepartment(deptId,"会签部门");
            if (dt == null || dt.Rows.Count == 0)
            {
                int index = deptId.LastIndexOf('-');
                if (index > -1)
                {
                    string parentCode = deptId.Substring(0, index);
                    dt = GetCounterSignDepts(parentCode);
                }
            }
            return dt;
        }

        public static bool SyncDataToDB(string instance, List<WF_CountersignParameter> cs_params)
        {
            try
            {
                WF_CounterSign_Process.DeleteCountersign(instance);

                for (int i = 0; i < cs_params.Count; i++)
                {
                    WF_CounterSign_Process.InsertCountersign(instance, cs_params[i]);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }
    }
}
