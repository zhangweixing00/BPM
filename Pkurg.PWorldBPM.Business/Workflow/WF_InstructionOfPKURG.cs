using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Common.Log;
using Pkurg.BPM.Data;
using Pkurg.BPM.Services;

namespace Pkurg.PWorldBPM.Business
{
    public class WF_InstructionOfPKURG
    {
        private string className = "BFInstructionOfPkurg";

        /// <summary>
        /// 得到请示单列表
        /// </summary>
        /// <returns></returns>
        public TList<InstructionOfPkurg> GetInstructionOfPkurgList()
        {
            InstructionOfPkurgService service = new InstructionOfPkurgService();


            InstructionOfPkurgQuery query = new InstructionOfPkurgQuery();
            query.Clear();

            SqlSortBuilder<InstructionOfPkurgColumn> sort = new SqlSortBuilder<InstructionOfPkurgColumn>();
            sort.AppendASC(InstructionOfPkurgColumn.Date);


            return service.Find(query.GetParameters(), sort.GetSortColumns());
        }
        /// <summary>
        /// 根据请示单主键得到请示单对象
        /// </summary>
        /// <param name="InstructionOfPkurgID"></param>
        /// <returns></returns>
        public InstructionOfPkurg GetInstructionOfPkurgById(string ID)
        {
            InstructionOfPkurgService drs = new InstructionOfPkurgService();
            return drs.Get(new InstructionOfPkurgKey(ID));
        }

        /// <summary>
        /// 根据工作流实例ID得到请示单信息
        /// </summary>
        /// <param name="WFLInstanceId"></param>
        /// <returns></returns>
        public TList<InstructionOfPkurg> GetInstructionOfPkurgByWFLInstanceId(string WFLInstanceId)
        {
            InstructionOfPkurgService rs = new InstructionOfPkurgService();
            InstructionOfPkurgQuery query = new InstructionOfPkurgQuery();

            query.Clear();
            query.AppendEquals(string.Empty, InstructionOfPkurgColumn.WflInstanceId, WFLInstanceId);

            SqlSortBuilder<InstructionOfPkurgColumn> sort = new SqlSortBuilder<InstructionOfPkurgColumn>();
            sort.AppendASC(InstructionOfPkurgColumn.CreateAtTime);

            return rs.Find(query.GetParameters(), sort.GetSortColumns());
        }

        #region 请示单数据更新
        /// <summary>
        /// 添加请示单
        /// </summary>
        /// <param name="rd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        //public bool AddGeneralReimburse(Entities.Reimburse rd, TList<ReceiptInfo> list, decimal Balance, string strBankAccount, Employee CurrentEmployee)
        public bool AddInstructionOfPkurg(InstructionOfPkurg rd)
        {
            string methodName = "AddInstructionOfPkurg";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("InstructionOfPkurg={0}", rd));

            TransactionManager transaction = DataRepository.Provider.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                //插入请示单
                bool result = DataRepository.InstructionOfPkurgProvider.Insert(transaction, rd);

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

        public bool UpdateInstructionOfPkurg(InstructionOfPkurg rd)
        {
            string methodName = "UpdateInstructionOfPkurg";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("InstructionOfPkurg={0}", rd));

            TransactionManager transaction = DataRepository.Provider.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                //插入请示单
                bool result = DataRepository.InstructionOfPkurgProvider.Update(transaction, rd);

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
        /// 删除请示单
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="CurrentEmployee"></param>
        /// <returns></returns>
        public bool DeleteInstructionOfPkurg(string InstructionOfPkurgID)
        {
            string methodName = "DeleteInstructionOfPkurg";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + MessageType.IN + ":" + string.Format("InstructionOfPkurg={0}", InstructionOfPkurgID));

            TransactionManager transaction = DataRepository.Provider.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                //删除该合同下计价方式
                bool result = DataRepository.InstructionOfPkurgProvider.Delete(transaction, InstructionOfPkurgID);

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


        #endregion

        #region 更新状态
        public bool UpdateStatus(string ID, string strStatus, string wfInstanceId)
        {
            bool result = false;
            string methodName = "UpdateStatus";
            Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN + ":" + string.Format("ID={0};Status={1};InstanceId={2}", ID, strStatus, wfInstanceId));

            InstructionOfPkurg instructionOfPkurg = GetInstructionOfPkurgById(ID);
            instructionOfPkurg.SumitTime = DateTime.Now;
            instructionOfPkurg.ApproveStatus = strStatus;
            instructionOfPkurg.WflInstanceId = wfInstanceId;

            result = DataRepository.InstructionOfPkurgProvider.Update(instructionOfPkurg);
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
        
    }
}
