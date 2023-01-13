namespace MyBot.Api.Types;

/// <summary>
/// 参数标识
/// </summary>
public enum ParamType
{
    /// <summary>
    /// 频道GuildId
    /// </summary>
    guild_id,
    /// <summary>
    /// 成员Id
    /// </summary>
    user_id,
    /// <summary>
    /// 身份组Id
    /// </summary>
    role_id,
    /// <summary>
    /// 子频道Id
    /// </summary>
    channel_id,
    /// <summary>
    /// 消息Id
    /// </summary>
    message_id,
    /// <summary>
    /// 日程Id
    /// </summary>
    schedule_id, 
    type,
    id,
    /// <summary>
    /// 帖子Id
    /// </summary>
    thread_id,
}