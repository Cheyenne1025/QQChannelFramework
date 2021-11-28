namespace QQChannelFramework.Models.Types;

/// <summary>
/// 频道事件枚举
/// </summary>
public enum GuildEvents
{
    /// <summary>
    /// 就绪
    /// </summary>
    READY,
    /// <summary>
    /// 当机器人加入新频道时
    /// </summary>
    GUILD_CREATE,
    /// <summary>
    /// 当主频道资料发生变更时
    /// </summary>
    GUILD_UPDATE,
    /// <summary>
    /// 当机器人退出频道时
    /// </summary>
    GUILD_DELETE,
    /// <summary>
    /// 当子频道被创建时
    /// </summary>
    CHANNEL_CREATE,
    /// <summary>
    /// 当子频道信息被更新时
    /// </summary>
    CHANNEL_UPDATE,
    /// <summary>
    /// 当子频道被删除时
    /// </summary>
    CHANNEL_DELETE,
    /// <summary>
    /// 当成员加入时
    /// </summary>
    GUILD_MEMBER_ADD,
    /// <summary>
    /// 当成员资料变更时
    /// </summary>
    GUILD_MEMBER_UPDATE,
    /// <summary>
    /// 当成员被移除时
    /// </summary>
    GUILD_MEMBER_REMOVE,
    /// <summary>
    /// 当收到用户发给机器人的私信消息时
    /// </summary>
    DIRECT_MESSAGE_CREATE,
    /// <summary>
    /// 音频开始播放时
    /// </summary>
    AUDIO_START,
    /// <summary>
    /// 音频播放结束时
    /// </summary>
    AUDIO_FINISH,
    /// <summary>
    /// 上麦时
    /// </summary>
    AUDIO_ON_MIC,
    /// <summary>
    /// 下麦时
    /// </summary>
    AUDIO_OFF_MIC,
    /// <summary>
    /// 当收到@机器人的消息时
    /// </summary>
    AT_MESSAGE_CREATE,
    /// <summary>
    /// 重连事件补发完毕
    /// </summary>
    RESUMED
}