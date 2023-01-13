namespace MyBot.Models.MessageModels;

/// <summary>
/// 消息内嵌数据
/// </summary>
public class MessageEmbedField
{
    /// <summary>
    /// 字段名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 字段值
    /// </summary>
    public string Value { get; set; }
}