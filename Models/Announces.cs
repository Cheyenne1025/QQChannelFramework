using System;
using Newtonsoft.Json;

namespace QQChannelFramework.Models
{
    /// <summary>
    /// 公告对象
    /// </summary>
    public class Announces
    {
        /// <summary>
        /// 主频道ID
        /// </summary>
        [JsonProperty("guild_id")]
        public string Guild { get; set; }

        /// <summary>
        /// 子频道ID
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChildChannel { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}

