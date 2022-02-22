using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QQChannelFramework.Api;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models;
using QQChannelFramework.Models.MessageModels;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Models.WsModels;
using QQChannelFramework.WS;

namespace QQChannelFramework.Expansions.Bot;

/// <summary>
/// QQ频道机器人
/// </summary>
public sealed partial class ChannelBot : FunctionWebSocket {
    private string _url;
    private QQChannelApi _api;

    public ChannelBot(QQChannelApi qqChannelApi) : base(qqChannelApi.OpenApiAccessInfo) {
        _api = qqChannelApi;
        
        CommandInfo _parseCommand(Message message)
        {
            var realContent = message.Content.Trim();

            // Check null and check count
            if (message.Mentions is { Count: > 0 })
            {
                foreach (var user in message.Mentions.Where(user => user != null))
                {
                    //在末尾处At没有空格
                    realContent = realContent.Replace($"<@!{user.Id}> ", string.Empty);
                    realContent = realContent.Replace($"<@!{user.Id}>", string.Empty);
                }
                if (!string.IsNullOrWhiteSpace(message.Content))
                    message.Content = message.Content.Trim();
            }

            if (message.MentionEveryone)
            {
                if (message.Content != null)
                {
                    message.Content = message.Content.Replace($"@everyone ", string.Empty);
                    message.Content = message.Content.Replace($"@everyone", string.Empty);

                    if (!string.IsNullOrWhiteSpace(message.Content))
                        message.Content = message.Content.Trim();
                }
            }

            var rawData = realContent.Split(" ");

            if (rawData[0].Length is 0)
            {
                return default(CommandInfo);
            }

            CommandInfo commandInfo = new();
            commandInfo.Key = rawData[0];
            commandInfo.Param = rawData.ToList();
            commandInfo.Param.RemoveAt(0);
            commandInfo.MessageId = message.Id;
            commandInfo.Sender = message.Author;
            commandInfo.GuildId = message.GuildId;
            commandInfo.ChannelId = message.ChannelId;
            commandInfo.Mentions = message.Mentions;

            return commandInfo;
        }

        ReceivedAtMessage += (message) => {
            var commandInfo = _parseCommand(message);

            InvokeCommand(commandInfo, out bool trigger);

            if (trigger) {
                CommandTrigger?.Invoke(commandInfo);
            }
        };

        ReceivedUserMessage += (message) => {
            if (_enableUserMessageTriggerCommand) {
                var commandInfo = _parseCommand(message);

                InvokeCommand(_parseCommand(message), out bool trigger);

                if (trigger) {
                    CommandTrigger?.Invoke(commandInfo);
                }
            }
        };

        // Websocket断线重连
        ConnectBreak += async () => {
            Debug.WriteLine("MyBot Websocket 断线重连"); 
            await Reconnect();
        };
    }

    /// <summary>
    /// 机器人上线
    /// </summary>
    public async ValueTask OnlineAsync() { 
        _url = await _api.GetWebSocketApi().GetUrlAsync().ConfigureAwait(false);
        await ConnectAsync(_url);
    }

    private async Task Reconnect() {
        while (true) {
            try {
                await Task.Delay(TimeSpan.FromSeconds(3));
                await OnlineAsync().ConfigureAwait(false);
                Debug.WriteLine("MyBot Websocket 重连完成");

                return;
            } catch (Exception ex) {
                Debug.WriteLine("MyBot Websocket 重连失败，3秒后重试");
                Debug.WriteLine(ex);
            }
        }
    }

    /// <summary>
    /// 机器人下线
    /// </summary>
    public async ValueTask OfflineAsync() {
        await CloseAsync();
    }
}