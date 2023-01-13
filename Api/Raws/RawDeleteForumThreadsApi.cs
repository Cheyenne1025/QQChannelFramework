using MyBot.Api.Types;

namespace MyBot.Api.Raws;

/// <summary>
/// 源Api信息 - 删除论坛帖子
/// </summary>
public struct RawDeleteForumThreadsApi : Base.IRawApiInfo
{
    public string Version => "1.0";
    public string Url => "/channels/{channel_id}/threads/{thread_id}";
    public MethodType Method => MethodType.DELETE;
}
