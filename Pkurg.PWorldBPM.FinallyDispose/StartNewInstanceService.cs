﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.4927
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.3038.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name = "CreateNewFormServiceSoap", Namespace = "http://ht.zz.vc/")]
public partial class CreateNewFormService : System.Web.Services.Protocols.SoapHttpClientProtocol
{

    private System.Threading.SendOrPostCallback CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureOperationCompleted;

    /// <remarks/>
    public CreateNewFormService()
    {
        this.Url = "http://172.25.20.47:1234/CreateNewFormService.asmx";
    }

    /// <remarks/>
    public event CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureCompletedEventHandler CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureCompleted;

    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ht.zz.vc/CreateNewFormByInstanceIDAndLoginNameWithStoredProcedure", RequestNamespace = "http://ht.zz.vc/", ResponseNamespace = "http://ht.zz.vc/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public bool CreateNewFormByInstanceIDAndLoginNameWithStoredProcedure(string instanceID, string loginName, string sp)
    {
        object[] results = this.Invoke("CreateNewFormByInstanceIDAndLoginNameWithStoredProcedure", new object[] {
                    instanceID,
                    loginName,
                    sp});
        return ((bool)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BeginCreateNewFormByInstanceIDAndLoginNameWithStoredProcedure(string instanceID, string loginName, string sp, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("CreateNewFormByInstanceIDAndLoginNameWithStoredProcedure", new object[] {
                    instanceID,
                    loginName,
                    sp}, callback, asyncState);
    }

    /// <remarks/>
    public bool EndCreateNewFormByInstanceIDAndLoginNameWithStoredProcedure(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((bool)(results[0]));
    }

    /// <remarks/>
    public void CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureAsync(string instanceID, string loginName, string sp)
    {
        this.CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureAsync(instanceID, loginName, sp, null);
    }

    /// <remarks/>
    public void CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureAsync(string instanceID, string loginName, string sp, object userState)
    {
        if ((this.CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureOperationCompleted == null))
        {
            this.CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateNewFormByInstanceIDAndLoginNameWithStoredProcedureOperationCompleted);
        }
        this.InvokeAsync("CreateNewFormByInstanceIDAndLoginNameWithStoredProcedure", new object[] {
                    instanceID,
                    loginName,
                    sp}, this.CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureOperationCompleted, userState);
    }

    private void OnCreateNewFormByInstanceIDAndLoginNameWithStoredProcedureOperationCompleted(object arg)
    {
        if ((this.CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureCompleted(this, new CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    public new void CancelAsync(object userState)
    {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureCompletedEventHandler(object sender, CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{

    private object[] results;

    internal CreateNewFormByInstanceIDAndLoginNameWithStoredProcedureCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
        base(exception, cancelled, userState)
    {
        this.results = results;
    }

    /// <remarks/>
    public bool Result
    {
        get
        {
            this.RaiseExceptionIfNecessary();
            return ((bool)(this.results[0]));
        }
    }
}