using MyBot.Api.Base;
using MyBot.Api.Types;
namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 获取主频道信息
/// </summary>
public struct RawGetMainChannelApi : IRawApiInfo
{
    public string Version => "1.0";

    public string Url => "/guilds/{guild_id}";

    public MethodType Method => MethodType.GET;

    }