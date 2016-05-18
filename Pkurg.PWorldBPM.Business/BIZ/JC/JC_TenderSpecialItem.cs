using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
   public class JC_TenderSpecialItem
    {
       //根据formid得到表单数据
       public static JC_TenderSpecialItemInfo GetJC_TenderSpecialItemInfoByFormID(string FormID)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = FormID;

            JC_TenderSpecialItemInfo model = new JC_TenderSpecialItemInfo();
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_TenderSpecialItemInfoByFormID_Get", parameters);

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
       /// 封装成一个实体
       /// </summary>
       /// <param name="dataRow"></param>
       /// <returns></returns>
       private static JC_TenderSpecialItemInfo DataRowToModel(DataRow dataRow)
       {
           JC_TenderSpecialItemInfo info = new JC_TenderSpecialItemInfo();

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
               if (dataRow["StartDeptId"] != null)
               {
                   info.StartDeptId = dataRow["StartDeptId"].ToString();
               }
               if (dataRow["DeptName"] != null)
               {
                   info.DeptName = dataRow["DeptName"].ToString();
               }
               if (dataRow["UserName"] != null)
               {
                   info.UserName = dataRow["UserName"].ToString();
               }
               if (dataRow["Tel"] != null)
               {
                   info.Tel = dataRow["Tel"].ToString();
               }
               if (dataRow["Date"] != null)
               {
                   info.Date = dataRow["Date"].ToString();
               }
               if (dataRow["IsAccreditByGroup"] != null)
               {
                   info.IsAccreditByGroup = dataRow["IsAccreditByGroup"].ToString();
               }
               if (dataRow["Title"] != null)
               {
                   info.Title = dataRow["Title"].ToString();
               }
               if (dataRow["Substance"] != null)
               {
                   info.Substance = dataRow["Substance"].ToString();
               }
               if (dataRow["IsApproval"] != null)
               {
                   info.IsApproval = dataRow["IsApproval"].ToString();
               }
               if (dataRow["Remark"] != null)
               {
                   info.Remark = dataRow["Remark"].ToString();
               }
               //if (dataRow["CounterSignNum"] != null)
               //{
               //    info.CounterSignNum = dataRow["CounterSignNum"].ToString();
               //}
               //if (dataRow["CounterSignUsers"] != null)
               //{
               //    info.CounterSignUsers = dataRow["CounterSignUsers"].ToString();
               //}
               //if (dataRow["CounterSignNumGroup"] != null)
               //{
               //    info.CounterSignNumGroup = dataRow["CounterSignNumGroup"].ToString();
               //}
               //if (dataRow["CounterSignUsersGroup"] != null)
               //{
               //    info.CounterSignUsersGroup = dataRow["CounterSignUsersGroup"].ToString();
               //}
           }
           return info;
       }
       /// <summary>
       /// 插入新的表单数据
       /// </summary>
       /// <param name="info"></param>
        public static void InsertJC_TenderSpecialItemInfo(JC_TenderSpecialItemInfo info)
        {
            SqlParameter[] parameters ={
            new SqlParameter("@FormID",SqlDbType.NVarChar,100),
            new SqlParameter("@SecurityLevel",SqlDbType.NVarChar,100),
            new SqlParameter("@UrgenLevel",SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",SqlDbType.NVarChar,100),
            new SqlParameter("@DeptName",SqlDbType.NVarChar,100),
            new SqlParameter("@UserName",SqlDbType.NVarChar,100),
            new SqlParameter("@Tel",SqlDbType.NVarChar,100),
            new SqlParameter("@Date",SqlDbType.NVarChar,100),
            new SqlParameter("@IsAccreditByGroup",SqlDbType.NVarChar,100),
            new SqlParameter("@Title",SqlDbType.NVarChar,200),
            new SqlParameter("@Substance",SqlDbType.NVarChar,2000),
            new SqlParameter("@Remark",SqlDbType.NVarChar,500)
            //new SqlParameter("@CounterSignNum",SqlDbType.NVarChar,500),
            //new SqlParameter("@CounterSignUsers",SqlDbType.NVarChar,500),
            //new SqlParameter("@CounterSignNumGroup",SqlDbType.NVarChar,500),
            //new SqlParameter("@CounterSignUsersGroup",SqlDbType.NVarChar,500)
                                      };
            parameters[0].Value = info.FormID;
            parameters[1].Value = info.SecurityLevel;
            parameters[2].Value = info.UrgenLevel;
            parameters[3].Value = info.StartDeptId;
            parameters[4].Value = info.DeptName;
            parameters[5].Value = info.UserName;
            parameters[6].Value = info.Tel;
            parameters[7].Value = info.Date;
            parameters[8].Value = info.IsAccreditByGroup;
            parameters[9].Value = info.Title;
            parameters[10].Value = info.Substance;
            parameters[11].Value = info.Remark;
            //parameters[12].Value = info.CounterSignNum;
            //parameters[13].Value = info.CounterSignUsers;
            //parameters[14].Value = info.CounterSignNumGroup;
            //parameters[15].Value = info.CounterSignUsersGroup;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_TenderSpecialItemInfo_Insert", parameters);
        }
        /// <summary>
        /// 更新表单数据
        /// </summary>
        /// <param name="info"></param>
        public static void UpdateJC_TenderSpecialItemInfo(JC_TenderSpecialItemInfo info)
        {
            SqlParameter[] parameters ={
            new SqlParameter("@FormID",SqlDbType.NVarChar,100),
            new SqlParameter("@SecurityLevel",SqlDbType.NVarChar,100),
            new SqlParameter("@UrgenLevel",SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",SqlDbType.NVarChar,100),
            new SqlParameter("@DeptName",SqlDbType.NVarChar,100),
            new SqlParameter("@UserName",SqlDbType.NVarChar,100),
            new SqlParameter("@Tel",SqlDbType.NVarChar,100),
            new SqlParameter("@Date",SqlDbType.NVarChar,100),
            new SqlParameter("@IsAccreditByGroup",SqlDbType.NVarChar,100),
            new SqlParameter("@Title",SqlDbType.NVarChar,200),
            new SqlParameter("@Substance",SqlDbType.NVarChar,2000),
            new SqlParameter("@Remark",SqlDbType.NVarChar,500)
            //new SqlParameter("@CounterSignNum",SqlDbType.NVarChar,500),
            //new SqlParameter("@CounterSignUsers",SqlDbType.NVarChar,500),
            //new SqlParameter("@CounterSignNumGroup",SqlDbType.NVarChar,500),
            //new SqlParameter("@CounterSignUsersGroup",SqlDbType.NVarChar,500)
                                      };
            parameters[0].Value = info.FormID;
            parameters[1].Value = info.SecurityLevel;
            parameters[2].Value = info.UrgenLevel;
            parameters[3].Value = info.StartDeptId;
            parameters[4].Value = info.DeptName;
            parameters[5].Value = info.UserName;
            parameters[6].Value = info.Tel;
            parameters[7].Value = info.Date;
            parameters[8].Value = info.IsAccreditByGroup;
            parameters[9].Value = info.Title;
            parameters[10].Value = info.Substance;
            parameters[11].Value = info.Remark;
            //parameters[12].Value = info.CounterSignNum;
            //parameters[13].Value = info.CounterSignUsers;
            //parameters[14].Value = info.CounterSignNumGroup;
            //parameters[15].Value = info.CounterSignUsersGroup;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_TenderSpecialItemInfo_Update", parameters);
        }
        /// <summary>
        ///  根据流程ID获取FormId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static JC_TenderSpecialItemInfo GetJC_TenderSpecialItemInfoByWfId(string id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,100)			};
            parameters[0].Value = id;

            JC_TenderSpecialItemInfo model = new JC_TenderSpecialItemInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.JC_TenderSpecialItemInfoByWfId_Get", parameters);
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
        /// 通过FormID更新表单IsApproval字段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static JC_TenderSpecialItemInfo UpdateJC_TenderSpecialItemInfoInfoByModel(JC_TenderSpecialItemInfo model)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsApproval",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = model.FormID;
            parameters[1].Value = model.IsApproval;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_TenderSpecialItemInfoByID_Update", parameters);
            return model;
        }
       /// <summary>
       /// 根据流程ID得到流程信息【结束】
       /// </summary>
       /// <param name="ProId"></param>
       /// <returns></returns>
        public static JC_TenderSpecialItemInfo GetJC_TenderSpecialItemInfoByFormId(string ProId)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = ProId;

            JC_TenderSpecialItemInfo model = new JC_TenderSpecialItemInfo();
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_TenderSpecialItemInfoByFormID_Get", parameters);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return DataRowToModel(dataTable.Rows[0]);
            }
            else
            {
                return null;
            } 
        }
       //appflow_2006
        public JC_TenderSpecialItemInfo GetJC_TenderSpecialItemInfo(string formId)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = formId;

            JC_TenderSpecialItemInfo model = new JC_TenderSpecialItemInfo();
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_TenderSpecialItemInfoByFormID_Get", parameters);

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
