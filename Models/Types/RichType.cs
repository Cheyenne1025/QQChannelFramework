using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Types
{
    /// <summary>
    /// 富文本类型
    /// </summary>
    public enum RichType : int
    {
        /// <summary>
        /// 普通文本
        /// </summary>
        TEXT = 1,
        /// <summary>
        /// at信息
        /// </summary>
        AT = 2,
        /// <summary>
        /// url信息
        /// </summary>
        URL = 3,
        /// <summary>
        /// 表情
        /// </summary>
        EMOJI = 4,
        /// <summary>
        /// #子频道
        /// </summary>
        CHANNEL = 5,
        /// <summary>
        /// 视频
        /// </summary>
        VIDEO = 10,
        /// <summary>
        /// 图片
        /// </summary>
        IMAGE = 11
    }
}
