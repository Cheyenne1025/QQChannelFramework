using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.ChannelPermissionModels;

/// <summary>
/// 接口权限对象
/// </summary>
public class APIPermission
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

    /// <summary>
    /// API 接口名称，例如 获取频道信
    /// </summary>
    [Newtonsoft.Json.JsonProperty("desc")]
    public string Desc { get; set; }

    /// <summary>
    /// 授权状态，auth_stats 为 1 时已授权
    /// </summary>
    [Newtonsoft.Json.JsonProperty("auth_status")]
    public int Status { get; set; }
}