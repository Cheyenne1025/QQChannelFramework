using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws; 

public struct RawGetPinMessagesApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/channels/{channel_id}/pins";

    public MethodType Method => MethodType.GET;
}