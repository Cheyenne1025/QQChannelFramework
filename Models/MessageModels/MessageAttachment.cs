using Newtonsoft.Json;
namespace MyBot.Models.MessageModels;

/// <summary>
/// 消息附件
/// </summary>
public class MessageAttachment {
    /// <summary>
    /// 附件地址
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }
}