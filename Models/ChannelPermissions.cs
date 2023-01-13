using Newtonsoft.Json;

namespace MyBot.Models
{
    /// <summary>
    /// 子频道权限对象
    /// </summary>
    public class ChannelPermissions
    {
        /// <summary>
        /// 子频道ID
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// 用户 ID
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 权限位图 (十进制)
        /// </summary>
        [JsonProperty("permissions")]
        public string Permissions { get; set; }
    }
}

