using MyBot.Tools.JsonConverters;
using Newtonsoft.Json;

namespace MyBot.Models;

/// <summary>
/// 主频道对象
/// </summary>
public class Guild
{
    /// <summary>
    /// 频道ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// 频道名称
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 频道头像Url
    /// </summary>
    [JsonProperty("icon")]
    public string Icon { get; set; }

    /// <summary>
    /// 频道创建人用户ID
    /// </summary>
    [JsonProperty("owner_id")]
    public string OwnerId { get; set; }

    /// <summary>
    /// 当前人是否为创建人
    /// </summary>
    [JsonProperty("owner")]
    public bool Owner { get; set; }

    /// <summary>
    /// 当前成员数
    /// </summary>
    [JsonProperty("member_count")]
    public int MemberCount { get; set; }

    /// <summary>
    /// 最大成员数
    /// </summary>
    [JsonProperty("max_members")]
    public int MaxMembers { get; set; }

    /// <summary>
    /// 频道描述
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    /// 加入时间
    /// </summary>
    [JsonProperty("joined_at")]
    [JsonConverter(typeof(UnixSecondsToDateTimeConverter))]
    public DateTime? JoinedAt { get; set; } 
}