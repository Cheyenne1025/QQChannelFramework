using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Models;
using QQChannelFramework.Models.ParamModels;
using QQChannelFramework.Models.Types;

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
    /// 连接断开事件
    /// </summary>
    public event NormalDelegate ConnectBreak;

    /// <summary>
    /// 连接异常事件
    /// </summary>
    public event ErrorDelegate OnError;

    /// <summary>
    /// 收到消息事件
    /// </summary>
    protected event ReceiveDelegate OnReceived;

    /// <summary>
    /// 链接关闭事件
    /// </summary>
    public event NormalDelegate OnClose;

    /// <summary>
    /// 数据发送成功事件
    /// </summary>
    public event NormalDelegate OnSend;

    protected string _url;
    private Uri connectUrl;
    protected ClientWebSocket webSocket;
    private byte[] receiveBuf = new byte[4096];
    
    public BaseWebSocket() {
        webSocket = new ClientWebSocket();
    }

    /// <summary>
    /// 开始连接
    /// </summary>
    /// <exception cref="Exception"></exception>
    protected async ValueTask ConnectAsync(string url) {
        _url = url;
        connectUrl = new Uri(url);

        try { 
            webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(connectUrl, CancellationToken.None); 
            
            OnConnected?.Invoke();
            BeginReceive();
        } catch (Exception ex) {
            OnError?.Invoke(ex);
        }
    }

    private void BeginReceive() { 
#pragma warning disable CS4014
        Task.Run(ReceiveAsync).ConfigureAwait(false);
#pragma warning restore CS4014
    }
    
    private async void ReceiveAsync() {
        try {
            var ms = new MemoryStream();

            while (true) {
                var result = await webSocket.ReceiveAsync(receiveBuf, CancellationToken.None);

                if (result.Count > 0) {
                    ms.Write(receiveBuf, 0, result.Count);
                }

                if (result.EndOfMessage)
                    break;
            }

            var bytes = ms.ToArray();
            var data = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            if (data.Length > 0) {
                OnReceived?.Invoke(JToken.Parse(data));
            }
        } catch (Exception ex) {
            OnError?.Invoke(ex);
        } finally {
            if (webSocket.State == WebSocketState.Open) {
                BeginReceive();
            } else { 
                ConnectBreak?.Invoke(); 
            }
        }
    }

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="jsonData"></param>
    /// <exception cref="Exception"></exception>
    public async void SendAsync(string jsonData) {
        if (webSocket.State is not WebSocketState.Open) {
            ConnectBreak?.Invoke(); 
            return;
        }

        try {
            var bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            await webSocket.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
            OnSend?.Invoke();
        } catch (Exception ex) {
            OnError?.Invoke(ex);
        }
    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    public async ValueTask CloseAsync() {
        if (webSocket is not null) {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None)
                .ConfigureAwait(false);

            OnClose?.Invoke();
        }
    }
}