using MyBot.Api.Types;
namespace MyBot.Api.Raws
{
    /// <summary>
    /// 源Api信息 - 获取指定日程信息
    /// </summary>
    public struct RawGetScheduleApi : Base.IRawApiInfo
    {
        public string Version => "1.0";

        
        public string Url => "/channels/{channel_id}/schedules/{schedule_id}";

        public MethodType Method => MethodType.GET;
    }
}

