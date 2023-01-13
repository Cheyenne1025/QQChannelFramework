using MyBot.Api.Types;

namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 全局禁言频道
/// </summary>
public struct RawGlobalMuteApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/guilds/{guild_id}/mute";

    public MethodType Method => MethodType.PATCH;
}