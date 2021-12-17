using System;
using Newtonsoft.Json;
using QQChannelFramework.Models.Types;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 表态对象
/// </summary>
public class ReactionTarget
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public ReactionTargetType Type { get; set; }
}