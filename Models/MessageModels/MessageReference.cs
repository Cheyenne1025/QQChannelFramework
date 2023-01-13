using Newtonsoft.Json;

namespace MyBot.Models.MessageModels; 
 
/// <summary>
/// 消息引用对象
/// </summary>
public class MessageReference
{
    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonProperty("message_id")]
    public string MessageId { get; set; }

    /// <summary>
    /// 是否忽略获取引用消息详情错误，默认否
    /// </summary>
    [JsonProperty("ignore_get_message_error")]
    public bool IgnoreGetMessageError { get; set; }
}