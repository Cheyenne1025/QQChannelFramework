namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 文本属性
    /// </summary>
    public class TextElem
    {
        /// <summary>
        /// 正文
        /// </summary>
        [Newtonsoft.Json.JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// 文本属性
        /// </summary>
        [Newtonsoft.Json.JsonProperty("props")]
        public TextProps Props { get; set; }
    }
}
