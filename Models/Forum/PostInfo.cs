namespace MyBot.Models.Forum
{
    /// <summary>
    /// 帖子事件包含的帖子内容信息
    /// </summary>
    public class PostInfo
    {
        /// <summary>
        /// 主题ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("thread_id")]
        public string ThreadId { get; set; }

        /// <summary>
        /// 帖子ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("post_id")]
        public string PostId { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        [Newtonsoft.Json.JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        [Newtonsoft.Json.JsonProperty("date_time")]
        public string DateTime { get; set; }
    }
}
