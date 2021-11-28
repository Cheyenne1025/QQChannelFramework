using System;
namespace QQChannelFramework.Models.Types
{
    public enum OpCode : int
    {
        /// <summary>
        /// 服务端进行消息推送
        /// </summary>
        Dispatch,
        /// <summary>
        /// 客户端或服务端发送心跳
        /// </summary>
        Heartbeat,
        /// <summary>
        /// 客户端发送鉴权
        /// </summary>
        Identify,
        /// <summary>
        /// 客户端恢复连接
        /// </summary>
        Resume = 6,
        /// <summary>
        /// 服务端通知客户端重新连接
        /// </summary>
        Reconnect = 7,
        /// <summary>
        /// 当identify或resume的时候，如果参数有错，服务端会返回该消息
        /// </summary>
        InvalidSession = 9,
        /// <summary>
        /// 当客户端与网关建立ws连接之后，网关下发的第一条消息
        /// </summary>
        Hello = 10,
        /// <summary>
        /// 当发送心跳成功之后，就会收到该消息
        /// </summary>
        HeartbeatACK = 11
    }
}

