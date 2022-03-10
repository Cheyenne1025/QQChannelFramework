using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws; 

public struct RawUnPinMessageApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    public bool NeedParam => false;

    public string Url => "/channels/{channel_id}/pins/{message_id}";

    public MethodType Method => MethodType.DELETE;
}