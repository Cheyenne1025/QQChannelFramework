namespace MyBot.Models.ChannelPermissionModels;

/// <summary>
/// 接口权限需求对象
/// </summary>
public class APIPermissionDemand
{
    /// <summary>
    /// 申请接口权限的频道 id
    /// </summary>
    [Newtonsoft.Json.JsonProperty("guild_id")]
    public string GuildId { get; set; }

    /// <summary>
    /// 接口权限需求授权链接发送的子频道 id
    /// </summary>
    [Newtonsoft.Json.JsonProperty("channel_id")]
    public string ChannelId { get; set; }

    /// <summary>
    /// 权限接口唯一标识
    /// </summary>
    [Newtonsoft.Json.JsonProperty("api_identify")]
    public APIPermissionDemandIdentify ApiIdentify { get; set; }

    /// <summary>
    /// 接口权限链接中的接口权限描述信息
    /// </summary>
    [Newtonsoft.Json.JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    /// 接口权限链接中的机器人可使用功能的描述信息
    /// </summary>
    [Newtonsoft.Json.JsonProperty("desc")]
    public string Desc { get; set; }
}