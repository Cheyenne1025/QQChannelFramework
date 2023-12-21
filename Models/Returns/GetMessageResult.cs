using MyBot.Models.MessageModels;
using Newtonsoft.Json;
namespace MyBot.Models.Returns;

/// <summary>
/// https://bot.q.qq.com/wiki/develop/api/openapi/message/get_message_of_id.html的返回值，实际情况与官方文档不符
/// </summary>
public class GetMessageResult
{
    /// <summary>
    /// 消息
    /// </summary>
    [JsonProperty("message")]
    public Message Message { get; set; }
}
