using System;
using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 表情
/// </summary>
public class Emoji
{
    /// <summary>
    /// 表情ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// 表情类型
    /// </summary>
    [JsonProperty("type")]
    public int Type { get; set; }
}