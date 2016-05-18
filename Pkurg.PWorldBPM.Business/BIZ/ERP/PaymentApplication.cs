using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Entites.BIZ.ERP;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

namespace Pkurg.PWorldBPM.Business.BIZ.ERP
{
    public class PaymentApplication
    {
        public static void UpdatePaymentApplication(PaymentApplicationInfo info)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsOverContract",System.Data.SqlDbType.Int),
            new SqlParameter("@ErpFormId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@ErpFormType",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@PaymentApplicationEngineer",System.Data.SqlDbType.Int),
            new SqlParameter("@ApproveResult",System.Data.SqlDbType.VarChar,100),
            new SqlParameter("@LeadersSelected",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@Amount",System.Data.SqlDbType.NVarChar,100)
            };
            parameters[0].Value = info.FormId;
            parameters[1].Value = info.IsOverContract;
            parameters[2].Value = info.ErpFormId;
            parameters[3].Value = info.ErpFormType;
            parameters[4].Value = info.StartDeptId;
            parameters[5].Value = info.IsCheckedChairman;
            parameters[6].Value = info.ApproveResult;
            parameters[7].Value = info.LeadersSelected;
            parameters[8].Value = info.Amount;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_UpdatePaymentApplication", parameters);

        }
        public static void InsertPaymentApplication(PaymentApplicationInfo info)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsOverContract",System.Data.SqlDbType.Int),
            new SqlParameter("@ErpFormId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@ErpFormType",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@PaymentApplicationEngineer",System.Data.SqlDbType.Int),
            new SqlParameter("@LeadersSelected",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@Amount",System.Data.SqlDbType.NVarChar,100)
            };
            parameters[0].Value = info.FormId;
            parameters[1].Value = info.IsOverContract;
            parameters[2].Value = info.ErpFormId;
            parameters[3].Value = info.ErpFormType;
            parameters[4].Value = info.StartDeptId;
            parameters[5].Value = info.IsCheckedChairman;
            parameters[6].Value = info.LeadersSelected;
            parameters[7].Value = info.Amount;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_InsertPaymentApplication", parameters);
        }
        //GetPaymentApplicationInfo

