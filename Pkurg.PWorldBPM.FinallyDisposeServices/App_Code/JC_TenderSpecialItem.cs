using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Pkurg.PWorldBPM.FinallyDisposeServices;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using System.Data;
using Pkurg.PWorld.Entities;
using Pkurg.PWorld.Business.Permission;

/// <summary>
///JC_TenderSpecialItem 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class JC_TenderSpecialItem : Pkurg.PWorldBPM.FinallyDisposeServices.FinallyDisposeServiceBase
{

    //部门编号都写在web配置里，在这里需要调用
    string CGDeptCode = System.Configuration.ConfigurationManager.AppSettings["CGDeptCode"];
    static string url = System.Configuration.ConfigurationManager.AppSettings["URL"];
    /// <summary>
    /// 重写流程结束方法
    /// </summary>
    public override Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo DoServiceEvent(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        Logger.logger.DebugFormat("Params:{0},{1}", k2_workflowId, dataFields.Keys.Count);
        foreach (KeyValuePair<string, string> item in dataFields)
        {
            Logger.logger.DebugFormat("df:{0}-{1}", item.Key, item.Value);
        }
        Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo info = new ExecuteResultInfo();
        try
        {
            //根据流程ID获取FormId
            JC_TenderSpecialItemInfo companyInfo = Pkurg.PWorldBPM.Business.BIZ.JC.JC_TenderSpecialItem.GetJC_TenderSpecialItemInfoByWfId(k2_workflowId.ToString());
            string jcFormCode = companyInfo.FormID;
            string jcFormName = companyInfo.Title;
            string StartUserName = companyInfo.UserName;
            //判断是否审批通过,存储0，1表示通过与不通过
            string resultString = dataFields["IsPass"];

            //这部分是根据自己的流程来判定的，现在只是通过流程ID得到FormId来进行判断结束
            Logger.logger.DebugFormat("Proc:{0},{1}", jcFormCode, resultString);

            JC_TenderSpecialItemInfo model = new JC_TenderSpecialItemInfo();
            model.FormID = jcFormCode;
            model.IsApproval = resultString;
            //两个文件名字最好不能起的一样，尤其是增删改查操作的时候文件名不能一样
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_TenderSpecialItem.UpdateJC_TenderSpecialItemInfoInfoByModel(model);



            //流程结束之后授权给集团招采部门所有人
            //得到各参数的值：AuthorizedByUserCode：授权人编号；AuthorizedByUserName：授权人姓名；ProId：流程ID；ProcName：流程编号；AuthorizedUserCode：被授权部门编号；AuthorizedUserName：被授权部门名称
            DataTable dt = Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstances.GetWorkFlowInstanceByFormID(jcFormCode);
            DataRow dataRow = dt.Rows[0];

            string AuthorizedByUserCode = dataRow["CreateByUserCode"].ToString();
            string AuthorizedByUserName = dataRow["CreateByUserName"].ToString();
            string ProId = jcFormCode;
            string ProcName = jcFormName;
            //得到子公司的招采部的编号以及名称
            Pkurg.PWorld.Entities.Department deptInfo = new Pkurg.PWorld.Business.Permission.BFPmsUserRoleDepartment().GetDeptByCurrentDeptCodeAndOtherDeptName(companyInfo.StartDeptId, "招标采购部");

            //授权给所在公司的招标采购部
            InsertAuthorizationToJCGroup(AuthorizedByUserCode, AuthorizedByUserName, ProId, ProcName, deptInfo.DepartCode);
            //授权给集团的采购管理部
            InsertAuthorizationToJCGroup(AuthorizedByUserCode, AuthorizedByUserName, ProId, ProcName, CGDeptCode);

            info.IsSuccess = true;
        }
        catch (Exception ex)
        {
            info.ExecException = ex.StackTrace;
            info.IsSuccess = false;
        }
        Logger.logger.DebugFormat("Result:{0}", info.IsSuccess);
        Logger.logger.DebugFormat("Result-e:{0}", info.ExecException);
        return info;
    }
    /// <summary>
    /// 得到集团采购管理部的成员
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    private static IList<VEmployeeAndAdditionalInfo> GetUserListByDeptCode(string code)
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        IList<VEmployeeAndAdditionalInfo> ds = new List<VEmployeeAndAdditionalInfo>();

        DataTable dtUsers = bfurd.GetSelectRoleUser(code, "招采流程收文人");

        if (dtUsers == null || dtUsers.Rows.Count <= 0)
        {
            ds = bfurd.GetNotSelectRoleUser("0");
            ds = ds.Where(x => x.DepartCode == code).ToList();
        }
        else
        {
            foreach (DataRow item in dtUsers.Rows)
            {
                ds.Add(new VEmployeeAndAdditionalInfo()
                {
                    LoginName = item["LoginName"].ToString(),
                    DepartName = item["DepartName"].ToString(),
                    EmployeeName = item["EmployeeName"].ToString(),
                    Email = item["LoginName"].ToString() + "@jtmail.founder.com",
                    EmployeeCode = item["EmployeeCode"].ToString()
                });
            }
        }
        return ds;
    }
    /// <summary>
    /// 插入授权表（授权给招采部门全部成员）  from  XX to XX 的格式
    /// 在该部分中的被授权人是一个部门的集合，所以需要获取部门的编号，遍历得到该部门的所有成员，并对每个成员进行授权
    /// </summary>
    /// <param name="AuthorizedByUserCode">授权人的编号</param>
    /// <param name="AuthorizedByUserName">授权人的姓名</param>
    /// <param name="ProId">流程ID,需要从dispose调用过程中就取到值</param>
    /// <param name="ProcName">流程名称，需要从dispose调用过程中就取到值</param>
    /// <param name="AuthorizedUserCode">被授权人编号(部门编号)</param>
    /// <param name="AuthorizedUserName">被授权人姓名（部门名称）</param>
    /// <returns></returns>

    public static void InsertAuthorizationToJCGroup(string AuthorizedByUserCode, string AuthorizedByUserName, string ProId, string ProcName, string AuthorizeDeptCode)
    {
        //发送邮件
        MailService mailService = new MailService();

        IList<VEmployeeAndAdditionalInfo> ds = GetUserListByDeptCode(AuthorizeDeptCode);
        foreach (var item in ds)
        {
            string AuthorizedUserCode = item.EmployeeCode;
            string AuthorizedUserName = item.EmployeeName;


            Pkurg.PWorldBPM.Business.Workflow.WF_Authorization.InsertAuthorization(AuthorizedByUserCode, AuthorizedByUserName, ProId, ProcName, AuthorizedUserCode, AuthorizedUserName);
            //发送邮件
            //定义两个变量，将title以及body使用字符串方式拼接出来
            JC_TenderSpecialItemInfo citycompanyInfo = Pkurg.PWorldBPM.Business.BIZ.JC.JC_TenderSpecialItem.GetJC_TenderSpecialItemInfoByFormId(ProId);
            string emailTitle = citycompanyInfo.DeptName + citycompanyInfo.UserName + "于" + citycompanyInfo.Date + "发起的" + citycompanyInfo.Title + "流程已审批结束！";
            Logger.logger.DebugFormat("WF_Authorization:{0}", AuthorizedUserCode);
            //得到实例ID号
            DataTable dt = Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstances.GetWorkFlowInstanceByFormID(ProId);
            DataRow dataRow = dt.Rows[0];

            string WorkFlowInstancesID = dataRow["InstanceID"].ToString();

            string ID = url + "/Workflow/ViewPage/V_JC_ProjectTenderCityCompany.aspx?id=" + WorkFlowInstancesID;

            string emailFinallyBodyFormat = @"您好！
       <br/>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + emailTitle + @"
       <br/>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;若要查看审批意见点击此处：&nbsp;&nbsp;&nbsp;&nbsp;<a href='" + ID + @"'>查看</a>
         <br/>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;谢谢
          <br/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";


            bool eResult = mailService.SendEmailCustom(item.Email, emailTitle, emailFinallyBodyFormat);

        }
    }

    /// <summary>
    /// 正式系统要开启
    /// </summary>
    /// <param name="k2_workflowId"></param>
    private void DoSendEmail(int k2_workflowId)
    {
        try
        {
            MailService mailService = new MailService();
            bool eResult = mailService.SendEndEmail(k2_workflowId);
            Logger.logger.DebugFormat("Email--Result:{0}", eResult);

        }
        catch (Exception ex)
        {
            Logger.logger.DebugFormat("Email--Result:{0}", false);
            Logger.logger.DebugFormat("Email--Result-e:{0}", ex.StackTrace);
        }
    }
}
