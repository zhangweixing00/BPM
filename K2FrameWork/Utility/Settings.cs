using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Web.Configuration;
using System.Collections.Specialized;
using System.Data;

namespace Utility
{
    public static class Settings
    {
        private static string K2ProcessSettingsPath = HttpContext.Current.Server.MapPath("~/WorkSpace/XML/K2ProcessSettings.xml");//General.GetConstValue("K2ProcessSettingFile");//

        #region GetProcessSettings

        public static List<ProcessSettingsEntity> GetAllProcessSettings()
        {
            List<ProcessSettingsEntity> listProcSettings = new List<ProcessSettingsEntity>();
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(K2ProcessSettingsPath);
                XmlNodeList nodeList = doc.SelectNodes("/Root/Process");

                if (nodeList != null && nodeList.Count > 0)
                {
                    foreach (XmlNode node in nodeList)
                    {
                        if (node == null)
                        {
                            continue;
                        }
                        ProcessSettingsEntity entity = new ProcessSettingsEntity();
                        entity.StartPage = GetNodeValue(node, "./StartPage");
                        entity.ViewPage = GetNodeValue(node, "./ViewPage");
                        entity.EditPage = GetNodeValue(node, "./EditPage");
                        string Destription = GetNodeValue(node, "./Description");
                        entity.ProcessFullName = GetNodeValue(node, "./ProcessFullName");
                        if (Destription == "")
                            entity.Description = GetNodeValue(node, "./ProcessName");
                        else
                            entity.Description = Destription;

                        listProcSettings.Add(entity);
                    }
                }

                return listProcSettings;
            }
            catch
            {
                return listProcSettings;
            }
        }

