namespace MyBot.Exceptions
{
    public class RequestRateTooHighException : Exception
    {
        public RequestRateTooHighException() : base("请求频率过高")
        {
        }
    }
}

