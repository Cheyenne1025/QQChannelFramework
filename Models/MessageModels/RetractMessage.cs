using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 撤回消息模型
/// </summary>
public class RetractMessage : Message
{
    /// <summary>
    /// 进行撤回操作的用户Id
    /// </summary>
    [JsonProperty("op_user")]
    public string OperationUserId { get; set; }  
}
