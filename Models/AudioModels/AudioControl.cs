using ChannelModels.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQChannelFramework.Models.AudioModels
{
    public class AudioControl
    {
        /// <summary>
        /// 音频数据的url status为0时传
        /// </summary>
        [Newtonsoft.Json.JsonProperty("audio_url")]
        public string Url { get; set; }

        /// <summary>
        /// 状态文本（比如：简单爱-周杰伦），可选，status为0时传，其他操作不传
        /// </summary>
        [Newtonsoft.Json.JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// 播放状态
        /// </summary>
        [Newtonsoft.Json.JsonProperty("status")]
        public AudioStatus Status { get; set; }
    }
}
