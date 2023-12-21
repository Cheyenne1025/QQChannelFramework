namespace MyBot.Models.Forum
{
    /// <summary>
    /// 该事件在话题频道内新发表主题或删除时生产事件中包含该对象
    /// <br/>
    /// 话题频道内发表的主帖称为主题
    /// </summary>
    public class Thread
    {
        /// <summary>
        /// 频道ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("guild_id")]
        public string GuildId { get; set; }

        /// <summary>
        /// 子频道ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// 作者ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("author_id")]
        public string AuthorId { get; set; }

        /// <summary>
        /// 主帖内容
        /// </summary>
        [Newtonsoft.Json.JsonProperty("thread_info")]
        public ThreadInfo ThreadInfo { get; set; }
    }
}
