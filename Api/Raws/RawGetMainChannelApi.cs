using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 获取主频道信息
/// </summary>
public struct RawGetMainChannelApi : IRawApiInfo
{
    public string Version => "1.0";

    public string Url => "/guilds/{guild_id}";

    public MethodType Method => MethodType.GET;

    public bool NeedParam => false;
}