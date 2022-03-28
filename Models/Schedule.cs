using System;
using Newtonsoft.Json;
using QQChannelFramework.Models.Types;
using QQChannelFramework.Tools.JsonConverters;

namespace QQChannelFramework.Models
{
    /// <summary>
    /// 日程对象
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// 日程Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 日程名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 日程描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 日程开始时间
        /// </summary>
        [JsonProperty("start_timestamp")]
        [JsonConverter(typeof(UnixSecondTimestampToDateTimeConverter))]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 日程结束时间
        /// </summary>
        [JsonProperty("end_timestamp")]
        [JsonConverter(typeof(UnixSecondTimestampToDateTimeConverter))]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [JsonProperty("creator")]
        public Member Creator { get; set; }

        /// <summary>
        /// 日程开始时跳转到的子频道 ID
        /// </summary>
        [JsonProperty("jump_channel_id")]
        public string JumpChannelId { get; set; }

        /// <summary>
        /// 日程提醒类型
        /// </summary>
        [JsonProperty("remind_type")]
        [JsonConverter(typeof(EnumToStringConverter<RemindType>))]
        public RemindType Type { get; set; }
    }
}

