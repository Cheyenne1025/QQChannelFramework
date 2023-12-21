namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 图片属性
    /// </summary>
    public class ImageElem
    {
        /// <summary>
        /// 第三方图片链接
        /// </summary>
        [Newtonsoft.Json.JsonProperty("third_url")]
        public string Url { get; set; }

        /// <summary>
        /// 宽度比例（缩放比，在屏幕里显示的比例）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("width_percent")]
        public double Width { get; set; }
    }
}