        public static PaymentApplicationInfo GetPaymentApplicationInfoByInstanceId(string id)
        {
            WorkFlowInstance instance = new WF_WorkFlowInstance().GetWorkFlowInstanceById(id);
            if (instance != null)
            {
                string formId = instance.FormId;
                return GetPaymentApplicationInfo(formId);
            }
            return null;

            //待改造
            //SqlParameter[] parameters = {
            //        new SqlParameter("@ID", SqlDbType.NVarChar,100)			};
            //parameters[0].Value = id;


            //DataTable dt = GeDataProvider().ExecutedProcedure("Biz.ERP_ContractApproval_GetModelByInstId", parameters);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    return DataRowToModel(dt);
            //}
            //else
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static PaymentApplicationInfo DataRowToModel(DataTable dataTable)
        {
            PaymentApplicationInfo info = new PaymentApplicationInfo();
            if (dataTable != null&&dataTable.Rows.Count>0)
            {
                if (dataTable.Rows[0]["FormId"] != null && dataTable.Rows[0]["FormId"].ToString() != "")
                {
                    info.FormId = dataTable.Rows[0]["FormId"].ToString();
                }
                if (dataTable.Rows[0]["IsOverContract"] != null && dataTable.Rows[0]["IsOverContract"].ToString() != "")
                {
                    info.IsOverContract = int.Parse(dataTable.Rows[0]["IsOverContract"].ToString());
                }
                if (dataTable.Rows[0]["ErpFormId"] != null && dataTable.Rows[0]["ErpFormId"].ToString() != "")
                {
                    info.ErpFormId = dataTable.Rows[0]["ErpFormId"].ToString();
                }
                if (dataTable.Rows[0]["ErpFormType"] != null && dataTable.Rows[0]["ErpFormType"].ToString() != "")
                {
                    info.ErpFormType = dataTable.Rows[0]["ErpFormType"].ToString();
                }
                if (dataTable.Rows[0]["StartDeptId"] != null && dataTable.Rows[0]["StartDeptId"].ToString() != "")
                {
                    info.StartDeptId = dataTable.Rows[0]["StartDeptId"].ToString();
                }
                if (dataTable.Rows[0]["PaymentApplicationEngineer"] != null && dataTable.Rows[0]["PaymentApplicationEngineer"].ToString() != "")
                {
                    info.IsCheckedChairman = int.Parse(dataTable.Rows[0]["PaymentApplicationEngineer"].ToString());
                }
                if (dataTable.Rows[0]["ApproveResult"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.ApproveResult = dataTable.Rows[0]["ApproveResult"].ToString();
                }
                if (dataTable.Rows[0]["CreateTime"] != null && dataTable.Rows[0]["CreateTime"].ToString() != "")
                {
                    info.CreateTime = DateTime.Parse(dataTable.Rows[0]["CreateTime"].ToString());
                }
                if (dataTable.Rows[0]["LeadersSelected"] != null && dataTable.Rows[0]["LeadersSelected"].ToString() != "")
                {
                    info.LeadersSelected = dataTable.Rows[0]["LeadersSelected"].ToString();
                }
                if (dataTable.Rows[0]["Amount"] != null && dataTable.Rows[0]["Amount"].ToString() != "")
                {
                    info.Amount = dataTable.Rows[0]["Amount"].ToString();
                }
            }
            return info;
        }

        public static PaymentApplicationInfo GetPaymentApplicationInfo(string FormID)
        {
            PaymentApplicationInfo info = null;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = FormID;
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_GetErpApplyOfForm", parameters);
            ///给实体赋值
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                info = new PaymentApplicationInfo();
                if (dataTable.Rows[0]["FormId"] != null && dataTable.Rows[0]["FormId"].ToString() != "")
                {
                    info.FormId = dataTable.Rows[0]["FormId"].ToString();
                }
                if (dataTable.Rows[0]["IsOverContract"] != null && dataTable.Rows[0]["IsOverContract"].ToString() != "")
                {
                    info.IsOverContract = int.Parse(dataTable.Rows[0]["IsOverContract"].ToString());
                }
                if (dataTable.Rows[0]["ErpFormId"] != null && dataTable.Rows[0]["ErpFormId"].ToString() != "")
                {
                    info.ErpFormId = dataTable.Rows[0]["ErpFormId"].ToString();
                }
                if (dataTable.Rows[0]["ErpFormType"] != null && dataTable.Rows[0]["ErpFormType"].ToString() != "")
                {
                    info.ErpFormType = dataTable.Rows[0]["ErpFormType"].ToString();
                }
                if (dataTable.Rows[0]["StartDeptId"] != null && dataTable.Rows[0]["StartDeptId"].ToString() != "")
                {
                    info.StartDeptId = dataTable.Rows[0]["StartDeptId"].ToString();
                }
                if (dataTable.Rows[0]["PaymentApplicationEngineer"] != null && dataTable.Rows[0]["PaymentApplicationEngineer"].ToString() != "")
                {
                    info.IsCheckedChairman = int.Parse(dataTable.Rows[0]["PaymentApplicationEngineer"].ToString());
                }
                if (dataTable.Rows[0]["ApproveResult"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.ApproveResult = dataTable.Rows[0]["ApproveResult"].ToString();
                }
                if (dataTable.Rows[0]["CreateTime"] != null && dataTable.Rows[0]["CreateTime"].ToString() != "")
                {
                    info.CreateTime = DateTime.Parse(dataTable.Rows[0]["CreateTime"].ToString());
                }
                if (dataTable.Rows[0]["LeadersSelected"] != null && dataTable.Rows[0]["LeadersSelected"].ToString() != "")
                {
                    info.LeadersSelected = dataTable.Rows[0]["LeadersSelected"].ToString();
                }
                if (dataTable.Rows[0]["Amount"] != null && dataTable.Rows[0]["Amount"].ToString() != "")
                {
                    info.Amount = dataTable.Rows[0]["Amount"].ToString();
                }
            }
            return info;
        }
    
        public static PaymentApplicationInfo GetPaymentApplicationInfoByWfId(string wfId)
        {
            PaymentApplicationInfo info = null;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = wfId;
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_GetErpApplyOfForm_ByWfId", parameters);
            ///给实体赋值
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                info = new PaymentApplicationInfo();
                if (dataTable.Rows[0]["FormId"] != null && dataTable.Rows[0]["FormId"].ToString() != "")
                {
                    info.FormId = dataTable.Rows[0]["FormId"].ToString();
                }
                if (dataTable.Rows[0]["IsOverContract"] != null && dataTable.Rows[0]["IsOverContract"].ToString() != "")
                {
                    info.IsOverContract = int.Parse(dataTable.Rows[0]["IsOverContract"].ToString());
                }
                if (dataTable.Rows[0]["ErpFormId"] != null && dataTable.Rows[0]["ErpFormId"].ToString() != "")
                {
                    info.ErpFormId = dataTable.Rows[0]["ErpFormId"].ToString();
                }
                if (dataTable.Rows[0]["ErpFormType"] != null && dataTable.Rows[0]["ErpFormType"].ToString() != "")
                {
                    info.ErpFormType = dataTable.Rows[0]["ErpFormType"].ToString();
                }
                if (dataTable.Rows[0]["StartDeptId"] != null && dataTable.Rows[0]["StartDeptId"].ToString() != "")
                {
                    info.StartDeptId = dataTable.Rows[0]["StartDeptId"].ToString();
                }
                if (dataTable.Rows[0]["PaymentApplicationEngineer"] != null && dataTable.Rows[0]["PaymentApplicationEngineer"].ToString() != "")
                {
                    info.IsCheckedChairman = int.Parse(dataTable.Rows[0]["PaymentApplicationEngineer"].ToString());
                }
                if (dataTable.Rows[0]["ApproveResult"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.ApproveResult = dataTable.Rows[0]["ApproveResult"].ToString();
                }
                if (dataTable.Rows[0]["CreateTime"] != null && dataTable.Rows[0]["CreateTime"].ToString() != "")
                {
                    info.CreateTime = DateTime.Parse(dataTable.Rows[0]["CreateTime"].ToString());
                }
                if (dataTable.Rows[0]["LeadersSelected"] != null && dataTable.Rows[0]["LeadersSelected"].ToString() != "")
                {
                    info.LeadersSelected = dataTable.Rows[0]["LeadersSelected"].ToString();
                }
                if (dataTable.Rows[0]["Amount"] != null && dataTable.Rows[0]["Amount"].ToString() != "")
                {
                    info.Amount = dataTable.Rows[0]["Amount"].ToString();
                }
            }
            return info;
        }

        /// <summary>
        /// 通过erpid得到formid
        /// </summary>
        /// <param name="erpId"></param>
        /// <returns></returns>
        public static string GetPaymentApplicationFormidByErpId(string erpId)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("@erpId",SqlDbType.NVarChar,100)
                                      };
            parameters[0].Value = erpId;
            PaymentApplicationInfo model = new PaymentApplicationInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.ERP_GetPaymentApplicationFormidByErpId", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 通过formid1得到instanceid
        /// </summary>
        /// <param name="formid3"></param>
        /// <returns></returns>
        public static string GetPaymentApplicationInstanceIdByFormId(string formid3)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("@formid3",SqlDbType.NVarChar,100)
                                      };
            parameters[0].Value = formid3;
            PaymentApplicationInfo model = new PaymentApplicationInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.ERP_GetPaymentApplicationInstanceIdByFormId", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
