using System.Collections.Generic;
using MyBot.Models.Types;
using Newtonsoft.Json;

namespace MyBot.Models;

/// <summary>
/// 子频道对象
/// </summary>
public class Channel
{
    /// <summary>
    /// 子频道ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// 所处主频道ID
    /// </summary>
    [JsonProperty("guild_id")]
    public string GuildId { get; set; }

    /// <summary>
    /// 子频道名称
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 子频道类型
    /// </summary>
    [JsonProperty("type")]
    public ChannelType Type { get; set; }

    /// <summary>
    /// 子类型
    /// </summary>
    [JsonProperty("sub_type")]
    public ChannelSubType SubType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [JsonProperty("position")]
    public int Position { get; set; }

    /// <summary>
    /// 分组ID
    /// </summary>
    [JsonProperty("parent_id")]
    public string ParentId { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonProperty("owner_id")]
    public string OwnerId { get; set; }
    
    
    /// <summary>
    /// 子频道私密类型 
    /// </summary>
    [JsonProperty("private_type")]
    public ChannelPrivateType? PrivateType  { get; set; }
    
    /// <summary>
    /// 额外成员
    /// </summary>
    [JsonProperty("private_user_ids")]
    public List<string> PrivateUserIds { get; set; }

    /// <summary>
    /// 子频道发言权限
    /// </summary>
    [JsonProperty("speak_permission")]
    public ChannelSpeakPermission? SpeakPermission { get; set; }
    
    /// <summary>
    /// 用于标识应用子频道应用类型，仅应用子频道时会使用该字段
    /// </summary>
    [JsonProperty("application_id")]
    public string ApplicationId { get; set; }
}