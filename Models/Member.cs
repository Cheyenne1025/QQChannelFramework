using System.Collections.Generic;
using MyBot.Tools.JsonConverters;
using Newtonsoft.Json;
namespace MyBot.Models;

/// <summary>
/// 成员对象
/// </summary>
public class Member
{
    /// <summary>
    /// 用户基础信息
    /// </summary>
    [JsonProperty("user")] 
    public User User { get; set; }

    /// <summary>
    /// 频道内昵称
    /// </summary>
    [JsonProperty("nick")] 
    public string Nick { get; set; }

    /// <summary>
    /// 用户在频道内的身份组ID
    /// </summary>
    [JsonProperty("roles")] 
    public List<string> Roles { get; set; }

    /// <summary>
    /// 用户加入频道的时间
    /// </summary>
    [JsonProperty("joined_at")] 
    [JsonConverter(typeof(UnixSecondsToDateTimeConverter))]
    public DateTime? JoinedAt { get; set; }
}