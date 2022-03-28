using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 获取当前用户频道列表
/// </summary>
public struct RawGetCurrentChannelsJoinedApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/users/@me/guilds";

    public MethodType Method => MethodType.GET;
}