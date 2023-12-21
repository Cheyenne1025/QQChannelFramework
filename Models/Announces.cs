using Newtonsoft.Json;
namespace MyBot.Models {
    public class RecommendChannel {
        [JsonProperty("channel_id")] public string ChannelId;
        [JsonProperty("introduce")] public string Introduce;
    }

    public enum AnnounceType {
        Member = 0,
        Welcome
    }

    /// <summary>
    /// 公告对象
    /// </summary>
    public class Announces {
        /// <summary>
        /// 主频道ID
        /// </summary>
        [JsonProperty("guild_id")]
        public string Guild { get; set; }

        /// <summary>
        /// 子频道ID
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        /// <summary>
        /// 公告类别
        /// </summary>
        [JsonProperty("announces_type")]
        public AnnounceType AnnounceType { get; set; }

        /// <summary>
        /// 推荐子频道详情列表
        /// </summary>
        [JsonProperty("recommend_channels")]
        public RecommendChannel[] RecommendChannels { get; set; }
    }
}