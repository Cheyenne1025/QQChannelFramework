using QQChannelFramework.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 创建频道 API 接口权限授权链接
/// </summary>
public struct RawCreateChannelPermissionAuthUrlApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => true;

    public string Url => "/guilds/{guild_id}/api_permission/demand";

    public MethodType Method => MethodType.POST;
}