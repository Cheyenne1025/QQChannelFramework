using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 删除日程
    /// </summary>
    public struct RawDeleteScheduleApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        public bool NeedParam => false;

        public string Url => "/channels/{channel_id}/schedules/{schedule_id}";

        public MethodType Method => MethodType.DELETE;
    }
}

