using System.Collections.Generic;
using Newtonsoft.Json;
namespace MyBot.Models.MessageModels; 

public class PinsMessage {
    /// <summary>
    /// 子频道内精华消息 id 数组
    /// </summary>
    [JsonProperty("message_ids")]
    public List<string> Id { get; set; } = new();

    /// <summary>
    /// 子频道
    /// </summary>
    [JsonProperty("channel_id")]
    public string ChannelId { get; set; }

    /// <summary>
    /// 主频道
    /// </summary>
    [JsonProperty("guild_id")]
    public string GuildId { get; set; }
}