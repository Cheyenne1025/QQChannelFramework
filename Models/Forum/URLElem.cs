namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - URL属性
    /// </summary>
    public class URLElem
    {
        /// <summary>
        /// URL链接
        /// </summary>
        [Newtonsoft.Json.JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// URL描述
        /// </summary>
        [Newtonsoft.Json.JsonProperty("desc")]
        public string Desc { get; set; }
    }
}
