using System.Collections.Generic;

namespace QQChannelFramework.Models;

/// <summary>
/// 成员信息与所在频道信息对象
/// </summary>
public class MemberWithGuildID
{
    /// <summary>
    /// 所在主频道Id
    /// </summary>
    public string GuildId { get; set; }

    /// <summary>
    /// 用户在频道内的昵称
    /// </summary>
    public string Nick { get; set; }

    /// <summary>
    /// 用户在频道内的身份
    /// </summary>
    public List<string> roles { get; set; }

    /// <summary>
    /// 用户加入频道的时间
    /// </summary>
    public DateTime? JoinedAt { get; set; }

    /// <summary>
    /// 用户信息
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// 操作人Id
    /// </summary>
    public string OperationUserId { get; set; }
}
