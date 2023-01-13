using MyBot.Api.Types;

namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 创建频道 API 接口权限授权链接
/// </summary>
public struct RawCreateChannelPermissionAuthUrlApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/guilds/{guild_id}/api_permission/demand";

    public MethodType Method => MethodType.POST;
}