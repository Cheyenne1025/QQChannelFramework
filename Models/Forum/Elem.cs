using MyBot.Models.Types;

namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 元素列表结构
    /// </summary>
    public class Elem
    {
        /// <summary>
        /// 文本元素
        /// </summary>
        [Newtonsoft.Json.JsonProperty("text")]
        public TextElem Text { get; set; }

        /// <summary>
        /// 图片元素
        /// </summary>
        [Newtonsoft.Json.JsonProperty("image")]
        public ImageElem Image { get; set; }

        /// <summary>
        /// 视频元素
        /// </summary>
        [Newtonsoft.Json.JsonProperty("video")]
        public VideoElem Video { get; set; }

        /// <summary>
        /// URL元素
        /// </summary>
        [Newtonsoft.Json.JsonProperty("url")]
        public URLElem Url { get; set; }

        /// <summary>
        /// 元素类型
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        public ElemType Type { get; set; }
    }
}
