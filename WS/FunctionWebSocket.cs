using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using ChannelModels.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Datas;
using QQChannelFramework.Exceptions;
using QQChannelFramework.Models.ParamModels;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Models.WsModels;

namespace QQChannelFramework.WS;

/// <summary>
/// 功能性WebSocket
/// </summary>
public partial class FunctionWebSocket : BaseWebSocket {
   private Timer heartbeatTimer;

   protected OpenApiAccessInfo _openApiAccessInfo;

   protected SessionInfo _sessionInfo;

   protected IdentifyData _identifyData;

   protected HashSet<Intents> _registeredEvents;

   protected string _nowS = string.Empty;

   protected bool _heartbeating = false;

   protected bool _enableShard = false;

   private bool _resumeIsBind;

   protected bool _enableUserMessageTriggerCommand;

   public FunctionWebSocket(OpenApiAccessInfo openApiAccessInfo) {
      _sessionInfo = new();
      _identifyData = new();
      _registeredEvents = new();
      _openApiAccessInfo = openApiAccessInfo;
      OnRawWebsocketMessageReceived += Process;
      heartbeatTimer = new Timer();

      heartbeatTimer.Elapsed += HeartbeatTimer_Elapsed;

      _identifyData.token = $"Bot {_openApiAccessInfo.BotAppId}.{_openApiAccessInfo.BotToken}";
      _identifyData.shard = new[] { 0, 1 };
   }

   /// <summary>
   /// 指定为私域机器人
   /// </summary>
   public void UsePrivateBot() {
      CommonState.PrivateBot = true;
   }

   /// <summary>
   /// 启用无须@ 触发指令功能 (私域机器人可用)
   /// </summary>
   public void EnableUserMessageTriggerCommand() {
      if (CommonState.PrivateBot is false) {
         throw new BotNotIsPrivateException();
      }

      _enableUserMessageTriggerCommand = true;
   }

   /// <summary>
   /// 关闭无须@ 触发指令功能 (私域机器人可用)
   /// </summary>
   public void CloseUserMessageTriggerCommand() {
      _enableUserMessageTriggerCommand = false;
   }

   private void HeartbeatTimer_Elapsed(object sender, ElapsedEventArgs e) {
      if (webSocket.State == System.Net.WebSockets.WebSocketState.Open) {
         Load load = new();
         load.op = (int)OpCode.Heartbeat;
         load.d = _nowS == string.Empty ? null : _nowS;

         SendAsync(JsonConvert.SerializeObject(load));
      }
      else {
         heartbeatTimer.Stop();

         HeartbeatBreak?.Invoke();
      }
   }
}
