using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.ChannelPermissionModels;

/// <summary>
/// 接口权限需求标识对象
/// </summary>
public record APIPermissionDemandIdentify
{
    /// <summary>
    /// API 接口名，例如 /guilds/{guild_id}/members/{user_id}
    /// </summary>
    [Newtonsoft.Json.JsonProperty("path")]
    public string Path { get; set; }

    /// <summary>
    /// 请求方法，例如 GET
    /// </summary>
    [Newtonsoft.Json.JsonProperty("method")]
    public string Method { get; set; }
}