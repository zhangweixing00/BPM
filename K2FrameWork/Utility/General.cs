using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;

namespace Utility
{
    public class General
    {
        /// <summary>
        /// 获取XML中固定值
        /// </summary>
        /// <param name="constName"></param>
        /// <returns></returns>
        public static string GetConstValue(string constName)
        {
            try
            {
                string constFilePath = ConfigurationManager.AppSettings["ConstSettingFilePath"];
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(constFilePath);

                XmlNode xn = xmldoc.SelectSingleNode("Consts/Const[@name='" + constName + "']");
                if (xn != null)
                    return xn.Attributes["value"].Value;

                return "";
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "HC.Admin.General.GetConstValue", "HC.Admin");
                return "";
            }
        }
    }
}
