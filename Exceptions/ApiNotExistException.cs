using System;
namespace QQChannelFramework.Exceptions
{
    public class ApiNotExistException : Exception
    {
        public ApiNotExistException() : base("Api不存在，请检查开发文档，确保Api可用")
        {
        }
    }
}

