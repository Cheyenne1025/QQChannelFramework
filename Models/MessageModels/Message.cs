using System.Collections.Generic;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 消息对象
/// </summary>
public class Message {
    /// <summary>
    /// 消息Id
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// 子频道
    /// </summary>
    [JsonProperty("channel_id")]
    public string ChildChannelId { get; set; }

    /// <summary>
    /// 主频道
    /// </summary>
    [JsonProperty("guild_id")]
    public string GuildId { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    [JsonProperty("content")]
    public string Content { get; set; }

    /// <summary>
    /// 消息创建时间
    /// </summary>
    [JsonProperty("timestamp")]
    public DateTime Time { get; set; }

    /// <summary>
    /// 消息编辑时间
    /// </summary>
    [JsonProperty("edited_timestamp")]
    public DateTime EditedTime { get; set; }

    /// <summary>
    /// 是否@全体成员
    /// </summary>
    [JsonProperty("mention_everyone")]
    public bool MentionEveryone { get; set; }

    /// <summary>
    /// 消息创建者
    /// </summary>
    [JsonProperty("author")]
    public User Author { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    [JsonProperty("attachments")] 
    public List<MessageAttachment> Attachments { get; set; }

    [JsonProperty("embeds")] 
    public List<MessageEmbed> Embeds { get; set; }

    /// <summary>
    /// 消息中@的人
    /// </summary>
    [JsonProperty("mentions")]
    public List<User> Mentions { get; set; }

    /// <summary>
    /// 消息创建者的Member信息
    /// </summary>
    [JsonProperty("member")]
    public Member Member { get; set; }

    /// <summary>
    /// ark消息
    /// </summary>
    [JsonProperty("ark")]
    public MessageArk Ark { get; set; }
    
    /// <summary>
    /// zi
    /// </summary>
    [JsonProperty("seq")]
    public int Sequence { get; set; }
}