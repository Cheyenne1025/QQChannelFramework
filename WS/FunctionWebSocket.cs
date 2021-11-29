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
public sealed partial class FunctionWebSocket : BaseWebSocket
{
    private Timer heartbeatTimer;

    private OpenApiAccessInfo _openApiAccessInfo;

    private SessionInfo _sessionInfo;

    private IdentifyData _identifyData;

    private HashSet<Intents> _registeredEvents;

    private string _nowS = string.Empty;

    private bool _heartbeating = false;

    private bool _enableShard = false;

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

    /// <summary>
    /// 执行重连 (如果处于连接状态将会主动断开后连接)
    /// </summary>
    public void Resume()
    {
        if(webSocket.State != System.Net.WebSockets.WebSocketState.Open)
        {
            Connect();
            OnConnected += ResumeAction;
        }
    }

    private void ResumeAction()
    {
        Load load = new();
        load.op = (int)OpCode.Resume;
        load.d = new Dictionary<string, object>()
        {
            {"token",_openApiAccessInfo.BotToken},
            {"session_id",_sessionInfo.SessionId },
            {"seq",1337 }
        };

        SendAsync(JsonConvert.SerializeObject(load));

        OnConnected -= ResumeAction;
    }
}