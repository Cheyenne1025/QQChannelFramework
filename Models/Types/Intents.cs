using System;
namespace QQChannelFramework.Models.Types
{
    /// <summary>
    /// 频道Intents
    /// </summary>
    public enum Intents : int
    {
        /// <summary>
        /// 频道类相关事件
        /// <para>包含: </para>
        /// <para>当机器人加入新频道时</para>
        /// <para>当频道资料发生变更时</para>
        /// <para>当机器人退出频道时</para>
        /// <para>当子频道被创建时</para>
        /// <para>当子频道被更新时</para>
        /// <para>当子频道被删除时</para>
        /// </summary>
        Guilds = 1 << 0,
        /// <summary>
        /// 频道成员相关事件
        /// <para>包含: </para>
        /// <para>当成员加入时</para>
        /// <para>当成员资料变更时</para>
        /// <para>当成员被移除时</para>
        /// </summary>
        GuildMembers = 1 << 1,
        /// <summary>
        /// 私聊事件
        /// <para>当收到用户发给机器人的私信消息时</para>
        /// </summary>
        DirectMessage = 1 << 12,
        /// <summary>
        /// 音频类相关事件
        /// <para>包含: </para>
        /// <para>音频开始播放时</para>
        /// <para>音频播放结束时</para>
        /// <para>上麦时</para>
        /// <para>下麦时</para>
        /// </summary>
        AudioAction = 1 << 29,
        /// <summary>
        /// @机器人事件
        /// <para>当收到@机器人的消息时</para>
        /// </summary>
        AtMessage = 1 << 30
    }
}

