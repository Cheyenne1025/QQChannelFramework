using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

public struct RawGetMessagesApi : Base.IRawApiInfo {
    public string Version => "1.0";

    public string Url => "/channels/{channel_id}/messages";

    public MethodType Method => MethodType.GET;
}