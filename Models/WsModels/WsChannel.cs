using System;
namespace QQChannelFramework.Models.WsModels
{
    /// <summary>
    /// WebSocket接收的ChildChannel数据对象
    /// </summary>
    public class WsChannel : Channel
    {
        /// <summary>
        /// 操作人的ID
        /// </summary>
        public string OperationUserId { get; set; }
    }
}

