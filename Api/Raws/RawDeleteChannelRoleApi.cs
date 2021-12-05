using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 删除频道身份组
/// </summary>
public struct RawDeleteChannelRoleApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => false;

    public string Url => "/guilds/{guild_id}/roles/{role_id}";

    public MethodType Method => MethodType.DELETE;
}