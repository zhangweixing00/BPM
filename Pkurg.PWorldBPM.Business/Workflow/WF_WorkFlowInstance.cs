using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Common.Log;
using Pkurg.BPM.Data;
using Pkurg.BPM.Services;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Workflow
{
   public class WF_WorkFlowInstance
    {
          private string className = "WF_WorkFlowInstance";

        /// <summary>
        /// 得到审批步骤列表
        /// </summary>
        /// <returns></returns>
      public TList<WorkFlowInstance> GetWorkFlowInstanceList()
        {
            WorkFlowInstanceService service = new WorkFlowInstanceService();


            WorkFlowInstanceQuery query = new WorkFlowInstanceQuery();
            query.Clear();

            SqlSortBuilder<WorkFlowInstanceColumn> sort = new SqlSortBuilder<WorkFlowInstanceColumn>();
            sort.AppendASC(WorkFlowInstanceColumn.CreateAtTime);


            return service.Find(query.GetParameters(), sort.GetSortColumns());
        }
      /// <summary>
      /// 根据工作流实例主键得到工作流实例对象
      /// </summary>
      /// <param name="WorkFlowInstanceID"></param>
      /// <returns></returns>
      public WorkFlowInstance GetWorkFlowInstanceById(string WorkFlowInstanceID)
        {
            WorkFlowInstanceService drs = new WorkFlowInstanceService();
            return drs.Get(new WorkFlowInstanceKey(WorkFlowInstanceID));
        }

    
      /// <summary>
      /// 根据表单编号得到审批步骤
      /// </summary>
      /// <param name="FormId"></param>
      /// <returns></returns>
      public WorkFlowInstance GetWorkFlowInstanceByFormId(string FormId)
      {
          WorkFlowInstance workFlowInstance = null;
          WorkFlowInstanceService rs = new WorkFlowInstanceService();
          WorkFlowInstanceQuery query = new WorkFlowInstanceQuery();

          query.Clear();
          query.AppendEquals(string.Empty, WorkFlowInstanceColumn.FormId, FormId);

          SqlSortBuilder<WorkFlowInstanceColumn> sort = new SqlSortBuilder<WorkFlowInstanceColumn>();
          sort.AppendASC(WorkFlowInstanceColumn.CreateAtTime);

          TList<WorkFlowInstance> WorkFlowInstanceList= rs.Find(query.GetParameters(), sort.GetSortColumns());
          if (WorkFlowInstanceList.Count>0)
          {
              workFlowInstance = WorkFlowInstanceList[0];
          }
          return workFlowInstance;
      }
      /// <summary>
      /// 根据根据工作流实例ID与步骤名得到审批步骤
      /// </summary>
      /// <param name="WflInstanceId"></param>
      /// <param name="CheckByUserRole"></param>
      /// <returns></returns>
      public WorkFlowInstance GetWorkFlowInstanceByWFInstanceId(string WfInstanceId)
      {
          WorkFlowInstance workFlowInstance = null;
          WorkFlowInstanceService rs = new WorkFlowInstanceService();
          WorkFlowInstanceQuery query = new WorkFlowInstanceQuery();

          query.Clear();
          query.AppendEquals(string.Empty, WorkFlowInstanceColumn.WfInstanceId, WfInstanceId);

          SqlSortBuilder<WorkFlowInstanceColumn> sort = new SqlSortBuilder<WorkFlowInstanceColumn>();
          sort.AppendASC(WorkFlowInstanceColumn.CreateAtTime);

          TList<WorkFlowInstance> WorkFlowInstanceList = rs.Find(query.GetParameters(), sort.GetSortColumns());
          if (WorkFlowInstanceList.Count > 0)
          {
              workFlowInstance = WorkFlowInstanceList[0];
          }
          return workFlowInstance;
      }
       
        #region 审批步骤数据更新
        /// <summary>
        /// 添加审批步骤
        /// </summary>
        /// <param name="rd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        //public bool AddGeneralReimburse(Entities.Reimburse rd, TList<ReceiptInfo> list, decimal Balance, string strBankAccount, Employee CurrentEmployee)
        public bool AddWorkFlowInstance(WorkFlowInstance rd)
        {
            string methodName = "AddWorkFlowInstance";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("WorkFlowInstance={0}", rd));

            TransactionManager transaction = DataRepository.Provider.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                //插入审批步骤
                bool result = DataRepository.WorkFlowInstanceProvider.Insert(transaction, rd);
              
                transaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                transaction.Rollback();
                Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + MessageType.Exception + ":" + string.Format("Exception={0}", exp));
                throw exp;
            }
        }

        public bool UpdateWorkFlowInstance(WorkFlowInstance rd)
        {
            string methodName = "UpdateWorkFlowInstance";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("WorkFlowInstance={0}", rd));

            TransactionManager transaction = DataRepository.Provider.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                //插入审批步骤
                bool result = DataRepository.WorkFlowInstanceProvider.Update(transaction, rd);
                
                transaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                transaction.Rollback();
                Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + MessageType.Exception + ":" + string.Format("Exception={0}", exp));
                throw exp;
            }
        }

        /// <summary>
        /// 删除审批步骤
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="CurrentEmployee"></param>
        /// <returns></returns>
        public bool DeleteWorkFlowInstance(string WorkFlowInstanceID)
        {
            string methodName = "DeleteWorkFlowInstance";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("WorkFlowInstance={0}", WorkFlowInstanceID));

            TransactionManager transaction = DataRepository.Provider.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                //删除该合同下计价方式
                bool result = DataRepository.WorkFlowInstanceProvider.Delete(transaction, WorkFlowInstanceID);
               
                transaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                transaction.Rollback();
                Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + MessageType.Exception + ":" + string.Format("Exception={0}", exp));
                throw exp;
            }
        }
        #region 更新状态
        public bool UpdateStatus(string wfInstanceId, string strStatus, string WorkItemCode, string WorkItemName, int WFTaskID, DateTime? FinishedTime, Pkurg.PWorld.Entities.Employee CurrentEmployee)
        {
            bool result = false;

            WorkFlowInstance workFlowInstance = GetWorkFlowInstanceByWFInstanceId(wfInstanceId);
            workFlowInstance.SumitTime = DateTime.Now;
            if (strStatus !="")
            {
                workFlowInstance.WfStatus = strStatus;
            }
            if (WorkItemCode != "")
            {
                workFlowInstance.WorkItemCode = WorkItemCode;
            }
            if (WorkItemName != "")
            {
                workFlowInstance.WorkItemName = WorkItemName;
            }
            if (WFTaskID > 0)
            {
                workFlowInstance.WfTaskId = WFTaskID;
            }
            if (FinishedTime != null)
            {
                workFlowInstance.FinishedTime = FinishedTime;
            }
            workFlowInstance.UpdateAtTime = DateTime.Now;
            workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
            workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;

            result = DataRepository.WorkFlowInstanceProvider.Update(workFlowInstance);
            if (!result)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion
        #region 更新状态
        public bool UpdateNowStatus(string wfInstanceId, string strStatus, string WorkItemCode, string WorkItemName, int WFTaskID, DateTime? FinishedTime, Pkurg.PWorld.Entities.Employee CurrentEmployee)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@UpdateByUserName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@wfInstanceId",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@UpdateByUserCode",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@WorkItemCode",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@WorkItemName",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@WFTaskID",System.Data.SqlDbType.Int),
            new SqlParameter("@WFStatus",System.Data.SqlDbType.NVarChar,10)
            };
            parameters[0].Value = CurrentEmployee.EmployeeName;
            parameters[1].Value = wfInstanceId;
            parameters[2].Value = CurrentEmployee.EmployeeCode;
            parameters[3].Value = WorkItemCode;
            parameters[4].Value = WorkItemName;
            parameters[5].Value = WFTaskID;
            parameters[6].Value = strStatus;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_UpdateAllInfoByWfInsId", parameters);
            return true;
        }
        #endregion
        #region 更新状态
        public bool UpdateNowStatusByFormID(string FormID, string WFStatus)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@WFStatus",System.Data.SqlDbType.NVarChar,10)
            };
            parameters[0].Value = FormID;
            parameters[1].Value = WFStatus;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_UpdateAllInfoByFormID", parameters);
            return true;
        }
        #endregion
        #endregion
    }
}
