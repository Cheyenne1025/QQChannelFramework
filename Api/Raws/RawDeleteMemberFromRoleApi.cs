using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 从身份组中删除成员
/// </summary>
public struct RawDeleteMemberFromRoleApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => true;

    public string Url => "/guilds/{guild_id}/members/{user_id}/roles/{role_id}";

    public MethodType Method => MethodType.DELETE;
}