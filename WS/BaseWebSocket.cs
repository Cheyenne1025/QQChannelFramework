using System;
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

public class BaseWebSocket
{
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

    ArraySegment<byte> bytesReceived;

    public BaseWebSocket()
    {
        webSocket = new ClientWebSocket();
        
        bytesReceived = new ArraySegment<byte>(new byte[1024]);
    }

    private void InitArraySegment()
    {
        bytesReceived = new ArraySegment<byte>(new byte[1024]);
    }

    /// <summary>
    /// 开始连接
    /// </summary>
    /// <exception cref="Exception"></exception>
    protected void Connect(string url)
    {
        _url = url;
        connectUrl = new Uri(url);

        try
        {
            webSocket = new ClientWebSocket();
            webSocket.ConnectAsync(connectUrl, CancellationToken.None).Wait();

            OnConnected?.Invoke();
            BeginReceive();
        }
        catch (Exception ex)
        {
            OnError?.Invoke(ex);
        }
    }

    private async void BeginReceive()
    {
        InitArraySegment();
        try
        {
            var result = await webSocket.ReceiveAsync(bytesReceived, CancellationToken.None);

            if (result.Count > 0)
            {
                var data = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);

                if(data.Length > 0)
                {
                    OnReceived?.Invoke(JToken.Parse(data));
                }
            }
        }
        catch (Exception ex)
        {
            OnError?.Invoke(ex);
        }
        finally
        {
            if (webSocket.State == WebSocketState.Open)
            {
                BeginReceive();
            }
        }
    }

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="jsonData"></param>
    /// <exception cref="Exception"></exception>
    public async void SendAsync(string jsonData)
    {
        if (webSocket.State is not WebSocketState.Open)
        {
            ConnectBreak?.Invoke();

            return;
        }

        try
        {
            ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonData));
            await webSocket.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
            OnSend?.Invoke();
        }
        catch (Exception ex)
        {
            OnError?.Invoke(ex);
        }
    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    public async void CloseAsync()
    {
        if (webSocket is not null)
        {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None).ConfigureAwait(false);

            OnClose?.Invoke();
        }
    }
}