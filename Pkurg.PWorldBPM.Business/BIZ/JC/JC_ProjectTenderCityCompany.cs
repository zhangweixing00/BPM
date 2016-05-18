using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
   public  class JC_ProjectTenderCityCompany
    {
       private string className = "JC_ProjectTenderCityCompany";

       /// <summary>
       /// 通过FormID得到一个对象实体
       /// </summary>
       /// <param name="FormID"></param>
       /// <returns></returns>
       public static JC_ProjectTenderCityCompanyInfo GetJC_ProjectTenderCityCompanyInfoByFormID(string FormID)
       {  
           SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
           parameters[0].Value = FormID;

           JC_ProjectTenderCityCompanyInfo model = new JC_ProjectTenderCityCompanyInfo();
           DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderCityCompanyInfoByFormID_Get", parameters);

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
       /// 得到一个实体对象[封装]
       /// </summary>
       /// <param name="dataRow"></param>
       /// <returns></returns>
       private static JC_ProjectTenderCityCompanyInfo DataRowToModel(DataRow dataRow)
       {
           JC_ProjectTenderCityCompanyInfo info = new JC_ProjectTenderCityCompanyInfo();

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
               if (dataRow["RelateDepartment"] != null)
               {
                   info.RelateDepartment = dataRow["RelateDepartment"].ToString();
               }
               if (dataRow["GroupRealateDept"] != null)
               {
                   info.GroupRealateDept = dataRow["GroupRealateDept"].ToString();
               }
               if (dataRow["GroupPurchaseDept"] != null)
               {
                   info.GroupPurchaseDept = dataRow["GroupPurchaseDept"].ToString();
               }
               if (dataRow["IsApproval"] != null)
               {
                   info.IsApproval = dataRow["IsApproval"].ToString();
               }
               if (dataRow["Remark"] != null)
               {
                   info.Remark = dataRow["Remark"].ToString();
               }
               if (dataRow["FirstLevel"] != null)
               {
                   info.FirstLevel = dataRow["FirstLevel"].ToString();
               }
           }
               return info;
       }
      
       /// <summary>
       /// 插入新的表单数据
       /// </summary>
       /// <param name="info"></param>
       public static void InsertJC_ProjectTenderCityCompanyInfo(JC_ProjectTenderCityCompanyInfo info)
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
            new SqlParameter("@RelateDepartment",SqlDbType.NVarChar,500),
            new SqlParameter("@GroupRealateDept",SqlDbType.NVarChar,500),
            new SqlParameter("@GroupPurchaseDept",SqlDbType.NVarChar,500),
            new SqlParameter("@Remark",SqlDbType.NVarChar,500),
            new SqlParameter("@FirstLevel",SqlDbType.NVarChar,100)
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
           parameters[11].Value = info.RelateDepartment;
           parameters[12].Value = info.GroupRealateDept;
           parameters[13].Value = info.GroupPurchaseDept;
           parameters[14].Value = info.Remark;
           parameters[15].Value = info.FirstLevel;

           DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderCityCompanyInfo_Insert", parameters);
       }
       /// <summary>
       /// 更新表单数据
       /// </summary>
       /// <param name="info"></param>
       public static void UpdateJC_ProjectTenderCityCompanyInfo(JC_ProjectTenderCityCompanyInfo info)
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
            new SqlParameter("@RelateDepartment",SqlDbType.NVarChar,500),
            new SqlParameter("@GroupRealateDept",SqlDbType.NVarChar,500),
            new SqlParameter("@GroupPurchaseDept",SqlDbType.NVarChar,500),
            new SqlParameter("@Remark",SqlDbType.NVarChar,500),
            new SqlParameter("@FirstLevel",SqlDbType.NVarChar,100)
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
            parameters[11].Value = info.RelateDepartment;
            parameters[12].Value = info.GroupRealateDept;
            parameters[13].Value = info.GroupPurchaseDept;
            parameters[14].Value = info.Remark;
            parameters[15].Value = info.FirstLevel;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderCityCompanyInfo_Update", parameters);
       }
      
       /// <summary>
       /// 通过FormID更新表单IsApproval字段
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static JC_ProjectTenderCityCompanyInfo UpdateJC_ProjectTenderCityCompanyInfoByModel(JC_ProjectTenderCityCompanyInfo model)
       {
           SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsApproval",System.Data.SqlDbType.NVarChar,100),
            };
           parameters[0].Value = model.FormID;
           parameters[1].Value = model.IsApproval;

           DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderCityCompanyInfoByID_Update", parameters);
           return model;
       }

       /// <summary>
       ///  根据流程ID获取FormId
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static JC_ProjectTenderCityCompanyInfo GetJC_ProjectTenderCityCompanyInfoByWfId(string id)
       {
           SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,100)			};
           parameters[0].Value = id;

           JC_ProjectTenderCityCompanyInfo model = new JC_ProjectTenderCityCompanyInfo();
           DataTable dt = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderCityCompanyInfoByWfId_Get", parameters);
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
       /// 给表WF_Approval_Record插入数据
       /// </summary>
       /// <param name="currentActiveName"></param>
       /// <param name="LoginName"></param>
       /// <param name="WFInstanceId"></param>
       public static void InsertWF_Approval_RecordByInstanceID(string currentActiveName, string LoginName, string WFInstanceId)
       {
           ApprovalRecord appRecord = new ApprovalRecord();
           SqlParameter[] parameters ={
                                          new SqlParameter("@WfInstanceID",SqlDbType.NVarChar,100),
                                          new SqlParameter("@CurrentActiveName",SqlDbType.NVarChar,200),
                                          new SqlParameter("@LoginName",SqlDbType.NVarChar,50)
                                      };

           parameters[0].Value = WFInstanceId;
           parameters[1].Value = currentActiveName;
           parameters[2].Value = LoginName;

           DataTable datatable = DBHelper.ExecutedProcedure("Biz.WF_Approval_Record_Insert", parameters);
       }

       public static JC_ProjectTenderCityCompanyInfo GetJC_ProjectTenderCityCompanyInfoByFormId(string ProId)
       {
           SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
           parameters[0].Value = ProId;

           JC_ProjectTenderCityCompanyInfo model = new JC_ProjectTenderCityCompanyInfo();
           DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderCityCompanyInfoByFormID_Get", parameters);

           if (dataTable != null && dataTable.Rows.Count > 0)
           {
               return DataRowToModel(dataTable.Rows[0]);
           }
           else
           {
               return null;
           } 
       }
       //appflow_10113
       public JC_ProjectTenderCityCompanyInfo GetJC_ProjectTenderCityCompanyInfo(string formId)
       {
           SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
           parameters[0].Value = formId;

           JC_ProjectTenderCityCompanyInfo model = new JC_ProjectTenderCityCompanyInfo();
           DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderCityCompanyInfoByFormID_Get", parameters);

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
