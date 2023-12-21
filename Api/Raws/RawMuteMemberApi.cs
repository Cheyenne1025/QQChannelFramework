using MyBot.Api.Types;
namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 禁言指定频道的成员
/// </summary>
public struct RawMuteMemberApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/guilds/{guild_id}/members/{user_id}/mute";

    public MethodType Method => MethodType.PATCH;
}