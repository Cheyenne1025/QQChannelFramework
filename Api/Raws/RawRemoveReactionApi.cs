using MyBot.Api.Types;
namespace MyBot.Api.Raws; 

/// <summary>
/// 删除自己的表情表态
/// </summary>
public struct RawRemoveReactionApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/channels/{channel_id}/messages/{message_id}/reactions/{type}/{id}";

    public MethodType Method => MethodType.DELETE;
}