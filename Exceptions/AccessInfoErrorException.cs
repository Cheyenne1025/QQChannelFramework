using System;
namespace QQChannelFramework.Exceptions
{
    public class AccessInfoErrorException : Exception
    {
        public AccessInfoErrorException() : base("鉴权信息有误")
        {
        }
    }
}

