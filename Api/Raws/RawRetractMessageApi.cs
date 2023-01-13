using MyBot.Api.Types;

namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 撤回消息
/// </summary>
public struct RawRetractMessageApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/channels/{channel_id}/messages/{message_id}";

    public MethodType Method => MethodType.DELETE;
}