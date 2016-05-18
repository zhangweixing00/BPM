using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Utility
{
    public class XmlHelper
    {
        #region private AppendChild
        private static void AppendChild(XmlDocument xDoc, XmlNode parentNode, params XmlParameter[] paras)
        {
            foreach (XmlParameter xpar in paras)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xpar.Name, null);
                string ns = xpar.NamespaceOfPrefix == null ? "" : newNode.GetNamespaceOfPrefix(xpar.NamespaceOfPrefix);
                foreach (AttributeParameter attp in xpar.Attributes)
                {
                    XmlNode attr = xDoc.CreateNode(XmlNodeType.Attribute, attp.Name, ns == "" ? null : ns);
                    attr.Value = attp.Value;
                    newNode.Attributes.SetNamedItem(attr);
                }
                newNode.InnerText = xpar.InnerText;
                parentNode.AppendChild(newNode);
            }
        }
        #endregion

        #region private AddEveryNode
        private static void AddEveryNode(XmlDocument xDoc, XmlNode parentNode, params XmlParameter[] paras)
        {
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)
            {
                if (xns.Name == parentNode.Name)
                {
                    AppendChild(xDoc, xns, paras);
                }
                else
                {
                    foreach (XmlNode xn in xns)
                    {
                        if (xn.Name == parentNode.Name)
                        {
                            AppendChild(xDoc, xn, paras);
                        }
                    }
                }
            }
        }
        #endregion

        #region xmlDoc
        /// <summary>
        /// 创建一个XmlDocument对象
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        public static XmlDocument xmlDoc(string fileFullName)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(fileFullName);
                return xDoc;
            }
            catch
            {
                try
                {
                    xDoc.LoadXml(fileFullName);
                    return xDoc;
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion

        #region CreateXML

        /// <summary>
        /// 创建一个XML
        /// </summary>
        /// <param name="rootName">根结点名称</param>
        /// <param name="elemName">元素节点名称</param>
        /// <param name="paras">XML参数</param>
        public static XmlDocument CreateXML(string rootName, string elemName, params XmlParameter[] paras)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn;
            xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootName);
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elemName, null);
            AppendChild(xDoc, ln, paras);
            root.AppendChild(ln);

            return xDoc;
        }

        #endregion

        #region CreateXMLFile
        /// <summary>
        /// 创建一个XML文档
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="rootName">根结点名称</param>
        /// <param name="elemName">元素节点名称</param>
        /// <param name="paras">XML参数</param>
        public static void CreateXMLFile(string fileFullName, string rootName, string elemName, params XmlParameter[] paras)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn;
            xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootName);
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elemName, null);
            AppendChild(xDoc, ln, paras);
            root.AppendChild(ln);
            try
            {
                xDoc.Save(fileFullName);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 创建一个XML文档
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="rootName">根结点名称</param>
        /// <param name="elemp">元素节点对象</param>
        /// <param name="paras">XML参数</param>
        public static void CreateXMLFile(string fileFullName, string rootName, XmlParameter elemp, params XmlParameter[] paras)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn;
            xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootName);
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elemp.Name, null);
            string ns = elemp.NamespaceOfPrefix == null ? "" : ln.GetNamespaceOfPrefix(elemp.NamespaceOfPrefix);
            foreach (AttributeParameter ap in elemp.Attributes)
            {
                XmlNode elemAtt = xDoc.CreateNode(XmlNodeType.Attribute, ap.Name, ns == "" ? null : ns);
                elemAtt.Value = ap.Value;
                ln.Attributes.SetNamedItem(elemAtt);
            }
            AppendChild(xDoc, ln, paras);
            root.AppendChild(ln);
            try
            {
                xDoc.Save(fileFullName);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 创建一个XML文档
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="rootp">根结点对象</param>
        /// <param name="elemName">元素节点名称</param>
        /// <param name="paras">XML参数</param>
        public static void CreateXMLFile(string fileFullName, XmlParameter rootp, string elemName, params XmlParameter[] paras)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn;
            xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootp.Name);
            string ns = rootp.NamespaceOfPrefix == null ? "" : root.GetNamespaceOfPrefix(rootp.NamespaceOfPrefix);
            foreach (AttributeParameter ap in rootp.Attributes)
            {
                XmlNode rootAtt = xDoc.CreateNode(XmlNodeType.Attribute, ap.Name, ns == "" ? null : ns);
                rootAtt.Value = ap.Value;
                root.Attributes.SetNamedItem(rootAtt);
            }
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elemName, null);
            AppendChild(xDoc, ln, paras);
            root.AppendChild(ln);
            try
            {
                xDoc.Save(fileFullName);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 创建一个XML文档
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="rootp">根结点对象</param>
        /// <param name="elemp">元素节点对象</param>
        /// <param name="paras">XML参数</param>
        public static void CreateXMLFile(string fileFullName, XmlParameter rootp, XmlParameter elemp, params XmlParameter[] paras)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn;
            xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootp.Name);
            string ns = rootp.NamespaceOfPrefix == null ? "" : root.GetNamespaceOfPrefix(rootp.NamespaceOfPrefix);
            foreach (AttributeParameter ap in rootp.Attributes)
            {
                XmlNode rootAtt = xDoc.CreateNode(XmlNodeType.Attribute, ap.Name, ns == "" ? null : ns);
                rootAtt.Value = ap.Value;
                root.Attributes.SetNamedItem(rootAtt);
            }
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elemp.Name, null);
            ns = elemp.NamespaceOfPrefix == null ? "" : ln.GetNamespaceOfPrefix(elemp.NamespaceOfPrefix);
            foreach (AttributeParameter ap in elemp.Attributes)
            {
                XmlNode elemAtt = xDoc.CreateNode(XmlNodeType.Attribute, ap.Name, ns == "" ? null : ns);
                elemAtt.Value = ap.Value;
                ln.Attributes.SetNamedItem(elemAtt);
            }
            AppendChild(xDoc, ln, paras);
            root.AppendChild(ln);
            try
            {
                xDoc.Save(fileFullName);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region AddNewNode
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="parentNode">新节点的父节点对象</param>
        /// <param name="paras">XML参数对象</param>
        public static bool AddNewNode(string fileFullName, XmlNode parentNode, params XmlParameter[] paras)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xDoc.DocumentElement.ChildNodes[0].Name, null);
                AppendChild(xDoc, newNode, paras);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, paras);
            }
            xDoc.Save(fileFullName);
            return true;
        }
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="parentNode">新节点的父节点对象</param>
        /// <param name="paras">XML参数对象</param>
        public static bool AddNewNode(XmlDocument xDoc, XmlNode parentNode, params XmlParameter[] paras)
        {
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xDoc.DocumentElement.ChildNodes[0].Name, null);
                AppendChild(xDoc, newNode, paras);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, paras);
            }
            return true;
        }
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="parentName">新节点的父节点名称</param>
        /// <param name="paras">XML参数对象</param>
        public static bool AddNewNode(XmlDocument xDoc, string parentName, params XmlParameter[] paras)
        {
            XmlNode parentNode = GetNode(xDoc, parentName);
            if (parentNode == null) return false;
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, "Node", null);
                AppendChild(xDoc, newNode, paras);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, paras);
            }
            return true;
        }
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="parentName">新节点的父节点名称</param>
        /// <param name="paras">XML参数对象</param>
        public static bool AddNewNode(string fileFullName, string parentName, params XmlParameter[] paras)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            XmlNode parentNode = GetNode(xDoc, parentName);
            if (parentNode == null) return false;
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xDoc.DocumentElement.ChildNodes[0].Name, null);
                AppendChild(xDoc, newNode, paras);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, paras);
            }
            xDoc.Save(fileFullName);
            return true;
        }
        #endregion

        #region AddAttribute
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(XmlDocument xDoc, XmlNode node, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            string ns = namespaceOfPrefix == null ? null : node.GetNamespaceOfPrefix(namespaceOfPrefix);
            XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attributeName, ns == "" ? null : ns);
            xn.Value = attributeValue;
            node.Attributes.SetNamedItem(xn);
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attps">节点属性参数</param>
        public static void AddAttribute(XmlDocument xDoc, XmlNode node, string namespaceOfPrefix, params AttributeParameter[] attps)
        {
            string ns = namespaceOfPrefix == null ? null : node.GetNamespaceOfPrefix(namespaceOfPrefix);
            foreach (AttributeParameter attp in attps)
            {
                XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attp.Name, ns == "" ? null : ns);
                xn.Value = attp.Value;
                node.Attributes.SetNamedItem(xn);
            }
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(string fileFullName, XmlNode node, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            AddAttribute(xDoc, node, namespaceOfPrefix, attributeName, attributeValue);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attps">节点属性参数</param>
        public static void AddAttribute(string fileFullName, XmlNode node, string namespaceOfPrefix, params AttributeParameter[] attps)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            AddAttribute(xDoc, node, namespaceOfPrefix, attps);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(XmlDocument xDoc, string nodeName, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) return;
                string ns = namespaceOfPrefix == null ? null : node.GetNamespaceOfPrefix(namespaceOfPrefix);
                XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attributeName, ns == "" ? null : ns);
                xn.Value = attributeValue;
                node.Attributes.SetNamedItem(xn);
            }
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attps">节点属性参数</param>
        public static void AddAttribute(XmlDocument xDoc, string nodeName, string namespaceOfPrefix, params AttributeParameter[] attps)
        {
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) return;
                string ns = namespaceOfPrefix == null ? null : node.GetNamespaceOfPrefix(namespaceOfPrefix);
                foreach (AttributeParameter attp in attps)
                {
                    XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attp.Name, ns == "" ? null : ns);
                    xn.Value = attp.Value;
                    node.Attributes.SetNamedItem(xn);
                }
            }
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(string fileFullName, string nodeName, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) break;
                AddAttribute(xDoc, node, namespaceOfPrefix, attributeName, attributeValue);
            }
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attps">节点属性参数</param>
        public static void AddAttribute(string fileFullName, string nodeName, string namespaceOfPrefix, params AttributeParameter[] attps)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) break;
                AddAttribute(xDoc, node, namespaceOfPrefix, attps);
            }
            xDoc.Save(fileFullName);
        }
        #endregion

        #region GetNode
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNode GetNode(string fileFullName, string nodeName)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            if (xDoc.DocumentElement.Name == nodeName) return (XmlNode)xDoc.DocumentElement;
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
            {
                if (xns.Name == nodeName) return xns;
                else
                {
                    XmlNode xn = GetNode(xns, nodeName);
                    return xn;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlNode node, string nodeName)
        {
            foreach (XmlNode xn in node)
            {
                if (xn.Name == nodeName) return xn;
                else
                {
                    XmlNode tmp = GetNode(xn, nodeName);
                    if (tmp != null) return tmp;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlDocument xDoc, string nodeName)
        {
            if (xDoc.DocumentElement.Name == nodeName) return (XmlNode)xDoc.DocumentElement;
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
            {
                if (xns.Name == nodeName) return xns;
                return GetNode(xns, nodeName);
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="Index">节点索引</param>
        /// <param name="nodeName">节点名称</param>
        public static XmlNode GetNode(XmlDocument xDoc, int Index, string nodeName)
        {
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            if (nlst.Count <= Index) return null;
            if (nlst[Index].Name == nodeName) return (XmlNode)nlst[Index];
            foreach (XmlNode xn in nlst[Index])
            {
                return GetNode(xn, nodeName);
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="Index">节点索引</param>
        /// <param name="nodeName">节点名称</param>
        public static XmlNode GetNode(string fileFullName, int Index, string nodeName)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            if (nlst.Count <= Index) return null;
            if (nlst[Index].Name == nodeName) return (XmlNode)nlst[Index];
            foreach (XmlNode xn in nlst[Index])
            {
                return GetNode(xn, nodeName);
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="innerText">节点内容</param>
        public static XmlNode GetNode(XmlNode node, string nodeName, string innerText)
        {
            foreach (XmlNode xn in node)
            {
                if (xn.Name == nodeName && xn.InnerText == innerText) return xn;
                else
                {
                    XmlNode tmp = GetNode(xn, nodeName, innerText);
                    if (tmp != null) return tmp;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="innerText">节点内容</param>
        public static XmlNode GetNode(XmlDocument xDoc, string nodeName, string innerText)
        {
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
            {
                if (xns.Name == nodeName && xns.InnerText == innerText) return xns;
                XmlNode tmp = GetNode(xns, nodeName, innerText);
                if (tmp != null) return tmp;
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="xpar">XML参数</param>
        public static XmlNode GetNode(XmlDocument xDoc, XmlParameter xpar)
        {
            return GetNode(xDoc, xpar.Name, xpar.InnerText);
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="xpar">XML参数</param>
        public static XmlNode GetNode(XmlNode node, XmlParameter xpar)
        {
            return GetNode(node, xpar.Name, node.InnerText);
        }
        #endregion

        #region UpdateNode
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="node">修改的节点对象</param>
        /// <param name="para">XML参数对象</param>
        public static void UpdateNode(XmlNode node, XmlParameter para)
        {
            node.InnerText = para.InnerText;
            for (int i = 0; i < para.Attributes.Length; i++)
            {
                node.Attributes.Item(i).Value = para.Attributes[i].Value;
            }
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="node">父节点对象</param>
        /// <param name="childIndex">该节点的索引</param>
        /// <param name="nodeText">修改后的内容</param>
        public static void UpdateNode(XmlNode node, int childIndex, string nodeText)
        {
            node.ChildNodes[childIndex].InnerText = nodeText;
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="node">修改的节点对象</param>
        /// <param name="nodeText">修改后的内容</param>
        public static void UpdateNode(XmlNode node, string nodeText)
        {
            node.InnerText = nodeText;
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="xDoc">XMLDocument对象</param>
        /// <param name="para">XML参数对象</param>
        public static void UpdateNode(XmlDocument xDoc, int Index, XmlParameter para)
        {
            XmlNode node = GetNode(xDoc, Index, para.Name);
            UpdateNode(node, para);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="xDoc">XMLDocument对象</param>
        /// <param name="nodeName">父节点名称</param>
        /// <param name="childIndex">该节点的索引</param>
        /// <param name="nodeText">修改后的内容</param>
        /// <param name="nodeValue">修改后的值，如果没有，那么该值为null</param>
        public static void UpdateNode(XmlDocument xDoc, int Index, string nodeName, int childIndex, string nodeText)
        {
            XmlNode node = GetNode(xDoc, Index, nodeName);
            UpdateNode(node, childIndex, nodeText);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="xDoc">XMLDocument对象</param>
        /// <param name="nodeName">修改的节点名称</param>
        /// <param name="nodeText">修改后的内容</param>
        /// <param name="nodeValue">修改后的值，如果没有，那么该值为null</param>
        public static void UpdateNode(XmlDocument xDoc, int Index, string nodeName, string nodeText)
        {
            XmlNode node = GetNode(xDoc, Index, nodeName);
            UpdateNode(node, nodeText);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="para">XML参数对象</param>
        public static void UpdateNode(string fileFullName, int Index, XmlParameter para)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            UpdateNode(xDoc, Index, para);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">父节点名称</param>
        /// <param name="childIndex">该节点的索引</param>
        /// <param name="nodeText">修改后的内容</param>
        public static void UpdateNode(string fileFullName, int Index, string nodeName, int childIndex, string nodeText)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            UpdateNode(xDoc, Index, nodeName, childIndex, nodeText);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">修改的节点名称</param>
        /// <param name="nodeText">修改后的内容</param>
        public static void UpdateNode(string fileFullName, int Index, string nodeName, string nodeText)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            UpdateNode(xDoc, Index, nodeName, nodeText);
            xDoc.Save(fileFullName);
        }
        #endregion

        #region DeleteNode
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="Index">节点索引</param>
        public static void DeleteNode(XmlDocument xDoc, int Index)
        {
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            nlst[Index].ParentNode.RemoveChild(nlst[Index]);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="Index">节点索引</param>
        public static void DeleteNode(string fileFullName, int Index)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            DeleteNode(xDoc, Index);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="xns">需要删除的节点对象</param>
        public static void DeleteNode(XmlDocument xDoc, params XmlNode[] xns)
        {
            foreach (XmlNode xnl in xns)
            {
                foreach (XmlNode xn in xDoc.DocumentElement.ChildNodes)
                {
                    if (xnl.Equals(xn))
                    {
                        xn.ParentNode.RemoveChild(xn);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="xns">需要删除的节点对象</param>
        public static void DeleteNode(string fileFullName, params XmlNode[] xns)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            DeleteNode(xDoc, xns);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeText">节点内容</param>
        public static void DeleteNode(XmlDocument xDoc, string nodeName, string nodeText)
        {
            foreach (XmlNode xn in xDoc.DocumentElement.ChildNodes)
            {
                if (xn.Name == nodeName)
                {
                    if (xn.InnerText == nodeText)
                    {
                        xn.ParentNode.RemoveChild(xn);
                        return;
                    }
                }
                else
                {
                    XmlNode node = GetNode(xn, nodeName);
                    if (node != null && node.InnerText == nodeText)
                    {
                        node.ParentNode.ParentNode.RemoveChild(node.ParentNode);
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeText">节点内容</param>
        public static void DeleteNode(string fileFullName, string nodeName, string nodeText)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            DeleteNode(xDoc, nodeName, nodeText);
            xDoc.Save(fileFullName);
        }
        #endregion

        #region SetAttribute
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="attps">属性参数</param>
        public static void SetAttribute(XmlNode node, params AttributeParameter[] attps)
        {
            XmlElement xe = (XmlElement)node;
            foreach (AttributeParameter attp in attps)
            {
                xe.SetAttribute(attp.Name, attp.Value);
            }
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void SetAttribute(XmlNode node, string attributeName, string attributeValue)
        {
            XmlElement xe = (XmlElement)node;
            xe.SetAttribute(attributeName, attributeValue);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="elem">元素对象</param>
        /// <param name="attps">属性参数</param>
        public static void SetAttribute(XmlElement elem, params AttributeParameter[] attps)
        {
            foreach (AttributeParameter attp in attps)
            {
                elem.SetAttribute(attp.Name, attp.Value);
            }
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="elem">元素对象</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void SetAttribute(XmlElement elem, string attributeName, string attributeValue)
        {
            elem.SetAttribute(attributeName, attributeValue);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="xpara">XML参数</param>
        /// <param name="attps">属性参数</param>
        public static void SetAttribute(XmlDocument xDoc, XmlParameter xpara, params AttributeParameter[] attps)
        {
            XmlElement xe = (XmlElement)GetNode(xDoc, xpara);
            if (xe == null) return;
            SetAttribute(xe, attps);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="xpara">XML参数</param>
        /// <param name="newValue">新属性值</param>
        public static void SetAttribute(XmlDocument xDoc, XmlParameter xpara, string attributeName, string newValue)
        {
            XmlElement xe = (XmlElement)GetNode(xDoc, xpara);
            if (xe == null) return;
            SetAttribute(xe, attributeName, newValue);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="xpara">XML参数</param>
        /// <param name="newValue">新属性值</param>
        public static void SetAttribute(string fileFullName, XmlParameter xpara, string attributeName, string newValue)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            SetAttribute(xDoc, xpara, attributeName, newValue);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="xpara">XML参数</param>
        /// <param name="attps">属性参数</param>
        public static void SetAttribute(string fileFullName, XmlParameter xpara, params AttributeParameter[] attps)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            SetAttribute(xDoc, xpara, attps);
            xDoc.Save(fileFullName);
        }
        #endregion
    }
}
