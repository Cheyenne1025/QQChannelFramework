using System;
namespace QQChannelFramework.Exceptions
{
    public class MissingDataException : Exception
    {
        public MissingDataException() : base("该请求需要携带数据，使用WithData()方法将数据携带")
        {
        }
    }
}

