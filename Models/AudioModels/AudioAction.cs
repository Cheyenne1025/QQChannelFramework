using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.AudioModels
{
    /// <summary>
    /// 音频行为控制数据模型
    /// </summary>
    public class AudioAction
    {
        /// <summary>
        /// 频道id
        /// </summary>
        [Newtonsoft.Json.JsonProperty("guild_id")]
        public string GuildId { get; set; }

        /// <summary>
        /// 子频道id
        /// </summary>
        [Newtonsoft.Json.JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// 音频数据的url status为0时传
        /// </summary>
        [Newtonsoft.Json.JsonProperty("audio_url")]
        public string AudioUrl { get; set; }

        /// <summary>
        /// 状态文本（比如：简单爱-周杰伦），可选，status为0时传，其他操作不传
        /// </summary>
        [Newtonsoft.Json.JsonProperty("text")]
        public string Text { get; set; }
    }
}
