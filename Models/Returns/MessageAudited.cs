using Newtonsoft.Json;
namespace MyBot.Models.Returns;

/// <summary>
/// 消息审核对象
/// </summary>
public class MessageAudited {
    /// <summary>
    /// 消息审核 id
    /// </summary>
    [JsonProperty("audit_id")]
    public string AuditId { get; set; }

    /// <summary>
    /// 消息审核时间
    /// </summary>
    [JsonProperty("audit_time")]
    public DateTime AuditTime { get; set; }

    /// <summary>
    /// 子频道 id
    /// </summary>
    [JsonProperty("channel_id")]
    public string ChannelId { get; set; }

    /// <summary>
    /// 消息创建时间
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 频道 id
    /// </summary>
    [JsonProperty("guild_id")]
    public string GuildId { get; set; }

    /// <summary>
    /// 消息 id，只有审核通过事件才会有值
    /// </summary>
    [JsonProperty("message_id")]
    public string MessageId { get; set; }
    
    /// <summary>
    /// 子频道消息 seq，用于消息间的排序，seq 在同一子频道中按从先到后的顺序递增，不同的子频道之间消息无法排序
    /// </summary>
    [JsonProperty("seq_in_channel")]
    public string SequenceInChannel { get; set; }
}