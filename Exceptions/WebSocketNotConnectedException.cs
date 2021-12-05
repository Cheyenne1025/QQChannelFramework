using System;
namespace QQChannelFramework.Exceptions
{
    public class WebSocketNotConnectedException : Exception
    {
        public WebSocketNotConnectedException() : base("WebSocket未连接")
        {
        }
    }
}

