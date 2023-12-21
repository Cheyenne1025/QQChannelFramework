using MyBot.Api.Types;
namespace MyBot.Api.Raws; 

public struct RawDirectSendMessageApi  : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/dms/{guild_id}/messages";

    public MethodType Method => MethodType.POST;
}