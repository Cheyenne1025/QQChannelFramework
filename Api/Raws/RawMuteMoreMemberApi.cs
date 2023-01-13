using MyBot.Api.Types;

namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 批量禁言成员
/// </summary>
public struct RawMuteMoreMemberApi : Base.IRawApiInfo
{
    public string Version => "1.0";
    public string Url => "/guilds/{guild_id}/mute";
    public MethodType Method => MethodType.PATCH;
}
