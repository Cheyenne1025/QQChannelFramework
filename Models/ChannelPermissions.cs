using System;
namespace QQChannelFramework.Models
{
    /// <summary>
    /// 子频道权限对象
    /// </summary>
    public class ChannelPermissions
    {
        /// <summary>
        /// 子频道ID
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// 用户 ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 权限位图 (十进制)
        /// </summary>
        public string Permissions { get; set; }
    }
}

