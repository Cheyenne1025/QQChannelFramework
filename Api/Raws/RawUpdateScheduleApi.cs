using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 修改日程
    /// </summary>
    public struct RawUpdateScheduleApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/channels/{channel_id}/schedules/{schedule_id}";

        public MethodType Method => MethodType.PATCH;
    }
}

