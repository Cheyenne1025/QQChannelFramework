using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
{
    /// <summary>
    /// 富文本 - 文本段落属性
    /// </summary>
    public class TextProps
    {
        /// <summary>
        /// 加粗
        /// </summary>
        [Newtonsoft.Json.JsonProperty("font_bold")]
        public bool FontBold { get; set; }

        /// <summary>
        /// 斜体
        /// </summary>
        [Newtonsoft.Json.JsonProperty("italic")]
        public bool Italic { get; set; }

        /// <summary>
        /// 下划线
        /// </summary>
        [Newtonsoft.Json.JsonProperty("underline")]
        public bool Underline { get; set; }
    }
}
