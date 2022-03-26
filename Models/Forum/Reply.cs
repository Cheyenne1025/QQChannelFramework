using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
{
    /// <summary>
    /// 话题频道对帖子回复或删除时生产该事件中包含该对象
    /// <br/>
    /// 话题频道内对帖子的评论称为回复
    /// </summary>
    public class Reply
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
        /// 回复内容
        /// </summary>
        [Newtonsoft.Json.JsonProperty("reply_info")]
        public ReplyInfo ReplyInfo { get; set; }
    }
}
