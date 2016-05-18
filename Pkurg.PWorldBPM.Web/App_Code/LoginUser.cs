using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
///LoginUser 的摘要说明
///系统模拟用户
/// </summary>
public class LoginUser
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public void Insert(string from, string to)
    {
        string Path = HttpContext.Current.Server.MapPath("~/Resource/LoginUsers.xml");
        XElement doc = XElement.Load(Path);
        XElement item = doc.Elements().Where(p => p.Attribute("From").Value == from.ToString()).FirstOrDefault();
        if (item != null)
        {
            item.SetAttributeValue("To", to);
            item.SetAttributeValue("Date", DateTime.Now.ToString());
            doc.Save(Path);
        }
        else
        {
            XElement element = new XElement("User", new XAttribute("From", from), new XAttribute("To", to), new XAttribute("Date", DateTime.Now.ToString()));
            doc.Add(element);
            //如果抛异常，没有权限修改文件，
            //取消Resource/LoginUsers.xml的"只读"属性 
            doc.Save(Path);
        }
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="from"></param>
    public void Delete(string from)
    {
        string Path = HttpContext.Current.Server.MapPath("~/Resource/LoginUsers.xml");
        XElement doc = XElement.Load(Path);
        XElement item = doc.Elements().Where(p => p.Attribute("From").Value == from.ToString()).FirstOrDefault();
        if (item != null)
        {
            item.Remove();
            doc.Save(Path);
        }
    }

    /// <summary>
    /// 删除被模拟
    /// 如果被模拟的人不存在，就删除
    /// </summary>
    /// <param name="to"></param>
    public bool DeleteByTo(string to)
    {
        try
        {
            bool flag = true;
            string Path = HttpContext.Current.Server.MapPath("~/Resource/LoginUsers.xml");
            XElement doc = XElement.Load(Path);
            var items = doc.Elements().Where(p => p.Attribute("To").Value == to.ToString()).ToList();
            foreach (var item in items)
            {
                flag = true;
                item.Remove();
            }

            if (flag)
            {
                doc.Save(Path);
            }
            return flag;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 是否存在模拟
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    public string IsExist(string from)
    {
        string to = "";
        string Path = HttpContext.Current.Server.MapPath("~/Resource/LoginUsers.xml");
        XElement doc = XElement.Load(Path);
        XElement item = doc.Elements().Where(p => p.Attribute("From").Value == from.ToString()).FirstOrDefault();
        if (item != null)
        {
            to = item.Attribute("To").Value;
        }
        return to;
    }
}