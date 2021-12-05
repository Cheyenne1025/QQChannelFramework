using System;
namespace QQChannelFramework.Models.WsModels
{
    /// <summary>
    /// WebSocket接收的Guild数据对象
    /// </summary>
    public class WsGuild : Guild
    {
        /// <summary>
        /// 操作人的ID
        /// </summary>
        public string OperationUserId { get; set; }
    }
}

