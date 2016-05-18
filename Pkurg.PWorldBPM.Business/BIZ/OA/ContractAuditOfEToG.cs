using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.BIZ.OA
{
   public class ContractAuditOfEToG
    {
        /// <summary>
        /// 通过FormID得到ContractAuditOfEToGInfo的信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
       public ContractAuditOfEToGInfo Get(string FormID)
        {
           SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = FormID;

            ContractAuditOfEToGInfo model = new ContractAuditOfEToGInfo();
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.OA_ContractAuditOfEToGInfoByFormID_Get", parameters);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return DataRowToModel(dataTable.Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个实体对象【封装】
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
       private ContractAuditOfEToGInfo DataRowToModel(DataRow dataRow)
        {
            ContractAuditOfEToGInfo info = new ContractAuditOfEToGInfo();

            if (dataRow != null)
            {
                if (dataRow["FormID"] != null)
                {
                    info.FormID = dataRow["FormID"].ToString();
                }
                if (dataRow["SecurityLevel"] != null)
                {
                    info.SecurityLevel = dataRow["SecurityLevel"].ToString();
                }
                if (dataRow["UrgenLevel"] != null)
                {
                    info.UrgenLevel = dataRow["UrgenLevel"].ToString();
                }
                if (dataRow["DeptName"] != null)
                {
                    info.DeptName = dataRow["DeptName"].ToString();
                }
                if (dataRow["DeptCode"] != null)
                {
                    info.DeptCode = dataRow["DeptCode"].ToString();
                }
                if (dataRow["UserName"] != null)
                {
                    info.UserName = dataRow["UserName"].ToString();
                }
                if (dataRow["Mobile"] != null)
                {
                    info.Mobile = dataRow["Mobile"].ToString();
                }
                if (dataRow["DateTime"] != null)
                {
                    info.DateTime = dataRow["DateTime"].ToString();
                }
                if (dataRow["ContractType1"] != null)
                {
                    info.ContractType1 = dataRow["ContractType1"].ToString();
                }
                if (dataRow["ContractTypeName1"] != null)
                {
                    info.ContractTypeName1 = dataRow["ContractTypeName1"].ToString();
                }
                if (dataRow["ContractType2"] != null)
                {
                    info.ContractType2 = dataRow["ContractType2"].ToString();
                }
                if (dataRow["ContractTypeName2"] != null)
                {
                    info.ContractTypeName2 = dataRow["ContractTypeName2"].ToString();
                }
                if (dataRow["ContractType3"] != null)
                {
                    info.ContractType3 = dataRow["ContractType3"].ToString();
                }
                if (dataRow["ContractTypeName3"] != null)
                {
                    info.ContractTypeName3 = dataRow["ContractTypeName3"].ToString();
                }
                if (dataRow["ContractSum"] != null)
                {
                    info.ContractSum = dataRow["ContractSum"].ToString();
                }
                if (dataRow["IsSupplementProtocol"] != null)
                {
                    info.IsSupplementProtocol = dataRow["IsSupplementProtocol"].ToString();
                }
                if (dataRow["IsSupplementProtocolText"] != null)
                {
                    info.IsSupplementProtocolText = dataRow["IsSupplementProtocolText"].ToString();
                }
                if (dataRow["IsFormatContract"] != null)
                {
                    info.IsFormatContract = dataRow["IsFormatContract"].ToString();
                }
                if (dataRow["IsNormText"] != null)
                {
                    info.IsNormText = dataRow["IsNormText"].ToString();
                }
                if (dataRow["IsBidding"] != null)
                {
                    info.IsBidding = dataRow["IsBidding"].ToString();
                }
                if (dataRow["IsEstateProject"] != null)
                {
                    info.IsEstateProject = dataRow["IsEstateProject"].ToString();
                }
                if (dataRow["EstateProjectName"] != null)
                {
                    info.EstateProjectName = dataRow["EstateProjectName"].ToString();
                }
                if (dataRow["EstateProjectNum"] != null)
                {
                    info.EstateProjectNum = dataRow["EstateProjectNum"].ToString();
                }
                if (dataRow["EstateProjectNameText"] != null)
                {
                    info.EstateProjectNameText = dataRow["EstateProjectNameText"].ToString();
                }
                if (dataRow["EstateProjectNumText"] != null)
                {
                    info.EstateProjectNumText = dataRow["EstateProjectNumText"].ToString();
                }
                if (dataRow["ContractSubject"] != null)
                {
                    info.ContractSubject = dataRow["ContractSubject"].ToString();
                }
                if (dataRow["ContractSubjectName"] != null)
                {
                    info.ContractSubjectName = dataRow["ContractSubjectName"].ToString();
                }
                if (dataRow["ContractSubjectName2"] != null)
                {
                    info.ContractSubjectName2 = dataRow["ContractSubjectName2"].ToString();
                }
                if (dataRow["ContractSubjectName3"] != null)
                {
                    info.ContractSubjectName3 = dataRow["ContractSubjectName3"].ToString();
                }
                if (dataRow["ContractSubjectName4"] != null)
                {
                    info.ContractSubjectName4 = dataRow["ContractSubjectName4"].ToString();
                }
                if (dataRow["ContractTitle"] != null)
                {
                    info.ContractTitle = dataRow["ContractTitle"].ToString();
                }
                if (dataRow["ContractContent"] != null)
                {
                    info.ContractContent = dataRow["ContractContent"].ToString();
                }
                if (dataRow["LeadersSelected"] != null)
                {
                    info.LeadersSelected = dataRow["LeadersSelected"].ToString();
                }
                if (dataRow["IsReport"] != null)
                {
                    info.IsReport = dataRow["IsReport"].ToString();
                }
                if (dataRow["IsApproval"] != null)
                {
                    info.IsApproval = dataRow["IsApproval"].ToString();
                }
                if (dataRow["RelatedFormID"] != null)
                {
                    info.RelatedFormID = dataRow["RelatedFormID"].ToString();
                }
            }
            return info;
        }
        /// <summary>
        /// 插入数据到表中
        /// </summary>
        /// <param name="obj"></param>
       public void Insert(ContractAuditOfEToGInfo model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.NVarChar,100),
					new SqlParameter("@SecurityLevel", SqlDbType.NVarChar,100),
					new SqlParameter("@UrgenLevel", SqlDbType.NVarChar,100),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,200),
					new SqlParameter("@DeptCode", SqlDbType.NVarChar,100),
					new SqlParameter("@UserName", SqlDbType.NVarChar,100),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,100),
					new SqlParameter("@DateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@ContractSum", SqlDbType.NVarChar,100),
					new SqlParameter("@IsSupplementProtocol", SqlDbType.NVarChar,100),
					new SqlParameter("@IsFormatContract", SqlDbType.NVarChar,100),
					new SqlParameter("@IsNormText", SqlDbType.NVarChar,100),
					new SqlParameter("@IsBidding", SqlDbType.NVarChar,100),
					new SqlParameter("@IsEstateProject", SqlDbType.NVarChar,100),
					new SqlParameter("@EstateProjectName", SqlDbType.NVarChar,300),
					new SqlParameter("@EstateProjectNum", SqlDbType.NVarChar,100),
					new SqlParameter("@ContractTitle", SqlDbType.NVarChar,500),
					new SqlParameter("@ContractContent", SqlDbType.NVarChar,4000),
					new SqlParameter("@LeadersSelected", SqlDbType.NVarChar,4000),
					new SqlParameter("@IsReport", SqlDbType.NVarChar,100),
					new SqlParameter("@ContractType1", SqlDbType.NVarChar,100),
					new SqlParameter("@ContractTypeName1", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractType2", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractTypeName2", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractType3", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractTypeName3", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractSubject", SqlDbType.NVarChar,500),
					new SqlParameter("@ContractSubjectName", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractSubjectName2", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractSubjectName3", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractSubjectName4", SqlDbType.NVarChar,200),
                    new SqlParameter("@IsSupplementProtocolText", SqlDbType.NVarChar,50),
                    new SqlParameter("@RelatedFormID", SqlDbType.NVarChar,200),
                    new SqlParameter("@EstateProjectNameText", SqlDbType.NVarChar,300),
                    new SqlParameter("@EstateProjectNumText", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.FormID;
            parameters[1].Value = model.SecurityLevel;
            parameters[2].Value = model.UrgenLevel;
            parameters[3].Value = model.DeptName;
            parameters[4].Value = model.DeptCode;
            parameters[5].Value = model.UserName;
            parameters[6].Value = model.Mobile;
            parameters[7].Value = model.DateTime;
            parameters[8].Value = model.ContractSum;
            parameters[9].Value = model.IsSupplementProtocol;
            parameters[10].Value = model.IsFormatContract;
            parameters[11].Value = model.IsNormText;
            parameters[12].Value = model.IsBidding;
            parameters[13].Value = model.IsEstateProject;
            parameters[14].Value = model.EstateProjectName;
            parameters[15].Value = model.EstateProjectNum;
            parameters[16].Value = model.ContractTitle;
            parameters[17].Value = model.ContractContent;
            parameters[18].Value = model.LeadersSelected;
            parameters[19].Value = model.IsReport;
            parameters[20].Value = model.ContractType1;
            parameters[21].Value = model.ContractTypeName1;
            parameters[22].Value = model.ContractType2;
            parameters[23].Value = model.ContractTypeName2;
            parameters[24].Value = model.ContractType3;
            parameters[25].Value = model.ContractTypeName3;
            parameters[26].Value = model.ContractSubject;
            parameters[27].Value = model.ContractSubjectName;
            parameters[28].Value = model.ContractSubjectName2;
            parameters[29].Value = model.ContractSubjectName3;
            parameters[30].Value = model.ContractSubjectName4;
            parameters[31].Value = model.IsSupplementProtocolText;
            parameters[32].Value = model.RelatedFormID;
            parameters[33].Value = model.EstateProjectNameText;
            parameters[34].Value = model.EstateProjectNumText;


            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.OA_ContractAuditOfEToGInfo_Insert", parameters);
        }
        /// <summary>
        /// 更新表
        /// </summary>
        /// <param name="obj"></param>
       public void Update(ContractAuditOfEToGInfo model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@FormID", SqlDbType.NVarChar,100),
					new SqlParameter("@SecurityLevel", SqlDbType.NVarChar,100),
					new SqlParameter("@UrgenLevel", SqlDbType.NVarChar,100),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,200),
					new SqlParameter("@DeptCode", SqlDbType.NVarChar,100),
					new SqlParameter("@UserName", SqlDbType.NVarChar,100),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,100),
					new SqlParameter("@DateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@ContractSum", SqlDbType.NVarChar,100),
					new SqlParameter("@IsSupplementProtocol", SqlDbType.NVarChar,100),
					new SqlParameter("@IsFormatContract", SqlDbType.NVarChar,100),
					new SqlParameter("@IsNormText", SqlDbType.NVarChar,100),
					new SqlParameter("@IsBidding", SqlDbType.NVarChar,100),
					new SqlParameter("@IsEstateProject", SqlDbType.NVarChar,100),
					new SqlParameter("@EstateProjectName", SqlDbType.NVarChar,300),
					new SqlParameter("@EstateProjectNum", SqlDbType.NVarChar,100),
					new SqlParameter("@ContractTitle", SqlDbType.NVarChar,500),
					new SqlParameter("@ContractContent", SqlDbType.NVarChar,4000),
					new SqlParameter("@LeadersSelected", SqlDbType.NVarChar,4000),
					new SqlParameter("@IsReport", SqlDbType.NVarChar,100),
					new SqlParameter("@ContractType1", SqlDbType.NVarChar,100),
					new SqlParameter("@ContractTypeName1", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractType2", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractTypeName2", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractType3", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractTypeName3", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractSubject", SqlDbType.NVarChar,500),
					new SqlParameter("@ContractSubjectName", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractSubjectName2", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractSubjectName3", SqlDbType.NVarChar,200),
					new SqlParameter("@ContractSubjectName4", SqlDbType.NVarChar,200),
                    new SqlParameter("@IsSupplementProtocolText", SqlDbType.NVarChar,50),
                    new SqlParameter("@RelatedFormID", SqlDbType.NVarChar,200),
                    new SqlParameter("@EstateProjectNameText", SqlDbType.NVarChar,300),
                    new SqlParameter("@EstateProjectNumText", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.FormID;
            parameters[1].Value = model.SecurityLevel;
            parameters[2].Value = model.UrgenLevel;
            parameters[3].Value = model.DeptName;
            parameters[4].Value = model.DeptCode;
            parameters[5].Value = model.UserName;
            parameters[6].Value = model.Mobile;
            parameters[7].Value = model.DateTime;
            parameters[8].Value = model.ContractSum;
            parameters[9].Value = model.IsSupplementProtocol;
            parameters[10].Value = model.IsFormatContract;
            parameters[11].Value = model.IsNormText;
            parameters[12].Value = model.IsBidding;
            parameters[13].Value = model.IsEstateProject;
            parameters[14].Value = model.EstateProjectName;
            parameters[15].Value = model.EstateProjectNum;
            parameters[16].Value = model.ContractTitle;
            parameters[17].Value = model.ContractContent;
            parameters[18].Value = model.LeadersSelected;
            parameters[19].Value = model.IsReport;
            parameters[20].Value = model.ContractType1;
            parameters[21].Value = model.ContractTypeName1;
            parameters[22].Value = model.ContractType2;
            parameters[23].Value = model.ContractTypeName2;
            parameters[24].Value = model.ContractType3;
            parameters[25].Value = model.ContractTypeName3;
            parameters[26].Value = model.ContractSubject;
            parameters[27].Value = model.ContractSubjectName;
            parameters[28].Value = model.ContractSubjectName2;
            parameters[29].Value = model.ContractSubjectName3;
            parameters[30].Value = model.ContractSubjectName4;
            parameters[31].Value = model.IsSupplementProtocolText;
            parameters[32].Value = model.RelatedFormID;
            parameters[33].Value = model.EstateProjectNameText;
            parameters[34].Value = model.EstateProjectNumText;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.OA_ContractAuditOfEToGInfo_Update", parameters);
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool UpdateStatus(string ID, string strStatus)
        {
            string sql = string.Format("UPDATE Biz.OA_ContractAuditOfEToG SET  IsApproval = '{0}'"
                                        + " where FormID = '{1}'", strStatus, ID);
            return DBHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 通过流程ID得到表单信息
        /// </summary>
        /// <param name="wfInstanceId"></param>
        /// <returns></returns>
        public ContractAuditOfEToGInfo GetInfoByWfId(string wfInstanceId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@WFInstanceId", System.Data.SqlDbType.NVarChar, 100)
            };
            parameters[0].Value = wfInstanceId;
            ContractAuditOfEToGInfo model = new ContractAuditOfEToGInfo();

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.OA_ContractAuditOfEToG_GetBywfId", parameters);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return DataRowToModel(dataTable.Rows[0]);
            }
            else
            {
                return null;
            }
        }
       //app3008
        public ContractAuditOfEToGInfo GetContractAuditOfEToGInfo(string formId)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = formId;

            ContractAuditOfEToGInfo model = new ContractAuditOfEToGInfo();
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.OA_ContractAuditOfEToGInfoByFormID_Get", parameters);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return DataRowToModel(dataTable.Rows[0]);
            }
            else
            {
                return null;
            }
        }
    }
}
