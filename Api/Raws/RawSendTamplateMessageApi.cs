using MyBot.Api.Types;
namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 发送模版信息
/// </summary>
public struct RawSendTamplateMessageApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/channels/{channel_id}/messages";

    public MethodType Method => MethodType.POST;
}