using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 获取频道内所有日程
    /// </summary>
    public struct RawGetSchedulesApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        public bool NeedParam => false;

        public string Url => "/channels/{channel_id}/schedules";

        public MethodType Method => MethodType.GET;
    }
}

