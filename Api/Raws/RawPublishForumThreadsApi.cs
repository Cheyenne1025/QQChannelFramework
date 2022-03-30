using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 发布帖子
/// </summary>
public struct RawPublishForumThreads : Base.IRawApiInfo
{
    public string Version => "1.0";
    public string Url => "/channels/{channel_id}/threads";
    public MethodType Method => MethodType.PUT;
}
