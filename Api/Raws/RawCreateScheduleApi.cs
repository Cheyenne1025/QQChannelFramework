using MyBot.Api.Types;

namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 创建日程
/// </summary>
public struct RawCreateScheduleApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/channels/{channel_id}/schedules";

    public MethodType Method => MethodType.POST;
}