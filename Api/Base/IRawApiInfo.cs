using System.Collections.Generic;

namespace QQChannelFramework.Api.Base;

/// <summary>
/// 源Api信息
/// </summary>
public interface IRawApiInfo
{
    /// <summary>
    /// Api版本
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// 是否需要参数
    /// </summary>
    public bool NeedParam { get; }

    /// <summary>
    /// 源Api地址
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// 请求方法
    /// </summary>
    public Types.MethodType Method { get; }
}