using System.Collections.Generic;
using Newtonsoft.Json;

namespace QQChannelFramework.Models;

/// <summary>
/// 成员信息与所在频道信息对象
/// </summary>
public class MemberWithGuildID : Member
{
    /// <summary>
    /// 所在主频道Id
    /// </summary>
    [JsonProperty("guild_id")]
    public string GuildId { get; set; }  

    /// <summary>
    /// 操作人Id
    /// </summary>
    [JsonProperty("op_user_id")]
    public string OperationUserId { get; set; }
}
