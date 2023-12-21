namespace MyBot.Exceptions;

/// <summary>
/// 主动消息触发审核时返回的异常
/// </summary>
public class MessageAuditException : Exception
{
    /// <summary>
    /// 响应中的code
    /// </summary>
    public int Code { get; private set; }

    /// <summary>
    /// 审核ID
    /// </summary>
    public string AuditId { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="auditId"></param>
    public MessageAuditException(int code, string message, string auditId) : base(message)
    {
        Code = code;
        AuditId = auditId;
    }
}