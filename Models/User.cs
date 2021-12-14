using Newtonsoft.Json;

namespace QQChannelFramework.Models;

/// <summary>
/// 用户对象
/// </summary>
public class User {
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [JsonProperty("username")]
    public string UserName { get; set; }

    /// <summary>
    /// 头像Url
    /// </summary>
    [JsonProperty("avatar")]
    public string Avatar { get; set; }

    /// <summary>
    /// 是否是机器人
    /// </summary>
    [JsonProperty("bot")]
    public bool IsBot { get; set; }

    /// <summary>
    /// 特殊关联应用的 openid，需要特殊申请并配置后才会返回。如需申请，请联系平台运营人员
    /// </summary>
    [JsonProperty("union_openid")]
    public string UnionOpenid { get; set; }

    /// <summary>
    /// 机器人关联的互联应用的用户信息，与union_openid关联的应用是同一个。如需申请，请联系平台运营人员
    /// </summary>
    [JsonProperty("union_user_account")]
    public string UnionUserAccount { get; set; }
}