using System;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 表情态对象
/// </summary>
public class MessageReaction
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonProperty("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// 主频道ID
    /// </summary>
    [JsonProperty("guild_id")]
    public string GuildId { get; set; }

    /// <summary>
    /// 子频道ID
    /// </summary>
    [JsonProperty("channel_id")]
    public string ChildChannel { get; set; }

    /// <summary>
    /// 表态对象
    /// </summary>
    [JsonProperty("target")]
    public ReactionTarget Target { get; set; }

    /// <summary>
    /// 表情
    /// </summary>
    [JsonProperty("emoji")]
    public Emoji Emoji { get; set; }
}