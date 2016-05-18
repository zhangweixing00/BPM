using System;

/// <summary>
///发送邮件服务
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name = "ServiceSoap", Namespace = "http://tempuri.org/")]
public class EmailHelper : System.Web.Services.Protocols.SoapHttpClientProtocol
{
     EmailHelper(string serviceUrl)
    {
        if (string.IsNullOrEmpty(serviceUrl))
        {
            throw new ArgumentNullException();
        }
        else
        {
            this.Url = serviceUrl;
        }
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="toUsers"></param>
    /// <returns></returns>
    public static string SendEmailToUsers(string title, string content, string toUsers)
    {
        string url = "http://172.25.56.57:8888/Service.asmx";
        return new EmailHelper(url).SendEmail(title, content, toUsers);
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendEmail", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    string SendEmail(string title, string content, string toUsers)
    {
        try
        {
            object[] results = this.Invoke("SendEmail", new object[] {
                    title,
                    content,
                    toUsers});
            return ((string)(results[0]));
        }
        catch (Exception ex)
        {
            LoggerR.logger.DebugFormat("发送邮件失败：{0}\r\n{1}", ex.Message, ex.StackTrace);
            return "发送邮件失败";
        }
    }
}