using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
{
    /// <summary>
    /// 话题频道内对帖子主题评论或删除时生产事件中包含该对象
    /// <br/>
    /// 话题频道内对主题的评论称为帖子
    /// </summary>
    public class Post
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
        /// 帖子内容
        /// </summary>
        [Newtonsoft.Json.JsonProperty("post_info")]
        public PostInfo PostInfo { get; set; }
    }
}
