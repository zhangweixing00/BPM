using System;

/// <summary>
///一些基类的参数配置类
/// </summary>

[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false,Inherited=true)]
public class BPMAttribute : Attribute
{
	public BPMAttribute()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public string AppId { get; set; }
}