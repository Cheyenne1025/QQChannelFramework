namespace MyBot.Exceptions
{
    public class WebSocketNotConnectedException : Exception
    {
        public WebSocketNotConnectedException() : base("WebSocket未连接")
        {
        }
    }
}

