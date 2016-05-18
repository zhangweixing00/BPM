using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pkurg.PWorldBPM.Business.BIZ.ERP;

/// <summary>
///PaymentApplication_Common 的摘要说明
/// </summary>
public class ContractApproval_Common
{
    public ContractApproval_Common()
    {

    }

    public static string GetErpUrl()
    {
        string erpFormId = HttpContext.Current.Request["erpFormId"];
        string erpFormType = HttpContext.Current.Request["erpFormType"];
        string startDeptId = HttpContext.Current.Request["startDeptId"];
        string id = HttpContext.Current.Request["Id"];

        if (string.IsNullOrEmpty(id))
        {//新建页面
            if (string.IsNullOrEmpty(erpFormId)
                || string.IsNullOrEmpty(erpFormType)
                )
            {

                //参数错误
                ExceptionHander.GoToErrorPage();
                return null;
            }
        }
        else
        {
            ContractApprovalInfo info = ContractApproval.GetModelByInstId(id);
            if (info == null)
            {
                ExceptionHander.GoToErrorPage();
                return null;
            }
            erpFormId = info.ErpFormId;
            erpFormType = info.ErpFormType;
        }

        Dictionary<string, string> pageNames =IFrameHelper.GetUrlDatas();

        if (!pageNames.Keys.Contains(erpFormType))
        {
            ExceptionHander.GoToErrorPage();
            return null;
        }
        string pageName = pageNames[erpFormType];
        return string.Format("{0}{1}", pageName, erpFormId);
    }

    //private static Dictionary<string, string> GetUrlDatas()
    //{
    //    string cacheName = "UrlDatas";
    //    Object ret = HttpContext.Current.Cache.Get(cacheName);
    //    if (ret == null)
    //    {
    //        Dictionary<string, string> pageNames = new Dictionary<string, string>();
    //        string path = HttpContext.Current.Server.MapPath("~/App_Data/Biz/ERP/urlData.xml");
    //        if (!File.Exists(path))
    //        {
    //            ExceptionHander.GoToErrorPage();
    //        }

    //        XmlDocument doc = new XmlDocument();
    //        doc.Load(path);
    //        XmlNodeList list = doc.SelectNodes("UrlDatas/UrlData");
    //        foreach (XmlNode item in list)
    //        {
    //            pageNames.Add(item.Attributes["type"].Value, item.InnerText);
    //        }
    //        HttpContext.Current.Cache.Insert(cacheName, pageNames, new System.Web.Caching.CacheDependency(path));
    //        return pageNames;
    //    }

    //    return ret as Dictionary<string, string>;
    //}

    public static string GetErpFormTitle(UPageBase page)
    {
        string formTitle = HttpContext.Current.Request["formTitle"];
        string id = HttpContext.Current.Request["Id"];

        if (string.IsNullOrEmpty(id))
        {//新建页面
            if (string.IsNullOrEmpty(formTitle))
            {
                return "ERP合同审批";
            }
            return formTitle;
        }
        else
        {
            return page._BPMContext.ProcInst.ProcName;
        }
    }
}