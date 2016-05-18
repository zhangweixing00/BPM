using System.IO;
using System.Web;

/// <summary>
///SignPicHelper 的摘要说明
/// </summary>
public class SignPicHelper
{
    /// <summary>
    /// 获取审批人显示信息
    /// </summary>
    /// <param name="approvalUserCode"></param>
    /// <param name="approvalUserName"></param>
    /// <returns></returns>
    public static string GetSignPic(string approvalUserCode, string approvalUserName)
    {
        if (string.IsNullOrEmpty(approvalUserCode))
        {
            return approvalUserName;
        }

        string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Img_Sign"), approvalUserCode) + ".gif";
        if (!File.Exists(filePath))
        {
            return approvalUserName;
        }
        return string.Format("<img class='ImgSign' src='/Files/Img_Sign/{0}.gif' alt='{1}' longdesc='{1}' title='{1}'/>", approvalUserCode, approvalUserName);
    }
}