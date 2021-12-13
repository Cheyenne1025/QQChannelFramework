using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QQChannelFramework.Api;
using QQChannelFramework.Models;
using QQChannelFramework.Models.MessageModels;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Models.WsModels;
using QQChannelFramework.WS;

namespace QQChannelFramework.Expansions.Bot;

/// <summary>
/// QQ频道机器人
/// </summary>
public sealed partial class ChannelBot : FunctionWebSocket
{
    private string _url;

    public ChannelBot(OpenApiAccessInfo openApiAccessInfo) : base(openApiAccessInfo)
    {
        CommandInfo _parseCommand(Message message)
        {
            var realContent = message.Content.Trim();

            if (message.Mentions.Count > 0)
            {
                foreach (var user in message.Mentions)
                {
                    realContent = realContent.Replace($"<@!{user.Id}> ", string.Empty);
                }
            }

            var rawData = realContent.Split(" ");

            CommandInfo commandInfo = new();
            commandInfo.Key = rawData[0];
            commandInfo.Param = rawData.ToList();
            commandInfo.Param.RemoveAt(0);
            commandInfo.MessageId = message.Id;
            commandInfo.Sender = message.Author;
            commandInfo.GuildId = message.GuildId;
            commandInfo.ChannelId = message.ChildChannelId;

            return commandInfo;
        }

        ReceivedAtMessage += (message) =>
        {
            var commandInfo = _parseCommand(message);

            InvokeCommand(commandInfo, out bool trigger);

            if (trigger)
            {
                CommandTrigger?.Invoke(commandInfo);
            }
        };

        ReceivedUserMessage += (message) =>
        {
            if (_enableUserMessageTriggerCommand)
            {
                var commandInfo = _parseCommand(message);

                InvokeCommand(_parseCommand(message), out bool trigger);

                if (trigger)
                {
                    CommandTrigger?.Invoke(commandInfo);
                }
            }
        };
    }

    /// <summary>
    /// 机器人上线
    /// </summary>
    public async ValueTask OnlineAsync()
    {
        QQChannelApi qQChannelApi = new(_openApiAccessInfo);
        _url = await qQChannelApi.UseBotIdentity().GetWebSocketApi().GetUrlAsync().ConfigureAwait(false);

        Connect(_url);
    }

    /// <summary>
    /// 机器人下线
    /// </summary>
    public void OfflineAsync()
    {
        CloseAsync();
    }
}