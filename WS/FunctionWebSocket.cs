using System;
using System.Collections.Generic;
using System.Timers;
using ChannelModels.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Models.ParamModels;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Models.WsModels;

namespace QQChannelFramework.WS;

/// <summary>
/// 功能性WebSocket
/// </summary>
public partial class FunctionWebSocket : BaseWebSocket
{
    private Timer heartbeatTimer;

    protected OpenApiAccessInfo _openApiAccessInfo;

    protected SessionInfo _sessionInfo;

    protected IdentifyData _identifyData;

    protected HashSet<Intents> _registeredEvents;

    protected string _nowS = string.Empty;

    protected bool _heartbeating = false;

    protected bool _enableShard = false;

    public FunctionWebSocket(OpenApiAccessInfo openApiAccessInfo)
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
        if (webSocket.State == System.Net.WebSockets.WebSocketState.Open)
        {
            Load load = new();
            load.op = (int)OpCode.Heartbeat;
            load.d = _nowS == string.Empty ? null : _nowS;

            SendAsync(JsonConvert.SerializeObject(load));
        }
        else
        {
            heartbeatTimer.Stop();

            HeartbeatBreak?.Invoke();
        }
    }
}