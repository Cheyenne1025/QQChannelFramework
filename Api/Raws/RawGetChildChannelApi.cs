using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 获取子频道信息
/// </summary>
public struct RawGetChildChannelApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => false;

    public string Url => "/channels/{channel_id}";

    public MethodType Method => MethodType.GET;
}