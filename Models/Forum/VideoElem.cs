namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 视频属性
    /// </summary>
    public class VideoElem
    {
        /// <summary>
        /// 第三方视频文件链接
        /// </summary>
        [Newtonsoft.Json.JsonProperty("third_url")]
        public string Url { get; set; }
    }
}