        public static string GetProcessDescription(string processName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(K2ProcessSettingsPath);
                XmlNode node = doc.SelectSingleNode("/Root/Process[ProcessName='" + processName + "']");
                if (node == null)
                {
                    return processName;
                }


                return GetNodeValue(node, "./Description");
            }
            catch
            {
                return processName;
            }
        }

        public static ProcessSettingsEntity GetProcessSettings(int procSetId)
        {
            try
            {
                ProcessSettingsEntity entity = new ProcessSettingsEntity();
                if (File.Exists(K2ProcessSettingsPath))
                {
                    XmlDocument doc = new XmlDocument();

                    doc.Load(K2ProcessSettingsPath);
                    XmlNode node = doc.SelectSingleNode("/Root/Process[@ProcSetId='" + procSetId.ToString() + "']");
                    if (node != null)
                    {
                        entity.StartPage = GetNodeValue(node, "./StartPage");
                        entity.ViewPage = GetNodeValue(node, "./ViewPage");
                        entity.EditPage = GetNodeValue(node, "./EditPage");
                        entity.Description = GetNodeValue(node, "./Description");
                    }
                }

                return entity;
            }
            catch
            {
                return new ProcessSettingsEntity();
            }
        }
        #endregion

        public static void CreateProcessSettings(DataTable dt)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(K2ProcessSettingsPath))
            {
                doc.Load(K2ProcessSettingsPath);
            }
            else
            {
                doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Root></Root>");
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlNode node = doc.SelectSingleNode("/Root/Process[@ProcSetId='" + dt.Rows[i]["ProcSetID"] + "']");
                if (node == null)
                {
                    XmlElement newNode = doc.CreateElement("Process");
                    newNode.SetAttribute("ProcSetId", dt.Rows[i]["ProcSetID"].ToString());
                    newNode.AppendChild(CreateChildNodeWidthValue(doc, "ProcessName", dt.Rows[i]["Name"].ToString()));
                    newNode.AppendChild(CreateChildNodeWidthValue(doc, "Folder", dt.Rows[i]["Folder"].ToString()));
                    newNode.AppendChild(CreateChildNodeWidthValue(doc, "ProcessFullName", dt.Rows[i]["FullName"].ToString()));
                    newNode.AppendChild(CreateChildNodeWidthValue(doc, "StartPage", dt.Rows[i]["StartPage"].ToString()));
                    newNode.AppendChild(CreateChildNodeWidthValue(doc, "ViewPage", dt.Rows[i]["ViewPage"].ToString()));
                    newNode.AppendChild(CreateChildNodeWidthValue(doc, "EditPage", dt.Rows[i]["EditPage"].ToString()));
                    newNode.AppendChild(CreateChildNodeWidthValue(doc, "Description", dt.Rows[i]["Description"].ToString()));

                    XmlNode rootNode = doc.SelectSingleNode("/Root");
                    rootNode.AppendChild(newNode);
                }
            }

            doc.Save(K2ProcessSettingsPath);
        }
        #region UpdateProcessSettings
        public static void UpdateProcessSettings(ProcessSettingsEntity entity)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(K2ProcessSettingsPath))
            {
                doc.Load(K2ProcessSettingsPath);
            }
            else
            {
                doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Root></Root>");
            }
            XmlNode node = doc.SelectSingleNode("/Root/Process[@ProcSetId='" + entity.ProcSetId.ToString() + "']");
            if (node == null)
            {
                XmlElement newNode = doc.CreateElement("Process");
                newNode.SetAttribute("ProcSetId", entity.ProcSetId.ToString());

                newNode.AppendChild(CreateChildNodeWidthValue(doc, "ProcessName", entity.ProcessName));
                newNode.AppendChild(CreateChildNodeWidthValue(doc, "Folder", entity.Folder));
                newNode.AppendChild(CreateChildNodeWidthValue(doc, "ProcessFullName", entity.ProcessFullName));
                newNode.AppendChild(CreateChildNodeWidthValue(doc, "StartPage", entity.StartPage));
                newNode.AppendChild(CreateChildNodeWidthValue(doc, "ViewPage", entity.ViewPage));
                newNode.AppendChild(CreateChildNodeWidthValue(doc, "EditPage", entity.EditPage));
                newNode.AppendChild(CreateChildNodeWidthValue(doc, "Description", entity.Description));

                XmlNode rootNode = doc.SelectSingleNode("/Root");
                rootNode.AppendChild(newNode);
            }
            else
            {
                XmlNode child;

                //Processname
                //child = node.SelectSingleNode("./ProcessName");
                //if (child == null)
                //{
                //    node.AppendChild(CreateChildNodeWidthValue(doc, "ProcessName", entity.ProcessName));
                //}
                //else
                //{
                //    child.InnerText = entity.ProcessName;
                //}

                //Folder
                //child = node.SelectSingleNode("./Folder");
                //if (child == null)
                //{
                //    node.AppendChild(CreateChildNodeWidthValue(doc, "Folder", entity.Folder));
                //}
                //else
                //{
                //    child.InnerText = entity.Folder;
                //}

                //ProcessFullName
                //child = node.SelectSingleNode("./ProcessFullName");
                //if (child == null)
                //{
                //    node.AppendChild(CreateChildNodeWidthValue(doc, "ProcessFullName", entity.ProcessFullName));
                //}
                //else
                //{
                //    child.InnerText = entity.ProcessFullName;
                //}

                //StartPage
                child = node.SelectSingleNode("./StartPage");
                if (child == null)
                {
                    node.AppendChild(CreateChildNodeWidthValue(doc, "StartPage", entity.StartPage));
                }
                else
                {
                    child.InnerText = entity.StartPage;
                }

                //ViewPage
                child = node.SelectSingleNode("./ViewPage");
                if (child == null)
                {
                    node.AppendChild(CreateChildNodeWidthValue(doc, "ViewPage", entity.ViewPage));
                }
                else
                {
                    child.InnerText = entity.ViewPage;
                }

                //EditPage
                child = node.SelectSingleNode("./EditPage");
                if (child == null)
                {
                    node.AppendChild(CreateChildNodeWidthValue(doc, "EditPage", entity.EditPage));
                }
                else
                {
                    child.InnerText = entity.EditPage;
                }

                //Description
                child = node.SelectSingleNode("./Description");
                if (child == null)
                {
                    node.AppendChild(CreateChildNodeWidthValue(doc, "Description", entity.Description));
                }
                else
                {
                    child.InnerText = entity.Description;
                }
            }

            doc.Save(K2ProcessSettingsPath);
        }
        #endregion

        #region KeyValuePair
        public static string GetConfigurationValue(string original)
        {
            string v = General.GetConstValue("K2_NameValue_" + original);
            if (v == "")
                v = original;

            return v;
            //NameValueCollection cc = WebConfigurationManager.AppSettings;
            //string v = cc["K2_NameValue_" + original];
            //if (string.IsNullOrEmpty(v))
            //{
            //    return original;
            //}
            //return v;
        }
        #endregion

        #region private
        private static string GetNodeValue(XmlNode node, string nodepath)
        {
            XmlNode childNode = node.SelectSingleNode(nodepath);
            if (childNode == null)
            {
                return string.Empty;
            }
            return childNode.InnerText;
        }

        public static string GetNodeValue(string procSetID, string nodepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(K2ProcessSettingsPath);
            XmlNode node = doc.SelectSingleNode("/Root/Process[@ProcSetId='" + procSetID.ToString() + "']");

            if (node != null)
            {
                XmlNode childNode = node.SelectSingleNode(nodepath);

                if (childNode != null)
                {
                    return childNode.InnerText;
                }
            }

            return string.Empty;
        }

        private static void SetNodeValue(XmlNode node, string nodepath, string value)
        {
            XmlNode childNode = node.SelectSingleNode(nodepath);
            if (childNode == null)
            {
                return;
            }
            childNode.InnerText = value;
        }

        private static XmlElement CreateChildNodeWidthValue(XmlDocument doc, string name, string value)
        {
            XmlElement node = doc.CreateElement(name);
            node.InnerText = value;
            return node;
        }
        #endregion
    }
}