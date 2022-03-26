using Newtonsoft.Json;
using QQChannelFramework.Tools.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
{
    /// <summary>
    /// 帖子事件包含的主帖内容相关信息
    /// </summary>
    public class ThreadInfo
    {
        /// <summary>
        /// 主帖ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("thread_id")]
        public string ThreadId { get; set; }

        /// <summary>
        /// 帖子标题
        /// </summary>
        [Newtonsoft.Json.JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        [Newtonsoft.Json.JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        [Newtonsoft.Json.JsonProperty("date_time")]
        [JsonConverter(typeof(TimeConverter<ThreadInfo>))]
        public DateTime? DateTime { get; set; }
    }
}
