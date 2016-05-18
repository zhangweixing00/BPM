using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;

/// <summary>
///ERP通用调用服务
/// </summary>
public class InvokeService_ERP
{
    public static string InvokeService(CallbackInfo callbackInfo)
    {
        ///补全
        int needAddCount = 5 - callbackInfo.ArrayParam.Count;
        for (int index = 0; index < needAddCount; index++)
        {
            callbackInfo.ArrayParam.Add("");
        }

        string url = System.Configuration.ConfigurationManager.AppSettings["ERP通用接口地址"];
        StringBuilder soap = new StringBuilder();
        soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        soap.AppendFormat("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ws=\"{0}\" xmlns:ns1=\"http://xmlns.oracle.com/apps/cux/soaprovider/plsql/cux_bpm_apr_utl/cux_common_interface/\" >", url);
        soap.Append("<soapenv:Header>");
        soap.Append("<wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">");
        soap.Append("<wsse:UsernameToken>");
        soap.Append("<wsse:Username>ASADMIN</wsse:Username>");//用户名
        soap.Append("<wsse:Password Type=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText\">111111</wsse:Password>");//口令
        soap.Append("</wsse:UsernameToken>");
        soap.Append("</wsse:Security>");
        soap.Append("</soapenv:Header>");
        soap.Append("<soapenv:Body>");
        soap.AppendFormat(@"<ns1:InputParameters>
         <ns1:P_DOC_ID>{0}</ns1:P_DOC_ID>
            <ns1:P_DOC_TYPE>{1}</ns1:P_DOC_TYPE>
            <ns1:P_APPR_RESULT>{2}</ns1:P_APPR_RESULT>
            <ns1:P_ATTRIBUTE1>{3}</ns1:P_ATTRIBUTE1>
            <ns1:P_ATTRIBUTE2>{4}</ns1:P_ATTRIBUTE2>
            <ns1:P_ATTRIBUTE3>{5}</ns1:P_ATTRIBUTE3>
            <ns1:P_ATTRIBUTE4>{6}</ns1:P_ATTRIBUTE4>
            <ns1:P_ATTRIBUTE5>{7}</ns1:P_ATTRIBUTE5>
      </ns1:InputParameters>", callbackInfo.FormCode, callbackInfo.FormType, callbackInfo.Status,
                              callbackInfo.ArrayParam[0],
                              callbackInfo.ArrayParam[1],
                              callbackInfo.ArrayParam[2],
                              callbackInfo.ArrayParam[3],
                              callbackInfo.ArrayParam[4]
                              );
        soap.Append("</soapenv:Body>");
        soap.Append("</soapenv:Envelope>");

        string isSimulateERPService=System.Configuration.ConfigurationManager.AppSettings["SimulateERPService"];
        if (!string.IsNullOrEmpty(isSimulateERPService)&&isSimulateERPService=="1")
        {
            return SimulateERPService();
        }
        var result = GetSOAPReSource(url, soap.ToString());
        return result;
    }

    /// <summary>
    /// 由于ERP不能根据情况及时调试，特模拟ERP系统返回数据
    /// </summary>
    /// <returns></returns>
    private static string SimulateERPService()
    {
        string filePath = HttpContext.Current.Server.MapPath("~/App_Data/eData.xml");
        if (File.Exists(filePath))
        {
           return File.ReadAllText(filePath);
        }
        return "";
    }

    public static ERP_CallbackResultInfo InvokeServiceAdvance(CallbackInfo callbackInfo)
    {
        string xml = "";
        try
        {
            xml = InvokeService(callbackInfo);
            Logger.logger.DebugFormat("BackXml:{0}", xml);
        }
        catch (Exception ex)
        {
            return new ERP_CallbackResultInfo()
             {
                 Log = ex.Message,
                 ResultType = ERP_CallbackResultType.ERP服务器异常,
                 resultXml = ex.StackTrace
             };
        }

        return ResultProcess(xml);
    }

    private static string GetSOAPReSource(string url, string datastr)
    {
        string result = "";
        Uri uri = new Uri(url);
        WebRequest webRequest = WebRequest.Create(uri);
        webRequest.ContentType = "text/xml; charset=utf-8";
        webRequest.Method = "POST";
        webRequest.Timeout = 3*60*1000;//按业务需要设置超时时间
        using (Stream requestStream = webRequest.GetRequestStream())
        {
            byte[] paramBytes = Encoding.UTF8.GetBytes(datastr.ToString());
            requestStream.Write(paramBytes, 0, paramBytes.Length);
        }
        //response
        WebResponse webResponse = webRequest.GetResponse();
        using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
        {
            result = myStreamReader.ReadToEnd();
        }
        return result;
    }

    public static ERP_CallbackResultInfo ResultProcess(string xml)
    {
        try
        {
            //xml = xml.Replace("env:", "");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNamespaceManager nsp = new XmlNamespaceManager(doc.NameTable);
            nsp.AddNamespace("env", "http://schemas.xmlsoap.org/soap/envelope/");
            nsp.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nsp.AddNamespace("cc", "http://xmlns.oracle.com/apps/cux/soaprovider/plsql/cux_bpm_apr_utl/cux_common_interface/");

            XmlNode node = doc.SelectSingleNode("env:Envelope/env:Body/cc:OutputParameters/cc:P_ERROR_MESSAGE", nsp);

            //XmlNode node = doc.SelectSingleNode("Envelope/Body/OutputParameters/P_ERROR_MESSAGE");

            if (node == null)
            {
                return new ERP_CallbackResultInfo()
                    {
                        Log = "没有找到P_ERROR_MESSAGE节点",
                        ResultType = ERP_CallbackResultType.返回数据异常,
                        resultXml = xml
                    };
            }
            string errorMessage = node.InnerText.ToLower();

            if (errorMessage.StartsWith("false"))
            {
                return new ERP_CallbackResultInfo()
                {
                    Log = errorMessage.Replace("false:", ""),
                    ResultType = ERP_CallbackResultType.返回数据异常,
                    resultXml = xml
                };
            }

            if (string.IsNullOrEmpty(errorMessage)||errorMessage=="true")
            {
                return new ERP_CallbackResultInfo()
                {
                    Log = "ok",
                    ResultType = ERP_CallbackResultType.调用成功,
                    resultXml = xml
                };
            }

            //if (node.InnerText == "#TIMEOUT")
            //{
            //    return new ERP_CallbackResultInfo()
            //    {
            //        Log = "超时",
            //        ResultType = ERP_CallbackResultType.超时,
            //        resultXml = xml
            //    };
            //}

            //if (node.Attributes["xsi:nil"].Value == "true")
            //{

            //}


        }
        catch (Exception ex)
        {
            return new ERP_CallbackResultInfo()
            {
                Log = ex.Message,
                ResultType = ERP_CallbackResultType.返回数据异常,
                resultXml = xml
            };
        }
        return new ERP_CallbackResultInfo()
         {
             Log = "返回数据异常",
             ResultType = ERP_CallbackResultType.返回数据异常,
             resultXml = xml
         };
    }
}

public enum ERP_CallbackResultType
{
    调用成功,
    超时,
    返回数据异常,
    ERP服务器异常
}
/// <summary>
/// 调用ERP返回的数据实体
/// </summary>
public class ERP_CallbackResultInfo
{
    public ERP_CallbackResultType ResultType { get; set; }
    public string Log { get; set; }
    public string resultXml { get; set; }
}