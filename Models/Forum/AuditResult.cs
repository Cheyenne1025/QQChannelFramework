using MyBot.Models.Types;

namespace MyBot.Models.Forum
{
    /// <summary>
    /// 论坛帖子审核结果事件
    /// </summary>
    public class AuditResult
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
        /// 回复ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("reply_id")]
        public string ReplyId { get; set; }

        /// <summary>
        /// 审核的类型
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        public AuditType Type { get; set; }

        /// <summary>
        /// 审核结果. 0:成功 1:失败
        /// </summary>
        [Newtonsoft.Json.JsonProperty("result")]
        public int Result { get; set; }

        /// <summary>
        /// result不为0时错误信息
        /// </summary>
        [Newtonsoft.Json.JsonProperty("err_msg")]
        public string ErrMsg { get; set; }
    }
}
