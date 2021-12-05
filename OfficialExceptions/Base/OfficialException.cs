using System;
namespace QQChannelFramework.OfficialExceptions
{
    /// <summary>
    /// 官方异常识别特性
    /// </summary>
    public class OfficialException : Attribute
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public OfficialException(int code,string message)
        {
            Code = code;
            Message = message;
        }
    }
}

