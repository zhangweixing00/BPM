using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Pkurg.PWorld.Business.Common;
using Pkurg.PWorldBPM.Business;

namespace Pkurg.PWorldBPM.Business.BIZ.ERP
{
    public class SupplementalAgreement
    {
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static void Add(SupplementalAgreementInfo model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.NVarChar,100),
					new SqlParameter("@ErpFormId", SqlDbType.NVarChar,100),
					new SqlParameter("@ErpFormType", SqlDbType.NVarChar,100),
					new SqlParameter("@StartDeptId", SqlDbType.NVarChar,100),
					new SqlParameter("@IsReportToResource", SqlDbType.Int,4),
					new SqlParameter("@IsReportToFounder", SqlDbType.Int,4),
					new SqlParameter("@RelationContract", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@ApproveResult", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.FormID;
            parameters[1].Value = model.ErpFormId;
            parameters[2].Value = model.ErpFormType;
            parameters[3].Value = model.StartDeptId;
            parameters[4].Value = model.IsReportToResource;
            parameters[5].Value = model.IsReportToFounder;
            parameters[6].Value = model.RelationContract;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.ApproveResult;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_SupplementalAgreement_ADD", parameters);
        }



        /// <summary>
        ///  更新一条数据
        /// </summary>
        public static void Update(SupplementalAgreementInfo model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.NVarChar,100),
					new SqlParameter("@ErpFormId", SqlDbType.NVarChar,100),
					new SqlParameter("@ErpFormType", SqlDbType.NVarChar,100),
					new SqlParameter("@StartDeptId", SqlDbType.NVarChar,100),
					new SqlParameter("@IsReportToResource", SqlDbType.Int,4),
					new SqlParameter("@IsReportToFounder", SqlDbType.Int,4),
					new SqlParameter("@RelationContract", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@ApproveResult", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.FormID;
            parameters[1].Value = model.ErpFormId;
            parameters[2].Value = model.ErpFormType;
            parameters[3].Value = model.StartDeptId;
            parameters[4].Value = model.IsReportToResource;
            parameters[5].Value = model.IsReportToFounder;
            parameters[6].Value = model.RelationContract;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.ApproveResult;
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_SupplementalAgreement_Update", parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(string FormID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.NVarChar,100)			};
            parameters[0].Value = FormID;

            DBHelper.ExecutedProcedure("Biz.ERP_SupplementalAgreement_Delete", parameters);
        }
 

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SupplementalAgreementInfo GetModel(string FormID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.NVarChar,100)			};
            parameters[0].Value = FormID;

            ContractApprovalInfo model = new  ContractApprovalInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.ERP_SupplementalAgreement_GetModel", parameters);
            if (dt!=null&&dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SupplementalAgreementInfo DataRowToModel(DataRow row)
        {
            SupplementalAgreementInfo model = new SupplementalAgreementInfo();
            if (row != null)
            {
                if (row["FormID"] != null)
                {
                    model.FormID = row["FormID"].ToString();
                }
                if (row["ErpFormId"] != null)
                {
                    model.ErpFormId = row["ErpFormId"].ToString();
                }
                if (row["ErpFormType"] != null)
                {
                    model.ErpFormType = row["ErpFormType"].ToString();
                }
                if (row["StartDeptId"] != null)
                {
                    model.StartDeptId = row["StartDeptId"].ToString();
                }
                if (row["IsReportToResource"] != null && row["IsReportToResource"].ToString() != "")
                {
                    model.IsReportToResource = int.Parse(row["IsReportToResource"].ToString());
                }
                if (row["IsReportToFounder"] != null && row["IsReportToFounder"].ToString() != "")
                {
                    model.IsReportToFounder = int.Parse(row["IsReportToFounder"].ToString());
                }
                if (row["RelationContract"] != null)
                {
                    model.RelationContract = row["RelationContract"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    model.CreateTime = row["CreateTime"].ToString();
                }
                if (row["ApproveResult"] != null)
                {
                    model.ApproveResult = row["ApproveResult"].ToString();
                }
            }
            return model;
        }

        public static SupplementalAgreementInfo GetModelByInstId(string id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,100)			};
            parameters[0].Value = id;

            ContractApprovalInfo model = new ContractApprovalInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.ERP_SupplementalAgreement_GetModelByInstId", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public static SupplementalAgreementInfo GetModelByWfId(string id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,100)			};
            parameters[0].Value = id;

            ContractApprovalInfo model = new ContractApprovalInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.ERP_SupplementalAgreement_GetModelByWfId", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 通过erpid得到formid
        /// </summary>
        /// <param name="erpId"></param>
        /// <returns></returns>
        public static string GetSupplementalAgreementFormidByErpId(string erpId)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("@erpId",SqlDbType.NVarChar,100)
                                      };
            parameters[0].Value = erpId;
            SupplementalAgreementInfo model = new SupplementalAgreementInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.ERP_GetSupplementalAgreementFormidByErpId", parameters);
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
        /// <param name="formid4"></param>
        /// <returns></returns>
        public static string GetSupplementalAgreementFormidByFormId(string formid4)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("@formid4",SqlDbType.NVarChar,100)
                                      };
            parameters[0].Value = formid4;
            SupplementalAgreementInfo model = new SupplementalAgreementInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.ERP_GetSupplementalAgreementFormidByFormId", parameters);
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
