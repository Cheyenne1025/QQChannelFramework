using System;
using System.Collections.Generic;
using System.Timers;
using ChannelModels.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Datas;
using QQChannelFramework.Models.ParamModels;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Models.WsModels;

namespace QQChannelFramework.WS
{
    /// <summary>
    /// 功能性WebSocket
    /// </summary>
    public sealed partial class FunctionWebSocket : BaseWebSocket
    {
        private Timer heartbeatTimer;

        private OpenApiAccessInfo _openApiAccessInfo;

        private SessionInfo _sessionInfo;

        private IdentifyData _identifyData;

        private HashSet<Intents> _registeredEvents;

        private string _nowS = string.Empty;

        public FunctionWebSocket(OpenApiAccessInfo openApiAccessInfo, string url) : base(url)
        {
            _sessionInfo = new();
            _identifyData = new();
            _registeredEvents = new();
            _openApiAccessInfo = openApiAccessInfo;
            OnReceived += Process;
            heartbeatTimer = new Timer();

            heartbeatTimer.Elapsed += HeartbeatTimer_Elapsed;

            _identifyData.token = $"Bot {_openApiAccessInfo.BotAppId}.{_openApiAccessInfo.BotToken}";
            _identifyData.shard = new int[2] { 0, 1 };
        }

        private void HeartbeatTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(webSocket.State == System.Net.WebSockets.WebSocketState.Open)
            {
                Load load = new();
                load.op = (int)OpCode.Heartbeat;
                load.d = _nowS == string.Empty ? null : _nowS;

                SendAsync(JsonConvert.SerializeObject(load));

                Console.WriteLine("心跳已发送");
            }
            else
            {
                Console.WriteLine($"已断开链接，心跳发送停止");
                heartbeatTimer.Stop();
            }
        }
    }
}

