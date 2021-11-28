using System;
using Newtonsoft.Json.Linq;

namespace QQChannelFramework.Models.WsModels
{
    /// <summary>
    /// WebSocket接收的数据
    /// </summary>
    public struct WsReceivedData
    {
        /// <summary>
        /// 接收到的数据
        /// </summary>
        public JToken Data { get; set; }

        /// <summary>
        /// 是否已经处理
        /// </summary>
        public bool IsProcessed { get; set; }
    }
}

