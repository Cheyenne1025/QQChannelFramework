using System.Linq;
using ChannelModels.Returns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Models;
using QQChannelFramework.Models.AudioModels;
using QQChannelFramework.Models.Forum;
using QQChannelFramework.Models.MessageModels;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Models.WsModels;
using QQChannelFramework.Tools;
namespace QQChannelFramework.WS;

partial class FunctionWebSocket {
   /// <summary>
   /// 处理数据
   /// </summary>
   /// <param name="data"></param>
   private async void Process(JObject data) {
      var opCode = (OpCode)int.Parse(data["op"].ToString());

      switch (opCode) {
         case OpCode.Dispatch:

            var t = data["t"].ToString();
            _nowS = data["s"].ToString();

            if (data.ContainsKey("id")) {
               var id = data["id"].Value<string>();
               OnEventId?.Invoke(id);
            }

            if (Enum.TryParse<GuildEvents>(t, out var e)) {
               switch (e) {
                  case GuildEvents.READY:

                     _sessionInfo.Version = data["d"]["version"].ToString();
                     _sessionInfo.SessionId = data["d"]["session_id"].ToString();
                     _sessionInfo.BotId = data["d"]["user"]["id"].ToString();
                     _sessionInfo.Name = data["d"]["user"]["username"].ToString();

                     AuthenticationSuccess?.Invoke();

                     break;

                  case GuildEvents.RESUMED:

                     //Resumed?.Invoke();

                     break;

                  case GuildEvents.GUILD_CREATE:

                     BotAreAddedToTheGuild?.Invoke(data["d"].ToObject<WsGuild>());

                     break;

                  case GuildEvents.GUILD_UPDATE:

                     GuildInfoChange?.Invoke(data["d"].ToObject<WsGuild>());

                     break;

                  case GuildEvents.GUILD_DELETE:

                     BotBeRemoved?.Invoke(data["d"].ToObject<WsGuild>());

                     break;

                  case GuildEvents.CHANNEL_CREATE:

                     ChannelCreated?.Invoke(data["d"].ToObject<WsChannel>());

                     break;

                  case GuildEvents.CHANNEL_UPDATE:

                     ChannelInfoChange?.Invoke(data["d"].ToObject<WsChannel>());

                     break;

                  case GuildEvents.CHANNEL_DELETE:

                     ChannelBeRemoved?.Invoke(data["d"].ToObject<WsChannel>());

                     break;

                  case GuildEvents.GUILD_MEMBER_ADD:

                     NewMemberJoin?.Invoke(data["d"].ToObject<MemberWithGuildID>());

                     break;

                  case GuildEvents.GUILD_MEMBER_UPDATE:

                     MemberInfoChange?.Invoke(data["d"].ToObject<MemberWithGuildID>());

                     break;

                  case GuildEvents.GUILD_MEMBER_REMOVE:

                     MemberLeaveGuild?.Invoke(data["d"].ToObject<MemberWithGuildID>());

                     break;

                  case GuildEvents.AT_MESSAGE_CREATE:

                     ReceivedAtMessage?.Invoke(data["d"].ToObject<Message>());

                     break;

                  case GuildEvents.MESSAGE_CREATE:

                     ReceivedUserMessage?.Invoke(data["d"].ToObject<Message>());

                     break;

                  case GuildEvents.MESSAGE_DELETE:
                     UserRetractMessage?.Invoke(data["d"].ToObject<RetractMessage>());
                     break;

                  case GuildEvents.PUBLIC_MESSAGE_DELETE:
                     UserRetractMessage?.Invoke(data["d"].ToObject<RetractMessage>());
                     break;

                  case GuildEvents.DIRECT_MESSAGE_CREATE:

                     ReceivedDirectMessage?.Invoke(data["d"].ToObject<Message>());

                     break;

                  case GuildEvents.MESSAGE_REACTION_ADD:

                     MessageReactionIsAdded?.Invoke(data["d"].ToObject<MessageReaction>());

                     break;

                  case GuildEvents.MESSAGE_REACTION_REMOVE:

                     MessageReactionIsRemoved?.Invoke(data["d"]
                        .ToObject<MessageReaction>());

                     break;

                  case GuildEvents.MESSAGE_AUDIT_PASS:
                     MessageAuditPass?.Invoke(data["d"].ToObject<MessageAudited>());
                     break;

                  case GuildEvents.MESSAGE_AUDIT_REJECT:
                     MessageAuditReject?.Invoke(data["d"].ToObject<MessageAudited>());
                     break;

                  case GuildEvents.AUDIO_START:
                     AudioStart?.Invoke(data["d"].ToObject<AudioAction>());
                     break;

                  case GuildEvents.AUDIO_FINISH:
                     AudioFinish?.Invoke(data["d"].ToObject<AudioAction>());
                     break;

                  case GuildEvents.AUDIO_ON_MIC:
                     BotTopMic?.Invoke(data["d"].ToObject<AudioAction>());
                     break;

                  case GuildEvents.AUDIO_OFF_MIC:
                     BotOffMic?.Invoke(data["d"].ToObject<AudioAction>());
                     break;

                  case GuildEvents.FORUM_THREAD_CREATE:
                     ForumThreadAreCreated?.Invoke(data["d"].ToObject<Thread>());
                     break;

                  case GuildEvents.FORUM_THREAD_UPDATE:
                     ForumThreadWasUpdated?.Invoke(data["d"].ToObject<Thread>());
                     break;

                  case GuildEvents.FORUM_THREAD_DELETE:
                     ForumThreadDeleted?.Invoke(data["d"].ToObject<Thread>());
                     break;

                  case GuildEvents.FORUM_POST_CREATE:
                     ForumPostAreCreated?.Invoke(data["d"].ToObject<Post>());
                     break;

                  case GuildEvents.FORUM_POST_DELETE:
                     ForumPostDeleted?.Invoke(data["d"].ToObject<Post>());
                     break;

                  case GuildEvents.FORUM_REPLY_CREATE:
                     ForumReplyAreCreated?.Invoke(data["d"].ToObject<Reply>());
                     break;

                  case GuildEvents.FORUM_REPLY_DELETE:
                     ForumReplyDeleted?.Invoke(data["d"].ToObject<Reply>());
                     break;

                  case GuildEvents.FORUM_PUBLISH_AUDIT_RESULT:
                     ForumPublishAuditResultReceived?.Invoke(data["d"].ToObject<AuditResult>());
                     break;

                  case GuildEvents.GROUP_AT_MESSAGE_CREATE:
                     ReceivedChatGroupMessage?.Invoke(data["d"].ToObject<ChatMessage>());
                     break;

                  case GuildEvents.C2C_MESSAGE_CREATE:
                     ReceivedChatUserMessage?.Invoke(data["d"].ToObject<ChatMessage>());
                     break;

                  case GuildEvents.GROUP_ADD_ROBOT:
                     ChatGroupAdded?.Invoke(data["d"].ToObject<ChatMessageGroupEvent>());
                     break;
                  case GuildEvents.GROUP_DEL_ROBOT:
                     ChatGroupRemoved?.Invoke(data["d"].ToObject<ChatMessageGroupEvent>());
                     break;
                  case GuildEvents.GROUP_MSG_REJECT:
                     ChatGroupMessageReject?.Invoke(data["d"].ToObject<ChatMessageGroupEvent>());
                     break;
                  case GuildEvents.GROUP_MSG_RECEIVE:
                     ChatGroupMessageReceive?.Invoke(data["d"].ToObject<ChatMessageGroupEvent>());
                     break;

                  case GuildEvents.FRIEND_ADD:
                     ChatUserAdded?.Invoke(data["d"].ToObject<ChatMessageUserEvent>());
                     break;
                  case GuildEvents.FRIEND_DEL:
                     ChatUserRemoved?.Invoke(data["d"].ToObject<ChatMessageUserEvent>());
                     break;
                  case GuildEvents.C2C_MSG_REJECT:
                     ChatUserMessageReject?.Invoke(data["d"].ToObject<ChatMessageUserEvent>());
                     break;
                  case GuildEvents.C2C_MSG_RECEIVE:
                     ChatUserMessageReceive?.Invoke(data["d"].ToObject<ChatMessageUserEvent>());
                     break;
               }
            } else {
               BotLog.Log($"未知事件 {t} {data}");
            }

            OnDispatch?.Invoke(data);

            break;

         case OpCode.Hello:

            heartbeatTimer.Interval = int.Parse(data["d"]!["heartbeat_interval"]!.ToString());
            heartbeatTimer.Start();

            // 目前用新的令牌鉴权会失败
            var identifyData = new IdentifyData {
               intents = 0,
               token = $"QQBot {await _openApiAccessInfo.GetAuthorization()}",
               shard = _shard
            };

            if (!_registeredEvents.Any()) //需要至少订阅一个事件才能正常启动
            {
               //这里应该给用户一点提示的 但是没找到在哪发
               AuthenticationError?.Invoke();
               await CloseAsync();
               break;
            }
            foreach (var type in _registeredEvents) {
               identifyData.intents += (int)type;
            }

            Load load = new() {
               op = (int)OpCode.Identify,
               d = identifyData
            };

            SendAsync(JsonConvert.SerializeObject(load));

            break;

         case OpCode.InvalidSession:

            AuthenticationError?.Invoke();

            await CloseAsync();

            break;

         case OpCode.HeartbeatACK:

            _heartbeating = true;

            HeartbeatSendSuccess?.Invoke();

            break;

         case OpCode.Reconnect:

            Reconnecting?.Invoke();

            await CloseAsync();
            await ConnectAsync(_url);

            break;

         case OpCode.Heartbeat:

            // 官方还未启用

            break;
      }
   }
}
