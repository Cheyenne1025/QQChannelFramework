using System.Collections.Generic;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 消息对象
/// </summary>
public class Message
{
    /// <summary>
    /// 消息Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 子频道
    /// </summary>
    public string ChildChannelId { get; set; }

    /// <summary>
    /// 主频道
    /// </summary>
    public string GuildId { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 消息创建时间
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// 消息编辑时间
    /// </summary>
    public DateTime EditedTime { get; set; }

    /// <summary>
    /// 是否@全体成员
    /// </summary>
    public bool MentionEveryone { get; set; }

    /// <summary>
    /// 消息创建者
    /// </summary>
    public User Author { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    public MessageAttachment? Attachments { get; set; }

    public MessageEmbed? Embeds { get; set; }

    /// <summary>
    /// 消息中@的人
    /// </summary>
    public List<User>? Mentions { get; set; }

    /// <summary>
    /// 消息创建者的Member信息
    /// </summary>
    public Member Member { get; set; }

    /// <summary>
    /// ark消息
    /// </summary>
    public MessageArk? Ark { get; set; }
}