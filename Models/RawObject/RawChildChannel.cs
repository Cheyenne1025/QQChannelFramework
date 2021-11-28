using System;
namespace QQChannelFramework.Models.RawObject
{
    /// <summary>
    /// 源对象 - 子频道对象
    /// </summary>
    public struct RawChildChannel
    {
        /// <summary>
        /// 子频道id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 频道id
        /// </summary>
        public string guild_id { get; set; }

        /// <summary>
        /// 子频道名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 子频道类型
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 子频道子类型
        /// </summary>
        public int sub_type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int position { get; set; }

        /// <summary>
        /// 分组 id
        /// </summary>
        public string parent_id { get; set; }

        /// <summary>
        /// 创建人 id
        /// </summary>
        public string owner_id { get; set; }
    }
}

