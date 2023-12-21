using MyBot.Api.Types;
namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 获取频道全部身份组
/// </summary>
public struct RawGetChannelRolesApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/guilds/{guild_id}/roles";

    public MethodType Method => MethodType.GET;
}