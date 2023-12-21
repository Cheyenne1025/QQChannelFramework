namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 平台图片属性
    /// </summary>
    public class PlatImage
    {
        /// <summary>
        /// 架平图片链接
        /// </summary>
        [Newtonsoft.Json.JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 图片宽度
        /// </summary>
        [Newtonsoft.Json.JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// 图片高度
        /// </summary>
        [Newtonsoft.Json.JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("image_id")]
        public string Image { get; set; }
    }
}
