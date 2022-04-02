using Newtonsoft.Json;

namespace QQChannelFramework.Models.MessageModels;

/// <summary>
/// 撤回消息模型
/// </summary>
public class RetractMessage
{
    /// <summary>
    /// 撤回的消息
    /// </summary>
    [JsonProperty("message")]
    public Message Message { get; set; }

    /// <summary>
    /// 进行撤回操作的用户
    /// </summary>
    [JsonProperty("op_user")]
    public RetractOperationUser OperationUser { get; set; }
}

public class RetractOperationUser
{
    /// <summary>
    /// 用户Id
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }
}
