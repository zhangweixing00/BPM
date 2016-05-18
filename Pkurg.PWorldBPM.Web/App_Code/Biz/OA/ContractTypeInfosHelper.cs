using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;

/// <summary>
///TypeInfosHelper 的摘要说明
/// </summary>
public class ContractTypeInfosHelper
{
    public ContractTypeInfosHelper()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    //public static List<ContractTypeInfo> GetFirstContractTypeInfos()
    //{

    //        return GetFirstContractTypeInfos();

    //}

    public static List<ContractTypeInfo> GetSecondContractTypeInfos(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            XmlDocument doc = GetContractTypeDoc();
            XmlNode node = doc.SelectSingleNode("Root/FirstTypeInfo[@key=" + key + "]");
            if (node != null)
            {
                XmlNodeList list = node.ChildNodes;
                return ConvertNodesToList(list);
            }

        }
        return new List<ContractTypeInfo>();
    }

    public static List<ContractTypeInfo> GetThirdContractTypeInfos(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            XmlDocument doc = GetContractTypeDoc();
            XmlNode node = doc.SelectSingleNode("Root/FirstTypeInfo/SecondTypeInfo[@key=" + key + "]");
            if (node != null)
            {
                XmlNodeList list = node.ChildNodes;
                return ConvertNodesToList(list);
            }

        }
        return new List<ContractTypeInfo>();
    }

    public static List<ContractTypeInfo> GetFirstContractTypeInfos()
    {
        string cacheName_FirstContractTypeInfos = "cacheName_FirstContractTypeInfos";
        if (HttpContext.Current.Cache[cacheName_FirstContractTypeInfos] == null)
        {
            XmlDocument doc = GetContractTypeDoc();
            XmlNodeList list = doc.SelectNodes("Root/FirstTypeInfo");
            return ConvertNodesToList(list);
        }
        else
        {
            return HttpContext.Current.Cache[cacheName_FirstContractTypeInfos] as List<ContractTypeInfo>;
        }

    }

    private static List<ContractTypeInfo> ConvertNodesToList(XmlNodeList list)
    {
        List<ContractTypeInfo> infos = new List<ContractTypeInfo>();
        foreach (XmlNode item in list)
        {
            infos.Add(new ContractTypeInfo()
            {
                Key = item.Attributes["key"].Value,
                Value = item.Attributes["value"].Value
            });
        }
        return infos;
    }

    private static XmlDocument GetContractTypeDoc()
    {
        string cacheName_ContractTypeDoc = "cacheName_ContractTypeDoc";
        if (HttpContext.Current.Cache[cacheName_ContractTypeDoc] == null)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Files/xml/ContractType.xml");
            if (!File.Exists(filePath))
            {
                throw new Exception("不存在ContractType文件");
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                HttpContext.Current.Cache.Insert(cacheName_ContractTypeDoc, doc, new System.Web.Caching.CacheDependency(filePath));
                return doc;
            }
            catch (Exception ex)
            {
                throw new Exception("xml文件不正确");
            }

        }
        else
        {
            return HttpContext.Current.Cache[cacheName_ContractTypeDoc] as XmlDocument;
        }
    }

    public static object GetFirstContractTypeInfos1()
    {
        string cacheName_FirstContractTypeInfos = "cacheName_FirstContractTypeInfos";
        if (HttpContext.Current.Cache[cacheName_FirstContractTypeInfos] == null)
        {
            XmlDocument doc = GetContractTypeDoc1();
            XmlNodeList list = doc.SelectNodes("Root/FirstTypeInfo");
            return ConvertNodesToList(list);
        }
        else
        {
            return HttpContext.Current.Cache[cacheName_FirstContractTypeInfos] as List<ContractTypeInfo>;
        }
    }

    private static XmlDocument GetContractTypeDoc1()
    {
        string cacheName_ContractTypeDoc = "cacheName_ContractTypeDoc";
        if (HttpContext.Current.Cache[cacheName_ContractTypeDoc] == null)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Files/xml/WYXML/WY_ContractType.xml");
            if (!File.Exists(filePath))
            {
                throw new Exception("不存在ContractType文件");
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                HttpContext.Current.Cache.Insert(cacheName_ContractTypeDoc, doc, new System.Web.Caching.CacheDependency(filePath));
                return doc;
            }
            catch (Exception ex)
            {
                throw new Exception("xml文件不正确");
            }

        }
        else
        {
            return HttpContext.Current.Cache[cacheName_ContractTypeDoc] as XmlDocument;
        }
    }

    public static object GetSecondContractTypeInfos1(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            XmlDocument doc = GetContractTypeDoc1();
            XmlNode node = doc.SelectSingleNode("Root/FirstTypeInfo[@key=" + key + "]");
            if (node != null)
            {
                XmlNodeList list = node.ChildNodes;
                return ConvertNodesToList(list);
            }

        }
        return new List<ContractTypeInfo>();
    }

    public static object GetThirdContractTypeInfos1(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            XmlDocument doc = GetContractTypeDoc1();
            XmlNode node = doc.SelectSingleNode("Root/FirstTypeInfo/SecondTypeInfo[@key=" + key + "]");
            if (node != null)
            {
                XmlNodeList list = node.ChildNodes;
                return ConvertNodesToList(list);
            }

        }
        return new List<ContractTypeInfo>();
    }
}