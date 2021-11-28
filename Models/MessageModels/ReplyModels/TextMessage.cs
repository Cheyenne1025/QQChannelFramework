using System;
namespace QQChannelFramework.Models.MessageModels.ReplyModels
{
    /// <summary>
    /// 文字消息
    /// </summary>
    public struct TextMessage
    {
        /// <summary>
        /// 回复内容 支持内嵌格式
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 要回复的消息Id 为空视为主动推送
        /// </summary>
        public string msg_id { get; set; }
    }
}