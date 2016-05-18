using System;
using Pkurg.PWorldBPM.Business.Controls;

/// <summary>
///CommonService 的摘要说明
/// </summary>
namespace AppCode.ERP
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ERP_CommonSoap", Namespace = "http://tempuri.org/")]
    public class CommonService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        public CommonService()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public CommonService(int appId)
        {
            string url = WF_GetRelatedLinks.GetRelatedLinkByAppID(appId.ToString());
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("没有配置流程与erp服务接口地址");
            }
            this.Url = url;
        }

        public CommonService(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("没有配置流程与erp服务接口地址");
            }
            this.Url = url;
        }
        /// <summary>
        /// 根据流程ERPID通知终止流程
        /// </summary>
        /// <param name="erpFormCode"></param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NotifyStopByERPCode", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ERP_CallbackResultType NotifyStopByERPCode(string erpFormCode)
        {
            try
            {
                object[] results = this.Invoke("NotifyStopByERPCode", new object[] {
                    erpFormCode});
                return ((ERP_CallbackResultType)(results[0]));
            }
            catch (Exception)
            {
                return ERP_CallbackResultType.ERP服务器异常;
            }

        }

        /// <summary>
        /// 二次验证
        /// </summary>
        /// <param name="erpFormCode"></param>
        /// <param name="isSubmit"></param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NotifyStartAdvance", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ERP_CallbackResultType NotifyStartAdvance(string erpFormCode, bool isSubmit)
        {
            try
            {
                object[] results = this.Invoke("NotifyStartAdvance", new object[] {
                    erpFormCode,
                    isSubmit});
                return ((ERP_CallbackResultType)(results[0]));
            }
            catch (Exception)
            {
                return ERP_CallbackResultType.ERP服务器异常;
            }

        }

        /// <summary>
        /// 结束回调
        /// </summary>
        /// <param name="k2_workflowId"></param>
        /// <param name="XmlData"></param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InvokeService", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ExecuteResultInfo InvokeService(int k2_workflowId, string XmlData)
        {
            object[] results = this.Invoke("InvokeService", new object[] {
                    k2_workflowId,
                    XmlData});
            return ((ExecuteResultInfo)(results[0]));
        }

        /// <summary>
        /// 根据流程实例ID通知终止流程
        /// </summary>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NotifyStop", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ERP_CallbackResultType NotifyStop(string instanceID)
        {
            object[] results = this.Invoke("NotifyStop", new object[] {
                    instanceID});
            return ((ERP_CallbackResultType)(results[0]));
        }

    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [Serializable()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public enum ERP_CallbackResultType
    {

        /// <remarks/>
        调用成功,

        /// <remarks/>
        超时,

        /// <remarks/>
        返回数据异常,

        /// <remarks/>
        ERP服务器异常,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class ExecuteResultInfo
    {

        private string execExceptionField;

        private bool isSuccessField;

        /// <remarks/>
        public string ExecException
        {
            get
            {
                return this.execExceptionField;
            }
            set
            {
                this.execExceptionField = value;
            }
        }

        /// <remarks/>
        public bool IsSuccess
        {
            get
            {
                return this.isSuccessField;
            }
            set
            {
                this.isSuccessField = value;
            }
        }
    }

}