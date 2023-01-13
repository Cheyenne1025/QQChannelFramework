namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 子频道信息
    /// </summary>
    public class ChannelInfo
    {
        /// <summary>
        /// 子频道ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("channel_id")]
        public int ChannelId { get; set; }

        /// <summary>
        /// 子频道名称
        /// </summary>
        [Newtonsoft.Json.JsonProperty("channel_name")]
        public string ChannelName { get; set; }
    }
}
