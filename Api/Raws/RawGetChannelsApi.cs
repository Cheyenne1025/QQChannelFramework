using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 获取频道下的子频道列表
/// </summary>
public struct RawGetChannelsApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/guilds/{guild_id}/channels";

    public MethodType Method => MethodType.GET;
}