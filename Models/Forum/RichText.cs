using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
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
        public Paragraph Paragraphs { get; set; }
    }
}
