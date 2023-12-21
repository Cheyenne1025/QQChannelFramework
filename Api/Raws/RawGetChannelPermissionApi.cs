using MyBot.Api.Types;
namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 获取频道可用权限列表
/// </summary>
public struct RawGetChannelPermissionApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/guilds/{guild_id}/api_permission";

    public MethodType Method => MethodType.GET;
}