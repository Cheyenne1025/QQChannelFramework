using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyBot.Models.MessageModels;

/// <summary>
/// 消息内嵌
/// </summary>
public class MessageEmbed {
    /// <summary>
    /// 标题
    /// </summary> 
    [JsonProperty("title")]
    public string Title { get; set; } 

    /// <summary>
    /// 消息弹窗内容
    /// </summary>
    [JsonProperty("prompt")]
    public string Prompt { get; set; } 

    /// <summary>
    /// 缩略图
    /// </summary>
    [JsonProperty("thumbnail")]
    public MessageEmbedThumbnail Thumbnail { get; set; }
    
    /// <summary>
    /// Fields
    /// </summary>
    [JsonProperty("fields")]
    public List<MessageEmbedField> Fields { get; set; }
}