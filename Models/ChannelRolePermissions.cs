using System;
using Newtonsoft.Json;

namespace QQChannelFramework.Models
{
    /// <summary>
    /// 子频道身份组权限对象
    /// </summary>
    public class ChannelRolePermissions
    {
        /// <summary>
        /// 子频道ID
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// 身份组 ID
        /// </summary>
        [JsonProperty("role_id")]
        public string RoleId { get; set; }

        /// <summary>
        /// 权限位图 (十进制)
        /// </summary>
        [JsonProperty("permissions")]
        public string Permissions { get; set; }
    }
}

