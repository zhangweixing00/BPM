using System;
using System.Data.SqlClient;
using System.Web.Services;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Common;
using Pkurg.PWorld.Business.Manage;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.Controls;
using Pkurg.PWorldBPM.Business.Workflow;

/// <summary>
///WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool CreateNewFormByInstanceIDAndEmployeeCodeWithStoredProcedure(string InstanceID, string EmployeeCode, string sp)
    {
        DataProvider dataProvider = new DataProvider();
        dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;


        BFEmployee bfEmployee = new BFEmployee();
        Employee employeeInfo = new Employee();
        WF_WorkFlowInstance wFlowInst = new WF_WorkFlowInstance();
        BFApprovalRecord bFApprovalRecord = new BFApprovalRecord();
        EmployeeAdditional employeeAddition = bfEmployee.GetEmployeeAdditionalById(EmployeeCode);
        employeeInfo = bfEmployee.GetEmployeeByEmployeeCode(EmployeeCode);
        string EmployeeName = bfEmployee.GetEmployeeNameByEmployeeCode(EmployeeCode);

        WorkFlowInstance wFlowInstanceBefore = new WorkFlowInstance();
        wFlowInstanceBefore = wFlowInst.GetWorkFlowInstanceById(InstanceID);
        WorkFlowInstance newWorkFlowInstance = new WorkFlowInstance();

        {
            newWorkFlowInstance.InstanceId = Guid.NewGuid().ToString();
            newWorkFlowInstance.AppId = wFlowInstanceBefore.AppId;
            newWorkFlowInstance.FormId = BPMHelp.GetSerialNumber("SQ_");
            newWorkFlowInstance.WfInstanceId = wFlowInstanceBefore.WfInstanceId;
            newWorkFlowInstance.OrderNo = wFlowInstanceBefore.OrderNo;
            newWorkFlowInstance.IsDel = 0;
            newWorkFlowInstance.CreateByUserCode = EmployeeCode;
            newWorkFlowInstance.CreateByUserName = EmployeeName;
            newWorkFlowInstance.CreateAtTime = System.DateTime.Now;
            newWorkFlowInstance.UpdateByUserCode = EmployeeCode;
            newWorkFlowInstance.UpdateByUserName = EmployeeName;
            newWorkFlowInstance.UpdateAtTime = wFlowInstanceBefore.UpdateAtTime;
            newWorkFlowInstance.CreateDeptCode = employeeInfo.DepartCode;
            newWorkFlowInstance.CreateDeptName = employeeInfo.DepartName;
            newWorkFlowInstance.WorkItemCode = wFlowInstanceBefore.WorkItemCode;
            newWorkFlowInstance.WorkItemName = wFlowInstanceBefore.WorkItemName;
            newWorkFlowInstance.WfTaskId = wFlowInstanceBefore.WfTaskId;
            newWorkFlowInstance.FinishedTime = null;
            newWorkFlowInstance.Remark = wFlowInstanceBefore.Remark;
            newWorkFlowInstance.FormTitle = wFlowInstanceBefore.FormTitle;
            newWorkFlowInstance.WfStatus = "0";
            newWorkFlowInstance.SumitTime = null;
            newWorkFlowInstance.FormData = wFlowInstanceBefore.FormData;
        }

        var newApprovalRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record();

        newApprovalRecord.ApprovalID = Guid.NewGuid().ToString();
        newApprovalRecord.FormID = newWorkFlowInstance.FormId;
        newApprovalRecord.InstanceID = newWorkFlowInstance.InstanceId;
        newApprovalRecord.CreateByUserCode = employeeInfo.EmployeeCode;
        newApprovalRecord.CreateByUserName = employeeInfo.EmployeeName;
        newApprovalRecord.CreateAtTime = System.DateTime.Now;
        newApprovalRecord.ApproveByUserCode = employeeInfo.EmployeeCode;
        newApprovalRecord.ApproveByUserName = employeeInfo.EmployeeName;
        newApprovalRecord.UpdateByUserCode = employeeInfo.EmployeeCode;
        newApprovalRecord.UpdateByUserName = employeeInfo.EmployeeName;
        newApprovalRecord.UpdateAtTime = wFlowInstanceBefore.UpdateAtTime;
        newApprovalRecord.CurrentActiveName = "拟稿";



        Pkurg.BPM.Entities.FlowRelated relationInfo = new FlowRelated()
        {
            FlowId = newWorkFlowInstance.InstanceId,
            CreatorName = employeeInfo.EmployeeName,
            CreateTime = System.DateTime.Now,
            CreatorId = employeeInfo.EmployeeCode,
            RelatedFlowId = wFlowInstanceBefore.InstanceId,
            RelatedFlowCreator = wFlowInstanceBefore.CreateByUserName,
            RelatedFlowEndTime = wFlowInstanceBefore.FinishedTime,
            RelatedFlowName = wFlowInstanceBefore.FormTitle,
        };

        WF_Relation.AddRelatedFlowInfo(relationInfo);

        SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID_Before",System.Data.SqlDbType.NVarChar,100), 
            new SqlParameter("@FormID_New",System.Data.SqlDbType.NVarChar,100)
        };
        parameters[0].Value = wFlowInstanceBefore.FormId;
        parameters[1].Value = newWorkFlowInstance.FormId;
        dataProvider.ExecutedProcedure(sp, parameters);

        bool isSuccessForInstance = wFlowInst.AddWorkFlowInstance(newWorkFlowInstance);
        bool isSuccessForApprovalRecord = bFApprovalRecord.AddApprovalRecord(newApprovalRecord);

        if (isSuccessForInstance && isSuccessForApprovalRecord)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
