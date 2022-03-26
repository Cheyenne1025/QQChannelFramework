using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
{
    /// <summary>
    /// 富文本 - 链接信息
    /// </summary>
    public class URLInfo
    {
        /// <summary>
        /// 链接地址
        /// </summary>
        [Newtonsoft.Json.JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        [Newtonsoft.Json.JsonProperty("display_text")]
        public string DisplayText { get; set; }
    }
}
