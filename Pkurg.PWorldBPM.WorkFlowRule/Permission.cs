using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Pkurg.PWorldBPM.WorkFlowRule
{
    /// <summary>
    /// 流程制度门户的权限控制
    /// </summary>
    public class Permission
    {
        public List<WR_Permission> GetList()
        {
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                return db.WR_Permission.Where(p => p.Record_Status == 0).ToList();
            }
        }

        public bool CheckPermission(string flowId, string loginName, string deptCode)
        {
            //loginName 域账号
            //deptCode 部门ID

            bool flag = false;
            flowId = flowId + ",";

            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                //Reject_Users
                if (db.WR_Permission.Where(p => p.Record_Status == 0 && (p.Flow_Ids + ",").Contains(flowId) && (p.Reject_Users + ",").Contains(loginName + ",")).Any())
                {
                    flag = false;
                }
                //Allow_Users
                else if (db.WR_Permission.Where(p => p.Record_Status == 0 && (p.Flow_Ids + ",").Contains(flowId) && (p.Allow_Users + ",").Contains(loginName + ",")).Any())
                {
                    flag = true;
                }
                else
                {
                    flag = CheckPermissionByDept(flowId, deptCode);
                }
            }
            return flag;
        }

        ////递归
        bool CheckPermissionByDept(string flowId, string deptCode)
        {
            //loginName 域账号
            //deptCode 部门ID
            bool change = false;
            bool flag = false;
            using (WorkFlowRuleDataContext db = new WorkFlowRuleDataContext())
            {
                //Reject_Depts
                if (db.WR_Permission.Where(p => p.Record_Status == 0 && (p.Flow_Ids + ",").Contains(flowId) && (p.Reject_Depts + ",").Contains(deptCode + ",")).Any())
                {
                    change = true;
                    flag = false;
                }
                //Allow_Depts
                else if (db.WR_Permission.Where(p => p.Record_Status == 0 && (p.Flow_Ids + ",").Contains(flowId) && (p.Allow_Depts + ",").Contains(deptCode + ",")).Any())
                {
                    change = true;
                    flag = true;
                }
            }

            if (change)
            {
                return flag;
            }
            else
            {
                //递归
                if (deptCode.Contains('-') && !change)
                {
                    return CheckPermissionByDept(flowId, deptCode.Substring(0, deptCode.LastIndexOf('-')));
                }
                return flag;
            }
        }
    }
}
