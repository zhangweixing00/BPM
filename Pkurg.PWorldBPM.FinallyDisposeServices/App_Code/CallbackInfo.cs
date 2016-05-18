using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///回调返回实体
/// </summary>
public class CallbackInfo
{
	public CallbackInfo()
	{
        ArrayParam = new List<string>();
	}

    public string FormCode { get; set; }

    public string FormType { get; set; }

    public string Status { get; set; }

    public string XmlParam { get; set; }

    public List<string> ArrayParam { get; set; }
}