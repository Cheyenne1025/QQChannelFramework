using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 消息内嵌缩略图
/// </summary>
public class MessageEmbedThumbnail
{
    /// <summary>
    /// 地址
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }
}