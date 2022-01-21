namespace QQChannelFramework.Models;

/// <summary>
/// 私信会话对象
/// </summary>
public class DirectMessageSession {
    /// <summary>
    /// 私信会话关联的频道 id
    /// </summary>
    public string GuildId { get; set; }

    /// <summary>
    /// 私信会话关联的子频道 id
    /// </summary>
    public string ChannelId { get; set; }

    /// <summary>
    /// 创建私信会话时间戳
    /// </summary>
    public DateTime? CreateTime { get; set; }
}