using System;
using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 创建日程
/// </summary>
public struct RawCreateScheduleApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => true;

    public string Url => "/channels/{channel_id}/schedules";

    public MethodType Method => MethodType.POST;
}