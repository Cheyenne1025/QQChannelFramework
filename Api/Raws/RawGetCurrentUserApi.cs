using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 获取当前用户信息
/// </summary>
public struct RawGetCurrentUserApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => false;

    public string Url => "/users/@me";

    public MethodType Method => MethodType.GET;
}