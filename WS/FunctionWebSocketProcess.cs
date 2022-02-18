using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ChannelModels.Returns;
using ChannelModels.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Models.WsModels;

namespace QQChannelFramework.WS;

partial class FunctionWebSocket {
    /// <summary>
    /// 处理数据
    /// </summary>
    /// <param name="data"></param>
    private async void Process(JToken data) {
        var opCode = (OpCode) int.Parse(data["op"].ToString());

        switch (opCode) {
            case OpCode.Dispatch:

                var t = data["t"].ToString();

                _nowS = data["s"].ToString();

                switch (Enum.Parse<GuildEvents>(t)) {
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

                        BotAreAddedToTheGuild?.Invoke(new WsGuild() {
                            Id = data["d"]["id"].ToString(),
                            Description = data["d"]["description"].ToString(),
                            Name = data["d"]["name"].ToString(),
                            Icon = data["d"]["icon"].ToString(),
                            MaxMembers = int.Parse(data["d"]["max_members"].ToString()),
                            MemberCount = int.Parse(data["d"]["member_count"].ToString()),
                            OwnerId = data["d"]["owner_id"].ToString(),
                            Owner = false,
                            JoinedAt = Tools.ConvertHelper.GetDateTime(int.Parse(data["d"]["joined_at"].ToString())),
                            OperationUserId = data["d"]["op_user_id"].ToString()
                        });

                        break;

                    case GuildEvents.GUILD_UPDATE:

                        GuildInfoChange?.Invoke(new WsGuild() {
                            Id = data["d"]["id"].ToString(),
                            Description = data["d"]["description"].ToString(),
                            Name = data["d"]["name"].ToString(),
                            Icon = data["d"]["icon"].ToString(),
                            MaxMembers = int.Parse(data["d"]["max_members"].ToString()),
                            MemberCount = int.Parse(data["d"]["member_count"].ToString()),
                            OwnerId = data["d"]["owner_id"].ToString(),
                            Owner = false,
                            JoinedAt = Tools.ConvertHelper.GetDateTime(int.Parse(data["d"]["joined_at"].ToString())),
                            OperationUserId = data["d"]["op_user_id"].ToString()
                        });

                        break;

                    case GuildEvents.GUILD_DELETE:

                        BotBeRemoved?.Invoke(new WsGuild() {
                            Id = data["d"]["id"].ToString(),
                            Description = data["d"]["description"].ToString(),
                            Name = data["d"]["name"].ToString(),
                            Icon = data["d"]["icon"].ToString(),
                            MaxMembers = int.Parse(data["d"]["max_members"].ToString()),
                            MemberCount = int.Parse(data["d"]["member_count"].ToString()),
                            OwnerId = data["d"]["owner_id"].ToString(),
                            Owner = false,
                            JoinedAt = Tools.ConvertHelper.GetDateTime(int.Parse(data["d"]["joined_at"].ToString())),
                            OperationUserId = data["d"]["op_user_id"].ToString()
                        });

                        break;

                    case GuildEvents.CHANNEL_CREATE:

                        ChildChannelCreated?.Invoke(new WsChildChannel() {
                            Id = data["d"]["id"].ToString(),
                            GuildId = data["d"]["guild_id"].ToString(),
                            Name = data["d"]["name"].ToString(),
                            OwnerId = data["d"]["owner_id"].ToString(),
                            OperationUserId = data["d"]["op_user_id"].ToString(),
                            Type = Enum.Parse<ChildChannelType>(data["d"]["type"].ToString()),
                            SubType = Enum.Parse<ChildChannelSubType>(data["d"]["sub_type"].ToString()),
                        });

                        break;

                    case GuildEvents.CHANNEL_UPDATE:

                        ChildChannelInfoChange?.Invoke(new WsChildChannel() {
                            Id = data["d"]["id"].ToString(),
                            GuildId = data["d"]["guild_id"].ToString(),
                            Name = data["d"]["name"].ToString(),
                            OwnerId = data["d"]["owner_id"].ToString(),
                            OperationUserId = data["d"]["op_user_id"].ToString(),
                            Type = Enum.Parse<ChildChannelType>(data["d"]["type"].ToString()),
                            SubType = Enum.Parse<ChildChannelSubType>(data["d"]["sub_type"].ToString()),
                        });

                        break;

                    case GuildEvents.CHANNEL_DELETE:

                        ChildChannelBeRemoved?.Invoke(new WsChildChannel() {
                            Id = data["d"]["id"].ToString(),
                            GuildId = data["d"]["guild_id"].ToString(),
                            Name = data["d"]["name"].ToString(),
                            OwnerId = data["d"]["owner_id"].ToString(),
                            OperationUserId = data["d"]["op_user_id"].ToString(),
                            Type = Enum.Parse<ChildChannelType>(data["d"]["type"].ToString()),
                            SubType = Enum.Parse<ChildChannelSubType>(data["d"]["sub_type"].ToString()),
                        });

                        break;

                    case GuildEvents.GUILD_MEMBER_ADD:

                        var newMemberModel = new Models.MemberWithGuildID() {
                            GuildId = data["d"]["guild_id"].ToString(),
                            JoinedAt = null,
                            Nick = data["d"]["nick"].ToString(),
                            OperationUserId = data["d"]["op_user_id"].ToString(),
                            User = new() {
                                IsBot = bool.Parse(data["d"]["user"]["bot"].ToString()),
                                Id = data["d"]["user"]["id"].ToString(),
                                UserName = data["d"]["user"]["username"].ToString()
                            },
                            Roles = new()
                        };

                        foreach (var role in JArray.Parse(data["d"]["roles"].ToString())) {
                            newMemberModel.Roles.Add(role.ToString());
                        }

                        NewMemberJoin?.Invoke(newMemberModel);

                        break;

                    case GuildEvents.GUILD_MEMBER_UPDATE:

                        var updateMemberModel = new Models.MemberWithGuildID() {
                            GuildId = data["d"]["guild_id"].ToString(),
                            JoinedAt = null,
                            Nick = data["d"]["nick"].ToString(),
                            OperationUserId = data["d"]["op_user_id"].ToString(),
                            User = new() {
                                IsBot = bool.Parse(data["d"]["user"]["bot"].ToString()),
                                Id = data["d"]["user"]["id"].ToString(),
                                UserName = data["d"]["user"]["username"].ToString()
                            },
                            Roles = new()
                        };

                        foreach (var role in JArray.Parse(data["d"]["roles"].ToString())) {
                            updateMemberModel.Roles.Add(role.ToString());
                        }

                        MemberInfoChange?.Invoke(updateMemberModel);

                        break;

                    case GuildEvents.GUILD_MEMBER_REMOVE:

                        var deleteMemberModel = new Models.MemberWithGuildID() {
                            GuildId = data["d"]["guild_id"].ToString(),
                            JoinedAt = null,
                            Nick = data["d"]["nick"].ToString(),
                            OperationUserId = data["d"]["op_user_id"].ToString(),
                            User = new() {
                                IsBot = bool.Parse(data["d"]["user"]["bot"].ToString()),
                                Id = data["d"]["user"]["id"].ToString(),
                                UserName = data["d"]["user"]["username"].ToString()
                            },
                            Roles = new()
                        };

                        foreach (var role in JArray.Parse(data["d"]["roles"].ToString())) {
                            deleteMemberModel.Roles.Add(role.ToString());
                        }

                        MemberExistGuild?.Invoke(deleteMemberModel);

                        break;

                    case GuildEvents.AT_MESSAGE_CREATE:

                        ReceivedAtMessage?.Invoke(data["d"].ToObject<Models.MessageModels.Message>());

                        break;

                    case GuildEvents.MESSAGE_CREATE:

                        ReceivedUserMessage?.Invoke(data["d"].ToObject<Models.MessageModels.Message>());

                        break;
                    
                    case GuildEvents.DIRECT_MESSAGE_CREATE:

                        ReceivedDirectMessage?.Invoke(data["d"].ToObject<Models.MessageModels.Message>());

                        break;

                    case GuildEvents.MESSAGE_REACTION_ADD:

                        MessageReactionIsAdded?.Invoke(data["d"].ToObject<Models.MessageModels.MessageReaction>());

                        break;

                    case GuildEvents.MESSAGE_REACTION_REMOVE:

                        MessageReactionIsRemoved?.Invoke(data["d"].ToObject<Models.MessageModels.MessageReaction>());

                        break;
                    
                    case GuildEvents.MESSAGE_AUDIT_PASS:  
                        MessageAuditPass?.Invoke(data["d"].ToObject<MessageAudited>()); 
                        break;
                    
                    case GuildEvents.MESSAGE_AUDIT_REJECT: 
                        MessageAuditReject?.Invoke(data["d"].ToObject<MessageAudited>());
                        break;
                }

                OnDispatch?.Invoke(data);

                break;

            case OpCode.Hello:

                heartbeatTimer.Interval = int.Parse(data["d"]["heartbeat_interval"].ToString());
                heartbeatTimer.Start();

                _identifyData.intents = 0;

                foreach (var type in _registeredEvents) {
                    _identifyData.intents += (int) type;
                }

                Load load = new();
                load.op = (int) OpCode.Identify;
                load.d = _identifyData;

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