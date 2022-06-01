using System;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Models;
using QQChannelFramework.Models.ParamModels;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Tools;

namespace QQChannelFramework.WS;

public delegate void NormalDelegate();
public delegate void NoticeDelegate(string message);
public delegate void ErrorDelegate(Exception ex);
public delegate void ReceiveDelegate(JToken receiveData);
public delegate void CommandTriggerDelegate(CommandInfo commandInfo);

public class BaseWebSocket {
    /// <summary>
    /// 连接成功事件
    /// </summary>
    public event NormalDelegate OnConnected;

    /// <summary>
    /// 连接断开事件 <br/>
    /// <b>MyBot内部实现了断线重连机制，不需要用户通过监听此事件进行主动断线重连</b>
    /// </summary>
    public event NormalDelegate ConnectBreak;

    /// <summary>
    /// 连接异常事件 <br/>
    /// <b>MyBot内部实现了断线重连机制，不需要用户通过监听此事件进行主动断线重连</b>
    /// </summary>
    public event ErrorDelegate OnError;

    /// <summary>
    /// 收到消息事件
    /// </summary>
    protected event ReceiveDelegate OnReceived;

    /// <summary>
    /// 连接关闭事件 <br/>
    /// <b>MyBot内部实现了断线重连机制，不需要用户通过监听此事件进行主动断线重连</b>
    /// </summary>
    public event NormalDelegate OnClose;

    /// <summary>
    /// 数据发送成功事件
    /// </summary>
    public event NormalDelegate OnSend;

    protected string _url;
    private Uri connectUrl;
    protected ClientWebSocket webSocket = null;
    private byte[] receiveBuf = new byte[4096];

    private CancellationTokenSource _websocketCancellationTokenSource = null;

    /// <summary>
    /// 开始连接
    /// </summary>
    /// <exception cref="Exception"></exception>
    protected async ValueTask ConnectAsync(string url) {
        _url = url;
        connectUrl = new Uri(url); 

        // 释放上一个Websocket
        try {
            await CloseAsync();
        } catch (Exception ex) {
            BotLog.Log(ex);
        }

        try {
            webSocket = new ClientWebSocket();
            _websocketCancellationTokenSource = new CancellationTokenSource();
            await webSocket.ConnectAsync(connectUrl, _websocketCancellationTokenSource.Token).ConfigureAwait(false);

            OnConnected?.Invoke();
            BeginReceive();
        } catch (Exception ex) {
            OnError?.Invoke(ex); 
        }
    }

    private void BeginReceive() {
#pragma warning disable CS4014
        Task.Run(ReceiveAsync, _websocketCancellationTokenSource.Token);
#pragma warning restore CS4014
    }

    private async Task ReceiveAsync() {
        var cancellationToken = _websocketCancellationTokenSource.Token;
        try {
            var ms = new MemoryStream(); 
            WebSocketReceiveResult result;
            while (true) {
                cancellationToken.ThrowIfCancellationRequested();
                result = await webSocket.ReceiveAsync(receiveBuf, cancellationToken)
                    .ConfigureAwait(false);

                if (result.Count > 0) {
                    ms.Write(receiveBuf, 0, result.Count);
                }

                if (result.EndOfMessage)
                    break;
            }

            cancellationToken.ThrowIfCancellationRequested();

            if (result.MessageType != WebSocketMessageType.Close) {
                var bytes = ms.ToArray();
                var data = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                if (data.Length > 0) { 
                    OnReceived?.Invoke(JToken.Parse(data));
                }
            } else { 
                BotLog.Log($"BotWs close handshake {result.CloseStatus} {result.CloseStatusDescription}"); 
            }
        } catch (TaskCanceledException x) {
            BotLog.Log(x);
        } catch (Exception ex) {
            OnError?.Invoke(ex);
        } finally {
            // 被取消则不触发事件
            if (!cancellationToken.IsCancellationRequested) {
                if (webSocket.State == WebSocketState.Open) {
                    BeginReceive();
                } else {
                    BotLog.Log($"{DateTime.Now} Connection break post receive check.");
                    ConnectBreak?.Invoke();
                }
            }
        }
    }

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="jsonData"></param>
    /// <exception cref="Exception"></exception>
    public async void SendAsync(string jsonData) {
        if (webSocket == null || _websocketCancellationTokenSource == null) {
            return;
        } 

        if (!_websocketCancellationTokenSource.IsCancellationRequested &&
            webSocket.State is not WebSocketState.Open) {
            BotLog.Log($"{DateTime.Now} Connection break before send check.");
            ConnectBreak?.Invoke();
            return;
        }

        try {
            var bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            await webSocket.SendAsync(bytesToSend, WebSocketMessageType.Text, true,
                    _websocketCancellationTokenSource.Token)
                .ConfigureAwait(false);
            OnSend?.Invoke();
        } catch (TaskCanceledException x) {
            BotLog.Log(x);
        } catch (IOException x) {
            BotLog.Log(x);
        } catch (Exception ex) {
            OnError?.Invoke(ex);
        } finally {
            // 被取消则不触发事件
            if (!_websocketCancellationTokenSource.IsCancellationRequested) {
                if (webSocket.State != WebSocketState.Open) {
                    BotLog.Log($"{DateTime.Now} Connection break post send check.");
                    ConnectBreak?.Invoke();
                }
            }
        }
    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    public async ValueTask CloseAsync() {
        BotLog.Log($"{DateTime.Now} BotWs close, ws is null? {webSocket is null}.");
        if (webSocket is not null) {  
            try {
                _websocketCancellationTokenSource.Cancel(); 
                webSocket.Dispose();
                _websocketCancellationTokenSource.Dispose();
            } catch (Exception ex) {
                BotLog.Log(ex);
            }

            _websocketCancellationTokenSource = null;
            webSocket = null;

            OnClose?.Invoke();
        }
    }
}