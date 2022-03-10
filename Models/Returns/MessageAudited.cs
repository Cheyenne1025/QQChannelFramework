using Newtonsoft.Json;

namespace ChannelModels.Returns; 

public class MessageAudited {
    [JsonProperty("audit_id")]
    public string AuditId { get; set; }

    [JsonProperty("audit_time")]
    public DateTime AuditTime { get; set; }

    [JsonProperty("channel_id")]
    public string ChannelId { get; set; }

    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    [JsonProperty("guild_id")]
    public string GuildId { get; set; }

    [JsonProperty("message_id")]
    public string MessageId { get; set; }
    
    /// <summary>
    /// 子频道消息 seq，用于消息间的排序，seq 在同一子频道中按从先到后的顺序递增，不同的子频道之间消息无法排序
    /// </summary>
    [JsonProperty("seq_in_channel")]
    public string SequenceInChannel { get; set; }
}