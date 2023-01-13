namespace MyBot.Exceptions
{
    public class ParamErrorException : Exception
    {
        public ParamErrorException(string message) : base(message)
        {
        }
    }
}

