using MyBot.Api.Types;

namespace MyBot.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 删除日程
    /// </summary>
    public struct RawDeleteScheduleApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/channels/{channel_id}/schedules/{schedule_id}";

        public MethodType Method => MethodType.DELETE;
    }
}

