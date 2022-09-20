using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 拉取对消息指定表情表态的用户列表
/// </summary>
public struct RawGetReactionsApi : Base.IRawApiInfo
{
    public string Version => "1.0";


    public string Url => "/channels/{channel_id}/messages/{message_id}/reactions/{type}/{id}";

    public MethodType Method => MethodType.GET;
}
