using System;
using QQChannelFramework.Datas;
using QQChannelFramework.Models.Types;

namespace QQChannelFramework.WS;

partial class FunctionWebSocket {
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
   public FunctionWebSocket RegisterGuildsEvent() {
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
   public FunctionWebSocket RegisterGuildMembersEvent() {
      _registeredEvents.Add(Intents.GuildMembers);

      return this;
   }

   /// <summary>
   /// 注册直接消息相关事件
   /// <para>包含: </para>
   /// <para>当收到用户发给机器人的私信消息时</para>
   /// </summary>
   /// <returns></returns> 
   public FunctionWebSocket RegisterDirectMessageEvent() {
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
   public FunctionWebSocket RegisterAudioActionEvent() {
      _registeredEvents.Add(Intents.AudioAction);

      return this;
   }

   /// <summary>
   /// 注册单聊/群聊事件 
   /// </summary>
   /// <returns></returns>
   public FunctionWebSocket RegisterChatEvent() {
      _registeredEvents.Add(Intents.Chat);

      return this;
   }

   /// <summary>
   /// 注册互动事件 
   /// </summary>
   /// <returns></returns>
   public FunctionWebSocket RegisterInteractionEvent() {
      _registeredEvents.Add(Intents.Interaction);

      return this;
   }

   /// <summary>
   /// 注册@机器人事件
   /// <para>当收到@机器人的消息时</para>
   /// </summary>
   /// <returns></returns>
   public FunctionWebSocket RegisterAtMessageEvent() {
      _registeredEvents.Add(Intents.AtMessage);

      return this;
   }

   /// <summary>
   /// 注册用户发送消息时间 (私域机器人可用)
   ///  <para>当收到用户发送的消息时 (免@机器人)</para>
   /// </summary>
   /// <returns></returns>
   public FunctionWebSocket RegisterUserMessageEvent() {
      if (CommonState.PrivateBot is false) {
         throw new Exceptions.BotNotIsPrivateException();
      }

      _registeredEvents.Add(Intents.UserMessage);

      return this;
   }

   /// <summary>
   /// 注册消息表情态相关事件
   /// <para>为消息添加表情表态</para>
   /// <para>为消息删除表情表态</para>
   /// </summary>
   /// <returns></returns>
   public FunctionWebSocket RegisterMessageReactionEvent() {
      _registeredEvents.Add(Intents.GuildMessageReactions);

      return this;
   }

   /// <summary>
   /// 注册主动消息审核相关事件
   /// <para>消息审核通过</para>
   /// <para>消息审核未通过</para>
   /// </summary>
   /// <returns></returns>
   public FunctionWebSocket RegisterAuditEvent() {
      _registeredEvents.Add(Intents.Audit);
      return this;
   }

   /// <summary>
   /// 注册论坛相关事件
   /// <para>当用户创建主题时</para>
   /// <para>当用户更新主题时</para>
   /// <para>当用户删除主题时</para>
   /// <para>当用户创建帖子时</para>
   /// <para>当用户删除帖子时</para>
   /// <para>当用户回复评论时</para>
   /// <para>当用户回复评论时</para>
   /// </summary>
   /// <returns></returns> 
   public FunctionWebSocket RegisterForumEvent() {
      if (CommonState.PrivateBot is false) {
         throw new Exceptions.BotNotIsPrivateException();
      }

      _registeredEvents.Add(Intents.Forum);

      return this;
   }
}
