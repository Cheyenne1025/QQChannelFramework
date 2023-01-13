using System.Collections.Generic;

namespace MyBot.Models.Forum
{
    /// <summary>
    /// 富文本 - 段落结构
    /// </summary>
    public class Paragraph
    {
        /// <summary>
        /// 元素列表
        /// </summary>
        [Newtonsoft.Json.JsonProperty("elems")]
        public List<Elem> Elems { get; set; }

        /// <summary>
        /// 段落属性
        /// </summary>
        [Newtonsoft.Json.JsonProperty("props")]
        public ParagraphProps Props { get; set; }
    }
}
