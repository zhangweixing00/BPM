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
    public class Instruction
    {
        public static void UpdateInstruction(InstructionInfo info)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsReport",System.Data.SqlDbType.Int),
            new SqlParameter("@ErpFormId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@ErpFormType",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DeptName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@UserName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@CreateTime",System.Data.SqlDbType.DateTime),
            new SqlParameter("@LeadersSelected",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@ApproveResult",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsCheckedChairman",System.Data.SqlDbType.Int),
            };
            parameters[0].Value = info.FormId;
            parameters[1].Value = info.IsReport;
            parameters[2].Value = info.ErpFormId;
            parameters[3].Value = info.ErpFormType;
            parameters[4].Value = info.StartDeptId;
            parameters[5].Value = info.DeptName;
            parameters[6].Value = info.UserName;
            parameters[7].Value = info.CreateTime;
            parameters[8].Value = info.LeadersSelected;
            parameters[9].Value = info.ApproveResult;
            parameters[10].Value = info.IsCheckedChairman;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_Instruction_Update", parameters);

        }
        public static void InsertInstructionInfo(InstructionInfo info)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsReport",System.Data.SqlDbType.Int),
            new SqlParameter("@ErpFormId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@ErpFormType",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DeptName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@UserName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@CreateTime",System.Data.SqlDbType.DateTime),
            new SqlParameter("@LeadersSelected",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@ApproveResult",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsCheckedChairman",System.Data.SqlDbType.Int),
            };
            parameters[0].Value = info.FormId;
            parameters[1].Value = info.IsReport;
            parameters[2].Value = info.ErpFormId;
            parameters[3].Value = info.ErpFormType;
            parameters[4].Value = info.StartDeptId;
            parameters[5].Value = info.DeptName;
            parameters[6].Value = info.UserName;
            parameters[7].Value = info.CreateTime;
            parameters[8].Value = info.LeadersSelected;
            parameters[9].Value = info.ApproveResult;
            parameters[10].Value = info.IsCheckedChairman;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_Instruction_Insert", parameters);
        }

        public static InstructionInfo GetInstructionInfoByInstanceId(string id)
        {
            WorkFlowInstance instance = new WF_WorkFlowInstance().GetWorkFlowInstanceById(id);
            if (instance != null)
            {
                string formId = instance.FormId;
                return GetInstructionInfo(formId);
            }
            return null;
        }
        public static InstructionInfo GetInstructionInfo(string FormID)
        {
            InstructionInfo info = null;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = FormID;
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_Instruction_Get", parameters);
            ///给实体赋值
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                info = new InstructionInfo();
                if (dataTable.Rows[0]["FormId"] != null && dataTable.Rows[0]["FormId"].ToString() != "")
                {
                    info.FormId = dataTable.Rows[0]["FormId"].ToString();
                }
                if (dataTable.Rows[0]["IsReport"] != null && dataTable.Rows[0]["IsReport"].ToString() != "")
                {
                    info.IsReport = int.Parse(dataTable.Rows[0]["IsReport"].ToString());
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
                if (dataTable.Rows[0]["DeptName"] != null && dataTable.Rows[0]["DeptName"].ToString() != "")
                {
                    info.ErpFormId = dataTable.Rows[0]["DeptName"].ToString();
                }
                if (dataTable.Rows[0]["UserName"] != null && dataTable.Rows[0]["UserName"].ToString() != "")
                {
                    info.StartDeptId = dataTable.Rows[0]["UserName"].ToString();
                }
                if (dataTable.Rows[0]["CreateTime"] != null && dataTable.Rows[0]["CreateTime"].ToString() != "")
                {
                    info.CreateTime = DateTime.Parse(dataTable.Rows[0]["CreateTime"].ToString());
                }
                if (dataTable.Rows[0]["LeadersSelected"] != null && dataTable.Rows[0]["LeadersSelected"].ToString() != "")
                {
                    info.ErpFormType = dataTable.Rows[0]["LeadersSelected"].ToString();
                }
                if (dataTable.Rows[0]["ApproveResult"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.ApproveResult = dataTable.Rows[0]["ApproveResult"].ToString();
                }
                if (dataTable.Rows[0]["IsCheckedChairman"] != null && dataTable.Rows[0]["IsCheckedChairman"].ToString() != "")
                {
                    info.IsReport = int.Parse(dataTable.Rows[0]["IsCheckedChairman"].ToString());
                }
            }
            return info;
        }

        public static InstructionInfo GetInstructionInfoByWfId(string wfId)
        {
            InstructionInfo info = null;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@WFInstanceId",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = wfId;
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.ERP_Instruction_GetByWfId", parameters);
            ///给实体赋值
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                info = new InstructionInfo();
                if (dataTable.Rows[0]["FormId"] != null && dataTable.Rows[0]["FormId"].ToString() != "")
                {
                    info.FormId = dataTable.Rows[0]["FormId"].ToString();
                }
                if (dataTable.Rows[0]["IsReport"] != null && dataTable.Rows[0]["IsReport"].ToString() != "")
                {
                    info.IsReport = int.Parse(dataTable.Rows[0]["IsReport"].ToString());
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
                if (dataTable.Rows[0]["DeptName"] != null && dataTable.Rows[0]["DeptName"].ToString() != "")
                {
                    info.ErpFormId = dataTable.Rows[0]["DeptName"].ToString();
                }
                if (dataTable.Rows[0]["UserName"] != null && dataTable.Rows[0]["UserName"].ToString() != "")
                {
                    info.StartDeptId = dataTable.Rows[0]["UserName"].ToString();
                }
                if (dataTable.Rows[0]["CreateTime"] != null && dataTable.Rows[0]["CreateTime"].ToString() != "")
                {
                    info.CreateTime = DateTime.Parse(dataTable.Rows[0]["CreateTime"].ToString());
                }
                if (dataTable.Rows[0]["LeadersSelected"] != null && dataTable.Rows[0]["LeadersSelected"].ToString() != "")
                {
                    info.ErpFormType = dataTable.Rows[0]["LeadersSelected"].ToString();
                }
                if (dataTable.Rows[0]["ApproveResult"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.ApproveResult = dataTable.Rows[0]["ApproveResult"].ToString();
                }
                if (dataTable.Rows[0]["IsCheckedChairman"] != null && dataTable.Rows[0]["IsCheckedChairman"].ToString() != "")
                {
                    info.IsReport = int.Parse(dataTable.Rows[0]["IsCheckedChairman"].ToString());
                }
            }
            return info;
        }
        /// <summary>
        /// 通过erpid得到formid
        /// </summary>
        /// <param name="erpId"></param>
        /// <returns></returns>
        public static string GetInstructionFormidByErpId(string erpId)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("@erpId",SqlDbType.NVarChar,100)
                                      };
            parameters[0].Value = erpId;
            InstructionInfo model = new InstructionInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.ERP_GetInstructionFormidByErpId", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }

        public static string GetInstructionInstanceIdByFormId(string formid2)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("@formid2",SqlDbType.NVarChar,100)
                                      };
            parameters[0].Value = formid2;
            ContractApprovalInfo model = new ContractApprovalInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.ERP_GetInstructionInstanceIdByFormId", parameters);
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
