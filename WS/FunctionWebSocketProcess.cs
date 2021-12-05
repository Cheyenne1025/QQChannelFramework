using System;
using ChannelModels.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Models.WsModels;

namespace QQChannelFramework.WS;

partial class FunctionWebSocket
{
    /// <summary>
    /// 处理数据
    /// </summary>
    /// <param name="data"></param>
    private void Process(JToken data)
    {
        var opCode = (OpCode)int.Parse(data["op"].ToString());

        switch (opCode)
        {
            case OpCode.Dispatch:

                var t = data["t"].ToString();

                _nowS = data["s"].ToString();

                switch (Enum.Parse<GuildEvents>(t))
                {
                    case GuildEvents.READY:

                        _sessionInfo.Version = data["d"]["version"].ToString();
                        _sessionInfo.SessionId = data["d"]["session_id"].ToString();
                        _sessionInfo.BotId = data["d"]["user"]["id"].ToString();
                        _sessionInfo.Name = data["d"]["user"]["username"].ToString();

                        AuthenticationSuccess?.Invoke();

                        break;

                    case GuildEvents.RESUMED:

                        Resumed?.Invoke();

                        break;

                    case GuildEvents.GUILD_CREATE:

                        BotAreAddedToTheGuild?.Invoke(new WsGuild()
                        {
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

                        GuildInfoChange?.Invoke(new WsGuild()
                        {
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

                        BotBeRemoved?.Invoke(new WsGuild()
                        {
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

                        ChildChannelCreated?.Invoke(new WsChildChannel()
                        {
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

                        ChildChannelInfoChange?.Invoke(new WsChildChannel()
                        {
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

                        ChildChannelBeRemoved?.Invoke(new WsChildChannel()
                        {
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

                        var newMemberModel = new Models.MemberWithGuildID()
                        {
                            GuildId = data["d"]["guild_id"].ToString(),
                            JoinedAt = null,
                            Nick = data["d"]["nick"].ToString(),
                            OperationUserId = data["d"]["op_user_id"].ToString(),
                            User = new()
                            {
                                IsBot = bool.Parse(data["d"]["user"]["bot"].ToString()),
                                Id = data["d"]["user"]["id"].ToString(),
                                UserName = data["d"]["user"]["username"].ToString()
                            },
                            Roles = new()
                        };

                        foreach (var role in JArray.Parse(data["d"]["roles"].ToString()))
                        {
                            newMemberModel.Roles.Add(role.ToString());
                        }

                        NewMemberJoin?.Invoke(newMemberModel);

                        break;

                    case GuildEvents.GUILD_MEMBER_UPDATE:

                        var updateMemberModel = new Models.MemberWithGuildID()
                        {
                            GuildId = data["d"]["guild_id"].ToString(),
                            JoinedAt = null,
                            Nick = data["d"]["nick"].ToString(),
                            OperationUserId = data["d"]["op_user_id"].ToString(),
                            User = new()
                            {
                                IsBot = bool.Parse(data["d"]["user"]["bot"].ToString()),
                                Id = data["d"]["user"]["id"].ToString(),
                                UserName = data["d"]["user"]["username"].ToString()
                            },
                            Roles = new()
                        };

                        foreach (var role in JArray.Parse(data["d"]["roles"].ToString()))
                        {
                            updateMemberModel.Roles.Add(role.ToString());
                        }

                        MemberInfoChange?.Invoke(updateMemberModel);

                        break;

                    case GuildEvents.GUILD_MEMBER_REMOVE:

                        var deleteMemberModel = new Models.MemberWithGuildID()
                        {
                            GuildId = data["d"]["guild_id"].ToString(),
                            JoinedAt = null,
                            Nick = data["d"]["nick"].ToString(),
                            OperationUserId = data["d"]["op_user_id"].ToString(),
                            User = new()
                            {
                                IsBot = bool.Parse(data["d"]["user"]["bot"].ToString()),
                                Id = data["d"]["user"]["id"].ToString(),
                                UserName = data["d"]["user"]["username"].ToString()
                            },
                            Roles = new()
                        };

                        foreach (var role in JArray.Parse(data["d"]["roles"].ToString()))
                        {
                            deleteMemberModel.Roles.Add(role.ToString());
                        }

                        MemberExistGuild?.Invoke(deleteMemberModel);

                        break;

                    case GuildEvents.AT_MESSAGE_CREATE:

                        var messageModel = new Models.MessageModels.Message()
                        {
                            Id = data["d"]["id"].ToString(),
                            ChildChannelId = data["d"]["channel_id"].ToString(),
                            Content = data["d"]["content"].ToString(),
                            GuildId = data["d"]["guild_id"].ToString(),
                            Author = new()
                            {
                                Avatar = data["d"]["author"]["avatar"].ToString(),
                                IsBot = bool.Parse(data["d"]["author"]["bot"].ToString()),
                                Id = data["d"]["author"]["id"].ToString(),
                                UserName = data["d"]["author"]["username"].ToString()
                            },
                            Member = new()
                            {
                                JoinedAt = DateTime.Parse(data["d"]["member"]["joined_at"].ToString()),
                                Roles = new()
                            },
                            Time = DateTime.Parse(data["d"]["timestamp"].ToString()),
                            Mentions = new()
                        };

                        foreach (var role in JArray.Parse(data["d"]["member"]["roles"].ToString()))
                        {
                            messageModel.Member.Roles.Add(role.ToString());
                        }

                        foreach (var userInfo in JArray.Parse(data["d"]["mentions"].ToString()))
                        {
                            messageModel.Mentions.Add(new Models.User()
                            {
                                Avatar = userInfo["avatar"].ToString(),
                                IsBot = bool.Parse(userInfo["bot"].ToString()),
                                Id = userInfo["id"].ToString(),
                                UserName = userInfo["username"].ToString()
                            });
                        }

                        ReceivedAtMessage?.Invoke(messageModel);

                        break;

                    case GuildEvents.MESSAGE_CREATE:

                        var messageModel2 = new Models.MessageModels.Message()
                        {
                            Id = data["d"]["id"].ToString(),
                            ChildChannelId = data["d"]["channel_id"].ToString(),
                            Content = data["d"]["content"].ToString(),
                            GuildId = data["d"]["guild_id"].ToString(),
                            Author = new()
                            {
                                Avatar = data["d"]["author"]["avatar"].ToString(),
                                IsBot = bool.Parse(data["d"]["author"]["bot"].ToString()),
                                Id = data["d"]["author"]["id"].ToString(),
                                UserName = data["d"]["author"]["username"].ToString()
                            },
                            Member = new()
                            {
                                JoinedAt = DateTime.Parse(data["d"]["member"]["joined_at"].ToString()),
                                Roles = new()
                            },
                            Time = DateTime.Parse(data["d"]["timestamp"].ToString()),
                            Mentions = new()
                        };

                        foreach (var role in JArray.Parse(data["d"]["member"]["roles"].ToString()))
                        {
                            messageModel2.Member.Roles.Add(role.ToString());
                        }

                        ReceivedUserMessage?.Invoke(messageModel2);

                        break;
                }

                OnDispatch?.Invoke(data);

                break;

            case OpCode.Hello:

                heartbeatTimer.Interval = int.Parse(data["d"]["heartbeat_interval"].ToString());
                heartbeatTimer.Start();

                foreach (var type in _registeredEvents)
                {
                    _identifyData.intents += (int)type;
                }

                Load load = new();
                load.op = (int)OpCode.Identify;
                load.d = _identifyData;

                SendAsync(JsonConvert.SerializeObject(load));

                break;

            case OpCode.InvalidSession:

                AuthenticationError?.Invoke();

                CloseAsync();

                break;

            case OpCode.HeartbeatACK:

                _heartbeating = true;

                HeartbeatSendSuccess?.Invoke();

                break;
        }
    }
}