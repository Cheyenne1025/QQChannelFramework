using System.Collections.Generic;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 消息内嵌
/// </summary>
public class MessageEmbed
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 消息弹窗内容
    /// </summary>
    public string Prompt { get; set; }

    /// <summary>
    /// 消息创建时间
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Fields
    /// </summary>
    public List<MessageEmbedField> Fields { get; set; }
}