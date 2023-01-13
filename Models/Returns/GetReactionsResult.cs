using Newtonsoft.Json;

namespace MyBot.Models.Returns;

/// <summary>
/// 拉取消息指定表情表态的用户列表
/// </summary>
public class GetReactionsResult
{
    /// <summary>
    /// 用户对象，参考 User，会返回 id, username, avatar
    /// </summary>
    [JsonProperty("users")]
    public User[] Users { get; set; }

    /// <summary>
    /// 分页参数，用于拉取下一页
    /// </summary>
    [JsonProperty("cookie")]
    public string Cookie { get; set; }

    /// <summary>
    /// 是否已拉取完成到最后一页，true代表完成
    /// </summary>
    [JsonProperty("is_end")]
    public bool IsEnd { get; set; }
}