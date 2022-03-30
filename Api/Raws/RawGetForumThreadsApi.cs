using QQChannelFramework.Api.Types;

namespace QQChannelFramework.Api.Raws;

/// <summary>
/// 源Api信息 - 获取论坛帖子列表
/// </summary>
public struct RawGetForumThreadsApi : Base.IRawApiInfo
{
    public string Version => "1.0";
    public string Url => "/channels/{channel_id}/threads";
    public MethodType Method => MethodType.GET;
}
