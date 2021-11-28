using System;
namespace QQChannelFramework.Datas
{
    /// <summary>
    /// OpenApi接入信息
    /// </summary>
    public struct OpenApiAccessInfo
    {
        /// <summary>
        /// 机器人开发识别码
        /// </summary>
        public string BotAppId { get; set; }

        /// <summary>
        /// 机器人Token
        /// </summary>
        public string BotToken { get; set; }

        /// <summary>
        /// 机器人密钥
        /// </summary>
        public string BotSecret { get; set; }
    }
}

