using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using Pkurg.PWorldBPM.FinallyDisposeServices;

namespace Pkurg.PWorldBPM.FinallyDispose
{
    public class DynamicWebService
    {
        public DynamicWebService(string url)
        {
            this.Url = url;
        }
        public void DoServiceEvent(int k2_workflowId, SerializableDictionary<string, string> dataFields)
        {
            StringBuilder txt = new StringBuilder();
            foreach (KeyValuePair<string,string> item in dataFields)
            {
                txt.AppendFormat("{0}|{1}$", item.Key, item.Value);
            }

            string param=txt.ToString().Trim('$');

            //1. 创建WebClient下载WSDL信息
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(string.Format("{0}?wsdl", Url));

            // 2. 创建和格式化 WSDL 文档。
            ServiceDescription description = ServiceDescription.Read(stream);

            // 3. 创建客户端代理代理类。
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap"; // 指定访问协议。
            importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。

            importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。

            // 4. 使用 CodeDom 编译客户端代理类。
            CodeNamespace nmspace = new CodeNamespace(); // 为代理类添加命名空间，缺省为全局空间。
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);

            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);

            //5. 修改WebService接口的基类（默认基类是SoapHttpClientProtocol），而因为使用的wse2.0，所以需要修改基类以便后面传递身份验证信息
            //CodeTypeDeclaration ctDecl = nmspace.Types[0];
            //nmspace.Types.Remove(ctDecl);
            //ctDecl.BaseTypes[0] = new CodeTypeReference("Microsoft.Web.Services2.WebServicesClientProtocol");
            //nmspace.Types.Add(ctDecl);

            //创建代码生成器
            CodeDomProvider provider = new Microsoft.CSharp.CSharpCodeProvider();

            //6. 指定代码生成器，并获得源码
            ICodeGenerator icg = provider.CreateGenerator();
            StringBuilder srcStringBuilder = new StringBuilder();
            StringWriter sw = new StringWriter(srcStringBuilder);
            icg.GenerateCodeFromNamespace(nmspace, sw, null);
            string proxySource = srcStringBuilder.ToString();
            sw.Close();

            //7. 创建编译的参数
            CompilerParameters parameter = new CompilerParameters();
            //注意以下两个属性设置为false才能在多次动态调用时不会报错
            parameter.GenerateExecutable = false;
            parameter.GenerateInMemory = false;
            //用于输出dll文件，调试的时候查看结果
            //parameter.OutputAssembly = "WebService.dll"; // 可以指定你所需的任何文件名。
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");


            //8. 动态编译文件
            ICodeCompiler compiler = provider.CreateCompiler();
            CompilerResults result = compiler.CompileAssemblyFromSource(parameter, proxySource);//compiler.CompileAssemblyFromDom(parameter, unit);			

            try
            {
                // 9. 检查编译是否出错
                if (!result.Errors.HasErrors)
                {
                    //10. 使用 Reflection 调用 WebService。
                    Assembly asm = result.CompiledAssembly;
                    // 如果在前面为代理类添加了命名空间，此处需要将命名空间添加到类型前面。

                    //foreach (var item in asm.GetTypes())
                    //{
                        //if (item.IsSubclassOf(typeof(FinallyDisposeServiceBase)))
                        //{
                    string className = WF_GetRelatedLinks.GetRelatedClassName(k2_workflowId.ToString());
                    Type t = asm.GetType(className);
                            object o = Activator.CreateInstance(t);
                            //调用WebService的方法
                            MethodInfo method = t.GetMethod("InvokeService");
                            //传递方法所需参数
                            method.Invoke(o, new object[] { k2_workflowId, param });
                            //获取返回结果;
                    //    }
                    //}

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }

        }

        public string Url { get; set; }
    }
}
