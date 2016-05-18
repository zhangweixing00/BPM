using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Net;
using System.Web.Services.Description;
using Microsoft.CSharp;

/// <summary>
///动态调用Web Service
/// </summary>
public class Dynamic
{
    #region Private

    /// <summary>
    /// 动态转换类型
    /// </summary>
    /// <param name="value">原始值</param>
    /// <param name="type">转换类型</param>
    /// <returns></returns>
    object ConvertObject(string value, string type)
    {
        object val = new object();
        type = type.ToLower();
        switch (type)
        {
            case "int":
                val = Convert.ToInt32(value);
                break;
            case "datetime":
                val = Convert.ToDateTime(value);
                break;
            case "double":
                val = Convert.ToDouble(value);
                break;
            case "string":
                val = Convert.ToString(value);
                break;
            case "boolean":
                val = Convert.ToBoolean(value);
                break;
            default:
                val = Convert.ToString(value);
                break;
        }
        return val;
    }

    //动态调用web服务
    public static object InvokeWebService(string url, string methodname, object[] args)
    {
        return InvokeWebService(url, null, methodname, args);
    }

    public static object InvokeWebService(string url, string classname, string methodname, object[] args)
    {
        string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
        if ((classname == null) || (classname == ""))
        {
            classname = GetWsClassName(url);
        }

        try
        {
            //获取WSDL
            WebClient wc = new WebClient();
            if (!url.Contains("?"))
            {
                url = url + "?wsdl";
            }

            Stream stream = wc.OpenRead(url);
            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);

            //生成客户端代理类代码
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider csc = new CSharpCodeProvider();
            ICodeCompiler icc = csc.CreateCompiler();

            //设定编译参数
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");

            //编译代理类
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }

            //生成代理实例，并调用方法
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            System.Reflection.MethodInfo mi = t.GetMethod(methodname);

            return mi.Invoke(obj, args);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
        }
    }

    private static string GetWsClassName(string wsUrl)
    {
        string[] parts = wsUrl.Split('/');
        string[] pps = parts[parts.Length - 1].Split('.');

        string str = pps[0];
        if (str.Contains("?"))
        {
            str = str.Substring(0, str.LastIndexOf("?"));
        }
        return str;
    }

    #endregion
}