using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SourceCode.Workflow.Client;
using System.Xml;
using System.Configuration;
namespace K2.BDAdmin.Web.WorkSpace
{
    public partial class ViewFlow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string ProcInstID = Request.QueryString["ProcInstID"];
            //if (!string.IsNullOrEmpty(ProcInstID))
            //{
            //    using (Connection K2Con = new Connection())
            //    {
            //        //Open a connection to the K2[blackpearl] server
            //        K2Con.Open(ConfigurationManager.AppSettings["K2ServerName"], ConfigurationManager.AppSettings["K2ClientConnection"]);

            //        XmlDocument xmlDoc = new XmlDocument();
            //        xmlDoc.LoadXml(K2Con.ViewProcessInstance(int.Parse(ProcInstID)));

            //        XmlNodeList nodelist = xmlDoc.GetElementsByTagName("LineInsts");
            //        XmlNode newnode = xmlDoc.CreateNode(XmlNodeType.Element, "LineInsts", "");

            //        for (int i = 0; i < nodelist.Item(0).ChildNodes.Count; i++)
            //        {
            //            if (nodelist.Item(0).ChildNodes[i].Attributes["Result"].Value == "1")
            //            {
            //                newnode.AppendChild(nodelist.Item(0).ChildNodes[i].Clone());
            //            }
            //        }
            //        xmlDoc.GetElementsByTagName("ViewProcess").Item(0).ReplaceChild(newnode, xmlDoc.GetElementsByTagName("LineInsts").Item(0));
            //        strXML.Value = xmlDoc.InnerXml;

            //        K2Con.Close();
            //    }
            //}
        }
    }
}
