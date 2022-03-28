using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 增加频道身份组成员
/// </summary>
public struct RawAddMemberToRoleApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/guilds/{guild_id}/members/{user_id}/roles/{role_id}";

    public MethodType Method => MethodType.PUT;
}