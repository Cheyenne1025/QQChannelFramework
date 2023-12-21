using MyBot.Api.Types;
namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 获取论坛帖子详细
/// </summary>
public struct RawGetForumThreadsDetailApi : Base.IRawApiInfo
{
    public string Version => "1.0";
    public string Url => "/channels/{channel_id}/threads/{thread_id}";
    public MethodType Method => MethodType.GET;
}
