using System;
using QQChannelFramework.Models.Types;

namespace QQChannelFramework.WS
{
    sealed partial class FunctionWebSocket
    {
        /// <summary>
        /// 注册频道相关事件
        /// <para>包含: </para>
        /// <para>当机器人加入新频道时</para>
        /// <para>当频道资料发生变更时</para>
        /// <para>当机器人退出频道时</para>
        /// <para>当子频道被创建时</para>
        /// <para>当子频道被更新时</para>
        /// <para>当子频道被删除时</para>
        /// </summary>
        /// <returns></returns>
        public FunctionWebSocket RegisterGuildsEvent()
        {
            _registeredEvents.Add(Intents.Guilds);

            return this;
        }

        /// <summary>
        /// 注册频道成员相关事件
        /// <para>包含: </para>
        /// <para>当成员加入时</para>
        /// <para>当成员资料变更时</para>
        /// <para>当成员被移除时</para>
        /// </summary>
        /// <returns></returns> 
        public FunctionWebSocket RegisterGuildMembersEvent()
        {
            _registeredEvents.Add(Intents.GuildMembers);

            return this;
        }

        /// <summary>
        /// 注册直接消息相关事件
        /// <para>包含: </para>
        /// <para>当收到用户发给机器人的私信消息时</para>
        /// </summary>
        /// <returns></returns>
        [Obsolete("官方暂未开放使用", true)]
        public FunctionWebSocket RegisterDirectMessageEvent()
        {
            throw new Exception("无法注册直接消息相关事件，官方正在规划开发该功能");

            _registeredEvents.Add(Intents.DirectMessage);

            return this;
        }

        /// <summary>
        /// 注册音频类相关事件
        /// <para>包含: </para>
        /// <para>音频开始播放时</para>
        /// <para>音频播放结束时</para>
        /// <para>上麦时</para>
        /// <para>下麦时</para>
        /// </summary>
        /// <returns></returns>
        public FunctionWebSocket RegisterAudioActionEvent()
        {
            _registeredEvents.Add(Intents.AudioAction);

            return this;
        }

        /// <summary>
        /// 注册@机器人事件
        /// <para>当收到@机器人的消息时</para>
        /// </summary>
        /// <returns></returns>
        public FunctionWebSocket RegisterAtMessageEvent()
        {
            _registeredEvents.Add(Intents.AtMessage);

            return this;
        }
    }
}

