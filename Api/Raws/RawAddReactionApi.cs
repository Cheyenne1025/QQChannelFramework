using MyBot.Api.Types;
namespace MyBot.Api.Raws; 

/// <summary>
/// 发表表情表态 
/// </summary>
public struct RawAddReactionApi : Base.IRawApiInfo
{
    public string Version => "1.0";
 
    public string Url => "/channels/{channel_id}/messages/{message_id}/reactions/{type}/{id}";

    public MethodType Method => MethodType.PUT;
}