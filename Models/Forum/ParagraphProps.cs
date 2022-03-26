using QQChannelFramework.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
{
    /// <summary>
    /// 富文本 - 段落属性
    /// </summary>
    public class ParagraphProps
    {
        /// <summary>
        /// 段落对齐方向属性
        /// </summary>
        [Newtonsoft.Json.JsonProperty("alignment")]
        public Alignment Alignment { get; set; }
    }
}
