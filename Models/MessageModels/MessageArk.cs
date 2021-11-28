using System;
using System.Collections.Generic;

namespace QQChannelFramework.Models.MessageModels
{
    public class MessageArk
    {
        /// <summary>
        /// 模版ID
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// kv值列表
        /// </summary>
        public List<MessageArkKv> Kv { get; set; }
    }
}

