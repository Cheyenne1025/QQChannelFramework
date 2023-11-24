using System;
using ChannelModels.Returns;
using QQChannelFramework.Models;
using QQChannelFramework.Models.AudioModels;
using QQChannelFramework.Models.Forum;
using QQChannelFramework.Models.MessageModels;
using QQChannelFramework.Models.WsModels;

namespace QQChannelFramework.WS;

partial class FunctionWebSocket {
   public delegate void GuildDelegate(WsGuild guild);
   public delegate void ChannelDelegate(WsChannel channel);
   public delegate void MemberDelegate(MemberWithGuildID memberWithGuildID);
   public delegate void MessageDelegate(Message message);
   public delegate void RetractMessageDelegate(RetractMessage retractMessage);
   public delegate void MessageReactionDelegate(MessageReaction messageReactionInfo);
   public delegate void AuditDelegate(MessageAudited audit);
   public delegate void AudioDelegate(AudioAction audioAction);
   public delegate void ForumThreadDelegate(Thread thread);
   public delegate void ForumPostDelegate(Post post);
   public delegate void ForumReplyDelegate(Reply reply);
   public delegate void FourmAuditDelegate(AuditResult auditResult);
   public delegate void ChatMessageDelegate(ChatMessage message);

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>随消息到达的EventId，可用作发送消被动息</para>
   /// </summary>
   public event Action<string> OnEventId;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>机器人被加入到某个频道的时候</para>
   /// </summary>
   public event GuildDelegate BotAreAddedToTheGuild;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>频道信息变更</para>
   /// </summary>
   public event GuildDelegate GuildInfoChange;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>频道被解散</para>
   /// <para>机器人被移除</para>
   /// </summary>
   public event GuildDelegate BotBeRemoved;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>子频道被创建</para>
   /// </summary>
   public event ChannelDelegate ChannelCreated;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>子频道信息变更</para>
   /// </summary>
   public event ChannelDelegate ChannelInfoChange;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>子频道被删除</para>
   /// </summary>
   public event ChannelDelegate ChannelBeRemoved;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>新成员加入频道</para>
   /// </summary>
   public event MemberDelegate NewMemberJoin;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>成员信息变更</para>
   /// </summary>
   public event MemberDelegate MemberInfoChange;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>成员退出频道</para>
   /// </summary>
   public event MemberDelegate MemberLeaveGuild;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>用户发送消息，并且@当前机器人</para>
   /// </summary>
   public event MessageDelegate ReceivedAtMessage;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>用户发送消息时 (仅私域机器人可用)</para>
   /// </summary>
   public event MessageDelegate ReceivedUserMessage;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>用户私聊消息时</para>
   /// </summary>
   public event MessageDelegate ReceivedDirectMessage;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>收到事件消息时</para>
   /// </summary>
   public event ReceiveDelegate OnDispatch;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>心跳发送成功时</para>
   /// </summary>
   public event NormalDelegate HeartbeatSendSuccess;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>心跳中断</para>
   /// </summary>
   public event NormalDelegate HeartbeatBreak;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>鉴权失败时</para>
   /// </summary>
   public event NormalDelegate AuthenticationError;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>鉴权成功时</para>
   /// </summary>
   public event NormalDelegate AuthenticationSuccess;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>正在重连</para>
   /// </summary>
   public event NormalDelegate Reconnecting;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>重连事件补发完毕时</para>
   /// </summary>
   [Obsolete("该事件暂时停用", true)]
   public event NormalDelegate Resumed;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>消息被用户添加表情态</para>
   /// </summary>
   public event MessageReactionDelegate MessageReactionIsAdded;
   /// <summary>
   /// <para>触发时机: </para>
   /// <para>消息表情态被用户取消(移除)</para>
   /// </summary>
   public event MessageReactionDelegate MessageReactionIsRemoved;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>消息审核通过</para>
   /// </summary>
   public event AuditDelegate MessageAuditPass;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>消息审核不通过</para>
   /// </summary>
   public event AuditDelegate MessageAuditReject;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>音频开始播放时</para>
   /// </summary>
   public event AudioDelegate AudioStart;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>音频播放结束时</para>
   /// </summary>
   public event AudioDelegate AudioFinish;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>机器人上麦时</para>
   /// </summary>
   public event AudioDelegate BotTopMic;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>机器人下麦时</para>
   /// </summary>
   public event AudioDelegate BotOffMic;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>论坛新主题创建时</para>
   /// </summary>
   public event ForumThreadDelegate ForumThreadAreCreated;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>论坛主题被更新后</para>
   /// </summary>
   public event ForumThreadDelegate ForumThreadWasUpdated;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>论坛主题被删除后</para>
   /// </summary>
   public event ForumThreadDelegate ForumThreadDeleted;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>论坛主题收到评论</para>
   /// </summary>
   public event ForumPostDelegate ForumPostAreCreated;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>论坛主题的评论被删除</para>
   /// </summary>
   public event ForumPostDelegate ForumPostDeleted;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>论坛主题的评论的收到回复</para>
   /// </summary>
   public event ForumReplyDelegate ForumReplyAreCreated;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>论坛主题的评论的收到的回复被删除</para>
   /// </summary>
   public event ForumReplyDelegate ForumReplyDeleted;

   /// <summary>
   /// <para>触发时机: </para>
   /// <para>收到论坛主题发布的审核结果时</para>
   /// </summary>
   public event FourmAuditDelegate ForumPublishAuditResultReceived;

   /// <summary>
   /// <para>触发时机: </para>
   /// 用户撤回消息时
   /// </summary>
   public event RetractMessageDelegate UserRetractMessage;
   /// <summary>
   /// 群聊消息
   /// </summary>
   public event ChatMessageDelegate ReceivedChatGroupMessage;
   /// <summary>
   /// 单聊消息
   /// </summary>
   public event ChatMessageDelegate ReceivedChatUserMessage;
}
