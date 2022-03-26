using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
{
    /// <summary>
    /// 回复事件包含的回复内容信息
    /// </summary>
    public class ReplyInfo
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
        /// 回复ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("reply_id")]
        public string ReplyId { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [Newtonsoft.Json.JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        [Newtonsoft.Json.JsonProperty("date_time")]
        public string DateTime { get; set; }
    }
}
