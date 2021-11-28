using System;
namespace QQChannelFramework.Models.WsModels
{
    /// <summary>
    /// 负载结构
    /// </summary>
    public struct Load
    {
        /// <summary>
        /// opcode
        /// </summary>
        public int op { get; set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        public object d { get; set; }

        /// <summary>
        /// 唯一序列号
        /// </summary>
        public int s { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public string t { get; set; }
    }
}

