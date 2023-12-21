using MyBot.Models.Types;
namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本内容
    /// </summary>
    public class RichObject
    {
        /// <summary>
        /// 富文本类型
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        public RichType Type { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        [Newtonsoft.Json.JsonProperty("text_info")]
        public TextInfo TextInfo { get; set; }

        /// <summary>
        /// @内容
        /// </summary>
        [Newtonsoft.Json.JsonProperty("at_info")]
        public AtInfo AtInfo { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [Newtonsoft.Json.JsonProperty("url_info")]
        public URLInfo URLInfo { get; set; }

        /// <summary>
        /// 表情
        /// </summary>
        [Newtonsoft.Json.JsonProperty("emoji_info")]
        public EmojiInfo EmojiInfo { get; set; }

        /// <summary>
        /// 提到的子频道
        /// </summary>
        [Newtonsoft.Json.JsonProperty("channel_info")]
        public ChannelInfo ChannelInfo { get; set; }
    }
}
