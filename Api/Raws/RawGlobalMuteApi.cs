using QQChannelFramework.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 全局禁言频道
/// </summary>
public struct RawGlobalMuteApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => true;

    public string Url => "/guilds/{guild_id}/mute";

    public MethodType Method => MethodType.PATCH;
}