using System;
namespace QQChannelFramework.Exceptions
{
    public class BotNotIsPrivateException : Exception
    {
        public BotNotIsPrivateException() : base("未指定为私域机器人")
        {
        }
    }
}

