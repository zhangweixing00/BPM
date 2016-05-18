using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace IDAL
{
    public interface IDepartment
    {
        IList<DepartmentInfo> GetSubDepartments(string orgCode, string deptCode);

        DataSet GetDeptType();

        DataSet GetSortDepartment(string orgID);

        DataSet GetDepartmentInfo(string deptCode);

        bool CreateDepartment(DepartmentInfo dept, string shouldOrderNO, string action);

        bool UpdateDepartment(DepartmentInfo dept, string shouldOrderNO, string action);

        DataSet GetDepartmentByUserCode(string usercode);

        bool DeleteDeptUser(string deptCode, string userCodes);

        DataSet GetDepartmentInfo();
    }
}
