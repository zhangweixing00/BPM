using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using Pkurg.PWorldBPM.Business.Workflow;

/// <summary>
///针对ERP嵌入页面进行处理
/// </summary>
public class IFrameHelper
{
    /// <summary>
    /// 加载页面时调用
    /// </summary>
    /// <returns></returns>
    public static string GetErpUrl()
    {
        string erpId = HttpContext.Current.Request["erpFormId"];
        string erpType = HttpContext.Current.Request["erpFormType"];

        string id = HttpContext.Current.Request["Id"];
        if (string.IsNullOrEmpty(id))
        {
            //新建页面
            if (string.IsNullOrEmpty(erpId) || string.IsNullOrEmpty(erpType))
            {
                //参数错误
                ExceptionHander.GoToErrorPage("IFrameHelper 参数错误");
                return null;
            }
            else
            {
                string erpUrl = GetERPPageUrl(erpId, erpType);
                return erpUrl;
            }
        }
        else
        {
            string fileName = string.Format("ERP_{0}.html", id);
            string filePath = Path.Combine("/Files/ERP/", fileName);

            //查看页面或者审批页面，第一次生成文件
            if (!File.Exists(HttpContext.Current.Server.MapPath("~" + filePath)))
            {
                try
                {
                    IFrameHelper.DownloadLocalFileUrl(id);
                }
                catch
                {

                }
            }

            if (!File.Exists(HttpContext.Current.Server.MapPath("~" + filePath)))
            {
                return GetERPPageUrlByInstId(id);
            }
            return filePath;
        }
    }

    private static string GetERPPageUrlByInstId(string id)
    {
        WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
        Pkurg.BPM.Entities.WorkFlowInstance inst = wf_WorkFlowInstance.GetWorkFlowInstanceById(id);
        if (inst == null)
        {
            throw new ArgumentException("id参数不正确");
        }

        string erpFormId = "";
        string erpFormType = "";

        switch (inst.AppId)
        {
            //补充协议
            case "2004":
                var supplementalAgreementinfo = Pkurg.PWorldBPM.Business.BIZ.ERP.SupplementalAgreement.GetModelByInstId(id);
                if (supplementalAgreementinfo == null)
                {
                    ExceptionHander.GoToErrorPage("IFrameHelper GetERPPageUrlByInstId");
                    return null;
                }
                erpFormId = supplementalAgreementinfo.ErpFormId;
                erpFormType = supplementalAgreementinfo.ErpFormType;
                break;

            //ERP付款申请单
            case "10105":
                var paymentApplication = Pkurg.PWorldBPM.Business.BIZ.ERP.PaymentApplication.GetPaymentApplicationInfoByInstanceId(id);
                if (paymentApplication == null)
                {
                    ExceptionHander.GoToErrorPage("IFrameHelper GetERPPageUrlByInstId");
                    return null;
                }
                erpFormId = paymentApplication.ErpFormId;
                erpFormType = paymentApplication.ErpFormType;
                break;
            //ERP请示单
            case "10107":
                var instruction = Pkurg.PWorldBPM.Business.BIZ.ERP.Instruction.GetInstructionInfoByInstanceId(id);
                if (instruction == null)
                {
                    ExceptionHander.GoToErrorPage("IFrameHelper GetERPPageUrlByInstId");
                    return null;
                }
                erpFormId = instruction.ErpFormId;
                erpFormType = instruction.ErpFormType;
                break;
            //合同审批
            case "10109":
                var info = Pkurg.PWorldBPM.Business.BIZ.ERP.ContractApproval.GetModelByInstId(id);
                if (info == null)
                {
                    ExceptionHander.GoToErrorPage("IFrameHelper GetERPPageUrlByInstId");
                    return null;
                }
                erpFormId = info.ErpFormId;
                erpFormType = info.ErpFormType;
                break;
                //合同结算
            case "10111":
                //通过instanceid得到formid，再得到实体
                var contractfinalaccountinfo =DBContext.GetBizContext().ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == inst.FormId); 
                if (contractfinalaccountinfo == null)
                {
                    ExceptionHander.GoToErrorPage("IFrameHelper GetERPPageUrlByInstId");
                    return null;
                }
                //相关参数
                erpFormId = contractfinalaccountinfo.ErpFormId;
                erpFormType = contractfinalaccountinfo.ErpFormType;
                break;
                //
            case "3027":
                //通过instanceid得到formid，再得到实体
                var contractbinfo =DBContext.GetBizContext().ERP_ContractPlanningBalance.FirstOrDefault(x => x.FormID == inst.FormId);
                if (contractbinfo == null)
                {
                    ExceptionHander.GoToErrorPage("IFrameHelper GetERPPageUrlByInstId");
                    return null;
                }
                //相关参数
                erpFormId = contractbinfo.ErpFormId;
                erpFormType = contractbinfo.ErpFormType;
                break;
            default:
                break;
        }
        return GetERPPageUrl(erpFormId, erpFormType);
    }

    /// <summary>
    /// 提交页面时调用
    /// </summary>
    /// <param name="instId"></param>
    /// <returns></returns>
    public static string DownloadLocalFileUrl(string instId)
    {
        string erpUrl = "";
        string erpId = HttpContext.Current.Request["erpFormId"];
        string erpType = HttpContext.Current.Request["erpFormType"];
        if (string.IsNullOrEmpty(erpId) || string.IsNullOrEmpty(erpType))
        {

            //参数错误
            erpUrl = GetERPPageUrlByInstId(instId);
        }
        else
            erpUrl = GetERPPageUrl(erpId, erpType);

        string fileName = string.Format("ERP_{0}.html", instId);
        string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/ERP"), fileName);
        if (!File.Exists(filePath))
        {
            WebClient wb = new WebClient();
            wb.DownloadFile(erpUrl, filePath);
        }

        return string.Format("/Files/{0}", fileName);
    }

    private static string GetERPPageUrl(string erpId, string erpType)
    {
        Dictionary<string, string> pageNames = IFrameHelper.GetUrlDatas();

        if (!pageNames.Keys.Contains(erpType))
        {
            ExceptionHander.GoToErrorPage("IFrameHelper GetERPPageUrl");
            //return null;
        }
        string pageName = pageNames[erpType];
        string erpUrl = string.Format("{0}{1}", pageName, erpId);
        return erpUrl;
    }

    public static Dictionary<string, string> GetUrlDatas()
    {
        string cacheName = "UrlDatas";
        Object ret = HttpContext.Current.Cache.Get(cacheName);
        if (ret == null)
        {
            Dictionary<string, string> pageNames = new Dictionary<string, string>();
            string path = HttpContext.Current.Server.MapPath("~/App_Data/Biz/ERP/urlData.xml");
            if (!File.Exists(path))
            {
                ExceptionHander.GoToErrorPage("IFrameHelper GetUrlDatas");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList list = doc.SelectNodes("UrlDatas/UrlData");
            foreach (XmlNode item in list)
            {
                pageNames.Add(item.Attributes["type"].Value, string.Format("{0}?{1}=", item.InnerText, item.Attributes["rParmam"].Value));
            }
            HttpContext.Current.Cache.Insert(cacheName, pageNames, new System.Web.Caching.CacheDependency(path));
            return pageNames;
        }

        return ret as Dictionary<string, string>;
    }
}