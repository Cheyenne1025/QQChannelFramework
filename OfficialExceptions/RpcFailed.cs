using System;
namespace QQChannelFramework.OfficialExceptions
{
    [OfficialException(300002, "RPC失败，请检查传入参数是否正确或存在，包括 频道Guild,身份组ID等...")]
    public struct RpcFailed
    {
    }
}

