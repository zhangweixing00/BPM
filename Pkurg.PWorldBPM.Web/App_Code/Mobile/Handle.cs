using System;
using System.Collections.Generic;
using System.DirectoryServices;

/// <summary>
/// app.ashx直接调用该类
/// </summary>
[System.Runtime.InteropServices.GuidAttribute("6B9AB995-97BF-49C8-9BFA-87CB90FE9FAE")]
public class Handle
{
    #region Public

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="account"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string Login(string account, string password)
    {
        bool flag = false;

        //模拟用户都是mobile_域账号，不去验证密码。
        //是否可以启动移动OA的模拟账号,正式环境配置AppSettings IsAllowMobileUser=0;
        string isAllowMobileUser = System.Configuration.ConfigurationManager.AppSettings["IsAllowMobileUser"];
        //移动模拟账号的开头,默认 mobile_
        string mobileTag = System.Configuration.ConfigurationManager.AppSettings["MobileTag"];
        if (string.IsNullOrEmpty(mobileTag))
        {
            mobileTag = "mobile_";
        }
        string mobileUser = account;
        if (account.StartsWith(mobileTag) && isAllowMobileUser != "0")
        {
            account = account.Replace(mobileTag, "");
            flag = true;
        }
        else
        {
            flag = CheckLogin(account, password);
        }

        //MobileLog.InsertLog("login", string.Format("account={0}", mobileUser), "", account);

        if (flag)
        {
            MobileUserInfo model = RequestProcess.GetMobileUserInfoByAccount(account);
            if (model != null)
            {
                if (model.UserCode == null)
                {
                    return "";
                }
                else
                {
                    //MobileLog.InsertLog("login", string.Format("account={0}", account), "Success", account);

                    model.UserEnCode = EnCode(account);
                    string json = JsonHelper.JsonSerializer(model);
                    return json;
                }
            }
            return "";
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 获取待办列表
    /// </summary>
    /// <param name="userCode"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public static string GetToDoList(string userCode, int pageIndex)
    {
        userCode = DeCode(userCode);

        //MobileLog.InsertLog("gettodolist", string.Format("usercode={0},pageIndex={1}", userCode, pageIndex), "", userCode);

        List<WorkFlowItem> items = RequestProcess.GetTodoList(userCode, pageIndex);
        //todo:从后台获取列表
        string json = JsonHelper.JsonSerializer(items);
        return json;
    }

    /// <summary>
    /// 获取已办列表
    /// </summary>
    /// <param name="userCode"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public static string GetDoneList(string userCode, int pageIndex)
    {
        userCode = DeCode(userCode);

        //MobileLog.InsertLog("getdonelist", string.Format("usercode={0},pageIndex={1}", userCode, pageIndex), "", userCode);

        List<WorkFlowItem> items = RequestProcess.GetDoneList(userCode, pageIndex);
        string json = JsonHelper.JsonSerializer(items);
        return json;
    }

    /// <summary>
    /// 获取归档列表
    /// </summary>
    /// <param name="userCode"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public static string GetArchiveList(string userCode, int pageIndex)
    {
        userCode = DeCode(userCode);

        //MobileLog.InsertLog("getarchivelist", string.Format("usercode={0},pageIndex={1}", userCode, pageIndex), "", userCode);

        List<WorkFlowItem> items = RequestProcess.GetArchiveList(userCode, ++pageIndex);
        //todo:从后台获取列表
        string json = JsonHelper.JsonSerializer(items);
        return json;
    }

    /// <summary>
    /// 获取流程详细
    /// </summary>
    /// <param name="instanceId"></param>
    /// <returns></returns>
    public static string GetWorkFlowInfo(string instanceId)
    {
        //MobileLog.InsertLog("getworkflowinfo", string.Format("instanceId={0}", instanceId), "", "");

        DetailInfo model = AppflowFactory.GetAppflow(instanceId).GetFlowInfoById(instanceId);
        //todo:从后台获取
        string json = JsonHelper.JsonSerializer(model);
        return json;
    }

    /// <summary>
    /// 审批动作
    /// </summary>
    /// <param name="userCode"></param>
    /// <param name="sn"></param>
    /// <param name="instanceId"></param>
    /// <param name="result">Y/N</param>
    /// <param name="remark">审批意见</param>
    /// <returns></returns>
    public static string Approve(string userCode, string sn, string instanceId, string result, string remark)
    {
        MobileLog.InsertLog("approve", string.Format("userCode={0},sn={1},instanceId={2},result={3},remark={4}", DeCode(userCode), sn, instanceId, result, remark), "", DeCode(userCode));
        result = result == "Y" ? "同意" : "不同意";
        bool isSuccess = AppflowFactory.GetAppflow(instanceId).StartApproval(sn, instanceId, remark, result);
        return JsonHelper.JsonSerializer(isSuccess);
    }

    /// <summary>
    /// 加签操作
    /// </summary>
    /// <param name="fromUserCode">加签人</param>
    /// <param name="toUserCode">被加签人</param>
    /// <param name="sn">SN号</param>
    /// <param name="sn">实例ID</param>
    /// <param name="remark">加签意见</param>
    /// <returns></returns>
    public static string AddSign(string fromUserCode, string toUserCode, string sn, string instanceId, string remark)
    {
        MobileLog.InsertLog("addsign", string.Format("fromUserCode={0},toUserCode={1},sn={2},instanceId={3},remark={4}", DeCode(fromUserCode), toUserCode, sn, instanceId, remark), "", DeCode(fromUserCode));
        fromUserCode = DeCode(fromUserCode);
        bool isSuccess = false;
        //todo:加签的实现(fromUserCode,toUserCode,sn)

        isSuccess = AppflowFactory.GetAppflow(instanceId).AddSign(fromUserCode,toUserCode,sn, instanceId, remark);
        return JsonHelper.JsonSerializer(isSuccess);
    }

    /// <summary>
    /// 根据关键字查询用户列表
    /// </summary>
    /// <param name="keyWord">查询关键字</param>
    /// <param name="pageIndex">当前页面</param>
    /// <param name="pageSize">页面大小</param>
    /// <returns></returns>
    public static string GetUserList(string keyWord, int pageIndex, int pageSize)
    {
        //MobileLog.InsertLog("getuserlist", string.Format("keyword={0},pageIndex={1},pageSize={2}", keyWord, pageIndex, pageSize), "", "");

        //List<MobileUserInfo> items = new List<MobileUserInfo>();
        //todo:获取用户列表(keyWord, pageIndex,pageSize)
        var items = RequestProcess.GetUserList(keyWord, ++pageIndex, pageSize);
        string json = JsonHelper.JsonSerializer(items);
        return json;
    }

    #endregion

    #region Private

    static bool CheckLogin(string userCode, string pasword)
    {
        try
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["LDAP"];
            if (string.IsNullOrEmpty(path))
            {
                path = "LDAP://172.25.1.3/DC=jtcorp,DC=founder,DC=com";
            }

            DirectoryEntry entry = new DirectoryEntry(path, userCode, pasword);
            return entry.NativeGuid != null;
        }
        catch (Exception)
        {
            return false;
        }
    }


    /// <summary>
    /// 域账号加密
    /// </summary>
    /// <param name="userCode"></param>
    /// <returns></returns>
    private static string EnCode(string userCode)
    {
        return DESEncrypt.Encrypt(userCode);
    }

    /// <summary>
    /// 域账号解密
    /// </summary>
    /// <param name="userCode"></param>
    /// <returns></returns>
    private static string DeCode(string userCode)
    {
        return DESEncrypt.Decrypt(userCode);
    }

    #endregion
}