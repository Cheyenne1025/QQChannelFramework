using System.Collections.Generic;

namespace MyBot.Models
{
    /// <summary>
    /// 指令信息
    /// </summary>
    public struct CommandInfo
    {
        /// <summary>
        /// 指令
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 指令参数
        /// </summary>
        public List<string> Param { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 指令主频道ID
        /// </summary>
        public string GuildId { get; set; }

        /// <summary>
        /// 指令来源子频道ID
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// 发送人信息
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        /// 消息中@的人
        /// </summary>
        public List<User> Mentions { get;set; }
    }
}

