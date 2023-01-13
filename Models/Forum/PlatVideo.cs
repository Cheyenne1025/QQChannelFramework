namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 平台视频属性
    /// </summary>
    public class PlatVideo
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
        /// 视频ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("video_id")]
        public string VideoId { get; set; }

        /// <summary>
        /// 视频时长
        /// </summary>
        [Newtonsoft.Json.JsonProperty("duration")]
        public int Duration { get; set; }

        /// <summary>
        /// 视频封面图属性
        /// </summary>
        [Newtonsoft.Json.JsonProperty("cover")]
        public PlatImage Cover { get; set; }
    }
}
