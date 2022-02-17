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
}