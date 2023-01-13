using System.Collections.Generic;

namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本内容
    /// </summary>
    public class RichText
    {
        /// <summary>
        /// 段落，一段落一行，段落内无元素的为空行
        /// </summary>
        [Newtonsoft.Json.JsonProperty("paragraphs")]
        public List<Paragraph> Paragraphs { get; set; }
    }
}
