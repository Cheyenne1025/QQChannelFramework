namespace QQChannelFramework.Models.Types;

/// <summary>
/// 频道Intents
/// </summary>
public enum Intents : int {
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
   /// 用户发送消息 (私域机器人可用)
   /// </summary>
   UserMessage = 1 << 9,
   /// <summary>
   /// 消息表情态事件
   /// <para>包含: </para>
   /// <para>为消息添加表情表态</para>
   /// <para>为消息删除表情表态</para>
   /// </summary>
   GuildMessageReactions = 1 << 10,
   /// <summary>
   /// 私聊事件
   /// <para>当收到用户发给机器人的私信消息时</para>
   /// </summary>
   DirectMessage = 1 << 12,
   /// <summary>
   /// 单聊/群聊
   /// <para>包含: </para>
   /// <para>单聊消息</para> 
   /// </summary>
   GroupAndCustomer = 1 << 25,
   /// <summary>
   /// 互动事件
   /// <para>包含: </para>
   /// <para>互动创建</para> 
   /// </summary>
   Interaction = 1 << 26,
   /// <summary>
   /// 审核事件
   /// <para>包含: </para>
   /// <para>消息审核通过</para>
   /// <para>消息审核不通过</para> 
   /// </summary>
   Audit = 1 << 27,
   /// <summary>
   /// 论坛事件
   /// <para>包含: </para>
   /// <para>当用户创建主题时</para>
   /// <para>当用户更新主题时</para>
   /// <para>当用户删除主题时</para>
   /// <para>当用户创建帖子时</para>
   /// <para>当用户删除帖子时</para>
   /// <para>当用户回复评论时</para>
   /// <para>当用户删除评论时</para>
   /// </summary>
   Forum = 1 << 28,
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
   AtMessage = 1 << 30,
}
