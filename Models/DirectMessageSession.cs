using Newtonsoft.Json;

namespace QQChannelFramework.Models;

/// <summary>
/// 私信会话对象
/// </summary>
public class DirectMessageSession {
    /// <summary>
    /// 私信会话关联的频道 id
    /// </summary>
    [JsonProperty("guild_id")]
    public string GuildId { get; set; }

    /// <summary>
    /// 私信会话关联的子频道 id
    /// </summary>
    [JsonProperty("channel_id")]
    public string ChannelId { get; set; }

    /// <summary>
    /// 创建私信会话时间戳
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }
}