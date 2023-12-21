namespace MyBot.Exceptions
{
    public class WebSocketLinkingException : Exception
    {
        public WebSocketLinkingException() : base("WebScoket处于连接状态，请先Close")
        {
        }
    }
}

