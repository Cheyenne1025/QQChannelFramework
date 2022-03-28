using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 创建频道身份组
/// </summary>
public struct RawCreateChannelRoleApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/guilds/{guild_id}/roles";

    public MethodType Method => MethodType.POST;
}