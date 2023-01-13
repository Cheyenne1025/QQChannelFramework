using MyBot.Api.Types;

namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 获取指定消息
/// </summary>
public struct RawGetMessageApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/channels/{channel_id}/messages/{message_id}";

    public MethodType Method => MethodType.GET;
}