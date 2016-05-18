using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;

/// <summary>
///GroupContractSubjectHelper 的摘要说明
/// </summary>
public class GroupContractSubjectHelper
{
    public static List<ContractTypeInfo> GetContractSubjectInfos()
    {
        string cacheName_ContractSubjectInfos = "cacheName_ContractSubjectInfos";
        if (HttpContext.Current.Cache[cacheName_ContractSubjectInfos] == null)
        {
            XmlDocument doc = new XmlDocument();
            string filePath = HttpContext.Current.Server.MapPath("~/Files/xml/ContractSubject.xml");
            if (!File.Exists(filePath))
            {
                throw new Exception("不存在ContractSubject文件");
            }
            try
            {
                doc.Load(filePath);
                XmlNodeList list = doc.SelectNodes("Root/Columns");
                List<ContractTypeInfo> infos = new List<ContractTypeInfo>();
                foreach (XmlNode item in list)
                {
                    infos.Add(new ContractTypeInfo()
                    {
                        Key = item.ChildNodes[0].InnerXml,
                        Value = item.ChildNodes[1].InnerXml
                    });
                }
                HttpContext.Current.Cache.Insert(cacheName_ContractSubjectInfos, infos, new System.Web.Caching.CacheDependency(filePath));
                return infos;
            }
            catch
            {
                throw new Exception("ContractSubject文件不正确");
            }
        }
        else
        {
            return HttpContext.Current.Cache[cacheName_ContractSubjectInfos] as List<ContractTypeInfo>;
        }

    }
}