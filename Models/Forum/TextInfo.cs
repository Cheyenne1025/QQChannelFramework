namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 普通文本
    /// </summary>
    public class TextInfo
    {
        /// <summary>
        /// 文本
        /// </summary>
        [Newtonsoft.Json.JsonProperty("text")]
        public string Text { get; set; }
    }
}
