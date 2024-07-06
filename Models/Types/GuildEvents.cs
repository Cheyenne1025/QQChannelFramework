namespace MyBot.Models.Types;

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
    /// 公域消息撤回
    /// </summary>
    PUBLIC_MESSAGE_DELETE,
    /// <summary>
    /// 重连事件补发完毕
    /// </summary>
    RESUMED,
    /// <summary>
    /// 用户发送消息
    /// </summary>
    MESSAGE_CREATE,
    /// <summary>
    /// 消息被撤回
    /// </summary>
    MESSAGE_DELETE,
    /// <summary>
    /// 为消息添加表情表态
    /// </summary>
    MESSAGE_REACTION_ADD,
    /// <summary>
    /// 为消息删除表情表态
    /// </summary>
    MESSAGE_REACTION_REMOVE,
    /// <summary>
    /// 当用户创建主题时
    /// </summary>
    FORUM_THREAD_CREATE,
    /// <summary>
    /// 当用户更新主题时
    /// </summary>
    FORUM_THREAD_UPDATE,
    /// <summary>
    /// 当用户删除主题时
    /// </summary>
    FORUM_THREAD_DELETE,
    /// <summary>
    /// 当用户创建帖子时
    /// </summary>
    FORUM_POST_CREATE,
    /// <summary>
    /// 当用户删除帖子时
    /// </summary>
    FORUM_POST_DELETE,
    /// <summary>
    /// 当用户对评论回复时
    /// </summary>
    FORUM_REPLY_CREATE,
    /// <summary>
    /// 当用户删除对评论的回复时
    /// </summary>
    FORUM_REPLY_DELETE,
    /// <summary>
    /// 消息审核通过
    /// </summary>
    MESSAGE_AUDIT_PASS,
    /// <summary>
    /// 消息审核不通过
    /// </summary>
    MESSAGE_AUDIT_REJECT,
    /// <summary>
    /// 帖子审核事件
    /// </summary>
    FORUM_PUBLISH_AUDIT_RESULT,
    /// <summary>
    /// 机器人被添加到群聊
    /// </summary>
    GROUP_ADD_ROBOT,
    /// <summary>
    /// 机器人被移出群聊
    /// </summary>
    GROUP_DEL_ROBOT,
    /// <summary>
    /// 群管理员主动在机器人资料页操作关闭通知
    /// </summary>
    GROUP_MSG_REJECT,
    /// <summary>
    /// 群管理员主动在机器人资料页操作开启通知
    /// </summary>
    GROUP_MSG_RECEIVE,
    /// <summary>
    /// 机器人被添加好友
    /// </summary>
    FRIEND_ADD,
    /// <summary>
    /// 机器人被移除好友
    /// </summary>
    FRIEND_DEL,
    /// <summary>
    /// 机器人被拒收消息
    /// </summary>
    C2C_MSG_REJECT,
    /// <summary>
    /// 机器人被接收消息
    /// </summary>
    C2C_MSG_RECEIVE,
    /// <summary>
    /// 私聊
    /// </summary>
    C2C_MESSAGE_CREATE,
    /// <summary>
    /// 群聊被@
    /// </summary>
    GROUP_AT_MESSAGE_CREATE,
    /// <summary>
    /// 主动订阅
    /// </summary>
    SUBSCRIBE_MESSAGE_STATUS
}