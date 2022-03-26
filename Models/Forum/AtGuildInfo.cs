using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.Forum
{
    /// <summary>
    /// @频道信息
    /// </summary>
    public class AtGuildInfo
    {
        /// <summary>
        /// 频道ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("guild_id")]
        public string GuildId { get; set; }

        /// <summary>
        /// 频道名称
        /// </summary>
        [Newtonsoft.Json.JsonProperty("guild_name")]
        public string GuildName { get; set; }
    }
}
